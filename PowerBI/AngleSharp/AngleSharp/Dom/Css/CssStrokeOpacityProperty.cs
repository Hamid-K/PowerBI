using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D5 RID: 725
	internal sealed class CssStrokeOpacityProperty : CssProperty
	{
		// Token: 0x06001585 RID: 5509 RVA: 0x0004D4CA File Offset: 0x0004B6CA
		internal CssStrokeOpacityProperty()
			: base(PropertyNames.StrokeOpacity, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0004D4D8 File Offset: 0x0004B6D8
		internal override IValueConverter Converter
		{
			get
			{
				return CssStrokeOpacityProperty.StyleConverter;
			}
		}

		// Token: 0x04000C68 RID: 3176
		private static readonly IValueConverter StyleConverter = Converters.NumberConverter.OrDefault(1f);
	}
}
