using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002DF RID: 735
	internal sealed class CssTextDecorationProperty : CssShorthandProperty
	{
		// Token: 0x060015A3 RID: 5539 RVA: 0x0004D639 File Offset: 0x0004B839
		internal CssTextDecorationProperty()
			: base(PropertyNames.TextDecoration, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x0004D647 File Offset: 0x0004B847
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextDecorationProperty.StyleConverter;
			}
		}

		// Token: 0x04000C72 RID: 3186
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.ColorConverter.Option().For(new string[] { PropertyNames.TextDecorationColor }),
			Converters.TextDecorationStyleConverter.Option().For(new string[] { PropertyNames.TextDecorationStyle }),
			Converters.TextDecorationLinesConverter.Option().For(new string[] { PropertyNames.TextDecorationLine })
		}).OrDefault();
	}
}
