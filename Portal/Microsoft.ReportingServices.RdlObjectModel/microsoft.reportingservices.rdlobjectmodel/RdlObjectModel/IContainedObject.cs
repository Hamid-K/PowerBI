using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E3 RID: 483
	public interface IContainedObject
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600100B RID: 4107
		// (set) Token: 0x0600100C RID: 4108
		IContainedObject Parent { get; set; }
	}
}
