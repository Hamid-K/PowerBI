using System;
using System.Collections.Generic;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Css
{
	// Token: 0x02000105 RID: 261
	internal interface IValueConverter
	{
		// Token: 0x0600085D RID: 2141
		IPropertyValue Convert(IEnumerable<CssToken> value);

		// Token: 0x0600085E RID: 2142
		IPropertyValue Construct(CssProperty[] properties);
	}
}
