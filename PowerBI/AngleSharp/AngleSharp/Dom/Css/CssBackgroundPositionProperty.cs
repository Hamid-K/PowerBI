using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000265 RID: 613
	internal sealed class CssBackgroundPositionProperty : CssProperty
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x0004B8AA File Offset: 0x00049AAA
		internal CssBackgroundPositionProperty()
			: base(PropertyNames.BackgroundPosition, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0004B8B8 File Offset: 0x00049AB8
		internal override IValueConverter Converter
		{
			get
			{
				return CssBackgroundPositionProperty.ListConverter;
			}
		}

		// Token: 0x04000BF4 RID: 3060
		private static readonly IValueConverter ListConverter = Converters.PointConverter.FromList().OrDefault(Point.Center);
	}
}
