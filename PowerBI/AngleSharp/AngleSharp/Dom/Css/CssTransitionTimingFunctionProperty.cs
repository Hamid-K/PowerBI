using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F1 RID: 753
	internal sealed class CssTransitionTimingFunctionProperty : CssProperty
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x0004DCC4 File Offset: 0x0004BEC4
		internal CssTransitionTimingFunctionProperty()
			: base(PropertyNames.TransitionTimingFunction, PropertyFlags.None)
		{
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x0004DCD2 File Offset: 0x0004BED2
		internal override IValueConverter Converter
		{
			get
			{
				return CssTransitionTimingFunctionProperty.ListConverter;
			}
		}

		// Token: 0x04000C84 RID: 3204
		private static readonly IValueConverter ListConverter = Converters.TransitionConverter.FromList().OrDefault(Map.TimingFunctions[Keywords.Ease]);
	}
}
