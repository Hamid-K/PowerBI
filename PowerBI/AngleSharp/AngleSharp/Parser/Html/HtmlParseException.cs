using System;

namespace AngleSharp.Parser.Html
{
	// Token: 0x0200006C RID: 108
	public class HtmlParseException : Exception
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00013C1B File Offset: 0x00011E1B
		public HtmlParseException(int code, string message, TextPosition position)
			: base(message)
		{
			this.Code = code;
			this.Position = position;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00013C32 File Offset: 0x00011E32
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00013C3A File Offset: 0x00011E3A
		public TextPosition Position { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00013C43 File Offset: 0x00011E43
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00013C4B File Offset: 0x00011E4B
		public int Code { get; private set; }
	}
}
