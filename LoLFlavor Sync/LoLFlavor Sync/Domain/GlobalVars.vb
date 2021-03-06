﻿Imports Microsoft.Win32
Imports System.IO
Namespace Global.LoLFlavor_Sync.Domain
    ''' <summary>
    ''' Provides static properties and methods for LoLFlavor Sync.
    ''' </summary>
    Module GlobalVars
        Public Property AllChampions As List(Of Champion)

        Public Property VersionLocal As String = "1.8.3"
        Public Property VersionOnline As String
        Public Property VersionUrl As New Uri("https://raw.githubusercontent.com/JohandeGraaf/LoLFlavor-Sync/master/LoLFlavor%20Sync.version?rand=" & (New Random).Next(0, 9999), UriKind.Absolute)
        Public Property VersionLFS As New Uri("http://lolflavor.com/Api/buildFree/GetVersion", System.UriKind.Absolute)
        Public Property VersionFileName As String = "LoLFlavorSync.version"

        Public Property Garena As Boolean = False

        Public Property ChampionVar As String = "{Champion}"
        Public Property LaneVar As String = "{Lane}"
        Public Property LoLVar As String = "{LoLFolder}"

        Public Property LoLFlavorSourceUrlFormat As String = "http://www.lolflavor.com/champions/{Champion}/Recommended/{Champion}_{Lane}_scrape.json"

        Public Property RiotDestinationPathB As String = "\Config\Champions\"
        Public Property RiotDestinationPath As String = RiotDestinationPathB & ChampionVar & "\Recommended\"
        Public Property RiotDestinationFile As String = ChampionVar & "_" & LaneVar & "_scrape.json"
        Public Property GarenaDestinationPathBBase As String = "\GameData\Apps\" & LoLVar & "\Game\DATA\Characters\"
        Public Property GarenaDestinationPathBase As String = ChampionVar & "\Recommended\"
        Public Property GarenaDestinationPathB As String
        Public Property GarenaDestinationPath As String
        Public Property GarenaDestinationFile As String = ChampionVar & "_" & LaneVar & "_scrape.json"

        Public Property LoLPath As LoLPath = LoLPath.EMPTY

        Public Property RegKey As RegistryKey = Registry.CurrentUser
        Public Property RegPath As String = "Software\FlavorSync"

        Public Property UrlExecutable As String = "http://lolflavorsync.mcpvp.eu"
        Public Property UrlBoLProfile As String = "http://botoflegends.com/forum/user/59209-ampersand/"
        Public Property UrlSkype As String = "skype:johan95pb?chat"
        Public Property UrlGitHub As String = "https://github.com/JohandeGraaf"

        Public Property Changelog As String =
            "1.0.0 - Release." & Environment.NewLine &
            "1.1.0 - Fixed some bugs." & Environment.NewLine &
            "1.2.0 - Added some settings." & Environment.NewLine &
            Environment.NewLine &
            "1.5.0 - Changed the download method to improve performance." & Environment.NewLine &
            "1.6.0 - Added settings to add champions and add a custom source for builds." & Environment.NewLine &
            "1.6.1 - Added Azir and Gnar." & Environment.NewLine &
            "1.6.2 - Added update message." & Environment.NewLine &
            "1.6.3 - Added Kalista." & Environment.NewLine &
            "1.6.4 - Added Rek'Sai." & Environment.NewLine &
            "1.7.0 - Recoded almost everything; improved performance; less bugs." & Environment.NewLine &
            "1.7.1 - Added Garena support; Shows when LoLFlavor.com was last updated; " & Environment.NewLine &
            "           Option to remove all old builds, or overwrite them; Improved build install." & Environment.NewLine &
            "1.7.2 - Added Bard and small fixes." & Environment.NewLine &
            "1.7.3 - Added Ekko." & Environment.NewLine &
            "1.7.4 - Fixed crash when selecting Garena and no directory is found." & Environment.NewLine &
            "1.7.5 - Added Tahm Kench." & Environment.NewLine &
            "1.7.51 - Fixed: Not connected to internet bug." & Environment.NewLine &
            "1.7.6 - Added Kindred and changed UI a bit." & Environment.NewLine &
            "1.7.7 - Added Illaoi." & Environment.NewLine &
            "1.7.8 - Added Jhin." & Environment.NewLine &
            "1.7.9 - Added Aurelion Sol." & Environment.NewLine &
            "1.8.0 - Added Taliyah" & Environment.NewLine &
            "1.8.1 - Added Kled" & Environment.NewLine &
            "1.8.2 - Added Camile & Ivern" & Environment.NewLine &
            "1.8.3 - Added Xayah & Rakan," & Environment.NewLine &
            "           Champion list is now loaded from GitHub instead of being hardcoded."

        Public Property About As String =
            "LoLFlavor Sync - Version " & VersionLocal & Environment.NewLine &
            Environment.NewLine &
            "Copyright © 2014-2017 - Johan de Graaf"

        Public Property OptionSkipForm As Boolean
            Get
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath)
                        Return Convert.ToBoolean(key.GetValue("skip"))
                    End Using
                Catch ex As Exception
                    Return Nothing
                End Try
            End Get
            Set(value As Boolean)
                CreateSubKey()
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath, True)
                        key.SetValue("skip", value.ToString(), RegistryValueKind.String)
                    End Using
                Catch ex As UnauthorizedAccessException
                    MessageBox.Show("Error: Not enough permissions to write to " & RegKey.ToString() & Environment.NewLine & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End Set
        End Property

        Public Property OptionLastUsed As Date
            Get
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath)
                        Return Date.FromBinary(key.GetValue("lastUsedBinary"))
                    End Using
                Catch ex As Exception
                    Return Nothing
                End Try
            End Get
            Set(value As Date)
                CreateSubKey()
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath, True)
                        key.SetValue("lastUsedBinary", value.ToBinary(), RegistryValueKind.QWord)
                    End Using
                Catch ex As UnauthorizedAccessException
                    MessageBox.Show("Error: Not enough permissions to write to " & RegKey.ToString() & Environment.NewLine & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End Set
        End Property

        Public Property OptionVersionCheckDisabled As Boolean
            Get
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath)
                        Return Convert.ToBoolean(key.GetValue("versionCheckDisabled"))
                    End Using
                Catch ex As Exception
                    Return Nothing
                End Try
            End Get
            Set(value As Boolean)
                CreateSubKey()
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath, True)
                        key.SetValue("versionCheckDisabled", value.ToString(), RegistryValueKind.String)
                    End Using
                Catch ex As UnauthorizedAccessException
                    MessageBox.Show("Error: Not enough permissions to write to " & RegKey.ToString() & Environment.NewLine & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End Set
        End Property

        Public Property OptionLoLPath As String
            Get
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath)
                        Return key.GetValue("loLPath")
                    End Using
                Catch ex As Exception
                    Return Nothing
                End Try
            End Get
            Set(value As String)
                CreateSubKey()
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath, True)
                        key.SetValue("loLPath", value, RegistryValueKind.String)
                    End Using
                Catch ex As UnauthorizedAccessException
                    MessageBox.Show("Error: Not enough permissions to write to " & RegKey.ToString() & Environment.NewLine & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End Set
        End Property

        Public Property OptionUseGarena As Boolean
            Get
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath)
                        Return Convert.ToBoolean(key.GetValue("useGarena"))
                    End Using
                Catch ex As Exception
                    Return Nothing
                End Try
            End Get
            Set(value As Boolean)
                CreateSubKey()
                Try
                    Using key As RegistryKey = RegKey.OpenSubKey(RegPath, True)
                        key.SetValue("useGarena", value.ToString(), RegistryValueKind.String)
                    End Using
                Catch ex As UnauthorizedAccessException
                    MessageBox.Show("Error: Not enough permissions to write to " & RegKey.ToString() & Environment.NewLine & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End Set
        End Property

        Private Sub CreateSubKey()
            Using key As RegistryKey = RegKey
                key.CreateSubKey(RegPath, RegistryKeyPermissionCheck.ReadWriteSubTree)
            End Using
        End Sub

        Public Sub DeleteRegistryKeys()
            Using key As RegistryKey = RegKey
                key.DeleteSubKeyTree(RegPath, False)
            End Using
        End Sub

        Public Sub DelFolderContent(ByVal path As String)
            DelFolderContent(path, False)
        End Sub

        Public Sub DelFolderContent(ByVal path As String, ByVal deleteRootDirectory As Boolean)
            DelFolderContent(path, deleteRootDirectory, False, 0, Sub(x As Integer) x = x, False, Sub(x As String) x = x)
        End Sub

        Public Sub DelFolderContent(ByVal path As String, ByVal deleteRootDirectory As Boolean, ByVal withProgressBar As Boolean, ByVal pbIncrement As Integer, ByRef addPB As Action(Of Integer), ByVal withStatus As Boolean, ByRef addStatus As Action(Of String))
            If System.IO.Directory.Exists(path) Then
                For Each foundFile As String In Enumerable.Reverse(My.Computer.FileSystem.GetFiles(path, FileIO.SearchOption.SearchAllSubDirectories))
                    My.Computer.FileSystem.DeleteFile(foundFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                    If withStatus Then addStatus("Deleted file: " & foundFile)
                    If withProgressBar Then addPB(pbIncrement)
                Next
                For Each foundFolder As String In Enumerable.Reverse(My.Computer.FileSystem.GetDirectories(path, FileIO.SearchOption.SearchAllSubDirectories))
                    My.Computer.FileSystem.DeleteDirectory(foundFolder, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                    If withStatus Then addStatus("Deleted folder: " & foundFolder)
                    If withProgressBar Then addPB(pbIncrement)
                Next
                If deleteRootDirectory Then
                    My.Computer.FileSystem.DeleteDirectory(path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
                    If withStatus Then addStatus("Deleted folder: " & path)
                End If
            End If
        End Sub

        Public Function ReadFile(ByVal path As String) As String
            Dim fs As New FileStream(path, FileMode.OpenOrCreate)
            Dim fileBuffer(fs.Length - 1) As Byte
            fs.Read(fileBuffer, 0, fs.Length)
            fs.Close()
            Return System.Text.Encoding.UTF8.GetString(fileBuffer)
        End Function

        Public Sub DetectGarenaPath()
            Dim rpath As String = GlobalVars.GarenaDestinationPathBBase
            rpath = GlobalVars.LoLPath.Path & rpath.Substring(0, rpath.IndexOf(GlobalVars.LoLVar) - 1)

            Dim dirInfo As New DirectoryInfo(rpath)
            Dim apath As DirectoryInfo = Nothing

            If dirInfo.GetDirectories.Count <= 0 Then
                Application.Exit()
            ElseIf dirInfo.GetDirectories.Count = 1 Then
                apath = dirInfo.GetDirectories.First()
            ElseIf dirInfo.GetDirectories.Count > 1 Then
                apath = (From d In dirInfo.GetDirectories
                         Order By d.LastWriteTime Descending
                         Select d).First()
            End If

            Dim fullPath As String = apath.FullName & "\Game\DATA\Characters\"
            GlobalVars.GarenaDestinationPathB = fullPath.Replace(GlobalVars.LoLPath.Path, "")
            GlobalVars.GarenaDestinationPath = GlobalVars.GarenaDestinationPathB & GlobalVars.GarenaDestinationPathBase
        End Sub
    End Module
End Namespace
