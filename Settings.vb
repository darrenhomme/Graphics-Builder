'Version 1.09  01/19/2017

Public Class Settings

    Dim FileCount As Integer
    Dim FILE_NAME As String = ".\txt\Settings.txt"
    Dim Fields = New String() {"Guest1", "Guest2", "Guest3", "Guest4", "Guest5", "Guest6", "Name", "Title", "Phone", "email", "Website", "Facebook", "Twitter", "YouTube", "CityPhone", "CityWebsite", "OfficePhone", "Officeemail", "Producer1", "Producer2", "Host1", "Host2", "Director1", "Director2", "Graphics1", "Graphics2", "Audio1", "Audio2", "Camera1", "Camera2", "Camera3", "Floor1", "Floor2", "Floor3", "CommentLine", "ShowPhone", "Showemail", "ShowWebsite", "ShowFacebook", "ShowTwitter", "ShowYouTube", "RecordDate", "ShowTitle"}



    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim TextLine As New List(Of String)
        If System.IO.File.Exists(FILE_NAME) = True Then
            Dim objReader As New System.IO.StreamReader(FILE_NAME)
            Do While objReader.Peek() <> -1
                TextLine.Add(objReader.ReadLine())
                FileCount = FileCount + 1
            Loop
            objReader.Close()

            If FileCount < Fields.Length Then
                For i = FileCount To Fields.Length - 1
                    TextLine.Add(1)
                Next i
            End If

            For i = 0 To Fields.length - 1
                If TextLine(i) = "1" Then
                    CType(Me.Controls.Item("Check" & Fields(i)), CheckBox).Checked = True
                End If
            Next i

        Else
            For i = 0 To Fields.Length - 1
                CType(Me.Controls.Item("Check" & Fields(i)), CheckBox).Checked = True
            Next i
        End If

    End Sub



    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Dim TextLine(50) As String
        For i = 0 To Fields.Length - 1
            If CType(Me.Controls.Item("Check" & Fields(i)), CheckBox).Checked = True Then
                TextLine(i) = 1
            Else
                TextLine(i) = 0
            End If
        Next i
        System.IO.File.WriteAllLines(FILE_NAME, TextLine)


        Me.Hide()
        Form1.FileCheck()
    End Sub






End Class
