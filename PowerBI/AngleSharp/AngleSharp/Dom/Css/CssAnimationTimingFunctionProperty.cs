using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200025F RID: 607
	internal sealed class CssAnimationTimingFunctionProperty : CssProperty
	{
		// Token: 0x06001424 RID: 5156 RVA: 0x0004B79F File Offset: 0x0004999F
		internal CssAnimationTimingFunctionProperty()
			: base(PropertyNames.AnimationTimingFunction, PropertyFlags.None)
		{
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0004B7AD File Offset: 0x000499AD
		internal override IValueConverter Converter
		{
			get
			{
				return CssAnimationTimingFunctionProperty.ListConverter;
			}
		}

		// Token: 0x04000BEE RID: 3054
		private static readonly IValueConverter ListConverter = Converters.TransitionConverter.FromList().OrDefault(Map.TimingFunctions[Keywords.Ease]);
	}
}
