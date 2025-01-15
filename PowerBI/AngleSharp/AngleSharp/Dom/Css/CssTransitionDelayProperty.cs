using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002ED RID: 749
	internal sealed class CssTransitionDelayProperty : CssProperty
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x0004DB70 File Offset: 0x0004BD70
		internal CssTransitionDelayProperty()
			: base(PropertyNames.TransitionDelay, PropertyFlags.None)
		{
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x0004DB7E File Offset: 0x0004BD7E
		internal override IValueConverter Converter
		{
			get
			{
				return CssTransitionDelayProperty.ListConverter;
			}
		}

		// Token: 0x04000C80 RID: 3200
		private static readonly IValueConverter ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);
	}
}
