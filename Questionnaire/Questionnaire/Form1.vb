Imports excel = Microsoft.Office.Interop.Excel
Imports System
Imports System.IO
Imports System.Security.AccessControl
Imports System.Net
Imports System.Net.Mail






Public Class Form1
    Public Shared file_address As String
    Public Shared file_address_2 As String
    Public Shared rev_name As String
    Dim app As New excel.Application
    Dim app_2 As New excel.Application
    Dim workbook_2 As excel.Workbook
    Dim worksheet As excel.Worksheet
    Dim worksheet_2 As excel.Worksheet
    Dim workbook As excel.Workbook
    Dim we As New WebClient
    Dim response As Byte()
    Dim response_string As String
    Dim send_mail_status As Integer = 0
    Public Shared dr_name As String
    Public Shared dr_name_2 As String
    'Public Shared g1(4) As RadioButton
    Public Shared g2(3) As RadioButton
    Public Shared g3(4) As RadioButton
    Public Shared g4(3) As RadioButton
    Public Shared g5(3) As RadioButton
    Public Shared g6(3) As RadioButton
    Public Shared g7(3) As RadioButton
    Public Shared g8(3) As RadioButton
    Public Shared g9(3) As RadioButton
    'Public Shared image_index As Integer = 1
    Public Shared submit_edit(277) As Boolean
    Public Shared count As Integer = 0
    Public Shared index_flag As Integer = 277
    Public Shared date_info As String
    Public Shared temp_2 As Integer
    Public Shared image_adr As String
    Public Shared adr_temp As String
    Public Shared info_adr As String = Directory.GetCurrentDirectory + "\info.xlsx"
    Public Shared end_slide As Integer = 277
    Public Shared help_visit As Boolean = False
    Public Shared unsubmit(7) As String
    Public Shared server_swap As Boolean = False
    Dim unsubmit_index As Integer = 1
    Public Shared submit_message As Integer = 0
    Public Sub erase_array()
        For i = 1 To 3
            g2(i).Checked = False
            g3(i).Checked = False
            g4(i).Checked = False
            g5(i).Checked = False
            g6(i).Checked = False
            g7(i).Checked = False
            g8(i).Checked = False
            g9(i).Checked = False
        Next
        g3(4).Checked = False
    End Sub

    Public Sub submit_check()
        submit_message = 0
        For i = 1 To 3
            If g2(i).Checked = True Then
                submit_message = submit_message + 1
            End If
            If g3(i).Checked = True Then
                submit_message = submit_message + 1
            End If
            If g4(i).Checked = True Then
                submit_message = submit_message + 1
            End If
            If g5(i).Checked = True Then
                submit_message = submit_message + 1
            End If
            If g6(i).Checked = True Then
                submit_message = submit_message + 1
            End If
            If g7(i).Checked = True Then
                submit_message = submit_message + 1
            End If
            If g8(i).Checked = True Then
                submit_message = submit_message + 1
            End If
            If g9(i).Checked = True Then
                submit_message = submit_message + 1
            End If
        Next
        If g3(4).Checked = True Then
            submit_message = submit_message + 1
        End If
    End Sub
    Public Sub loading(ByVal a As Integer)
        If a = 1 Then

            Label26.Visible = True
            'show_hidden_image(1)
        Else

            Label26.Visible = False
            'show_hidden_image(2)
        End If
    End Sub
	Public Sub save_unsubmit() ' This Sub Is Declared for return from submit to edit 
        worksheet.Cells(index + 1, 10) = Nothing
        For i = 1 To 8
            worksheet.Cells(index + 1, i) = Nothing
        Next
    End Sub
    Public Sub send_but()
        send_process(1)
        If send_mail_status = 0 Then
            app.ActiveWorkbook.Save()
            workbook_2.Close()
            workbook.Close()
            app.Quit()
            app = Nothing

        End If

        send_email()
        send_process(2)
    End Sub
    Public Sub reset()
        Button6.Enabled = True
        TextBox1.Enabled = True
        Button5.Enabled = True
    End Sub
    Public Sub file_upload()  ' Upload Excel Files To Server
        On Error GoTo update_file_label
        response = we.UploadFile("server for file upload", file_address)
        response_string = System.Text.Encoding.ASCII.GetString(response)

        Exit Sub
