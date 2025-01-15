using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E3 RID: 739
	internal sealed class CssTextShadowProperty : CssProperty
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x0004D746 File Offset: 0x0004B946
		internal CssTextShadowProperty()
			: base(PropertyNames.TextShadow, PropertyFlags.Inherited | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0004D755 File Offset: 0x0004B955
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextShadowProperty.StyleConverter;
			}
		}

		// Token: 0x04000C76 RID: 3190
		private static readonly IValueConverter StyleConverter = Converters.MultipleShadowConverter.OrDefault();
	}
}
