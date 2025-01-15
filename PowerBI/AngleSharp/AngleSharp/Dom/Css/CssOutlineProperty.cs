using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002CB RID: 715
	internal sealed class CssOutlineProperty : CssShorthandProperty
	{
		// Token: 0x06001567 RID: 5479 RVA: 0x0004D2E0 File Offset: 0x0004B4E0
		internal CssOutlineProperty()
			: base(PropertyNames.Outline, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0004D2EE File Offset: 0x0004B4EE
		internal override IValueConverter Converter
		{
			get
			{
				return CssOutlineProperty.StyleConverter;
			}
		}

		// Token: 0x04000C5E RID: 3166
		private static readonly IValueConverter StyleConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.LineWidthConverter.Option().For(new string[] { PropertyNames.OutlineWidth }),
			Converters.LineStyleConverter.Option().For(new string[] { PropertyNames.OutlineStyle }),
			Converters.InvertedColorConverter.Option().For(new string[] { PropertyNames.OutlineColor })
		}).OrDefault();
	}
}
