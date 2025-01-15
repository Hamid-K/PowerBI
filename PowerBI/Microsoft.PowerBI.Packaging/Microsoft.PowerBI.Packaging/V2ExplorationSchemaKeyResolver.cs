using System;
using System.Globalization;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000031 RID: 49
	internal class V2ExplorationSchemaKeyResolver : IV2ExplorationSchemaKeyResolver
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00005624 File Offset: 0x00003824
		public string GetSchemaKeyForFile(string filePath)
		{
			StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
			if (filePath.EndsWith(".bookmark.json", stringComparison) && filePath.StartsWith("bookmarks/", stringComparison))
			{
				return "bookmark";
			}
			if (filePath.EndsWith("bookmarks/bookmarks.json", stringComparison))
			{
				return "bookmarksMetadata";
			}
			if (filePath.EndsWith("/page.json", stringComparison))
			{
				return "page";
			}
			if (filePath.EndsWith("/pages.json", stringComparison))
			{
				return "pagesMetadata";
			}
			if (filePath.Equals("report.json", stringComparison))
			{
				return "report";
			}
			if (filePath.Equals("reportExtensions.json", stringComparison))
			{
				return "reportExtension";
			}
			if (filePath.EndsWith("/version.json", stringComparison) || filePath.Equals("version.json", stringComparison))
			{
				return "versionMetadata";
			}
			if (filePath.EndsWith("/visual.json", stringComparison))
			{
				return "visualContainer";
			}
			if (filePath.EndsWith("/mobile.json", stringComparison))
			{
				return "visualContainerMobileState";
			}
			throw new NotImplementedException(("Can't compute schema key for '" + filePath + "'.").ToString(CultureInfo.CurrentCulture));
		}
	}
}
