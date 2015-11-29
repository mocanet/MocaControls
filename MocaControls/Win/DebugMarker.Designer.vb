
Namespace Win

	<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 Partial Class DebugMarker
		Inherits System.Windows.Forms.UserControl

		'UserControl1 は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
		<System.Diagnostics.DebuggerNonUserCode()> _
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			Try
				If disposing AndAlso components IsNot Nothing Then
					components.Dispose()
				End If
			Finally
				MyBase.Dispose(disposing)
			End Try
		End Sub

		'Windows フォーム デザイナで必要です。
		Private components As System.ComponentModel.IContainer

		'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
		'Windows フォーム デザイナを使用して変更できます。  
		'コード エディタを使って変更しないでください。
		<System.Diagnostics.DebuggerStepThrough()> _
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DebugMarker))
			Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
			Me.SuspendLayout()
			'
			'DebugMarker
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
			Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
			Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
			Me.DoubleBuffered = True
			Me.MaximumSize = New System.Drawing.Size(16, 16)
			Me.MinimumSize = New System.Drawing.Size(16, 16)
			Me.Name = "DebugMarker"
			Me.Size = New System.Drawing.Size(16, 16)
			Me.ResumeLayout(False)

		End Sub
		Friend WithEvents toolTip As System.Windows.Forms.ToolTip

	End Class

End Namespace
