using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.AnalysisServices.Azure.Common.DataContracts;
using Microsoft.ASAzure.ASClusterManagementClient;
using Microsoft.Cloud.Platform.Azure.WindowsFabric;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.PowerBI.ContentProviders;

namespace Microsoft.AnalysisServices.Azure.Gateway
{
	// Token: 0x0200002B RID: 43
	[ServiceContract]
	[ECFContract(IsExternal = true, FlattenHierarchy = true)]
	public interface IClusterManagementService : IClusterManagementService
	{
		// Token: 0x060002BD RID: 701
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/wow/virtualservers/{vsName}/databases/{dbName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginCreatePowerBIDatabase(string dbName, string vsName, DatabaseType? databaseType, DatabaseSource databaseSource, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002BE RID: 702
		BindDatabaseResult EndCreatePowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x060002BF RID: 703
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/wow/virtualservers/{vsName}/sourcedatabase/{sourceDBName}/targetdatabase/{targetDBName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginClonePowerBIDatabase(string sourceDBName, string targetDBName, string vsName, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002C0 RID: 704
		BindDatabaseResult EndClonePowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x060002C1 RID: 705
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "DELETE", UriTemplate = "/wow/virtualservers/{vsName}/databases/{dbName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginDeletePowerBIDatabase(string dbName, string vsName, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002C2 RID: 706
		void EndDeletePowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x060002C3 RID: 707
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/powerbi/models/abf/{dbName}?createdBy={createdBy}&initialLoadInMB={initialLoadInMB}&shouldProcess={shouldProcess}&userPuid={userPuid}&tenantId={tenantId}&isTabularModel={isTabularModel}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginProvisionAndPopulateDatabaseWithAbf(string dbName, string createdBy, int initialLoadInMB, byte[] abfContent, bool shouldProcess, string userPuid, string tenantId, IEnumerable<DataSourceMapping> dataSources, DatabaseType? databaseType, bool isTabularModel, AsyncCallback callback, object context);

		// Token: 0x060002C4 RID: 708
		BindDatabaseResult EndProvisionAndPopulateDatabaseWithAbf(IAsyncResult result);

		// Token: 0x060002C5 RID: 709
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/powerbi/models/xmla/{dbName}?createdBy={createdBy}&initialLoadInMB={initialLoadInMB}&shouldProcess={shouldProcess}&userPuid={userPuid}&tenantId={tenantId}&isTabularModel={isTabularModel}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginProvisionAndPopulateDatabaseWithXmla(string dbName, string createdBy, int initialLoadInMB, string xmlaSchema, bool shouldProcess, string userPuid, string tenantId, IEnumerable<DataSourceMapping> dataSources, DatabaseType? databaseType, bool isTabularModel, AsyncCallback callback, object context);

		// Token: 0x060002C6 RID: 710
		BindDatabaseResult EndProvisionAndPopulateDatabaseWithXmla(IAsyncResult result);

		// Token: 0x060002C7 RID: 711
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/powerbi/models/tom/{dbName}?createdBy={createdBy}&initialLoadInMB={initialLoadInMB}&shouldProcess={shouldProcess}&userPuid={userPuid}&tenantId={tenantId}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginProvisionAndPopulateDatabaseWithTom(string dbName, string createdBy, int initialLoadInMB, string tabularJsonSchema, bool shouldProcess, string userPuid, string tenantId, IEnumerable<DataSourceMapping> dataSources, DatabaseType? databaseType, AsyncCallback callback, object context);

		// Token: 0x060002C8 RID: 712
		BindDatabaseResult EndProvisionAndPopulateDatabaseWithTom(IAsyncResult result);

		// Token: 0x060002C9 RID: 713
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/wow/virtualservers/{vsName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginCreatePowerBIVirtualServer(string vsName, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002CA RID: 714
		string EndCreatePowerBIVirtualServer(IAsyncResult asyncResult);

		// Token: 0x060002CB RID: 715
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "DELETE", UriTemplate = "/wow/virtualservers/{vsName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginDeletePowerBIVirtualServer(string vsName, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002CC RID: 716
		void EndDeletePowerBIVirtualServer(IAsyncResult asyncResult);

		// Token: 0x060002CD RID: 717
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/powerbi/modelinfo/virtualservers/{vsName}/databases/{dbName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetPowerBIDatabaseModelInfo(string dbName, string vsName, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002CE RID: 718
		ModelInfo EndGetPowerBIDatabaseModelInfo(IAsyncResult asyncResult);

		// Token: 0x060002CF RID: 719
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/powerbi/resolve/virtualservers/{vsName}/databases/{dbName}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginResolvePowerBIDatabase(string dbName, string vsName, WindowsFabricEndpoint prevEndpoint, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002D0 RID: 720
		ExtendedAnalyticsServiceResolveResult EndResolvePowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x060002D1 RID: 721
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/powerbi/resolve/winfabservice/{serviceType}/{resolvedEndoint}/{targetEndpoint}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginResolvePowerBIService(string serviceType, string resolvedEndoint, string targetEndpoint, string[] knownEndpoints, string[] keys, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002D2 RID: 722
		EndpointIdentifier[] EndResolvePowerBIService(IAsyncResult asyncResult);

		// Token: 0x060002D3 RID: 723
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/powerbi/{vsName}/authorityid", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginGetAuthorityId(string vsName, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002D4 RID: 724
		string EndGetAuthorityId(IAsyncResult asyncResult);

		// Token: 0x060002D5 RID: 725
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "GET", UriTemplate = "/anping", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginANPing(AsyncCallback callback, object context);

		// Token: 0x060002D6 RID: 726
		bool EndANPing(IAsyncResult asyncResult);

		// Token: 0x060002D7 RID: 727
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/wow/evict/virtualservers/{vsName}/databases/{dbName}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginEvictPowerBIDatabase(string dbName, string vsName, AsyncCallback callback, object context);

		// Token: 0x060002D8 RID: 728
		Uri EndEvictPowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x060002D9 RID: 729
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/wow/resurrect/virtualservers/{vsName}/databases/{dbName}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginResurrectPowerBIDatabase(string dbName, string vsName, bool forceResurrect, AsyncCallback callback, object context);

		// Token: 0x060002DA RID: 730
		ServiceEntity EndResurrectPowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x060002DB RID: 731
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/wow/process/virtualservers/{vsName}/databases/{dbName}?userPuid={userPuid}&tenantId={tenantId}", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginProcessPowerBIDatabase(string dbName, string vsName, string userPuid, string tenantId, AsyncCallback callback, object context);

		// Token: 0x060002DC RID: 732
		void EndProcessPowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x060002DD RID: 733
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/wow/process/powerBIprocessInfo", ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginProcessPowerBIDatabaseWithProcessInfo(PowerBIProcessDatabaseInfo processInfo, AsyncCallback callback, object context);

		// Token: 0x060002DE RID: 734
		void EndProcessPowerBIDatabaseWithProcessInfo(IAsyncResult asyncResult);

		// Token: 0x060002DF RID: 735
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "PUT", UriTemplate = "/wow/sync/state", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginPopulateStateFromOperationalStore(AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002E0 RID: 736
		void EndPopulateStateFromOperationalStore(IAsyncResult asyncResult);

		// Token: 0x060002E1 RID: 737
		[OperationContract(AsyncPattern = true)]
		[WebInvoke(Method = "DELETE", UriTemplate = "/wow/clear/kvsstate", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		IAsyncResult BeginClearKeyValueStoreState(AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x060002E2 RID: 738
		void EndClearKeyValueStoreState(IAsyncResult asyncResult);
	}
}
