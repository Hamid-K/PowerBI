using System;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000098 RID: 152
	[ServiceContract]
	[ECFContract(FlattenHierarchy = true)]
	public interface ILongRunningAdminProvisioningService : IAdminProvisioningService
	{
	}
}
