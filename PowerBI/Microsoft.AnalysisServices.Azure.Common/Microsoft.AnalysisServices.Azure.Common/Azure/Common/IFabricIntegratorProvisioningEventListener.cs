using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200007E RID: 126
	public interface IFabricIntegratorProvisioningEventListener
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060004E2 RID: 1250
		// (remove) Token: 0x060004E3 RID: 1251
		event EventHandler<FabricIntegratorProvisioningEventArgs> ProvisioningEventCompletedHandler;
	}
}
