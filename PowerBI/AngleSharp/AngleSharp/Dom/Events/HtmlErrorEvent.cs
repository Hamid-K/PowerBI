using System;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Parser.Html;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E6 RID: 486
	public class HtmlErrorEvent : Event
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x000473E5 File Offset: 0x000455E5
		public HtmlErrorEvent(HtmlParseError code, TextPosition position)
			: base(EventNames.ParseError, false, false)
		{
			this._code = code;
			this._position = position;
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00047402 File Offset: 0x00045602
		public TextPosition Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0004740A File Offset: 0x0004560A
		public int Code
		{
			get
			{
				return this._code.GetCode();
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00047417 File Offset: 0x00045617
		public string Message
		{
			get
			{
				return this._code.GetMessage<HtmlParseError>();
			}
		}

		// Token: 0x04000A4B RID: 2635
		private readonly HtmlParseError _code;

		// Token: 0x04000A4C RID: 2636
		private readonly TextPosition _position;
	}
}
