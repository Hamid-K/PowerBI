using System;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000099 RID: 153
	[ServiceContract]
	[ECFContract(FlattenHierarchy = true)]
	public interface ILongRunningFabricIntegratorProvisioningService : IFabricIntegratorProvisioningService
	{
	}
}
