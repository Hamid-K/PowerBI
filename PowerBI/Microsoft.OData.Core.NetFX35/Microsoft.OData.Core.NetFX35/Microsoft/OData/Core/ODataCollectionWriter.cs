using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000037 RID: 55
	public abstract class ODataCollectionWriter
	{
		// Token: 0x06000201 RID: 513
		public abstract void WriteStart(ODataCollectionStart collectionStart);

		// Token: 0x06000202 RID: 514
		public abstract void WriteItem(object item);

		// Token: 0x06000203 RID: 515
		public abstract void WriteEnd();

		// Token: 0x06000204 RID: 516
		public abstract void Flush();
	}
}
