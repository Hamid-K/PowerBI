using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200026C RID: 620
	internal sealed class CssBorderBottomWidthProperty : CssProperty
	{
		// Token: 0x0600144B RID: 5195 RVA: 0x0004BC7D File Offset: 0x00049E7D
		internal CssBorderBottomWidthProperty()
			: base(PropertyNames.BorderBottomWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0004BC8C File Offset: 0x00049E8C
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderBottomWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000BFD RID: 3069
		private static readonly IValueConverter StyleConverter = Converters.LineWidthConverter.OrDefault(Length.Medium);
	}
}
