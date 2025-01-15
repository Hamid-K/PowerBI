using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200029A RID: 666
	internal sealed class CssPageBreakBeforeProperty : CssProperty
	{
		// Token: 0x060014D5 RID: 5333 RVA: 0x0004C868 File Offset: 0x0004AA68
		internal CssPageBreakBeforeProperty()
			: base(PropertyNames.PageBreakBefore, PropertyFlags.None)
		{
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x0004C876 File Offset: 0x0004AA76
		internal override IValueConverter Converter
		{
			get
			{
				return CssPageBreakBeforeProperty.StyleConverter;
			}
		}

		// Token: 0x04000C2F RID: 3119
		private static readonly IValueConverter StyleConverter = Converters.PageBreakModeConverter.OrDefault(BreakMode.Auto);
	}
}
