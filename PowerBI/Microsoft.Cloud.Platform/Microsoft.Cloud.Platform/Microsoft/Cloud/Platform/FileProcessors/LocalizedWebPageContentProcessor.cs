using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F6 RID: 246
	public class LocalizedWebPageContentProcessor : DynamicLocalizedFileContentProcessor
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x000182C7 File Offset: 0x000164C7
		public LocalizedWebPageContentProcessor(ILocalizedStringsManager resourcesManager, string placeholderStartMarker, string placeholderEndMarker, IEnumerable<CssLocalizationData> cssLocalizationData, CacheUsage cacheUsage)
			: this(resourcesManager, placeholderStartMarker, placeholderEndMarker, TemplateFileProcessor.DefaultFileContentEncoding, cssLocalizationData, cacheUsage)
		{
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000182DB File Offset: 0x000164DB
		public LocalizedWebPageContentProcessor(ILocalizedStringsManager resourcesManager, string placeholderStartMarker, string placeholderEndMarker, Encoding fileContentEncoding, IEnumerable<CssLocalizationData> cssLocalizationData, CacheUsage cacheUsage)
			: base(resourcesManager, placeholderStartMarker, placeholderEndMarker, fileContentEncoding, LocalizedWebPageContentProcessor.GetLocalizedResources(cssLocalizationData), cacheUsage)
		{
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00018179 File Offset: 0x00016379
		protected override string GetCacheKey(FileProcessorInfo fileProcessorInfo, IFileContentInfo fileInfo)
		{
			return "{0}_{1}".FormatWithInvariantCulture(new object[] { fileInfo.HashString, fileProcessorInfo.Culture });
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000182F4 File Offset: 0x000164F4
		private static Dictionary<string, Func<string, CultureInfo, string>> GetLocalizedResources(IEnumerable<CssLocalizationData> cssLocalizationData)
		{
			Dictionary<string, Func<string, CultureInfo, string>> dictionary = new Dictionary<string, Func<string, CultureInfo, string>>();
			foreach (CssLocalizationData cssLocalizationData2 in cssLocalizationData)
			{
				CssLocalizationData clonedLocData = new CssLocalizationData(cssLocalizationData2.CssPlaceholder, cssLocalizationData2.LtrCssValue, cssLocalizationData2.RtlCssValue);
				dictionary.Add(cssLocalizationData2.CssPlaceholder, (string key, CultureInfo cultureInfo) => LocalizedWebPageContentProcessor.GetCssString(key, cultureInfo, clonedLocData));
			}
			return dictionary;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00018378 File Offset: 0x00016578
		[CanBeNull]
		private static string GetCssString(string key, CultureInfo culture, CssLocalizationData cssLocalizationData)
		{
			if (!key.Equals(cssLocalizationData.CssPlaceholder, StringComparison.Ordinal))
			{
				return null;
			}
			if (culture.TextInfo.IsRightToLeft)
			{
				return cssLocalizationData.RtlCssValue;
			}
			return cssLocalizationData.LtrCssValue;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000183A5 File Offset: 0x000165A5
		protected override string CustomizeDefaultLocalizedStringsManager(string localizedResource)
		{
			if (localizedResource != null)
			{
				return HttpUtility.HtmlEncode(localizedResource);
			}
			return localizedResource;
		}
	}
}
