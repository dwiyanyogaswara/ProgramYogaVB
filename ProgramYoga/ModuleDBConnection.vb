Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports SD3Fungsi
Module ModuleDBConnection
    Dim connStringOracle, conStrOracleODP As String
    Dim userOrcl, passwordOrcl, serverOrcl, ODPServerOrcl As String
    Dim newEncript As New SettingLib.Class1

    Dim VAR_KODE_DC As String

    Private IpLocal As String = IpKomp.GetIPAddress()
    Private VAR_PROD_VER As String = Application.ProductVersion

    Public Sub CekVersiProgram()
        Try
            Dim Ret
            Ret = newEncript.GetVersiODP(conStrOracleODP, VAR_KODE_DC, "DCTRNBAP.EXE", VAR_PROD_VER, IpLocal)
            If Ret.ToString.Contains("OKE") = False Then
                MsgBox(Ret)
                End
            End If
        Catch ex As Exception
            MsgBox(ex.StackTrace.ToString())
            'MsgBox(strKoneksi_OleDB & KodeDc & "RETURTOKOPC.EXE" & VAR_PROD_VER & IpLocal)
        End Try
    End Sub
    Private Sub CreateConnectionStringOracle(ByVal userOrcl As String, ByVal passwordOrcl As String, ByVal serverOrcl As String, ByVal ODPServerOrcl As String)
        connStringOracle = "Provider=MSDAORA.1;User ID=" & userOrcl & ";Password = " & passwordOrcl & " ;Data Source=" & serverOrcl
        conStrOracleODP = "Data Source=" & ODPServerOrcl & ";Password=" & passwordOrcl & ";User ID=" & userOrcl & ";"
    End Sub

    Public Function GetUserOracle() As String
        Return newEncript.GetVariabel("UserOrcl")
    End Function

    Public Function GetPasswordOracle() As String
        Return newEncript.GetVariabel("PasswordOrcl")
    End Function

    Public Function GetServerOracle() As String
        Return newEncript.GetVariabel("ServerOrcl")
    End Function

    Public Function GetServerODPOracle() As String
        Return newEncript.GetVariabel("ODPOrcl")
    End Function

    Public Function CheckConnection()
        userOrcl = GetUserOracle()
        passwordOrcl = GetPasswordOracle()
        serverOrcl = GetServerOracle()
        ODPServerOrcl = GetServerODPOracle()

        Try
            CreateConnectionStringOracle(userOrcl, passwordOrcl, serverOrcl, ODPServerOrcl)

            VAR_KODE_DC = ExecScalarODP("Select TBL_DC_KODE FROM DC_TABEL_DC_T")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CreateOracleConnectionODP() As OracleConnection
        Return New OracleConnection(conStrOracleODP)
    End Function

    Public Function CreateOracleCommandODP(ByRef connection As OracleConnection, Optional ByVal sql As String = "") As OracleCommand
        Return New OracleCommand(sql, connection)
    End Function

    Public Function ExecScalarODP(ByVal sql As String) As String
        Using connection = CreateOracleConnectionODP()
            Using cmd = CreateOracleCommandODP(connection)
                cmd.InitialLONGFetchSize = 9999 'Handle datatype : LONG
                Try
                    If (connection.State = ConnectionState.Open) Then
                        connection.Close()
                    End If
                    connection.Open()
                    cmd.CommandText = sql
                    Return cmd.ExecuteScalar()
                Catch ex As Exception
                    'WriteErrorGeneral(ex.Message)
                    Return Nothing
                End Try
            End Using
        End Using
    End Function
End Module
