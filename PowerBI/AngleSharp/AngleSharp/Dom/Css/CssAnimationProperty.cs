using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200025E RID: 606
	internal sealed class CssAnimationProperty : CssShorthandProperty
	{
		// Token: 0x06001421 RID: 5153 RVA: 0x0004B662 File Offset: 0x00049862
		internal CssAnimationProperty()
			: base(PropertyNames.Animation, PropertyFlags.None)
		{
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0004B670 File Offset: 0x00049870
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationProperty.ListConverter;
			}
		}

		// Token: 0x04000BED RID: 3053
		private static readonly IValueConverter ListConverter = Converters.WithAny(new IValueConverter[]
		{
			Converters.TimeConverter.Option().For(new string[] { PropertyNames.AnimationDuration }),
			Converters.TransitionConverter.Option().For(new string[] { PropertyNames.AnimationTimingFunction }),
			Converters.TimeConverter.Option().For(new string[] { PropertyNames.AnimationDelay }),
			Converters.PositiveOrInfiniteNumberConverter.Option().For(new string[] { PropertyNames.AnimationIterationCount }),
			Converters.AnimationDirectionConverter.Option().For(new string[] { PropertyNames.AnimationDirection }),
			Converters.AnimationFillStyleConverter.Option().For(new string[] { PropertyNames.AnimationFillMode }),
			Converters.PlayStateConverter.Option().For(new string[] { PropertyNames.AnimationPlayState }),
			Converters.IdentifierConverter.Option().For(new string[] { PropertyNames.AnimationName })
		}).FromList().OrDefault();
	}
}
