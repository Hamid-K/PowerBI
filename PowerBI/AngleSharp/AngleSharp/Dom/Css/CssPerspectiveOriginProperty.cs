using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E8 RID: 744
	internal sealed class CssPerspectiveOriginProperty : CssProperty
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x0004D80D File Offset: 0x0004BA0D
		internal CssPerspectiveOriginProperty()
			: base(PropertyNames.PerspectiveOrigin, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0004D81B File Offset: 0x0004BA1B
		internal override IValueConverter Converter
		{
			get
			{
				return CssPerspectiveOriginProperty.PerspectiveConverter;
			}
		}

		// Token: 0x04000C7B RID: 3195
		private static readonly IValueConverter PerspectiveConverter = Converters.LengthOrPercentConverter.Or(Keywords.Left, new Point(Length.Zero, Length.Half)).Or(Keywords.Center, new Point(Length.Half, Length.Half)).Or(Keywords.Right, new Point(Length.Full, Length.Half))
			.Or(Keywords.Top, new Point(Length.Half, Length.Zero))
			.Or(Keywords.Bottom, new Point(Length.Half, Length.Full))
			.Or(Converters.WithAny(new IValueConverter[]
			{
				Converters.LengthOrPercentConverter.Or(Keywords.Left, Length.Zero).Or(Keywords.Right, Length.Full).Or(Keywords.Center, Length.Half)
					.Option(Length.Half),
				Converters.LengthOrPercentConverter.Or(Keywords.Top, Length.Zero).Or(Keywords.Bottom, Length.Full).Or(Keywords.Center, Length.Half)
					.Option(Length.Half)
			}))
			.OrDefault(Point.Center);
	}
}
