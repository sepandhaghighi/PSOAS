Public Class Form4

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If My.Computer.Network.IsAvailable Then
            Me.Visible = False
            Form1.send_but()
        Else
            MsgBox("Please Check Your Netwrok Connection!!")
        End If


    End Sub
End Class