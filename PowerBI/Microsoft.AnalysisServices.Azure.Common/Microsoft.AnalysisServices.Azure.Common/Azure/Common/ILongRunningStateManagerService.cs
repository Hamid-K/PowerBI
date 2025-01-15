using System;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200009A RID: 154
	[ServiceContract]
	[ECFContract(FlattenHierarchy = true)]
	public interface ILongRunningStateManagerService : IStateManagerService
	{
	}
}
