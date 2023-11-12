Imports System.Data.OleDb
Imports System.Runtime.InteropServices

Public Class form1
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function AllocConsole() As Boolean
    End Function

    Private Sub btnLoadDb_Click(sender As Object, e As EventArgs) Handles btnLoadDb.Click
        DisplayDb(dgvView)
    End Sub

    Public Sub btnLoadTbs_Click(sender As Object, e As EventArgs) Handles btnLoadTbs.Click
        Dim txtBoxes As List(Of TextBox) = CRUD.CreateTextBoxesForColumns(pnlTextBoxes)
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        CRUD.Reload($"SELECT * FROM {GetTableName()}", dgvView)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearTextBoxes(pnlTextBoxes)
    End Sub

    Private Sub ClearTextBoxes(parentControl As Control)
        For Each ctrl As Control In parentControl.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Clear()
            ElseIf ctrl.HasChildren Then
                ClearTextBoxes(ctrl) ' Recursive call for nested controls
            End If
        Next
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim values As Dictionary(Of String, String) = GetValuesFromTextBoxes(pnlTextBoxes)
        Dim success As Boolean = Create(values)
        If success Then
            MessageBox.Show("Record created successfully")
        Else
            MessageBox.Show("Failed to create record")
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        CRUD.DeleteSelectedRow(dgvView)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

    End Sub


    Private Sub dgvView_SelectionChanged(sender As Object, e As EventArgs) Handles dgvView.SelectionChanged


    End Sub

End Class
