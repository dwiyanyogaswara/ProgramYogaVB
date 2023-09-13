Public Class FormLogin
    Dim versionNumber, productName1 As String
    Dim username, password As String

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        productName1 = Application.ProductName
        versionNumber = Application.ProductVersion

        Dim connProgram As Boolean = CheckConnection()
        If connProgram Then
            CekVersiProgram()
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        username = tbUsername.Text
        password = tbPassword.Text

        MessageBox.Show(username & password & ProductName & versionNumber)
        Me.Hide()
        Form1.Show()
    End Sub
End Class