Imports System.Text


Public Class Form1
    'Version 1.53  02/03/2017

    'Varables to check if the txt files have been changed remotely
    'Used in function FileCHeck

    Dim GuestHeaderCheck As String
    Dim NameCheck(6) As String
    Dim TitleCheck(6) As String
    Dim PhoneCheck(6) As String
    Dim emailCheck(6) As String
    Dim WebsiteCheck(6) As String
    Dim FacebookCheck(6) As String
    Dim TwitterCheck(6) As String
    Dim YouTubeCheck(6) As String
    Dim CityPhoneCheck(6) As String
    Dim CityWebsiteCheck(6) As String
    Dim OfficePhoneCheck(6) As String
    Dim OfficeemailCheck(6) As String

    Dim ProducerHeaderCheck As String
    Dim ProducerCheck(2) As String
    Dim HostHeaderCheck As String
    Dim HostCheck(2) As String

    Dim DirectorHeaderCheck As String
    Dim DirectorCheck(2) As String
    Dim GraphicsHeaderCheck As String
    Dim GraphicsCheck(2) As String
    Dim AudioHeaderCheck As String
    Dim AudioCheck(2) As String

    Dim CameraHeaderCheck As String
    Dim CameraCheck(3) As String
    Dim FloorHeaderCheck As String
    Dim FloorCheck(3) As String

    Dim CommentLineCheck(1) As String
    Dim ShowPhoneCheck(1) As String
    Dim ShowemailCheck(1) As String
    Dim ShowWebsiteCheck(1) As String
    Dim ShowFacebookCheck(1) As String
    Dim ShowTwitterCheck(1) As String
    Dim ShowYouTubeCheck(1) As String

    Dim RecordDateCheck(1) As String
    Dim ShowNameCheck(1) As String
    Dim ShowTitleCheck(1) As String

    Dim DBCheck As String
    Dim CDBCheck As String

    Dim SettingsCheck As String

    Dim Fields = New String() {"Guest1", "Guest2", "Guest3", "Guest4", "Guest5", "Guest6", "Name", "Title", "Phone", "email", "Website", "Facebook", "Twitter", "YouTube", "CityPhone", "CityWebsite", "OfficePhone", "Officeemail", "Producer1", "Producer2", "Host1", "Host2", "Director1", "Director2", "Graphics1", "Graphics2", "Audio1", "Audio2", "Camera1", "Camera2", "Camera3", "Floor1", "Floor2", "Floor3", "CommentLine", "ShowPhone", "Showemail", "ShowWebsite", "ShowFacebook", "ShowTwitter", "ShowYouTube", "RecordDate", "ShowTitle", "ShowName"}

    Dim CDBitem = New String() {"Producer1", "Producer2", "Host1", "Host2", "Director1", "Director2", "Graphics1", "Graphics2", "Audio1", "Audio2", "Camera1", "Camera2", "Camera3", "Floor1", "Floor2", "Floor3"}


    Dim SaveToGraphicsLog As New List(Of String)

    Dim SaveToGraphicsLogString As String

    Dim LogFileName As String



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Check Directories
        CheckDir("txt")
        CheckDir("Database")
        CheckDir("CrewDatabase")
        CheckDir("Templates")
        CheckDir("CutFiles")
        CheckDir("Project")
        CheckDir("Logs")

        'Write the Current Date to ShowDate.txt
        Dim thisDate As Date
        thisDate = Today
        My.Computer.FileSystem.WriteAllText(".\txt\RecordDate.txt", thisDate, False, Encoding.ASCII)


        'Populate the Form Fields
        FileCheck()
    End Sub



    Private Sub TextFileCheckTime_Tick(sender As Object, e As EventArgs) Handles TextFileCheckTime.Tick
        'Checks For Changes in txt Files Every 15 Secounds
        FileCheck()
    End Sub

    Public Sub CheckDir(ByVal dir As String)
        If (Not System.IO.Directory.Exists(dir)) Then
            System.IO.Directory.CreateDirectory(dir)
        End If
    End Sub

    Public Sub HideGuest(ByVal Number As String)
        Me.Controls("LBGuest" & Number).Visible = False
        Me.Controls("DB" & Number).Visible = False
        HideTextLabel("Name" & Number)
        HideTextLabel("Title" & Number)
        HideTextLabel("Phone" & Number)
        HideTextLabel("email" & Number)
        HideTextLabel("Website" & Number)
        HideTextLabel("Facebook" & Number)
        HideTextLabel("Twitter" & Number)
        HideTextLabel("YouTube" & Number)
        HideTextLabel("CityPhone" & Number)
        HideTextLabel("CityWebsite" & Number)
        HideTextLabel("OfficePhone" & Number)
        HideTextLabel("Officeemail" & Number)
    End Sub

    Public Sub ShowGuest(ByVal Number As String)
        Me.Controls("LBGuest" & Number).Visible = True
        Me.Controls("DB" & Number).Visible = True
        ShowTextLabel("Name" & Number)
        ShowTextLabel("Title" & Number)
        ShowTextLabel("Phone" & Number)
        ShowTextLabel("email" & Number)
        ShowTextLabel("Website" & Number)
        ShowTextLabel("Facebook" & Number)
        ShowTextLabel("Twitter" & Number)
        ShowTextLabel("YouTube" & Number)
        ShowTextLabel("CityPhone" & Number)
        ShowTextLabel("CityWebsite" & Number)
        ShowTextLabel("OfficePhone" & Number)
        ShowTextLabel("Officeemail" & Number)
    End Sub

    Public Sub HideGuestField(ByVal Field As String)
        For i = 1 To 6
            HideTextLabel(Field & i)
        Next
    End Sub

    Public Sub ShowGuestField(ByVal Field As String)
        For i = 1 To 6
            If Me.Controls("LBGuest" & i).Visible = True Then
                ShowTextLabel(Field & i)
            End If
        Next
    End Sub

    Public Sub HideCrew(ByVal Field As String)
        Me.Controls("CDB" & Field).Visible = False
        HideTextLabel(Field)
    End Sub

    Public Sub ShowCrew(ByVal Field As String)
        Me.Controls("CDB" & Field).Visible = True
        ShowTextLabel(Field)
    End Sub

    Public Sub HideTextLabel(ByVal Field As String)
        Me.Controls("LB" & Field).Visible = False
        Me.Controls(Field).Text = ""
        WriteText(Field, 0)
        Me.Controls(Field).Visible = False
    End Sub

    Public Sub ShowTextLabel(ByVal Field As String)
        Me.Controls("LB" & Field).Visible = True
        Me.Controls(Field).Visible = True
    End Sub

    Public Sub FileCheck()
        'Show or Hide Fields based on Settings.txt in the txt dir
        Dim SettingsCheck2 As String = ""
        Dim TextLine As New List(Of String)
        Dim FileCount As Integer
        Dim FILE_NAME As String = ".\txt\Settings.txt"
        If System.IO.File.Exists(FILE_NAME) = True Then
            Dim objReader As New System.IO.StreamReader(FILE_NAME)
            Do While objReader.Peek() <> -1
                TextLine.Add(objReader.ReadLine())
                SettingsCheck2 = SettingsCheck2 & TextLine(FileCount)
                FileCount = FileCount + 1
            Loop
            objReader.Close()

            If FileCount < Fields.Length Then
                For i = FileCount To Fields.Length - 1
                    TextLine.Add(1)
                Next i
            End If

            If SettingsCheck <> SettingsCheck2 Then
                Dim GuestTopY As Integer = 12
                Dim GuestBottomY As Integer = 251

                Dim CrewY As Integer = 536
                Dim YHolder As Integer = 0

                'If all guest fields are blank hide guests
                Dim GuestFieldsCheck As Integer = 0
                For i = 6 To 17
                    If TextLine(i) Then GuestFieldsCheck = GuestFieldsCheck + 1
                Next i
                If GuestFieldsCheck = 0 Then
                    For i = 0 To 5
                        TextLine(i) = "0"
                    Next i
                End If

                'Hide or Show Guests
                For i = 0 To 5
                    If TextLine(i) = "1" Then
                        ShowGuest(i + 1)
                    Else
                        HideGuest(i + 1)
                    End If
                Next i

                'Hide or Show Guest Fields
                For i = 6 To 17
                    If TextLine(i) = "1" Then
                        ShowGuestField(Fields(i))
                    Else
                        HideGuestField(Fields(i))
                    End If
                Next i

                'Hide or Show Crew Fields
                For i = 18 To 33
                    If TextLine(i) = "1" Then
                        ShowCrew(Fields(i))
                    Else
                        HideCrew(Fields(i))
                    End If
                Next i

                'Hide Or Show() the Show Fields
                For i = 34 To 42
                    If TextLine(i) = "1" Then
                        ShowTextLabel(Fields(i))
                    Else
                        HideTextLabel(Fields(i))
                    End If
                Next i

                'Move guests up if not visable
                Dim GuestViz As Integer = 0
                    Dim GuestViz2 As Integer = 0
                    For i = 1 To 3
                        If Me.Controls("LBGuest" & i).Visible = True Then
                            GuestViz = GuestViz + 1
                        End If
                    Next i
                    For i = 4 To 6
                        If Me.Controls("LBGuest" & i).Visible = True Then
                            GuestViz2 = GuestViz2 + 1
                        End If
                    Next i
                    If GuestViz = 0 Then
                        GuestBottomY = GuestTopY
                    End If


                    'Arrange fields
                    'Guest Fields
                    Dim DBY As Integer = 0

                For n = 1 To 6
                    If n = 4 Then
                        GuestBottomY = DBY + 20
                    End If
                    If n < 4 Then
                        YHolder = GuestTopY
                    ElseIf n > 3 Then
                        YHolder = GuestBottomY
                    End If

                    Me.Controls("LBGuest" & n).Top = YHolder
                    Me.Controls("DB" & n).Top = YHolder + 22

                    For i = 6 To 17
                        If Me.Controls("LB" & Fields(i) & n).visible = True Then
                            Me.Controls("LB" & Fields(i) & n).Top = YHolder + 3
                            Me.Controls(Fields(i) & n).Top = YHolder
                            YHolder = YHolder + 22
                            If YHolder > DBY Then
                                DBY = YHolder
                            End If
                        End If

                    Next i
                Next n

                DBY = DBY + 20
                    'Datebase Fields
                    LBGuestDB.Top = DBY + 2
                    DBbox.Top = DBY
                    DBload.Top = DBY
                    Clear.Top = DBY
                    SaveDB.Top = DBY

                    If GuestViz = 0 And GuestViz2 = 0 Then
                        LBGuestDB.Visible = False
                        DBbox.Visible = False
                        DBload.Visible = False
                        Clear.Visible = False
                        SaveDB.Visible = False
                    Else
                        LBGuestDB.Visible = True
                        DBbox.Visible = True
                        DBload.Visible = True
                        Clear.Visible = True
                        SaveDB.Visible = True
                    End If


                    'Crew Fields
                    CrewY = DBY + 40
                    Dim CrewViz As Integer = 0
                    Dim CDBY As Integer = 0
                For i = 18 To 33
                    If i = 18 Or i = 26 Then
                        YHolder = CrewY
                    End If
                    If Me.Controls("LB" & Fields(i)).Visible = True Then
                        Me.Controls("CDB" & Fields(i)).Top = YHolder + 3
                        Me.Controls("LB" & Fields(i)).Top = YHolder + 3
                        Me.Controls(Fields(i)).Top = YHolder
                        YHolder = YHolder + 22
                        CrewViz = CrewViz + 1
                        If YHolder > CDBY Then
                            CDBY = YHolder
                        End If
                    End If
                Next i

                'Show Fields
                YHolder = CrewY
                For i = 34 To 43
                    If Me.Controls("LB" & Fields(i)).Visible = True Then
                        Me.Controls("LB" & Fields(i)).Top = YHolder + 3
                        Me.Controls(Fields(i)).Top = YHolder
                        YHolder = YHolder + 22
                    End If
                Next i

                'Hide Crew DB if there are no crew fields
                If CrewViz = 0 Then
                        LBCDB.Visible = False
                        CDBbox.Visible = False
                        CDBload.Visible = False
                        CDBSave.Visible = False
                    Else
                        LBCDB.Visible = True
                        CDBbox.Visible = True
                        CDBload.Visible = True
                        CDBSave.Visible = True
                    End If
                    'Position crew fields
                    CDBY = CDBY + 12
                    LBCDB.Top = CDBY
                    CDBbox.Top = CDBY
                    CDBload.Top = CDBY
                    CDBSave.Top = CDBY

                    'Position Save and settings buttons
                    CDBY = CDBY + 35
                    YHolder = YHolder + 5
                    If CDBY > YHolder Then
                        YHolder = CDBY
                    End If
                    ButtonSettings.Top = YHolder
                    ButtonSave.Top = YHolder

                    'Size the window
                    Me.Height = YHolder + 60

                End If
                SettingsCheck = SettingsCheck2
        End If

        'Compair txt Files to the Variable Values
        'Overwrires Form Field if txt File is Changed
        'No Check Against Form Value is Used

        'Guests
        For i = 1 To 6
            NameCheck(i) = FileCompair("Name" & i, NameCheck(i))
            TitleCheck(i) = FileCompair("Title" & i, TitleCheck(i))
            PhoneCheck(i) = FileCompair("Phone" & i, PhoneCheck(i))
            emailCheck(i) = FileCompair("email" & i, emailCheck(i))
            WebsiteCheck(i) = FileCompair("Website" & i, WebsiteCheck(i))
            FacebookCheck(i) = FileCompair("Facebook" & i, FacebookCheck(i))
            TwitterCheck(i) = FileCompair("Twitter" & i, TwitterCheck(i))
            YouTubeCheck(i) = FileCompair("YouTube" & i, YouTubeCheck(i))
            CityPhoneCheck(i) = FileCompair("CityPhone" & i, CityPhoneCheck(i))
            CityWebsiteCheck(i) = FileCompair("CityWebsite" & i, CityWebsiteCheck(i))
            OfficePhoneCheck(i) = FileCompair("OfficePhone" & i, OfficePhoneCheck(i))
            OfficeemailCheck(i) = FileCompair("Officeemail" & i, OfficeemailCheck(i))
        Next i

        'Crew
        For i = 1 To 2
            ProducerCheck(i) = FileCompair("Producer" & i, ProducerCheck(i))
            HostCheck(i) = FileCompair("Host" & i, HostCheck(i))
            DirectorCheck(i) = FileCompair("Director" & i, DirectorCheck(i))
            GraphicsCheck(i) = FileCompair("Graphics" & i, GraphicsCheck(i))
            AudioCheck(i) = FileCompair("Audio" & i, AudioCheck(i))
        Next i

        For i = 1 To 3
            CameraCheck(i) = FileCompair("Camera" & i, CameraCheck(i))
            FloorCheck(i) = FileCompair("Floor" & i, FloorCheck(i))
        Next i

        'Show Info
        For i = 1 To 1
            CommentLineCheck(i) = FileCompair("CommentLine", CommentLineCheck(i))
            ShowPhoneCheck(i) = FileCompair("ShowPhone", ShowPhoneCheck(i))
            ShowemailCheck(i) = FileCompair("Showemail", ShowemailCheck(i))
            ShowWebsiteCheck(i) = FileCompair("ShowWebsite", ShowWebsiteCheck(i))
            ShowFacebookCheck(i) = FileCompair("ShowFacebook", ShowFacebookCheck(i))
            ShowTwitterCheck(i) = FileCompair("ShowTwitter", ShowTwitterCheck(i))
            ShowYouTubeCheck(i) = FileCompair("ShowYouTube", ShowYouTubeCheck(i))
            RecordDateCheck(i) = FileCompair("RecordDate", RecordDateCheck(i))
            ShowNameCheck(i) = FileCompair("ShowName", ShowNameCheck(i))
            ShowTitleCheck(i) = FileCompair("ShowTitle", ShowTitleCheck(i))
        Next i


        'Populate Guest Database Dropdown
        Dim DBCheck2 As String = ""
        Dim dir = ".\Database\"
        'Gets current file list
        For Each DBfilelist As String In System.IO.Directory.GetFiles(dir)
            DBCheck2 = DBCheck2 & System.IO.Path.GetFileNameWithoutExtension(DBfilelist)
        Next
        'If the current list differs from the saved list repopulate the dropdown
        If DBCheck2 <> DBCheck Then
            DBbox.Items.Clear()
            For Each DBfilelist As String In System.IO.Directory.GetFiles(dir)
                DBbox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(DBfilelist))
            Next
            DBCheck = DBCheck2
        End If

        'Populate Crew Database Dropdown
        Dim CDBCheck2 As String = ""
        Dim cdir = ".\CrewDatabase\"
        'Gets current file list
        For Each CDBfilelist As String In System.IO.Directory.GetFiles(cdir)
            CDBCheck2 = CDBCheck2 & System.IO.Path.GetFileNameWithoutExtension(CDBfilelist)
        Next
        'If the current list differs from the saved list repopulate the dropdown
        If CDBCheck2 <> CDBCheck Then
            CDBbox.Items.Clear()
            For Each CDBfilelist As String In System.IO.Directory.GetFiles(cdir)
                CDBbox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(CDBfilelist))
            Next
            CDBCheck = CDBCheck2
        End If

    End Sub

    Public Function FileCompair(FileName As String, VariableValue As String)

        If System.IO.File.Exists(".\txt\" & FileName & ".txt") And Me.Controls(FileName).Visible = True Then
            If VariableValue <> My.Computer.FileSystem.ReadAllText(".\txt\" & FileName & ".txt") Then
                VariableValue = My.Computer.FileSystem.ReadAllText(".\txt\" & FileName & ".txt")
                Dim VariableValue2 As String = VariableValue
                VariableValue = VariableValue.Replace(ChrW(0), "")
                VariableValue = VariableValue.Replace(ChrW(10), "")
                VariableValue = VariableValue.Replace(ChrW(13), "")
                Me.Controls(FileName).Text = Trim(VariableValue)
                If VariableValue2 <> VariableValue Then WriteText(FileName, 0)
            End If
        End If

        Return VariableValue
    End Function

    Private Sub DBload_Click(sender As Object, e As EventArgs) Handles DBload.Click
        'Loads Guest Info If Radio Button is Checked Off of Dropdown
        If DBbox.SelectedIndex > -1 Then
            Dim FileCount As Integer
            Dim FILE_NAME As String = ".\Database\" & DBbox.SelectedItem & ".txt"
            Dim TextLine As New List(Of String)
            If System.IO.File.Exists(FILE_NAME) = True Then
                Dim objReader As New System.IO.StreamReader(FILE_NAME)

                Do While objReader.Peek() <> -1
                    TextLine.Add(objReader.ReadLine())
                    FileCount = FileCount + 1
                Loop

                If FileCount < 20 Then
                    For i = FileCount To 19
                        TextLine.Add("")
                    Next i
                End If

                For i = 1 To 6
                    If CType(Me.Controls.Item("DB" & i), RadioButton).Checked = True Then
                        If Me.Controls("Name" & i).Visible = True Then Me.Controls("Name" & i).Text = TextLine(0)
                        If Me.Controls("Title" & i).Visible = True Then Me.Controls("Title" & i).Text = TextLine(1)
                        If Me.Controls("Phone" & i).Visible = True Then Me.Controls("Phone" & i).Text = TextLine(2)
                        If Me.Controls("email" & i).Visible = True Then Me.Controls("email" & i).Text = TextLine(3)
                        If Me.Controls("Website" & i).Visible = True Then Me.Controls("Website" & i).Text = TextLine(4)
                        If Me.Controls("Facebook" & i).Visible = True Then Me.Controls("Facebook" & i).Text = TextLine(5)
                        If Me.Controls("Twitter" & i).Visible = True Then Me.Controls("Twitter" & i).Text = TextLine(6)
                        If Me.Controls("YouTube" & i).Visible = True Then Me.Controls("YouTube" & i).Text = TextLine(7)
                        If Me.Controls("CityPhone" & i).Visible = True Then Me.Controls("CityPhone" & i).Text = TextLine(10)
                        If Me.Controls("CityWebsite" & i).Visible = True Then Me.Controls("CityWebsite" & i).Text = TextLine(11)
                        If Me.Controls("OfficePhone" & i).Visible = True Then Me.Controls("OfficePhone" & i).Text = TextLine(12)
                        If Me.Controls("Officeemail" & i).Visible = True Then Me.Controls("Officeemail" & i).Text = TextLine(13)
                    End If
                Next i

                objReader.Close()
            End If
        End If
    End Sub

    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        'Clears Guest Info If Radio Button is Checked

        For i = 1 To 6
            If CType(Me.Controls.Item("DB" & i), RadioButton).Checked = True Then
                Me.Controls("Name" & i).Text = ""
                Me.Controls("Title" & i).Text = ""
                Me.Controls("Phone" & i).Text = ""
                Me.Controls("email" & i).Text = ""
                Me.Controls("Website" & i).Text = ""
                Me.Controls("Facebook" & i).Text = ""
                Me.Controls("Twitter" & i).Text = ""
                Me.Controls("YouTube" & i).Text = ""
                Me.Controls("CityPhone" & i).Text = ""
                Me.Controls("CityWebsite" & i).Text = ""
                Me.Controls("OfficePhone" & i).Text = ""
                Me.Controls("Officeemail" & i).Text = ""
            End If
        Next i

    End Sub

    Private Sub SaveDB_Click(sender As Object, e As EventArgs) Handles SaveDB.Click
        'Saves Guest Info to Database and Reloads Dropdown
        Dim TextLine(20) As String

        For i = 1 To 6
            If CType(Me.Controls.Item("DB" & i), RadioButton).Checked = True Then
                TextLine(0) = Me.Controls("Name" & i).Text
                TextLine(1) = Me.Controls("Title" & i).Text
                TextLine(2) = Me.Controls("Phone" & i).Text
                TextLine(3) = Me.Controls("email" & i).Text
                TextLine(4) = Me.Controls("Website" & i).Text
                TextLine(5) = Me.Controls("Facebook" & i).Text
                TextLine(6) = Me.Controls("Twitter" & i).Text
                TextLine(7) = Me.Controls("YouTube" & i).Text
                TextLine(10) = Me.Controls("CityPhone" & i).Text
                TextLine(11) = Me.Controls("CityWebsite" & i).Text
                TextLine(12) = Me.Controls("OfficePhone" & i).Text
                TextLine(13) = Me.Controls("Officeemail" & i).Text
            End If
        Next i

        If TextLine(0) <> "" Then
            System.IO.File.WriteAllLines(".\Database\" & TextLine(0) & ".txt", TextLine)

            'Populate the Form Fields
            FileCheck()

            'Save MessageBox
            MessageBox.Show(TextLine(0) & " Saved to Database", "Graphics Builder",
   MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If

    End Sub

    Private Sub CDBload_Click(sender As Object, e As EventArgs) Handles CDBload.Click
        'Loads Crew Info If Radio Button is Checked Off of Dropdown

        If CDBbox.SelectedItem <> "" Then
            Dim i As Integer = 0
            For Each element As Char In CDBitem
                If CType(Me.Controls.Item("CDB" & CDBitem(i)), RadioButton).Checked = True Then
                    If Me.Controls(CDBitem(i)).Visible = True Then Me.Controls(CDBitem(i)).Text = CDBbox.SelectedItem
                End If
                i = i + 1
            Next
        End If
    End Sub

    Private Sub SaveCDB_Click(sender As Object, e As EventArgs) Handles CDBSave.Click
        'Saves Crew Info to Database and Reloads Dropdown
        Dim CDBvalue As String
        CDBvalue = ""
        Dim i As Integer = 0
        For Each element As Char In CDBitem
            If CType(Me.Controls.Item("CDB" & CDBitem(i)), RadioButton).Checked = True And Me.Controls(CDBitem(i)).Text <> "" And Me.Controls(CDBitem(i)).visible = True Then
                My.Computer.FileSystem.WriteAllText(".\CrewDatabase\" & Me.Controls(CDBitem(i)).Text & ".txt", "", False)
                CDBvalue = Me.Controls(CDBitem(i)).Text
            End If
            i = i + 1
        Next

        If CDBvalue <> "" Then
            'Populate the Form Fields
            FileCheck()
            'Save MessageBox
            MessageBox.Show(CDBvalue & " Saved to Crew Database", "Graphics Builder", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If

    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click

        SaveAllText()

        SaveAllCut()


        'Message Box Popup to Say the Files are Saved
        MessageBox.Show("Files Saved to Graphics", "Graphics Builder",
           MessageBoxButtons.OK, MessageBoxIcon.Asterisk)



    End Sub

    Public Sub SaveAllText()
        'Saves All the Fields to txt files

        'Reset Log
        SaveToGraphicsLog.Clear()


            'Guest Info
            For i = 1 To 6
            'Name
            NameCheck(i) = WriteText("Name", i)
            'Title
            TitleCheck(i) = WriteText("Title", i)
            'Phone
            PhoneCheck(i) = WriteText("Phone", i)
            'email
            emailCheck(i) = WriteText("email", i)
            'Website
            WebsiteCheck(i) = WriteText("Website", i)
            'Facebook
            FacebookCheck(i) = WriteText("Facebook", i)
            'Twitter
            TwitterCheck(i) = WriteText("Twitter", i)
            'YouTube
            YouTubeCheck(i) = WriteText("YouTube", i)
            'CityPhone
            CityPhoneCheck(i) = WriteText("CityPhone", i)
            'CityWebsite
            CityWebsiteCheck(i) = WriteText("CityWebsite", i)
            'OfficePhone
            OfficePhoneCheck(i) = WriteText("OfficePhone", i)
            'Officeemail
            OfficeemailCheck(i) = WriteText("Officeemail", i)
        Next i

        'Crew
        For i = 1 To 2
            'Producer
            ProducerCheck(i) = WriteText("Producer", i)
            'Host
            HostCheck(i) = WriteText("Host", i)
            'Director
            DirectorCheck(i) = WriteText("Director", i)
            'Graphics
            GraphicsCheck(i) = WriteText("Graphics", i)
            'Audio
            AudioCheck(i) = WriteText("Audio", i)
        Next i

        For i = 1 To 3
            'Camera
            CameraCheck(i) = WriteText("Camera", i)
            'Floor
            FloorCheck(i) = WriteText("Floor", i)
        Next i

        'Show Info
        'ShowPhone
        ShowPhoneCheck(1) = WriteText("ShowPhone", 0)
        'CommentLine
        CommentLineCheck(1) = WriteText("CommentLine", 0)
        'ShowYouTube
        ShowYouTubeCheck(1) = WriteText("ShowYouTube", 0)
        'ShowTwitter
        ShowTwitterCheck(1) = WriteText("ShowTwitter", 0)
        'ShowFacebook
        ShowFacebookCheck(1) = WriteText("ShowFacebook", 0)
        'ShowWebsite
        ShowWebsiteCheck(1) = WriteText("ShowWebsite", 0)
        'Showemail
        ShowemailCheck(1) = WriteText("Showemail", 0)
        'RecordDate
        RecordDateCheck(1) = WriteText("RecordDate", 0)
        'ShowName
        ShowNameCheck(1) = WriteText("ShowName", 0)
        'ShowTitle
        ShowTitleCheck(1) = WriteText("ShowTitle", 0)

        'Write txt Files for Use as Credit Titles
        'Singular or Plural
        'Guest Header
        GuestHeaderCheck = WriteTextHeader("Name", "GuestHeader", "Guest:", "Guests:", 6)
        'Producer Header
        ProducerHeaderCheck = WriteTextHeader("Producer", "ProducerHeader", "Producer:", "Producers:", 2)
        'Host Header
        HostHeaderCheck = WriteTextHeader("Host", "HostHeader", "Host:", "Hosts:", 2)
        'Director Header
        DirectorHeaderCheck = WriteTextHeader("Director", "DirectorHeader", "Director:", "Directors:", 2)
        'Graphics Header
        GraphicsHeaderCheck = WriteTextHeader("Graphics", "GraphicsHeader", "Graphics Operator:", "Graphics Operators:", 2)
        'Audio Header
        AudioHeaderCheck = WriteTextHeader("Audio", "AudioHeader", "Audio Operator:", "Audio Operators:", 2)
        'Camera Header
        CameraHeaderCheck = WriteTextHeader("Camera", "CameraHeader", "Camera Operator:", "Camera Operators:", 3)
        'Floor Header
        FloorHeaderCheck = WriteTextHeader("Floor", "FloorHeader", "Floor Director:", "Floor Directors:", 3)


        'Save Log File
        SaveLog()


    End Sub


    Public Sub SaveLog()
        'Save Log File
        LogFileName = CStr(Now())
        LogFileName = LogFileName.Replace("/", "-")
        LogFileName = LogFileName.Replace(":", "-")
        Dim SaveToGraphicsLogArray As String() = SaveToGraphicsLog.ToArray
        System.IO.File.WriteAllLines(".\Logs\" & LogFileName & ".txt", SaveToGraphicsLogArray)

        'Reset Log
        SaveToGraphicsLogString = ""

        For i = 0 To SaveToGraphicsLogArray.Length - 1
            SaveToGraphicsLogString = SaveToGraphicsLogString & ChrW(10) & SaveToGraphicsLogArray(i)
        Next i

    End Sub




    Public Sub SaveAllCut()
        'Cut Files
        'Delete Old Cut Files
        Dim DeleteCutFiles As String
        For Each DeleteCutFiles In System.IO.Directory.GetFiles(".\CutFiles\", "*.tcf")
            System.IO.File.Delete(DeleteCutFiles)
        Next DeleteCutFiles
        For Each DeleteCutFiles In System.IO.Directory.GetFiles(".\Project\", "*.tcf")
            System.IO.File.Delete(DeleteCutFiles)
        Next DeleteCutFiles

        'Number Cut Files
        Dim CutFileCount As Integer = 100
        'Open
        CutFileCount = OpenSlate(CutFileCount)
        'Bug
        CutFileCount = Bug(CutFileCount)
        'Lower Thirds
        'Host Names
        For i = 1 To 2
            If CStr(HostCheck(i)) <> "   " Then
                CutFileCount = LowerThird(CStr(HostCheck(i)), "Host of " & CStr(ShowNameCheck(1)), "Blank.tga", "Host" & i, CutFileCount)
            End If
        Next i
        'Comment Line
        If CStr(CommentLineCheck(1)) <> "   " Then
            CutFileCount = LowerThird(CStr(CommentLineCheck(1)), "Comment Line", "Blank.tga", "CommentLine", CutFileCount)
        End If
        'Show Phone
        If CStr(ShowPhoneCheck(1)) <> "   " Then
            CutFileCount = LowerThird(CStr(ShowPhoneCheck(1)), CStr(ShowNameCheck(1)) & " Phone", "Blank.tga", "ShowPhone", CutFileCount)
        End If
        'Show email
        If CStr(ShowemailCheck(1)) <> "   " Then
            CutFileCount = LowerThird(CStr(ShowemailCheck(1)), "", "Blank.tga", "Showemail", CutFileCount)
        End If
        'Show Website
        If CStr(ShowWebsiteCheck(1)) <> "   " Then
            CutFileCount = LowerThird(CStr(ShowWebsiteCheck(1)), "", "Blank.tga", "ShowWebsite", CutFileCount)
        End If
        'Show Facebook
        If CStr(ShowFacebookCheck(1)) <> "   " Then
            CutFileCount = LowerThird(CStr(ShowFacebookCheck(1)), "", "Facebook.tga", "ShowFacebook", CutFileCount)
        End If
        'Show Twitter
        If CStr(ShowTwitterCheck(1)) <> "   " Then
            CutFileCount = LowerThird(CStr(ShowTwitterCheck(1)), "", "Twitter.tga", "ShowTwitter", CutFileCount)
        End If
        'Show YouTube
        If CStr(ShowYouTubeCheck(1)) <> "   " Then
            CutFileCount = LowerThird(CStr(ShowYouTubeCheck(1)), "", "YouTube.tga", "ShowYouTube", CutFileCount)
        End If

        'Guests
        For i = 1 To 6
            'Name
            If CStr(NameCheck(i)) <> "   " Then
                CutFileCount = LowerThird(CStr(NameCheck(i)), CStr(TitleCheck(i)), "Blank.tga", "Guest" & i, CutFileCount)
            End If
            'Contact
            If PhoneCheck(i) <> "   " And emailCheck(i) <> "   " Then
                CutFileCount = LowerThird(CStr(PhoneCheck(i)), CStr(emailCheck(i)), "Blank.tga", "Contact" & i, CutFileCount)
            ElseIf PhoneCheck(i) = "   " And emailCheck(i) <> "   " Then
                CutFileCount = LowerThird(CStr(emailCheck(i)), "", "Blank.tga", "Contact" & i, CutFileCount)
            ElseIf PhoneCheck(i) <> "   " And emailCheck(i) = "   " Then
                CutFileCount = LowerThird(CStr(PhoneCheck(i)), "", "Blank.tga", "Contact" & i, CutFileCount)
            End If
            'Website
            If CStr(WebsiteCheck(i)) <> "   " Then
                CutFileCount = LowerThird(CStr(WebsiteCheck(i)), "", "Blank.tga", "Website" & i, CutFileCount)
            End If
            'Facebook
            If CStr(FacebookCheck(i)) <> "   " Then
                CutFileCount = LowerThird(CStr(FacebookCheck(i)), "", "Facebook.tga", "Facebook" & i, CutFileCount)
            End If
            'Twitter
            If CStr(TwitterCheck(i)) <> "   " Then
                CutFileCount = LowerThird(CStr(TwitterCheck(i)), "", "Twitter.tga", "Twitter" & i, CutFileCount)
            End If
            'YouTube
            If CStr(YouTubeCheck(i)) <> "   " Then
                CutFileCount = LowerThird(CStr(YouTubeCheck(i)), "", "YouTube.tga", "YouTube" & i, CutFileCount)
            End If
            'City
            If CityPhoneCheck(i) <> "   " And CityWebsiteCheck(i) <> "   " Then
                CutFileCount = LowerThird(CStr(CityPhoneCheck(i)), CStr(CityWebsiteCheck(i)), "Blank.tga", "City" & i, CutFileCount)
            ElseIf CityPhoneCheck(i) = "   " And CityWebsiteCheck(i) <> "   " Then
                CutFileCount = LowerThird(CStr(CityWebsiteCheck(i)), "", "Blank.tga", "City" & i, CutFileCount)
            ElseIf CityPhoneCheck(i) <> "   " And CityWebsiteCheck(i) = "   " Then
                CutFileCount = LowerThird(CStr(CityPhoneCheck(i)), "", "Blank.tga", "City" & i, CutFileCount)
            End If
            'Office
            If OfficePhoneCheck(i) <> "   " And OfficeemailCheck(i) <> "   " Then
                CutFileCount = LowerThird(CStr(OfficePhoneCheck(i)), CStr(OfficeemailCheck(i)), "Blank.tga", "Office" & i, CutFileCount)
            ElseIf OfficePhoneCheck(i) = "   " And OfficeemailCheck(i) <> "   " Then
                CutFileCount = LowerThird(CStr(OfficeemailCheck(i)), "", "Blank.tga", "Office" & i, CutFileCount)
            ElseIf OfficePhoneCheck(i) <> "   " And OfficeemailCheck(i) = "   " Then
                CutFileCount = LowerThird(CStr(OfficePhoneCheck(i)), "", "Blank.tga", "Office" & i, CutFileCount)
            End If
        Next i

        'Credits
        CutFileCount = Credits(CutFileCount)
    End Sub

    Public Function WriteText(ByVal WriteTextName As String, ByVal WriteTextLoop As Integer)
        Dim WriteTextContent As String
        If WriteTextLoop > 0 Then
            'used for loops
            If Me.Controls(WriteTextName & WriteTextLoop).Visible = False Then
                Me.Controls(WriteTextName & WriteTextLoop).Text = ""
            End If

            WriteTextContent = Me.Controls(WriteTextName & WriteTextLoop).Text
            WriteTextContent = WriteTextContent.Replace(ChrW(0), "")
            WriteTextContent = WriteTextContent.Replace(ChrW(10), "")
            WriteTextContent = WriteTextContent.Replace(ChrW(13), "")
            If Me.Controls(WriteTextName & WriteTextLoop).Text = "" Then
                WriteTextContent = "   "
            End If
            My.Computer.FileSystem.WriteAllText(".\txt\" & WriteTextName & WriteTextLoop & ".txt", WriteTextContent, False, Encoding.ASCII)
        Else
            'non loops
            If Me.Controls(WriteTextName).Visible = False Then
                Me.Controls(WriteTextName).Text = ""
            End If

            WriteTextContent = Me.Controls(WriteTextName).Text
            WriteTextContent = WriteTextContent.Replace(ChrW(0), "")
            WriteTextContent = WriteTextContent.Replace(ChrW(10), "")
            WriteTextContent = WriteTextContent.Replace(ChrW(13), "")
            If Me.Controls(WriteTextName).Text = "" Then
                WriteTextContent = "   "
            End If
            My.Computer.FileSystem.WriteAllText(".\txt\" & WriteTextName & ".txt", WriteTextContent, False, Encoding.Default)
        End If

        'Add to Log
        If Trim(WriteTextContent) <> "" Then
            SaveToGraphicsLog.Add(WriteTextName & WriteTextLoop & "     " & WriteTextContent)
        End If

        Return WriteTextContent
    End Function

    Public Function WriteTextHeader(ByVal WriteTextFieldName As String, ByVal WriteTextHeaderName As String, ByVal WriteTextSingular As String, ByVal WriteTextPlural As String, ByVal WriteTextHeaderLoop As Integer)
        Dim ReturnValue As String
        Dim HeaderCount As Integer = 0
        For i = 1 To WriteTextHeaderLoop
            If Trim(Me.Controls(WriteTextFieldName & i).Text) <> "" Then
                HeaderCount = HeaderCount + 1
            End If
        Next i

        If HeaderCount > 1 Then
            ReturnValue = WriteTextPlural
        Else
            ReturnValue = WriteTextSingular
        End If
        If HeaderCount = 0 Then
            ReturnValue = "   "
        End If
        My.Computer.FileSystem.WriteAllText(".\txt\" & WriteTextHeaderName & ".txt", ReturnValue, False, Encoding.ASCII)

        Return ReturnValue
    End Function

    Public Function AddOneSpace(ByVal InputString As String) As String
        'Adds Null for page names and dir in cut files
        Dim CharArray() As Char = InputString.ToCharArray
        Dim SpaceString As String = ""

        Dim i As Integer = 0
        For Each element As Char In CharArray
            SpaceString = SpaceString + CharArray(i) & ChrW(0)
            i = i + 1
        Next

        Return SpaceString
    End Function

    Public Function AddSpace(ByVal InputString As String) As String
        'Adds 5 null for text in cut files
        Dim CharArray() As Char = InputString.ToCharArray
        Dim SpaceString As String = ""

        Dim i As Integer = 0
        For Each element As Char In CharArray
            SpaceString = SpaceString + CharArray(i) & ChrW(0) & ChrW(0) & ChrW(0) & ChrW(0) & ChrW(0)
            i = i + 1
        Next

        Return SpaceString
    End Function

    Public Function CutFileTitle(ByVal CutFileString As String, OldText As String, NewText As String)
        'Replaces the old cutfile title with a new one and character count
        CutFileString = CutFileString.Replace(ChrW(OldText.Length) & ChrW(0) & ChrW(0) & ChrW(0) & AddOneSpace(OldText), ChrW(NewText.Length) & ChrW(0) & ChrW(0) & ChrW(0) & AddOneSpace(NewText))

        Return CutFileString
    End Function

    Public Function CutFileText(ByVal CutFileString As String, OldText As String, NewText As String)
        'Replaces the old cutfile text with new text and character count
        If NewText = "" Then
            NewText = "   "
        End If
        CutFileString = CutFileString.Replace(ChrW(OldText.Length) & ChrW(0) & ChrW(0) & ChrW(0) & AddSpace(OldText), ChrW(NewText.Length) & ChrW(0) & ChrW(0) & ChrW(0) & AddSpace(NewText))

        Return CutFileString
    End Function

    Public Function CutFilePicture(ByVal CutFileString As String, OldText As String, NewText As String)
        'Replaces the pictures in cut files with a charater count
        CutFileString = CutFileString.Replace(ChrW(OldText.Length) & ChrW(0) & ChrW(0) & ChrW(0) & AddOneSpace(OldText), ChrW(NewText.Length) & ChrW(0) & ChrW(0) & ChrW(0) & AddOneSpace(NewText))

        Return CutFileString
    End Function

    Public Function GetCutFileList(ByVal SearchString As String)
        Dim FileName As String
        Dim FileNameList As New List(Of String)

        Dim dir = ".\Templates\"
        'Gets current file list
        For Each filelist As String In System.IO.Directory.GetFiles(dir)
            FileName = System.IO.Path.GetFileNameWithoutExtension(filelist)
            If FileName.Contains(SearchString) = True Then
                FileNameList.Add(FileName)
            End If
        Next

        Return FileNameList
    End Function

    Public Function OpenSlate(ByVal CutFileCount As Integer)
        Dim FileCount As Integer = 0
        Dim FileNameList As New List(Of String)
        FileNameList = GetCutFileList("Open")
        Dim NameTitle As String = ""
        Dim CityPhoneCityWebsite As String = ""
        Dim OfficePhoneOfficeemail As String = ""

        For Each item As String In FileNameList
            If System.IO.File.Exists(".\Templates\" & FileNameList.Item(FileCount) & ".tcf") Then
                Dim Output As String = My.Computer.FileSystem.ReadAllText(".\Templates\" & FileNameList.Item(FileCount) & ".tcf", Encoding.GetEncoding("IBM861"))

                'Page Name
                Output = CutFileTitle(Output, "Open", CutFileCount & "Open" & FileCount + 1)
                'Show Title
                Output = CutFileText(Output, "Show Title", ShowTitleCheck(1))
                'Guest Header
                Output = CutFileText(Output, "Guest Header", GuestHeaderCheck)
                'Guests
                For i = 1 To 6
                    'Name
                    Output = CutFileText(Output, "Guest " & i, NameCheck(i))
                    Output = CutFileText(Output, "Name " & i, NameCheck(i))
                    'Title
                    Output = CutFileText(Output, "Title " & i, TitleCheck(i))
                    'Name - Title
                    If Trim(NameCheck(i)) = "" And Trim(TitleCheck(i)) = "" Then
                        NameTitle = "   "
                    ElseIf Trim(NameCheck(i)) <> "" And Trim(TitleCheck(i)) = "" Then
                        NameTitle = NameCheck(i)
                    ElseIf Trim(NameCheck(i)) <> "" And Trim(TitleCheck(i)) <> "" Then
                        NameTitle = NameCheck(i) & " - " & TitleCheck(i)
                    End If
                    Output = CutFileText(Output, "Name Title " & i, NameTitle)
                    NameTitle = ""
                    'Phone
                    Output = CutFileText(Output, "Phone " & i, PhoneCheck(i))
                    'email
                    Output = CutFileText(Output, "email " & i, emailCheck(i))
                    'Website
                    Output = CutFileText(Output, "Website " & i, WebsiteCheck(i))
                    'Facebook
                    Output = CutFileText(Output, "Facebook " & i, FacebookCheck(i))
                    'Twitter
                    Output = CutFileText(Output, "Twitter " & i, TwitterCheck(i))
                    'YouTube
                    Output = CutFileText(Output, "YouTube " & i, YouTubeCheck(i))
                    'CityPhone
                    Output = CutFileText(Output, "CityPhone " & i, CityPhoneCheck(i))
                    'CityWebsite
                    Output = CutFileText(Output, "CityWebsite " & i, CityWebsiteCheck(i))
                    'CityPhone - CityWebsite
                    If Trim(CityPhoneCheck(i)) = "" And Trim(CityWebsiteCheck(i)) = "" Then
                        CityPhoneCityWebsite = "   "
                    ElseIf Trim(CityPhoneCheck(i)) <> "" And Trim(CityWebsiteCheck(i)) = "" Then
                        CityPhoneCityWebsite = CityPhoneCheck(i)
                    ElseIf Trim(CityPhoneCheck(i)) = "" And Trim(CityWebsiteCheck(i)) <> "" Then
                        CityPhoneCityWebsite = CityWebsiteCheck(i)
                    ElseIf Trim(CityPhoneCheck(i)) <> "" And Trim(CityWebsiteCheck(i)) <> "" Then
                        CityPhoneCityWebsite = CityPhoneCheck(i) & " - " & CityWebsiteCheck(i)
                    End If
                    Output = CutFileText(Output, "CityPhone CityWebsite " & i, CityPhoneCityWebsite)
                    CityPhoneCityWebsite = ""
                    'OfficePhone
                    Output = CutFileText(Output, "OfficePhone " & i, OfficePhoneCheck(i))
                    'Officeemail
                    Output = CutFileText(Output, "Officeemail " & i, OfficeemailCheck(i))
                    'OfficePhone - Officeemail
                    If Trim(OfficePhoneCheck(i)) = "" And Trim(OfficeemailCheck(i)) = "" Then
                        OfficePhoneOfficeemail = "   "
                    ElseIf Trim(OfficePhoneCheck(i)) <> "" And Trim(OfficeemailCheck(i)) = "" Then
                        OfficePhoneOfficeemail = OfficePhoneCheck(i)
                    ElseIf Trim(OfficePhoneCheck(i)) = "" And Trim(OfficeemailCheck(i)) <> "" Then
                        OfficePhoneOfficeemail = OfficeemailCheck(i)
                    ElseIf Trim(OfficePhoneCheck(i)) <> "" And Trim(OfficeemailCheck(i)) <> "" Then
                        OfficePhoneOfficeemail = OfficePhoneCheck(i) & " - " & OfficeemailCheck(i)
                    End If
                    Output = CutFileText(Output, "OfficePhone Officeemail " & i, OfficePhoneOfficeemail)
                    OfficePhoneOfficeemail = ""
                Next i

                'Record Date
                Output = CutFileText(Output, "Record Date", RecordDateCheck(1))
                'Producer Header
                Output = CutFileText(Output, "Producer Header", ProducerHeaderCheck)
                For i = 1 To 2
                    'Producers
                    Output = CutFileText(Output, "Producer " & i, ProducerCheck(i))
                Next i
                'Host Header
                Output = CutFileText(Output, "Host Header", HostHeaderCheck)
                For i = 1 To 2
                    'Hosts
                    Output = CutFileText(Output, "Host " & i, HostCheck(i))
                Next i
                My.Computer.FileSystem.WriteAllText(".\CutFiles\" & CutFileCount & "Open" & FileCount + 1 & ".tcf", Output, False, Encoding.GetEncoding("IBM861"))
                CutFileCount = CutFileCount + 1
            End If

            FileCount = FileCount + 1
        Next
        Return CutFileCount
    End Function

    Public Function Bug(ByVal CutFileCount As Integer)
        Dim FileCount As Integer = 0
        Dim FileNameList As New List(Of String)
        FileNameList = GetCutFileList("Bug")

        For Each item As String In FileNameList
            If System.IO.File.Exists(".\Templates\" & FileNameList.Item(FileCount) & ".tcf") Then
                Dim Output As String = My.Computer.FileSystem.ReadAllText(".\Templates\" & FileNameList.Item(FileCount) & ".tcf", Encoding.GetEncoding("IBM861"))

                'Page Name
                Output = CutFileTitle(Output, "Bug", CutFileCount & "Bug" & FileCount + 1)
                My.Computer.FileSystem.WriteAllText(".\CutFiles\" & CutFileCount & "Bug" & FileCount + 1 & ".tcf", Output, False, Encoding.GetEncoding("IBM861"))
                CutFileCount = CutFileCount + 1
            End If
            FileCount = FileCount + 1
        Next

        Return CutFileCount
    End Function

    Public Function LowerThird(ByVal TopLine As String, ByVal BottomLine As String, ByVal Pic As String, ByVal FileName As String, ByVal CutFileCount As Integer)

        If System.IO.File.Exists(".\Templates\Lower.tcf") Then
            Dim Output As String = My.Computer.FileSystem.ReadAllText(".\Templates\Lower.tcf", Encoding.GetEncoding("IBM861"))
            Dim Path As String = "C:\Users\MyComputer\Desktop\Images\"

            'Page Name
            Output = CutFileTitle(Output, "Lower", CutFileCount & FileName)
            'Top Line
            Output = CutFileText(Output, "Name", TopLine)
            'Botton Line
            Output = CutFileText(Output, "Title", BottomLine)
            'Pic
            Output = CutFilePicture(Output, Path & "Picture.tga", Path & Pic)
            My.Computer.FileSystem.WriteAllText(".\CutFiles\" & CutFileCount & FileName & ".tcf", Output, False, Encoding.GetEncoding("IBM861"))
            CutFileCount = CutFileCount + 1
        End If
        Return CutFileCount
    End Function

    Public Function Credits(ByVal CutFileCount As Integer)
        Dim FileCount As Integer = 0
        Dim FileNameList As New List(Of String)
        FileNameList = GetCutFileList("Credits")
        Dim NameTitle As String = ""
        Dim CityPhoneCityWebsite As String = ""
        Dim OfficePhoneOfficeemail As String = ""

        For Each item As String In FileNameList
            If System.IO.File.Exists(".\Templates\" & FileNameList.Item(FileCount) & ".tcf") Then
                Dim Output As String = My.Computer.FileSystem.ReadAllText(".\Templates\" & FileNameList.Item(FileCount) & ".tcf", Encoding.GetEncoding("IBM861"))

                'Page Name
                Output = CutFileTitle(Output, "Credits", CutFileCount & "Credits" & FileCount + 1)
                'Show Title
                Output = CutFileText(Output, "Show Title", ShowTitleCheck(1))
                'Guest Header
                Output = CutFileText(Output, "Guest Header", GuestHeaderCheck)
                'Guests
                For i = 1 To 6
                    'Name
                    Output = CutFileText(Output, "Guest " & i, NameCheck(i))
                    Output = CutFileText(Output, "Name " & i, NameCheck(i))
                    'Title
                    Output = CutFileText(Output, "Title " & i, TitleCheck(i))
                    'Name - Title
                    If Trim(NameCheck(i)) = "" And Trim(TitleCheck(i)) = "" Then
                        NameTitle = "   "
                    ElseIf Trim(NameCheck(i)) <> "" And Trim(TitleCheck(i)) = "" Then
                        NameTitle = NameCheck(i)
                    ElseIf Trim(NameCheck(i)) <> "" And Trim(TitleCheck(i)) <> "" Then
                        NameTitle = NameCheck(i) & " - " & TitleCheck(i)
                    End If
                    Output = CutFileText(Output, "Name Title " & i, NameTitle)
                    NameTitle = ""
                    'Phone
                    Output = CutFileText(Output, "Phone " & i, PhoneCheck(i))
                    'email
                    Output = CutFileText(Output, "email " & i, emailCheck(i))
                    'Website
                    Output = CutFileText(Output, "Website " & i, WebsiteCheck(i))
                    'Facebook
                    Output = CutFileText(Output, "Facebook " & i, FacebookCheck(i))
                    'Twitter
                    Output = CutFileText(Output, "Twitter " & i, TwitterCheck(i))
                    'YouTube
                    Output = CutFileText(Output, "YouTube " & i, YouTubeCheck(i))
                    'CityPhone
                    Output = CutFileText(Output, "CityPhone " & i, CityPhoneCheck(i))
                    'CityWebsite
                    Output = CutFileText(Output, "CityWebsite " & i, CityWebsiteCheck(i))
                    'CityPhone - CityWebsite
                    If Trim(CityPhoneCheck(i)) = "" And Trim(CityWebsiteCheck(i)) = "" Then
                        CityPhoneCityWebsite = "   "
                    ElseIf Trim(CityPhoneCheck(i)) <> "" And Trim(CityWebsiteCheck(i)) = "" Then
                        CityPhoneCityWebsite = CityPhoneCheck(i)
                    ElseIf Trim(CityPhoneCheck(i)) = "" And Trim(CityWebsiteCheck(i)) <> "" Then
                        CityPhoneCityWebsite = CityWebsiteCheck(i)
                    ElseIf Trim(CityPhoneCheck(i)) <> "" And Trim(CityWebsiteCheck(i)) <> "" Then
                        CityPhoneCityWebsite = CityPhoneCheck(i) & " - " & CityWebsiteCheck(i)
                    End If
                    Output = CutFileText(Output, "CityPhone CityWebsite " & i, CityPhoneCityWebsite)
                    CityPhoneCityWebsite = ""
                    'OfficePhone
                    Output = CutFileText(Output, "OfficePhone " & i, OfficePhoneCheck(i))
                    'Officeemail
                    Output = CutFileText(Output, "Officeemail " & i, OfficeemailCheck(i))
                    'OfficePhone - Officeemail
                    If Trim(OfficePhoneCheck(i)) = "" And Trim(OfficeemailCheck(i)) = "" Then
                        OfficePhoneOfficeemail = "   "
                    ElseIf Trim(OfficePhoneCheck(i)) <> "" And Trim(OfficeemailCheck(i)) = "" Then
                        OfficePhoneOfficeemail = OfficePhoneCheck(i)
                    ElseIf Trim(OfficePhoneCheck(i)) = "" And Trim(OfficeemailCheck(i)) <> "" Then
                        OfficePhoneOfficeemail = OfficeemailCheck(i)
                    ElseIf Trim(OfficePhoneCheck(i)) <> "" And Trim(OfficeemailCheck(i)) <> "" Then
                        OfficePhoneOfficeemail = OfficePhoneCheck(i) & " - " & OfficeemailCheck(i)
                    End If
                    Output = CutFileText(Output, "OfficePhone Officeemail " & i, OfficePhoneOfficeemail)
                    OfficePhoneOfficeemail = ""
                Next i
                'Producer Header
                Output = CutFileText(Output, "Producer Header", ProducerHeaderCheck)
                For i = 1 To 2
                    'Producers
                    Output = CutFileText(Output, "Producer " & i, ProducerCheck(i))
                Next i
                'Host Header
                Output = CutFileText(Output, "Host Header", HostHeaderCheck)
                For i = 1 To 2
                    'Hosts
                    Output = CutFileText(Output, "Host " & i, HostCheck(i))
                Next i
                'Director Header
                Output = CutFileText(Output, "Director Header", DirectorHeaderCheck)
                For i = 1 To 2
                    'Directors
                    Output = CutFileText(Output, "Director " & i, DirectorCheck(i))
                Next i
                'Graphics Header
                Output = CutFileText(Output, "Graphics Header", GraphicsHeaderCheck)
                For i = 1 To 2
                    'Graphics
                    Output = CutFileText(Output, "Graphics " & i, GraphicsCheck(i))
                Next i
                'Audio Header
                Output = CutFileText(Output, "Audio Header", AudioHeaderCheck)
                For i = 1 To 2
                    'Audio
                    Output = CutFileText(Output, "Audio " & i, AudioCheck(i))
                Next i
                'Camera Header
                Output = CutFileText(Output, "Camera Header", CameraHeaderCheck)
                For i = 1 To 3
                    'Camera
                    Output = CutFileText(Output, "Camera " & i, CameraCheck(i))
                Next i
                'Floor Header
                Output = CutFileText(Output, "Floor Header", FloorHeaderCheck)
                For i = 1 To 3
                    'Floor
                    Output = CutFileText(Output, "Floor " & i, FloorCheck(i))
                Next i
                'Record Date
                Output = CutFileText(Output, "Record Date", RecordDateCheck(1))

                My.Computer.FileSystem.WriteAllText(".\CutFiles\" & CutFileCount & "Credits" & FileCount + 1 & ".tcf", Output, False, Encoding.GetEncoding("IBM861"))
                CutFileCount = CutFileCount + 1
            End If
            FileCount = FileCount + 1
        Next
        Return CutFileCount
    End Function

    Public Sub ResetChecks()

    End Sub

    Private Sub ButtonSettings_Click(sender As Object, e As EventArgs) Handles ButtonSettings.Click
        Settings.Show()
    End Sub


End Class
