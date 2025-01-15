using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200023E RID: 574
	internal class SubscriptionManager
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x00051A19 File Offset: 0x0004FC19
		public SubscriptionManager(RSService service)
		{
			this.m_service = service;
			this.m_db.ConnectionManager = this.m_service.Storage.ConnectionManager;
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00051A59 File Offset: 0x0004FC59
		internal ConnectionManager ConnectionManager
		{
			get
			{
				return this.m_service.Storage.ConnectionManager;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00051A6B File Offset: 0x0004FC6B
		private string UserName
		{
			get
			{
				return this.m_service.UserContext.UserName;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00051A7D File Offset: 0x0004FC7D
		private UserContext UserContext
		{
			get
			{
				return this.m_service.UserContext;
			}
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00051A8C File Offset: 0x0004FC8C
		public Guid CreateSubscription(Guid id, ExternalItemPath report, Guid reportID, string eventType, string matchData, ExtensionSettings extensionSettings, string description, ParameterValueOrFieldReference[] parameters, DataRetrievalPlan dataSettings, byte[] reportSecDesc)
		{
			SubscriptionImpl subscriptionImpl = new SubscriptionImpl(report, eventType, id, this.m_service);
			subscriptionImpl.m_extensionSettings = extensionSettings;
			subscriptionImpl.m_description = description;
			subscriptionImpl.m_modifiedDate = DateTime.Now;
			subscriptionImpl.m_eventType = eventType;
			subscriptionImpl.m_matchData = Microsoft.ReportingServices.Diagnostics.Task.ConvertXmlCulture(matchData, Microsoft.ReportingServices.Diagnostics.Task.CultureConversion.ToNeutralCulture);
			subscriptionImpl.m_parameters = parameters;
			subscriptionImpl.SetDataSettings(dataSettings);
			subscriptionImpl.m_securityDesc = reportSecDesc;
			subscriptionImpl.m_itemtID = reportID;
			subscriptionImpl.m_connectionManager = this.ConnectionManager;
			try
			{
				RepLibRes.Culture = Localization.FallbackUICulture;
				subscriptionImpl.LastStatus = RepLibRes.NewSubscription;
			}
			finally
			{
				RepLibRes.Culture = null;
			}
			if (Globals.Configuration.Extensions.Delivery[subscriptionImpl.DeliveryExtension] == null)
			{
				throw new DeliveryExtensionNotFoundException();
			}
			if (!Globals.Configuration.EventTypes.ContainsKey(eventType))
			{
				throw new UnknownEventTypeException(eventType);
			}
			this.VerifyCanSubscribeToEvent(eventType, report);
			this.ValidateExtensionSettingsData(subscriptionImpl);
			this.ValidateDataSettingsMappings(subscriptionImpl.m_dataSet, subscriptionImpl.m_extensionSettings, parameters);
			this.SetDataSourceInformation(subscriptionImpl.ID, subscriptionImpl.m_dataSource, true);
			this.m_db.CreateSubscription(subscriptionImpl);
			this.ValidateSubscriptionData(subscriptionImpl, eventType, matchData);
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace("Subscription Created for report {0} at {1} by {2}", new object[]
				{
					report.Value,
					DateTime.Now.ToString("s", CultureInfo.InvariantCulture),
					Environment.UserName
				});
			}
			return subscriptionImpl.m_id;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00051C14 File Offset: 0x0004FE14
		public Guid CreateCacheRefreshPlan(ExternalItemPath itemPath, Guid itemID, string eventType, string matchData, string description, ParameterValueOrFieldReference[] parameters, byte[] itemSecDesc)
		{
			Guid empty = Guid.Empty;
			SubscriptionImpl subscriptionImpl = new SubscriptionImpl(itemPath, eventType, Guid.Empty, this.m_service);
			subscriptionImpl.m_description = description;
			subscriptionImpl.m_modifiedDate = DateTime.Now;
			subscriptionImpl.m_eventType = eventType;
			subscriptionImpl.m_matchData = Microsoft.ReportingServices.Diagnostics.Task.ConvertXmlCulture(matchData, Microsoft.ReportingServices.Diagnostics.Task.CultureConversion.ToNeutralCulture);
			subscriptionImpl.m_parameters = parameters;
			subscriptionImpl.m_securityDesc = itemSecDesc;
			subscriptionImpl.m_itemtID = itemID;
			subscriptionImpl.m_connectionManager = this.ConnectionManager;
			try
			{
				RepLibRes.Culture = Localization.FallbackUICulture;
				subscriptionImpl.LastStatus = (string.Equals(eventType, "DataModelRefresh", StringComparison.OrdinalIgnoreCase) ? RepLibRes.NewScheduledRefreshPlan : RepLibRes.NewCacheRefreshPlan);
			}
			finally
			{
				RepLibRes.Culture = null;
			}
			if (!Globals.Configuration.IsSupportedEvent(eventType))
			{
				throw new UnknownEventTypeException(eventType);
			}
			this.VerifyCanSubscribeToEvent(eventType, itemPath);
			this.m_db.CreateSubscription(subscriptionImpl);
			this.ValidateSubscriptionData(subscriptionImpl, eventType, matchData);
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace("Subscription Created for report {0} at {1} by {2}", new object[]
				{
					itemPath.Value,
					DateTime.Now.ToString("s", CultureInfo.InvariantCulture),
					Environment.UserName
				});
			}
			return subscriptionImpl.m_id;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00051D50 File Offset: 0x0004FF50
		private void ValidateSubscriptionData(Microsoft.ReportingServices.Extensions.Subscription subscription, string eventType, string subscriptionData)
		{
			if (subscription.EventType != "SnapshotUpdated")
			{
				this.ValidateSubscriptionSchedule(subscription, subscriptionData, this.m_service.UserContext);
			}
			IEventHandler eventHandler = ExtensionClassFactory.GetEventHandlerByEventType(eventType) as IEventHandler;
			if (eventHandler != null)
			{
				eventHandler.ValidateSubscriptionData(subscription, subscriptionData, this.m_service.UserContext);
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00051DA4 File Offset: 0x0004FFA4
		public void ValidateSubscriptionSchedule(Microsoft.ReportingServices.Extensions.Subscription subscription, string subscriptionData, UserContext userContext)
		{
			SchedulingDBInterface schedulingDBInterface = new SchedulingDBInterface();
			schedulingDBInterface.ConnectionManager = ((SubscriptionImpl)subscription).m_connectionManager;
			Microsoft.ReportingServices.Diagnostics.Task task = schedulingDBInterface.GetTimeBasedSubscriptionSchedule(subscription.ID);
			Guid empty = Guid.Empty;
			ScheduleType scheduleType = ScheduleType.Scoped;
			ItemScheduleAction itemScheduleAction = null;
			ReportScheduleActions reportScheduleActions = (ReportScheduleActions)Enum.Parse(typeof(ReportScheduleActions), subscription.EventType);
			if (!this.IsPrivateSchedule(subscriptionData))
			{
				Globals.ParseGuidParameter(subscriptionData, "matchData");
				empty = new Guid(subscriptionData);
				scheduleType = ScheduleType.Shared;
				itemScheduleAction = new ItemScheduleAction();
				itemScheduleAction.Action = reportScheduleActions;
				itemScheduleAction.SubscriptionID = subscription.ID;
				itemScheduleAction.ItemID = subscription.ItemID;
				itemScheduleAction.ScheduleID = empty;
				if (schedulingDBInterface.GetTask(empty).Type != ScheduleType.Shared)
				{
					throw new ScheduleNotFoundException(subscriptionData);
				}
			}
			ExternalItemPath externalItemPath = new ExternalItemPath(subscription.ReportName);
			if (task != null)
			{
				RSService rsservice = new RSService(false);
				if (task.Type == scheduleType)
				{
					if (task.Type != ScheduleType.Shared)
					{
						task.FromXml(subscriptionData);
						task.EventType = subscription.EventType;
						task.EventData = subscription.ID.ToString();
						task.Creator = userContext;
						schedulingDBInterface.UpdateTaskProperties(task, true);
						return;
					}
					if (task.ID != empty)
					{
						schedulingDBInterface.RemoveReportFromSchedule(task.ID, subscription.ItemID, subscription.ID, reportScheduleActions);
						schedulingDBInterface.AddReportToSchedule(itemScheduleAction);
						return;
					}
				}
				else
				{
					if (task.Type == ScheduleType.Shared)
					{
						schedulingDBInterface.RemoveReportFromSchedule(task.ID, subscription.ItemID, subscription.ID, reportScheduleActions);
						task = new Microsoft.ReportingServices.Diagnostics.Task(Guid.Empty);
						task.FromXml(subscriptionData);
						task.EventType = subscription.EventType;
						task.EventData = subscription.ID.ToString();
						task.Creator = userContext;
						task.Path = rsservice.ExternalToCatalogItemPath(rsservice.ServiceHelper.GetSiteUrl(externalItemPath, userContext));
						schedulingDBInterface.CreateTimeBasedSubscriptionSchedule(rsservice.ExternalToCatalogItemPath(externalItemPath), subscription.ID, reportScheduleActions, task);
						return;
					}
					schedulingDBInterface.DeleteTimeBasedSubscriptionSchedule(subscription.ID);
					schedulingDBInterface.AddReportToSchedule(itemScheduleAction);
				}
				return;
			}
			if (empty == Guid.Empty)
			{
				task = new Microsoft.ReportingServices.Diagnostics.Task(Guid.Empty);
				task.FromXml(subscriptionData);
				task.EventType = subscription.EventType;
				task.EventData = subscription.ID.ToString();
				task.Creator = userContext;
				RSService rsservice2 = new RSService(false);
				task.Path = rsservice2.ExternalToCatalogItemPath(rsservice2.ServiceHelper.GetSiteUrl(externalItemPath, userContext));
				schedulingDBInterface.CreateTimeBasedSubscriptionSchedule(rsservice2.ExternalToCatalogItemPath(externalItemPath), subscription.ID, reportScheduleActions, task);
				return;
			}
			schedulingDBInterface.AddReportToSchedule(itemScheduleAction);
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00052041 File Offset: 0x00050241
		private bool IsPrivateSchedule(string subscriptionData)
		{
			return subscriptionData != null && subscriptionData.IndexOf("<", Localization.CatalogStringComparison) >= 0;
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0005205C File Offset: 0x0005025C
		private void ChangeEventType(Microsoft.ReportingServices.Extensions.Subscription subscription, string eventType)
		{
			if (subscription.EventType == eventType)
			{
				return;
			}
			IEventHandler eventHandler = ExtensionClassFactory.GetEventHandlerByEventType(subscription.EventType) as IEventHandler;
			if (eventHandler != null)
			{
				eventHandler.CleanUp(subscription);
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00052094 File Offset: 0x00050294
		private void VerifyCanSubscribeToEvent(string eventType, ExternalItemPath reportName)
		{
			IEventHandler eventHandler = ExtensionClassFactory.GetEventHandlerByEventType(eventType) as IEventHandler;
			if (eventHandler != null && !eventHandler.CanSubscribe(new CatalogQuery
			{
				m_connManager = this.ConnectionManager,
				AllowCommit = false
			}, reportName.Value))
			{
				throw new CannotSubscribeToEventException(eventType);
			}
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x000520E0 File Offset: 0x000502E0
		private void SetDataSourceInformation(Guid subscriptionID, DataSourceInfo dataSource, bool create)
		{
			if (dataSource == null)
			{
				return;
			}
			if (!dataSource.ReferenceByPath)
			{
				if (dataSource.NeedPrompt)
				{
					throw new InvalidDataSourceCredentialSettingException();
				}
			}
			else if (!DataSourceCatalogItem.GoodForUnattendedExecution(this.GetReferencedDataSource(dataSource)))
			{
				throw new InvalidDataSourceCredentialSettingException();
			}
			if (create)
			{
				this.AddDataSource(subscriptionID, dataSource);
				return;
			}
			this.m_service.Storage.DeleteDataSources(subscriptionID);
			this.AddDataSource(subscriptionID, dataSource);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00052140 File Offset: 0x00050340
		private void AddDataSource(Guid subscriptionID, DataSourceInfo dataSource)
		{
			if (dataSource.ReferenceByPath && !this.m_service.Storage.ObjectExists(this.m_service.CatalogToExternal(dataSource.DataSourceReference)))
			{
				throw new ItemNotFoundException(dataSource.DataSourceReference);
			}
			Guid guid;
			ItemType itemType;
			byte[] array;
			this.m_service.Storage.AddDataSource(Guid.Empty, subscriptionID, dataSource, this.m_service, null, out guid, out itemType, out array);
			if (dataSource.ReferenceByPath)
			{
				if (guid == Guid.Empty)
				{
					throw new ItemNotFoundException(dataSource.DataSourceReference);
				}
				if (itemType != ItemType.DataSource && itemType != ItemType.Model)
				{
					throw new WrongItemTypeException(dataSource.DataSourceReference);
				}
				if (!this.m_service.SecMgr.CheckAccess(ItemType.DataSource, array, DatasourceOperation.ReadProperties, this.m_service.CatalogToExternal(dataSource.DataSourceReference)))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				if (this.m_service.Storage.GetDataSourcesAndResolveModelLink(guid).GetTheOnlyDataSource().NeedPrompt)
				{
					throw new InvalidDataSourceCredentialSettingException();
				}
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00052238 File Offset: 0x00050438
		private void ValidateDataSettingsMappings(Microsoft.ReportingServices.Library.Soap.DataSetDefinition dataSet, ExtensionSettings extensionSettings, ParameterValueOrFieldReference[] parameters)
		{
			if (dataSet == null)
			{
				if (extensionSettings != null)
				{
					ParameterValueOrFieldReference[] parameterValues = extensionSettings.ParameterValues;
					for (int i = 0; i < parameterValues.Length; i++)
					{
						if (parameterValues[i] is ParameterFieldReference)
						{
							throw new InvalidParameterException("ExtensionSettings");
						}
					}
				}
				if (parameters != null)
				{
					for (int i = 0; i < parameters.Length; i++)
					{
						if (parameters[i] is ParameterFieldReference)
						{
							throw new InvalidParameterException("Parameters");
						}
					}
					return;
				}
			}
			else
			{
				Settings settings = new Settings();
				settings.FromSoapParameterValueArray(extensionSettings.ParameterValues);
				ParamValues paramValues = new ParamValues();
				paramValues.FromXml(ParameterValueOrFieldReference.ThisArrayToXml(parameters));
				Microsoft.ReportingServices.Library.DataSet dataSet2 = new Microsoft.ReportingServices.Library.DataSet();
				dataSet2.FromSoapDataSet(dataSet);
				foreach (object obj in settings.m_fields.Keys)
				{
					string text = (string)obj;
					if (dataSet2.Fields.FieldSet[text] == null)
					{
						throw new MissingElementException("Field");
					}
				}
				foreach (string text2 in paramValues.FieldKeys)
				{
					if (dataSet2.Fields.FieldSet[text2] == null)
					{
						throw new MissingElementException("Field");
					}
				}
			}
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00052380 File Offset: 0x00050580
		private void ValidateExtensionSettingsData(SubscriptionImpl subscription)
		{
			bool flag = this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.CreateAnySubscription, subscription.ItemPath);
			Settings settings = new Settings();
			settings.FromSoapParameterValueArray(subscription.m_extensionSettings.ParameterValues);
			IDeliveryExtension deliveryExtension = (IDeliveryExtension)ExtensionClassFactory.GetNewInstanceExtensionClass(subscription.DeliveryExtension, "Delivery");
			if (deliveryExtension != null)
			{
				try
				{
					Setting[] array = ProviderManager.InitDeliveryExtension(deliveryExtension, flag, this.m_service, subscription.ItemPath);
					Setting[] array2 = deliveryExtension.ValidateUserData(settings.SettingsArray);
					if (array2 != null)
					{
						foreach (Setting setting in array2)
						{
							if (setting.Error != null && setting.Error != string.Empty)
							{
								throw new InvalidExtensionParameter(setting.Error);
							}
						}
					}
					this.PrepareSubscriptionSettingsForStorage(array, settings, subscription);
					return;
				}
				catch (Exception ex)
				{
					throw new DeliveryErrorException(ex);
				}
			}
			throw new DeliveryExtensionNotFoundException();
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00052474 File Offset: 0x00050674
		private void MergeSubscriptionProperties(SubscriptionImpl subscription, string eventType, string matchData, ExtensionSettings extensionSettings, string description, ParameterValueOrFieldReference[] parameters, DataRetrievalPlan dataSettings, ItemType itemType = ItemType.Unknown)
		{
			if (extensionSettings != null)
			{
				subscription.m_extensionSettings = extensionSettings;
			}
			else
			{
				this.PrepareSubscriptionSettingsForClient(subscription, false);
			}
			if (dataSettings != null)
			{
				subscription.SetDataSettings(dataSettings);
			}
			subscription.m_description = description;
			if (!string.IsNullOrEmpty(eventType))
			{
				this.ChangeEventType(subscription, eventType);
				subscription.m_eventType = eventType;
				subscription.m_matchData = matchData;
			}
			if (itemType != ItemType.PowerBIReport)
			{
				parameters = this.ValidateSubscriptionParameters(subscription.ItemPath, (parameters != null) ? parameters : subscription.m_parameters, JobType.UserJobType);
				if (parameters != null)
				{
					subscription.m_parameters = parameters;
				}
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000524FC File Offset: 0x000506FC
		public void SetSubscriptionProperties(Guid id, string eventType, string matchData, ExtensionSettings extensionSettings, string description, ParameterValueOrFieldReference[] parameters, DataRetrievalPlan dataSettings, bool isDataDrivenCall)
		{
			SubscriptionImpl subscription = this.m_db.GetSubscription(id, this.m_service, false);
			if (!this.SubscriptionExists(subscription))
			{
				throw new SubscriptionNotFoundException(id.ToString());
			}
			if ((!subscription.IsDataDriven() && isDataDrivenCall) || (subscription.IsDataDriven() && !isDataDrivenCall))
			{
				throw new SubscriptionNotFoundException(id.ToString());
			}
			if (!this.UserHasUpdatePermission(subscription))
			{
				throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
			}
			subscription.m_connectionManager = this.ConnectionManager;
			this.m_service.SetExternalRoot(subscription.ReportName);
			string text = null;
			if (!string.IsNullOrEmpty(eventType))
			{
				text = matchData;
				matchData = Microsoft.ReportingServices.Diagnostics.Task.ConvertXmlCulture(matchData, Microsoft.ReportingServices.Diagnostics.Task.CultureConversion.ToNeutralCulture);
			}
			else
			{
				text = Microsoft.ReportingServices.Diagnostics.Task.ConvertXmlCulture(subscription.m_matchData, Microsoft.ReportingServices.Diagnostics.Task.CultureConversion.ToClientCulture);
			}
			this.MergeSubscriptionProperties(subscription, eventType, matchData, extensionSettings, description, parameters, dataSettings, ItemType.Unknown);
			if (Globals.Configuration.Extensions.Delivery[subscription.DeliveryExtension] == null)
			{
				throw new DeliveryExtensionNotFoundException();
			}
			if (!Globals.Configuration.EventTypes.ContainsKey(subscription.m_eventType))
			{
				throw new UnknownEventTypeException(subscription.m_eventType);
			}
			this.VerifyCanSubscribeToEvent(subscription.m_eventType, subscription.ItemPath);
			this.ValidateExtensionSettingsData(subscription);
			this.ValidateDataSettingsMappings(subscription.m_dataSet, subscription.m_extensionSettings, subscription.m_parameters);
			this.SetDataSourceInformation(subscription.ID, subscription.m_dataSource, false);
			CatalogItemContext catalogItemContext = new CatalogItemContext(this.m_service, subscription.ItemPath, "report");
			RSReportContext rsreportContext = new RSReportContext(this.m_service, catalogItemContext);
			rsreportContext.RetrieveProperties();
			if (rsreportContext.LinkID != Guid.Empty)
			{
				CatalogItemPath pathById = this.m_service.Storage.GetPathById(rsreportContext.LinkID);
				rsreportContext.ItemContext.SetReportDefinitionPath(this.m_service.CatalogToExternal(pathById));
			}
			rsreportContext.ThrowIfNotSubscribableByProperties(isDataDrivenCall);
			rsreportContext.RetrieveAllDataSources(true, false);
			rsreportContext.ThrowIfNotGoodForUnattended(false);
			if (subscription.m_inactiveFlags != InActiveFlags.Active && subscription.m_inactiveFlags != InActiveFlags.DisabledByUser)
			{
				if ((subscription.m_inactiveFlags & InActiveFlags.DeliveryProviderRemoved) > InActiveFlags.Active && ("" == subscription.DeliveryExtension || subscription.DeliveryExtension == null))
				{
					throw new CannotActivateSubscriptionException();
				}
				subscription.m_inactiveFlags = InActiveFlags.Active;
				try
				{
					RepLibRes.Culture = Localization.FallbackUICulture;
					subscription.LastStatus = RepLibRes.SubscriptionReady;
				}
				finally
				{
					RepLibRes.Culture = null;
				}
			}
			subscription.ModifiedBy = this.m_service.UserContext;
			subscription.m_modifiedDate = DateTime.Now;
			this.m_db.UpdateSubscription(subscription);
			this.ValidateSubscriptionData(subscription, subscription.m_eventType, text);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00052788 File Offset: 0x00050988
		public void SetCacheRefreshPlanProperties(Guid id, string eventType, string matchData, string description, ParameterValueOrFieldReference[] parameters)
		{
			SubscriptionImpl subscription = this.m_db.GetSubscription(id, this.m_service, true);
			if (!subscription.EventType.Equals(ReportScheduleActions.RefreshCache.ToString()) && !subscription.EventType.Equals(ReportScheduleActions.DataModelRefresh.ToString()))
			{
				throw new CacheRefreshPlanNotFoundException(id.ToString());
			}
			subscription.m_connectionManager = this.ConnectionManager;
			this.m_service.SetExternalRoot(subscription.ItemName);
			string text = null;
			if (!string.IsNullOrEmpty(eventType))
			{
				text = matchData;
				matchData = Microsoft.ReportingServices.Diagnostics.Task.ConvertXmlCulture(matchData, Microsoft.ReportingServices.Diagnostics.Task.CultureConversion.ToNeutralCulture);
			}
			else
			{
				text = Microsoft.ReportingServices.Diagnostics.Task.ConvertXmlCulture(subscription.m_matchData, Microsoft.ReportingServices.Diagnostics.Task.CultureConversion.ToClientCulture);
			}
			if (!Globals.Configuration.IsSupportedEvent(subscription.m_eventType))
			{
				throw new UnknownEventTypeException(subscription.m_eventType);
			}
			this.VerifyCanSubscribeToEvent(subscription.m_eventType, subscription.ItemPath);
			CatalogItem catalogItem = this.m_service.EnsureCacheRefreshPlanIsAllowed(subscription.ItemPath.Value);
			this.MergeSubscriptionProperties(subscription, eventType, matchData, null, description, parameters, null, catalogItem.ThisItemType);
			if (subscription.m_inactiveFlags != InActiveFlags.Active)
			{
				subscription.m_inactiveFlags = InActiveFlags.Active;
				try
				{
					RepLibRes.Culture = Localization.FallbackUICulture;
					subscription.LastStatus = RepLibRes.SubscriptionReady;
				}
				finally
				{
					RepLibRes.Culture = null;
				}
			}
			subscription.ModifiedBy = this.m_service.UserContext;
			subscription.m_modifiedDate = DateTime.Now;
			this.m_db.UpdateSubscription(subscription);
			this.ValidateSubscriptionData(subscription, subscription.m_eventType, text);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00052904 File Offset: 0x00050B04
		public void GetSubscriptionProperties(Guid id, out ExtensionSettings extensionSettings, out string description, out ActiveState active, out string status, out string owner, out string eventType, out string matchData, out ParameterValueOrFieldReference[] parameters, out DataRetrievalPlan dataSettings, bool lookingForDataDriven, IPathTranslator pathTranslator)
		{
			SubscriptionImpl subscription = this.m_db.GetSubscription(id, this.m_service, false);
			if ("TimedSubscription" != subscription.EventType && "SnapshotUpdated" != subscription.EventType)
			{
				throw new SubscriptionNotFoundException(id.ToString());
			}
			if (lookingForDataDriven && !subscription.IsDataDriven())
			{
				throw new SubscriptionNotFoundException(id.ToString());
			}
			if (!lookingForDataDriven && subscription.IsDataDriven())
			{
				throw new SubscriptionNotFoundException(id.ToString());
			}
			if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.ReadAnySubscription, subscription.ItemPath))
			{
				if (!this.IsSameUser(subscription.Owner, this.UserContext) || lookingForDataDriven)
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.ReadSubscription, subscription.ItemPath))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
			}
			if (subscription.DeliveryExtension != null && subscription.DeliveryExtension != string.Empty)
			{
				this.PrepareSubscriptionSettingsForClient(subscription, true);
				extensionSettings = subscription.m_extensionSettings;
			}
			else
			{
				extensionSettings = null;
			}
			description = subscription.m_description;
			status = subscription.LastStatus;
			eventType = subscription.m_eventType;
			matchData = Microsoft.ReportingServices.Diagnostics.Task.ConvertXmlCulture(subscription.m_matchData, Microsoft.ReportingServices.Diagnostics.Task.CultureConversion.ToClientCulture);
			matchData = Microsoft.ReportingServices.Diagnostics.Task.FromToXml(matchData);
			parameters = subscription.m_parameters;
			dataSettings = subscription.GetDataSettings();
			active = new ActiveState(subscription.ActiveState);
			owner = subscription.Owner.UserName;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00052AA4 File Offset: 0x00050CA4
		public void GetCacheRefreshPlanProperties(Guid id, out string description, out ActiveState state, out string status, out string eventType, out string matchData, out ParameterValueOrFieldReference[] parameters, IPathTranslator pathTranslator)
		{
			SubscriptionImpl subscription = this.m_db.GetSubscription(id, this.m_service, true);
			if ("RefreshCache" != subscription.EventType)
			{
				throw new CacheRefreshPlanNotFoundException(id.ToString());
			}
			if (!this.m_service.SecMgr.CheckAccess(subscription.m_itemType, subscription.m_securityDesc, ReportOperation.ReadPolicy, subscription.ItemPath))
			{
				throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
			}
			description = subscription.m_description;
			status = subscription.LastStatus;
			eventType = subscription.m_eventType;
			matchData = Microsoft.ReportingServices.Diagnostics.Task.ConvertXmlCulture(subscription.m_matchData, Microsoft.ReportingServices.Diagnostics.Task.CultureConversion.ToClientCulture);
			matchData = Microsoft.ReportingServices.Diagnostics.Task.FromToXml(matchData);
			parameters = subscription.m_parameters;
			state = subscription.ActiveState;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00052B64 File Offset: 0x00050D64
		public void DisableSubscription(Guid id)
		{
			SubscriptionImpl subscriptionForUpdate = this.GetSubscriptionForUpdate(id);
			if (!SubscriptionManager.IsSubscriptionDisabled(subscriptionForUpdate))
			{
				subscriptionForUpdate.m_inactiveFlags |= InActiveFlags.DisabledByUser;
				subscriptionForUpdate.LastStatus = RepLibRes.SubscriptionDisabled;
				this.m_db.UpdateSubscription(subscriptionForUpdate);
				if (this.m_tracer.TraceInfo)
				{
					this.m_tracer.Trace("Subscription {0} disabled at {1} by {2}", new object[]
					{
						id,
						DateTime.Now,
						this.UserName
					});
				}
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00052BEC File Offset: 0x00050DEC
		public void EnableSubscription(Guid id)
		{
			SubscriptionImpl subscriptionForUpdate = this.GetSubscriptionForUpdate(id);
			if (SubscriptionManager.IsSubscriptionDisabled(subscriptionForUpdate))
			{
				subscriptionForUpdate.m_inactiveFlags &= (InActiveFlags)(-129);
				subscriptionForUpdate.LastStatus = RepLibRes.SubscriptionReady;
				this.m_db.UpdateSubscription(subscriptionForUpdate);
				if (this.m_tracer.TraceInfo)
				{
					this.m_tracer.Trace("Subscription {0} enabled at {1} by {2}", new object[]
					{
						id,
						DateTime.Now,
						this.UserName
					});
				}
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00052C74 File Offset: 0x00050E74
		public void DeleteSubscription(Guid id, bool isCacheRefreshPlanExpected)
		{
			SubscriptionImpl subscription = this.m_db.GetSubscription(id, this.m_service, isCacheRefreshPlanExpected);
			if (isCacheRefreshPlanExpected && !subscription.EventType.Equals(ReportScheduleActions.RefreshCache.ToString()) && !subscription.EventType.Equals(ReportScheduleActions.DataModelRefresh.ToString()))
			{
				throw new CacheRefreshPlanNotFoundException(id.ToString());
			}
			if (!isCacheRefreshPlanExpected && "TimedSubscription" != subscription.EventType && "SnapshotUpdated" != subscription.EventType)
			{
				throw new SubscriptionNotFoundException(id.ToString());
			}
			if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.DeleteAnySubscription, subscription.ItemPath))
			{
				if (!this.IsSameUser(subscription.Owner, this.UserContext) || subscription.IsDataDriven())
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
				if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.DeleteSubscription, subscription.ItemPath))
				{
					throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
				}
			}
			this.m_db.DeleteSubscription(id);
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace("Subscription {0} deleted at {1} by {2}", new object[]
				{
					id,
					DateTime.Now,
					this.UserName
				});
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00052DE8 File Offset: 0x00050FE8
		private DataSourceInfo GetReferencedDataSource(DataSourceInfo dsReference)
		{
			if (!dsReference.ReferenceByPath)
			{
				return null;
			}
			ItemType itemType;
			Guid guid;
			byte[] array;
			if (!this.m_service.Storage.ObjectExists(this.m_service.CatalogToExternal(dsReference.DataSourceReference), out itemType, out guid, out array))
			{
				throw new ItemNotFoundException(dsReference.DataSourceReference);
			}
			RSService.EnsureItemType(itemType, dsReference.DataSourceReference, new ItemType[]
			{
				ItemType.DataSource,
				ItemType.Model
			});
			if (!this.m_service.SecMgr.CheckAccess(itemType, array, CommonOperation.ReadProperties, this.m_service.CatalogToExternal(dsReference.DataSourceReference)))
			{
				throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
			}
			return this.m_service.Storage.GetDataSourcesAndResolveModelLink(guid).GetTheOnlyDataSource();
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00052E98 File Offset: 0x00051098
		public Microsoft.ReportingServices.Library.Soap.DataSetDefinition PrepareQuery(Microsoft.ReportingServices.Library.Soap2005.DataSource dataSource, Microsoft.ReportingServices.Library.Soap.DataSetDefinition dataSet, out ReportParameter[] parameters, out bool changed)
		{
			changed = true;
			parameters = null;
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, Microsoft.ReportingServices.Library.Soap2005.DataSource.ThisToXml(dataSource));
			DataSourceInfo dataSourceInfo = DataSourceInfo.ParseDataSourceNode(xmlDocument.DocumentElement, false, true, DataProtection.Instance);
			if (dataSourceInfo.ReferenceByPath)
			{
				dataSourceInfo = this.GetReferencedDataSource(dataSourceInfo);
			}
			Microsoft.ReportingServices.Library.DataSet dataSet2 = new Microsoft.ReportingServices.Library.DataSet();
			dataSet2.FromSoapDataSet(dataSet);
			Microsoft.ReportingServices.Library.DataSet dataSet3 = new Microsoft.ReportingServices.Library.DataSet();
			global::System.Data.IDbConnection dbConnection2;
			Microsoft.ReportingServices.DataProcessing.IDbConnection dbConnection = this.m_service.OpenDataSourceConnection(dataSourceInfo, this.m_service.HowToCreateDataExtensionInstance, false, false, this.m_service.UserName, out dbConnection2);
			try
			{
				Microsoft.ReportingServices.DataProcessing.IDbCommand dbCommand = null;
				Microsoft.ReportingServices.DataProcessing.IDataReader dataReader = null;
				Microsoft.ReportingServices.DataProcessing.IDbCommand dbCommand2;
				dbCommand = (dbCommand2 = dbConnection.CreateCommand());
				try
				{
					if (dataSet.Query.TimeoutSpecified)
					{
						dbCommand.CommandTimeout = dataSet.Query.Timeout;
					}
					dbCommand.CommandText = dataSet2.Query.CommandText;
					dbCommand.CommandType = Microsoft.ReportingServices.DataProcessing.CommandType.Text;
					if (dataSourceInfo.IsModel)
					{
						IDbCommandAnalysis dbCommandAnalysis = dbCommand as IDbCommandAnalysis;
						if (dbCommandAnalysis != null)
						{
							Microsoft.ReportingServices.DataProcessing.IDataParameterCollection parameters2 = dbCommandAnalysis.GetParameters();
							if (parameters2 != null)
							{
								List<ReportParameter> list = new List<ReportParameter>();
								foreach (object obj in parameters2)
								{
									Microsoft.ReportingServices.DataProcessing.IDataParameter dataParameter = (Microsoft.ReportingServices.DataProcessing.IDataParameter)obj;
									list.Add(new ReportParameter
									{
										Name = dataParameter.ParameterName
									});
								}
								parameters = list.ToArray();
							}
						}
					}
					bool flag = false;
					try
					{
						dataReader = dbCommand.ExecuteReader(Microsoft.ReportingServices.DataProcessing.CommandBehavior.SchemaOnly);
						if (dataReader != null && dataReader.FieldCount == 0)
						{
							flag = true;
						}
					}
					catch (Exception ex)
					{
						SqlException ex2 = ex as SqlException;
						OleDbException ex3 = ex as OleDbException;
						if ((ex2 == null || 208 != ex2.Number) && (ex3 == null || 208 != ex3.Errors[0].NativeError))
						{
							throw;
						}
						flag = true;
					}
					if (flag)
					{
						if (this.m_tracer.TraceWarning)
						{
							this.m_tracer.Trace(TraceLevel.Warning, "Data-driven subscription query with SchemaOnly flag did not return schema information. Re-executing as full query.");
						}
						if (dataReader != null)
						{
							dataReader.Dispose();
						}
						dataReader = dbCommand.ExecuteReader(Microsoft.ReportingServices.DataProcessing.CommandBehavior.SingleResult);
					}
				}
				finally
				{
					if (dbCommand2 != null)
					{
						dbCommand2.Dispose();
					}
				}
				using (dataReader)
				{
					dataSet3.AccentSensitivity = dataSet2.AccentSensitivity;
					dataSet3.CaseSensitivity = dataSet2.CaseSensitivity;
					dataSet3.Collation = dataSet.Collation;
					dataSet3.KanatypeSensitivity = dataSet2.KanatypeSensitivity;
					dataSet3.Query = dataSet2.Query;
					dataSet3.WidthSensitivity = dataSet2.WidthSensitivity;
					int fieldCount = dataReader.FieldCount;
					for (int i = 0; i < fieldCount; i++)
					{
						string name = dataReader.GetName(i);
						if (dataSet3.Fields.FieldSet[name] == null)
						{
							dataSet3.Fields.FieldSet.Add(name, name);
						}
					}
				}
			}
			catch (RSException ex4)
			{
				throw ex4;
			}
			catch (Exception ex5)
			{
				throw new CannotPrepareQueryException(ex5);
			}
			finally
			{
				if (dbConnection != null)
				{
					dbConnection.Close();
				}
			}
			if (dataSet.Fields != null && dataSet3.Fields.FieldSet.Count == dataSet2.Fields.FieldSet.Count)
			{
				changed = false;
				foreach (object obj2 in dataSet3.Fields.FieldSet.Keys)
				{
					string text = (string)obj2;
					string text2 = dataSet3.Fields.FieldSet[text];
					if (dataSet2.Fields.FieldSet[text] != text2)
					{
						changed = true;
						break;
					}
				}
			}
			if (changed)
			{
				dataSet2 = dataSet3;
			}
			return dataSet2.ToSoapDataSet();
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000532DC File Offset: 0x000514DC
		public List<SubscriptionImpl> ListSubscriptions(string user, ExternalItemPath path, bool pathIsSiteOrFolder, SubscriptionType subscriptionType, bool includeEncryptedSettings = true)
		{
			List<SubscriptionImpl> list = this.m_db.ListSubscriptions(user, path, pathIsSiteOrFolder, this.m_service.UserContext.AuthenticationType, this.m_service, subscriptionType);
			List<SubscriptionImpl> list2 = new List<SubscriptionImpl>();
			foreach (SubscriptionImpl subscriptionImpl in list)
			{
				if (!subscriptionImpl.IsDataDriven() || Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DataDrivenSubscriptions))
				{
					if (SubscriptionType.CacheRefreshPlan != subscriptionType)
					{
						if (!this.CheckTimedSubscriptionAccess(subscriptionImpl, path, pathIsSiteOrFolder))
						{
							continue;
						}
						if (includeEncryptedSettings && !string.IsNullOrEmpty(subscriptionImpl.DeliveryExtension))
						{
							this.PrepareSubscriptionSettingsForClient(subscriptionImpl, true);
						}
						else
						{
							subscriptionImpl.m_extensionSettings = null;
						}
					}
					list2.Add(subscriptionImpl);
				}
			}
			return list2;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000533A4 File Offset: 0x000515A4
		public SubscriptionImpl GetSubscription(Guid id, bool includeEncryptedSettings = true)
		{
			SubscriptionImpl subscription = this.m_db.GetSubscription(id, this.m_service, false);
			if (subscription.EventType.Equals(ReportScheduleActions.TimedSubscription.ToString()))
			{
				if (!this.CheckTimedSubscriptionAccess(subscription, subscription.ItemPath, false))
				{
					throw new AccessDeniedException(this.UserContext.UserName, ErrorCode.rsAccessDenied);
				}
				if (includeEncryptedSettings && !string.IsNullOrEmpty(subscription.DeliveryExtension))
				{
					this.PrepareSubscriptionSettingsForClient(subscription, true);
				}
				else
				{
					subscription.m_extensionSettings = null;
				}
			}
			else if ((subscription.EventType.Equals(ReportScheduleActions.RefreshCache.ToString()) || subscription.EventType.Equals(ReportScheduleActions.DataModelRefresh.ToString())) && !this.m_service.SecMgr.CheckAccess(subscription.m_itemType, subscription.m_securityDesc, ReportOperation.ReadPolicy, subscription.ItemPath))
			{
				throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
			}
			return subscription;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00053498 File Offset: 0x00051698
		private bool CheckTimedSubscriptionAccess(SubscriptionImpl s, ExternalItemPath path, bool pathIsSiteOrFolder)
		{
			bool flag = !pathIsSiteOrFolder && ItemPathBase.IsNullOrEmpty(path);
			if (this.m_service.SecMgr.CheckAccess(ItemType.Report, s.m_securityDesc, ReportOperation.ReadAnySubscription, s.ItemPath))
			{
				return true;
			}
			if (!this.IsSameUser(s.Owner, this.UserContext) || s.IsDataDriven())
			{
				return false;
			}
			bool flag2 = this.m_service.SecMgr.CheckAccess(ItemType.Report, s.m_securityDesc, ReportOperation.ReadSubscription, s.ItemPath);
			if (!flag2 && !flag)
			{
				throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
			}
			return flag2;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00053528 File Offset: 0x00051728
		public List<SubscriptionImpl> ListSubscriptionsUsingDataSource(ExternalItemPath name, IPathTranslator pathTranslator)
		{
			List<SubscriptionImpl> list = this.m_db.ListSubscriptionsUsingDataSource(name, pathTranslator);
			List<SubscriptionImpl> list2 = new List<SubscriptionImpl>();
			foreach (SubscriptionImpl subscriptionImpl in list)
			{
				if (this.m_service.SecMgr.CheckAccess(ItemType.Report, subscriptionImpl.m_securityDesc, ReportOperation.ReadProperties, subscriptionImpl.ItemPath) && this.m_service.SecMgr.CheckAccess(ItemType.Report, subscriptionImpl.m_securityDesc, ReportOperation.ReadAnySubscription, subscriptionImpl.ItemPath))
				{
					if (!subscriptionImpl.IsDataDriven())
					{
						throw new InternalCatalogException("Non data driven subscription returned from ListSubscriptionsUsingDataSource");
					}
					if (!string.IsNullOrEmpty(subscriptionImpl.DeliveryExtension))
					{
						this.PrepareSubscriptionSettingsForClient(subscriptionImpl, true);
					}
					else
					{
						subscriptionImpl.m_extensionSettings = null;
					}
					list2.Add(subscriptionImpl);
				}
			}
			return list2;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00053604 File Offset: 0x00051804
		internal void ChangeSubscriptionOwner(Guid id, string newOwner)
		{
			SubscriptionImpl subscription = this.m_db.GetSubscription(id, this.m_service, false);
			RSService rsservice = new RSService(newOwner, this.m_service.UserContext.AuthenticationType, subscription.ItemPath.Value);
			bool flag;
			if (subscription.IsDataDriven())
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DataDrivenSubscriptions);
				flag = rsservice.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.UpdateAnySubscription, subscription.ItemPath);
			}
			else
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				flag = rsservice.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.UpdateSubscription, subscription.ItemPath);
			}
			if (this.m_service.UserName.Equals(subscription.Owner.UserName, StringComparison.OrdinalIgnoreCase))
			{
				if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.UpdateSubscription, subscription.ItemPath))
				{
					throw new AccessDeniedException(this.m_service.UserName, ErrorCode.rsAccessDenied);
				}
			}
			else if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.UpdateAnySubscription, subscription.ItemPath))
			{
				throw new AccessDeniedException(this.m_service.UserName, ErrorCode.rsAccessDenied);
			}
			if (!flag)
			{
				throw new UserCannotOwnSubscriptionException(string.Format(CultureInfo.InvariantCulture, "Subscription = {0}, User = {1}.", id, rsservice.UserContext.UserName));
			}
			subscription.m_owner = rsservice.UserContext;
			subscription.ModifiedBy = this.m_service.UserContext;
			this.m_db.UpdateSubscription(subscription);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00053783 File Offset: 0x00051983
		internal void InvalidateSubscription(Guid subscriptionId, InActiveFlags inactiveFlags, string status)
		{
			this.m_db.InvalidateSubscription(subscriptionId, inactiveFlags, status);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00053793 File Offset: 0x00051993
		internal void UpdateSubscriptionLastRunInfo(Guid subscriptionID, InActiveFlags stateFlag, DateTime lastRunTime, string status)
		{
			this.m_db.UpdateSubscriptionLastRunInfo(subscriptionID, stateFlag, lastRunTime, status);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x000537A5 File Offset: 0x000519A5
		internal void UpdateSubscriptionStatus(Guid subscriptionID, string status)
		{
			this.m_db.UpdateSubscriptionStatus(subscriptionID, status);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000537B4 File Offset: 0x000519B4
		private string GetDateString(DateTime date)
		{
			string text = "";
			if (date != DateTime.MinValue)
			{
				text = Globals.ToPublicDateTimeFormat(date);
			}
			return text;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000537DC File Offset: 0x000519DC
		private void PrepareSubscriptionSettingsForStorage(Setting[] settingsMetaData, Settings settings, SubscriptionImpl subscription)
		{
			if (settingsMetaData != null)
			{
				foreach (Setting setting in settingsMetaData)
				{
					if (setting.Encrypted)
					{
						SettingImpl settingImpl = settings[setting.Name];
						if (settingImpl != null && !settingImpl.UseField && settingImpl.Value != null && settingImpl.Value.Length > 0)
						{
							string text = CatalogEncryption.Instance.EncryptToString(settingImpl.Value, setting.Name);
							settingImpl.Value = text;
						}
					}
				}
			}
			subscription.m_extensionSettings.ParameterValues = settings.ToSoapParameterValueOrFieldReferenceArray();
			subscription.m_version = DataProtection.CurrentVersion;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00053874 File Offset: 0x00051A74
		private void PrepareSubscriptionSettingsForClient(SubscriptionImpl subscription, bool removePassword)
		{
			bool flag = this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.CreateAnySubscription, subscription.ItemPath);
			IDeliveryExtension deliveryExtension = (IDeliveryExtension)ExtensionClassFactory.GetNewInstanceExtensionClass(subscription.DeliveryExtension, "Delivery");
			if (deliveryExtension == null)
			{
				subscription.m_extensionSettings = null;
				return;
			}
			Setting[] array = ProviderManager.InitDeliveryExtension(deliveryExtension, flag, null, subscription.ItemPath);
			if (array == null)
			{
				return;
			}
			Settings settings = new Settings();
			settings.FromSoapParameterValueArray(subscription.m_extensionSettings.ParameterValues);
			SubscriptionManager.DecryptSubscriptionSettings(array, settings, removePassword, subscription.m_version);
			subscription.m_extensionSettings.ParameterValues = settings.ToSoapParameterValueOrFieldReferenceArray();
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0005390C File Offset: 0x00051B0C
		internal static void PrepareSubscriptionSettingsForUse(Setting[] settingsMetaData, ref Microsoft.ReportingServices.Library.Soap.ParameterValue[] values, int version)
		{
			if (settingsMetaData == null)
			{
				return;
			}
			Settings settings = new Settings();
			Settings settings2 = settings;
			ParameterValueOrFieldReference[] array = values;
			settings2.FromSoapParameterValueArray(array);
			SubscriptionManager.DecryptSubscriptionSettings(settingsMetaData, settings, false, version);
			values = settings.ToSoapParameterValueArray();
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00053940 File Offset: 0x00051B40
		private static void DecryptSubscriptionSettings(Setting[] settingsMetaData, Settings settings, bool removePasswords, int version)
		{
			foreach (Setting setting in settingsMetaData)
			{
				if (setting.IsPassword && removePasswords)
				{
					SettingImpl settingImpl = settings[setting.Name];
					if (settingImpl != null && !settingImpl.UseField)
					{
						settings.RemoveSetting(setting.Name);
					}
				}
				else if (setting.Encrypted)
				{
					SettingImpl settingImpl2 = settings[setting.Name];
					if (settingImpl2 != null && !settingImpl2.UseField)
					{
						string value = settingImpl2.Value;
						if (value != null && value.Length > 0)
						{
							try
							{
								string text = CatalogEncryption.Instance.DecryptToString(version, value, setting.Name);
								settingImpl2.Value = text;
							}
							catch (COMException ex)
							{
								if (ex.ErrorCode != -2146893819 || Globals.IsServiceProcess)
								{
									throw;
								}
								settings.RemoveSetting(setting.Name);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00053A2C File Offset: 0x00051C2C
		internal ParameterValueOrFieldReference[] ValidateSubscriptionParameters(ExternalItemPath reportName, ParameterValueOrFieldReference[] subscriptionParameters, JobType jobType)
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(this.m_service, reportName, "reportName");
			ItemParameterDefinition itemParameterDefinition;
			try
			{
				itemParameterDefinition = ItemParameterDefinition.Load(catalogItemContext, null, true, this.m_service, SecurityRequirements.None);
			}
			finally
			{
			}
			bool flag = ExecutionOptions.IsSnapshotExecution(itemParameterDefinition.ExecutionOptions);
			DatasourceCredentialsCollection datasourceCredentialsCollection = new DatasourceCredentialsCollection();
			catalogItemContext.RSRequestParameters.DatasourcesCred = datasourceCredentialsCollection;
			NameValueCollection nameValueCollection = null;
			Hashtable hashtable = new Hashtable();
			if (subscriptionParameters != null)
			{
				nameValueCollection = new NameValueCollection(subscriptionParameters.Length);
				foreach (ParameterValueOrFieldReference parameterValueOrFieldReference in subscriptionParameters)
				{
					Microsoft.ReportingServices.Library.Soap.ParameterValue parameterValue = parameterValueOrFieldReference as Microsoft.ReportingServices.Library.Soap.ParameterValue;
					if (parameterValue != null)
					{
						nameValueCollection.Add(parameterValue.Name, parameterValue.Value);
					}
					else
					{
						ParameterFieldReference parameterFieldReference = parameterValueOrFieldReference as ParameterFieldReference;
						if (parameterFieldReference != null)
						{
							if (hashtable.Contains(parameterFieldReference.ParameterName))
							{
								throw new NotYetSupportedException();
							}
							hashtable.Add(parameterFieldReference.ParameterName, null);
						}
					}
				}
			}
			bool flag2 = false;
			ParameterInfoCollection parameterInfoCollection;
			using (this.m_service.SetStreamFactory(new MemoryThenFileStreamFactory()))
			{
				if (ItemType.DataSet == itemParameterDefinition.Type)
				{
					parameterInfoCollection = this.m_service.GetDataSetParameters(itemParameterDefinition, nameValueCollection, jobType);
					flag2 = true;
				}
				else
				{
					parameterInfoCollection = this.m_service.GetReportParametersForRendering(catalogItemContext, itemParameterDefinition.ReportId, itemParameterDefinition.LinkId, itemParameterDefinition.SnapshotExecutionDate, new CatalogSessionParameterStorage(null, itemParameterDefinition), nameValueCollection, null, null, jobType);
				}
			}
			ParamValues paramValues = new ParamValues();
			paramValues.FromXml(ParameterValueOrFieldReference.ThisArrayToXml(subscriptionParameters));
			parameterInfoCollection.ValidateInputValues(paramValues, flag, flag2);
			return ParameterValueOrFieldReference.XmlToThisArray(paramValues.ToXml(true), false);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00053BC4 File Offset: 0x00051DC4
		private bool IsSameUser(UserContext user1, UserContext user2)
		{
			return Security.IsSameUser(user1, user2);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00053BD0 File Offset: 0x00051DD0
		private bool UserHasUpdatePermission(SubscriptionImpl subscription)
		{
			if (!subscription.IsDataDriven())
			{
				if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.UpdateAnySubscription, subscription.ItemPath))
				{
					if (!this.IsSameUser(subscription.Owner, this.UserContext))
					{
						return false;
					}
					if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.UpdateSubscription, subscription.ItemPath))
					{
						return false;
					}
				}
			}
			else if (!this.m_service.SecMgr.CheckAccess(ItemType.Report, subscription.m_securityDesc, ReportOperation.UpdateAnySubscription, subscription.ItemPath))
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00053C63 File Offset: 0x00051E63
		private bool SubscriptionExists(SubscriptionImpl subscription)
		{
			return !("TimedSubscription" != subscription.EventType) || !("SnapshotUpdated" != subscription.EventType);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00053C8C File Offset: 0x00051E8C
		private SubscriptionImpl GetSubscriptionForUpdate(Guid id)
		{
			SubscriptionImpl subscription = this.m_db.GetSubscription(id, this.m_service, false);
			if (!this.UserHasUpdatePermission(subscription))
			{
				throw new AccessDeniedException(this.UserName, ErrorCode.rsAccessDenied);
			}
			if (!this.SubscriptionExists(subscription))
			{
				throw new SubscriptionNotFoundException(id.ToString());
			}
			return subscription;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00053CE1 File Offset: 0x00051EE1
		private static bool IsSubscriptionDisabled(SubscriptionImpl subscription)
		{
			return subscription.ActiveState.DisabledByUserSpecified && subscription.ActiveState.DisabledByUser;
		}

		// Token: 0x0400078D RID: 1933
		private SubscriptionDB m_db = new SubscriptionDB();

		// Token: 0x0400078E RID: 1934
		private RSTrace m_tracer = RSTrace.SubscriptionTracer;

		// Token: 0x0400078F RID: 1935
		private RSService m_service;
	}
}
