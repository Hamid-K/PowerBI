using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200027C RID: 636
	internal sealed class CssBorderTopStyleProperty : CssProperty
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x0004C176 File Offset: 0x0004A376
		internal CssBorderTopStyleProperty()
			: base(PropertyNames.BorderTopStyle, PropertyFlags.None)
		{
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0004C184 File Offset: 0x0004A384
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderTopStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000C0D RID: 3085
		private static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);
	}
}
