
Namespace Win

	<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
	Partial Class MocaDi
		Inherits System.Windows.Forms.UserControl

		'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
		<System.Diagnostics.DebuggerNonUserCode()> _
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			Try
				If disposing AndAlso components IsNot Nothing Then
					components.Dispose()

					Moca.Di.MocaContainerFactory.Destroy()
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MocaDi))
			Me.SuspendLayout()
			'
			'MocaDi
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
			Me.AutoSize = True
			Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
			Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
			Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
			Me.Margin = New System.Windows.Forms.Padding(0)
			Me.MaximumSize = New System.Drawing.Size(16, 16)
			Me.MinimumSize = New System.Drawing.Size(16, 16)
			Me.Name = "MocaDi"
			Me.Size = New System.Drawing.Size(16, 16)
			Me.ResumeLayout(False)

		End Sub

	End Class

End Namespace
