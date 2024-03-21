Imports System.IO
Imports System.IO.Compression
Imports System.Threading.Tasks

Public Class RyujinxZipExtractor

    Public Async Function ExtractZipAsync(extractionPath As String, progressBar As RyujinxProgressBar) As Task
        Await Task.Run(Sub()
                           ' Find the zip file with a name starting with "Ryujinx"
                           Dim zipFiles() As String = Directory.GetFiles(Application.StartupPath, "Ryujinx*.zip")
                           If Directory.Exists(Path.Combine(extractionPath, "publish")) Then
                               Directory.Delete(Path.Combine(extractionPath, "publish"), True)
                           End If
                           If zipFiles.Length > 0 Then
                               ' Get the first matching zip file
                               Dim zipFilePath As String = zipFiles(0)

                               ' Create the extraction directory if it does not exist
                               If Not Directory.Exists(extractionPath) Then
                                   Directory.CreateDirectory(extractionPath)
                               End If

                               ' Extract the zip file
                               ZipFile.ExtractToDirectory(zipFilePath, extractionPath)

                               ' Update progress (assuming 100% completion)
                               UpdateProgressBar(progressBar, 1, 1)
                           Else
                               Throw New FileNotFoundException("No zip file starting with 'Ryujinx' found in the directory.")
                           End If
                       End Sub)
    End Function

    Private Sub UpdateProgressBar(progressBar As RyujinxProgressBar, totalEntries As Integer, extractedEntries As Integer)
        ' Calculate the percentage
        Dim percentage As Integer = CInt((extractedEntries / totalEntries) * 100) ' Divide by 3 for updating progress in thirds

        ' Update the progress bar
        If percentage <= 100 Then
            progressBar.Value = percentage
        End If
    End Sub
End Class
