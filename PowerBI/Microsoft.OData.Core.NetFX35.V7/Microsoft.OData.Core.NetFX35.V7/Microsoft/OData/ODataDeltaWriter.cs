using System;

namespace Microsoft.OData
{
	// Token: 0x02000057 RID: 87
	public abstract class ODataDeltaWriter
	{
		// Token: 0x060002AD RID: 685
		public abstract void WriteStart(ODataDeltaResourceSet deltaResourceSet);

		// Token: 0x060002AE RID: 686
		public abstract void WriteEnd();

		// Token: 0x060002AF RID: 687
		public abstract void WriteStart(ODataNestedResourceInfo nestedResourceInfo);

		// Token: 0x060002B0 RID: 688
		public abstract void WriteStart(ODataResourceSet expandedResourceSet);

		// Token: 0x060002B1 RID: 689
		public abstract void WriteStart(ODataResource deltaResource);

		// Token: 0x060002B2 RID: 690
		public abstract void WriteDeltaDeletedEntry(ODataDeltaDeletedEntry deltaDeletedEntry);

		// Token: 0x060002B3 RID: 691
		public abstract void WriteDeltaLink(ODataDeltaLink deltaLink);

		// Token: 0x060002B4 RID: 692
		public abstract void WriteDeltaDeletedLink(ODataDeltaDeletedLink deltaDeletedLink);

		// Token: 0x060002B5 RID: 693
		public abstract void Flush();
	}
}
