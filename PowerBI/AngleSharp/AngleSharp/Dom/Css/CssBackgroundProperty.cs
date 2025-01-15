using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000266 RID: 614
	internal sealed class CssBackgroundProperty : CssShorthandProperty
	{
		// Token: 0x06001439 RID: 5177 RVA: 0x0004B8DA File Offset: 0x00049ADA
		internal CssBackgroundProperty()
			: base(PropertyNames.Background, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0004B8E8 File Offset: 0x00049AE8
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundProperty.StyleConverter;
			}
		}

		// Token: 0x04000BF5 RID: 3061
		private static readonly IValueConverter NormalLayerConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.OptionalImageSourceConverter.Option().For(new string[] { PropertyNames.BackgroundImage }),
			Converters.WithOrder(new IValueConverter[]
			{
				Converters.PointConverter.Option().For(new string[] { PropertyNames.BackgroundPosition }),
				Converters.BackgroundSizeConverter.StartsWithDelimiter().Option().For(new string[] { PropertyNames.BackgroundSize })
			}),
			Converters.BackgroundRepeatsConverter.Option().For(new string[] { PropertyNames.BackgroundRepeat }),
			Converters.BackgroundAttachmentConverter.Option().For(new string[] { PropertyNames.BackgroundAttachment }),
			Converters.BoxModelConverter.Option().For(new string[] { PropertyNames.BackgroundOrigin }),
			Converters.BoxModelConverter.Option().For(new string[] { PropertyNames.BackgroundClip })
		});

		// Token: 0x04000BF6 RID: 3062
		private static readonly IValueConverter FinalLayerConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.OptionalImageSourceConverter.Option().For(new string[] { PropertyNames.BackgroundImage }),
			Converters.WithOrder(new IValueConverter[]
			{
				Converters.PointConverter.Option().For(new string[] { PropertyNames.BackgroundPosition }),
				Converters.BackgroundSizeConverter.StartsWithDelimiter().Option().For(new string[] { PropertyNames.BackgroundSize })
			}),
			Converters.BackgroundRepeatsConverter.Option().For(new string[] { PropertyNames.BackgroundRepeat }),
			Converters.BackgroundAttachmentConverter.Option().For(new string[] { PropertyNames.BackgroundAttachment }),
			Converters.BoxModelConverter.Option().For(new string[] { PropertyNames.BackgroundOrigin }),
			Converters.BoxModelConverter.Option().For(new string[] { PropertyNames.BackgroundClip }),
			Converters.CurrentColorConverter.Option().For(new string[] { PropertyNames.BackgroundColor })
		});

		// Token: 0x04000BF7 RID: 3063
		private static readonly IValueConverter StyleConverter = CssBackgroundProperty.NormalLayerConverter.RequiresEnd(CssBackgroundProperty.FinalLayerConverter).OrDefault();
	}
}
