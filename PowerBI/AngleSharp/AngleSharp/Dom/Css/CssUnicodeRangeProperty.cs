using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002C2 RID: 706
	internal sealed class CssUnicodeRangeProperty : CssProperty
	{
		// Token: 0x0600154D RID: 5453 RVA: 0x0004D0DD File Offset: 0x0004B2DD
		public CssUnicodeRangeProperty()
			: base(PropertyNames.UnicodeRange, PropertyFlags.None)
		{
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0004B13B File Offset: 0x0004933B
		internal override IValueConverter Converter
		{
			get
			{
				return Converters.Any;
			}
		}
	}
}
