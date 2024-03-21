Imports System.Net

Public Class RyujinxReleaseChecker

    Public Shared Async Function GetLatestVersion() As Task(Of String)
        Dim releaseUrl As String = "https://github.com/Ryujinx/release-channel-master/releases/latest"
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


End Class
