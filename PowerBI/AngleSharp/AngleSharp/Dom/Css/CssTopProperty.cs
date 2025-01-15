using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002AE RID: 686
	internal sealed class CssTopProperty : CssProperty
	{
		// Token: 0x06001511 RID: 5393 RVA: 0x0004CC77 File Offset: 0x0004AE77
		internal CssTopProperty()
			: base(PropertyNames.Top, PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0004CC86 File Offset: 0x0004AE86
		internal override IValueConverter Converter
		{
			get
			{
				return CssTopProperty.StyleConverter;
			}
		}

		// Token: 0x04000C43 RID: 3139
		private static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.OrDefault(Keywords.Auto);
	}
}
