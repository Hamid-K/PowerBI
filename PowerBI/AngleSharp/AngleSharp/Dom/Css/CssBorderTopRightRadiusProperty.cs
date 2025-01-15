using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000289 RID: 649
	internal sealed class CssBorderTopRightRadiusProperty : CssProperty
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x0004C52E File Offset: 0x0004A72E
		internal CssBorderTopRightRadiusProperty()
			: base(PropertyNames.BorderTopRightRadius, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0004C53C File Offset: 0x0004A73C
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderTopRightRadiusProperty.StyleConverter;
			}
		}

		// Token: 0x04000C1E RID: 3102
		private static readonly IValueConverter StyleConverter = Converters.BorderRadiusConverter.OrDefault(Length.Zero);
	}
}
