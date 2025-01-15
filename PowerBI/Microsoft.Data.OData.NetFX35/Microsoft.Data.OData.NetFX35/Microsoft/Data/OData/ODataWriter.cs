using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001A4 RID: 420
	public abstract class ODataWriter
	{
		// Token: 0x06000C1F RID: 3103
		public abstract void WriteStart(ODataFeed feed);

		// Token: 0x06000C20 RID: 3104
		public abstract void WriteStart(ODataEntry entry);

		// Token: 0x06000C21 RID: 3105
		public abstract void WriteStart(ODataNavigationLink navigationLink);

		// Token: 0x06000C22 RID: 3106
		public abstract void WriteEnd();

		// Token: 0x06000C23 RID: 3107
		public abstract void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x06000C24 RID: 3108
		public abstract void Flush();
	}
}
