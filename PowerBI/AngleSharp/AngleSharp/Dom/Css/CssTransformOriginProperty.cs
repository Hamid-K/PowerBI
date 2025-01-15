using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002EA RID: 746
	internal sealed class CssTransformOriginProperty : CssProperty
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x0004D980 File Offset: 0x0004BB80
		internal CssTransformOriginProperty()
			: base(PropertyNames.TransformOrigin, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0004D98E File Offset: 0x0004BB8E
		internal override IValueConverter Converter
		{
			get
			{
				return CssTransformOriginProperty.StyleConverter;
			}
		}

		// Token: 0x04000C7D RID: 3197
		private static IValueConverter StyleConverter = Converters.WithOrder(new IValueConverter[]
		{
			Converters.LengthOrPercentConverter.Or(Keywords.Center, Point.Center).Or(Converters.WithAny(new IValueConverter[]
			{
				Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half)
					.Option(Length.Half),
				Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half)
					.Option(Length.Half)
			})).Or(Converters.WithAny(new IValueConverter[]
			{
				Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half)
					.Option(Length.Half),
				Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half)
					.Option(Length.Half)
			}))
				.Required(),
			Converters.LengthConverter.Option(Length.Zero)
		}).OrDefault(Point.Center);
	}
}
