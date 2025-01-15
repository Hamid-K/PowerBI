using System;
using System.Globalization;

namespace AngleSharp.Parser.Css
{
	// Token: 0x02000089 RID: 137
	internal sealed class CssUnitToken : CssToken
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0001BE63 File Offset: 0x0001A063
		public CssUnitToken(CssTokenType type, string value, string dimension, TextPosition position)
			: base(type, value, position)
		{
			this._unit = dimension;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001BC85 File Offset: 0x00019E85
		public float Value
		{
			get
			{
				return float.Parse(base.Data, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001BE76 File Offset: 0x0001A076
		public string Unit
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001BE7E File Offset: 0x0001A07E
		public override string ToValue()
		{
			return base.Data + this._unit;
		}

		// Token: 0x0400033C RID: 828
		private readonly string _unit;
	}
}
