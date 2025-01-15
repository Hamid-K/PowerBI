using System;
using System.Collections.Concurrent;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000EF RID: 239
	public abstract class BaseFileContentProcessor : IFileContentProcessor
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x00017DF9 File Offset: 0x00015FF9
		protected BaseFileContentProcessor(CacheUsage cacheUsage)
		{
			this.m_useCache = cacheUsage;
			this.m_fileContentInfoCache = new ConcurrentDictionary<string, IFileContentInfo>();
		}

		// Token: 0x060006B9 RID: 1721
		public abstract bool CanProcessFile(FileProcessorInfo fileProcessorInfo);

		// Token: 0x060006BA RID: 1722 RVA: 0x00017E14 File Offset: 0x00016014
		public IFileContentInfo Process(FileProcessorInfo processorInfo, IFileContentInfo fileInfo)
		{
			if (!this.m_useCache.HasFlag(CacheUsage.UseCache))
			{
				return this.ProcessExecute(processorInfo, fileInfo);
			}
			string cacheKey = this.GetCacheKey(processorInfo, fileInfo);
			IFileContentInfo fileContentInfo;
			if (this.m_fileContentInfoCache.TryGetValue(cacheKey, out fileContentInfo))
			{
				return fileContentInfo;
			}
			fileContentInfo = this.ProcessExecute(processorInfo, fileInfo);
			IFileContentInfo orAdd = this.m_fileContentInfoCache.GetOrAdd(cacheKey, fileContentInfo);
			ExtendedDiagnostics.EnsureOperation(fileContentInfo.Equals(orAdd), "While processing request in BaseFileContentProcessor, there has been a race condition for adding key '{0}' to the cache, with different values".FormatWithInvariantCulture(new object[] { cacheKey }));
			return orAdd;
		}

		// Token: 0x060006BB RID: 1723
		protected abstract string GetCacheKey(FileProcessorInfo context, IFileContentInfo fileInfo);

		// Token: 0x060006BC RID: 1724
		protected abstract IFileContentInfo ProcessExecute(FileProcessorInfo processorInfo, IFileContentInfo fileInfo);

		// Token: 0x060006BD RID: 1725 RVA: 0x00017E95 File Offset: 0x00016095
		protected void InvalidateCache()
		{
			this.m_fileContentInfoCache.Clear();
		}

		// Token: 0x04000244 RID: 580
		private readonly CacheUsage m_useCache;

		// Token: 0x04000245 RID: 581
		private readonly ConcurrentDictionary<string, IFileContentInfo> m_fileContentInfoCache;
	}
}
