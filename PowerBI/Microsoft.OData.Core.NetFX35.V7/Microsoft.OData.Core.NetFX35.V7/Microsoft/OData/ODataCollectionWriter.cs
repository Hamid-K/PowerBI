using System;

namespace Microsoft.OData
{
	// Token: 0x02000045 RID: 69
	public abstract class ODataCollectionWriter
	{
		// Token: 0x0600022A RID: 554
		public abstract void WriteStart(ODataCollectionStart collectionStart);

		// Token: 0x0600022B RID: 555
		public abstract void WriteItem(object item);

		// Token: 0x0600022C RID: 556
		public abstract void WriteEnd();

		// Token: 0x0600022D RID: 557
		public abstract void Flush();
	}
}
