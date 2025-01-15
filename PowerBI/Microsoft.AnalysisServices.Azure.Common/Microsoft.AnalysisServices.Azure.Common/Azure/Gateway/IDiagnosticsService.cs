using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.AnalysisServices.Azure.Common.DataContracts;
using Microsoft.Cloud.Platform.Azure.WindowsFabric;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Gateway
{
	// Token: 0x0200002C RID: 44
	[ServiceContract]
	[ECFContract(IsExternal = true)]
	public interface IDiagnosticsService
	{
		// Token: 0x060002E3 RID: 739
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/ping", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginPing(AsyncCallback callback, object context);

		// Token: 0x060002E4 RID: 740
		bool EndPing(IAsyncResult asyncResult);

		// Token: 0x060002E5 RID: 741
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getdbentity/{virtualServerName}/{databaseName}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetDatabaseEntity(string virtualServerName, string databaseName, AsyncCallback callback, object context);

		// Token: 0x060002E6 RID: 742
		DatabaseEntity EndGetDatabaseEntity(IAsyncResult asyncResult);

		// Token: 0x060002E7 RID: 743
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getboundservice/{virtualServerName}/{databaseName}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetBoundService(string virtualServerName, string databaseName, AsyncCallback callback, object context);

		// Token: 0x060002E8 RID: 744
		ServiceEntity EndGetBoundService(IAsyncResult asyncResult);

		// Token: 0x060002E9 RID: 745
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getdatabaseendpoint/{virtualServerName}/{databaseName}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetDatabaseEndpoint(string virtualServerName, string databaseName, AsyncCallback callback, object context);

		// Token: 0x060002EA RID: 746
		WindowsFabricEndpoint EndGetDatabaseEndpoint(IAsyncResult asyncResult);

		// Token: 0x060002EB RID: 747
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getentitiessoftype/{type}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetEntitiesOfType(string type, AsyncCallback callback, object context);

		// Token: 0x060002EC RID: 748
		PersistableEntityContainer EndGetEntitiesOfType(IAsyncResult asyncResult);

		// Token: 0x060002ED RID: 749
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getentitybykey/?key={key}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetEntityFullInformationByKey(string key, AsyncCallback callback, object context);

		// Token: 0x060002EE RID: 750
		PersistableEntityFullInformation EndGetEntityFullInformationByKey(IAsyncResult asyncResult);

		// Token: 0x060002EF RID: 751
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getbounddatabase/?uri={serviceuri}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetBoundDatabase(string serviceuri, AsyncCallback callback, object context);

		// Token: 0x060002F0 RID: 752
		DatabaseEntity EndGetBoundDatabase(IAsyncResult asyncResult);

		// Token: 0x060002F1 RID: 753
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getservicemetrics", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetServiceMetrics(AsyncCallback callback, object context);

		// Token: 0x060002F2 RID: 754
		AggregatedServiceMetrics[] EndGetServiceMetrics(IAsyncResult asyncResult);

		// Token: 0x060002F3 RID: 755
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getclustermetrics", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetClusterMetrics(AsyncCallback callback, object context);

		// Token: 0x060002F4 RID: 756
		IDictionary<string, AggregatedClusterMetrics> EndGetClusterMetrics(IAsyncResult asyncResult);

		// Token: 0x060002F5 RID: 757
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/restoreDb/{vsName}/databases/{dbName}?type={type}&initialLoadInMB={initialLoadInMB}&containerId={containerId}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginRestorePowerBIDatabase(string dbName, string vsName, DatabaseType type, int initialLoadInMB, string containerId, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002F6 RID: 758
		BindDatabaseResult EndRestorePowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x060002F7 RID: 759
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getpersistableitemcounts", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetPersistableItemCounts(AsyncCallback callback, object context);

		// Token: 0x060002F8 RID: 760
		Dictionary<PersistableItemTypes, int> EndGetPersistableItemCounts(IAsyncResult asyncResult);

		// Token: 0x060002F9 RID: 761
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/getdatabasecount", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetDatabaseCount(AsyncCallback callback, object context);

		// Token: 0x060002FA RID: 762
		int EndGetDatabaseCount(IAsyncResult asyncResult);

		// Token: 0x060002FB RID: 763
		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "/enableengineevents/?tracedef={traceDefXml}", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Json)]
		Task EnableEngineEventsAsync(string traceDefXml);

		// Token: 0x060002FC RID: 764
		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "/disableengineevents", ResponseFormat = WebMessageFormat.Json)]
		Task DisableEngineEventsAsync();

		// Token: 0x060002FD RID: 765
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "DELETE", UriTemplate = "/unbinddatabase/{virtualServerName}/{databaseName}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginUnbindDatabase(string virtualServerName, string databaseName, AsyncCallback callback, object context);

		// Token: 0x060002FE RID: 766
		void EndUnbindDatabase(IAsyncResult asyncResult);

		// Token: 0x060002FF RID: 767
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "DELETE", UriTemplate = "/deleteserviceentity/{serviceEntity}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginDeleteServiceEntity(string serviceEntity, AsyncCallback callback, object context);

		// Token: 0x06000300 RID: 768
		void EndDeleteServiceEntity(IAsyncResult asyncResult);
	}
}
