using System;
using System.Xml;
using System.Xml.XPath;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000544 RID: 1348
	internal sealed class LineInfo : IXmlLineInfo
	{
		// Token: 0x060041FA RID: 16890 RVA: 0x000E01BB File Offset: 0x000DE3BB
		internal LineInfo(XPathNavigator nav)
			: this((IXmlLineInfo)nav)
		{
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000E01C9 File Offset: 0x000DE3C9
		internal LineInfo(IXmlLineInfo lineInfo)
		{
			this.m_hasLineInfo = lineInfo.HasLineInfo();
			this.m_lineNumber = lineInfo.LineNumber;
			this.m_linePosition = lineInfo.LinePosition;
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000E01F5 File Offset: 0x000DE3F5
		private LineInfo()
		{
			this.m_hasLineInfo = false;
			this.m_lineNumber = 0;
			this.m_linePosition = 0;
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x000E0212 File Offset: 0x000DE412
		public int LineNumber
		{
			get
			{
				return this.m_lineNumber;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x000E021A File Offset: 0x000DE41A
		public int LinePosition
		{
			get
			{
				return this.m_linePosition;
			}
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000E0222 File Offset: 0x000DE422
		public bool HasLineInfo()
		{
			return this.m_hasLineInfo;
		}

		// Token: 0x040016EC RID: 5868
		private readonly bool m_hasLineInfo;

		// Token: 0x040016ED RID: 5869
		private readonly int m_lineNumber;

		// Token: 0x040016EE RID: 5870
		private readonly int m_linePosition;

		// Token: 0x040016EF RID: 5871
		internal static readonly LineInfo Empty = new LineInfo();
	}
}
