using System;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200009E RID: 158
	[ServiceContract]
	[ECFContract]
	public interface IDataChangeNotification
	{
		// Token: 0x06000585 RID: 1413
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginNotify(string tenant, string dataset, string table, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000586 RID: 1414
		void EndNotify(IAsyncResult result);
	}
}
