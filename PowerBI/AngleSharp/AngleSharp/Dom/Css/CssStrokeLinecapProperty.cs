using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D2 RID: 722
	internal sealed class CssStrokeLinecapProperty : CssProperty
	{
		// Token: 0x0600157C RID: 5500 RVA: 0x0004D461 File Offset: 0x0004B661
		public CssStrokeLinecapProperty()
			: base(PropertyNames.StrokeLinecap, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0004D46F File Offset: 0x0004B66F
		internal override IValueConverter Converter
		{
			get
			{
				return CssStrokeLinecapProperty.StyleConverter;
			}
		}

		// Token: 0x04000C65 RID: 3173
		private static readonly IValueConverter StyleConverter = Converters.StrokeLinecapConverter.OrDefault(StrokeLinecap.Butt);
	}
}
