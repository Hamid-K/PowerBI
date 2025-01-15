using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000066 RID: 102
	internal sealed class XmlPIToken : XmlToken
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000ED2A File Offset: 0x0000CF2A
		public XmlPIToken(TextPosition position)
			: base(XmlTokenType.ProcessingInstruction, position)
		{
			this._target = string.Empty;
			this._content = string.Empty;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000ED4A File Offset: 0x0000CF4A
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000ED52 File Offset: 0x0000CF52
		public string Target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000ED5B File Offset: 0x0000CF5B
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000ED63 File Offset: 0x0000CF63
		public string Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		// Token: 0x04000229 RID: 553
		private string _target;

		// Token: 0x0400022A RID: 554
		private string _content;
	}
}
