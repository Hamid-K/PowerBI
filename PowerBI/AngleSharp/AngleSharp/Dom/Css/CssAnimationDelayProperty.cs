using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000257 RID: 599
	internal sealed class CssAnimationDelayProperty : CssProperty
	{
		// Token: 0x0600140C RID: 5132 RVA: 0x0004B51E File Offset: 0x0004971E
		internal CssAnimationDelayProperty()
			: base(PropertyNames.AnimationDelay, PropertyFlags.None)
		{
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0004B52C File Offset: 0x0004972C
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationDelayProperty.ListConverter;
			}
		}

		// Token: 0x04000BE6 RID: 3046
		private static readonly IValueConverter ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);
	}
}
