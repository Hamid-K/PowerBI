using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002CF RID: 719
	internal sealed class CssObjectPositionProperty : CssProperty
	{
		// Token: 0x06001573 RID: 5491 RVA: 0x0004D3F3 File Offset: 0x0004B5F3
		internal CssObjectPositionProperty()
			: base(PropertyNames.ObjectPosition, PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0004D401 File Offset: 0x0004B601
		internal override IValueConverter Converter
		{
			get
			{
				return CssObjectPositionProperty.StyleConverter;
			}
		}

		// Token: 0x04000C62 RID: 3170
		private static readonly IValueConverter StyleConverter = Converters.PointConverter.OrDefault(Point.Center);
	}
}
