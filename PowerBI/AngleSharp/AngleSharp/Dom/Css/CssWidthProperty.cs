using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002AF RID: 687
	internal sealed class CssWidthProperty : CssProperty
	{
		// Token: 0x06001514 RID: 5396 RVA: 0x0004CCA3 File Offset: 0x0004AEA3
		internal CssWidthProperty()
			: base(PropertyNames.Width, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0004CCB2 File Offset: 0x0004AEB2
		internal override IValueConverter Converter
		{
			get
			{
				return CssWidthProperty.StyleConverter;
			}
		}

		// Token: 0x04000C44 RID: 3140
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Keywords.Auto);
	}
}
