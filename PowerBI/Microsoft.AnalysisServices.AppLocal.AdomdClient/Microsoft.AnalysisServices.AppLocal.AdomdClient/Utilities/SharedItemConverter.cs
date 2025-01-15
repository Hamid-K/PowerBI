using System;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000150 RID: 336
	internal sealed class SharedItemConverter : ISharedItemConverter
	{
		// Token: 0x060010AC RID: 4268 RVA: 0x0003943C File Offset: 0x0003763C
		public SharedItemConverter(PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			this.prepareMethod = prepareMethod;
			this.convertMethod = convertMethod;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00039452 File Offset: 0x00037652
		public void PrepareItemForCaching(string cacheName, ref object item, out string typeCode)
		{
			this.prepareMethod(cacheName, ref item, out typeCode);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00039462 File Offset: 0x00037662
		public object ConvertCachedItem(string cacheName, object cachedItem, string typeCode)
		{
			return this.convertMethod(cacheName, cachedItem, typeCode);
		}

		// Token: 0x04000B1E RID: 2846
		private readonly PrepareItemForCaching prepareMethod;

		// Token: 0x04000B1F RID: 2847
		private readonly ConvertCachedItem convertMethod;
	}
}
