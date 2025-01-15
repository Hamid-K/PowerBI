using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Interfaces.SoapProxy
{
	// Token: 0x02000087 RID: 135
	public interface ISoapRS2010Proxy
	{
		// Token: 0x06000419 RID: 1049
		Microsoft.SqlServer.ReportingServices2010.CatalogItem CreateCatalogItem(IPrincipal userPrincipal, string itemType, string name, string parentFolder, bool overwrite, byte[] definition, Microsoft.SqlServer.ReportingServices2010.Property[] properties);

		// Token: 0x0600041A RID: 1050
		void DeleteItem(IPrincipal userPrincipal, string itemPath);

		// Token: 0x0600041B RID: 1051
		ItemParameter[] GetItemParameters(IPrincipal userPrincipal, string itemPath, string historyId, bool forRendering, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] values, DataSourceCredentials[] credentials);

		// Token: 0x0600041C RID: 1052
		Dictionary<string, ReportParameterType> GetParameterTypes(IPrincipal userPrincipal, string reportpath);

		// Token: 0x0600041D RID: 1053
		void SetItemParamters(IPrincipal userPrincipal, string itemPath, ItemParameter[] parameters);

		// Token: 0x0600041E RID: 1054
		void SetItemDefinition(IPrincipal userPrincipal, string itemPath, byte[] definition, Microsoft.SqlServer.ReportingServices2010.Property[] properties);

		// Token: 0x0600041F RID: 1055
		CreateReportEditSessionResult CreateReportEditSession(IPrincipal userPrincipal, string report, string parent, byte[] definition);

		// Token: 0x06000420 RID: 1056
		SubscriptionProperties GetSubscriptionProperties(IPrincipal userPrincipal, string id);

		// Token: 0x06000421 RID: 1057
		string CreateSubscription(IPrincipal userPrincipal, string itemPath, Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings, string description, string eventType, string matchData, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] parameters);

		// Token: 0x06000422 RID: 1058
		void DeleteSubscription(IPrincipal userPrincipal, string subscriptionId);

		// Token: 0x06000423 RID: 1059
		void ChangeSubscriptionOwner(IPrincipal userPrincipal, string subscriptionId, string owner);

		// Token: 0x06000424 RID: 1060
		void SetSubscriptionProperties(IPrincipal userPrincipal, string subscriptionId, Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings, string description, string eventType, string matchData, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] parameters);

		// Token: 0x06000425 RID: 1061
		string CreateDataDrivenSubscription(IPrincipal userPrincipal, string itemPath, Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings, Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan dataRetrievalPlan, string description, string eventType, string matchData, ParameterValueOrFieldReference[] parameters);

		// Token: 0x06000426 RID: 1062
		void SetDataDrivenSubscriptionProperties(IPrincipal userPrincipal, string subscriptionId, Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings, Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan dataRetrievalPlan, string description, string eventType, string matchData, ParameterValueOrFieldReference[] parameters);

		// Token: 0x06000427 RID: 1063
		DataDrivenSubscriptionProperties GetDataDrivenSubscriptionProperties(IPrincipal userPrincipal, string subscriptionID);

		// Token: 0x06000428 RID: 1064
		List<Microsoft.SqlServer.ReportingServices2010.Subscription> GetSubscriptionsUsingDataSource(IPrincipal userPrincipal, string itemPath);

		// Token: 0x06000429 RID: 1065
		Microsoft.SqlServer.ReportingServices2010.Extension[] ListExtensions(IPrincipal userPrincipal, string extensionType);

		// Token: 0x0600042A RID: 1066
		Microsoft.SqlServer.ReportingServices2010.ExtensionParameter[] ListExtensionParameters(IPrincipal userPrincipal, string extensionName);

		// Token: 0x0600042B RID: 1067
		Microsoft.SqlServer.ReportingServices2010.ExtensionParameter[] ValidateExtensionSettings(IPrincipal userPrincipal, string extensionName, ParameterValueOrFieldReference[] paramValues);

		// Token: 0x0600042C RID: 1068
		string CreateItemHistorySnapshot(IPrincipal userPrincipal, string itemPath);

		// Token: 0x0600042D RID: 1069
		void UpdateItemExecutionSnapshot(IPrincipal userPrincipal, string itemPath);

		// Token: 0x0600042E RID: 1070
		void SetItemHistoryOptions(IPrincipal userPrincipal, string itemPath, bool enableManualSnapshotCreation, bool keepExecutionSnapshots, ScheduleDefinitionOrReference schedule);

		// Token: 0x0600042F RID: 1071
		DataSetDefinition PrepareQuery(IPrincipal userPrincipal, Microsoft.SqlServer.ReportingServices2010.DataSource datasource, DataSetDefinition dataSetDefinition);

		// Token: 0x06000430 RID: 1072
		void DeleteSchedule(IPrincipal userPrincipal, string scheduleId);

		// Token: 0x06000431 RID: 1073
		string CreateSchedule(IPrincipal userPrincipal, string name, Microsoft.SqlServer.ReportingServices2010.ScheduleDefinition scheduleDefinition, string site);

		// Token: 0x06000432 RID: 1074
		void SetScheduleProperties(IPrincipal userPrincipal, string name, string id, Microsoft.SqlServer.ReportingServices2010.ScheduleDefinition scheduleDefinition);

		// Token: 0x06000433 RID: 1075
		void PauseSchedule(IPrincipal userPrincipal, string id);

		// Token: 0x06000434 RID: 1076
		void ResumeSchedule(IPrincipal userPrincipal, string id);

		// Token: 0x06000435 RID: 1077
		void SetExecutionOptions(IPrincipal userPrincipal, string itemPath, string executionSetting, ScheduleDefinitionOrReference schedule);

		// Token: 0x06000436 RID: 1078
		void SetCacheOptions(IPrincipal userPrincipal, string itemPath, bool cacheItem, ExpirationDefinition cacheExpiration);

		// Token: 0x06000437 RID: 1079
		string CreateCacheRefreshPlan(IPrincipal userPrincipal, string itemPath, string description, string eventType, string matchData, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] parameters);

		// Token: 0x06000438 RID: 1080
		void SetCacheRefreshPlanProperties(IPrincipal userPrincipal, string planId, string description, string eventType, string matchData, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] parameters);

		// Token: 0x06000439 RID: 1081
		void DeleteCacheRefreshPlan(IPrincipal userPrincipal, string planId);

		// Token: 0x0600043A RID: 1082
		bool TestConnectForDataSourceDefinition(IPrincipal userPrincipal, DataSourceDefinition dataSourceDefinition, string userName, string password, out string connectError);

		// Token: 0x0600043B RID: 1083
		bool TestConnectForItemDataSource(IPrincipal userPrincipal, string ItemPath, string DataSourceName, string UserName, string Password, out string ConnectError);
	}
}
