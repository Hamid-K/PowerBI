using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200040C RID: 1036
	internal interface IMemberHierarchy
	{
		// Token: 0x06002C71 RID: 11377
		IList<DataRegionMemberInstance> GetChildMemberInstances(bool isRowMember, int memberIndexInCollection);

		// Token: 0x06002C72 RID: 11378
		IList<DataCellInstance> GetCellInstances(int columnMemberSequenceId);

		// Token: 0x06002C73 RID: 11379
		IDisposable AddCellInstance(int columnMemberSequenceId, int cellIndexInCollection, DataCellInstance cellInstance, IScalabilityCache cache);

		// Token: 0x06002C74 RID: 11380
		IDisposable AddMemberInstance(DataRegionMemberInstance instance, int indexInCollection, IScalabilityCache cache, out int instanceIndex);
	}
}
