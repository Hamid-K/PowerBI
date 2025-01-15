using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002BF RID: 703
	internal sealed class CssLetterSpacingProperty : CssProperty
	{
		// Token: 0x06001545 RID: 5445 RVA: 0x0004D076 File Offset: 0x0004B276
		internal CssLetterSpacingProperty()
			: base(PropertyNames.LetterSpacing, PropertyFlags.Inherited | PropertyFlags.Unitless)
		{
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0004D084 File Offset: 0x0004B284
		internal override IValueConverter Converter
		{
			get
			{
				return CssLetterSpacingProperty.StyleConverter;
			}
		}

		// Token: 0x04000C54 RID: 3156
		private static readonly IValueConverter StyleConverter = Converters.OptionalLengthConverter.OrDefault();
	}
}
