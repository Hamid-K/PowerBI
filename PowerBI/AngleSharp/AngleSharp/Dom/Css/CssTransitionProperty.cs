using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002EF RID: 751
	internal sealed class CssTransitionProperty : CssShorthandProperty
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x0004DBD0 File Offset: 0x0004BDD0
		internal CssTransitionProperty()
			: base(PropertyNames.Transition, PropertyFlags.None)
		{
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0004DBDE File Offset: 0x0004BDDE
		internal override IValueConverter Converter
		{
			get
			{
				return CssTransitionProperty.ListConverter;
			}
		}

		// Token: 0x04000C82 RID: 3202
		internal static readonly IValueConverter ListConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.AnimatableConverter.Option().For(new string[] { PropertyNames.TransitionProperty }),
			Converters.TimeConverter.Option().For(new string[] { PropertyNames.TransitionDuration }),
			Converters.TransitionConverter.Option().For(new string[] { PropertyNames.TransitionTimingFunction }),
			Converters.TimeConverter.Option().For(new string[] { PropertyNames.TransitionDelay })
		}).FromList().OrDefault();
	}
}
