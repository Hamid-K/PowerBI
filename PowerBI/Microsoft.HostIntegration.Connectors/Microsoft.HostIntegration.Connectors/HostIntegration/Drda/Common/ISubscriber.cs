using System;
using System.ServiceModel;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000819 RID: 2073
	[ServiceContract]
	public interface ISubscriber
	{
		// Token: 0x060041A8 RID: 16808
		[OperationContract(IsOneWay = true)]
		void LogEvent(LogData data);

		// Token: 0x060041A9 RID: 16809
		[OperationContract(IsOneWay = true)]
		void ConnectionEvent(ConnectionInfo[] connections);

		// Token: 0x060041AA RID: 16810
		[OperationContract(IsOneWay = true)]
		void Close();
	}
}