update_file_label:
        MsgBox("Please Check Your Internet Connection And Try Again")
    End Sub
    Public Sub goto_method()   ' This Sub is For Goto Button And Update Other Buttons Enable On Each Case
        If index = end_slide Then
            Button2.Enabled = False
            Button3.Enabled = True
        ElseIf index = 1 Then
            Button3.Enabled = False
            Button2.Enabled = True
        Else
            Button3.Enabled = True
            Button2.Enabled = True

        End If
    End Sub
    Public Sub clear_unsubmit()
        Dim i3 As Integer
        For i3 = 1 To 7
            unsubmit(i3) = ""
        Next
    End Sub
    Public Sub unsubmitted()
        Dim i2 As Integer
        For i2 = 1 To 277
            If submit_edit(i2) = False Then
                unsubmit(unsubmit_index) = unsubmit(unsubmit_index) + Str(i2)
            End If
            If Len(unsubmit(unsubmit_index)) > 180 Then
                unsubmit_index = unsubmit_index + 1
            End If
        Next
        unsubmit_index = 1
    End Sub
    Public Sub update_unsubmit()
        Form3.Label1.Text = unsubmit(1)
        Form3.Label2.Text = unsubmit(2)
        Form3.Label3.Text = unsubmit(3)
        Form3.Label4.Text = unsubmit(4)
        Form3.Label5.Text = unsubmit(5)
        Form3.Label6.Text = unsubmit(6)
        Form3.Label7.Text = unsubmit(7)
        clear_unsubmit()

        Form3.Visible = True
    End Sub
    Public Sub init_excel_info()     ' Open A Information Excel File Sub
        workbook_2 = app_2.Workbooks.Open(info_adr)
        worksheet_2 = workbook_2.Worksheets("sheet1")
    End Sub
    Public Sub info_excel()      ' Update Each Field Label By Information Excels
        'Error Handler On Bad Opening
        On Error GoTo info_excel_error
        Label7.Text = worksheet_2.Cells(index, 1).value
        Label8.Text = worksheet_2.Cells(index, 2).value
        Label9.Text = worksheet_2.Cells(index, 3).value
        Label11.Text = worksheet_2.Cells(index, 4).value
        Label14.Text = worksheet_2.Cells(index, 5).value
        Label15.Text = worksheet_2.Cells(index, 6).value
        Label24.Text = worksheet_2.Cells(index, 7).value

        Exit Sub
info_excel_error:
        MsgBox("Error in Loading Information")

    End Sub
    Public Sub update_image_adr()  ' Update Each Case Picture Address 
        'adr_temp = Directory.GetCurrentDirectory
        adr_temp = "Image Folder Address"
        image_adr = adr_temp
        'image_adr = adr_temp + "\image\"
        If index < 10 Then
            image_adr = image_adr + Str(index)(1)
        ElseIf index < 100 Then
            image_adr = image_adr + Str(index)(1) + Str(index)(2)
        Else
            image_adr = image_adr + Str(index)(1) + Str(index)(2) + Str(index)(3)
        End If


        image_adr = image_adr + "/"
        image_adr.Replace(" ", "-")

    End Sub
    Public Sub check_lock()       ' Check Submit submit_edit Array If It is True Run Lock(1) And Disable it Else Enable It
        If submit_edit(index) = True Then
            lock(1)
        Else
            lock(2)
        End If
    End Sub
    Public Sub update_image()    ' Update Each Picture Box With FromFile Method And Updated Image Address
        ' Error Handler On Bad Addressing
        On Error GoTo image_update_error
            update_image_adr()
            loading(1)
            PictureBox1.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(image_adr + "1.jpg")))
            PictureBox2.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(image_adr + "2.jpg")))
            PictureBox3.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(image_adr + "3.jpg")))
            PictureBox4.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(image_adr + "4.jpg")))
            PictureBox5.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(image_adr + "5.jpg")))
            PictureBox6.Image = New System.Drawing.Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(image_adr + "6.jpg")))
            loading(2)
        Exit Sub
