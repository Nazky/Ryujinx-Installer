Imports System.IO

Public Class FileDeleter

    Public Shared Sub DeleteFilesWithPrefix(directoryPath As String, prefix As String)
        Try
            ' Get files in the directory that start with the specified prefix
            Dim filesToDelete As String() = Directory.GetFiles(directoryPath, prefix & "*")

            ' Loop through the files and delete them
            For Each fileToDelete As String In filesToDelete
                File.Delete(fileToDelete)
            Next

            Console.WriteLine("Files with prefix '" & prefix & "' deleted successfully.")
        Catch ex As Exception
            Console.WriteLine("Error deleting files: " & ex.Message)
        End Try
    End Sub

End Class
