using System;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000034 RID: 52
	internal sealed class SharedItemConverter : ISharedItemConverter
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00007E88 File Offset: 0x00006088
		public SharedItemConverter(PrepareItemForCaching prepareMethod, ConvertCachedItem convertMethod)
		{
			this.prepareMethod = prepareMethod;
			this.convertMethod = convertMethod;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007E9E File Offset: 0x0000609E
		public void PrepareItemForCaching(string cacheName, ref object item, out string typeCode)
		{
			this.prepareMethod(cacheName, ref item, out typeCode);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007EAE File Offset: 0x000060AE
		public object ConvertCachedItem(string cacheName, object cachedItem, string typeCode)
		{
			return this.convertMethod(cacheName, cachedItem, typeCode);
		}

		// Token: 0x040000DC RID: 220
		private readonly PrepareItemForCaching prepareMethod;

		// Token: 0x040000DD RID: 221
		private readonly ConvertCachedItem convertMethod;
	}
}
