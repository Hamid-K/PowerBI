using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C3 RID: 707
	internal sealed class CssWordSpacingProperty : CssProperty
	{
		// Token: 0x0600154F RID: 5455 RVA: 0x0004D0EB File Offset: 0x0004B2EB
		internal CssWordSpacingProperty()
			: base(PropertyNames.WordSpacing, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
		{
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0004D0FA File Offset: 0x0004B2FA
		internal override IValueConverter Converter
		{
			get
			{
				return CssWordSpacingProperty.StyleConverter;
			}
		}

		// Token: 0x04000C56 RID: 3158
		private static readonly IValueConverter StyleConverter = Converters.OptionalLengthConverter.OrDefault();
	}
}
