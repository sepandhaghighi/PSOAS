Imports System.IO
Imports System.Text
Imports System.Net

Public Class Form5
    Dim user_address As String = "http://sepandhaghighi.github.io/Questionnaire/Username.txt"
    Dim pass_address As String = "http://sepandhaghighi.github.io/Questionnaire/Pass.txt"
    Public Shared password As String
    Dim client As WebClient = New WebClient()
    Dim ureader As StreamReader
    Dim preader As StreamReader
    Dim username As String
    Dim i As Integer = 0
    Public Shared user_list(10) As String
    Public Shared pass_list(10) As String
    Public Shared usercount As Integer = 0
    Public Sub user_reader()
        ureader = New StreamReader(client.OpenRead(user_address))
        preader = New StreamReader(client.OpenRead(pass_address))
        usercount = 1
        While (True)
            username = ureader.ReadLine
            password = preader.ReadLine
            If Len(username) > 2 Then
                user_list(usercount) = username
                pass_list(usercount) = password
                usercount = usercount + 1
            Else
                Exit While
            End If

        End While

    End Sub

    Public Sub check_user()
        On Error GoTo download_error
        user_reader()
        For k = 1 To usercount
            If user_list(k) = TextBox1.Text And pass_list(k) = TextBox2.Text And Len(TextBox2.Text) = 8 Then
                Form1.Visible = True
                Me.Visible = False
                Form1.TextBox1.Text = TextBox1.Text
                Exit For
            End If
        Next
        Label3.Visible = True
        Timer2.Enabled = True
        Exit Sub
download_error:
        Label4.Visible = True
        Timer2.Enabled = True
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        check_user()

    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label3.Visible = False
        Label4.Visible = False
        Timer2.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Show" Then
            TextBox2.UseSystemPasswordChar = False
            TextBox2.PasswordChar = ""
            Button3.Text = "Hide"
            Button3.BackColor = Color.Green
        Else
            TextBox2.UseSystemPasswordChar = True
            TextBox2.PasswordChar = "*"
            Button3.Text = "Show"
            Button3.BackColor = DefaultBackColor
        End If

    End Sub
End Class