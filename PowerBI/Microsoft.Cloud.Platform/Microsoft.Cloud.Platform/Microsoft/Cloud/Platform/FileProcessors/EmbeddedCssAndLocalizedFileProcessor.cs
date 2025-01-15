using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F7 RID: 247
	public class EmbeddedCssAndLocalizedFileProcessor : LocalizedWebPageContentProcessor
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x000183B4 File Offset: 0x000165B4
		public EmbeddedCssAndLocalizedFileProcessor(ILocalizedStringsManager stringsManager, IFileContentInfoFactory fileContentFactory, IEnumerable<CssLocalizationData> css, Encoding encoding)
			: base(stringsManager, "~", "~", encoding, css.Select((CssLocalizationData c) => CssLocalizationData.GetCssLocalizationDataFromFile(c.CssPlaceholder, c.LtrCssValue, c.RtlCssValue, fileContentFactory, Encoding.UTF8)), CacheUsage.UseCache)
		{
		}

		// Token: 0x04000252 RID: 594
		private const string c_placeholderMarker = "~";
	}
}
