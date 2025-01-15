using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200053E RID: 1342
	internal interface IRIFObjectCreator
	{
		// Token: 0x06004973 RID: 18803
		IPersistable CreateRIFObject(ObjectType objectType, ref IntermediateFormatReader context);
	}
}
