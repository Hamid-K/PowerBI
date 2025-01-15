using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004D0 RID: 1232
	internal interface ICustomPropertiesHolder
	{
		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x06003E7F RID: 15999
		IInstancePath InstancePath { get; }

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x06003E80 RID: 16000
		DataValueList CustomProperties { get; }
	}
}
