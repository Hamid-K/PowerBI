using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000AB RID: 171
	[ServiceContract]
	[ECFContract(IsExternal = true)]
	public interface IXmlaService
	{
		// Token: 0x0600060B RID: 1547
		[OperationContract(AsyncPattern = false)]
		[WebInvoke(Method = "GET", UriTemplate = "/ping", ResponseFormat = WebMessageFormat.Xml)]
		void Ping();
	}
}
