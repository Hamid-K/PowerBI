using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Interfaces.SoapProxy;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.SoapProxy
{
	// Token: 0x0200002B RID: 43
	internal sealed class SoapRS2010Proxy : ISoapRS2010Proxy
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000D5DE File Offset: 0x0000B7DE
		internal SoapRS2010Proxy(IPortalConfigurationManager portalConfigurationManager)
		{
			this.portalConfigurationManager = portalConfigurationManager;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000D5ED File Offset: 0x0000B7ED
		private SoapRsManagementConnection CreateRsConnection(IPrincipal userPrincipal)
		{
			return new SoapRsManagementConnection(this.portalConfigurationManager.Current.ReportServerUrl, this.portalConfigurationManager.Current.ReportServerHostName, userPrincipal.Identity);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000D61C File Offset: 0x0000B81C
		public Microsoft.SqlServer.ReportingServices2010.CatalogItem CreateCatalogItem(IPrincipal userPrincipal, string itemType, string name, string parentFolder, bool overwrite, byte[] definition, Microsoft.SqlServer.ReportingServices2010.Property[] properties)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			Warning[] warnings;
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<Microsoft.SqlServer.ReportingServices2010.CatalogItem>(rsConnection2010, userPrincipal, () => rsConnection2010.CreateCatalogItem(itemType, name, parentFolder, overwrite, definition, properties, out warnings));
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D684 File Offset: 0x0000B884
		public void DeleteItem(IPrincipal userPrincipal, string itemPath)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.DeleteItem(itemPath);
			});
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		public void SetDataSource(IPrincipal userPrincipal, string itemPath, DataSourceDefinition dataSourceDefinition)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<int>(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetDataSourceContents(itemPath, dataSourceDefinition);
				return 0;
			});
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D70C File Offset: 0x0000B90C
		public ItemParameter[] GetItemParameters(IPrincipal userPrincipal, string itemPath, string historyId, bool forRendering, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] values, DataSourceCredentials[] credentials)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<ItemParameter[]>(rsConnection2010, userPrincipal, () => rsConnection2010.GetItemParameters(itemPath, historyId, forRendering, values, credentials));
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D76C File Offset: 0x0000B96C
		public Dictionary<string, ReportParameterType> GetParameterTypes(IPrincipal userPrincipal, string reportpath)
		{
			return this.GetItemParameters(userPrincipal, reportpath, null, false, new Microsoft.SqlServer.ReportingServices2010.ParameterValue[0], null).ToDictionary((ItemParameter paramter) => paramter.Name, (ItemParameter paramter) => (ReportParameterType)Enum.Parse(typeof(ReportParameterType), paramter.ParameterTypeName));
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		public void SetItemParamters(IPrincipal userPrincipal, string itemPath, ItemParameter[] parameters)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<int>(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetItemParameters(itemPath, parameters);
				return 0;
			});
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D818 File Offset: 0x0000BA18
		public void SetItemDefinition(IPrincipal userPrincipal, string itemPath, byte[] definition, Microsoft.SqlServer.ReportingServices2010.Property[] properties)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<Warning[]>(rsConnection2010, userPrincipal, () => rsConnection2010.SetItemDefinition(itemPath, definition, properties));
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000D868 File Offset: 0x0000BA68
		public SubscriptionProperties GetSubscriptionProperties(IPrincipal userPrincipal, string id)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<SubscriptionProperties>(rsConnection2010, userPrincipal, delegate
			{
				SubscriptionProperties subscriptionProperties = new SubscriptionProperties();
				subscriptionProperties.Id = id;
				subscriptionProperties.Owner = rsConnection2010.GetSubscriptionProperties(id, out subscriptionProperties.ExtensionSettings, out subscriptionProperties.Description, out subscriptionProperties.Active, out subscriptionProperties.Status, out subscriptionProperties.EventType, out subscriptionProperties.MatchData, out subscriptionProperties.Parameters);
				return subscriptionProperties;
			});
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		public string CreateSubscription(IPrincipal userPrincipal, string itemPath, Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings, string description, string eventType, string matchData, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] parameters)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<string>(rsConnection2010, userPrincipal, () => rsConnection2010.CreateSubscription(itemPath, extensionSettings, description, eventType, matchData, parameters));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000D910 File Offset: 0x0000BB10
		public void DeleteSubscription(IPrincipal userPrincipal, string subscriptionId)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.DeleteSubscription(subscriptionId);
			});
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000D950 File Offset: 0x0000BB50
		public void SetSubscriptionProperties(IPrincipal userPrincipal, string subscriptionId, Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings, string description, string eventType, string matchData, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] parameters)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetSubscriptionProperties(subscriptionId, extensionSettings, description, eventType, matchData, parameters);
			});
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		public void ChangeSubscriptionOwner(IPrincipal userPrincipal, string subscriptionId, string owner)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<bool>(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.ChangeSubscriptionOwner(subscriptionId, owner);
				return true;
			});
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000DA00 File Offset: 0x0000BC00
		public string CreateDataDrivenSubscription(IPrincipal userPrincipal, string itemPath, Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings, Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan dataRetrievalPlan, string description, string eventType, string matchData, ParameterValueOrFieldReference[] parameters)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<string>(rsConnection2010, userPrincipal, () => rsConnection2010.CreateDataDrivenSubscription(itemPath, extensionSettings, dataRetrievalPlan, description, eventType, matchData, parameters));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000DA70 File Offset: 0x0000BC70
		public void SetDataDrivenSubscriptionProperties(IPrincipal userPrincipal, string subscriptionId, Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings, Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan dataRetrievalPlan, string description, string eventType, string matchData, ParameterValueOrFieldReference[] parameters)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetDataDrivenSubscriptionProperties(subscriptionId, extensionSettings, dataRetrievalPlan, description, eventType, matchData, parameters);
			});
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		public DataDrivenSubscriptionProperties GetDataDrivenSubscriptionProperties(IPrincipal userPrincipal, string subscriptionID)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<DataDrivenSubscriptionProperties>(rsConnection2010, userPrincipal, delegate
			{
				DataDrivenSubscriptionProperties dataDrivenSubscriptionProperties = new DataDrivenSubscriptionProperties();
				dataDrivenSubscriptionProperties.Id = subscriptionID;
				dataDrivenSubscriptionProperties.Owner = rsConnection2010.GetDataDrivenSubscriptionProperties(subscriptionID, out dataDrivenSubscriptionProperties.ExtensionSettings, out dataDrivenSubscriptionProperties.DataSettings, out dataDrivenSubscriptionProperties.Description, out dataDrivenSubscriptionProperties.Active, out dataDrivenSubscriptionProperties.Status, out dataDrivenSubscriptionProperties.EventType, out dataDrivenSubscriptionProperties.MatchData, out dataDrivenSubscriptionProperties.Parameters);
				return dataDrivenSubscriptionProperties;
			});
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000DB20 File Offset: 0x0000BD20
		public List<Microsoft.SqlServer.ReportingServices2010.Subscription> GetSubscriptionsUsingDataSource(IPrincipal userPrincipal, string itemPath)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<List<Microsoft.SqlServer.ReportingServices2010.Subscription>>(rsConnection2010, userPrincipal, () => rsConnection2010.ListSubscriptionsUsingDataSource(itemPath).ToList<Microsoft.SqlServer.ReportingServices2010.Subscription>());
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000DB60 File Offset: 0x0000BD60
		public string CreateCacheRefreshPlan(IPrincipal userPrincipal, string itemPath, string description, string eventType, string matchData, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] parameters)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<string>(rsConnection2010, userPrincipal, () => rsConnection2010.CreateCacheRefreshPlan(itemPath, description, eventType, matchData, parameters));
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		public void SetCacheRefreshPlanProperties(IPrincipal userPrincipal, string planId, string description, string eventType, string matchData, Microsoft.SqlServer.ReportingServices2010.ParameterValue[] parameters)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetCacheRefreshPlanProperties(planId, description, eventType, matchData, parameters);
			});
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000DC20 File Offset: 0x0000BE20
		public void DeleteCacheRefreshPlan(IPrincipal userPrincipal, string planId)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.DeleteCacheRefreshPlan(planId);
			});
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000DC60 File Offset: 0x0000BE60
		public void SetItemProperties(IPrincipal userPrincipal, string itemPath, Microsoft.SqlServer.ReportingServices2010.Property[] properties)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<int>(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetProperties(itemPath, properties);
				return 0;
			});
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		public CreateReportEditSessionResult CreateReportEditSession(IPrincipal userPrincipal, string report, string parent, byte[] definition)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<CreateReportEditSessionResult>(rsConnection2010, userPrincipal, () => this.InternalCreateReportEditSession(rsConnection2010, report, parent, definition));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000DD00 File Offset: 0x0000BF00
		public Microsoft.SqlServer.ReportingServices2010.Extension[] ListExtensions(IPrincipal userPrincipal, string extensionType)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<Microsoft.SqlServer.ReportingServices2010.Extension[]>(rsConnection2010, userPrincipal, () => rsConnection2010.ListExtensions(extensionType));
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public Microsoft.SqlServer.ReportingServices2010.ExtensionParameter[] ListExtensionParameters(IPrincipal userPrincipal, string extensionName)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<Microsoft.SqlServer.ReportingServices2010.ExtensionParameter[]>(rsConnection2010, userPrincipal, () => rsConnection2010.GetExtensionSettings(extensionName));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public Microsoft.SqlServer.ReportingServices2010.ExtensionParameter[] ValidateExtensionSettings(IPrincipal userPrincipal, string extensionName, ParameterValueOrFieldReference[] paramValues)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<Microsoft.SqlServer.ReportingServices2010.ExtensionParameter[]>(rsConnection2010, userPrincipal, () => rsConnection2010.ValidateExtensionSettings(extensionName, paramValues, string.Empty));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		public string CreateItemHistorySnapshot(IPrincipal userPrincipal, string itemPath)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			Warning[] warnings = new Warning[0];
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<string>(rsConnection2010, userPrincipal, () => rsConnection2010.CreateItemHistorySnapshot(itemPath, out warnings));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000DE14 File Offset: 0x0000C014
		public void UpdateItemExecutionSnapshot(IPrincipal userPrincipal, string itemPath)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			new Warning[0];
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.UpdateItemExecutionSnapshot(itemPath);
			});
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000DE5C File Offset: 0x0000C05C
		public void SetItemHistoryOptions(IPrincipal userPrincipal, string itemPath, bool enableManualSnapshotCreation, bool keepExecutionSnapshots, ScheduleDefinitionOrReference schedule)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<int>(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetItemHistoryOptions(itemPath, enableManualSnapshotCreation, keepExecutionSnapshots, schedule);
				return 0;
			});
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		public DataSetDefinition PrepareQuery(IPrincipal userPrincipal, Microsoft.SqlServer.ReportingServices2010.DataSource datasource, DataSetDefinition dataSetDefinition)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<DataSetDefinition>(rsConnection2010, userPrincipal, () => this.InternalPrepareQuery(rsConnection2010, datasource, dataSetDefinition));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000DF04 File Offset: 0x0000C104
		public void DeleteSchedule(IPrincipal userPrincipal, string scheduleId)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.DeleteSchedule(scheduleId);
			});
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000DF44 File Offset: 0x0000C144
		public string CreateSchedule(IPrincipal userPrincipal, string name, Microsoft.SqlServer.ReportingServices2010.ScheduleDefinition scheduleDefinition, string site)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			return SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<string>(rsConnection2010, userPrincipal, () => rsConnection2010.CreateSchedule(name, scheduleDefinition, site));
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000DF94 File Offset: 0x0000C194
		public void SetScheduleProperties(IPrincipal userPrincipal, string name, string id, Microsoft.SqlServer.ReportingServices2010.ScheduleDefinition scheduleDefinition)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetScheduleProperties(name, id, scheduleDefinition);
			});
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		public void PauseSchedule(IPrincipal userPrincipal, string id)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.PauseSchedule(id);
			});
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E024 File Offset: 0x0000C224
		public void ResumeSchedule(IPrincipal userPrincipal, string id)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.ResumeSchedule(id);
			});
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000E064 File Offset: 0x0000C264
		public void SetCacheOptions(IPrincipal userPrincipal, string itemPath, bool cacheItem, ExpirationDefinition cacheExpiration)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetCacheOptions(itemPath, cacheItem, cacheExpiration);
			});
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		public void SetExecutionOptions(IPrincipal userPrincipal, string itemPath, string executionSetting, ScheduleDefinitionOrReference schedule)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism(rsConnection2010, userPrincipal, delegate
			{
				rsConnection2010.SetExecutionOptions(itemPath, executionSetting, schedule);
			});
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000E104 File Offset: 0x0000C304
		public bool TestConnectForDataSourceDefinition(IPrincipal userPrincipal, DataSourceDefinition dataSourceDefinition, string userName, string password, out string connectError)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			string error = null;
			connectError = null;
			bool flag = SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<bool>(rsConnection2010, userPrincipal, () => rsConnection2010.TestConnectForDataSourceDefinition(dataSourceDefinition, userName, password, out error));
			if (!flag)
			{
				connectError = error;
			}
			return flag;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000E16C File Offset: 0x0000C36C
		public bool TestConnectForItemDataSource(IPrincipal userPrincipal, string itemPath, string dataSourceName, string userName, string password, out string connectError)
		{
			SoapRsManagementConnection rsConnection2010 = this.CreateRsConnection(userPrincipal);
			string error = null;
			connectError = null;
			bool flag = SoapAuthenticationHelper.ExecuteWithCorrespondingAuthMechanism<bool>(rsConnection2010, userPrincipal, () => rsConnection2010.TestConnectForItemDataSource(itemPath, dataSourceName, userName, password, out error));
			if (!flag)
			{
				connectError = error;
			}
			return flag;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		private CreateReportEditSessionResult InternalCreateReportEditSession(SoapRsManagementConnection rsConnection2010, string report, string parent, byte[] definition)
		{
			Warning[] array = new Warning[0];
			string text = rsConnection2010.CreateReportEditSession(report, parent, definition, out array);
			return new CreateReportEditSessionResult
			{
				Report = text,
				Warnings = array
			};
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000E210 File Offset: 0x0000C410
		private DataSetDefinition InternalPrepareQuery(SoapRsManagementConnection rsConnection2010, Microsoft.SqlServer.ReportingServices2010.DataSource dataSource, DataSetDefinition dataSet)
		{
			string[] array = new string[0];
			bool flag;
			return rsConnection2010.PrepareQuery(dataSource, dataSet, out flag, out array);
		}

		// Token: 0x04000093 RID: 147
		private readonly IPortalConfigurationManager portalConfigurationManager;
	}
}