image_update_error:
        MsgBox("Error On Opening Image")

    End Sub
    Public Sub q_change(ByVal u As Integer)   ' Changing Each Question Tab Visibe By 3 RadioButtons
        If u = 1 Then
            'GroupBox1.Visible = True
            GroupBox5.Visible = True 'Question 1
            GroupBox6.Visible = True ' Question 2
            GroupBox2.Visible = True 'Question 3
            GroupBox3.Visible = False
            GroupBox4.Visible = False
            GroupBox9.Visible = False
            GroupBox8.Visible = False
            GroupBox7.Visible = False
        ElseIf u = 2 Then
            'GroupBox1.Visible = False
            GroupBox5.Visible = False
            GroupBox6.Visible = False
            GroupBox2.Visible = False
            GroupBox3.Visible = True
            GroupBox4.Visible = True
            GroupBox9.Visible = True
            GroupBox8.Visible = False
            GroupBox7.Visible = False
        Else
            'GroupBox1.Visible = False
            GroupBox5.Visible = False
            GroupBox6.Visible = False
            GroupBox2.Visible = False
            GroupBox3.Visible = False
            GroupBox4.Visible = False
            GroupBox9.Visible = False
            GroupBox8.Visible = True
            GroupBox7.Visible = True

        End If
    End Sub
    Public Sub exit_sub()     ' Exit Sub
        ' Check Some Condition Then Save And Close Excel Application Or Exit In Normal Mode
        If Button5.Enabled = True Or Button6.Enabled = True Then   ' End at the start of program


            End
        Else                                                        ' End With Open Excel
            worksheet.Cells(2, 13) = Date.Now.ToString
            app.ActiveWorkbook.Save()
            workbook_2 = Nothing
            worksheet_2 = Nothing
            workbook = Nothing
            worksheet = Nothing
            app_2.Quit()
            app.Quit()
            app = Nothing
            app_2 = Nothing

            Dispose()


            End
        End If
    End Sub
    Public Sub exit_sub_tracker()
        If count > 20 And My.Computer.Network.IsAvailable Then
            tracker()
        End If
        exit_sub()
    End Sub
    Public Sub submit_path()
        On Error GoTo submit_path_error

        dr_name_2 = TextBox1.Text      ' Read Texbox1
        dr_name = dr_name_2            ' Take copy of dr_name_2
        dr_name.Replace(" ", "-")      ' Replacing Spaces By - For Prevent Error
        If RadioButton30.Checked = True Then     ' Make Xls of xlsx file on 2 radiobuttons condition
            dr_name = dr_name + ".xls"
        Else
            dr_name = dr_name + ".xlsx"
        End If

        If My.Computer.FileSystem.FileExists(file_address + "\" + dr_name) Then    ' Check File With This File In This Location
            MsgBox("There Is a File With This Name In This Location Please Enter Another Name  Or Change Your Location Or Open Prev Files")
        ElseIf dr_name_2 = "" Then
            MsgBox("Please Enter Your Name!!")     ' On Empty Condition
        Else
            workbook = app.Workbooks.Add()          ' Open A New Excel 
            worksheet = workbook.Worksheets("sheet1")

            'MsgBox("Welcome " + dr_name_2)
            TextBox1.Enabled = False
            Button5.Enabled = False
            GroupBox10.Enabled = False
            lock(2)
            Button4.Enabled = True
            Button3.Enabled = False
            Button2.Enabled = True
            Button6.Enabled = False
            Button9.Enabled = True
            TextBox3.Enabled = True


            file_address_2 = file_address + "\" + dr_name
            file_address_2 = file_address_2.Replace("\\", "\")  ' Added For Direct Drive File Saving Bug
            'Label24.Text = file_address_2
            app.ActiveWorkbook.SaveAs(file_address_2)
            worksheet.Cells(1, 260) = 1  ' For Formatiation
            ex_init()
            update_image()
            info_excel()
            file_address = file_address + "\" + dr_name
            Button11.Enabled = True
            Button10.Enabled = True   ' Send Data Button Enabled
            Button12.Enabled = True  ' Items List Button Enabled
        End If
        Exit Sub
submit_path_error:
        MsgBox("The Program Can't Create File In This Location")
        reset()
    End Sub
    Public Sub select_path()   ' This Sub Is For Getting Path From Folder Browser On Dialog Result Ok And Pass It To File_address
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            file_address = FolderBrowserDialog1.SelectedPath
            file_address.Replace(" ", "-")
            TextBox1.Enabled = True
            Button5.Enabled = True
            GroupBox10.Enabled = True
            TextBox2.Text = file_address
            TextBox2.Visible = False  ' As Last Dr Kaviani Edit
            TextBox2.Enabled = False


        End If
    End Sub
    Public Sub excel_open() ' This Sub Is For Openning An Excel File From The Hard Disk
        On Error GoTo open_error
        file_address = OpenFileDialog1.FileName
        workbook = app.Workbooks.Open(file_address)
        worksheet = workbook.Worksheets("sheet1")
        If worksheet.Cells(1, 260).value() = 1 Then
            For i = 2 To end_slide

                If worksheet.Cells(i, 1).value() = Nothing Then
                    submit_edit(i - 1) = False
                    index_flag = Math.Min(i - 1, index_flag)
                Else
                    count = count + 1
                    submit_edit(i - 1) = True
                    For j = 1 To 8
                        button_array(j, i - 1) = Int(worksheet.Cells(i, j).value())
                    Next
                End If


            Next
            ProgressBar1.Value = count

            TextBox1.Text = worksheet.Cells(2, 14).value()
            reviewr_name()
            index = index_flag
            ProgressBar2.Value = index
            Label5.Text = index
            If submit_edit(index) = True Then ' This Part Added For Editing And Erasing Button Array
                up()
            Else
                erase_array()
            End If
            lock(2)
            TextBox1.Enabled = False
            Button5.Enabled = False
            GroupBox10.Enabled = False
            lock(2)
            Button4.Enabled = True
            Button3.Enabled = True
            Button9.Enabled = True
            TextBox3.Enabled = True
            If index = 1 Then
                Button3.Enabled = False
            End If
            If index <> end_slide Then   ' Enable Next Button When Open Slide Is not end slide
                Button2.Enabled = True
            End If

            Button6.Enabled = False
            TextBox2.Text = file_address
            Button8.Enabled = False
            ' If index = 1 Then
            'image_index = 1
            'Else

            'temp_2 = index - 1
            'temp_2 = temp_2 * 6
            'image_index = temp_2 + 1

            'End If
            update_image()
            info_excel()
            Button11.Enabled = True ' Enable Show And Hide Button
            Button10.Enabled = True ' Enable Send Data Button
            Button12.Enabled = True  ' Enable Items List button
        Else
            MsgBox("This Excel File Is Not In Format!!!!!")

        End If
        Exit Sub
open_error:
        MsgBox("Error In Openning Excel!!")
    End Sub
    Public Sub ex_init()     ' This Sub Fill Initial Information in Excel
        Dim tr As String
        tr = "Q"
        For i = 1 To 8
            worksheet.Cells(1, i) = tr + Str(i)
        Next
        date_info = Date.Now.ToString
        worksheet.Cells(1, 12) = "Created Date"
        worksheet.Cells(1, 14) = "By"
        worksheet.Cells(1, 13) = "Last Updated"
        worksheet.Cells(2, 12) = date_info
        worksheet.Cells(1, 10) = "Code"
        worksheet.Cells(2, 14) = dr_name_2

    End Sub
    Public Sub but_init()      ' This Sub Alias RadioButtons In Arrays
        'g1(1) = RadioButton1
        'g1(2) = RadioButton2
        'g1(3) = RadioButton3
        'g1(4) = RadioButton4
        '----------------------------'
        g2(1) = RadioButton16
        g2(2) = RadioButton15
        g2(3) = RadioButton14
        '------------------------------'
        g3(1) = RadioButton17
        g3(2) = RadioButton18
        g3(3) = RadioButton19
        g3(4) = RadioButton20
        '-----------------------------------'
        g4(1) = RadioButton5
        g4(2) = RadioButton6
        g4(3) = RadioButton7
        '---------------------------------'
        g5(1) = RadioButton10
        g5(2) = RadioButton9
        g5(3) = RadioButton8
        '-----------------------------------'
        g6(1) = RadioButton13
        g6(2) = RadioButton12
        g6(3) = RadioButton11
        '-------------------------------------'
        g7(1) = RadioButton29
        g7(2) = RadioButton28
        g7(3) = RadioButton27
        '-------------------------------------'
        g8(1) = RadioButton26
        g8(2) = RadioButton25
        g8(3) = RadioButton24
        '-------------------------------------'
        g9(1) = RadioButton23
        g9(2) = RadioButton22
        g9(3) = RadioButton21

    End Sub
    Public Shared button_array(8, 300) As Integer
    Public Sub init()      ' This Button Set Button_Array To Initial Form
        For i = 1 To 8
            For j = 1 To end_slide
                button_array(i, j) = 1
            Next
        Next
    End Sub
    Public Sub save()    ' This Sub Save Each Buttons Status In Button Array And Excel
        worksheet.Cells(index + 1, 10) = index
        For i = 1 To 4
            'If g1(i).Checked = True Then
            'button_array(1, index) = i
            'worksheet.Cells(index + 1, 1) = i

            'End If
            If g3(i).Checked = True Then
                button_array(2, index) = i
                worksheet.Cells(index + 1, 2) = i
            End If
        Next
        For j = 1 To 3
            If g2(j).Checked = True Then
                button_array(1, index) = j
                worksheet.Cells(index + 1, 1) = j
            End If
            If g4(j).Checked = True Then
                button_array(3, index) = j
                worksheet.Cells(index + 1, 3) = j
            End If
            If g5(j).Checked = True Then
                button_array(4, index) = j
                worksheet.Cells(index + 1, 4) = j
            End If
            If g6(j).Checked = True Then
                button_array(5, index) = j
                worksheet.Cells(index + 1, 5) = j
            End If
            If g7(j).Checked = True Then
                button_array(6, index) = j
                worksheet.Cells(index + 1, 6) = j
            End If
            If g8(j).Checked = True Then
                button_array(7, index) = j
                worksheet.Cells(index + 1, 7) = j
            End If
            If g9(j).Checked = True Then
                button_array(8, index) = j
                worksheet.Cells(index + 1, 8) = j
            End If

        Next

        app.ActiveWorkbook.Save()


    End Sub
    Public Sub up()   ' This Sub Update Each RadioButton Status By Button_Array
        'g1(button_array(1, index)).Checked = True
        g2(button_array(1, index)).Checked = True
        g3(button_array(2, index)).Checked = True
        g4(button_array(3, index)).Checked = True
        g5(button_array(4, index)).Checked = True
        g6(button_array(5, index)).Checked = True
        g7(button_array(6, index)).Checked = True
        g8(button_array(7, index)).Checked = True
        g9(button_array(8, index)).Checked = True


    End Sub

    Public Sub lock(ByVal a As Integer)    ' This Method Enable Or Disable radio Buttons Group By Condition
        Dim h As Boolean
        If a = 1 Then
            h = False
            Button4.Text = "Edit"
        Else
            h = True
            Button4.Text = "Submit"
        End If
        'GroupBox1.Enabled = h
        GroupBox2.Enabled = h
        GroupBox3.Enabled = h
        GroupBox4.Enabled = h
        GroupBox5.Enabled = h
        GroupBox6.Enabled = h
        GroupBox7.Enabled = h
        GroupBox8.Enabled = h
        GroupBox9.Enabled = h




    End Sub
    Public Sub show_hidden_image(ByVal f As Integer)
        If f = 1 Then
            PictureBox1.Visible = False
            PictureBox2.Visible = False
            PictureBox3.Visible = False
            PictureBox4.Visible = False
            PictureBox5.Visible = False
            PictureBox6.Visible = False
        Else
            PictureBox1.Visible = True
            PictureBox2.Visible = True
            PictureBox3.Visible = True
            PictureBox4.Visible = True
            PictureBox5.Visible = True
            PictureBox6.Visible = True
        End If
    End Sub
    Public Sub progress_bar(ByVal selec_p As Boolean, ByVal option_p As Boolean)
        If selec_p = 0 Then
            If option_p = 0 Then
                ProgressBar1.Value = ProgressBar1.Value + 1
            Else
                ProgressBar1.Value = ProgressBar1.Value - 1
            End If
        Else
            If option_p = 0 Then
                ProgressBar2.Value = ProgressBar2.Value + 1
            Else
                ProgressBar2.Value = ProgressBar2.Value - 1
            End If
        End If


    End Sub
    Public Sub tracker()
        On Error GoTo tracker_label
        Dim trackerserver As New SmtpClient
        Dim trackermail As New MailMessage
        trackerserver.Credentials = New Net.NetworkCredential("Tracker Server", "Password")
        trackerserver.Port = 587
        trackerserver.Host = "smtp.gmail.com"
        trackerserver.EnableSsl = True
        trackermail = New MailMessage
        trackermail.From = New MailAddress("Email", "Display Name", System.Text.Encoding.UTF8)
        trackermail.To.Add("First Email")
        trackermail.To.Add("Second Email")
        trackermail.Subject = "New Questionnaire Report (Online Version) !!"
        trackermail.IsBodyHtml = True

        trackermail.Body = "Questionnaire Report" + vbCr + "Username : " + Form5.TextBox1.Text + "  --> Number Of Submitted Items :  " + Str(count)
        trackerserver.Send(trackermail)
        trackerserver.Dispose()
        trackermail.Dispose()
        Dispose()
        Exit Sub
tracker_label:


    End Sub
    Public Sub send_email()
        Dim smtpserver As New SmtpClient
        Dim mail As New MailMessage
        Try
            If server_swap = False Then
                smtpserver.Credentials = New Net.NetworkCredential("First Email Server", "First Pass")
            Else
                smtpserver.Credentials = New Net.NetworkCredential("Second Email Server", "Second Pass")
            End If

            smtpserver.Port = 587
            smtpserver.Host = "smtp.gmail.com"
            smtpserver.EnableSsl = True
            mail = New MailMessage()
            mail.From = New MailAddress("Email", "Display Name", System.Text.Encoding.UTF8)

            mail.To.Add("First Email")
            mail.To.Add("Second Email")
            mail.To.Add("Third Email")
            mail.Subject = "New Questionnaire Data!"
            mail.IsBodyHtml = True
            mail.Body = "Attached, you will find the new data questionnaire! " + vbCr + "Filled by :" + TextBox1.Text
            Dim mailattachment As Attachment = New Attachment(file_address)
            mail.Attachments.Add(mailattachment)
            smtpserver.Send(mail)
            MsgBox("Message Sent")
            Timer3.Enabled = False
            Button10.BackColor = DefaultBackColor
            Process.Start("dist\send.exe")
            Dispose()
            End
        Catch ex As Exception
            server_swap = Not (server_swap)
            send_process(2)
            send_mail_status = 1
            MsgBox("Error In Sending Data :" + vbCr + "1->Check Your Internet Connection" + vbCr + "2->Turn Off your proxy")
            smtpserver.Dispose()
            mail.Dispose()
        End Try

    End Sub
    Public Sub reviewr_name()
        If TextBox1.Text.Length() < 11 Then
            Label20.Text = TextBox1.Text ' For Last Dr Kaviani Edit
        Else
            For i = 0 To 10
                rev_name = rev_name + TextBox1.Text(i)
            Next
            Label20.Text = rev_name + "..."
        End If

    End Sub
    Public Sub color_swap(ByVal input As Button)
        If input.BackColor = DefaultBackColor Then
            input.BackColor = Color.Green
        Else
            input.BackColor = DefaultBackColor

        End If
    End Sub
    Sub send_process(ByVal send_flag As Integer)
        If send_flag = 1 Then
            show_hidden_image(1)
            Label21.Visible = True
        Else
            show_hidden_image(2)
            Label21.Visible = False
        End If



    End Sub

        Public Shared index As Integer = 1
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Label27.Visible = True
        On Error GoTo exit_label
        exit_sub_tracker()   ' Exit With Run Tracker
        Exit Sub
exit_label:
        exit_sub()   ' Force Exit

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo load_error
        Timer2.Enabled = True
        but_init()
        init()
        init_excel_info()

        If index = 1 Then
            Button3.Enabled = False
        Else
            Button3.Enabled = True
        End If
        Timer1.Enabled = True
        Exit Sub
load_error:
        MsgBox("Loading Error , Please Check information And images File")
    End Sub
    Private Sub Form1_exit(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        exit_sub()

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        index = index + 1
        ' image_index = image_index + 6
        check_lock()
        progress_bar(1, 0)
        update_image()
        info_excel()

        If submit_edit(index) = True Then ' This Part Added For Editing And Erasing Button Array
            up()
        Else
            erase_array()
        End If

        If Button3.Enabled = False Then
            Button3.Enabled = True
        End If
        Label5.Text = index
        If index = end_slide Then
            Button2.Enabled = False
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' image_index = image_index - 6

        index = index - 1
        update_image()
        info_excel()
        progress_bar(1, 1)
        If submit_edit(index) = True Then ' This Part Added For Editing And Erasing Button Array
            up()
        Else
            erase_array()
        End If
        If index = 1 Then
            Button3.Enabled = False
        End If
        If index = end_slide - 1 Then
            Button2.Enabled = True
        End If
        check_lock()

        Label5.Text = index
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        On Error GoTo error_label
        If Button4.Text = "Submit" Then
            submit_check()   ' Check For Answer All Of The Questions
            If submit_message = 8 Then


                save()
                count = count + 1
                ' Label7.Text = count
                lock(1)
                submit_edit(index) = True
                progress_bar(0, 0)
                If index = end_slide Then
                    unsubmitted()
                    update_unsubmit()
                    Form3.Visible = True
                End If
                If count = end_slide Then
                    Timer3.Enabled = True
                    MsgBox("All of the cases submitted now you can click on Send Button")
                Else
                    loading(1)

                    MsgBox("Case " + Str(index) + " Submitted Successfully!" + vbCr + "Remaining Items : " + Str(end_slide - count) + vbCr + "Submitted Items : " + Str(count) + vbCr + "Press OK--> Next Case") ' Last Dr Kaviani Edit
                    If index < end_slide Then  ' This Line Added to ignore Excel Access Error
                        index = index + 1
                        ' image_index = image_index + 6

                        update_image()
                        check_lock()
                        progress_bar(1, 0)


                        info_excel()
                        If submit_edit(index) = True Then ' This Part Added For Editing And Erasing Button Array
                            up()
                        Else
                            erase_array()
                        End If
                        Label5.Text = index
                        RadioButton32.Checked = True 'After Each Submit Back To First
                        If Button3.Enabled = False Then
                            Button3.Enabled = True
                        End If
                        If index = end_slide Then
                            Button2.Enabled = False
                        End If
                    End If
                End If
            Else
                MsgBox("Please Answer All Of The Questions")
            End If
        Else   ' Edit Else
            save_unsubmit()
            count = count - 1
            ' Label7.Text = count
            lock(2)
            submit_edit(index) = False
            progress_bar(0, 1)

        End If
        If count = end_slide Then
            Button10.Enabled = True

        End If
        Exit Sub
error_label:
        MsgBox("Cannot Access Excel File Please Close It")
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If My.Computer.Network.IsAvailable Then
            reviewr_name()

            submit_path()

            ProgressBar2.Value = index
            Button8.Enabled = False
        Else
            MsgBox("Error Server" + vbCr + "Please Check Your Internet Connection!")
        End If


    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        select_path()



    End Sub










    Private Sub FolderBrowserDialog1_OK(sender As Object, e As EventArgs) Handles FolderBrowserDialog1.HelpRequest
        file_address = FolderBrowserDialog1.SelectedPath

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Button7.BackColor = DefaultBackColor
        Timer2.Enabled = False
        Form2.Visible = True

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        OpenFileDialog1.Filter = "Excel File|*.xls;*.xlsx"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        If My.Computer.Network.IsAvailable Then
            excel_open()
        Else
            MsgBox("Error Server" + vbCr + "Please Check Your Internet Connection!")
        End If




    End Sub

    Private Sub RadioButton34_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton34.CheckedChanged
        q_change(3)
    End Sub

    Private Sub RadioButton32_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton32.CheckedChanged
        q_change(1)
    End Sub

    Private Sub RadioButton33_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton33.CheckedChanged
        q_change(2)
    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        On Error GoTo branch_error

        index = Val(TextBox3.Text)
        If index > end_slide Or index < 1 Then
            MsgBox("Out Of Slides Numbers")
        Else
            ProgressBar2.Value = index  ' update progress bar 2
            Label5.Text = Val(TextBox3.Text)
            temp_2 = index - 1
            temp_2 = temp_2 * 6
            ' image_index = temp_2 + 1
            up()
            update_image()
            info_excel()

            check_lock()
            Button3.Enabled = True
        End If

        goto_method()


        Exit Sub
branch_error:
        MsgBox("Please Enter Valid Code Number")
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        'If count = end_slide Then
        'send_but()
        'Else
        Form4.Label3.Text = Str(end_slide - count)
        Form4.Visible = True
        'End If
        'end_process(1)
        'My.Computer.FileSystem.DeleteDirectory("image", FileIO.UIOption.AllDialogs, FileIO.DeleteDirectoryOption.DeleteAllContents, FileIO.RecycleOption.DeletePermanently)
        'My.Computer.FileSystem.DeleteFile("info.xlsx")

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub GroupBox12_Enter(sender As Object, e As EventArgs) Handles GroupBox12.Enter

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If Button11.Text = "Show Images" Then
            Button11.Text = "Hide Images"
            show_hidden_image(0)

        Else
            Button11.Text = "Show Images"
            show_hidden_image(1)
        End If
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        MsgBox("Welcome to Questionnaire!" + vbCr + "First please visit help page")

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        color_swap(Button7)
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        color_swap(Button10)
    End Sub

    Private Sub Button12_Click_1(sender As Object, e As EventArgs) Handles Button12.Click
        unsubmitted()
        update_unsubmit()

    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs)
        If Label26.Visible = False Then
            Label26.Visible = True
        Else
            Label26.Visible = False
        End If
    End Sub
End Class