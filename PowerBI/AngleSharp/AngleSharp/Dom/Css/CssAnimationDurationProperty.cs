using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000259 RID: 601
	internal sealed class CssAnimationDurationProperty : CssProperty
	{
		// Token: 0x06001412 RID: 5138 RVA: 0x0004B57A File Offset: 0x0004977A
		internal CssAnimationDurationProperty()
			: base(PropertyNames.AnimationDuration, PropertyFlags.None)
		{
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0004B588 File Offset: 0x00049788
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationDurationProperty.ListConverter;
			}
		}

		// Token: 0x04000BE8 RID: 3048
		private static readonly IValueConverter ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);
	}
}
