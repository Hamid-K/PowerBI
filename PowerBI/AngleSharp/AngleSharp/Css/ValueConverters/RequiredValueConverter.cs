using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css.ValueConverters
{
	// Token: 0x02000141 RID: 321
	internal sealed class RequiredValueConverter : IValueConverter
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x00040426 File Offset: 0x0003E626
		public RequiredValueConverter(IValueConverter converter)
		{
			this._converter = converter;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00040435 File Offset: 0x0003E635
		public IPropertyValue Convert(IEnumerable<CssToken> value)
		{
			if (!value.Any<CssToken>())
			{
				return null;
			}
			return this._converter.Convert(value);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0004044D File Offset: 0x0003E64D
		public IPropertyValue Construct(CssProperty[] properties)
		{
			return this._converter.Construct(properties);
		}

		// Token: 0x04000901 RID: 2305
		private readonly IValueConverter _converter;
	}
}
