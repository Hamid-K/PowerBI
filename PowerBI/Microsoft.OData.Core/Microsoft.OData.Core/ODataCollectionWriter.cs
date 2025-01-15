using System;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000067 RID: 103
	public abstract class ODataCollectionWriter
	{
		// Token: 0x06000399 RID: 921
		public abstract void WriteStart(ODataCollectionStart collectionStart);

		// Token: 0x0600039A RID: 922
		public abstract Task WriteStartAsync(ODataCollectionStart collectionStart);

		// Token: 0x0600039B RID: 923
		public abstract void WriteItem(object item);

		// Token: 0x0600039C RID: 924
		public abstract Task WriteItemAsync(object item);

		// Token: 0x0600039D RID: 925
		public abstract void WriteEnd();

		// Token: 0x0600039E RID: 926
		public abstract Task WriteEndAsync();

		// Token: 0x0600039F RID: 927
		public abstract void Flush();

		// Token: 0x060003A0 RID: 928
		public abstract Task FlushAsync();
	}
}
