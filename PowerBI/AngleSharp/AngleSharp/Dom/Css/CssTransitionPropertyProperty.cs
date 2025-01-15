using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F0 RID: 752
	internal sealed class CssTransitionPropertyProperty : CssProperty
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x0004DC8F File Offset: 0x0004BE8F
		internal CssTransitionPropertyProperty()
			: base(PropertyNames.TransitionProperty, PropertyFlags.None)
		{
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0004DC9D File Offset: 0x0004BE9D
		internal override IValueConverter Converter
		{
			get
			{
				return CssTransitionPropertyProperty.ListConverter;
			}
		}

		// Token: 0x04000C83 RID: 3203
		private static readonly IValueConverter ListConverter = Converters.AnimatableConverter.FromList().OrNone().OrDefault(Keywords.All);
	}
}
