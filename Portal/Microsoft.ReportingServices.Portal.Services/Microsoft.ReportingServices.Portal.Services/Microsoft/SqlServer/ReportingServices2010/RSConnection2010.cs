using System;
using System.Net;
using System.Web.Services.Protocols;
using Microsoft.SqlServer.ReportingServices;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000073 RID: 115
	internal class RSConnection2010 : ReportingService2010
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000380 RID: 896 RVA: 0x00015B8C File Offset: 0x00013D8C
		// (remove) Token: 0x06000381 RID: 897 RVA: 0x00015BC4 File Offset: 0x00013DC4
		internal event RSConnection2010.BeforeExecutionEventHandler BeforeExecution;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000382 RID: 898 RVA: 0x00015BFC File Offset: 0x00013DFC
		// (remove) Token: 0x06000383 RID: 899 RVA: 0x00015C34 File Offset: 0x00013E34
		internal event RSConnection2010.ExceptionEncounteredEventHandler ExceptionEncountered;

		// Token: 0x06000384 RID: 900 RVA: 0x00015C69 File Offset: 0x00013E69
		public RSConnection2010(string reportServerUrl)
			: this(reportServerUrl, false)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00015C73 File Offset: 0x00013E73
		public RSConnection2010(string reportServerUrl, bool useSharePointProxy)
		{
			this.m_useSharePointProxy = useSharePointProxy;
			this.InitializeReportServerUrl(reportServerUrl);
			base.Timeout = -1;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00015C90 File Offset: 0x00013E90
		public string UrlForQuery
		{
			get
			{
				this.SetConnectionProtocol();
				return base.Url;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00015CA0 File Offset: 0x00013EA0
		protected void InitializeReportServerUrl(string reportServerUrl)
		{
			if (reportServerUrl == null)
			{
				return;
			}
			UriBuilder uriBuilder = new UriBuilder(reportServerUrl);
			if (string.Compare(uriBuilder.Scheme, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_nonSecureServerUrl = null;
				this.m_secureServerUrl = uriBuilder.Uri.AbsoluteUri;
				this.UseSecureConnection = true;
			}
			else
			{
				this.m_nonSecureServerUrl = uriBuilder.Uri.AbsoluteUri;
				uriBuilder.Port = -1;
				uriBuilder.Scheme = Uri.UriSchemeHttps;
				this.m_secureServerUrl = uriBuilder.Uri.AbsoluteUri;
				this.UseSecureConnection = false;
			}
			this.m_checkedServerConfiguration = false;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00015D30 File Offset: 0x00013F30
		private void SetConnectionProtocol()
		{
			if (!this.UseSecureConnection && !this.m_checkedServerConfiguration)
			{
				try
				{
					this.UseSecureConnection = base.IsSSLRequired();
				}
				catch (Exception ex)
				{
					this.UseSecureConnection = true;
					try
					{
						base.IsSSLRequired();
					}
					catch
					{
						this.UseSecureConnection = false;
						WebException ex2 = ex as WebException;
						if (ex2 != null)
						{
							HttpWebResponse httpWebResponse = ex2.Response as HttpWebResponse;
							if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.Forbidden)
							{
								throw;
							}
						}
						throw ex;
					}
				}
				this.m_checkedServerConfiguration = true;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00015DC4 File Offset: 0x00013FC4
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00015DCC File Offset: 0x00013FCC
		public bool UseSecureConnection
		{
			get
			{
				return this.m_useSecureConnection;
			}
			private set
			{
				this.m_useSecureConnection = value;
				string text = (this.m_useSecureConnection ? this.m_secureServerUrl : this.m_nonSecureServerUrl);
				if (this.m_useSharePointProxy)
				{
					base.Url = text + "/_vti_bin/ReportServer/ReportService2010.asmx";
					return;
				}
				base.Url = text + "/ReportService2010.asmx";
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00015E22 File Offset: 0x00014022
		public string ServerEditionString
		{
			get
			{
				if (base.ServerInfoHeaderValue == null || base.ServerInfoHeaderValue.ReportServerEdition == null)
				{
					this.ListItemTypes();
				}
				return base.ServerInfoHeaderValue.ReportServerEdition;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00015E4C File Offset: 0x0001404C
		public new string GetItemType(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<string>(this, "GetItemType", () => this.GetItemType(itemPath)).ExecuteMethod();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00015E8C File Offset: 0x0001408C
		public new void DeleteItem(string itemPath)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "DeleteItem", delegate
			{
				this.DeleteItem(itemPath);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00015ECC File Offset: 0x000140CC
		public new void MoveItem(string itemPath, string target)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "MoveItem", delegate
			{
				this.MoveItem(itemPath, target);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00015F14 File Offset: 0x00014114
		public new CatalogItem[] ListParents(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<CatalogItem[]>(this, "ListParents", () => this.ListParents(itemPath)).ExecuteMethod();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00015F54 File Offset: 0x00014154
		public new CatalogItem[] ListChildren(string itemPath, bool recursive)
		{
			return new RSConnection2010.SoapMethodWrapper<CatalogItem[]>(this, "ListChildren", () => this.ListChildren(itemPath, recursive)).ExecuteMethod();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00015F98 File Offset: 0x00014198
		public new CatalogItem[] ListDependentItems(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<CatalogItem[]>(this, "ListDependentItems", () => this.ListDependentItems(itemPath)).ExecuteMethod();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00015FD8 File Offset: 0x000141D8
		public new Property[] GetProperties(string itemPath, Property[] properties)
		{
			return new RSConnection2010.SoapMethodWrapper<Property[]>(this, "GetProperties", () => this.GetProperties(itemPath, properties)).ExecuteMethod();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001601C File Offset: 0x0001421C
		public new void SetProperties(string itemPath, Property[] properties)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetProperties", delegate
			{
				this.SetProperties(itemPath, properties);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00016064 File Offset: 0x00014264
		public new void CreateRole(string name, string description, string[] taskIds)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "CreateRole", delegate
			{
				this.CreateRole(name, description, taskIds);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000160B0 File Offset: 0x000142B0
		public new void DeleteRole(string name)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "DeleteRole", delegate
			{
				this.DeleteRole(name);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000160F0 File Offset: 0x000142F0
		public new Role[] ListRoles(string scope, string site)
		{
			return new RSConnection2010.SoapMethodWrapper<Role[]>(this, "ListRoles", () => this.ListRoles(scope, site)).ExecuteMethod();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00016134 File Offset: 0x00014334
		public new string[] GetRoleProperties(string name, string site, out string description)
		{
			string outDescription = null;
			string[] array = new RSConnection2010.SoapMethodWrapper<string[]>(this, "GetRoleProperties", () => this.GetRoleProperties(name, site, out outDescription)).ExecuteMethod();
			description = outDescription;
			return array;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00016188 File Offset: 0x00014388
		public new void SetRoleProperties(string name, string description, string[] taskIds)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetRoleProperties", delegate
			{
				this.SetRoleProperties(name, description, taskIds);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000161D4 File Offset: 0x000143D4
		public new Task[] ListTasks(string scope)
		{
			return new RSConnection2010.SoapMethodWrapper<Task[]>(this, "ListTasks", () => this.ListTasks(scope)).ExecuteMethod();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00016214 File Offset: 0x00014414
		public new CatalogItem[] FindItems(string folder, BooleanOperatorEnum booleanOperator, Property[] searchOptions, SearchCondition[] conditions)
		{
			return new RSConnection2010.SoapMethodWrapper<CatalogItem[]>(this, "FindItems", () => this.FindItems(folder, booleanOperator, searchOptions, conditions)).ExecuteMethod();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00016267 File Offset: 0x00014467
		public new IAsyncResult BeginFindItems(string folder, BooleanOperatorEnum booleanOperator, Property[] searchOptions, SearchCondition[] conditions, AsyncCallback callback, object asyncState)
		{
			return base.BeginFindItems(folder, booleanOperator, searchOptions, conditions, callback, asyncState);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00016278 File Offset: 0x00014478
		public new CatalogItem[] EndFindItems(IAsyncResult asyncResult)
		{
			return new RSConnection2010.SoapMethodWrapper<CatalogItem[]>(this, "EndFindItems", () => this.EndFindItems(asyncResult)).ExecuteMethod();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000162B8 File Offset: 0x000144B8
		public new Policy[] GetPolicies(string itemPath, out bool inheritParent)
		{
			bool outInheritParent = false;
			Policy[] array = new RSConnection2010.SoapMethodWrapper<Policy[]>(this, "GetPolicies", () => this.GetPolicies(itemPath, out outInheritParent)).ExecuteMethod();
			inheritParent = outInheritParent;
			return array;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00016304 File Offset: 0x00014504
		public new void SetPolicies(string itemPath, Policy[] policies)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetPolicies", delegate
			{
				this.SetPolicies(itemPath, policies);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00016349 File Offset: 0x00014549
		public new Policy[] GetSystemPolicies()
		{
			return new RSConnection2010.SoapMethodWrapper<Policy[]>(this, "GetSystemPolicies", () => base.GetSystemPolicies()).ExecuteMethod();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00016368 File Offset: 0x00014568
		public new void SetSystemPolicies(Policy[] policies)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetSystemPolicies", delegate
			{
				this.SetSystemPolicies(policies);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000163A8 File Offset: 0x000145A8
		public new void InheritParentSecurity(string itemPath)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "InheritParentSecurity", delegate
			{
				this.InheritParentSecurity(itemPath);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000163E8 File Offset: 0x000145E8
		public new string[] GetPermissions(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<string[]>(this, "GetPermissions", () => this.GetPermissions(itemPath)).ExecuteMethod();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00016425 File Offset: 0x00014625
		public new string[] GetSystemPermissions()
		{
			return new RSConnection2010.SoapMethodWrapper<string[]>(this, "GetSystemPermissions", () => base.GetSystemPermissions()).ExecuteMethod();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00016444 File Offset: 0x00014644
		public new string GetItemLink(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<string>(this, "GetItemLink", () => this.GetItemLink(itemPath)).ExecuteMethod();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00016484 File Offset: 0x00014684
		public new void SetItemLink(string itemPath, string link)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetItemLink", delegate
			{
				this.SetItemLink(itemPath, link);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000164CC File Offset: 0x000146CC
		public new Subscription[] ListSubscriptions(string itemPathOrSiteUrl)
		{
			return new RSConnection2010.SoapMethodWrapper<Subscription[]>(this, "ListSubscriptions", () => this.ListSubscriptions(itemPathOrSiteUrl)).ExecuteMethod();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001650C File Offset: 0x0001470C
		public new void ChangeSubscriptionOwner(string subscriptionId, string newOwner)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "ChangeSubscriptionOwner", delegate
			{
				this.ChangeSubscriptionOwner(subscriptionId, newOwner);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00016554 File Offset: 0x00014754
		public new void DisableSubscription(string subscriptionId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "DisableSubscription", delegate
			{
				this.DisableSubscription(subscriptionId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00016594 File Offset: 0x00014794
		public new void EnableSubscription(string subscriptionId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "EnableSubscription", delegate
			{
				this.EnableSubscription(subscriptionId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000165D4 File Offset: 0x000147D4
		public new void DeleteSubscription(string subscriptionId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "DeleteSubscription", delegate
			{
				this.DeleteSubscription(subscriptionId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00016614 File Offset: 0x00014814
		public new Subscription[] ListSubscriptionsUsingDataSource(string dataSource)
		{
			return new RSConnection2010.SoapMethodWrapper<Subscription[]>(this, "ListSubscriptionsUsingDataSource", () => this.ListSubscriptionsUsingDataSource(dataSource)).ExecuteMethod();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00016654 File Offset: 0x00014854
		public new string CreateSubscription(string itemPath, ExtensionSettings extensionSettings, string description, string eventType, string matchData, ParameterValue[] parameters)
		{
			return new RSConnection2010.SoapMethodWrapper<string>(this, "CreateSubscription", () => this.CreateSubscription(itemPath, extensionSettings, description, eventType, matchData, parameters)).ExecuteMethod();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000166B8 File Offset: 0x000148B8
		public new string CreateDataDrivenSubscription(string itemPath, ExtensionSettings extensionSettings, DataRetrievalPlan dataRetrievalPlan, string description, string eventType, string matchData, ParameterValueOrFieldReference[] parameters)
		{
			return new RSConnection2010.SoapMethodWrapper<string>(this, "CreateDataDrivenSubscription", () => this.CreateDataDrivenSubscription(itemPath, extensionSettings, dataRetrievalPlan, description, eventType, matchData, parameters)).ExecuteMethod();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00016724 File Offset: 0x00014924
		public new string GetSubscriptionProperties(string subscriptionID, out ExtensionSettings extensionSettings, out string description, out ActiveState active, out string status, out string eventType, out string matchData, out ParameterValue[] parameters)
		{
			ExtensionSettings outExtensionSettings = null;
			string outDescription = null;
			ActiveState outActive = null;
			string outStatus = null;
			string outEventType = null;
			string outMatchData = null;
			ParameterValue[] outParameters = null;
			string text = new RSConnection2010.SoapMethodWrapper<string>(this, "GetSubscriptionProperties", () => this.GetSubscriptionProperties(subscriptionID, out outExtensionSettings, out outDescription, out outActive, out outStatus, out outEventType, out outMatchData, out outParameters)).ExecuteMethod();
			extensionSettings = outExtensionSettings;
			description = outDescription;
			active = outActive;
			status = outStatus;
			eventType = outEventType;
			matchData = outMatchData;
			parameters = outParameters;
			return text;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000167D0 File Offset: 0x000149D0
		public new string GetDataDrivenSubscriptionProperties(string subscriptionID, out ExtensionSettings extensionSettings, out DataRetrievalPlan dataRetrievalPlan, out string description, out ActiveState active, out string status, out string eventType, out string matchData, out ParameterValueOrFieldReference[] parameters)
		{
			ExtensionSettings outExtensionSettings = null;
			DataRetrievalPlan outDataRetrievalPlan = null;
			string outDescription = null;
			ActiveState outActive = null;
			string outStatus = null;
			string outEventType = null;
			string outMatchData = null;
			ParameterValueOrFieldReference[] outParameters = null;
			string text = new RSConnection2010.SoapMethodWrapper<string>(this, "GetDataDrivenSubscriptionProperties", () => this.GetDataDrivenSubscriptionProperties(subscriptionID, out outExtensionSettings, out outDataRetrievalPlan, out outDescription, out outActive, out outStatus, out outEventType, out outMatchData, out outParameters)).ExecuteMethod();
			extensionSettings = outExtensionSettings;
			dataRetrievalPlan = outDataRetrievalPlan;
			description = outDescription;
			active = outActive;
			status = outStatus;
			eventType = outEventType;
			matchData = outMatchData;
			parameters = outParameters;
			return text;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001688C File Offset: 0x00014A8C
		public new void SetSubscriptionProperties(string subscriptionId, ExtensionSettings extensionSettings, string description, string eventType, string matchData, ParameterValue[] parameters)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetSubscriptionProperties", delegate
			{
				this.SetSubscriptionProperties(subscriptionId, extensionSettings, description, eventType, matchData, parameters);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000168F0 File Offset: 0x00014AF0
		public new void SetDataDrivenSubscriptionProperties(string subscriptionId, ExtensionSettings extensionSettings, DataRetrievalPlan dataRetrievalPlan, string description, string eventType, string matchData, ParameterValueOrFieldReference[] parameters)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetDataDrivenSubscriptionProperties", delegate
			{
				this.SetDataDrivenSubscriptionProperties(subscriptionId, extensionSettings, dataRetrievalPlan, description, eventType, matchData, parameters);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001695C File Offset: 0x00014B5C
		public new DataSetDefinition PrepareQuery(DataSource dataSource, DataSetDefinition dataSet, out bool changed, out string[] parameterNames)
		{
			bool outChanged = false;
			string[] outParameterNames = null;
			DataSetDefinition dataSetDefinition = new RSConnection2010.SoapMethodWrapper<DataSetDefinition>(this, "PrepareQuery", () => this.PrepareQuery(dataSource, dataSet, out outChanged, out outParameterNames)).ExecuteMethod();
			changed = outChanged;
			parameterNames = outParameterNames;
			return dataSetDefinition;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000169C0 File Offset: 0x00014BC0
		public new ExtensionParameter[] GetExtensionSettings(string extension)
		{
			return new RSConnection2010.SoapMethodWrapper<ExtensionParameter[]>(this, "GetExtensionSettings", () => this.GetExtensionSettings(extension)).ExecuteMethod();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00016A00 File Offset: 0x00014C00
		public new ExtensionParameter[] ValidateExtensionSettings(string extension, ParameterValueOrFieldReference[] parameterValues, string site)
		{
			return new RSConnection2010.SoapMethodWrapper<ExtensionParameter[]>(this, "ValidateExtensionSettings", () => this.ValidateExtensionSettings(extension, parameterValues, site)).ExecuteMethod();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00016A4C File Offset: 0x00014C4C
		public new Extension[] ListExtensions(string extensionType)
		{
			return new RSConnection2010.SoapMethodWrapper<Extension[]>(this, "ListExtensions", () => this.ListExtensions(extensionType)).ExecuteMethod();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00016A8C File Offset: 0x00014C8C
		public new void FireEvent(string eventType, string eventData, string site)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "FireEvent", delegate
			{
				this.FireEvent(eventType, eventData, site);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00016AD8 File Offset: 0x00014CD8
		public new void LogonUser(string userName, string password, string authority)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "LogonUser", delegate
			{
				this.LogonUser(userName, password, authority);
				return 0;
			}).ExecuteMethod(false);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00016B25 File Offset: 0x00014D25
		public new void Logoff()
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "Logoff", delegate
			{
				base.Logoff();
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00016B44 File Offset: 0x00014D44
		public new bool GetCacheOptions(string itemPath, out ExpirationDefinition cacheExpiration)
		{
			ExpirationDefinition outCacheExpiration = null;
			bool flag = new RSConnection2010.SoapMethodWrapper<bool>(this, "GetCacheOptions", () => this.GetCacheOptions(itemPath, out outCacheExpiration)).ExecuteMethod();
			cacheExpiration = outCacheExpiration;
			return flag;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00016B90 File Offset: 0x00014D90
		public new void SetCacheOptions(string itemPath, bool cacheItem, ExpirationDefinition cacheExpiration)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetCacheOptions", delegate
			{
				this.SetCacheOptions(itemPath, cacheItem, cacheExpiration);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00016BDC File Offset: 0x00014DDC
		public new void FlushCache(string itemPath)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "FlushCache", delegate
			{
				this.FlushCache(itemPath);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00016C1C File Offset: 0x00014E1C
		public new string GetExecutionOptions(string itemPath, out ScheduleDefinitionOrReference schedule)
		{
			ScheduleDefinitionOrReference outSchedule = null;
			string text = new RSConnection2010.SoapMethodWrapper<string>(this, "GetExecutionOptions", () => this.GetExecutionOptions(itemPath, out outSchedule)).ExecuteMethod();
			schedule = outSchedule;
			return text;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00016C68 File Offset: 0x00014E68
		public new void SetExecutionOptions(string itemPath, string executionSetting, ScheduleDefinitionOrReference schedule)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetExecutionOptions", delegate
			{
				this.SetExecutionOptions(itemPath, executionSetting, schedule);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00016CB4 File Offset: 0x00014EB4
		public new string CreateItemHistorySnapshot(string itemPath, out Warning[] warnings)
		{
			Warning[] outWarnings = null;
			string text = new RSConnection2010.SoapMethodWrapper<string>(this, "CreateItemHistorySnapshot", () => this.CreateItemHistorySnapshot(itemPath, out outWarnings)).ExecuteMethod();
			warnings = outWarnings;
			return text;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00016D00 File Offset: 0x00014F00
		public new void DeleteItemHistorySnapshot(string itemPath, string historyId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "DeleteItemHistorySnapshot", delegate
			{
				this.DeleteItemHistorySnapshot(itemPath, historyId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00016D48 File Offset: 0x00014F48
		public new int GetItemHistoryLimit(string itemPath, out bool isSystem, out int systemLimit)
		{
			bool outIsSystem = false;
			int outSystemLimit = 0;
			int num = new RSConnection2010.SoapMethodWrapper<int>(this, "GetItemHistoryLimit", () => this.GetItemHistoryLimit(itemPath, out outIsSystem, out outSystemLimit)).ExecuteMethod();
			isSystem = outIsSystem;
			systemLimit = outSystemLimit;
			return num;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00016DA4 File Offset: 0x00014FA4
		public new void SetItemHistoryLimit(string itemPath, bool useSystem, int historyLimit)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetItemHistoryLimit", delegate
			{
				this.SetItemHistoryLimit(itemPath, useSystem, historyLimit);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00016DF0 File Offset: 0x00014FF0
		public new bool GetItemHistoryOptions(string itemPath, out bool keepExecutionSnapshots, out ScheduleDefinitionOrReference schedule)
		{
			bool outKeepExecutionSnapshots = false;
			ScheduleDefinitionOrReference outSchedule = null;
			bool flag = new RSConnection2010.SoapMethodWrapper<bool>(this, "GetItemHistoryOptions", () => this.GetItemHistoryOptions(itemPath, out outKeepExecutionSnapshots, out outSchedule)).ExecuteMethod();
			keepExecutionSnapshots = outKeepExecutionSnapshots;
			schedule = outSchedule;
			return flag;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00016E4C File Offset: 0x0001504C
		public new void SetItemHistoryOptions(string itemPath, bool enableManualSnapshotCreation, bool keepExecutionSnapshots, ScheduleDefinitionOrReference schedule)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetItemHistoryOptions", delegate
			{
				this.SetItemHistoryOptions(itemPath, enableManualSnapshotCreation, keepExecutionSnapshots, schedule);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00016EA0 File Offset: 0x000150A0
		public new void UpdateItemExecutionSnapshot(string itemPath)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "UpdateItemExecutionSnapshot", delegate
			{
				this.UpdateItemExecutionSnapshot(itemPath);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00016EE0 File Offset: 0x000150E0
		public new ItemHistorySnapshot[] ListItemHistory(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<ItemHistorySnapshot[]>(this, "ListItemHistory", () => this.ListItemHistory(itemPath)).ExecuteMethod();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00016F20 File Offset: 0x00015120
		public new string CreateSchedule(string name, ScheduleDefinition scheduleDefinition, string site)
		{
			return new RSConnection2010.SoapMethodWrapper<string>(this, "CreateSchedule", () => this.CreateSchedule(name, scheduleDefinition, site)).ExecuteMethod();
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00016F6C File Offset: 0x0001516C
		public new void DeleteSchedule(string scheduleId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "DeleteSchedule", delegate
			{
				this.DeleteSchedule(scheduleId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00016FAC File Offset: 0x000151AC
		public new Schedule[] ListSchedules(string site)
		{
			return new RSConnection2010.SoapMethodWrapper<Schedule[]>(this, "ListSchedules", () => this.ListSchedules(site)).ExecuteMethod();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00016FEC File Offset: 0x000151EC
		public new CatalogItem[] ListScheduledItems(string scheduleId)
		{
			return new RSConnection2010.SoapMethodWrapper<CatalogItem[]>(this, "ListScheduledItems", () => this.ListScheduledItems(scheduleId)).ExecuteMethod();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001702C File Offset: 0x0001522C
		public new Schedule GetScheduleProperties(string scheduleId)
		{
			return new RSConnection2010.SoapMethodWrapper<Schedule>(this, "GetScheduleProperties", () => this.GetScheduleProperties(scheduleId)).ExecuteMethod();
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001706C File Offset: 0x0001526C
		public new void SetScheduleProperties(string name, string scheduleId, ScheduleDefinition scheduleDefinition)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetScheduleProperties", delegate
			{
				this.SetScheduleProperties(name, scheduleId, scheduleDefinition);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000170B8 File Offset: 0x000152B8
		public new void PauseSchedule(string scheduleId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "PauseSchedule", delegate
			{
				this.PauseSchedule(scheduleId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000170F8 File Offset: 0x000152F8
		public new void ResumeSchedule(string scheduleId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "ResumeSchedule", delegate
			{
				this.ResumeSchedule(scheduleId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00017136 File Offset: 0x00015336
		public new Event[] ListEvents()
		{
			return new RSConnection2010.SoapMethodWrapper<Event[]>(this, "ListEvents", () => base.ListEvents()).ExecuteMethod();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00017154 File Offset: 0x00015354
		public new ItemParameter[] GetItemParameters(string itemPath, string historyId, bool forRendering, ParameterValue[] values, DataSourceCredentials[] credentials)
		{
			return new RSConnection2010.SoapMethodWrapper<ItemParameter[]>(this, "GetItemParameters", () => this.GetItemParameters(itemPath, historyId, forRendering, values, credentials)).ExecuteMethod();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000171B0 File Offset: 0x000153B0
		public new void SetItemParameters(string itemPath, ItemParameter[] parameters)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetItemParameters", delegate
			{
				this.SetItemParameters(itemPath, parameters);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000171F8 File Offset: 0x000153F8
		public new DataSourcePrompt[] GetItemDataSourcePrompts(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<DataSourcePrompt[]>(this, "GetItemDataSourcePrompts", () => this.GetItemDataSourcePrompts(itemPath)).ExecuteMethod();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00017238 File Offset: 0x00015438
		public new DataSource[] GetItemDataSources(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<DataSource[]>(this, "GetItemDataSources", () => this.GetItemDataSources(itemPath)).ExecuteMethod();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00017278 File Offset: 0x00015478
		public new void SetItemDataSources(string itemPath, DataSource[] dataSources)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetItemDataSources", delegate
			{
				this.SetItemDataSources(itemPath, dataSources);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000172C0 File Offset: 0x000154C0
		public new void EnableDataSource(string dataSource)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "EnableDataSource", delegate
			{
				this.EnableDataSource(dataSource);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00017300 File Offset: 0x00015500
		public new void DisableDataSource(string dataSource)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "DisableDataSource", delegate
			{
				this.DisableDataSource(dataSource);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00017340 File Offset: 0x00015540
		public new Property[] GetSystemProperties(Property[] properties)
		{
			return new RSConnection2010.SoapMethodWrapper<Property[]>(this, "GetSystemProperties", () => this.GetSystemProperties(properties)).ExecuteMethod();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00017380 File Offset: 0x00015580
		public new void SetSystemProperties(Property[] properties)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetSystemProperties", delegate
			{
				this.SetSystemProperties(properties);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000173BE File Offset: 0x000155BE
		public new Job[] ListJobs()
		{
			return new RSConnection2010.SoapMethodWrapper<Job[]>(this, "ListJobs", () => base.ListJobs()).ExecuteMethod();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000173DC File Offset: 0x000155DC
		public new bool CancelJob(string jobId)
		{
			return new RSConnection2010.SoapMethodWrapper<bool>(this, "CancelJob", () => this.CancelJob(jobId)).ExecuteMethod();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001741C File Offset: 0x0001561C
		public new CatalogItem GenerateModel(string dataSource, string model, string parent, Property[] properties, out Warning[] warnings)
		{
			Warning[] outWarnings = null;
			CatalogItem catalogItem = new RSConnection2010.SoapMethodWrapper<CatalogItem>(this, "GenerateModel", () => this.GenerateModel(dataSource, model, parent, properties, out outWarnings)).ExecuteMethod();
			warnings = outWarnings;
			return catalogItem;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00017480 File Offset: 0x00015680
		public new Warning[] RegenerateModel(string model)
		{
			return new RSConnection2010.SoapMethodWrapper<Warning[]>(this, "RegenerateModel", () => this.RegenerateModel(model)).ExecuteMethod();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000174C0 File Offset: 0x000156C0
		public new string[] GetModelItemPermissions(string model, string modelItemId)
		{
			return new RSConnection2010.SoapMethodWrapper<string[]>(this, "GetModelItemPermissions", () => this.GetModelItemPermissions(model, modelItemId)).ExecuteMethod();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00017504 File Offset: 0x00015704
		public new Policy[] GetModelItemPolicies(string model, string modelItemId, out bool inheritParent)
		{
			bool outInheritParent = false;
			Policy[] array = new RSConnection2010.SoapMethodWrapper<Policy[]>(this, "GetModelItemPolicies", () => this.GetModelItemPolicies(model, modelItemId, out outInheritParent)).ExecuteMethod();
			inheritParent = outInheritParent;
			return array;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00017558 File Offset: 0x00015758
		public new void SetModelItemPolicies(string model, string modelItemId, Policy[] policies)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetModelItemPolicies", delegate
			{
				this.SetModelItemPolicies(model, modelItemId, policies);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000175A4 File Offset: 0x000157A4
		public new byte[] GetUserModel(string model, string perspective)
		{
			return new RSConnection2010.SoapMethodWrapper<byte[]>(this, "GetUserModel", () => this.GetUserModel(model, perspective)).ExecuteMethod();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000175E8 File Offset: 0x000157E8
		public new void InheritModelItemParentSecurity(string model, string modelItemId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "InheritModelItemParentSecurity", delegate
			{
				this.InheritModelItemParentSecurity(model, modelItemId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00017630 File Offset: 0x00015830
		public new ModelDrillthroughReport[] ListModelDrillthroughReports(string model, string modelItemId)
		{
			return new RSConnection2010.SoapMethodWrapper<ModelDrillthroughReport[]>(this, "ListModelDrillthroughReports", () => this.ListModelDrillthroughReports(model, modelItemId)).ExecuteMethod();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00017674 File Offset: 0x00015874
		public new void SetModelDrillthroughReports(string model, string modelItemId, ModelDrillthroughReport[] reports)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetModelDrillthroughReports", delegate
			{
				this.SetModelDrillthroughReports(model, modelItemId, reports);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000176C0 File Offset: 0x000158C0
		public new ModelItem[] ListModelItemChildren(string model, string modelItemId, bool recursive)
		{
			return new RSConnection2010.SoapMethodWrapper<ModelItem[]>(this, "ListModelItemChildren", () => this.ListModelItemChildren(model, modelItemId, recursive)).ExecuteMethod();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001770C File Offset: 0x0001590C
		public new ModelCatalogItem[] ListModelPerspectives(string model)
		{
			return new RSConnection2010.SoapMethodWrapper<ModelCatalogItem[]>(this, "ListModelPerspectives", () => this.ListModelPerspectives(model)).ExecuteMethod();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001774C File Offset: 0x0001594C
		public new void RemoveAllModelItemPolicies(string model)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "RemoveAllModelItemPolicies", delegate
			{
				this.RemoveAllModelItemPolicies(model);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001778C File Offset: 0x0001598C
		public new void CreateLinkedItem(string itemPath, string parent, string link, Property[] properties)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "CreateLinkedItem", delegate
			{
				this.CreateLinkedItem(itemPath, parent, link, properties);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000177E0 File Offset: 0x000159E0
		public new CatalogItem CreateDataSource(string dataSource, string parent, bool overwrite, DataSourceDefinition definition, Property[] properties)
		{
			return new RSConnection2010.SoapMethodWrapper<CatalogItem>(this, "CreateDataSource", () => this.CreateDataSource(dataSource, parent, overwrite, definition, properties)).ExecuteMethod();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001783C File Offset: 0x00015A3C
		public new CatalogItem CreateFolder(string folder, string parent, Property[] properties)
		{
			return new RSConnection2010.SoapMethodWrapper<CatalogItem>(this, "CreateFolder", () => this.CreateFolder(folder, parent, properties)).ExecuteMethod();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00017888 File Offset: 0x00015A88
		public new CatalogItem CreateCatalogItem(string itemType, string name, string parent, bool overwrite, byte[] definition, Property[] properties, out Warning[] warnings)
		{
			Warning[] outWarnings = null;
			CatalogItem catalogItem = new RSConnection2010.SoapMethodWrapper<CatalogItem>(this, "CreateCatalogItem", () => this.CreateCatalogItem(itemType, name, parent, overwrite, definition, properties, out outWarnings)).ExecuteMethod();
			warnings = outWarnings;
			return catalogItem;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000178FC File Offset: 0x00015AFC
		public new byte[] GetItemDefinition(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<byte[]>(this, "GetItemDefinition", () => this.GetItemDefinition(itemPath)).ExecuteMethod();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00017939 File Offset: 0x00015B39
		public new IAsyncResult BeginGetItemDefinition(string ItemPath, AsyncCallback callback, object asyncState)
		{
			return base.BeginGetItemDefinition(ItemPath, callback, asyncState);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00017944 File Offset: 0x00015B44
		public new byte[] EndGetItemDefinition(IAsyncResult asyncResult)
		{
			return new RSConnection2010.SoapMethodWrapper<byte[]>(this, "EndGetItemDefinition", () => this.EndGetItemDefinition(asyncResult)).ExecuteMethod();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00017984 File Offset: 0x00015B84
		public new Warning[] SetItemDefinition(string itemPath, byte[] definition, Property[] properties)
		{
			return new RSConnection2010.SoapMethodWrapper<Warning[]>(this, "SetItemDefinition", () => this.SetItemDefinition(itemPath, definition, properties)).ExecuteMethod();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000179CF File Offset: 0x00015BCF
		public new string[] ListSecurityScopes()
		{
			return new RSConnection2010.SoapMethodWrapper<string[]>(this, "ListSecurityScopes", () => base.ListSecurityScopes()).ExecuteMethod();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000179ED File Offset: 0x00015BED
		public new string[] ListExecutionSettings()
		{
			return new RSConnection2010.SoapMethodWrapper<string[]>(this, "ListExecutionSettings", () => base.ListExecutionSettings()).ExecuteMethod();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00017A0B File Offset: 0x00015C0B
		public new string[] ListItemTypes()
		{
			return new RSConnection2010.SoapMethodWrapper<string[]>(this, "ListItemTypes", () => base.ListItemTypes()).ExecuteMethod();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00017A29 File Offset: 0x00015C29
		public new string[] ListDatabaseCredentialRetrievalOptions()
		{
			return new RSConnection2010.SoapMethodWrapper<string[]>(this, "ListDatabaseCredentialRetrievalOptions", () => base.ListDatabaseCredentialRetrievalOptions()).ExecuteMethod();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00017A48 File Offset: 0x00015C48
		public new string GetReportServerConfigInfo(bool scaleOut)
		{
			return new RSConnection2010.SoapMethodWrapper<string>(this, "GetReportServerConfigInfo", () => this.GetReportServerConfigInfo(scaleOut)).ExecuteMethod();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00017A88 File Offset: 0x00015C88
		public new string CreateReportEditSession(string report, string parent, byte[] definition, out Warning[] warnings)
		{
			Warning[] outWarnings = null;
			string text = new RSConnection2010.SoapMethodWrapper<string>(this, "CreateReportEditSession", () => this.CreateReportEditSession(report, parent, definition, out outWarnings)).ExecuteMethod();
			warnings = outWarnings;
			return text;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00017AE4 File Offset: 0x00015CE4
		public new bool TestConnectForItemDataSource(string itemPath, string dataSourceName, string userName, string password, out string connectError)
		{
			string outConnectError = null;
			bool flag = new RSConnection2010.SoapMethodWrapper<bool>(this, "TestConnectForItemDataSource", () => this.TestConnectForItemDataSource(itemPath, dataSourceName, userName, password, out outConnectError)).ExecuteMethod();
			connectError = outConnectError;
			return flag;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00017B48 File Offset: 0x00015D48
		public new bool TestConnectForDataSourceDefinition(DataSourceDefinition dataSourceDefinition, string userName, string password, out string connectError)
		{
			string outConnectError = null;
			bool flag = new RSConnection2010.SoapMethodWrapper<bool>(this, "TestConnectForDataSourceDefinition", () => this.TestConnectForDataSourceDefinition(dataSourceDefinition, userName, password, out outConnectError)).ExecuteMethod();
			connectError = outConnectError;
			return flag;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00017BA4 File Offset: 0x00015DA4
		public new ItemReferenceData[] GetItemReferences(string itemPath, string referenceItemType)
		{
			return new RSConnection2010.SoapMethodWrapper<ItemReferenceData[]>(this, "GetItemReferences", () => this.GetItemReferences(itemPath, referenceItemType)).ExecuteMethod();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00017BE8 File Offset: 0x00015DE8
		public new void SetItemReferences(string itemPath, ItemReference[] itemReferences)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetItemReferences", delegate
			{
				this.SetItemReferences(itemPath, itemReferences);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00017C30 File Offset: 0x00015E30
		public new CacheRefreshPlan[] ListCacheRefreshPlans(string itemPath)
		{
			return new RSConnection2010.SoapMethodWrapper<CacheRefreshPlan[]>(this, "ListCacheRefreshPlans", () => this.ListCacheRefreshPlans(itemPath)).ExecuteMethod();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00017C70 File Offset: 0x00015E70
		public new string CreateCacheRefreshPlan(string itemPath, string description, string eventType, string matchData, ParameterValue[] parameters)
		{
			return new RSConnection2010.SoapMethodWrapper<string>(this, "CreateCacheRefreshPlan", () => this.CreateCacheRefreshPlan(itemPath, description, eventType, matchData, parameters)).ExecuteMethod();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00017CCC File Offset: 0x00015ECC
		public new string GetCacheRefreshPlanProperties(string cacheRefreshPlanId, out string lastRunStatus, out CacheRefreshPlanState state, out string eventType, out string matchData, out ParameterValue[] parameters)
		{
			string outLastRunStatus = null;
			CacheRefreshPlanState outState = null;
			string outEventType = null;
			string outMatchData = null;
			ParameterValue[] outParameters = null;
			string text = new RSConnection2010.SoapMethodWrapper<string>(this, "GetCacheRefreshPlanProperties", () => this.GetCacheRefreshPlanProperties(cacheRefreshPlanId, out outLastRunStatus, out outState, out outEventType, out outMatchData, out outParameters)).ExecuteMethod();
			lastRunStatus = outLastRunStatus;
			state = outState;
			eventType = outEventType;
			matchData = outMatchData;
			parameters = outParameters;
			return text;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00017D58 File Offset: 0x00015F58
		public new void SetCacheRefreshPlanProperties(string cacheRefreshPlanId, string description, string eventType, string matchData, ParameterValue[] parameters)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "SetCacheRefreshPlanProperties", delegate
			{
				this.SetCacheRefreshPlanProperties(cacheRefreshPlanId, description, eventType, matchData, parameters);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00017DB4 File Offset: 0x00015FB4
		public new void DeleteCacheRefreshPlan(string cacheRefreshPlanId)
		{
			new RSConnection2010.SoapMethodWrapper<int>(this, "DeleteCacheRefreshPlan", delegate
			{
				this.DeleteCacheRefreshPlan(cacheRefreshPlanId);
				return 0;
			}).ExecuteMethod();
		}

		// Token: 0x04000100 RID: 256
		private bool m_useSharePointProxy;

		// Token: 0x04000101 RID: 257
		private bool m_useSecureConnection;

		// Token: 0x04000102 RID: 258
		private bool m_checkedServerConfiguration;

		// Token: 0x04000103 RID: 259
		private string m_secureServerUrl;

		// Token: 0x04000104 RID: 260
		private string m_nonSecureServerUrl;

		// Token: 0x04000105 RID: 261
		internal const string SoapEndpoint = "ReportService2010.asmx";

		// Token: 0x02000194 RID: 404
		// (Invoke) Token: 0x06000939 RID: 2361
		internal delegate void BeforeExecutionEventHandler(RSConnection2010 conn, string methodName);

		// Token: 0x02000195 RID: 405
		// (Invoke) Token: 0x0600093D RID: 2365
		internal delegate void ExceptionEncounteredEventHandler(RSConnection2010 conn, string methodName, Exception e);

		// Token: 0x02000196 RID: 406
		internal sealed class MissingEndpointException : Exception
		{
			// Token: 0x06000940 RID: 2368 RVA: 0x00020BC2 File Offset: 0x0001EDC2
			public MissingEndpointException(Exception inner)
				: base(SoapExceptionStrings.MissingEndpoint, inner)
			{
			}

			// Token: 0x06000941 RID: 2369 RVA: 0x00020BD0 File Offset: 0x0001EDD0
			public static void ThrowIfEndpointMissing(WebException e)
			{
				if (e.Status == WebExceptionStatus.ProtocolError && e.Response != null)
				{
					HttpWebResponse httpWebResponse = e.Response as HttpWebResponse;
					if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.NotFound)
					{
						throw new RSConnection2010.MissingEndpointException(e);
					}
				}
			}
		}

		// Token: 0x02000197 RID: 407
		private sealed class SoapVersionMismatchException : Exception
		{
			// Token: 0x06000942 RID: 2370 RVA: 0x00020C11 File Offset: 0x0001EE11
			private SoapVersionMismatchException(Exception inner)
				: base(SoapExceptionStrings.VersionMismatch, inner)
			{
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x00020C1F File Offset: 0x0001EE1F
			public static void ThrowIfVersionMismatch(SoapException e, string expectedEndpoint)
			{
				if (e.Code == SoapException.ClientFaultCode && !e.Actor.EndsWith(expectedEndpoint, StringComparison.OrdinalIgnoreCase))
				{
					throw new RSConnection2010.SoapVersionMismatchException(e);
				}
			}
		}

		// Token: 0x02000198 RID: 408
		private sealed class SoapMethodWrapper<TReturn>
		{
			// Token: 0x06000944 RID: 2372 RVA: 0x00020C49 File Offset: 0x0001EE49
			public SoapMethodWrapper(RSConnection2010 conn, string methodName, RSConnection2010.SoapMethodWrapper<TReturn>.SoapMethod method)
			{
				this.m_conn = conn;
				this.m_methodName = methodName;
				this.m_method = method;
			}

			// Token: 0x06000945 RID: 2373 RVA: 0x00020C66 File Offset: 0x0001EE66
			public TReturn ExecuteMethod()
			{
				return this.ExecuteMethod(true);
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x00020C70 File Offset: 0x0001EE70
			public TReturn ExecuteMethod(bool setConnectionProtocol)
			{
				TReturn treturn;
				try
				{
					this.OnBeforeExecution();
					if (setConnectionProtocol)
					{
						this.m_conn.SetConnectionProtocol();
					}
					treturn = this.m_method();
				}
				catch (SoapException ex)
				{
					this.OnExceptionEncountered(ex);
					RSConnection2010.SoapVersionMismatchException.ThrowIfVersionMismatch(ex, "ReportService2010.asmx");
					throw;
				}
				catch (WebException ex2)
				{
					this.OnExceptionEncountered(ex2);
					RSConnection2010.MissingEndpointException.ThrowIfEndpointMissing(ex2);
					throw;
				}
				catch (InvalidOperationException ex3)
				{
					this.OnExceptionEncountered(ex3);
					throw new RSConnection2010.MissingEndpointException(ex3);
				}
				catch (Exception ex4)
				{
					this.OnExceptionEncountered(ex4);
					throw;
				}
				return treturn;
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x00020D14 File Offset: 0x0001EF14
			private void OnBeforeExecution()
			{
				if (this.m_conn.BeforeExecution != null)
				{
					this.m_conn.BeforeExecution(this.m_conn, this.m_methodName);
				}
			}

			// Token: 0x06000948 RID: 2376 RVA: 0x00020D3F File Offset: 0x0001EF3F
			private void OnExceptionEncountered(Exception e)
			{
				if (this.m_conn.ExceptionEncountered != null)
				{
					this.m_conn.ExceptionEncountered(this.m_conn, this.m_methodName, e);
				}
			}

			// Token: 0x040004A8 RID: 1192
			private RSConnection2010 m_conn;

			// Token: 0x040004A9 RID: 1193
			private string m_methodName;

			// Token: 0x040004AA RID: 1194
			private RSConnection2010.SoapMethodWrapper<TReturn>.SoapMethod m_method;

			// Token: 0x02000218 RID: 536
			// (Invoke) Token: 0x06000A50 RID: 2640
			public delegate TReturn SoapMethod();
		}
	}
}
