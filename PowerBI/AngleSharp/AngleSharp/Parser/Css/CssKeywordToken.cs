using System;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000084 RID: 132
	internal sealed class CssKeywordToken : CssToken
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x0001BBDB File Offset: 0x00019DDB
		public CssKeywordToken(CssTokenType type, string data, TextPosition position)
			: base(type, data, position)
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001BBE8 File Offset: 0x00019DE8
		public override string ToValue()
		{
			switch (base.Type)
			{
			case CssTokenType.Hash:
				return "#" + base.Data;
			case CssTokenType.AtKeyword:
				return "@" + base.Data;
			case CssTokenType.Function:
				return base.Data + "(";
			}
			return base.Data;
		}
	}
}
