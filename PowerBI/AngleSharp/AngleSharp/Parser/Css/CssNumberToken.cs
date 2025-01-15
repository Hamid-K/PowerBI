using System;
using System.Globalization;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000085 RID: 133
	internal sealed class CssNumberToken : CssToken
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0001BC53 File Offset: 0x00019E53
		public CssNumberToken(string number, TextPosition position)
			: base(CssTokenType.Number, number, position)
		{
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001BC5E File Offset: 0x00019E5E
		public bool IsInteger
		{
			get
			{
				return base.Data.IndexOfAny(CssNumberToken.floatIndicators) == -1;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001BC73 File Offset: 0x00019E73
		public int IntegerValue
		{
			get
			{
				return int.Parse(base.Data, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001BC85 File Offset: 0x00019E85
		public float Value
		{
			get
			{
				return float.Parse(base.Data, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x04000331 RID: 817
		private static readonly char[] floatIndicators = new char[] { '.', 'e', 'E' };
	}
}
