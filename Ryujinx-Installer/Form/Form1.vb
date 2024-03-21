Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Net
Imports System.Threading.Tasks

Public Class Form1

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim programFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
    Private ReadOnly downloader As New RyujinxReleaseDownloader()
    Private ReadOnly extractor As New RyujinxZipExtractor()


    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        drag = True ' Start dragging
        mousex = Windows.Forms.Cursor.Position.X - Me.Left ' Record the offset of the mouse pointer relative to the form's left edge
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top ' Record the offset of the mouse pointer relative to the form's top edge
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If drag Then ' If dragging is in progress
            Me.Left = Windows.Forms.Cursor.Position.X - mousex ' Set the new left position of the form
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey ' Set the new top position of the form
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        drag = False ' Stop dragging
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MakeRoundedForm(Me, 12)
        TextBox1.Text = Path.Combine(programFilesPath, "Ryujinx")
        If InternetChecker.IsConnected() Then
            Console.WriteLine("Internet connection is available.")
        Else
            Console.WriteLine("No internet connection.")
            MsgBox("You need to have a internet connection to use this software !", MsgBoxStyle.Critical)
            Me.Close()
        End If
    End Sub

    ' Function to make the form rounded
    Private Sub MakeRoundedForm(form As Form, radius As Integer)
        ' Create a GraphicsPath object to define the shape
        Dim path As New GraphicsPath()

        ' Create a rounded rectangle
        Dim bounds As Rectangle = form.ClientRectangle
        bounds.Inflate(-1, -1) ' Adjust for borders
        Dim diameter As Integer = 2 * radius
        Dim arc As New Rectangle(bounds.Location, New Size(diameter, diameter))

        ' Add the arcs to the path
        path.AddArc(arc, 180, 90)
        arc.X = bounds.Right - diameter
        path.AddArc(arc, 270, 90)
        arc.Y = bounds.Bottom - diameter
        path.AddArc(arc, 0, 90)
        arc.X = bounds.Left
        path.AddArc(arc, 90, 90)
        path.CloseFigure()

        ' Apply the new region to the form
        form.Region = New Region(path)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub RyujinxProgressBar1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Async Sub RoundButton1_Click(sender As Object, e As EventArgs) Handles RoundButton1.Click
        Try
            Panel2.Visible = False
            Await downloader.DownloadLatestVersionAsync(RyujinxProgressBar1)
            Dim extractionPath As String = TextBox1.Text
            Dim programName As String = "Ryujinx"
            Dim installLocation As String = extractionPath
            Dim directoryPath As String = Application.StartupPath
            Dim prefixToDelete As String = "Ryujinx_"
            If Directory.Exists(extractionPath) Then
                If Directory.Exists(Path.Combine(extractionPath, "publish")) Then
                    Directory.Delete(Path.Combine(extractionPath, "publish"), True)
                End If
                If File.Exists(Path.Combine(extractionPath, "cv")) Then
                    File.Delete(Path.Combine(extractionPath, "cv"))
                End If
                If File.Exists(Path.Combine(extractionPath, "ip")) Then
                    File.Delete(Path.Combine(extractionPath, "ip"))
                End If
            Else
                Try
                    Directory.CreateDirectory(extractionPath)
                Catch ex As Exception
                    MsgBox("Make sure to run the installer in admin !", MsgBoxStyle.Critical)
                    Me.Close()
                End Try
            End If
            Label4.Text = "Extract zip..."
            Await extractor.ExtractZipAsync(extractionPath, RyujinxProgressBar1)
            Label4.Text = "Execute extra little steps..."
            SoftwareInstaller.InstallSoftware(programName, installLocation)
            File.Move("cv", Path.Combine(extractionPath, "cv"))
            File.WriteAllText(Path.Combine(extractionPath, "ip"), TextBox1.Text)
            FileDeleter.DeleteFilesWithPrefix(directoryPath, prefixToDelete)
            Panel3.Visible = True
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub RoundButton2_Click(sender As Object, e As EventArgs) Handles RoundButton2.Click
        Try
            Dim rif As New FolderBrowserDialog
            rif.Description = "Choose a path to install Ryujinx"
            If rif.ShowDialog = DialogResult.OK Then
                TextBox1.Text = Path.Combine(rif.SelectedPath, "Ryujinx")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
