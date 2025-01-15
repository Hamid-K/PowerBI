using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000021 RID: 33
	internal interface IRIFObjectCreator
	{
		// Token: 0x0600018E RID: 398
		IPersistable CreateRIFObject(ObjectType objectType, ref IntermediateFormatReader context);
	}
}
