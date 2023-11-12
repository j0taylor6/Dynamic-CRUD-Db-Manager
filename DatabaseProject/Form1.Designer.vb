<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dgvView = New DataGridView()
        btnLoadDb = New Button()
        btnReload = New Button()
        btnCreate = New Button()
        btnNew = New Button()
        btnUpdate = New Button()
        btnDelete = New Button()
        btnLoadTbs = New Button()
        pnlTextBoxes = New Panel()
        CType(dgvView, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvView
        ' 
        dgvView.AllowUserToAddRows = False
        dgvView.AllowUserToDeleteRows = False
        dgvView.AllowUserToResizeColumns = False
        dgvView.AllowUserToResizeRows = False
        dgvView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvView.Location = New Point(415, 12)
        dgvView.Name = "dgvView"
        dgvView.RowTemplate.Height = 25
        dgvView.Size = New Size(769, 497)
        dgvView.TabIndex = 0
        ' 
        ' btnLoadDb
        ' 
        btnLoadDb.Location = New Point(12, 12)
        btnLoadDb.Name = "btnLoadDb"
        btnLoadDb.Size = New Size(397, 53)
        btnLoadDb.TabIndex = 1
        btnLoadDb.Text = "Load Database"
        btnLoadDb.UseVisualStyleBackColor = True
        ' 
        ' btnReload
        ' 
        btnReload.Location = New Point(12, 470)
        btnReload.Name = "btnReload"
        btnReload.Size = New Size(397, 39)
        btnReload.TabIndex = 2
        btnReload.Text = "Reload"
        btnReload.UseVisualStyleBackColor = True
        ' 
        ' btnCreate
        ' 
        btnCreate.AccessibleRole = AccessibleRole.None
        btnCreate.Location = New Point(12, 364)
        btnCreate.Name = "btnCreate"
        btnCreate.Size = New Size(193, 47)
        btnCreate.TabIndex = 8
        btnCreate.Text = "Create"
        btnCreate.UseVisualStyleBackColor = True
        ' 
        ' btnNew
        ' 
        btnNew.AccessibleRole = AccessibleRole.None
        btnNew.Location = New Point(216, 364)
        btnNew.Name = "btnNew"
        btnNew.Size = New Size(193, 47)
        btnNew.TabIndex = 9
        btnNew.Text = "New"
        btnNew.UseVisualStyleBackColor = True
        ' 
        ' btnUpdate
        ' 
        btnUpdate.AccessibleRole = AccessibleRole.None
        btnUpdate.Location = New Point(12, 417)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(193, 47)
        btnUpdate.TabIndex = 10
        btnUpdate.Text = "Update"
        btnUpdate.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.AccessibleRole = AccessibleRole.None
        btnDelete.Location = New Point(216, 417)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(193, 47)
        btnDelete.TabIndex = 11
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnLoadTbs
        ' 
        btnLoadTbs.Location = New Point(12, 319)
        btnLoadTbs.Name = "btnLoadTbs"
        btnLoadTbs.Size = New Size(397, 39)
        btnLoadTbs.TabIndex = 12
        btnLoadTbs.Text = "Load Text Boxes"
        btnLoadTbs.UseVisualStyleBackColor = True
        ' 
        ' pnlTextBoxes
        ' 
        pnlTextBoxes.Location = New Point(12, 71)
        pnlTextBoxes.Name = "pnlTextBoxes"
        pnlTextBoxes.Size = New Size(397, 242)
        pnlTextBoxes.TabIndex = 13
        ' 
        ' form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1196, 513)
        Controls.Add(pnlTextBoxes)
        Controls.Add(btnLoadTbs)
        Controls.Add(btnDelete)
        Controls.Add(btnUpdate)
        Controls.Add(btnNew)
        Controls.Add(btnCreate)
        Controls.Add(btnReload)
        Controls.Add(btnLoadDb)
        Controls.Add(dgvView)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "form1"
        Text = "Dynamic Crud Database Manager"
        CType(dgvView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dgvView As DataGridView
    Friend WithEvents btnLoadDb As Button
    Friend WithEvents btnReload As Button
    Friend WithEvents btnCreate As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnLoadTbs As Button
    Friend WithEvents pnlTextBoxes As Panel
End Class
