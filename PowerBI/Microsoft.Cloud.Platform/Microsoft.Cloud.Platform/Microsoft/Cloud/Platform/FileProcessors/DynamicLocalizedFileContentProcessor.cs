using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F4 RID: 244
	public abstract class DynamicLocalizedFileContentProcessor : LocalizedFileContentProcessor
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x000181A0 File Offset: 0x000163A0
		protected DynamicLocalizedFileContentProcessor(ILocalizedStringsManager resourcesManager, string placeholderStartMarker, string placeholderEndMarker, Encoding fileContentEncoding, Dictionary<string, Func<string, CultureInfo, string>> localizedResources, CacheUsage cacheUsage)
			: base(new DynamicLocalizedFileContentProcessor.DynamicLocalizedStringsManager(resourcesManager, localizedResources), placeholderStartMarker, placeholderEndMarker, fileContentEncoding, cacheUsage)
		{
			this.m_overrides = localizedResources;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000181BE File Offset: 0x000163BE
		protected override string CustomizeLocalizedString(string key, string localizedResource)
		{
			if (!this.m_overrides.ContainsKey(key))
			{
				return this.CustomizeDefaultLocalizedStringsManager(localizedResource);
			}
			return localizedResource;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000181D7 File Offset: 0x000163D7
		protected virtual string CustomizeDefaultLocalizedStringsManager(string localizedResource)
		{
			return localizedResource;
		}

		// Token: 0x0400024E RID: 590
		private readonly Dictionary<string, Func<string, CultureInfo, string>> m_overrides;

		// Token: 0x020005DF RID: 1503
		private class DynamicLocalizedStringsManager : ILocalizedStringsManager
		{
			// Token: 0x06002BCE RID: 11214 RVA: 0x0009B20D File Offset: 0x0009940D
			public DynamicLocalizedStringsManager(ILocalizedStringsManager defaultManager, Dictionary<string, Func<string, CultureInfo, string>> overrides)
			{
				this.m_stringsManager = defaultManager;
				this.m_overrides = overrides;
			}

			// Token: 0x06002BCF RID: 11215 RVA: 0x0009B224 File Offset: 0x00099424
			public string GetLocalizedResource(string key, CultureInfo locale)
			{
				Func<string, CultureInfo, string> func;
				if (this.m_overrides.TryGetValue(key, out func))
				{
					return func(key, locale);
				}
				return this.m_stringsManager.GetLocalizedResource(key, locale);
			}

			// Token: 0x04000FF8 RID: 4088
			private ILocalizedStringsManager m_stringsManager;

			// Token: 0x04000FF9 RID: 4089
			private Dictionary<string, Func<string, CultureInfo, string>> m_overrides;
		}
	}
}
