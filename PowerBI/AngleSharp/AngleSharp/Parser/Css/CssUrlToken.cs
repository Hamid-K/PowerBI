using System;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x0200008A RID: 138
	internal sealed class CssUrlToken : CssToken
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0001BE91 File Offset: 0x0001A091
		public CssUrlToken(string functionName, string data, bool bad, TextPosition position)
			: base(CssTokenType.Url, data, position)
		{
			this._bad = bad;
			this._functionName = functionName;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001BEAB File Offset: 0x0001A0AB
		public bool IsBad
		{
			get
			{
				return this._bad;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0001BEB3 File Offset: 0x0001A0B3
		public string FunctionName
		{
			get
			{
				return this._functionName;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		public override string ToValue()
		{
			string text = base.Data.CssString();
			return this._functionName.CssFunction(text);
		}

		// Token: 0x0400033D RID: 829
		private readonly bool _bad;

		// Token: 0x0400033E RID: 830
		private readonly string _functionName;
	}
}
