using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200025A RID: 602
	internal sealed class CssAnimationFillModeProperty : CssProperty
	{
		// Token: 0x06001415 RID: 5141 RVA: 0x0004B5AA File Offset: 0x000497AA
		internal CssAnimationFillModeProperty()
			: base(PropertyNames.AnimationFillMode, PropertyFlags.None)
		{
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0004B5B8 File Offset: 0x000497B8
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationFillModeProperty.ListConverter;
			}
		}

		// Token: 0x04000BE9 RID: 3049
		private static readonly IValueConverter ListConverter = Converters.AnimationFillStyleConverter.FromList().OrDefault(AnimationFillStyle.None);
	}
}
