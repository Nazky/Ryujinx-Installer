Option Explicit On
Imports System.IO
Imports System.Net
Imports System.Net.WebRequestMethods
Imports System.IO.Compression
Imports System.Threading
Imports System.Security.Policy
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports ReaLTaiizor
Imports System.Text.Encoding
Imports System.Text

Public Class Main

    Private Sub HopeButton1_Click(sender As Object, e As EventArgs) Handles HopeButton1.Click
        Me.Close()
        About.Close()
        FR.Close()
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
        HopeTextBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Ryujinx-Unofficial"
        HopeTextBox1.Enabled = False
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        About.Show()
    End Sub

    Private Sub HopeButton2_Click(sender As Object, e As EventArgs) Handles HopeButton2.Click
        Try
            If HopeButton2.Text = "INSTALL" Then
                HopeButton2.Visible = False
                HopeCheckBox1.Visible = False
                CircleProgressBar1.Visible = True
                Label3.Visible = True
                Directory.CreateDirectory(HopeTextBox1.Text)
                If Directory.Exists(HopeTextBox1.Text & "\Ryujinx") Then
                    If Directory.Exists(HopeTextBox1.Text & "\Ryujinx-bak") Then
                        Directory.Delete(HopeTextBox1.Text & "\Ryujinx-bak", True)
                        Directory.Move(HopeTextBox1.Text & "\Ryujinx", HopeTextBox1.Text & "\Ryujinx-bak")
                    Else
                        Directory.Move(HopeTextBox1.Text & "\Ryujinx", HopeTextBox1.Text & "\Ryujinx-bak")
                    End If
                End If
                If Directory.Exists(HopeTextBox1.Text & "\Ryujinx-src") Then
                    Directory.Delete(HopeTextBox1.Text & "\Ryujinx-src", True)
                End If
                If HopeCheckBox1.Checked = True Then
                    HopeButton3.Enabled = False
                    checknet()
                    downloadgs("https://github.com/Ryujinx/Ryujinx/archive/refs/heads/master.zip", HopeTextBox1.Text & "\Ryujinx-src.zip")
                Else
                    HopeButton3.Enabled = False
                    downloadgs("https://github.com/Ryujinx/Ryujinx/archive/refs/heads/master.zip", HopeTextBox1.Text & "\Ryujinx-src.zip")
                End If
            Else
                Me.Hide()
                Dim sp As New ProcessStartInfo
                sp.WorkingDirectory = HopeTextBox1.Text
                sp.FileName = HopeTextBox1.Text & "\Ryujinx-Updater.exe"
                Process.Start(sp)
                Me.Close()
                About.Close()
                FR.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")
        End Try

    End Sub

    Sub downloadgs(url As String, fn As String)
        Try
            Using dl As New WebClient()
                AddHandler dl.DownloadProgressChanged, AddressOf pchanged
                AddHandler dl.DownloadFileCompleted, AddressOf done
                Me.Invoke(Sub() Label3.Text = "Download SRC from GitHub...")
                dl.DownloadFileAsync(New Uri(url), fn)
                'Me.Invoke(Sub() CircleProgressBar1.Value += 10)
                'MsgBox(wb.Document.Body.InnerText)
                Dim request As WebRequest = WebRequest.Create("https://github.com/Ryujinx/Ryujinx")
                Using response As WebResponse = request.GetResponse()
                    Using reader As New StreamReader(response.GetResponseStream())
                        Dim html As String = reader.ReadToEnd()
                        IO.File.WriteAllText("gh", html)
                    End Using
                End Using
                'MsgBox(wb.Document.GetElementsByTagName("relative-time"))
                'MsgBox(extract(IO.File.ReadAllText("gh.html"), "<relative-time", "</relative-time>"))
                Dim strB64Decoded As String = extract(IO.File.ReadAllText("gh"), "<relative-time", "</relative-time>")
                Dim data As Byte() = System.Text.Encoding.UTF8.GetBytes(strB64Decoded)
                Dim strB64Encoded As String = System.Convert.ToBase64String(data)
                IO.File.WriteAllText(HopeTextBox1.Text & "\version", strB64Encoded)
                IO.File.Delete("gh")
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")
        End Try

    End Sub

    Function extract(source As String, start As String, ending As String)
        Return source.Substring(InStr(source, start, CompareMethod.Text) + start.Length - 1, InStr(source, ending) - (InStr(source, start, CompareMethod.Text) + start.Length))
    End Function


    Sub pchanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs)

        CircleProgressBar1.Value = e.ProgressPercentage
    End Sub

    Sub done(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        ' Notify if download is completed successfully
        If e.Error Is Nothing Then
            'MsgBox("Download completed!")
            Me.Invoke(Sub() Label3.Text = "SRC downloaded.")
            Dim t As New Thread(Sub() extractsrc(HopeTextBox1.Text & "\Ryujinx-src.zip", HopeTextBox1.Text & "\Ryujinx-src"))
            t.Start()

        Else
            MsgBox(e.Error.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")
        End If
    End Sub

    Sub extractsrc(zip As String, folder As String)
        Try
            'Directory.Delete(HopeTextBox1.Text & "\Ryujinx-src", True)
            Me.Invoke(Sub() Label3.Text = "Extract SRC...")
            Directory.CreateDirectory(HopeTextBox1.Text & "\Ryujinx-src")
            ZipFile.ExtractToDirectory(zip, folder)
            IO.File.Delete(zip)
            Me.Invoke(Sub() Label3.Text = "SRC extracted.")
            'CircleProgressBar1.Value += 10
            'Thread.Sleep(1000)
            compilesrc()
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

    Sub compilesrc()
        Try
            Me.Invoke(Sub() Label3.Text = "Compiling SRC... (PLEASE WAIT !)")
            'MsgBox("test")
            systemcmd("dotnet", "build -c Release -o build", HopeTextBox1.Text & "\Ryujinx-src\Ryujinx-master")
            Me.Invoke(Sub() Label3.Text = "SRC compiled.")
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
            Me.Invoke(Sub() Label3.Text = "Moving folder...")
            Directory.Move(HopeTextBox1.Text & "\Ryujinx-src\Ryujinx-master\build", HopeTextBox1.Text & "\Ryujinx")
            IO.File.WriteAllBytes(HopeTextBox1.Text & "\ReaLTaiizor.dll", My.Resources.ReaLTaiizor)
            IO.File.WriteAllBytes(HopeTextBox1.Text & "\Ryujinx-Updater.exe", My.Resources.Ryujinx_Updater)
            Me.Invoke(Sub() Label3.Text = "Folder moved.")
            Me.Invoke(Sub() Label3.Text = "Deleting garbage...")
            Directory.Delete(HopeTextBox1.Text & "\Ryujinx-src", True)
            Me.Invoke(Sub() Label3.Text = "Create shortcut...")
            'MsgBox(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Ruijinx.lnk " & HopeTextBox1.Text & "\Ryujinx\Ryujinx.Ava.exe")
            If IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Ryujinx.lnk") Then
                IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Ryujinx.lnk")
            End If
            IO.File.WriteAllText("s.bat", "mklink " & Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\Ryujinx.lnk " & HopeTextBox1.Text & "\Ryujinx-Updater.exe")
            Shell("s.bat", AppWinStyle.Hide, True)
            IO.File.Delete("s.bat")
            Me.Invoke(Sub() CircleProgressBar1.Visible = False)
            Me.Invoke(Sub() CircleProgressBar1.Value = 0)
            Me.Invoke(Sub() HopeButton2.Text = "LAUNCH")
            Me.Invoke(Sub() HopeButton2.Visible = True)
            Me.Invoke(Sub() HopeButton2.Refresh())
            Me.Invoke(Sub() Label3.Text = "Installation done.")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")

        End Try

    End Sub

    Sub checknet()
        Try
            IO.File.WriteAllText("s.bat", "dotnet --list-runtimes > r.txt")
            Shell("s.bat", AppWinStyle.Hide, True)
            Dim r As String = IO.File.ReadAllText("r.txt")
            If r.Contains("Microsoft.NETCore.App 7") Then
                IO.File.Delete("r.txt")
                IO.File.Delete("s.bat")
                Me.Invoke(Sub() Label3.Text = ".net 7 Desktop Runtime found.")
                'Me.Invoke(Sub() CircleProgressBar1.Value += 10)
            Else
                'MsgBox(".net 5.0 Runtime not found please install it first !", MsgBoxStyle.Critical, "PS22PS4-GUI")
                Me.Invoke(Sub() Label3.Text = ".net 7 Desktop Runtime not found.")
                Me.Invoke(Sub() Me.WindowState = FormWindowState.Minimized)
                Shell("winget install Microsoft.DotNet.DesktopRuntime.7 -h", AppWinStyle.NormalFocus, True)
                Shell("winget install Microsoft.DotNet.SDK.7 -h", AppWinStyle.NormalFocus, True)
                Me.Invoke(Sub() Me.WindowState = FormWindowState.Normal)
                Me.Invoke(Sub() Me.Invoke(Sub() Label3.Text = ".net 7 installed."))
                IO.File.Delete("r.txt")
                IO.File.Delete("s.bat")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Ryujinx-Installer")

        End Try


    End Sub

    Private Sub HopeButton3_Click(sender As Object, e As EventArgs) Handles HopeButton3.Click
        Dim f As New FolderBrowserDialog
        f.Description = "Choose installation folder"
        If f.ShowDialog = DialogResult.OK Then
            HopeTextBox1.Text = f.SelectedPath & "\Ryujinx-Unofficial"
        End If
    End Sub

End Class
