using System;

namespace Microsoft.Data.OData
{
	// Token: 0x02000185 RID: 389
	public abstract class ODataCollectionWriter
	{
		// Token: 0x06000AA1 RID: 2721
		public abstract void WriteStart(ODataCollectionStart collectionStart);

		// Token: 0x06000AA2 RID: 2722
		public abstract void WriteItem(object item);

		// Token: 0x06000AA3 RID: 2723
		public abstract void WriteEnd();

		// Token: 0x06000AA4 RID: 2724
		public abstract void Flush();
	}
}
