using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200025B RID: 603
	internal sealed class CssAnimationIterationCountProperty : CssProperty
	{
		// Token: 0x06001418 RID: 5144 RVA: 0x0004B5D6 File Offset: 0x000497D6
		internal CssAnimationIterationCountProperty()
			: base(PropertyNames.AnimationIterationCount, PropertyFlags.None)
		{
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0004B5E4 File Offset: 0x000497E4
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationIterationCountProperty.ListConverter;
			}
		}

		// Token: 0x04000BEA RID: 3050
		private static readonly IValueConverter ListConverter = Converters.PositiveOrInfiniteNumberConverter.FromList().OrDefault(1f);
	}
}
