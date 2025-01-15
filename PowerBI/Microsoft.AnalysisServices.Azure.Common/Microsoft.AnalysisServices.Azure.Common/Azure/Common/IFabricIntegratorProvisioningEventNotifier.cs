using System;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200007F RID: 127
	public interface IFabricIntegratorProvisioningEventNotifier : IFabricIntegratorProvisioningEventListener
	{
		// Token: 0x060004E4 RID: 1252
		void NotifyFabricIntegratorProvisioningEvent(FabricIntegratorProvisioningEventArgs args);
	}
}
