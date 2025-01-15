using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002B9 RID: 697
	internal sealed class CssFontSizeAdjustProperty : CssProperty
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x0004CF7A File Offset: 0x0004B17A
		internal CssFontSizeAdjustProperty()
			: base(PropertyNames.FontSizeAdjust, PropertyFlags.Inherited | PropertyFlags.Animatable)
		{
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0004CF89 File Offset: 0x0004B189
		internal override IValueConverter Converter
		{
			get
			{
				return CssFontSizeAdjustProperty.StyleConverter;
			}
		}

		// Token: 0x04000C4E RID: 3150
		private static readonly IValueConverter StyleConverter = Converters.OptionalNumberConverter.OrDefault();
	}
}
