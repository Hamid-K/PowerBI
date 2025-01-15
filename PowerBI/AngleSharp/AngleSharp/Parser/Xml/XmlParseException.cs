using System;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x02000059 RID: 89
	public class XmlParseException : Exception
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		public XmlParseException(int code, string message, TextPosition position)
			: base(message)
		{
			this.Code = code;
			this.Position = position;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000D7E7 File Offset: 0x0000B9E7
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		public TextPosition Position { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000D800 File Offset: 0x0000BA00
		public int Code { get; private set; }
	}
}
