using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000065 RID: 101
	internal sealed class XmlEndOfFileToken : XmlToken
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000ED1F File Offset: 0x0000CF1F
		public XmlEndOfFileToken(TextPosition position)
			: base(XmlTokenType.EndOfFile, position)
		{
		}
	}
}
