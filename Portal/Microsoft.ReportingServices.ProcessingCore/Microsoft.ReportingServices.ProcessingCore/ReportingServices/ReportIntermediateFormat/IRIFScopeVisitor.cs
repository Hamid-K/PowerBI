using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000528 RID: 1320
	public interface IRIFScopeVisitor
	{
		// Token: 0x06004748 RID: 18248
		void PreVisit(DataRegion dataRegion);

		// Token: 0x06004749 RID: 18249
		void PostVisit(DataRegion dataRegion);

		// Token: 0x0600474A RID: 18250
		void PreVisit(ReportHierarchyNode member);

		// Token: 0x0600474B RID: 18251
		void PostVisit(ReportHierarchyNode member);

		// Token: 0x0600474C RID: 18252
		void PreVisit(Cell cell, int rowIndex, int colIndex);

		// Token: 0x0600474D RID: 18253
		void PostVisit(Cell cell, int rowIndex, int colIndex);
	}
}
