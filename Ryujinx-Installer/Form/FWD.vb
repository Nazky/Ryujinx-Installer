Imports System.IO
Imports System.Net
Imports System.Reflection.Emit
Imports System.Security.Policy
Imports System.Threading
Imports HtmlAgilityPack
Imports ReaLTaiizor.Controls
Imports ReaLTaiizor.Drawing.Poison.PoisonPaint.ForeColor

Public Class FWD
    Private MouseIsDown As Boolean = False
    Private MouseIsDownLoc As Point = Nothing
    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove

        If e.Button = MouseButtons.Left Then
            If MouseIsDown = False Then
                MouseIsDown = True
                MouseIsDownLoc = New Point(e.X, e.Y)
            End If

            Me.Location = New Point(Me.Location.X + e.X - MouseIsDownLoc.X, Me.Location.Y + e.Y - MouseIsDownLoc.Y)
        End If
    End Sub
    Private Sub Panel2_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel2.MouseUp
        MouseIsDown = False
    End Sub
    Private Sub FWD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getfwlist(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String("aHR0cHM6Ly9oZWwxLTEubWlycm9yLmxld2Qud3RmL2FyY2hpdmUvbmludGVuZG8vc3dpdGNoL2Zpcm13YXJlLw==")))
    End Sub
    Sub getfwlist(url As String)
        Try
            Dim web As New HtmlWeb()
            Dim doc As HtmlDocument = web.Load(url)

            Dim tables As HtmlNodeCollection = doc.DocumentNode.SelectNodes("//table")

            Dim rows As HtmlNodeCollection = tables(0).SelectNodes("//tr")
            For i As Integer = 0 To rows.Count - 1

                Dim cols As HtmlNodeCollection = rows(i).SelectNodes("//a")
                For j As Integer = 0 To cols.Count - 1

                    Dim value As String = cols(j).InnerText
                    If value.Contains("insert_drive_file") Then
                    ElseIf value.Contains("Firmware ") Then
                        If ForeverComboBox1.Items.Contains(value.Replace("folder_zip ", "").Replace(" F", "F")) Then
                        Else
                            ForeverComboBox1.Items.Add(value.Replace("folder_zip ", "").Replace(" F", "F"))
                        End If
                    End If

                Next
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Firmware-Downloader")
        End Try

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub HopeButton1_Click(sender As Object, e As EventArgs) Handles HopeButton1.Click
        Me.Hide()
    End Sub

    Private Sub HopeButton2_Click(sender As Object, e As EventArgs) Handles HopeButton2.Click
        Try
            If ForeverComboBox1.Text = "" Then
                MsgBox("Please choose a firmware first !", MsgBoxStyle.Critical, "Firmware-Downloader")
            Else
                Dim sfw As New SaveFileDialog
                sfw.FileName = ForeverComboBox1.Text
                sfw.Title = "Choose where you want to save the firmware file."
                sfw.AddExtension = True
                sfw.Filter = "ZIP File|*.zip"
                If sfw.ShowDialog = DialogResult.OK Then
                    Using dl As New WebClient()
                        HopeProgressBar1.Visible = True
                        HopeButton2.Enabled = False
                        ForeverComboBox1.Enabled = False
                        AddHandler dl.DownloadProgressChanged, AddressOf pchanged
                        AddHandler dl.DownloadFileCompleted, AddressOf done
                        dl.DownloadFileAsync(New Uri(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String("aHR0cHM6Ly9oZWwxLTEubWlycm9yLmxld2Qud3RmL2FyY2hpdmUvbmludGVuZG8vc3dpdGNoL2Zpcm13YXJlLw==")) & ForeverComboBox1.Text), sfw.FileName)
                    End Using
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Firmware-Downloader")
        End Try
    End Sub

    Sub pchanged(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs)
        HopeProgressBar1.ValueNumber = e.ProgressPercentage
    End Sub

    Sub done(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        ' Notify if download is completed successfully
        If e.Error Is Nothing Then
            MsgBox("Download completed !", MsgBoxStyle.Information, "Firmware-Downloader")
        Else
            MsgBox(e.Error.Message, MsgBoxStyle.Critical, "Firmware-Downloader")
        End If
    End Sub
End Class