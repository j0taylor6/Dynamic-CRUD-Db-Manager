Imports System.Collections.Immutable
Imports System.Data.OleDb
Imports System.Windows.Forms

Module CRUD
    Private _tableName As String = String.Empty
    Public FilePath As String = GetFilePath()

    Public Function Connection() As OleDbConnection


        If String.IsNullOrEmpty(FilePath) Then
            MessageBox.Show("No file selected.")
            Return Nothing

        Else
            Dim connectionString As String = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={FilePath};"
            Try
                Return New OleDbConnection(connectionString)
            Catch ex As Exception
                MessageBox.Show("Error occurred creating connection: " & ex.Message)
                Return Nothing
            End Try
        End If
    End Function

    Public Function GetFilePath() As String
        Dim openFileDialog As New OpenFileDialog
        openFileDialog.Filter = "Access Files (*.accdb)|*.accdb|All Files (*.*)|*.*" ' Filter to only show .accdb files

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            Return openFileDialog.FileName
        Else
            Return Nothing ' Return Nothing if no file is selected
        End If
    End Function


    Public Function GetTableName() As String
        ' Check if the table name has already been set
        If String.IsNullOrEmpty(_tableName) Then
            ' If not set, prompt the user for the table name
            _tableName = InputBox("Please enter the table name", "Table Input", "Table Name Here")
            If String.IsNullOrEmpty(_tableName) Then
                MessageBox.Show("No input provided.")
            End If
        End If
        ' Return the table name
        Return _tableName
    End Function



    Public Sub DisplayDb(ByVal dgvView As DataGridView)

        Dim query As String = $"SELECT * FROM {GetTableName()}" ' Replace with your table name

        Using conn As OleDbConnection = Connection()

            Try
                Dim command As New OleDbCommand(query, conn)
                Dim table As New DataTable()
                Dim adapter As New OleDbDataAdapter(command)
                adapter.Fill(table)
                dgvView.DataSource = table ' Assuming dgvView is your DataGridView

            Catch ex As Exception
                MessageBox.Show("Error occurred: " & ex.Message)
                Clipboard.SetText(ex.Message)

            End Try

        End Using

    End Sub

    Public Function GetColumnNames() As List(Of String)
        Dim columnNames As New List(Of String)()

        Dim tableName As String = GetTableName()
        If String.IsNullOrEmpty(tableName) Then
            MessageBox.Show("Table name is required.")
            Return columnNames
        End If

        Dim query As String = $"SELECT * FROM {tableName} WHERE 1 = 0" ' Query to get column schema without data

        Using conn As OleDbConnection = Connection()
            If conn Is Nothing Then
                MessageBox.Show("Failed to establish a database connection.")
                Return columnNames
            End If

            Try
                conn.Open()
                Using command As New OleDbCommand(query, conn)
                    Using reader As OleDbDataReader = command.ExecuteReader(CommandBehavior.SchemaOnly)
                        Dim tableSchema As DataTable = reader.GetSchemaTable()
                        For Each row As DataRow In tableSchema.Rows
                            columnNames.Add(row("ColumnName").ToString())
                        Next
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error occurred: " & ex.Message)
                Clipboard.SetText(ex.Message)
            End Try
        End Using

        Return columnNames
    End Function

    Public Function CreateTextBoxesForColumns(ByVal parentControl As Control) As List(Of TextBox)
        Dim textBoxes As New List(Of TextBox)()
        Dim columnNames As List(Of String) = GetColumnNames()
        Dim yPos As Integer = 10

        For Each columnName In columnNames
            Dim newTextBox As New TextBox()
            newTextBox.Name = "txt" & columnName
            newTextBox.PlaceholderText = columnName
            newTextBox.Location = New Point(10, yPos)
            newTextBox.Width = 200

            yPos += 30 ' Increment the Y position for the next TextBox

            parentControl.Controls.Add(newTextBox)
            textBoxes.Add(newTextBox)
        Next

        Return textBoxes
    End Function

    Public Sub Reload(ByVal SQL As String, ByVal DGV As DataGridView)
        Using conn As OleDbConnection = Connection()
            If conn Is Nothing Then
                MessageBox.Show("Failed to establish a database connection.")
                Exit Sub
            End If

            Try
                conn.Open()

                Using cmd As New OleDbCommand(SQL, conn)
                    Dim table As New DataTable()
                    Dim adapter As New OleDbDataAdapter(cmd)
                    adapter.Fill(table)
                    DGV.DataSource = table ' Update DataGridView with the new data
                End Using
            Catch ex As Exception
                MessageBox.Show("Error occurred while reloading data: " & ex.Message)
                Clipboard.SetText(ex.Message)
            Finally
                If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub

    Public Function Create(ByVal values As Dictionary(Of String, String)) As Boolean
        Dim tableName As String = GetTableName()
        If String.IsNullOrEmpty(tableName) Then
            MessageBox.Show("Table name is required.")
            Return False
        End If

        If values Is Nothing OrElse values.Count = 0 Then
            MessageBox.Show("No data provided for insertion.")
            Return False
        End If

        Dim columns As String = String.Join(", ", values.Keys)
        Dim parameters As String = String.Join(", ", values.Keys.Select(Function(k) "@" & k))

        Dim query As String = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})"

        Using conn As OleDbConnection = Connection()
            If conn Is Nothing Then
                MessageBox.Show("Failed to establish a database connection.")
                Return False
            End If

            Try
                conn.Open()
                Using command As New OleDbCommand(query, conn)
                    ' Add parameters to command
                    For Each kvp In values
                        command.Parameters.AddWithValue("@" & kvp.Key, kvp.Value)
                    Next

                    command.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                MessageBox.Show("Error occurred while creating record: " & ex.Message)
                Clipboard.SetText(ex.Message)
                Return False
            Finally
                If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using

        Return True
    End Function

    Public Function GetValuesFromTextBoxes(ByVal parentControl As Control) As Dictionary(Of String, String)
        Dim values As New Dictionary(Of String, String)()

        ' Loop through all controls in the parent control
        For Each control As Control In parentControl.Controls
            ' Check if the control is a TextBox and its name starts with "txt"
            If TypeOf control Is TextBox AndAlso control.Name.StartsWith("txt") Then
                Dim textBox As TextBox = DirectCast(control, TextBox)
                Dim columnName As String = textBox.Name.Substring(3) ' Remove "txt" prefix to get the column name
                Dim columnValue As String = textBox.Text ' Get the text value of the TextBox

                ' Add the column name and value to the dictionary
                Try
                    values.Add(columnName, columnValue)
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try

            End If
        Next

        Return values
    End Function

    Public Sub DeleteSelectedRow(ByVal dgv As DataGridView)
        If dgv.SelectedRows.Count = 0 Then
            MessageBox.Show("No row selected.")
            Return
        End If

        ' Replace '0' with the index of the column that contains your unique identifier
        Dim uniqueIdentifierColumnIndex As Integer = 0
        Dim id As String = dgv.SelectedRows(0).Cells(uniqueIdentifierColumnIndex).Value.ToString()
        Dim tableName As String = GetTableName()

        If String.IsNullOrEmpty(tableName) Then
            MessageBox.Show("Table name is required.")
            Return
        End If

        ' Replace 'Id' with the actual name of your primary key column in the database
        Dim query As String = $"DELETE FROM {tableName} WHERE Id = {GetIdColumnName()}"

        Using conn As OleDbConnection = Connection()
            If conn Is Nothing Then
                MessageBox.Show("Failed to establish a database connection.")
                Return
            End If

            Try
                conn.Open()
                Using cmd As New OleDbCommand(query, conn)
                    ' Add the parameter value for the ID
                    cmd.Parameters.AddWithValue("@Id", id)
                    Dim result As Integer = cmd.ExecuteNonQuery()

                    If result > 0 Then
                        MessageBox.Show("Row deleted successfully.")
                    Else
                        MessageBox.Show("No row was deleted.")
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error occurred while deleting the row: " & ex.Message)
            Finally
                If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub

    Public Function GetIdColumnName() As String
        Dim tableName As String = GetTableName()
        If String.IsNullOrEmpty(tableName) Then
            MessageBox.Show("Table name is required.")
            Return String.Empty
        End If

        Using conn As OleDbConnection = Connection()
            Dim query As String = $"SELECT * FROM {tableName} WHERE 1 = 0"
            Try
                conn.Open()
                Using command As New OleDbCommand(query, conn)
                    Using reader As OleDbDataReader = command.ExecuteReader(CommandBehavior.KeyInfo)
                        Dim schemaTable As DataTable = reader.GetSchemaTable()
                        For Each row As DataRow In schemaTable.Rows
                            If row("IsKey") AndAlso row("IsKey").Equals(True) Then
                                Return row("ColumnName").ToString()
                            End If
                        Next
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error occurred: " & ex.Message)
                Clipboard.SetText(ex.Message)
                Return String.Empty
            Finally
                If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using

        Return String.Empty
    End Function

End Module
