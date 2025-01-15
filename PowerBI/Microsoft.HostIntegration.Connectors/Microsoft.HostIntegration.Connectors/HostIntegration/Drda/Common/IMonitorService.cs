using System;
using System.ServiceModel;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000818 RID: 2072
	[ServiceContract(CallbackContract = typeof(ISubscriber))]
	public interface IMonitorService
	{
		// Token: 0x060041A5 RID: 16805
		[OperationContract(IsOneWay = true)]
		void RegisterForEvents();

		// Token: 0x060041A6 RID: 16806
		[OperationContract(IsOneWay = true)]
		void UnregisterForEvents();

		// Token: 0x060041A7 RID: 16807
		[OperationContract]
		ConnectionInfo[] GetConnections();
	}
}
