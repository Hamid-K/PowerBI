using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B8 RID: 696
	internal sealed class CssFontProperty : CssShorthandProperty
	{
		// Token: 0x0600152F RID: 5423 RVA: 0x0004CE0F File Offset: 0x0004B00F
		internal CssFontProperty()
			: base(PropertyNames.Font, PropertyFlags.Inherited | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0004CE1E File Offset: 0x0004B01E
		internal override IValueConverter Converter
		{
			get
			{
				return CssFontProperty.StyleConverter;
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0004CE25 File Offset: 0x0004B025
		private static void SetSystemFont(SystemFont font)
		{
			switch (font)
			{
			default:
				return;
			}
		}

		// Token: 0x04000C4D RID: 3149
		private static readonly IValueConverter StyleConverter = Converters.WithOrder(new IValueConverter[]
		{
			Converters.WithAny(new IValueConverter[]
			{
				Converters.FontStyleConverter.Option().For(new string[] { PropertyNames.FontStyle }),
				Converters.FontVariantConverter.Option().For(new string[] { PropertyNames.FontVariant }),
				Converters.FontWeightConverter.Or(Converters.WeightIntegerConverter).Option().For(new string[] { PropertyNames.FontWeight }),
				Converters.FontStretchConverter.Option().For(new string[] { PropertyNames.FontStretch })
			}),
			Converters.WithOrder(new IValueConverter[]
			{
				Converters.FontSizeConverter.Required().For(new string[] { PropertyNames.FontSize }),
				Converters.LineHeightConverter.StartsWithDelimiter().Option().For(new string[] { PropertyNames.LineHeight }),
				Converters.FontFamiliesConverter.Required().For(new string[] { PropertyNames.FontFamily })
			})
		}).Or(Converters.SystemFontConverter);
	}
}
