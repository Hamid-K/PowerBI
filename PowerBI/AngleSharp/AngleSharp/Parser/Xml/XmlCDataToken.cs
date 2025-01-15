using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000060 RID: 96
	internal sealed class XmlCDataToken : XmlToken
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		public XmlCDataToken(TextPosition position)
			: this(position, string.Empty)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000EB9A File Offset: 0x0000CD9A
		public XmlCDataToken(TextPosition position, string data)
			: base(XmlTokenType.CData, position)
		{
			this._data = data;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000EBAB File Offset: 0x0000CDAB
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x0400021F RID: 543
		private readonly string _data;
	}
}
