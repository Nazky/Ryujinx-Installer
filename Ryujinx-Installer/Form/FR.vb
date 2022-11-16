Imports System.IO
Public Class FR

    Private Sub FR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MsgBox(Application.ExecutablePath)
        Me.Hide()
        If Application.StartupPath = System.IO.Path.GetTempPath & "Ryujinx-Installer" Then
            Main.Show()
            Me.Close()
        Else
            Directory.CreateDirectory(System.IO.Path.GetTempPath & "\Ryujinx-Installer")
            File.Copy(Application.ExecutablePath, System.IO.Path.GetTempPath & "\Ryujinx-Installer\Ryujinx-Installer.exe", True)
            File.WriteAllBytes(System.IO.Path.GetTempPath & "\Ryujinx-Installer\MaterialSkin.dll", My.Resources.MaterialSkin)
            File.WriteAllBytes(System.IO.Path.GetTempPath & "\Ryujinx-Installer\ReaLTaiizor.dll", My.Resources.ReaLTaiizor)
            Process.Start(System.IO.Path.GetTempPath & "\Ryujinx-Installer\Ryujinx-Installer.exe")
            Me.Close()
        End If
    End Sub
End Class