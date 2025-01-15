using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002E2 RID: 738
	internal sealed class CssTextJustifyProperty : CssProperty
	{
		// Token: 0x060015AC RID: 5548 RVA: 0x0004D725 File Offset: 0x0004B925
		public CssTextJustifyProperty()
			: base(PropertyNames.TextJustify, PropertyFlags.None)
		{
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0004D733 File Offset: 0x0004B933
		internal override IValueConverter Converter
		{
			get
			{
				return CssTextJustifyProperty.StyleConverter;
			}
		}

		// Token: 0x04000C75 RID: 3189
		private static readonly IValueConverter StyleConverter = Converters.TextJustifyConverter;
	}
}
