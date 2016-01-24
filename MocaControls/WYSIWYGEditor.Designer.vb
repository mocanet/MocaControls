
Namespace Win

	<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
	Partial Class WYSIWYGEditor
		Inherits System.Windows.Forms.UserControl

		'UserControl はコンポーネント一覧をクリーンアップするために dispose をオーバーライドします。
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

		'Windows フォーム デザイナーで必要です。
		Private components As System.ComponentModel.IContainer

		'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
		'Windows フォーム デザイナーを使用して変更できます。  
		'コード エディターを使って変更しないでください。
		<System.Diagnostics.DebuggerStepThrough()> _
		Private Sub InitializeComponent()
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WYSIWYGEditor))
			Me.StyleToolStrip = New System.Windows.Forms.ToolStrip()
			Me.tsbtnBold = New System.Windows.Forms.ToolStripButton()
			Me.tsbtnItalic = New System.Windows.Forms.ToolStripButton()
			Me.tsbtnUnderline = New System.Windows.Forms.ToolStripButton()
			Me.tsbtnStrikeout = New System.Windows.Forms.ToolStripButton()
			Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
			Me.tsbtnSizeUp = New System.Windows.Forms.ToolStripButton()
			Me.tsbtnSizeDown = New System.Windows.Forms.ToolStripButton()
			Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
			Me.tsbtnColor = New System.Windows.Forms.ToolStripButton()
			Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
			Me.tsbtnBrowserView = New System.Windows.Forms.ToolStripButton()
			Me.rtxtSource = New System.Windows.Forms.RichTextBox()
			Me.ieView = New System.Windows.Forms.WebBrowser()
			Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
			Me.StyleToolStrip.SuspendLayout()
			Me.SuspendLayout()
			'
			'StyleToolStrip
			'
			Me.StyleToolStrip.BackColor = System.Drawing.Color.Transparent
			Me.StyleToolStrip.GripMargin = New System.Windows.Forms.Padding(0)
			Me.StyleToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
			Me.StyleToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbtnBold, Me.tsbtnItalic, Me.tsbtnUnderline, Me.tsbtnStrikeout, Me.ToolStripSeparator1, Me.tsbtnSizeUp, Me.tsbtnSizeDown, Me.ToolStripSeparator2, Me.tsbtnColor, Me.ToolStripSeparator3, Me.tsbtnBrowserView})
			Me.StyleToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
			Me.StyleToolStrip.Location = New System.Drawing.Point(0, 0)
			Me.StyleToolStrip.Name = "StyleToolStrip"
			Me.StyleToolStrip.Padding = New System.Windows.Forms.Padding(0)
			Me.StyleToolStrip.Size = New System.Drawing.Size(320, 23)
			Me.StyleToolStrip.Stretch = True
			Me.StyleToolStrip.TabIndex = 11
			Me.StyleToolStrip.Text = "ToolStrip1"
			'
			'tsbtnBold
			'
			Me.tsbtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.tsbtnBold.Image = CType(resources.GetObject("tsbtnBold.Image"), System.Drawing.Image)
			Me.tsbtnBold.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbtnBold.Name = "tsbtnBold"
			Me.tsbtnBold.Size = New System.Drawing.Size(23, 20)
			Me.tsbtnBold.Text = "Bold"
			'
			'tsbtnItalic
			'
			Me.tsbtnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.tsbtnItalic.Image = CType(resources.GetObject("tsbtnItalic.Image"), System.Drawing.Image)
			Me.tsbtnItalic.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbtnItalic.Name = "tsbtnItalic"
			Me.tsbtnItalic.Size = New System.Drawing.Size(23, 20)
			Me.tsbtnItalic.Text = "Italic"
			'
			'tsbtnUnderline
			'
			Me.tsbtnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.tsbtnUnderline.Image = CType(resources.GetObject("tsbtnUnderline.Image"), System.Drawing.Image)
			Me.tsbtnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbtnUnderline.Name = "tsbtnUnderline"
			Me.tsbtnUnderline.Size = New System.Drawing.Size(23, 20)
			Me.tsbtnUnderline.Text = "Underline"
			'
			'tsbtnStrikeout
			'
			Me.tsbtnStrikeout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.tsbtnStrikeout.Image = CType(resources.GetObject("tsbtnStrikeout.Image"), System.Drawing.Image)
			Me.tsbtnStrikeout.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbtnStrikeout.Name = "tsbtnStrikeout"
			Me.tsbtnStrikeout.Size = New System.Drawing.Size(23, 20)
			Me.tsbtnStrikeout.Text = "Strikeout"
			'
			'ToolStripSeparator1
			'
			Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
			Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 23)
			'
			'tsbtnSizeUp
			'
			Me.tsbtnSizeUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.tsbtnSizeUp.Image = CType(resources.GetObject("tsbtnSizeUp.Image"), System.Drawing.Image)
			Me.tsbtnSizeUp.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbtnSizeUp.Name = "tsbtnSizeUp"
			Me.tsbtnSizeUp.Size = New System.Drawing.Size(23, 20)
			Me.tsbtnSizeUp.Text = "Size Up"
			'
			'tsbtnSizeDown
			'
			Me.tsbtnSizeDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.tsbtnSizeDown.Image = CType(resources.GetObject("tsbtnSizeDown.Image"), System.Drawing.Image)
			Me.tsbtnSizeDown.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbtnSizeDown.Name = "tsbtnSizeDown"
			Me.tsbtnSizeDown.Size = New System.Drawing.Size(23, 20)
			Me.tsbtnSizeDown.Text = "Size Down"
			'
			'ToolStripSeparator2
			'
			Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
			Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 23)
			'
			'tsbtnColor
			'
			Me.tsbtnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.tsbtnColor.Image = CType(resources.GetObject("tsbtnColor.Image"), System.Drawing.Image)
			Me.tsbtnColor.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbtnColor.Name = "tsbtnColor"
			Me.tsbtnColor.Size = New System.Drawing.Size(23, 20)
			Me.tsbtnColor.Text = "Color"
			'
			'ToolStripSeparator3
			'
			Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
			Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 23)
			'
			'tsbtnBrowserView
			'
			Me.tsbtnBrowserView.CheckOnClick = True
			Me.tsbtnBrowserView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.tsbtnBrowserView.Image = CType(resources.GetObject("tsbtnBrowserView.Image"), System.Drawing.Image)
			Me.tsbtnBrowserView.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbtnBrowserView.Name = "tsbtnBrowserView"
			Me.tsbtnBrowserView.Size = New System.Drawing.Size(23, 20)
			Me.tsbtnBrowserView.Text = "Browser View"
			'
			'rtxtSource
			'
			Me.rtxtSource.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			 Or System.Windows.Forms.AnchorStyles.Left) _
			 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.rtxtSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.rtxtSource.Location = New System.Drawing.Point(0, 23)
			Me.rtxtSource.Margin = New System.Windows.Forms.Padding(0)
			Me.rtxtSource.Name = "rtxtSource"
			Me.rtxtSource.Size = New System.Drawing.Size(320, 260)
			Me.rtxtSource.TabIndex = 10
			Me.rtxtSource.Text = ""
			'
			'ieView
			'
			Me.ieView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			 Or System.Windows.Forms.AnchorStyles.Left) _
			 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.ieView.Location = New System.Drawing.Point(0, 23)
			Me.ieView.MinimumSize = New System.Drawing.Size(20, 20)
			Me.ieView.Name = "ieView"
			Me.ieView.Size = New System.Drawing.Size(320, 260)
			Me.ieView.TabIndex = 12
			'
			'WYSIWYGEditor
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
			Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
			Me.BackColor = System.Drawing.Color.Transparent
			Me.Controls.Add(Me.StyleToolStrip)
			Me.Controls.Add(Me.rtxtSource)
			Me.Controls.Add(Me.ieView)
			Me.Margin = New System.Windows.Forms.Padding(1)
			Me.Name = "WYSIWYGEditor"
			Me.Size = New System.Drawing.Size(320, 283)
			Me.StyleToolStrip.ResumeLayout(False)
			Me.StyleToolStrip.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub
		Friend WithEvents StyleToolStrip As System.Windows.Forms.ToolStrip
		Friend WithEvents tsbtnBold As System.Windows.Forms.ToolStripButton
		Friend WithEvents tsbtnItalic As System.Windows.Forms.ToolStripButton
		Friend WithEvents tsbtnUnderline As System.Windows.Forms.ToolStripButton
		Friend WithEvents tsbtnStrikeout As System.Windows.Forms.ToolStripButton
		Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Friend WithEvents tsbtnSizeUp As System.Windows.Forms.ToolStripButton
		Friend WithEvents tsbtnSizeDown As System.Windows.Forms.ToolStripButton
		Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
		Friend WithEvents tsbtnColor As System.Windows.Forms.ToolStripButton
		Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
		Friend WithEvents tsbtnBrowserView As System.Windows.Forms.ToolStripButton
		Friend WithEvents rtxtSource As System.Windows.Forms.RichTextBox
		Friend WithEvents ieView As System.Windows.Forms.WebBrowser
		Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog

	End Class

End Namespace
