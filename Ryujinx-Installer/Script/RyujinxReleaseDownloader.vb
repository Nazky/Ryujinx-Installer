Imports System.IO
Imports System.Net
Imports System.Threading.Tasks

Public Class RyujinxReleaseDownloader

    Private ReadOnly releaseUrl As String = "https://github.com/Ryujinx/release-channel-master/releases/latest"
    Private ReadOnly downloadUrlPattern As String = "https://github.com/Ryujinx/release-channel-master/releases/download/{0}/ryujinx-{0}-win_x64.zip"

    Public Async Function DownloadLatestVersionAsync(progressBar As RyujinxProgressBar) As Task
        ' Get the latest release version
        Dim latestVersion As String = Await GetLatestReleaseVersionAsync()
        Try
            If File.Exists("cv") Then
                File.Move("cv", "ov")
                File.WriteAllText("cv", latestVersion.ToString)
            Else
                File.WriteAllText("cv", latestVersion.ToString)
            End If
        Catch ex As Exception

        End Try

        'MsgBox(latestVersion.ToString)

        ' Download the latest release
        Await DownloadReleaseAsync(latestVersion, progressBar)
    End Function

    Private Async Function GetLatestReleaseVersionAsync() As Task(Of String)
        Dim response As HttpWebResponse = Nothing
        Dim latestVersion As String = Nothing

        Try
            ' Make a request to the release URL
            Dim request As HttpWebRequest = WebRequest.Create(releaseUrl)
            request.Method = "HEAD"
            response = CType(Await request.GetResponseAsync(), HttpWebResponse)

            ' Extract the version from the response URL
            Dim redirectedUrl As String = response.ResponseUri.AbsoluteUri
            latestVersion = redirectedUrl.Split("/"c).Last()
        Catch ex As Exception
            ' Handle any exceptions
            Console.WriteLine("Error retrieving latest release version: " & ex.Message)
        Finally
            response?.Close()
        End Try



        Return latestVersion
    End Function

    Private Async Function DownloadReleaseAsync(version As String, progressBar As RyujinxProgressBar) As Task
        Dim downloadUrl As String = String.Format(downloadUrlPattern, version)
        Dim downloadPath As String = Path.Combine(Application.StartupPath, $"Ryujinx_{version}.zip")

        Try
            ' Create a WebClient for downloading the release
            Dim webClient As New WebClient()
            AddHandler webClient.DownloadProgressChanged, Sub(sender, e)
                                                              ' Suspend layout to reduce flickering
                                                              progressBar.SuspendLayout()

                                                              ' Update the progress bar (divided by 2)
                                                              Dim percentage As Integer = e.ProgressPercentage
                                                              progressBar.Value = percentage

                                                              ' Resume layout after updating the progress bar
                                                              progressBar.ResumeLayout()
                                                          End Sub

            ' Download the release to a temporary file
            Await webClient.DownloadFileTaskAsync(New Uri(downloadUrl), downloadPath)

            ' Simulate extraction process (not implemented)
            ' You can add your extraction logic here

            ' Clear the progress bar when finished
            progressBar.Value = 0
        Catch ex As Exception
            ' Handle any exceptions
            Console.WriteLine("Error downloading release: " & ex.Message)
        End Try
    End Function

End Class
