Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Environment

Public Class Form1
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim programFilesPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)


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

    Private Sub RoundButton1_Click(sender As Object, e As EventArgs) Handles RoundButton1.Click
        Try
            Dim cd = MsgBox("Are you sure you want to unistall Ryujinx ?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation)
            If cd = DialogResult.Yes Then
                RoundButton1.Visible = False
                CheckBox1.Visible = False
                Dim programName As String = "Ryujinx"

                SoftwareInstaller.UninstallSoftware(programName)
                If CheckBox1.Checked = True Then
                    Directory.Delete(Path.Combine(GetFolderPath(SpecialFolder.ApplicationData), "Ryujinx"), True)
                End If
                Label3.Text = "Unistalled"
                MsgBox("Ryujinx as been unistall.", MsgBoxStyle.Information)
                Me.Close()
            Else
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
