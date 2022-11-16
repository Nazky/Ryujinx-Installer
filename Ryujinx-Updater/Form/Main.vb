Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Security.Policy
Imports System.Text
Imports System.Threading
Imports ReaLTaiizor.Controls

Public Class Main
    Private Sub HopeButton1_Click(sender As Object, e As EventArgs) Handles HopeButton1.Click
        Me.Close()
    End Sub

    Private MouseIsDown As Boolean = False
    Private MouseIsDownLoc As Point = Nothing
    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove

        If e.Button = MouseButtons.Left Then
            If MouseIsDown = False Then
                MouseIsDown = True
                MouseIsDownLoc = New Point(e.X, e.Y)
            End If

            Me.Location = New Point(Me.Location.X + e.X - MouseIsDownLoc.X, Me.Location.Y + e.Y - MouseIsDownLoc.Y)
        End If
    End Sub
    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        MouseIsDown = False
    End Sub


    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists(Application.StartupPath & "\gh") Then
            File.Delete(Application.StartupPath & "\gh")
        End If
        deletemp()
        checkupdate()
    End Sub

    Sub deletemp()
        If Directory.Exists(System.IO.Path.GetTempPath & "\Ryujinx-Installer") Then
            Directory.Delete(System.IO.Path.GetTempPath & "\Ryujinx-Installer", True)
        End If
    End Sub

    Sub checkupdate()
        Try
            Dim strB64Encoded As String = System.IO.File.ReadAllText(Application.StartupPath & "\version")
            Dim data As Byte() = Convert.FromBase64String(strB64Encoded)
            Dim strB64Decoded As String = System.Text.Encoding.UTF8.GetString(data)
            Dim request As WebRequest = WebRequest.Create("https://github.com/Ryujinx/Ryujinx")
            Using response As WebResponse = request.GetResponse()
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim html As String = reader.ReadToEnd()
                    IO.File.WriteAllText(Application.StartupPath & "\gh", html)
                End Using
            End Using

            If extract(IO.File.ReadAllText(Application.StartupPath & "\gh"), "<relative-time", "</relative-time>") = strB64Decoded Then
                Label3.Text = "No update found."
                Me.Hide()
                Process.Start(Application.StartupPath & "\Ryujinx\Ryujinx.Ava.exe")
                Me.Close()
            Else
                Label3.Text = "Update found."
                Label3.Visible = False
                Label2.Visible = True
                CircleProgressBar1.Visible = True
                If Directory.Exists(Application.StartupPath & "\Ryujinx") Then
                    If Directory.Exists(Application.StartupPath & "\Ryujinx-bak") Then
                        Directory.Delete(Application.StartupPath & "\Ryujinx-bak", True)
                        Directory.Move(Application.StartupPath & "\Ryujinx", Application.StartupPath & "\Ryujinx-bak")
                    Else
                        Directory.Move(Application.StartupPath & "\Ryujinx", Application.StartupPath & "\Ryujinx-bak")
                    End If
                End If
                If Directory.Exists(Application.StartupPath & "\Ryujinx-src") Then
                    Directory.Delete(Application.StartupPath & "\Ryujinx-src", True)
                End If
                downloadgs("https://github.com/Ryujinx/Ryujinx/archive/refs/heads/master.zip", Application.StartupPath & "\Ryujinx-src.zip")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")
        End Try

    End Sub

    Function extract(source As String, start As String, ending As String)
        Return source.Substring(InStr(source, start, CompareMethod.Text) + start.Length - 1, InStr(source, ending) - (InStr(source, start, CompareMethod.Text) + start.Length))
    End Function

    Sub downloadgs(url As String, fn As String)
        Try
            Using dl As New WebClient()
                AddHandler dl.DownloadProgressChanged, AddressOf pchanged
                AddHandler dl.DownloadFileCompleted, AddressOf done
                Me.Invoke(Sub() Label2.Text = "Download SRC from GitHub...")
                dl.DownloadFileAsync(New Uri(url), fn)
                'Me.Invoke(Sub() CircleProgressBar1.Value += 10)
                'MsgBox(wb.Document.Body.InnerText)
                Dim request As WebRequest = WebRequest.Create("https://github.com/Ryujinx/Ryujinx")
                Using response As WebResponse = request.GetResponse()
                    Using reader As New StreamReader(response.GetResponseStream())
                        Dim html As String = reader.ReadToEnd()
                        IO.File.WriteAllText(Application.StartupPath & "\gh", html)
                    End Using
                End Using
                'MsgBox(wb.Document.GetElementsByTagName("relative-time"))
                'MsgBox(extract(IO.File.ReadAllText("gh.html"), "<relative-time", "</relative-time>"))
                Dim strB64Decoded As String = extract(IO.File.ReadAllText(Application.StartupPath & "\gh"), "<relative-time", "</relative-time>")
                Dim data As Byte() = System.Text.Encoding.UTF8.GetBytes(strB64Decoded)
                Dim strB64Encoded As String = System.Convert.ToBase64String(data)
                IO.File.WriteAllText(Application.StartupPath & "\version", strB64Encoded)
                IO.File.Delete(Application.StartupPath & "\gh")
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")
        End Try

    End Sub

    Sub pchanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs)

        CircleProgressBar1.Value = e.ProgressPercentage
    End Sub

    Sub done(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        ' Notify if download is completed successfully
        If e.Error Is Nothing Then
            'MsgBox("Download completed!")
            Me.Invoke(Sub() Label2.Text = "SRC downloaded.")
            Dim t As New Thread(Sub() extractsrc(Application.StartupPath & "\Ryujinx-src.zip", Application.StartupPath & "\Ryujinx-src"))
            t.Start()

        Else
            MsgBox(e.Error.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")
        End If
    End Sub

    Sub extractsrc(zip As String, folder As String)
        Try
            'Directory.Delete(HopeTextBox1.Text & "\Ryujinx-src", True)
            Me.Invoke(Sub() Label3.Text = "Extract SRC...")
            Directory.CreateDirectory(Application.StartupPath & "\Ryujinx-src")
            ZipFile.ExtractToDirectory(zip, folder)
            IO.File.Delete(zip)
            Me.Invoke(Sub() Label2.Text = "SRC extracted.")
            'CircleProgressBar1.Value += 10
            'Thread.Sleep(1000)
            compilesrc()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")
        End Try

    End Sub

    Sub compilesrc()
        Try
            Me.Invoke(Sub() Label2.Text = "Compiling SRC... (PLEASE WAIT !)")
            'MsgBox("test")
            systemcmd("dotnet", "build -c Release -o build", Application.StartupPath & "\Ryujinx-src\Ryujinx-master")
            Me.Invoke(Sub() Label2.Text = "SRC compiled.")
            For Each p As Process In System.Diagnostics.Process.GetProcessesByName("dotnet")
                p.Kill()
            Next
            moverelease()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")
        End Try

    End Sub

    Sub moverelease()
        Try
            Me.Invoke(Sub() Label2.Text = "Moving folder...")
            Directory.Move(Application.StartupPath & "\Ryujinx-src\Ryujinx-master\build", Application.StartupPath & "\Ryujinx")
            Me.Invoke(Sub() Label2.Text = "Folder moved.")
            Me.Invoke(Sub() Label2.Text = "Deleting garbage...")
            Directory.Delete(Application.StartupPath & "\Ryujinx-src", True)
            Me.Invoke(Sub() CircleProgressBar1.Visible = False)
            Me.Invoke(Sub() CircleProgressBar1.Value = 0)
            Me.Invoke(Sub() Label2.Text = "Update done.")
            Me.Hide()
            Process.Start(Application.StartupPath & "\Ryujinx\Ryujinx.Ava.exe")
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")

        End Try

    End Sub

    Sub systemcmd(ByVal cmd As String, arg As String, Optional ByVal wd As String = "")
        Dim pHelp As New ProcessStartInfo
        pHelp.FileName = cmd
        pHelp.Arguments = arg
        pHelp.UseShellExecute = True
        pHelp.WindowStyle = ProcessWindowStyle.Hidden
        If wd = "" Then
        Else
            pHelp.WorkingDirectory = wd
        End If
        Dim proc As Process = Process.Start(pHelp)
        proc.WaitForExit()
    End Sub
End Class
