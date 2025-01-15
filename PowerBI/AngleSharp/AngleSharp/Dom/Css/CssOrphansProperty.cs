using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000250 RID: 592
	internal sealed class CssOrphansProperty : CssProperty
	{
		// Token: 0x060013F8 RID: 5112 RVA: 0x0004B3D5 File Offset: 0x000495D5
		internal CssOrphansProperty()
			: base(PropertyNames.Orphans, PropertyFlags.Inherited)
		{
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0004B3E3 File Offset: 0x000495E3
		internal override IValueConverter Converter
		{
			get
			{
				return CssOrphansProperty.StyleConverter;
			}
		}

		// Token: 0x04000BE1 RID: 3041
		private static readonly IValueConverter StyleConverter = Converters.NaturalIntegerConverter.OrDefault(2);
	}
}
