using System;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000082 RID: 130
	internal sealed class CssCommentToken : CssToken
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		public CssCommentToken(string data, bool bad, TextPosition position)
			: base(CssTokenType.Comment, data, position)
		{
			this._bad = bad;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001BAFA File Offset: 0x00019CFA
		public bool IsBad
		{
			get
			{
				return this._bad;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001BB04 File Offset: 0x00019D04
		public override string ToValue()
		{
			string text = (this._bad ? string.Empty : "*/");
			return "/*" + base.Data + text;
		}

		// Token: 0x0400032F RID: 815
		private readonly bool _bad;
	}
}
