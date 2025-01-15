using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.Interfaces.SoapProxy;
using Microsoft.ReportingServices.Portal.Services;
using Microsoft.ReportingServices.Portal.Services.Extensions;
using Microsoft.ReportingServices.Portal.Services.ODataExtensions;
using Microsoft.ReportingServices.Portal.Services.Util;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Repositories
{
	// Token: 0x02000016 RID: 22
	internal sealed class SubscriptionService : ISubscriptionService
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00009859 File Offset: 0x00007A59
		internal SubscriptionService(ISoapRS2010Proxy soapProxy, ICatalogRepository catalogRepository)
		{
			if (soapProxy == null)
			{
				throw new ArgumentNullException("soapProxy");
			}
			if (catalogRepository == null)
			{
				throw new ArgumentNullException("catalogRepository");
			}
			this._soapRS2010Proxy = soapProxy;
			this._catalogRepository = catalogRepository;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000988C File Offset: 0x00007A8C
		public void ExecuteSubscription(IPrincipal userPrincipal, Guid key)
		{
			if (this.GetSubscription(userPrincipal, key).IsActive)
			{
				this.FireEvent(userPrincipal, ReportScheduleActions.TimedSubscription.ToString(), key.ToString());
				this.UpdateSubscriptionStatus(userPrincipal, key, RepLibRes.SubscriptionExecuted);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000098D8 File Offset: 0x00007AD8
		public IQueryable<global::Model.Subscription> GetSubscriptions(IPrincipal userPrincipal)
		{
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			ListSubscriptionsAction listSubscriptionsAction = rsservice.ListSubscriptionsAction;
			listSubscriptionsAction.ActionParameters.PathIsSiteOrFolder = true;
			listSubscriptionsAction.ActionParameters.Owner = rsservice.UserContext.UserName;
			listSubscriptionsAction.ActionParameters.SubscriptionType = SubscriptionType.ReportSubscription;
			listSubscriptionsAction.ActionParameters.IncludeExtensionSettings = true;
			listSubscriptionsAction.Execute();
			return listSubscriptionsAction.ActionParameters.Children.Select((SubscriptionImpl x) => x.ToWebApiModel(this._soapRS2010Proxy.GetParameterTypes(userPrincipal, x.ReportName), null)).AsQueryable<global::Model.Subscription>();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000996C File Offset: 0x00007B6C
		public global::Model.Subscription GetSubscription(IPrincipal userPrincipal, Guid key)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			rsservice.WillDisconnectStorage();
			SubscriptionImpl subscription = rsservice.SubscriptionManager.GetSubscription(key, false);
			global::Model.Subscription subscription2;
			if (subscription.IsDataDriven())
			{
				DataDrivenSubscriptionProperties dataDrivenSubscriptionProperties = this._soapRS2010Proxy.GetDataDrivenSubscriptionProperties(userPrincipal, key.ToString());
				subscription2 = subscription.ToWebApiModelDataDriven(this._soapRS2010Proxy.GetParameterTypes(userPrincipal, subscription.ReportName), dataDrivenSubscriptionProperties);
			}
			else
			{
				SubscriptionProperties subscriptionProperties = this._soapRS2010Proxy.GetSubscriptionProperties(userPrincipal, key.ToString());
				subscription2 = subscription.ToWebApiModel(this._soapRS2010Proxy.GetParameterTypes(userPrincipal, subscription.ReportName), subscriptionProperties);
			}
			return subscription2;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009A18 File Offset: 0x00007C18
		public string CreateSubscription(IPrincipal userPrincipal, global::Model.Subscription subscription)
		{
			string empty = string.Empty;
			Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings = new Microsoft.SqlServer.ReportingServices2010.ExtensionSettings
			{
				Extension = subscription.DeliveryExtension,
				ParameterValues = this.GetSoapParameterValueOfFieldReferences(subscription.ExtensionSettings.ParameterValues)
			};
			if (subscription.IsDataDriven)
			{
				Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan dataRetrievalPlan = this.GetDataRetrievalPlan(userPrincipal, subscription);
				return this._soapRS2010Proxy.CreateDataDrivenSubscription(userPrincipal, subscription.Report, extensionSettings, dataRetrievalPlan, subscription.Description, subscription.EventType, this.GetMatchData(subscription), this.GetSoapParameterValuesOrFieldReference(subscription.ParameterValues, this._soapRS2010Proxy.GetParameterTypes(userPrincipal, subscription.Report)));
			}
			return this._soapRS2010Proxy.CreateSubscription(userPrincipal, subscription.Report, extensionSettings, subscription.Description, subscription.EventType, this.GetMatchData(subscription), this.GetSoapParameterValues(subscription.ParameterValues, this._soapRS2010Proxy.GetParameterTypes(userPrincipal, subscription.Report)));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009AF0 File Offset: 0x00007CF0
		public void UpdateSubscription(IPrincipal userPrincipal, Guid key, global::Model.Subscription subscription)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
			global::Model.Subscription subscription2 = this.GetSubscription(userPrincipal, key);
			string subscriptionCulture = this.GetSubscriptionCulture(userPrincipal, key);
			if (subscription2.IsActive != subscription.IsActive)
			{
				if (subscription.IsActive)
				{
					this.EnableSubscription(userPrincipal, key);
				}
				else
				{
					this.DisableSubscription(userPrincipal, key);
				}
			}
			CultureUtil.ExecuteInCulture(subscriptionCulture, delegate
			{
				if (subscription.IsDataDriven)
				{
					this.SetDataDrivenSubscriptionProperties(userPrincipal, key, subscription);
					return;
				}
				this.SetSubscriptionProperties(userPrincipal, key, subscription);
			});
			if (subscription2.Owner != subscription.Owner)
			{
				this.ChangeSubscriptionOwner(userPrincipal, key, subscription.Owner);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009BE4 File Offset: 0x00007DE4
		public void PatchSubscription(IPrincipal userPrincipal, Guid key, global::Model.Subscription subscription, string[] delta)
		{
			if (delta == null)
			{
				return;
			}
			if (delta.Contains("IsActive"))
			{
				if (subscription.IsActive)
				{
					this.EnableSubscription(userPrincipal, key);
				}
				else
				{
					this.DisableSubscription(userPrincipal, key);
				}
			}
			if (delta.Length != 0 && !subscription.IsDataDriven)
			{
				CultureUtil.ExecuteInCulture(this.GetSubscriptionCulture(userPrincipal, key), delegate
				{
					this.SetSubscriptionProperties(userPrincipal, key, subscription);
				});
			}
			if (delta.Contains("Owner"))
			{
				this.ChangeSubscriptionOwner(userPrincipal, key, subscription.Owner);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009CBC File Offset: 0x00007EBC
		private void SetSubscriptionProperties(IPrincipal userPrincipal, Guid key, global::Model.Subscription subscription)
		{
			Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings = new Microsoft.SqlServer.ReportingServices2010.ExtensionSettings
			{
				Extension = subscription.DeliveryExtension,
				ParameterValues = this.GetSoapParameterValueOfFieldReferences(subscription.ExtensionSettings.ParameterValues)
			};
			this._soapRS2010Proxy.SetSubscriptionProperties(userPrincipal, key.ToString(), extensionSettings, subscription.Description, subscription.EventType, this.GetMatchData(subscription), this.GetSoapParameterValues(subscription.ParameterValues, this._soapRS2010Proxy.GetParameterTypes(userPrincipal, subscription.Report)));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009D40 File Offset: 0x00007F40
		private void SetDataDrivenSubscriptionProperties(IPrincipal userPrincipal, Guid key, global::Model.Subscription subscription)
		{
			Microsoft.SqlServer.ReportingServices2010.ExtensionSettings extensionSettings = new Microsoft.SqlServer.ReportingServices2010.ExtensionSettings
			{
				Extension = subscription.DeliveryExtension,
				ParameterValues = this.GetSoapParameterValueOfFieldReferences(subscription.ExtensionSettings.ParameterValues)
			};
			Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan dataRetrievalPlan = this.GetDataRetrievalPlan(userPrincipal, subscription);
			this._soapRS2010Proxy.SetDataDrivenSubscriptionProperties(userPrincipal, key.ToString(), extensionSettings, dataRetrievalPlan, subscription.Description, subscription.EventType, this.GetMatchData(subscription), this.GetSoapParameterValuesOrFieldReference(subscription.ParameterValues, this._soapRS2010Proxy.GetParameterTypes(userPrincipal, subscription.Report)));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009DCC File Offset: 0x00007FCC
		private Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan GetDataRetrievalPlan(IPrincipal userPrincipal, global::Model.Subscription subscription)
		{
			Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan dataRetrievalPlan = new Microsoft.SqlServer.ReportingServices2010.DataRetrievalPlan();
			if (subscription.DataSource != null)
			{
				Microsoft.SqlServer.ReportingServices2010.DataSource dataSource = subscription.DataSource.ToProxy2010DataSource();
				DataSetDefinition dataSetDefinition = new DataSetDefinition
				{
					Query = subscription.DataQuery.ToSoapQueryDefinition()
				};
				dataRetrievalPlan.Item = subscription.DataSource.ToDataSourceDefinitionOrReference();
				dataRetrievalPlan.DataSet = this._soapRS2010Proxy.PrepareQuery(userPrincipal, dataSource, dataSetDefinition);
			}
			else
			{
				DataDrivenSubscriptionProperties dataDrivenSubscriptionProperties = this._soapRS2010Proxy.GetDataDrivenSubscriptionProperties(userPrincipal, subscription.Id.ToString());
				dataRetrievalPlan.Item = dataDrivenSubscriptionProperties.DataSettings.Item;
				dataRetrievalPlan.DataSet = dataDrivenSubscriptionProperties.DataSettings.DataSet;
				DataSourceDefinition dataSourceDefinition = dataRetrievalPlan.Item as DataSourceDefinition;
				if (dataSourceDefinition != null && dataSourceDefinition.CredentialRetrieval == CredentialRetrievalEnum.Store && dataSourceDefinition.Password == null)
				{
					byte[] dataSourcePasswordForSubscription = this._catalogRepository.GetDataSourcePasswordForSubscription(userPrincipal, subscription.Id);
					if (dataSourcePasswordForSubscription != null)
					{
						dataSourceDefinition.Password = CatalogEncryption.Instance.DecryptToString(dataSourcePasswordForSubscription, "Password");
					}
				}
			}
			return dataRetrievalPlan;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009ECD File Offset: 0x000080CD
		private void ChangeSubscriptionOwner(IPrincipal userPrincipal, Guid key, string owner)
		{
			ChangeSubscriptionOwnerAction changeSubscriptionOwnerAction = ServicesUtil.CreateRsService(userPrincipal).ChangeSubscriptionOwnerAction;
			changeSubscriptionOwnerAction.ActionParameters.SubscriptionID = key.ToString();
			changeSubscriptionOwnerAction.ActionParameters.NewOwner = owner;
			changeSubscriptionOwnerAction.Execute();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009F03 File Offset: 0x00008103
		public void DeleteSubscription(IPrincipal userPrincipal, Guid key)
		{
			this._soapRS2010Proxy.DeleteSubscription(userPrincipal, key.ToString());
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009F1E File Offset: 0x0000811E
		public void EnableSubscription(IPrincipal userPrincipal, Guid key)
		{
			EnableSubscriptionAction enableSubscriptionAction = ServicesUtil.CreateRsService(userPrincipal).EnableSubscriptionAction;
			enableSubscriptionAction.ActionParameters.SubscriptionID = key.ToString();
			enableSubscriptionAction.Execute();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009F48 File Offset: 0x00008148
		public void DisableSubscription(IPrincipal userPrincipal, Guid key)
		{
			DisableSubscriptionAction disableSubscriptionAction = ServicesUtil.CreateRsService(userPrincipal).DisableSubscriptionAction;
			disableSubscriptionAction.ActionParameters.SubscriptionID = key.ToString();
			disableSubscriptionAction.Execute();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009F74 File Offset: 0x00008174
		public void ExecuteCacheRefreshPlan(IPrincipal userPrincipal, Guid key)
		{
			global::Model.CacheRefreshPlan cacheRefreshPlan = this.GetCacheRefreshPlan(userPrincipal, key);
			this.FireEvent(userPrincipal, cacheRefreshPlan.EventType, key.ToString());
			this.UpdateSubscriptionStatus(userPrincipal, key, RepLibRes.RefreshCachePlanRefreshed);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009FB4 File Offset: 0x000081B4
		public global::Model.CacheRefreshPlan GetCacheRefreshPlan(IPrincipal userPrincipal, Guid key)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
			RSService rsservice = ServicesUtil.CreateRsService(userPrincipal);
			rsservice.WillDisconnectStorage();
			SubscriptionImpl subscription = rsservice.SubscriptionManager.GetSubscription(key, false);
			Dictionary<string, ReportParameterType> dictionary = (subscription.HasParameters ? this._soapRS2010Proxy.GetParameterTypes(userPrincipal, subscription.ReportName) : null);
			return subscription.ToCacheRefreshPlanModel(dictionary);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000A010 File Offset: 0x00008210
		public void CreateCacheRefreshPlan(IPrincipal userPrincipal, global::Model.CacheRefreshPlan cacherefreshPlan)
		{
			this._soapRS2010Proxy.CreateCacheRefreshPlan(userPrincipal, cacherefreshPlan.CatalogItemPath, cacherefreshPlan.Description, cacherefreshPlan.EventType, this.GetMatchData(cacherefreshPlan), this.GetParameterValues(userPrincipal, cacherefreshPlan));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A04B File Offset: 0x0000824B
		public void DeleteCacheRefreshPlan(IPrincipal userPrincipal, Guid key)
		{
			this._soapRS2010Proxy.DeleteCacheRefreshPlan(userPrincipal, key.ToString());
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A068 File Offset: 0x00008268
		public void UpdateCacheRefreshPlan(IPrincipal userPrincipal, Guid key, global::Model.CacheRefreshPlan plan)
		{
			CultureUtil.ExecuteInCulture(this.GetSubscriptionCulture(userPrincipal, key), delegate
			{
				this._soapRS2010Proxy.SetCacheRefreshPlanProperties(userPrincipal, key.ToString(), plan.Description, plan.EventType, this.GetMatchData(plan), this.GetParameterValues(userPrincipal, plan));
			});
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A0BC File Offset: 0x000082BC
		private Microsoft.SqlServer.ReportingServices2010.ParameterValue[] GetParameterValues(IPrincipal userPrincipal, global::Model.CacheRefreshPlan plan)
		{
			global::Model.CatalogItem catalogItem = this._catalogRepository.GetCatalogItem(userPrincipal, plan.CatalogItemPath);
			Microsoft.SqlServer.ReportingServices2010.ParameterValue[] array = new Microsoft.SqlServer.ReportingServices2010.ParameterValue[0];
			if (catalogItem.Type != CatalogItemType.PowerBIReport)
			{
				array = this.GetSoapParameterValues(plan.ParameterValues, this._soapRS2010Proxy.GetParameterTypes(userPrincipal, plan.CatalogItemPath));
			}
			return array;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A10C File Offset: 0x0000830C
		private void FireEvent(IPrincipal userPrincipal, string eventType, string eventData)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			rsService.ExecuteStorageAction(delegate
			{
				rsService.EventManager.FireEvent(eventType, eventData);
			});
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A150 File Offset: 0x00008350
		private string GetSubscriptionCulture(IPrincipal userPrincipal, Guid key)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			string culture = string.Empty;
			rsService.ExecuteStorageAction(delegate
			{
				SubscriptionImpl subscription = rsService.SubscriptionManager.GetSubscription(key, false);
				culture = subscription.Culture;
			});
			return culture;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A1A0 File Offset: 0x000083A0
		private void UpdateSubscriptionStatus(IPrincipal userPrincipal, Guid key, string status)
		{
			RSService rsService = ServicesUtil.CreateRsService(userPrincipal);
			rsService.ExecuteStorageAction(delegate
			{
				rsService.UpdateSubscriptionStatus(key, status);
			});
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000A1E4 File Offset: 0x000083E4
		private string GetMatchData(global::Model.Subscription subscription)
		{
			if (string.Compare(subscription.EventType, "SnapshotUpdated", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(subscription.Schedule.ScheduleID))
			{
				return subscription.Schedule.ScheduleID;
			}
			return subscription.Schedule.Definition.ToMatchData();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A234 File Offset: 0x00008434
		private string GetMatchData(global::Model.CacheRefreshPlan plan)
		{
			if (!string.IsNullOrEmpty(plan.Schedule.ScheduleID))
			{
				return plan.Schedule.ScheduleID;
			}
			return plan.Schedule.Definition.ToMatchData();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A264 File Offset: 0x00008464
		private ParameterValueOrFieldReference[] GetSoapParameterValueOfFieldReferences(IEnumerable<global::Model.ParameterValue> parameters)
		{
			if (parameters != null)
			{
				return parameters.Select((global::Model.ParameterValue parameter) => parameter.ToSoapParameterValueOrFieldReference()).ToArray<ParameterValueOrFieldReference>();
			}
			return new Microsoft.SqlServer.ReportingServices2010.ParameterValue[0];
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000A2A8 File Offset: 0x000084A8
		private Microsoft.SqlServer.ReportingServices2010.ParameterValue[] GetSoapParameterValues(IEnumerable<global::Model.ParameterValue> parameters, Dictionary<string, ReportParameterType> parameterTypes)
		{
			if (parameters != null)
			{
				return parameters.Select((global::Model.ParameterValue parameter) => parameter.ToSoapParameterValue(parameterTypes[parameter.Name])).ToArray<Microsoft.SqlServer.ReportingServices2010.ParameterValue>();
			}
			return new Microsoft.SqlServer.ReportingServices2010.ParameterValue[0];
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A2E4 File Offset: 0x000084E4
		private ParameterValueOrFieldReference[] GetSoapParameterValuesOrFieldReference(IEnumerable<global::Model.ParameterValue> parameters, Dictionary<string, ReportParameterType> parameterTypes)
		{
			if (parameters != null)
			{
				return parameters.Select((global::Model.ParameterValue parameter) => parameter.ToSoapParameterValueOrFieldReference(parameterTypes[parameter.Name])).ToArray<ParameterValueOrFieldReference>();
			}
			return new Microsoft.SqlServer.ReportingServices2010.ParameterValue[0];
		}

		// Token: 0x04000066 RID: 102
		private readonly ISoapRS2010Proxy _soapRS2010Proxy;

		// Token: 0x04000067 RID: 103
		private readonly ICatalogRepository _catalogRepository;
	}
}
