using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000280 RID: 640
	internal sealed class CssBorderImageProperty : CssShorthandProperty
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x0004C25A File Offset: 0x0004A45A
		internal CssBorderImageProperty()
			: base(PropertyNames.BorderImage, PropertyFlags.None)
		{
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0004C268 File Offset: 0x0004A468
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderImageProperty.ImageConverter;
			}
		}

		// Token: 0x04000C12 RID: 3090
		private static readonly IValueConverter ImageConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.OptionalImageSourceConverter.Option().For(new string[] { PropertyNames.BorderImageSource }),
			Converters.WithOrder(new IValueConverter[]
			{
				CssBorderImageSliceProperty.TheConverter.Option().For(new string[] { PropertyNames.BorderImageSlice }),
				CssBorderImageWidthProperty.TheConverter.StartsWithDelimiter().Option().For(new string[] { PropertyNames.BorderImageWidth }),
				CssBorderImageOutsetProperty.TheConverter.StartsWithDelimiter().Option().For(new string[] { PropertyNames.BorderImageOutset })
			}),
			CssBorderImageRepeatProperty.TheConverter.Option().For(new string[] { PropertyNames.BorderImageRepeat })
		}).OrDefault();
	}
}
