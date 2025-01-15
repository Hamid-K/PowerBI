using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200028B RID: 651
	internal sealed class CssBoxShadowProperty : CssProperty
	{
		// Token: 0x060014A8 RID: 5288 RVA: 0x0004C580 File Offset: 0x0004A780
		internal CssBoxShadowProperty()
			: base(PropertyNames.BoxShadow, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0004C58E File Offset: 0x0004A78E
		internal override IValueConverter Converter
		{
			get
			{
				return CssBoxShadowProperty.StyleConverter;
			}
		}

		// Token: 0x04000C20 RID: 3104
		private static readonly IValueConverter StyleConverter = Converters.MultipleShadowConverter.OrDefault();
	}
}
