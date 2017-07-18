#Region " Enum MessageIDs "

''' <summary>
''' メッセージリソースID
''' </summary>
''' <remarks></remarks>
Public Enum MessageIDs As Integer

    NONE = 0

#Region " 警告メッセージ "

    ''' <summary></summary>
    C001

#End Region
#Region " エラーメッセージ "

    ''' <summary>{0}を入力してください。</summary>
    E001
    ''' <summary>{0}の入力が間違っています。</summary>
    E002
    ''' <summary>{0}が長すぎます。半角{1}文字以下にしてください。現在：{2}文字相当</summary>
    E003
    ''' <summary>{0}が短すぎます。半角{1}文字以上にしてください。現在：{2}文字相当</summary>
    E004

#End Region

#Region " インフォメーション "

    ''' <summary></summary>
    I001

#End Region

End Enum

#End Region