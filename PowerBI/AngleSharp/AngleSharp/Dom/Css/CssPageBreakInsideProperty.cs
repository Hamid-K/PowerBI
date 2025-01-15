using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200029B RID: 667
	internal sealed class CssPageBreakInsideProperty : CssProperty
	{
		// Token: 0x060014D8 RID: 5336 RVA: 0x0004C88F File Offset: 0x0004AA8F
		internal CssPageBreakInsideProperty()
			: base(PropertyNames.PageBreakInside, PropertyFlags.None)
		{
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0004C89D File Offset: 0x0004AA9D
		internal override IValueConverter Converter
		{
			get
			{
				return CssPageBreakInsideProperty.StyleConverter;
			}
		}

		// Token: 0x04000C30 RID: 3120
		private static readonly IValueConverter StyleConverter = Converters.Assign<BreakMode>(Keywords.Auto, BreakMode.Auto).Or(Keywords.Avoid, BreakMode.Avoid).OrDefault(BreakMode.Auto);
	}
}
