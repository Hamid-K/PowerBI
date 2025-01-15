using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000068 RID: 104
	internal abstract class XmlToken
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000EE89 File Offset: 0x0000D089
		public XmlToken(XmlTokenType type, TextPosition position)
		{
			this._type = type;
			this._position = position;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000EE9F File Offset: 0x0000D09F
		public virtual bool IsIgnorable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000EEA2 File Offset: 0x0000D0A2
		public XmlTokenType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000EEAA File Offset: 0x0000D0AA
		public TextPosition Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x0400022E RID: 558
		private readonly XmlTokenType _type;

		// Token: 0x0400022F RID: 559
		private readonly TextPosition _position;
	}
}
