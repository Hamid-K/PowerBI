using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000278 RID: 632
	internal sealed class CssBorderSpacingProperty : CssProperty
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x0004C031 File Offset: 0x0004A231
		internal CssBorderSpacingProperty()
			: base(PropertyNames.BorderSpacing, PropertyFlags.Inherited)
		{
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0004C03F File Offset: 0x0004A23F
		internal override IValueConverter Converter
		{
			get
			{
				return CssBorderSpacingProperty.StyleConverter;
			}
		}

		// Token: 0x04000C09 RID: 3081
		private static readonly IValueConverter StyleConverter = Converters.LengthConverter.Many(1, 2).OrDefault(Length.Zero);
	}
}
