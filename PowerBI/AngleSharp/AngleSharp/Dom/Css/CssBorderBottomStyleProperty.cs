using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200026B RID: 619
	internal sealed class CssBorderBottomStyleProperty : CssProperty
	{
		// Token: 0x06001448 RID: 5192 RVA: 0x0004BC56 File Offset: 0x00049E56
		internal CssBorderBottomStyleProperty()
			: base(PropertyNames.BorderBottomStyle, PropertyFlags.None)
		{
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0004BC64 File Offset: 0x00049E64
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderBottomStyleProperty.StyleConverter;
			}
		}

		// Token: 0x04000BFC RID: 3068
		private static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);
	}
}
