Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Dim process As System.Diagnostics.Process = Process.GetCurrentProcess
        process.Kill()
    End Sub

    Private Sub btnCalendar_Click(sender As Object, e As EventArgs) Handles btnCalendar.Click
        Me.Hide()
        FormCalendar.Show()
    End Sub
End Class
