using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000062 RID: 98
	internal sealed class XmlCommentToken : XmlToken
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000EBEF File Offset: 0x0000CDEF
		public XmlCommentToken(TextPosition position)
			: this(position, string.Empty)
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000EBFD File Offset: 0x0000CDFD
		public XmlCommentToken(TextPosition position, string data)
			: base(XmlTokenType.Comment, position)
		{
			this._data = data;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000EC0E File Offset: 0x0000CE0E
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x04000221 RID: 545
		private readonly string _data;
	}
}
