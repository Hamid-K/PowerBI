using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200005D RID: 93
	public class EnhancedReportFormatMatcher : IFileFormatMatcher
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x00007EF5 File Offset: 0x000060F5
		public bool IsMatch(string relativePath)
		{
			return Regex.IsMatch(relativePath.Replace("/", "\\"), EnhancedReportFormatMatcher.RegFormat, RegexOptions.IgnoreCase);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00007F14 File Offset: 0x00006114
		private static string GetRegexForEnhancedReportFileFormat()
		{
			string[] enhancedReportSupportedFileExpressions = EnhancedReportFormatMatcher.GetEnhancedReportSupportedFileExpressions();
			List<string> list = new List<string>();
			foreach (string text in enhancedReportSupportedFileExpressions)
			{
				string text2 = Regex.Escape(text);
				text2 = "^" + text + "$";
				list.Add(text2);
			}
			return string.Join("|", list.ToArray());
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00007F74 File Offset: 0x00006174
		private static string[] GetEnhancedReportSupportedFileExpressions()
		{
			return new string[] { "report.json", "version.json", "reportExtensions.json", "pages\\\\pages.json", "pages\\\\[\\w\\-]+\\\\page.json", "pages\\\\[\\w\\-]+\\\\visuals\\\\[\\w\\-]+\\\\visual.json", "pages\\\\[\\w\\-]+\\\\visuals\\\\[\\w\\-]+\\\\mobile.json", "bookmarks\\\\bookmarks.json", "bookmarks\\\\[\\w\\-]+.bookmark.json" };
		}

		// Token: 0x04000164 RID: 356
		private static readonly string RegFormat = EnhancedReportFormatMatcher.GetRegexForEnhancedReportFileFormat();
	}
}
