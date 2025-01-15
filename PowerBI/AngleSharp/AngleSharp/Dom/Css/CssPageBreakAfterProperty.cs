using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000299 RID: 665
	internal sealed class CssPageBreakAfterProperty : CssProperty
	{
		// Token: 0x060014D2 RID: 5330 RVA: 0x0004C841 File Offset: 0x0004AA41
		internal CssPageBreakAfterProperty()
			: base(PropertyNames.PageBreakAfter, PropertyFlags.None)
		{
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0004C84F File Offset: 0x0004AA4F
		internal override IValueConverter Converter
		{
			get
			{
				return CssPageBreakAfterProperty.StyleConverter;
			}
		}

		// Token: 0x04000C2E RID: 3118
		private static readonly IValueConverter StyleConverter = Converters.PageBreakModeConverter.OrDefault(BreakMode.Auto);
	}
}
