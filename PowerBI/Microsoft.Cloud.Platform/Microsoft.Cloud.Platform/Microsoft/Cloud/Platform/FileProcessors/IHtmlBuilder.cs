using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000FB RID: 251
	public interface IHtmlBuilder
	{
		// Token: 0x06000703 RID: 1795
		string BuildHtml(HtmlTemplate template, CultureInfo culture);
	}
}
