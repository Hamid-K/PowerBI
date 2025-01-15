using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000060 RID: 96
	public abstract class ODataWriter
	{
		// Token: 0x060003E0 RID: 992
		public abstract void WriteStart(ODataFeed feed);

		// Token: 0x060003E1 RID: 993
		public abstract void WriteStart(ODataEntry entry);

		// Token: 0x060003E2 RID: 994
		public abstract void WriteStart(ODataNavigationLink navigationLink);

		// Token: 0x060003E3 RID: 995
		public abstract void WriteEnd();

		// Token: 0x060003E4 RID: 996
		public abstract void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink);

		// Token: 0x060003E5 RID: 997
		public abstract void Flush();
	}
}
