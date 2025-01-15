using System;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000150 RID: 336
	internal sealed class SharedItemConverter : ISharedItemConverter
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x0003910C File Offset: 0x0003730C
		public SharedItemConverter(PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			this.prepareMethod = prepareMethod;
			this.convertMethod = convertMethod;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00039122 File Offset: 0x00037322
		public void PrepareItemForCaching(string cacheName, ref object item, out string typeCode)
		{
			this.prepareMethod(cacheName, ref item, out typeCode);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00039132 File Offset: 0x00037332
		public object ConvertCachedItem(string cacheName, object cachedItem, string typeCode)
		{
			return this.convertMethod(cacheName, cachedItem, typeCode);
		}

		// Token: 0x04000B11 RID: 2833
		private readonly PrepareItemForCaching prepareMethod;

		// Token: 0x04000B12 RID: 2834
		private readonly ConvertCachedItem convertMethod;
	}
}
