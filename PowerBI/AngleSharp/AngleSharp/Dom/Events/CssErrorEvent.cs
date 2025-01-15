using System;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001DF RID: 479
	public class CssErrorEvent : Event
	{
		// Token: 0x06000FDC RID: 4060 RVA: 0x00046FF8 File Offset: 0x000451F8
		public CssErrorEvent(CssParseError code, TextPosition position)
			: base(EventNames.ParseError, false, false)
		{
			this._code = code;
			this._position = position;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00047015 File Offset: 0x00045215
		public TextPosition Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0004701D File Offset: 0x0004521D
		public int Code
		{
			get
			{
				return this._code.GetCode();
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0004702A File Offset: 0x0004522A
		public string Message
		{
			get
			{
				return this._code.GetMessage<CssParseError>();
			}
		}

		// Token: 0x04000A37 RID: 2615
		private CssParseError _code;

		// Token: 0x04000A38 RID: 2616
		private TextPosition _position;
	}
}
