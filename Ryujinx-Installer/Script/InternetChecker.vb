Imports System.Net

Public Class InternetChecker
    Public Shared Function IsConnected() As Boolean
        Try
            ' Attempt to access a website
            Dim request As WebRequest = WebRequest.Create("http://www.google.com")
            Dim response As WebResponse = request.GetResponse()
            response.Close()
            Return True
        Catch ex As Exception
            ' If an exception occurs, it means there is no internet connection
            Return False
        End Try
    End Function
End Class
