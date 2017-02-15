Public Class Form5
    Public Shared password As String = "28594985"
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Val(TextBox2.Text) = password Then
            Me.Visible = False
            Form1.Visible = True
        Else
            Label2.Visible = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Show" Then
            Button3.Text = "Hide"
            TextBox2.PasswordChar = ""
            TextBox2.UseSystemPasswordChar = False
            Button3.BackColor = Color.Green

        Else
            Button3.Text = "Show"
            TextBox2.PasswordChar = "*"
            TextBox2.UseSystemPasswordChar = True
            Button3.BackColor = DefaultBackColor
        End If
    End Sub
End Class