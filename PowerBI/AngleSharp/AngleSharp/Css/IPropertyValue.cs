using System;
using AngleSharp.Dom.Css;

namespace AngleSharp.Css
{
	// Token: 0x02000104 RID: 260
	internal interface IPropertyValue
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600085A RID: 2138
		string CssText { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600085B RID: 2139
		CssValue Original { get; }

		// Token: 0x0600085C RID: 2140
		CssValue ExtractFor(string name);
	}
}
