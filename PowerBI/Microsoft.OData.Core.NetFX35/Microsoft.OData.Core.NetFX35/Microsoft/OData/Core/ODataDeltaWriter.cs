using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000CB RID: 203
	public abstract class ODataDeltaWriter
	{
		// Token: 0x06000773 RID: 1907
		public abstract void WriteStart(ODataDeltaFeed deltaFeed);

		// Token: 0x06000774 RID: 1908
		public abstract void WriteEnd();

		// Token: 0x06000775 RID: 1909
		public abstract void WriteStart(ODataNavigationLink navigationLink);

		// Token: 0x06000776 RID: 1910
		public abstract void WriteStart(ODataFeed expandedFeed);

		// Token: 0x06000777 RID: 1911
		public abstract void WriteStart(ODataEntry deltaEntry);

		// Token: 0x06000778 RID: 1912
		public abstract void WriteDeltaDeletedEntry(ODataDeltaDeletedEntry deltaDeletedEntry);

		// Token: 0x06000779 RID: 1913
		public abstract void WriteDeltaLink(ODataDeltaLink deltaLink);

		// Token: 0x0600077A RID: 1914
		public abstract void WriteDeltaDeletedLink(ODataDeltaDeletedLink deltaDeletedLink);

		// Token: 0x0600077B RID: 1915
		public abstract void Flush();
	}
}
