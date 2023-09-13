Public Class FormCalendar
    Private Sub FormCalendar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Mengatur header kolom dengan nama hari dalam seminggu
        gridCalendar.ColumnCount = 7
        gridCalendar.Columns(0).Name = "Minggu"
        gridCalendar.Columns(1).Name = "Senin"
        gridCalendar.Columns(2).Name = "Selasa"
        gridCalendar.Columns(3).Name = "Rabu"
        gridCalendar.Columns(4).Name = "Kamis"
        gridCalendar.Columns(5).Name = "Jumat"
        gridCalendar.Columns(6).Name = "Sabtu"

        ' Mengisi sel-sel dengan tanggal sesuai dengan kalender saat ini
        FillCalendar(DateTime.Now)
    End Sub

    Private Sub FillCalendar(targetDate As Date)
        ' Menghapus semua baris yang ada
        gridCalendar.Rows.Clear()

        ' Menghitung tanggal awal dan jumlah hari dalam bulan
        Dim firstDayOfMonth As Date = New Date(targetDate.Year, targetDate.Month, 1)
        Dim daysInMonth As Integer = Date.DaysInMonth(targetDate.Year, targetDate.Month)

        ' Menambahkan baris-baris ke DataGridView sesuai dengan jumlah minggu dalam bulan
        Dim currentDay As Date = firstDayOfMonth
        While currentDay.Month = targetDate.Month
            Dim row As New DataGridViewRow()
            For i As Integer = 0 To 6
                ' Memeriksa apakah indeks valid sebelum mengisi sel
                If i >= CInt(currentDay.DayOfWeek) AndAlso currentDay.Day <= daysInMonth Then
                    Dim cell As New DataGridViewTextBoxCell()
                    cell.Value = currentDay.Day.ToString()
                    row.Cells.Add(cell)
                    'row.Cells(i).Value = currentDay.Day.ToString()
                    currentDay = currentDay.AddDays(1)
                Else
                    Dim cell As New DataGridViewTextBoxCell()
                    cell.Value = "-"
                    row.Cells.Add(cell)
                End If
            Next
            gridCalendar.Rows.Add(row)
        End While
    End Sub


    Private Sub FormCalendar_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Dim process As System.Diagnostics.Process = Process.GetCurrentProcess
        process.Kill()
    End Sub

    Private Sub MonthCalendar1_DateSelected(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        Dim tglSelected As Date = MonthCalendar1.SelectionStart

        lblTglSelected.Text = tglSelected.ToShortDateString()
    End Sub

End Class