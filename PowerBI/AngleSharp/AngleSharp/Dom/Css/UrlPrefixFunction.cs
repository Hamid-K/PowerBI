using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000237 RID: 567
	internal sealed class UrlPrefixFunction : DocumentFunction
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x0004AC4B File Offset: 0x00048E4B
		public UrlPrefixFunction(string url)
			: base(FunctionNames.UrlPrefix, url)
		{
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004AC59 File Offset: 0x00048E59
		public override bool Matches(Url url)
		{
			return url.Href.StartsWith(base.Data, StringComparison.OrdinalIgnoreCase);
		}
	}
}
