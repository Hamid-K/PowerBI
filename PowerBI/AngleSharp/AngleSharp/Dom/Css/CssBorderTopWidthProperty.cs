using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200027D RID: 637
	internal sealed class CssBorderTopWidthProperty : CssProperty
	{
		// Token: 0x0600147E RID: 5246 RVA: 0x0004C19D File Offset: 0x0004A39D
		internal CssBorderTopWidthProperty()
			: base(PropertyNames.BorderTopWidth, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0004C1AC File Offset: 0x0004A3AC
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderTopWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C0E RID: 3086
		private static readonly IValueConverter StyleConverter = Converters.LineWidthConverter.OrDefault(Length.Medium);
	}
}
