using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Library.Soap2010;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001FF RID: 511
	internal class ReportingService2005Impl
	{
		// Token: 0x0600113F RID: 4415 RVA: 0x0003BD83 File Offset: 0x00039F83
		internal ReportingService2005Impl(RSService service, GetExceptionForEndpoint getExceptionForEndpoint)
		{
			this.m_reportingService = service;
			this.m_getExceptionForEndpoint = getExceptionForEndpoint;
			Global.m_Tracer.Assert(this.m_getExceptionForEndpoint != null, "m_getExceptionForEndpoint is null");
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0003BDB1 File Offset: 0x00039FB1
		internal RSService Service
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_reportingService;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0003BDB9 File Offset: 0x00039FB9
		protected GetExceptionForEndpoint GetExceptionForEndpoint
		{
			get
			{
				return this.m_getExceptionForEndpoint;
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0003BDC4 File Offset: 0x00039FC4
		internal void CreateBatch(out string BatchID)
		{
			try
			{
				BatchID = this.Service.CreateBatch().ToString();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0003BE0C File Offset: 0x0003A00C
		internal void CancelBatch(Guid batchId)
		{
			try
			{
				this.Service.CancelBatch(batchId);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003BE48 File Offset: 0x0003A048
		internal void ExecuteBatch(Guid batchId)
		{
			try
			{
				this.Service.ExecuteBatch(batchId);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0003BE84 File Offset: 0x0003A084
		internal void GetSystemProperties(Property[] Properties, out Property[] Values)
		{
			try
			{
				GetSystemPropertiesAction getSystemPropertiesAction = this.Service.GetSystemPropertiesAction;
				getSystemPropertiesAction.ActionParameters.RequestedProperties = Properties;
				getSystemPropertiesAction.Execute();
				Values = getSystemPropertiesAction.ActionParameters.SystemProperties;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0003BEDC File Offset: 0x0003A0DC
		internal void SetSystemProperties(Property[] Properties)
		{
			try
			{
				SetSystemPropertiesAction setSystemPropertiesAction = this.Service.SetSystemPropertiesAction;
				setSystemPropertiesAction.ActionParameters.SystemProperties = Properties;
				setSystemPropertiesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003BF28 File Offset: 0x0003A128
		internal void GetUserSettings(Property[] Properties, out Property[] Values)
		{
			try
			{
				GetUserSettingsAction getUserSettingsAction = this.Service.GetUserSettingsAction;
				getUserSettingsAction.ActionParameters.RequestedProperties = Properties;
				getUserSettingsAction.Execute();
				Values = getUserSettingsAction.ActionParameters.UserProperties;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0003BF80 File Offset: 0x0003A180
		internal void SetUserSettings(Property[] Properties)
		{
			try
			{
				SetUserSettingsAction setUserSettingsAction = this.Service.SetUserSettingsAction;
				setUserSettingsAction.ActionParameters.Properties = Properties;
				setUserSettingsAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003BFCC File Offset: 0x0003A1CC
		internal void DeleteItem(string Item)
		{
			this.DeleteItem(Item, Guid.Empty);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0003BFDC File Offset: 0x0003A1DC
		internal void DeleteItem(string Item, Guid batchId)
		{
			try
			{
				DeleteItemAction deleteItemAction = this.Service.DeleteItemAction;
				deleteItemAction.BatchID = batchId;
				deleteItemAction.ActionParameters.ItemPath = Item;
				deleteItemAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0003C02C File Offset: 0x0003A22C
		internal void MoveItem(string Item, string Target)
		{
			this.MoveItem(Item, Target, Guid.Empty);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0003C03C File Offset: 0x0003A23C
		internal void MoveItem(string Item, string Target, Guid batchId)
		{
			try
			{
				MoveItemAction moveItemAction = this.Service.MoveItemAction;
				moveItemAction.BatchID = batchId;
				moveItemAction.ActionParameters.SourceItemPath = Item;
				moveItemAction.ActionParameters.TargetItemPath = Target;
				moveItemAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003C098 File Offset: 0x0003A298
		internal void ListChildren(string Item, bool Recursive, out CatalogItemList CatalogItems)
		{
			try
			{
				ListChildrenAction listChildrenAction = this.Service.ListChildrenAction;
				listChildrenAction.ActionParameters.ItemPath = Item;
				listChildrenAction.ActionParameters.Recursive = Recursive;
				listChildrenAction.Execute();
				CatalogItems = listChildrenAction.ActionParameters.Children;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003C0FC File Offset: 0x0003A2FC
		internal void ListDependentItems(string Item, out CatalogItemList CatalogItems)
		{
			try
			{
				ListDependentItemsAction listDependentItemsAction = this.Service.ListDependentItemsAction;
				listDependentItemsAction.ActionParameters.ItemPath = Item;
				listDependentItemsAction.Execute();
				CatalogItems = listDependentItemsAction.ActionParameters.DependentItems;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0003C154 File Offset: 0x0003A354
		internal void GetProperties(string Item, Property[] Properties, ItemNamespaceEnum itemNamespace, out Property[] Values)
		{
			try
			{
				GetPropertiesAction getPropertiesAction = this.Service.GetPropertiesAction;
				getPropertiesAction.ActionParameters.ItemNamespace = itemNamespace;
				getPropertiesAction.ActionParameters.ItemPath = Item;
				getPropertiesAction.ActionParameters.RequestedProperties = Properties;
				getPropertiesAction.Execute();
				Values = getPropertiesAction.ActionParameters.PropertyValues;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003C1C8 File Offset: 0x0003A3C8
		internal void SetProperties(string Item, Property[] Properties)
		{
			this.SetProperties(Item, Properties, Guid.Empty);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0003C1D8 File Offset: 0x0003A3D8
		internal void SetProperties(string Item, Property[] Properties, Guid batchId)
		{
			try
			{
				SetPropertiesAction setPropertiesAction = this.Service.SetPropertiesAction;
				setPropertiesAction.BatchID = batchId;
				setPropertiesAction.ActionParameters.ItemPath = Item;
				setPropertiesAction.ActionParameters.Properties = Properties;
				setPropertiesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003C234 File Offset: 0x0003A434
		internal void GetItemType(string Item, out ItemType Type)
		{
			try
			{
				GetItemTypeAction getItemTypeAction = this.Service.GetItemTypeAction;
				getItemTypeAction.ActionParameters.ItemPath = Item;
				getItemTypeAction.Execute();
				Type = (ItemType)getItemTypeAction.ActionParameters.ItemType;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003C28C File Offset: 0x0003A48C
		internal void CreateFolder(string Folder, string Parent, Property[] Properties)
		{
			this.CreateFolder(Folder, Parent, Properties);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003C298 File Offset: 0x0003A498
		internal void CreateFolder(string Folder, string Parent, Property[] Properties, Guid batchId)
		{
			try
			{
				CreateFolderAction createFolderAction = this.Service.CreateFolderAction;
				createFolderAction.BatchID = batchId;
				createFolderAction.ActionParameters.ItemName = Folder;
				createFolderAction.ActionParameters.ParentPath = Parent;
				createFolderAction.ActionParameters.Properties = Properties;
				createFolderAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0003C304 File Offset: 0x0003A504
		internal void CreateReport(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, out Warning[] Warnings)
		{
			this.CreateReport(Report, Parent, Overwrite, Definition, Properties, Guid.Empty, out Warnings);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0003C31C File Offset: 0x0003A51C
		internal void CreateReport(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, Guid batchId, out Warning[] Warnings)
		{
			try
			{
				CreateReportAction createReportAction = this.Service.CreateReportAction;
				createReportAction.BatchID = batchId;
				createReportAction.ActionParameters.ItemName = Report;
				createReportAction.ActionParameters.ParentPath = Parent;
				createReportAction.ActionParameters.Overwrite = Overwrite;
				createReportAction.ActionParameters.ReportDefinition = Definition;
				createReportAction.ActionParameters.Properties = Properties;
				createReportAction.Execute();
				Warnings = createReportAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0003C3B0 File Offset: 0x0003A5B0
		internal void GetReportDefinition(string Report, ItemType itemType, out byte[] Definition)
		{
			try
			{
				GetReportDefinitionAction getReportDefinitionAction = this.Service.GetReportDefinitionAction;
				getReportDefinitionAction.ActionParameters.ItemPath = Report;
				getReportDefinitionAction.ActionParameters.ItemType = itemType;
				getReportDefinitionAction.Execute();
				Definition = getReportDefinitionAction.ActionParameters.ReportDefinition;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0003C414 File Offset: 0x0003A614
		internal void SetReportDefinition(string Report, byte[] Definition, out Warning[] Warnings)
		{
			this.SetReportDefinition(Report, Definition, Guid.Empty, out Warnings);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003C424 File Offset: 0x0003A624
		internal void SetReportDefinition(string Report, byte[] Definition, Guid batchId, out Warning[] Warnings)
		{
			try
			{
				SetReportDefinitionAction setReportDefinitionAction = this.Service.SetReportDefinitionAction;
				setReportDefinitionAction.BatchID = batchId;
				setReportDefinitionAction.ActionParameters.ItemPath = Report;
				setReportDefinitionAction.ActionParameters.Definition = Definition;
				setReportDefinitionAction.Execute();
				Warnings = setReportDefinitionAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0003C490 File Offset: 0x0003A690
		internal void SetRdlxReportDefinition(string Report, byte[] Definition, out Warning[] Warnings)
		{
			this.SetRdlxReportDefinition(Report, Definition, Guid.Empty, out Warnings);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003C4A0 File Offset: 0x0003A6A0
		internal void SetRdlxReportDefinition(string Report, byte[] Definition, Guid batchId, out Warning[] Warnings)
		{
			try
			{
				SetRdlxReportDefinitionAction setRdlxReportDefinitionAction = this.Service.SetRdlxReportDefinitionAction;
				setRdlxReportDefinitionAction.BatchID = batchId;
				setRdlxReportDefinitionAction.ActionParameters.ItemPath = Report;
				setRdlxReportDefinitionAction.ActionParameters.Definition = Definition;
				setRdlxReportDefinitionAction.Execute();
				Warnings = setRdlxReportDefinitionAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0003C50C File Offset: 0x0003A70C
		internal void CreateResource(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties)
		{
			this.CreateResource(Resource, Parent, Overwrite, Contents, MimeType, Properties, Guid.Empty);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0003C524 File Offset: 0x0003A724
		internal void CreateResource(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties, Guid batchId)
		{
			try
			{
				CreateResourceAction createResourceAction = this.Service.CreateResourceAction;
				createResourceAction.BatchID = batchId;
				createResourceAction.ActionParameters.ItemName = Resource;
				createResourceAction.ActionParameters.ParentPath = Parent;
				createResourceAction.ActionParameters.Overwrite = Overwrite;
				createResourceAction.ActionParameters.Content = Contents;
				createResourceAction.ActionParameters.MimeType = MimeType;
				createResourceAction.ActionParameters.Properties = Properties;
				createResourceAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
		internal void SetResourceContents(string Resource, byte[] Contents, string MimeType)
		{
			this.SetResourceContents(Resource, Contents, MimeType);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0003C5C0 File Offset: 0x0003A7C0
		internal void SetResourceContents(string Resource, byte[] Contents, string MimeType, Guid batchId)
		{
			try
			{
				SetResourceContentsAction setResourceContentsAction = this.Service.SetResourceContentsAction;
				setResourceContentsAction.BatchID = batchId;
				setResourceContentsAction.ActionParameters.ItemPath = Resource;
				setResourceContentsAction.ActionParameters.Definition = Contents;
				setResourceContentsAction.ActionParameters.MimeType = MimeType;
				setResourceContentsAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0003C62C File Offset: 0x0003A82C
		internal void GetResourceContents(string Resource, out byte[] Contents, out string MimeType)
		{
			try
			{
				GetResourceContentsAction getResourceContentsAction = this.Service.GetResourceContentsAction;
				getResourceContentsAction.ActionParameters.ItemPath = Resource;
				getResourceContentsAction.Execute();
				Contents = getResourceContentsAction.ActionParameters.Content;
				MimeType = getResourceContentsAction.ActionParameters.MimeType;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0003C694 File Offset: 0x0003A894
		internal void GetReportParameters(string Report, string HistoryID, bool ForRendering, Microsoft.ReportingServices.Library.Soap.ParameterValue[] Values, DataSourceCredentials[] Credentials, out ParameterInfoCollection Parameters)
		{
			try
			{
				GetReportParametersAction getReportParametersAction = this.Service.GetReportParametersAction;
				getReportParametersAction.ActionParameters.ItemPath = Report;
				getReportParametersAction.ActionParameters.HistoryID = HistoryID;
				getReportParametersAction.ActionParameters.ForRendering = ForRendering;
				getReportParametersAction.ActionParameters.ParameterValidationValues = Microsoft.ReportingServices.Library.Soap.ParameterValue.ThisArrayToNameValueCollection(Values);
				getReportParametersAction.ActionParameters.DatasourceCredentials = DataSourceCredentials.ThisArrayToDatasourcesCredentials(Credentials);
				getReportParametersAction.Execute();
				Parameters = getReportParametersAction.ActionParameters.Parameters;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0003C72C File Offset: 0x0003A92C
		internal void SetReportParameters(string Report, ParameterInfoCollection Parameters)
		{
			this.SetReportParameters(Report, Parameters, Guid.Empty);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0003C73C File Offset: 0x0003A93C
		internal void SetReportParameters(string Report, ParameterInfoCollection Parameters, Guid batchId)
		{
			try
			{
				SetReportParametersAction setReportParametersAction = this.Service.SetReportParametersAction;
				setReportParametersAction.BatchID = batchId;
				setReportParametersAction.ActionParameters.ItemPath = Report;
				setReportParametersAction.ActionParameters.Parameters = Parameters;
				setReportParametersAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003C798 File Offset: 0x0003A998
		internal void CreateLinkedItem(string ItemPath, string Parent, string Link, Property[] Properties)
		{
			this.CreateLinkedItem(ItemPath, Parent, Link, Properties, Guid.Empty);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0003C7AC File Offset: 0x0003A9AC
		internal void CreateLinkedItem(string ItemPath, string Parent, string Link, Property[] Properties, Guid batchId)
		{
			try
			{
				CreateLinkedReportAction createLinkedReportAction = this.Service.CreateLinkedReportAction;
				createLinkedReportAction.BatchID = batchId;
				createLinkedReportAction.ActionParameters.ItemName = ItemPath;
				createLinkedReportAction.ActionParameters.ParentPath = Parent;
				createLinkedReportAction.ActionParameters.LinkPath = Link;
				createLinkedReportAction.ActionParameters.Properties = Properties;
				createLinkedReportAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0003C824 File Offset: 0x0003AA24
		internal void GetItemLink(string ItemPath, out string Link)
		{
			try
			{
				GetReportLinkAction getReportLinkAction = this.Service.GetReportLinkAction;
				getReportLinkAction.ActionParameters.ReportPath = ItemPath;
				getReportLinkAction.Execute();
				Link = getReportLinkAction.ActionParameters.LinkPath;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003C87C File Offset: 0x0003AA7C
		internal void SetItemLink(string ItemPath, string Link)
		{
			this.SetItemLink(ItemPath, Link, Guid.Empty);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0003C88C File Offset: 0x0003AA8C
		internal void SetItemLink(string ItemPath, string Link, Guid batchId)
		{
			try
			{
				SetReportLinkAction setReportLinkAction = this.Service.SetReportLinkAction;
				setReportLinkAction.BatchID = batchId;
				setReportLinkAction.ActionParameters.ReportPath = ItemPath;
				setReportLinkAction.ActionParameters.LinkPath = Link;
				setReportLinkAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0003C8E8 File Offset: 0x0003AAE8
		internal void GetRenderResource(string Format, string DeviceInfo, out byte[] Result, out string MimeType)
		{
			try
			{
				if (Format == null || Format.Length == 0)
				{
					throw new MissingParameterException("Format");
				}
				CatalogItemContext catalogItemContext = new CatalogItemContext(this.Service);
				catalogItemContext.RSRequestParameters.SetCatalogParameters(null);
				catalogItemContext.RSRequestParameters.FormatParamValue = Format;
				catalogItemContext.RSRequestParameters.SetRenderingParameters(DeviceInfo);
				Result = this.Service.GetRenderResource(catalogItemContext, out MimeType);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0003C96C File Offset: 0x0003AB6C
		internal void SetExecutionOptions(string Report, ExecutionSettingEnum ExecutionSetting, [XmlElement(typeof(ScheduleDefinition))] [XmlElement(typeof(ScheduleReference))] [XmlElement(typeof(NoSchedule))] ScheduleDefinitionOrReference Schedule)
		{
			this.SetExecutionOptions(Report, ExecutionSetting, Schedule, Guid.Empty);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x0003C97C File Offset: 0x0003AB7C
		internal void SetExecutionOptions(string Report, ExecutionSettingEnum ExecutionSetting, [XmlElement(typeof(ScheduleDefinition))] [XmlElement(typeof(ScheduleReference))] [XmlElement(typeof(NoSchedule))] ScheduleDefinitionOrReference Schedule, Guid batchId)
		{
			try
			{
				if (ExecutionSetting != ExecutionSettingEnum.Live)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ExecutionSnapshots);
				}
				if (Schedule != null)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				}
				SetExecutionOptionsAction setExecutionOptionsAction = this.Service.SetExecutionOptionsAction;
				setExecutionOptionsAction.BatchID = batchId;
				setExecutionOptionsAction.ActionParameters.ReportPath = Report;
				setExecutionOptionsAction.ActionParameters.ExecutionSettings = ExecutionSetting;
				setExecutionOptionsAction.ActionParameters.Schedule = Schedule;
				setExecutionOptionsAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		internal void GetExecutionOptions(string Report, out ExecutionSettingEnum ExecutionSetting, [XmlElement(typeof(ScheduleDefinition))] [XmlElement(typeof(ScheduleReference))] [XmlElement(typeof(NoSchedule))] out ScheduleDefinitionOrReference Schedule)
		{
			try
			{
				GetExecutionOptionsAction getExecutionOptionsAction = this.Service.GetExecutionOptionsAction;
				getExecutionOptionsAction.ActionParameters.ReportPath = Report;
				getExecutionOptionsAction.Execute();
				ExecutionSetting = getExecutionOptionsAction.ActionParameters.ExecutionSettings;
				Schedule = getExecutionOptionsAction.ActionParameters.Schedule;
				if (ExecutionSetting != ExecutionSettingEnum.Live)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ExecutionSnapshots);
				}
				if (Schedule != null)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0003CA9C File Offset: 0x0003AC9C
		internal void SetCacheOptions(string Report, bool CacheReport, [XmlElement(typeof(TimeExpiration))] [XmlElement(typeof(ScheduleExpiration))] ExpirationDefinition Expiration)
		{
			this.SetCacheOptions(Report, CacheReport, Expiration, Guid.Empty);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0003CAAC File Offset: 0x0003ACAC
		internal void SetCacheOptions(string Report, bool CacheReport, [XmlElement(typeof(TimeExpiration))] [XmlElement(typeof(ScheduleExpiration))] ExpirationDefinition Expiration, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Caching);
				SetCacheOptionsAction setCacheOptionsAction = this.Service.SetCacheOptionsAction;
				setCacheOptionsAction.BatchID = batchId;
				setCacheOptionsAction.ActionParameters.ReportPath = Report;
				setCacheOptionsAction.ActionParameters.CacheReport = CacheReport;
				setCacheOptionsAction.ActionParameters.Expiration = Expiration;
				setCacheOptionsAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0003CB28 File Offset: 0x0003AD28
		internal void GetCacheOptions(string Report, out bool CacheReport, [XmlElement(typeof(TimeExpiration))] [XmlElement(typeof(ScheduleExpiration))] out ExpirationDefinition Expiration)
		{
			try
			{
				GetCacheOptionsAction getCacheOptionsAction = this.Service.GetCacheOptionsAction;
				getCacheOptionsAction.ActionParameters.ReportPath = Report;
				getCacheOptionsAction.Execute();
				CacheReport = getCacheOptionsAction.ActionParameters.CacheReport;
				Expiration = getCacheOptionsAction.ActionParameters.Expiration;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003CB90 File Offset: 0x0003AD90
		internal void UpdateItemExecutionSnapshot(string ItemPath)
		{
			this.UpdateItemExecutionSnapshot(ItemPath, Guid.Empty);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003CBA0 File Offset: 0x0003ADA0
		internal void UpdateItemExecutionSnapshot(string ItemPath, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ExecutionSnapshots);
				UpdateExecutionSnapshotAction updateExecutionSnapshotAction = this.Service.UpdateExecutionSnapshotAction;
				updateExecutionSnapshotAction.BatchID = batchId;
				updateExecutionSnapshotAction.ActionParameters.ReportPath = ItemPath;
				updateExecutionSnapshotAction.ActionParameters.JobType = JobType.UserJobType;
				using (this.Service.SetStreamFactory(new MemoryThenFileStreamFactory()))
				{
					updateExecutionSnapshotAction.Execute();
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0003CC3C File Offset: 0x0003AE3C
		internal void FlushCache(string Report)
		{
			this.FlushCache(Report, Guid.Empty);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0003CC4C File Offset: 0x0003AE4C
		internal void FlushCache(string Report, Guid batchId)
		{
			try
			{
				FlushCacheAction flushCacheAction = this.Service.FlushCacheAction;
				flushCacheAction.BatchID = batchId;
				flushCacheAction.ActionParameters.ItemPath = Report;
				flushCacheAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		internal void ListJobs(out ICollection<RunningJobContext> Jobs)
		{
			try
			{
				ListRunningJobsAction listRunningJobsAction = this.Service.ListRunningJobsAction;
				listRunningJobsAction.Execute();
				Jobs = listRunningJobsAction.ActionParameters.Jobs;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0003CCE8 File Offset: 0x0003AEE8
		internal bool CancelJob(string JobID)
		{
			bool cancelled;
			try
			{
				CancelJobAction cancelJobAction = this.Service.CancelJobAction;
				cancelJobAction.ActionParameters.JobID = JobID;
				cancelJobAction.Execute();
				cancelled = cancelJobAction.ActionParameters.Cancelled;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
			return cancelled;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0003CD40 File Offset: 0x0003AF40
		internal void CreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties)
		{
			this.CreateDataSource(DataSource, Parent, Overwrite, Definition, Properties, Guid.Empty);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0003CD54 File Offset: 0x0003AF54
		internal void CreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, Guid batchId)
		{
			try
			{
				CreateDataSourceAction createDataSourceAction = this.Service.CreateDataSourceAction;
				createDataSourceAction.BatchID = batchId;
				createDataSourceAction.ActionParameters.ItemName = DataSource;
				createDataSourceAction.ActionParameters.ParentPath = Parent;
				createDataSourceAction.ActionParameters.Overwrite = Overwrite;
				createDataSourceAction.ActionParameters.DataSourceDefinition = Definition;
				createDataSourceAction.ActionParameters.Properties = Properties;
				createDataSourceAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0003CDD8 File Offset: 0x0003AFD8
		internal void GetDataSourceContents(string DataSource, out DataSourceDefinition Definition)
		{
			try
			{
				GetDataSourceContentsAction getDataSourceContentsAction = this.Service.GetDataSourceContentsAction;
				getDataSourceContentsAction.ActionParameters.DataSourcePath = DataSource;
				getDataSourceContentsAction.Execute();
				Definition = getDataSourceContentsAction.ActionParameters.DataSourceDefinition;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0003CE30 File Offset: 0x0003B030
		internal void SetDataSourceContents(string DataSource, DataSourceDefinition Definition)
		{
			this.SetDataSourceContents(DataSource, Definition, Guid.Empty);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0003CE40 File Offset: 0x0003B040
		internal void SetDataSourceContents(string DataSource, DataSourceDefinition Definition, Guid batchId)
		{
			try
			{
				SetDataSourceContentsAction setDataSourceContentsAction = this.Service.SetDataSourceContentsAction;
				setDataSourceContentsAction.BatchID = batchId;
				setDataSourceContentsAction.ActionParameters.ItemPath = DataSource;
				setDataSourceContentsAction.ActionParameters.DataSourceDefinition = Definition;
				setDataSourceContentsAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0003CE9C File Offset: 0x0003B09C
		internal void EnableDataSource(string DataSource)
		{
			this.EnableDataSource(DataSource, Guid.Empty);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0003CEAC File Offset: 0x0003B0AC
		internal void EnableDataSource(string DataSource, Guid batchId)
		{
			try
			{
				ChangeDataSourceStateAction changeDataSourceStateAction = this.Service.ChangeDataSourceStateAction;
				changeDataSourceStateAction.BatchID = batchId;
				changeDataSourceStateAction.ActionParameters.DataSourcePath = DataSource;
				changeDataSourceStateAction.ActionParameters.Enable = true;
				changeDataSourceStateAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0003CF08 File Offset: 0x0003B108
		internal void DisableDataSource(string DataSource)
		{
			this.DisableDataSource(DataSource, Guid.Empty);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0003CF18 File Offset: 0x0003B118
		internal void DisableDataSource(string DataSource, Guid batchId)
		{
			try
			{
				ChangeDataSourceStateAction changeDataSourceStateAction = this.Service.ChangeDataSourceStateAction;
				changeDataSourceStateAction.BatchID = batchId;
				changeDataSourceStateAction.ActionParameters.DataSourcePath = DataSource;
				changeDataSourceStateAction.ActionParameters.Enable = false;
				changeDataSourceStateAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0003CF74 File Offset: 0x0003B174
		internal void SetItemDataSources(string Item, Microsoft.ReportingServices.Library.Soap2005.DataSource[] DataSources)
		{
			this.SetItemDataSources(Item, DataSources, Guid.Empty);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0003CF84 File Offset: 0x0003B184
		internal void SetItemDataSources(string Item, Microsoft.ReportingServices.Library.Soap2005.DataSource[] DataSources, Guid batchId)
		{
			try
			{
				SetItemDataSourcesAction setItemDataSourcesAction = this.Service.SetItemDataSourcesAction;
				setItemDataSourcesAction.BatchID = batchId;
				setItemDataSourcesAction.ActionParameters.ItemPath = Item;
				setItemDataSourcesAction.ActionParameters.ItemDataSources = DataSources;
				setItemDataSourcesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0003CFE0 File Offset: 0x0003B1E0
		internal void GetItemDataSources(string Item, out Microsoft.ReportingServices.Library.Soap2005.DataSource[] DataSources)
		{
			try
			{
				GetItemDataSourcesAction getItemDataSourcesAction = this.Service.GetItemDataSourcesAction;
				getItemDataSourcesAction.ActionParameters.ItemPath = Item;
				getItemDataSourcesAction.Execute();
				DataSources = getItemDataSourcesAction.ActionParameters.DataSources;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003D038 File Offset: 0x0003B238
		internal void GetItemDataSourcePrompts(string Item, out DataSourcePrompt[] DataSourcePrompts)
		{
			try
			{
				GetItemDataSourcePromptsAction getItemDataSourcePromptsAction = this.Service.GetItemDataSourcePromptsAction;
				getItemDataSourcePromptsAction.ActionParameters.ItemPath = Item;
				getItemDataSourcePromptsAction.Execute();
				DataSourcePrompts = getItemDataSourcePromptsAction.ActionParameters.DataSourcePrompts;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0003D090 File Offset: 0x0003B290
		internal void CreateItemHistorySnapshot(string Report, out string HistoryID, out Warning[] Warnings)
		{
			this.CreateItemHistorySnapshot(Report, Guid.Empty, out HistoryID, out Warnings);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003D0A0 File Offset: 0x0003B2A0
		internal void CreateItemHistorySnapshot(string Report, Guid batchId, out string HistoryID, out Warning[] Warnings)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				CreateSnapshotAction createSnapshotAction = this.Service.CreateSnapshotAction;
				createSnapshotAction.BatchID = batchId;
				createSnapshotAction.ActionParameters.ReportPath = Report;
				createSnapshotAction.ActionParameters.JobType = JobType.UserJobType;
				using (this.Service.SetStreamFactory(new MemoryThenFileStreamFactory()))
				{
					createSnapshotAction.Execute();
				}
				HistoryID = createSnapshotAction.ActionParameters.HistoryID;
				Warnings = createSnapshotAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003D158 File Offset: 0x0003B358
		internal void SetItemHistoryOptions(string ItemPath, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, [XmlElement(typeof(ScheduleDefinition))] [XmlElement(typeof(ScheduleReference))] [XmlElement(typeof(NoSchedule))] ScheduleDefinitionOrReference Schedule)
		{
			this.SetItemHistoryOptions(ItemPath, EnableManualSnapshotCreation, KeepExecutionSnapshots, Schedule, Guid.Empty);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0003D16C File Offset: 0x0003B36C
		internal void SetItemHistoryOptions(string ItemPath, bool EnableManualSnapshotCreation, bool KeepExecutionSnapshots, [XmlElement(typeof(ScheduleDefinition))] [XmlElement(typeof(ScheduleReference))] [XmlElement(typeof(NoSchedule))] ScheduleDefinitionOrReference Schedule, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				if (Schedule != null)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				}
				SetReportHistoryOptionsAction setReportHistoryOptionsAction = this.Service.SetReportHistoryOptionsAction;
				setReportHistoryOptionsAction.BatchID = batchId;
				setReportHistoryOptionsAction.ActionParameters.ReportPath = ItemPath;
				setReportHistoryOptionsAction.ActionParameters.ManualCreationEnabled = EnableManualSnapshotCreation;
				setReportHistoryOptionsAction.ActionParameters.KeepExecutionSnapshots = KeepExecutionSnapshots;
				setReportHistoryOptionsAction.ActionParameters.Schedule = Schedule;
				setReportHistoryOptionsAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003D208 File Offset: 0x0003B408
		internal void GetItemHistoryOptions(string ItemPath, out bool EnableManualSnapshotCreation, out bool KeepExecutionSnapshots, [XmlElement(typeof(ScheduleDefinition))] [XmlElement(typeof(ScheduleReference))] [XmlElement(typeof(NoSchedule))] out ScheduleDefinitionOrReference Schedule)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				GetReportHistoryOptionsAction getReportHistoryOptionsAction = this.Service.GetReportHistoryOptionsAction;
				getReportHistoryOptionsAction.ActionParameters.ReportPath = ItemPath;
				getReportHistoryOptionsAction.Execute();
				EnableManualSnapshotCreation = getReportHistoryOptionsAction.ActionParameters.ManualCreationEnabled;
				KeepExecutionSnapshots = getReportHistoryOptionsAction.ActionParameters.KeepExecutionSnapshots;
				Schedule = getReportHistoryOptionsAction.ActionParameters.Schedule;
				if (Schedule != null)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
		internal void SetItemHistoryLimit(string ItemPath, bool UseSystem, int HistoryLimit)
		{
			this.SetItemHistoryLimit(ItemPath, UseSystem, HistoryLimit, Guid.Empty);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
		internal void SetItemHistoryLimit(string ItemPath, bool UseSystem, int HistoryLimit, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				SetSnapshotLimitAction setSnapshotLimitAction = this.Service.SetSnapshotLimitAction;
				setSnapshotLimitAction.BatchID = batchId;
				setSnapshotLimitAction.ActionParameters.ReportPath = ItemPath;
				setSnapshotLimitAction.ActionParameters.UseSystem = UseSystem;
				setSnapshotLimitAction.ActionParameters.ScopedLimit = HistoryLimit;
				setSnapshotLimitAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0003D32C File Offset: 0x0003B52C
		internal void GetItemHistoryLimit(string ItemPath, out int HistoryLimit, out bool IsSystem, out int SystemLimit)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				GetSnapshotLimitAction getSnapshotLimitAction = this.Service.GetSnapshotLimitAction;
				getSnapshotLimitAction.ActionParameters.ReportPath = ItemPath;
				getSnapshotLimitAction.Execute();
				HistoryLimit = getSnapshotLimitAction.ActionParameters.ScopedLimit;
				IsSystem = getSnapshotLimitAction.ActionParameters.UseSystem;
				SystemLimit = getSnapshotLimitAction.ActionParameters.SystemLimit;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0003D3B0 File Offset: 0x0003B5B0
		internal void ListReportHistory(string Report, out ReportHistorySnapshot[] ReportHistory)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				ListHistoryAction listHistoryAction = this.Service.ListHistoryAction;
				listHistoryAction.ActionParameters.ReportPath = Report;
				listHistoryAction.Execute();
				ReportHistory = listHistoryAction.ActionParameters.ReportHistory;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0003D418 File Offset: 0x0003B618
		internal void DeleteItemHistorySnapshot(string ItemPath, string HistoryID)
		{
			this.DeleteItemHistorySnapshot(ItemPath, HistoryID, Guid.Empty);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0003D428 File Offset: 0x0003B628
		internal void DeleteItemHistorySnapshot(string ItemPath, string HistoryID, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				DeleteSnapshotAction deleteSnapshotAction = this.Service.DeleteSnapshotAction;
				deleteSnapshotAction.BatchID = batchId;
				deleteSnapshotAction.ActionParameters.ReportPath = ItemPath;
				deleteSnapshotAction.ActionParameters.SnapshotID = HistoryID;
				deleteSnapshotAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0003D494 File Offset: 0x0003B694
		internal void FindItems(string Folder, BooleanOperatorEnum BooleanOperator, Property[] SearchOptions, Microsoft.ReportingServices.Library.Soap2010.SearchCondition[] Conditions, ServerCompatLevel compatLevel, out CatalogItemList Items)
		{
			try
			{
				if (Folder == null)
				{
					throw new MissingParameterException("Folder");
				}
				if (Conditions == null)
				{
					throw new MissingParameterException("Conditions");
				}
				Items = this.Service.FindItems(Folder, BooleanOperator, SearchOptions, Conditions, compatLevel);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0003D4F4 File Offset: 0x0003B6F4
		internal void CreateSchedule(string Name, ScheduleDefinition ScheduleDefinition, string Site, out string ScheduleID)
		{
			this.CreateSchedule(Name, ScheduleDefinition, Site, Guid.Empty, out ScheduleID);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0003D508 File Offset: 0x0003B708
		internal void CreateSchedule(string Name, ScheduleDefinition ScheduleDefinition, string Site, Guid batchId, out string ScheduleID)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				CreateScheduleAction createScheduleAction = this.Service.CreateScheduleAction;
				createScheduleAction.BatchID = batchId;
				createScheduleAction.ActionParameters.Name = Name;
				createScheduleAction.ActionParameters.ScheduleDefinition = ScheduleDefinition;
				createScheduleAction.ActionParameters.Site = Site;
				createScheduleAction.Execute();
				ScheduleID = createScheduleAction.ActionParameters.ScheduleID;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0003D594 File Offset: 0x0003B794
		internal void DeleteSchedule(string ScheduleID)
		{
			this.DeleteSchedule(ScheduleID, Guid.Empty);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		internal void DeleteSchedule(string ScheduleID, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				DeleteScheduleAction deleteScheduleAction = this.Service.DeleteScheduleAction;
				deleteScheduleAction.BatchID = batchId;
				deleteScheduleAction.ActionParameters.ScheduleID = ScheduleID;
				deleteScheduleAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0003D604 File Offset: 0x0003B804
		internal void SetScheduleProperties(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition)
		{
			this.SetScheduleProperties(Name, ScheduleID, ScheduleDefinition, Guid.Empty);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0003D614 File Offset: 0x0003B814
		internal void SetScheduleProperties(string Name, string ScheduleID, ScheduleDefinition ScheduleDefinition, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				SetSchedulePropertiesAction setSchedulePropertiesAction = this.Service.SetSchedulePropertiesAction;
				setSchedulePropertiesAction.BatchID = batchId;
				setSchedulePropertiesAction.ActionParameters.Name = Name;
				setSchedulePropertiesAction.ActionParameters.ScheduleID = ScheduleID;
				setSchedulePropertiesAction.ActionParameters.ScheduleDefinition = ScheduleDefinition;
				setSchedulePropertiesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0003D690 File Offset: 0x0003B890
		internal void GetScheduleProperties(string ScheduleID, out Microsoft.ReportingServices.Library.Soap.Schedule Schedule)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				GetSchedulePropertiesAction getSchedulePropertiesAction = this.Service.GetSchedulePropertiesAction;
				getSchedulePropertiesAction.ActionParameters.ScheduleID = ScheduleID;
				getSchedulePropertiesAction.Execute();
				Schedule = getSchedulePropertiesAction.ActionParameters.Schedule;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003D6F8 File Offset: 0x0003B8F8
		internal void ListScheduledReports(string ScheduleID, out CatalogItemList Reports)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				ListScheduledReportsAction listScheduledReportsAction = this.Service.ListScheduledReportsAction;
				listScheduledReportsAction.ActionParameters.ScheduleID = ScheduleID;
				listScheduledReportsAction.Execute();
				Reports = listScheduledReportsAction.ActionParameters.Children;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0003D760 File Offset: 0x0003B960
		internal void ListSchedules(string Site, out Microsoft.ReportingServices.Library.Soap.Schedule[] Schedules)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				ListSchedulesAction listSchedulesAction = this.Service.ListSchedulesAction;
				listSchedulesAction.ActionParameters.Site = Site;
				listSchedulesAction.Execute();
				Schedules = listSchedulesAction.ActionParameters.Children;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0003D7C8 File Offset: 0x0003B9C8
		internal void PauseSchedule(string ScheduleID)
		{
			this.PauseSchedule(ScheduleID, Guid.Empty);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
		internal void PauseSchedule(string ScheduleID, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				PauseScheduleAction pauseScheduleAction = this.Service.PauseScheduleAction;
				pauseScheduleAction.BatchID = batchId;
				pauseScheduleAction.ActionParameters.ScheduleID = ScheduleID;
				pauseScheduleAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0003D838 File Offset: 0x0003BA38
		internal void ResumeSchedule(string ScheduleID)
		{
			this.ResumeSchedule(ScheduleID, Guid.Empty);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003D848 File Offset: 0x0003BA48
		internal void ResumeSchedule(string ScheduleID, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				ResumeScheduleAction resumeScheduleAction = this.Service.ResumeScheduleAction;
				resumeScheduleAction.BatchID = batchId;
				resumeScheduleAction.ActionParameters.ScheduleID = ScheduleID;
				resumeScheduleAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003D8A8 File Offset: 0x0003BAA8
		internal void CreateSubscription(string Report, ExtensionSettings ExtensionSettings, bool isDataDriven, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, out string SubscriptionID)
		{
			this.CreateSubscription(Report, ExtensionSettings, isDataDriven, DataRetrievalPlan, Description, EventType, MatchData, Parameters, Guid.Empty, out SubscriptionID);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		internal void CreateSubscription(string Report, ExtensionSettings ExtensionSettings, bool isDataDriven, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, Guid batchId, out string SubscriptionID)
		{
			try
			{
				if (isDataDriven)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DataDrivenSubscriptions);
				}
				else
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				}
				CreateSubscriptionAction createSubscriptionAction = this.Service.CreateSubscriptionAction;
				createSubscriptionAction.BatchID = batchId;
				createSubscriptionAction.ActionParameters.Report = Report;
				createSubscriptionAction.ActionParameters.IsDataDriven = isDataDriven;
				createSubscriptionAction.ActionParameters.ExtensionSettings = ExtensionSettings;
				createSubscriptionAction.ActionParameters.DataSettings = DataRetrievalPlan;
				createSubscriptionAction.ActionParameters.Description = Description;
				createSubscriptionAction.ActionParameters.EventType = EventType;
				createSubscriptionAction.ActionParameters.MatchData = MatchData;
				createSubscriptionAction.ActionParameters.Parameters = Parameters;
				createSubscriptionAction.Execute();
				SubscriptionID = createSubscriptionAction.ActionParameters.SubscriptionID;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003D9B4 File Offset: 0x0003BBB4
		internal void SetSubscriptionProperties(string SubscriptionID, ExtensionSettings ExtensionSettings, bool isDataDriven, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters)
		{
			this.SetSubscriptionProperties(SubscriptionID, ExtensionSettings, isDataDriven, DataRetrievalPlan, Description, EventType, MatchData, Parameters, Guid.Empty);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0003D9DC File Offset: 0x0003BBDC
		internal void SetSubscriptionProperties(string SubscriptionID, ExtensionSettings ExtensionSettings, bool isDataDriven, DataRetrievalPlan DataRetrievalPlan, string Description, string EventType, string MatchData, ParameterValueOrFieldReference[] Parameters, Guid batchId)
		{
			try
			{
				if (isDataDriven)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DataDrivenSubscriptions);
				}
				else
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				}
				SetSubscriptionPropertiesAction setSubscriptionPropertiesAction = this.Service.SetSubscriptionPropertiesAction;
				setSubscriptionPropertiesAction.BatchID = batchId;
				setSubscriptionPropertiesAction.ActionParameters.SubscriptionID = SubscriptionID;
				setSubscriptionPropertiesAction.ActionParameters.ExtensionSettings = ExtensionSettings;
				setSubscriptionPropertiesAction.ActionParameters.IsDataDriven = isDataDriven;
				setSubscriptionPropertiesAction.ActionParameters.DataSettings = DataRetrievalPlan;
				setSubscriptionPropertiesAction.ActionParameters.Description = Description;
				setSubscriptionPropertiesAction.ActionParameters.EventType = EventType;
				setSubscriptionPropertiesAction.ActionParameters.MatchData = MatchData;
				setSubscriptionPropertiesAction.ActionParameters.Parameters = Parameters;
				setSubscriptionPropertiesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0003DAB0 File Offset: 0x0003BCB0
		internal void GetSubscriptionProperties(string SubscriptionID, bool LookingForDataDriven, out string Owner, out ExtensionSettings ExtensionSettings, out DataRetrievalPlan DataRetrievalPlan, out string Description, out ActiveState Active, out string Status, out string EventType, out string MatchData, out ParameterValueOrFieldReference[] Parameters)
		{
			try
			{
				if (LookingForDataDriven)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DataDrivenSubscriptions);
				}
				else
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				}
				GetSubscriptionPropertiesAction getSubscriptionPropertiesAction = this.Service.GetSubscriptionPropertiesAction;
				getSubscriptionPropertiesAction.ActionParameters.SubscriptionID = SubscriptionID;
				getSubscriptionPropertiesAction.ActionParameters.LookingForDataDriven = LookingForDataDriven;
				getSubscriptionPropertiesAction.Execute();
				Owner = getSubscriptionPropertiesAction.ActionParameters.Owner;
				ExtensionSettings = getSubscriptionPropertiesAction.ActionParameters.ExtensionSettings;
				DataRetrievalPlan = getSubscriptionPropertiesAction.ActionParameters.DataSettings;
				Description = getSubscriptionPropertiesAction.ActionParameters.Description;
				Active = getSubscriptionPropertiesAction.ActionParameters.Active;
				Status = getSubscriptionPropertiesAction.ActionParameters.Status;
				EventType = getSubscriptionPropertiesAction.ActionParameters.EventType;
				MatchData = getSubscriptionPropertiesAction.ActionParameters.MatchData;
				Parameters = getSubscriptionPropertiesAction.ActionParameters.Parameters;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0003DBAC File Offset: 0x0003BDAC
		internal void DeleteSubscription(string SubscriptionID)
		{
			this.DeleteSubscription(SubscriptionID, Guid.Empty);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0003DBBC File Offset: 0x0003BDBC
		internal void DeleteSubscription(string SubscriptionID, Guid batchId)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				DeleteSubscriptionAction deleteSubscriptionAction = this.Service.DeleteSubscriptionAction;
				deleteSubscriptionAction.BatchID = batchId;
				deleteSubscriptionAction.ActionParameters.SubscriptionID = SubscriptionID;
				deleteSubscriptionAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003DC20 File Offset: 0x0003BE20
		internal void PrepareQuery(Microsoft.ReportingServices.Library.Soap2005.DataSource DataSource, DataSetDefinition DataSet, out DataSetDefinition DataSettings, out bool Changed, out string[] ParameterNames)
		{
			try
			{
				if (DataSource == null)
				{
					throw new MissingParameterException("DataSource");
				}
				if (DataSet == null)
				{
					throw new MissingParameterException("DataSet");
				}
				if (DataSource.Item == null)
				{
					throw new MissingParameterException("Item");
				}
				DataSourceReference dataSourceReference = DataSource.Item as DataSourceReference;
				if (dataSourceReference != null)
				{
					if (dataSourceReference.Reference == null)
					{
						throw new MissingElementException("Reference");
					}
					if (dataSourceReference.Reference == string.Empty)
					{
						throw new InvalidItemNameException(dataSourceReference.Reference, CatalogItemNameUtility.MaxItemNameLength);
					}
				}
				ReportParameter[] array = null;
				DataSettings = this.Service.PrepareQuery(DataSource, DataSet, out array, out Changed);
				if (array == null || array.Length == 0)
				{
					ParameterNames = null;
				}
				else
				{
					ParameterNames = new string[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						ParameterNames[i] = array[i].Name;
					}
				}
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003DD0C File Offset: 0x0003BF0C
		internal void GetExtensionSettings(string Extension, out ExtensionParameter[] ExtensionParameters)
		{
			try
			{
				if (Extension == null)
				{
					throw new MissingParameterException("Extension");
				}
				ExtensionParameters = this.Service.GetExtensionSettings(Extension);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003DD58 File Offset: 0x0003BF58
		internal void ValidateExtensionSettings(string Extension, ParameterValueOrFieldReference[] ParameterValues, string path, out ExtensionParameter[] ParameterErrors)
		{
			try
			{
				if (Extension == null)
				{
					throw new MissingParameterException("Extension");
				}
				Subscription.CheckParameterArray(ParameterValues, "ParameterValues");
				ParameterErrors = this.Service.ValidateExtensionSettings(Extension, ParameterValues, path);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0003DDB0 File Offset: 0x0003BFB0
		internal void ListSubscriptions(string Path, bool pathIsSite, string Owner, out SubscriptionImpl[] SubscriptionItems)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				ListSubscriptionsAction listSubscriptionsAction = this.Service.ListSubscriptionsAction;
				if (Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions))
				{
					listSubscriptionsAction.ActionParameters.Path = Path;
					listSubscriptionsAction.ActionParameters.PathIsSiteOrFolder = pathIsSite;
					listSubscriptionsAction.ActionParameters.Owner = Owner;
					listSubscriptionsAction.ActionParameters.SubscriptionType = SubscriptionType.ReportSubscription;
					listSubscriptionsAction.Execute();
				}
				SubscriptionItems = listSubscriptionsAction.ActionParameters.Children;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003DE54 File Offset: 0x0003C054
		internal void ListSubscriptionsUsingDataSource(string DataSource, out SubscriptionImpl[] SubscriptionItems)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				if (DataSource == null)
				{
					throw new MissingParameterException("DataSource");
				}
				SubscriptionImpl[] array = this.Service.ListSubscriptionsUsingDataSource(DataSource);
				SubscriptionItems = array;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003DEB0 File Offset: 0x0003C0B0
		internal void ListExtensions(ExtensionTypeEnum ExtensionType, out Microsoft.ReportingServices.Library.Soap2005.Extension[] Extensions)
		{
			try
			{
				Extensions = this.Service.ListExtensions(ExtensionType);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		internal void ListEvents(out Microsoft.ReportingServices.Library.Soap.Event[] Events)
		{
			try
			{
				ListEventsAction listEventsAction = this.Service.ListEventsAction;
				listEventsAction.Execute();
				Events = listEventsAction.ActionParameters.Events;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0003DF38 File Offset: 0x0003C138
		internal void FireEvent(string EventType, string EventData)
		{
			this.FireEvent(EventType, EventData, Guid.Empty);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0003DF48 File Offset: 0x0003C148
		internal void FireEvent(string EventType, string EventData, Guid batchId)
		{
			try
			{
				if (string.Compare(EventType, "TimedSubscription", StringComparison.OrdinalIgnoreCase) == 0)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				}
				FireEventAction fireEventAction = this.Service.FireEventAction;
				fireEventAction.BatchID = batchId;
				fireEventAction.ActionParameters.EventType = EventType;
				fireEventAction.ActionParameters.EventData = EventData;
				fireEventAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003DFC4 File Offset: 0x0003C1C4
		internal void ListTasks(SecurityScopeEnum SecurityScope, out Microsoft.ReportingServices.Library.Soap.Task[] Tasks)
		{
			try
			{
				ListTasksAction listTasksAction = this.Service.ListTasksAction;
				listTasksAction.ActionParameters.Scope = SecurityScope;
				listTasksAction.Execute();
				Tasks = listTasksAction.ActionParameters.Tasks;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0003E01C File Offset: 0x0003C21C
		internal void ListRoles(SecurityScopeEnum SecurityScope, string Path, out Role[] Roles)
		{
			try
			{
				ListRolesAction listRolesAction = this.Service.ListRolesAction;
				listRolesAction.ActionParameters.Scope = SecurityScope;
				listRolesAction.ActionParameters.ItemPath = Path;
				listRolesAction.Execute();
				Roles = listRolesAction.ActionParameters.Roles;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0003E080 File Offset: 0x0003C280
		internal void CreateRole(string Name, string Description, Microsoft.ReportingServices.Library.Soap.Task[] Tasks)
		{
			this.CreateRole(Name, Description, Tasks, Guid.Empty);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0003E090 File Offset: 0x0003C290
		internal void CreateRole(string Name, string Description, Microsoft.ReportingServices.Library.Soap.Task[] Tasks, Guid batchId)
		{
			try
			{
				CreateRoleAction createRoleAction = this.Service.CreateRoleAction;
				createRoleAction.BatchID = batchId;
				createRoleAction.ActionParameters.RoleName = Name;
				createRoleAction.ActionParameters.Description = Description;
				createRoleAction.ActionParameters.TaskIDs = Microsoft.ReportingServices.Library.Soap.Task.TaskIDArrayFromTaskArray(Tasks);
				createRoleAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0003E100 File Offset: 0x0003C300
		internal void DeleteRole(string Name)
		{
			this.DeleteRole(Name, Guid.Empty);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0003E110 File Offset: 0x0003C310
		internal void DeleteRole(string Name, Guid batchId)
		{
			try
			{
				DeleteRoleAction deleteRoleAction = this.Service.DeleteRoleAction;
				deleteRoleAction.BatchID = batchId;
				deleteRoleAction.ActionParameters.RoleName = Name;
				deleteRoleAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0003E160 File Offset: 0x0003C360
		internal void GetRoleProperties(string Name, out Microsoft.ReportingServices.Library.Soap.Task[] Tasks, out string Description)
		{
			try
			{
				GetRolePropertiesAction getRolePropertiesAction = this.Service.GetRolePropertiesAction;
				getRolePropertiesAction.ActionParameters.RoleName = Name;
				getRolePropertiesAction.Execute();
				Tasks = getRolePropertiesAction.ActionParameters.Tasks;
				Description = getRolePropertiesAction.ActionParameters.Description;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0003E1C8 File Offset: 0x0003C3C8
		internal void SetRoleProperties(string Name, string Description, Microsoft.ReportingServices.Library.Soap.Task[] Tasks)
		{
			this.SetRoleProperties(Name, Description, Tasks, Guid.Empty);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0003E1D8 File Offset: 0x0003C3D8
		internal void SetRoleProperties(string Name, string Description, Microsoft.ReportingServices.Library.Soap.Task[] Tasks, Guid batchId)
		{
			try
			{
				SetRolePropertiesAction setRolePropertiesAction = this.Service.SetRolePropertiesAction;
				setRolePropertiesAction.BatchID = batchId;
				setRolePropertiesAction.ActionParameters.RoleName = Name;
				setRolePropertiesAction.ActionParameters.Description = Description;
				setRolePropertiesAction.ActionParameters.TaskIDs = Microsoft.ReportingServices.Library.Soap.Task.TaskIDArrayFromTaskArray(Tasks);
				setRolePropertiesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0003E248 File Offset: 0x0003C448
		internal void GetSystemPolicies(out Policy[] Policies)
		{
			try
			{
				GetSystemPoliciesAction getSystemPoliciesAction = this.Service.GetSystemPoliciesAction;
				getSystemPoliciesAction.Execute();
				Policies = getSystemPoliciesAction.ActionParameters.Policies;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003E294 File Offset: 0x0003C494
		internal void SetSystemPolicies(Policy[] Policies)
		{
			this.SetSystemPolicies(Policies, Guid.Empty);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0003E2A4 File Offset: 0x0003C4A4
		internal void SetSystemPolicies(Policy[] Policies, Guid batchId)
		{
			try
			{
				SetSystemPoliciesAction setSystemPoliciesAction = this.Service.SetSystemPoliciesAction;
				setSystemPoliciesAction.BatchID = batchId;
				setSystemPoliciesAction.ActionParameters.Policies = Policy.PolicyArrayToXml(Policies);
				setSystemPoliciesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003E2FC File Offset: 0x0003C4FC
		internal void GetPolicies(string Item, out Policy[] Policies, out bool InheritParent)
		{
			try
			{
				GetPoliciesAction getPoliciesAction = this.Service.GetPoliciesAction;
				getPoliciesAction.ActionParameters.ItemPath = Item;
				getPoliciesAction.Execute();
				Policies = getPoliciesAction.ActionParameters.Policies;
				InheritParent = getPoliciesAction.ActionParameters.InheritParent;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0003E364 File Offset: 0x0003C564
		internal void SetPolicies(string Item, Policy[] Policies)
		{
			this.SetPolicies(Item, Policies, Guid.Empty);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003E374 File Offset: 0x0003C574
		internal void SetPolicies(string Item, Policy[] Policies, Guid batchId)
		{
			try
			{
				SetPoliciesAction setPoliciesAction = this.Service.SetPoliciesAction;
				setPoliciesAction.BatchID = batchId;
				setPoliciesAction.ActionParameters.ItemPath = Item;
				setPoliciesAction.ActionParameters.Policies = Policy.PolicyArrayToXml(Policies);
				setPoliciesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
		internal void InheritParentSecurity(string Item)
		{
			this.InheritParentSecurity(Item, Guid.Empty);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003E3E8 File Offset: 0x0003C5E8
		internal void InheritParentSecurity(string Item, Guid batchId)
		{
			try
			{
				DeletePoliciesAction deletePoliciesAction = this.Service.DeletePoliciesAction;
				deletePoliciesAction.BatchID = batchId;
				deletePoliciesAction.ActionParameters.ItemPath = Item;
				deletePoliciesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003E438 File Offset: 0x0003C638
		internal void GetSystemPermissions([XmlArrayItem("Operation")] out string[] Permissions)
		{
			try
			{
				GetSystemPermissionsAction getSystemPermissionsAction = this.Service.GetSystemPermissionsAction;
				getSystemPermissionsAction.Execute();
				Permissions = Permission.StringCollectionToThisArray(getSystemPermissionsAction.ActionParameters.Operations);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0003E48C File Offset: 0x0003C68C
		internal void GetPermissions(string Item, [XmlArrayItem("Operation")] out string[] Permissions)
		{
			try
			{
				GetPermissionsAction getPermissionsAction = this.Service.GetPermissionsAction;
				getPermissionsAction.ActionParameters.ItemPath = Item;
				getPermissionsAction.Execute();
				Permissions = Permission.StringCollectionToThisArray(getPermissionsAction.ActionParameters.Operations);
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0003E4EC File Offset: 0x0003C6EC
		internal void CreateModel(string Model, string Parent, byte[] Definition, Property[] Properties, out Warning[] Warnings)
		{
			this.CreateModel(Model, Parent, Definition, Properties, Guid.Empty, out Warnings);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003E500 File Offset: 0x0003C700
		internal void CreateModel(string Model, string Parent, byte[] Definition, Property[] Properties, Guid batchId, out Warning[] Warnings)
		{
			try
			{
				CreateModelAction createModelAction = this.Service.CreateModelAction;
				createModelAction.BatchID = batchId;
				createModelAction.ActionParameters.ItemName = Model;
				createModelAction.ActionParameters.ParentPath = Parent;
				createModelAction.ActionParameters.ModelDefinition = Definition;
				createModelAction.ActionParameters.Properties = Properties;
				createModelAction.Execute();
				Warnings = createModelAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0003E588 File Offset: 0x0003C788
		internal void GetModelDefinition(string Model, out byte[] Definition)
		{
			try
			{
				GetModelDefinitionAction getModelDefinitionAction = this.Service.GetModelDefinitionAction;
				getModelDefinitionAction.ActionParameters.ItemPath = Model;
				getModelDefinitionAction.Execute();
				Definition = getModelDefinitionAction.ActionParameters.ModelDefinition;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003E5E0 File Offset: 0x0003C7E0
		internal void SetModelDefinition(string Model, byte[] Definition, out Warning[] Warnings)
		{
			this.SetModelDefinition(Model, Definition, Guid.Empty, out Warnings);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		internal void SetModelDefinition(string Model, byte[] Definition, Guid batchId, out Warning[] Warnings)
		{
			try
			{
				SetModelDefinitionAction setModelDefinitionAction = this.Service.SetModelDefinitionAction;
				setModelDefinitionAction.BatchID = batchId;
				setModelDefinitionAction.ActionParameters.ItemPath = Model;
				setModelDefinitionAction.ActionParameters.ModelDefinition = Definition;
				setModelDefinitionAction.Execute();
				Warnings = setModelDefinitionAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0003E65C File Offset: 0x0003C85C
		internal void ListModelPerspectives(string Path, out ModelCatalogItem[] ModelCatalogItems)
		{
			try
			{
				ListModelPerspectivesAction listModelPerspectivesAction = this.Service.ListModelPerspectivesAction;
				listModelPerspectivesAction.ActionParameters.ItemPath = Path;
				listModelPerspectivesAction.Execute();
				ModelCatalogItems = listModelPerspectivesAction.ActionParameters.ModelsWithPerspectives;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
		internal void GetUserModel(string Model, string Perspective, out byte[] Definition)
		{
			try
			{
				GetUserModelAction getUserModelAction = this.Service.GetUserModelAction;
				getUserModelAction.ActionParameters.ItemPath = Model;
				getUserModelAction.ActionParameters.PerspectiveID = Perspective;
				getUserModelAction.Execute();
				Definition = getUserModelAction.GetUserModelDefinition();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003E714 File Offset: 0x0003C914
		internal void ListModelItemChildren(string Model, string ModelItemID, bool Recursive, out Microsoft.ReportingServices.Library.Soap2005.ModelItem[] ModelItems)
		{
			try
			{
				ListModelItemChildrenAction listModelItemChildrenAction = this.Service.ListModelItemChildrenAction;
				listModelItemChildrenAction.ActionParameters.ItemPath = Model;
				listModelItemChildrenAction.ActionParameters.ModelItemID = ModelItemID;
				listModelItemChildrenAction.ActionParameters.Recursive = Recursive;
				listModelItemChildrenAction.Execute();
				ModelItems = listModelItemChildrenAction.ActionParameters.ModelItemChildren;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0003E788 File Offset: 0x0003C988
		internal void GetModelItemPermissions(string Model, string ModelItemID, out string[] Permissions)
		{
			try
			{
				GetModelItemPermissionsAction getModelItemPermissionsAction = this.Service.GetModelItemPermissionsAction;
				getModelItemPermissionsAction.ActionParameters.ItemPath = Model;
				getModelItemPermissionsAction.ActionParameters.ModelItemID = ModelItemID;
				getModelItemPermissionsAction.Execute();
				Permissions = getModelItemPermissionsAction.ActionParameters.Permissions;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0003E7EC File Offset: 0x0003C9EC
		internal void GetModelItemPolicies(string Model, string ModelItemID, out Policy[] Policies, out bool InheritParent)
		{
			try
			{
				GetModelItemPoliciesAction getModelItemPoliciesAction = this.Service.GetModelItemPoliciesAction;
				getModelItemPoliciesAction.ActionParameters.ItemPath = Model;
				getModelItemPoliciesAction.ActionParameters.ModelItemID = ModelItemID;
				getModelItemPoliciesAction.Execute();
				Policies = Policy.XmlToPolicyArray(getModelItemPoliciesAction.ActionParameters.Policy);
				InheritParent = getModelItemPoliciesAction.ActionParameters.InheritParent;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0003E864 File Offset: 0x0003CA64
		internal void SetModelItemPolicies(string Model, string ModelItemID, Policy[] Policies)
		{
			this.SetModelItemPolicies(Model, ModelItemID, Policies, Guid.Empty);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0003E874 File Offset: 0x0003CA74
		internal void SetModelItemPolicies(string Model, string ModelItemID, Policy[] Policies, Guid batchId)
		{
			try
			{
				SetModelItemPoliciesAction setModelItemPoliciesAction = this.Service.SetModelItemPoliciesAction;
				setModelItemPoliciesAction.BatchID = batchId;
				setModelItemPoliciesAction.ActionParameters.ItemPath = Model;
				setModelItemPoliciesAction.ActionParameters.ModelItemID = ModelItemID;
				setModelItemPoliciesAction.ActionParameters.Policy = Policy.PolicyArrayToXml(Policies);
				setModelItemPoliciesAction.ActionParameters.InheritParent = false;
				setModelItemPoliciesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0003E8F0 File Offset: 0x0003CAF0
		internal void InheritModelItemParentSecurity(string Model, string ModelItemID)
		{
			this.InheritModelItemParentSecurity(Model, ModelItemID, Guid.Empty);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0003E900 File Offset: 0x0003CB00
		internal void InheritModelItemParentSecurity(string Model, string ModelItemID, Guid batchId)
		{
			try
			{
				SetModelItemPoliciesAction setModelItemPoliciesAction = this.Service.SetModelItemPoliciesAction;
				setModelItemPoliciesAction.BatchID = batchId;
				setModelItemPoliciesAction.ActionParameters.ItemPath = Model;
				setModelItemPoliciesAction.ActionParameters.ModelItemID = ModelItemID;
				setModelItemPoliciesAction.ActionParameters.InheritParent = true;
				setModelItemPoliciesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0003E968 File Offset: 0x0003CB68
		internal void RemoveAllModelItemPolicies(string Model)
		{
			this.RemoveAllModelItemPolicies(Model, Guid.Empty);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0003E978 File Offset: 0x0003CB78
		internal void RemoveAllModelItemPolicies(string Model, Guid batchId)
		{
			try
			{
				RemoveAllModelItemPoliciesAction removeAllModelItemPoliciesAction = this.Service.RemoveAllModelItemPoliciesAction;
				removeAllModelItemPoliciesAction.BatchID = batchId;
				removeAllModelItemPoliciesAction.ActionParameters.ItemPath = Model;
				removeAllModelItemPoliciesAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0003E9C8 File Offset: 0x0003CBC8
		internal void SetModelDrillthroughReports(string Model, string ModelItemID, ModelDrillthroughReport[] Reports)
		{
			this.SetModelDrillthroughReports(Model, ModelItemID, Reports, Guid.Empty);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0003E9D8 File Offset: 0x0003CBD8
		internal void SetModelDrillthroughReports(string Model, string ModelItemID, ModelDrillthroughReport[] Reports, Guid batchId)
		{
			try
			{
				SetDrillthroughReportsAction setDrillthroughReportsAction = this.Service.SetDrillthroughReportsAction;
				setDrillthroughReportsAction.BatchID = batchId;
				setDrillthroughReportsAction.ActionParameters.ModelPath = Model;
				setDrillthroughReportsAction.ActionParameters.ModelItemID = ModelItemID;
				setDrillthroughReportsAction.ActionParameters.Reports = Reports;
				setDrillthroughReportsAction.Execute();
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0003EA44 File Offset: 0x0003CC44
		internal void ListModelDrillthroughReports(string Model, string ModelItemID, out ModelDrillthroughReport[] Reports)
		{
			try
			{
				GetDrillthroughReportsAction getDrillthroughReportsAction = new GetDrillthroughReportsAction(this.Service);
				getDrillthroughReportsAction.ActionParameters.ModelPath = Model;
				getDrillthroughReportsAction.ActionParameters.ModelItemID = ModelItemID;
				getDrillthroughReportsAction.Execute();
				Reports = getDrillthroughReportsAction.ActionParameters.Reports;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0003EAA8 File Offset: 0x0003CCA8
		internal void GenerateModel(string DataSource, string Model, string Parent, Property[] Properties, out Warning[] Warnings)
		{
			this.GenerateModel(DataSource, Model, Parent, Properties, Guid.Empty, out Warnings);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0003EABC File Offset: 0x0003CCBC
		internal void GenerateModel(string DataSource, string Model, string Parent, Property[] Properties, Guid batchId, out Warning[] Warnings)
		{
			try
			{
				GenerateModelAction generateModelAction = this.Service.GenerateModelAction;
				generateModelAction.BatchID = batchId;
				generateModelAction.ActionParameters.DataSourcePath = DataSource;
				generateModelAction.ActionParameters.ItemName = Model;
				generateModelAction.ActionParameters.ParentPath = Parent;
				generateModelAction.ActionParameters.Properties = Properties;
				generateModelAction.Execute();
				Warnings = generateModelAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0003EB44 File Offset: 0x0003CD44
		internal void RegenerateModel(string Model, out Warning[] Warnings)
		{
			this.RegenerateModel(Model, Guid.Empty, out Warnings);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0003EB54 File Offset: 0x0003CD54
		internal void RegenerateModel(string Model, Guid batchId, out Warning[] Warnings)
		{
			try
			{
				RegenerateModelAction regenerateModelAction = this.Service.RegenerateModelAction;
				regenerateModelAction.BatchID = batchId;
				regenerateModelAction.ActionParameters.ItemPath = Model;
				regenerateModelAction.Execute();
				Warnings = regenerateModelAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw this.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0003EBB4 File Offset: 0x0003CDB4
		private byte[] ReturnResponse(RSStream result, out string mimeType)
		{
			result.Seek(0L, SeekOrigin.Begin);
			byte[] array = StreamSupport.ReadToEndUsingLength(result);
			mimeType = result.MimeType;
			result.Close();
			return array;
		}

		// Token: 0x04000682 RID: 1666
		private RSService m_reportingService;

		// Token: 0x04000683 RID: 1667
		private readonly GetExceptionForEndpoint m_getExceptionForEndpoint;
	}
}
