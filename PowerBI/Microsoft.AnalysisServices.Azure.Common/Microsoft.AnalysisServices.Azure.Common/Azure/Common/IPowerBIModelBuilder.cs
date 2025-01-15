using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A2 RID: 162
	[ServiceContract]
	[ECFContract]
	public interface IPowerBIModelBuilder
	{
		// Token: 0x060005BB RID: 1467
		[OperationContract]
		Task<PowerBIModelPopulateInfo> BuildPowerBIModelAsync(PowerBIModelProvisioningInfo powerBIModelInfo);

		// Token: 0x060005BC RID: 1468
		[OperationContract(AsyncPattern = true, Name = "BeginBuildPowerBIModel")]
		IAsyncResult BeginBuildPowerBIModel(PowerBIModelProvisioningInfo powerBIModelInfo, AsyncCallback callback, object context);

		// Token: 0x060005BD RID: 1469
		PowerBIModelPopulateInfo EndBuildPowerBIModel(IAsyncResult result);
	}
}
