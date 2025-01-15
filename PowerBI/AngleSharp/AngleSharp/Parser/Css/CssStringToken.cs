using System;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000087 RID: 135
	internal sealed class CssStringToken : CssToken
	{
		// Token: 0x0600043D RID: 1085 RVA: 0x0001BDC9 File Offset: 0x00019FC9
		public CssStringToken(string data, bool bad, char quote, TextPosition position)
			: base(CssTokenType.String, data, position)
		{
			this._bad = bad;
			this._quote = quote;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001BDE3 File Offset: 0x00019FE3
		public bool IsBad
		{
			get
			{
				return this._bad;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001BDEB File Offset: 0x00019FEB
		public char Quote
		{
			get
			{
				return this._quote;
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001BDF3 File Offset: 0x00019FF3
		public override string ToValue()
		{
			return base.Data.CssString();
		}

		// Token: 0x04000335 RID: 821
		private readonly bool _bad;

		// Token: 0x04000336 RID: 822
		private readonly char _quote;
	}
}
