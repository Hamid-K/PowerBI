using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F4 RID: 756
	internal sealed class CssOpacityProperty : CssProperty
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x0004DD4B File Offset: 0x0004BF4B
		internal CssOpacityProperty()
			: base(PropertyNames.Opacity, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x0004DD59 File Offset: 0x0004BF59
		internal override IValueConverter Converter
		{
			get
			{
				return CssOpacityProperty.StyleConverter;
			}
		}

		// Token: 0x04000C87 RID: 3207
		private static readonly IValueConverter StyleConverter = Converters.NumberConverter.OrDefault(1f);
	}
}
