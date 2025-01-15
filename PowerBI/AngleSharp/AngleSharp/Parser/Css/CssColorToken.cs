using System;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000081 RID: 129
	internal sealed class CssColorToken : CssToken
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		public CssColorToken(string data, TextPosition position)
			: base(CssTokenType.Color, data, position)
		{
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001BAB3 File Offset: 0x00019CB3
		public bool IsBad
		{
			get
			{
				return base.Data.Length != 3 && base.Data.Length != 6;
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001BAD6 File Offset: 0x00019CD6
		public override string ToValue()
		{
			return "#" + base.Data;
		}
	}
}
