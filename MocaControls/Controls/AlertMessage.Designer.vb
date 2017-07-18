
Namespace Win

	<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
	Partial Class AlertMessage
		Inherits System.Windows.Forms.UserControl

		'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
		<System.Diagnostics.DebuggerNonUserCode()> _
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If

                _timer.Stop()
            Finally
                MyBase.Dispose(disposing)
            End Try
		End Sub

		'Windows フォーム デザイナーで必要です。
		Private components As System.ComponentModel.IContainer

		'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
		'Windows フォーム デザイナーを使用して変更できます。  
		'コード エディターを使って変更しないでください。
		<System.Diagnostics.DebuggerStepThrough()> _
		Private Sub InitializeComponent()
            Me.btnAlertClose = New System.Windows.Forms.Button()
            Me.lblAlert = New System.Windows.Forms.Label()
            Me.SuspendLayout()
            '
            'btnAlertClose
            '
            Me.btnAlertClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnAlertClose.BackColor = System.Drawing.Color.Transparent
            Me.btnAlertClose.Cursor = System.Windows.Forms.Cursors.Hand
            Me.btnAlertClose.FlatAppearance.BorderColor = System.Drawing.Color.White
            Me.btnAlertClose.FlatAppearance.BorderSize = 0
            Me.btnAlertClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnAlertClose.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnAlertClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
            Me.btnAlertClose.Location = New System.Drawing.Point(552, 6)
            Me.btnAlertClose.Margin = New System.Windows.Forms.Padding(0)
            Me.btnAlertClose.Name = "btnAlertClose"
            Me.btnAlertClose.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
            Me.btnAlertClose.Size = New System.Drawing.Size(29, 28)
            Me.btnAlertClose.TabIndex = 10
            Me.btnAlertClose.Text = "×"
            Me.btnAlertClose.UseVisualStyleBackColor = True
            '
            'lblAlert
            '
            Me.lblAlert.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblAlert.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer))
            Me.lblAlert.Font = New System.Drawing.Font("メイリオ", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.lblAlert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
            Me.lblAlert.Location = New System.Drawing.Point(1, 1)
            Me.lblAlert.Margin = New System.Windows.Forms.Padding(0)
            Me.lblAlert.Name = "lblAlert"
            Me.lblAlert.Padding = New System.Windows.Forms.Padding(12, 9, 12, 0)
            Me.lblAlert.Size = New System.Drawing.Size(585, 77)
            Me.lblAlert.TabIndex = 9
            Me.lblAlert.Text = "Message"
            Me.lblAlert.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'AlertMessage
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(186, Byte), Integer))
            Me.Controls.Add(Me.btnAlertClose)
            Me.Controls.Add(Me.lblAlert)
            Me.Font = New System.Drawing.Font("メイリオ", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.Margin = New System.Windows.Forms.Padding(0)
            Me.Name = "AlertMessage"
            Me.Size = New System.Drawing.Size(587, 79)
            Me.ResumeLayout(False)

        End Sub
		Friend WithEvents btnAlertClose As System.Windows.Forms.Button
		Friend WithEvents lblAlert As System.Windows.Forms.Label

	End Class

End Namespace
