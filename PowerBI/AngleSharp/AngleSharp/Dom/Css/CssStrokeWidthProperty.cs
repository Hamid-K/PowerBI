using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002D7 RID: 727
	internal sealed class CssStrokeWidthProperty : CssProperty
	{
		// Token: 0x0600158B RID: 5515 RVA: 0x0004D516 File Offset: 0x0004B716
		internal CssStrokeWidthProperty()
			: base(PropertyNames.StrokeWidth, PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0004D524 File Offset: 0x0004B724
		internal override IValueConverter Converter
		{
			get
			{
				return CssStrokeWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C6A RID: 3178
		private static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter;
	}
}
