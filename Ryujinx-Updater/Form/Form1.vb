Imports System.IO
Imports System.Net
Imports System.Reflection.Emit
Imports System.Threading.Tasks
Public Class Form1
    Private ReadOnly downloader As New RyujinxReleaseDownloader()
    Private ReadOnly extractor As New RyujinxZipExtractor()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If InternetChecker.IsConnected() Then
                Console.WriteLine("Internet connection is available.")
                checkfornewversion()

            Else
                Console.WriteLine("No internet connection.")
                Label1.Text = "No internet connection."
                Process.Start(Path.Combine(Application.StartupPath, "publish\Ryujinx.exe"))
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Async Sub checkfornewversion()
        Try
            Dim nv As String = Await RyujinxReleaseChecker.GetLatestVersion()
            If nv IsNot Nothing Then
                Console.WriteLine("Latest version: " & nv)
            Else
                Console.WriteLine("Failed to retrieve latest version.")
            End If
            Dim cv As String = File.ReadAllText(Path.Combine(Application.StartupPath, "cv"))
            Dim directoryPath As String = File.ReadAllText(Path.Combine(Application.StartupPath, "ip"))
            Dim prefixToDelete As String = "Ryujinx_"
            'MsgBox($"Current version: {cv} {vbCrLf}New version: {nv}")
            If cv = nv.ToString Then
                Process.Start(Path.Combine(Application.StartupPath, "publish\Ryujinx.exe"))
                Me.Close()
            Else
                Dim up = MsgBox("New update avaible, download it ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)
                If up = DialogResult.Yes Then
                    Label1.Text = "Download latest version..."
                    Await downloader.DownloadLatestVersionAsync(RyujinxProgressBar1)
                    Label1.Text = "Extract zip..."
                    If Directory.Exists("publish") Then
                        Directory.Delete("publish", True)
                    End If
                    Await extractor.ExtractZipAsync(Application.StartupPath, RyujinxProgressBar1)
                    Label1.Text = "Done"
                    MsgBox("Update done.", MsgBoxStyle.Information)
                    FileDeleter.DeleteFilesWithPrefix(directoryPath, prefixToDelete)
                    File.WriteAllText(Path.Combine(Application.StartupPath, "cv"), nv)
                    Process.Start(Path.Combine(Application.StartupPath, "publish\Ryujinx.exe"))
                    Me.Close()
                Else
                    Process.Start(Path.Combine(Application.StartupPath, "publish\Ryujinx.exe"))
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub
End Class
