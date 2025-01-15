using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002A6 RID: 678
	internal sealed class CssBottomProperty : CssProperty
	{
		// Token: 0x060014F9 RID: 5369 RVA: 0x0004CB25 File Offset: 0x0004AD25
		internal CssBottomProperty()
			: base(PropertyNames.Bottom, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0004CB34 File Offset: 0x0004AD34
		internal override IValueConverter Converter
		{
			get
			{
				return CssBottomProperty.StyleConverter;
			}
		}

		// Token: 0x04000C3B RID: 3131
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Keywords.Auto);
	}
}
