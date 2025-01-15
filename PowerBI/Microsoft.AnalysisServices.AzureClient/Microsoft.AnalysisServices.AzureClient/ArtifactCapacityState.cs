using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000006 RID: 6
	[Guid("FB54F2ED-9378-4C5F-8031-C1DE73C0ED36")]
	[ComVisible(true)]
	public enum ArtifactCapacityState
	{
		// Token: 0x04000015 RID: 21
		Unknown,
		// Token: 0x04000016 RID: 22
		AssignedToCapacity,
		// Token: 0x04000017 RID: 23
		NotAssignedToCapacity
	}
}
