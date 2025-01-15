using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200088C RID: 2188
	internal interface ITransferable
	{
		// Token: 0x06007819 RID: 30745
		void TransferTo(IScalabilityCache scaleCache);
	}
}
