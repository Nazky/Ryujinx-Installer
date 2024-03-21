Imports Microsoft.Win32
Imports System.IO

Public Class SoftwareInstaller

    Public Shared Sub InstallSoftware(programName As String, installLocation As String)
        If Form1.CheckBox3.Checked = True Then
            ' Add the program to the installed programs list
            AddToInstalledPrograms(programName, installLocation, Path.Combine(Form1.TextBox1.Text, "Ryujinx-Logo.ico"))
        End If

        If Form1.CheckBox1.Checked = True Then
            ' Create a desktop shortcut
            CreateDesktopShortcut(programName, installLocation)
        End If

        If Form1.CheckBox2.Checked = True Then
            ' Add to Start menu
            AddToStartMenu(programName, installLocation)
        End If
        Try
            File.WriteAllBytes(Path.Combine(Form1.TextBox1.Text, "Ryujinx-Uninstaller.exe"), My.Resources.Ryujinx_Uninstaller)

            File.WriteAllBytes(Path.Combine(Form1.TextBox1.Text, "Ryujinx-Updater.exe"), My.Resources.Ryujinx_Updater)

            Dim icon As Icon = My.Resources.Ryujinx_Logo

            Dim tempFilePath As String = Path.GetTempFileName()
            Using fs As New FileStream(tempFilePath, FileMode.Create)
                icon.Save(fs)
            End Using

            Dim iconBytes() As Byte = File.ReadAllBytes(tempFilePath)

            File.Delete(tempFilePath)

            File.WriteAllBytes(Path.Combine(Form1.TextBox1.Text, "Ryujinx-Logo.ico"), iconBytes)
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub UninstallSoftware(programName As String)
        ' Remove from installed programs list, Start menu, and desktop shortcut
        RemoveFromInstalledPrograms(programName)
        RemoveFromStartMenu(programName)
        RemoveDesktopShortcut(programName)

        ' Run uninstall.exe
        'RunUninstallExe(programName)

        ' Delete installation directory
        DeleteInstallationDirectory(programName)
    End Sub

    Private Shared Sub AddToInstalledPrograms(programName As String, installLocation As String, iconPath As String)
        Dim uninstallKey As RegistryKey = Nothing
        Dim programKey As RegistryKey = Nothing

        Try
            uninstallKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall", True)
            If uninstallKey IsNot Nothing Then
                programKey = uninstallKey.CreateSubKey(programName)
                If programKey IsNot Nothing Then
                    programKey.SetValue("DisplayName", programName)
                    programKey.SetValue("UninstallString", $"cmd /c ""{installLocation}\Ryujinx-Uninstaller.exe""")
                    programKey.SetValue("InstallLocation", installLocation)
                    programKey.SetValue("DisplayIcon", $"{installLocation}\Ryujinx-Logo.ico")
                Else
                    ' Handle the case where programKey is null
                End If
            Else
                ' Handle the case where uninstallKey is null
            End If
        Catch ex As Exception
            ' Handle or log the exception
        Finally
            If programKey IsNot Nothing Then
                programKey.Close()
            End If
            If uninstallKey IsNot Nothing Then
                uninstallKey.Close()
            End If
        End Try
    End Sub


    Private Shared Sub RemoveFromInstalledPrograms(programName As String)
        Dim uninstallKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall", True)
        uninstallKey.DeleteSubKeyTree(programName, False)
        uninstallKey.Close()
    End Sub

    Private Shared Sub CreateDesktopShortcut(programName As String, installLocation As String)
        Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        Dim shortcutPath As String = Path.Combine(desktopPath, $"{programName}.lnk")

        Dim shell As New IWshRuntimeLibrary.WshShell()
        Dim shortcut As IWshRuntimeLibrary.IWshShortcut = DirectCast(shell.CreateShortcut(shortcutPath), IWshRuntimeLibrary.IWshShortcut)
        shortcut.TargetPath = Path.Combine(installLocation, "Ryujinx-Updater.exe")
        shortcut.Save()
    End Sub

    Private Shared Sub RemoveDesktopShortcut(programName As String)
        Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        Dim shortcutPath As String = Path.Combine(desktopPath, $"{programName}.lnk")

        If File.Exists(shortcutPath) Then
            File.Delete(shortcutPath)
        End If
    End Sub

    Private Shared Sub AddToStartMenu(programName As String, installLocation As String)
        Dim startMenuPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs")
        Dim shortcutPath As String = Path.Combine(startMenuPath, $"{programName}.lnk")

        Dim shell As New IWshRuntimeLibrary.WshShell()
        Dim shortcut As IWshRuntimeLibrary.IWshShortcut = DirectCast(shell.CreateShortcut(shortcutPath), IWshRuntimeLibrary.IWshShortcut)
        shortcut.TargetPath = Path.Combine(installLocation, "Ryujinx-Updater.exe")
        shortcut.Save()
    End Sub

    Private Shared Sub RemoveFromStartMenu(programName As String)
        Dim startMenuPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs")
        Dim shortcutPath As String = Path.Combine(startMenuPath, $"{programName}.lnk")

        If File.Exists(shortcutPath) Then
            File.Delete(shortcutPath)
        End If
    End Sub

    Private Shared Sub RunUninstallExe(programName As String)
        Dim installLocation As String = GetInstallLocation(programName)

        If installLocation IsNot Nothing Then
            Dim uninstallExePath As String = Path.Combine(installLocation, "Ryujinx-Uninstaller.exe")

            If File.Exists(uninstallExePath) Then
                Dim processStartInfo As New ProcessStartInfo(uninstallExePath)
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden

                Try
                    Process.Start(processStartInfo).WaitForExit()
                Catch ex As Exception
                    ' Handle any exceptions
                End Try
            End If
        End If
    End Sub

    Private Shared Function GetInstallLocation(programName As String) As String
        Dim uninstallKey As RegistryKey = Nothing
        Dim programKey As RegistryKey = Nothing

        Try
            uninstallKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall")
            If uninstallKey IsNot Nothing Then
                programKey = uninstallKey.OpenSubKey(programName)
                If programKey IsNot Nothing Then
                    Dim installLocation As Object = programKey.GetValue("InstallLocation")
                    If installLocation IsNot Nothing AndAlso TypeOf installLocation Is String Then
                        Return installLocation.ToString()
                    End If
                End If
            End If
        Catch ex As Exception
            ' Log or handle the exception
        Finally
            If programKey IsNot Nothing Then
                programKey.Close()
            End If
            If uninstallKey IsNot Nothing Then
                uninstallKey.Close()
            End If
        End Try

        Return Nothing ' Return null if installation location is not found
    End Function

    Private Shared Sub DeleteInstallationDirectory(programName As String)
        Dim installLocation As String = File.ReadAllText("ip")
        Console.WriteLine($"Installation path: {installLocation}")

        If installLocation IsNot Nothing Then
            Try
                ' Ensure the installation directory exists
                If Directory.Exists(installLocation) Then
                    ' Delete the installation directory itself
                    Directory.Delete(installLocation, True)
                End If
            Catch ex As Exception
                ' Handle any errors
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

End Class
