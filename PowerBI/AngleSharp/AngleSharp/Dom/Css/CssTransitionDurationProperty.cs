using System;
using AngleSharp.Css;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002EE RID: 750
	internal sealed class CssTransitionDurationProperty : CssProperty
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x0004DBA0 File Offset: 0x0004BDA0
		internal CssTransitionDurationProperty()
			: base(PropertyNames.TransitionDuration, PropertyFlags.None)
		{
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0004DBAE File Offset: 0x0004BDAE
		internal override IValueConverter Converter
		{
			get
			{
				return CssTransitionDurationProperty.ListConverter;
			}
		}

		// Token: 0x04000C81 RID: 3201
		private static readonly IValueConverter ListConverter = Converters.TimeConverter.FromList().OrDefault(Time.Zero);
	}
}
