using System;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000145 RID: 325
	internal sealed class SharedItemConverter : ISharedItemConverter
	{
		// Token: 0x0600113A RID: 4410 RVA: 0x0003BD40 File Offset: 0x00039F40
		public SharedItemConverter(PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			this.prepareMethod = prepareMethod;
			this.convertMethod = convertMethod;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003BD56 File Offset: 0x00039F56
		public void PrepareItemForCaching(string cacheName, ref object item, out string typeCode)
		{
			this.prepareMethod(cacheName, ref item, out typeCode);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003BD66 File Offset: 0x00039F66
		public object ConvertCachedItem(string cacheName, object cachedItem, string typeCode)
		{
			return this.convertMethod(cacheName, cachedItem, typeCode);
		}

		// Token: 0x04000AD7 RID: 2775
		private readonly PrepareItemForCaching prepareMethod;

		// Token: 0x04000AD8 RID: 2776
		private readonly ConvertCachedItem convertMethod;
	}
}
