using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A9 RID: 681
	internal sealed class CssMaxHeightProperty : CssProperty
	{
		// Token: 0x06001502 RID: 5378 RVA: 0x0004CBA9 File Offset: 0x0004ADA9
		internal CssMaxHeightProperty()
			: base(PropertyNames.MaxHeight, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0004CBB7 File Offset: 0x0004ADB7
		internal override IValueConverter Converter
		{
			get
			{
				return CssMaxHeightProperty.StyleConverter;
			}
		}

		// Token: 0x04000C3E RID: 3134
		private static readonly IValueConverter StyleConverter = Converters.OptionalLengthOrPercentConverter.OrDefault();
	}
}
