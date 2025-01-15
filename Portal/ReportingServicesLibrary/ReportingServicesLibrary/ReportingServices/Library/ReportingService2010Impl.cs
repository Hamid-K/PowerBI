using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Library.Soap2010;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000200 RID: 512
	internal sealed class ReportingService2010Impl : ReportingServiceSPImpl
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x0003EBE1 File Offset: 0x0003CDE1
		internal ReportingService2010Impl(RSService service, GetExceptionForEndpoint getExceptionForEndpoint)
			: base(service, getExceptionForEndpoint)
		{
			this._fileSizeRestrictions = new FileSizeRestrictions();
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0003EBF8 File Offset: 0x0003CDF8
		internal void GetItemType(string ItemPath, out string Type)
		{
			try
			{
				Type = this.GetItemType(ItemPath, true, "ItemPath", false).ToString();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0003EC44 File Offset: 0x0003CE44
		internal void CreateCatalogItem(string ItemType, string Name, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, out Microsoft.ReportingServices.Library.CatalogItem ItemInfo, out Warning[] Warnings)
		{
			try
			{
				Warnings = null;
				ItemInfo = null;
				if (!this._fileSizeRestrictions.SizeWithinLimits(Definition))
				{
					throw new FileSizeException();
				}
				Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemTypeEnum = (Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum)this.StringToEnum(typeof(Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum), ItemType, "ItemType");
				switch (itemTypeEnum)
				{
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Report:
					this.CreateReport(Name, Parent, Overwrite, Definition, Properties, ItemType.Report, out ItemInfo, out Warnings);
					goto IL_013D;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Resource:
					this.CreateResource(Name, Parent, Overwrite, Definition, Properties, out ItemInfo);
					goto IL_013D;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSource:
					this.CreateDataSource(Name, Parent, Overwrite, this.ByteArrayToDataSourceDefinition(Definition, "Definition"), Properties, "Name", out ItemInfo);
					goto IL_013D;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Model:
					if (Overwrite)
					{
						throw new InvalidParameterException("Overwrite");
					}
					this.CreateModel(Name, Parent, Definition, Properties, out ItemInfo, out Warnings);
					goto IL_013D;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSet:
					this.CreateDataSet(Name, Parent, Overwrite, Definition, Properties, out ItemInfo, out Warnings);
					goto IL_013D;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Component:
					this.CreateComponent(Name, Parent, Overwrite, Definition, Properties, out ItemInfo);
					goto IL_013D;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.RdlxReport:
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Crescent);
					this.CreateReport(Name, Parent, Overwrite, Definition, Properties, ItemType.RdlxReport, out ItemInfo, out Warnings);
					goto IL_013D;
				}
				throw new WrongItemTypeException(itemTypeEnum.ToString());
				IL_013D:;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0003EDBC File Offset: 0x0003CFBC
		private void CreateResource(string Resource, string Parent, bool Overwrite, byte[] Contents, Property[] Properties, out Microsoft.ReportingServices.Library.CatalogItem ItemInfo)
		{
			try
			{
				string text = this.MimeTypeFromProperties(ref Properties);
				if (text == null)
				{
					throw new MissingRequiredPropertyForItemTypeException("MimeType");
				}
				CreateResourceAction createResourceAction = base.Service.CreateResourceAction;
				createResourceAction.ActionParameters.ItemName = Resource;
				createResourceAction.ActionParameters.ParentPath = Parent;
				createResourceAction.ActionParameters.Overwrite = Overwrite;
				createResourceAction.ActionParameters.Content = Contents;
				createResourceAction.ActionParameters.MimeType = text;
				createResourceAction.ActionParameters.Properties = Properties;
				createResourceAction.Execute();
				ItemInfo = createResourceAction.ActionParameters.ItemInfo;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0003EE6C File Offset: 0x0003D06C
		internal void SetItemDefinition(string ItemPath, byte[] Definition, string expectedItemTypeName, Property[] Properties, out Warning[] Warnings)
		{
			try
			{
				Warnings = null;
				Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemType = this.GetItemType(ItemPath, false, "ItemPath", true);
				if (!string.IsNullOrEmpty(expectedItemTypeName))
				{
					if (itemType == Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown)
					{
						throw new ItemNotFoundException(ItemPath);
					}
					if (!expectedItemTypeName.Equals(itemType.ToString(), StringComparison.OrdinalIgnoreCase))
					{
						throw new WrongItemTypeException(expectedItemTypeName.ToString());
					}
				}
				if (!this._fileSizeRestrictions.SizeWithinLimits(Definition))
				{
					throw new FileSizeException();
				}
				switch (itemType)
				{
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown:
					throw new ItemNotFoundException(ItemPath);
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Report:
					if (Properties != null)
					{
						throw new InvalidParameterException("Properties");
					}
					base.SetReportDefinition(ItemPath, Definition, out Warnings);
					goto IL_016A;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Resource:
					this.SetResourceDefinition(ItemPath, Definition, Properties);
					goto IL_016A;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSource:
					if (Properties != null)
					{
						throw new InvalidParameterException("Properties");
					}
					base.SetDataSourceContents(ItemPath, this.ByteArrayToDataSourceDefinition(Definition, "Definition"));
					goto IL_016A;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Model:
					if (Properties != null)
					{
						throw new InvalidParameterException("Properties");
					}
					base.SetModelDefinition(ItemPath, Definition, out Warnings);
					goto IL_016A;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSet:
					if (Properties != null)
					{
						throw new InvalidParameterException("Properties");
					}
					this.SetDataSetDefinition(ItemPath, Definition, out Warnings);
					goto IL_016A;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Component:
					if (Properties != null)
					{
						throw new InvalidParameterException("Properties");
					}
					this.SetComponentDefinition(ItemPath, Definition);
					goto IL_016A;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.RdlxReport:
					if (Properties != null)
					{
						throw new InvalidParameterException("Properties");
					}
					base.SetRdlxReportDefinition(ItemPath, Definition, out Warnings);
					goto IL_016A;
				}
				throw new WrongItemTypeException(itemType.ToString());
				IL_016A:;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0003F010 File Offset: 0x0003D210
		private void SetResourceDefinition(string ItemPath, byte[] Definition, Property[] Properties)
		{
			try
			{
				string text = this.MimeTypeFromProperties(ref Properties);
				if (text == null)
				{
					throw new MissingRequiredPropertyForItemTypeException("MimeType");
				}
				SetResourceContentsAction setResourceContentsAction = base.Service.SetResourceContentsAction;
				setResourceContentsAction.ActionParameters.ItemPath = ItemPath;
				setResourceContentsAction.ActionParameters.Definition = Definition;
				setResourceContentsAction.ActionParameters.MimeType = text;
				setResourceContentsAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0003F088 File Offset: 0x0003D288
		private void GetResourceDefinition(string ItemPath, out byte[] Definition)
		{
			try
			{
				GetResourceContentsAction getResourceContentsAction = base.Service.GetResourceContentsAction;
				getResourceContentsAction.ActionParameters.ItemPath = ItemPath;
				getResourceContentsAction.Execute();
				Definition = getResourceContentsAction.ActionParameters.Content;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0003F0E0 File Offset: 0x0003D2E0
		internal void GetItemDefinitionByType(string ItemPath, string expectedItemType, out byte[] Definition)
		{
			try
			{
				if (string.IsNullOrEmpty(expectedItemType))
				{
					throw new InvalidParameterException("expectedItemType");
				}
				Definition = null;
				Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemType = this.GetItemType(ItemPath, false, "ItemPath", false);
				if (itemType == Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown)
				{
					throw new ItemNotFoundException(ItemPath);
				}
				if (!expectedItemType.Equals(itemType.ToString(), StringComparison.OrdinalIgnoreCase))
				{
					throw new WrongItemTypeException(expectedItemType.ToString());
				}
				Definition = this.GetItemDefinition(ItemPath, itemType);
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0003F168 File Offset: 0x0003D368
		internal void GetItemDefinition(string ItemPath, out byte[] Definition)
		{
			try
			{
				Definition = null;
				Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemType = this.GetItemType(ItemPath, false, "ItemPath", false);
				Definition = this.GetItemDefinition(ItemPath, itemType);
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0003F1B4 File Offset: 0x0003D3B4
		private byte[] GetItemDefinition(string ItemPath, Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemTypeEnum)
		{
			byte[] array = null;
			switch (itemTypeEnum)
			{
			case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown:
				throw new ItemNotFoundException(ItemPath);
			case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Report:
				base.GetReportDefinition(ItemPath, ItemType.Report, out array);
				return array;
			case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Resource:
				this.GetResourceDefinition(ItemPath, out array);
				return array;
			case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSource:
			{
				DataSourceDefinition dataSourceDefinition;
				base.GetDataSourceContents(ItemPath, out dataSourceDefinition);
				return Encoding.Unicode.GetBytes(DataSourceDefinition.ThisToXml(dataSourceDefinition));
			}
			case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Model:
				base.GetModelDefinition(ItemPath, out array);
				return array;
			case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSet:
				this.GetDataSetDefinition(ItemPath, out array);
				return array;
			case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Component:
				this.GetComponentDefinition(ItemPath, out array);
				return array;
			case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.RdlxReport:
				base.GetReportDefinition(ItemPath, ItemType.RdlxReport, out array);
				return array;
			}
			throw new WrongItemTypeException(itemTypeEnum.ToString());
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0003F274 File Offset: 0x0003D474
		internal void ListItemHistory(string Report, out ReportHistorySnapshot[] ItemHistory)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.History);
				ListHistoryAction listHistoryAction = base.Service.ListHistoryAction;
				listHistoryAction.ActionParameters.ReportPath = Report;
				listHistoryAction.Execute();
				ItemHistory = listHistoryAction.ActionParameters.ReportHistory;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0003F2DC File Offset: 0x0003D4DC
		internal void CreateFolder(string Folder, string Parent, Property[] Properties, out Microsoft.ReportingServices.Library.CatalogItem ItemInfo)
		{
			try
			{
				CreateFolderAction createFolderAction = base.Service.CreateFolderAction;
				createFolderAction.ActionParameters.ItemName = Folder;
				createFolderAction.ActionParameters.ParentPath = Parent;
				createFolderAction.ActionParameters.Properties = Properties;
				createFolderAction.Execute();
				ItemInfo = createFolderAction.ActionParameters.ItemInfo;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0003F350 File Offset: 0x0003D550
		internal void SetItemReferences(string ItemPath, ItemReference[] ItemReferences)
		{
			try
			{
				Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemType = this.GetItemType(ItemPath, false, "ItemPath", true);
				if (itemType == Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown)
				{
					throw new ItemNotFoundException(ItemPath);
				}
				if (itemType != Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Report)
				{
					switch (itemType)
					{
					case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Model:
						this.SetModelItemReferences(ItemPath, ItemReferences);
						goto IL_006C;
					case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSet:
						this.SetDataSetItemReferences(ItemPath, ItemReferences);
						goto IL_006C;
					case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.RdlxReport:
						goto IL_0034;
					}
					throw new WrongItemTypeException(itemType.ToString());
				}
				IL_0034:
				this.SetReportItemReferences(ItemPath, ItemReferences);
				IL_006C:;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0003F3EC File Offset: 0x0003D5EC
		private void SetDataSetItemReferences(string ItemPath, ItemReference[] ItemReferences)
		{
			SetDataSetItemReferencesAction setDataSetItemReferencesAction = base.Service.SetDataSetItemReferencesAction;
			setDataSetItemReferencesAction.ActionParameters.ItemPath = ItemPath;
			setDataSetItemReferencesAction.ActionParameters.ItemReferences = ItemReferences;
			setDataSetItemReferencesAction.Execute();
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0003F416 File Offset: 0x0003D616
		private void SetReportItemReferences(string ItemPath, ItemReference[] ItemReferences)
		{
			SetReportItemReferencesAction setReportItemReferencesAction = base.Service.SetReportItemReferencesAction;
			setReportItemReferencesAction.ActionParameters.ItemPath = ItemPath;
			setReportItemReferencesAction.ActionParameters.ItemReferences = ItemReferences;
			setReportItemReferencesAction.Execute();
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0003F440 File Offset: 0x0003D640
		private void SetModelItemReferences(string ItemPath, ItemReference[] ItemReferences)
		{
			SetModelItemReferencesAction setModelItemReferencesAction = base.Service.SetModelItemReferencesAction;
			setModelItemReferencesAction.ActionParameters.ItemPath = ItemPath;
			setModelItemReferencesAction.ActionParameters.ItemReferences = ItemReferences;
			setModelItemReferencesAction.Execute();
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0003F46C File Offset: 0x0003D66C
		internal void GetItemReferences(string ItemPath, string ReferenceItemType, out ItemReferenceData[] ItemReferences)
		{
			try
			{
				Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemType = this.GetItemType(ItemPath, false, "ItemPath", false);
				switch (itemType)
				{
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown:
					throw new ItemNotFoundException(ItemPath);
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Report:
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.LinkedReport:
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.RdlxReport:
					this.GetReportItemReferences(ItemPath, ReferenceItemType, out ItemReferences);
					goto IL_007C;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Model:
					this.GetModelItemReferences(ItemPath, out ItemReferences);
					goto IL_007C;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSet:
					this.GetDataSetItemReferences(ItemPath, out ItemReferences);
					goto IL_007C;
				}
				throw new WrongItemTypeException(itemType.ToString());
				IL_007C:;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0003F518 File Offset: 0x0003D718
		private void GetDataSetItemReferences(string ItemPath, out ItemReferenceData[] ItemReferences)
		{
			GetDataSetItemReferencesAction getDataSetItemReferencesAction = base.Service.GetDataSetItemReferencesAction;
			getDataSetItemReferencesAction.ActionParameters.ItemPath = ItemPath;
			getDataSetItemReferencesAction.Execute();
			ItemReferences = getDataSetItemReferencesAction.ActionParameters.ItemReferences;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x0003F550 File Offset: 0x0003D750
		private void GetReportItemReferences(string ItemPath, string ReferenceItemType, out ItemReferenceData[] ItemReferences)
		{
			GetReportItemReferencesAction getReportItemReferencesAction = base.Service.GetReportItemReferencesAction;
			getReportItemReferencesAction.ActionParameters.ItemPath = ItemPath;
			if (!string.IsNullOrEmpty(ReferenceItemType))
			{
				getReportItemReferencesAction.ActionParameters.ReferenceItemType = new Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum?((Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum)this.StringToEnum(typeof(Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum), ReferenceItemType, "ReferenceItemType"));
			}
			getReportItemReferencesAction.Execute();
			ItemReferences = getReportItemReferencesAction.ActionParameters.ItemReferences;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0003F5BC File Offset: 0x0003D7BC
		private void GetModelItemReferences(string ItemPath, out ItemReferenceData[] ItemReferences)
		{
			GetModelItemReferencesAction getModelItemReferencesAction = base.Service.GetModelItemReferencesAction;
			getModelItemReferencesAction.ActionParameters.ItemPath = ItemPath;
			getModelItemReferencesAction.Execute();
			ItemReferences = getModelItemReferencesAction.ActionParameters.ItemReferences;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003F5F4 File Offset: 0x0003D7F4
		internal string[] ListItemTypes()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0003F634 File Offset: 0x0003D834
		internal void ListSubscriptions(string ItemPathOrSiteURL, string Owner, out SubscriptionImpl[] SubscriptionItems)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				bool flag = false;
				if (ItemPathOrSiteURL != null)
				{
					ItemPathOrSiteURL = ItemPathOrSiteURL.Trim();
					if (ItemPathOrSiteURL.EndsWith("/", StringComparison.OrdinalIgnoreCase))
					{
						ItemPathOrSiteURL = ItemPathOrSiteURL.Substring(0, ItemPathOrSiteURL.Length - 1);
					}
				}
				if (!string.IsNullOrEmpty(ItemPathOrSiteURL))
				{
					switch (this.GetItemType(ItemPathOrSiteURL, true, "ItemPathOrSiteURL", false))
					{
					case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown:
						throw new ItemNotFoundException(ItemPathOrSiteURL);
					case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Folder:
						flag = true;
						goto IL_0094;
					case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Report:
					case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.LinkedReport:
						goto IL_0094;
					case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Site:
						throw new NotSupportedException();
					}
					throw new WrongItemTypeException(ItemPathOrSiteURL);
				}
				IL_0094:
				ListSubscriptionsAction listSubscriptionsAction = base.Service.ListSubscriptionsAction;
				listSubscriptionsAction.ActionParameters.Path = ItemPathOrSiteURL;
				listSubscriptionsAction.ActionParameters.PathIsSiteOrFolder = flag;
				listSubscriptionsAction.ActionParameters.Owner = Owner;
				listSubscriptionsAction.ActionParameters.SubscriptionType = SubscriptionType.ReportSubscription;
				listSubscriptionsAction.Execute();
				SubscriptionItems = listSubscriptionsAction.ActionParameters.Children;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0003F744 File Offset: 0x0003D944
		internal void DisableSubscription(string subscriptionID)
		{
			try
			{
				DisableSubscriptionAction disableSubscriptionAction = base.Service.DisableSubscriptionAction;
				disableSubscriptionAction.ActionParameters.SubscriptionID = subscriptionID;
				disableSubscriptionAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0003F790 File Offset: 0x0003D990
		internal void EnableSubscription(string subscriptionID)
		{
			try
			{
				EnableSubscriptionAction enableSubscriptionAction = base.Service.EnableSubscriptionAction;
				enableSubscriptionAction.ActionParameters.SubscriptionID = subscriptionID;
				enableSubscriptionAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0003F7DC File Offset: 0x0003D9DC
		internal void ChangeSubscriptionOwner(string subscriptionID, string newOwner)
		{
			try
			{
				ChangeSubscriptionOwnerAction changeSubscriptionOwnerAction = base.Service.ChangeSubscriptionOwnerAction;
				changeSubscriptionOwnerAction.ActionParameters.SubscriptionID = subscriptionID;
				changeSubscriptionOwnerAction.ActionParameters.NewOwner = newOwner;
				changeSubscriptionOwnerAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0003F834 File Offset: 0x0003DA34
		internal void CreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, string ParameterName, out Microsoft.ReportingServices.Library.CatalogItem ItemInfo)
		{
			try
			{
				Global.CheckItemName(DataSource, ItemType.DataSource, ParameterName);
				base.VerifyRSDataSourceFileExtension(DataSource);
				CreateDataSourceAction createDataSourceAction = base.Service.CreateDataSourceAction;
				createDataSourceAction.ActionParameters.ItemName = DataSource;
				createDataSourceAction.ActionParameters.ParentPath = Parent;
				createDataSourceAction.ActionParameters.Overwrite = Overwrite;
				createDataSourceAction.ActionParameters.DataSourceDefinition = Definition;
				createDataSourceAction.ActionParameters.Properties = Properties;
				createDataSourceAction.Execute();
				ItemInfo = createDataSourceAction.ActionParameters.ItemInfo;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003F8D0 File Offset: 0x0003DAD0
		public string[] ListDatabaseCredentialRetrievalOptions()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(CredentialRetrievalEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0003F910 File Offset: 0x0003DB10
		internal bool TestConnectForDataSourceDefinition(DataSourceDefinition DataSourceDefinition, string UserName, string Password, out string ConnectError)
		{
			bool connectionSuccess;
			try
			{
				TestConnectForDataSourceDefinitionAction testConnectForDataSourceDefinitionAction = base.Service.TestConnectForDataSourceDefinitionAction;
				testConnectForDataSourceDefinitionAction.ActionParameters.DataSourceDefinition = DataSourceDefinition;
				testConnectForDataSourceDefinitionAction.ActionParameters.UserName = UserName;
				testConnectForDataSourceDefinitionAction.ActionParameters.Password = Password;
				testConnectForDataSourceDefinitionAction.Execute();
				ConnectError = testConnectForDataSourceDefinitionAction.ActionParameters.ConnectionError;
				connectionSuccess = testConnectForDataSourceDefinitionAction.ActionParameters.ConnectionSuccess;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return connectionSuccess;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0003F990 File Offset: 0x0003DB90
		internal bool TestConnectForItemDataSource(string Item, string DataSourceName, string UserName, string Password, out string ConnectError)
		{
			bool connectionSuccess;
			try
			{
				TestConnectForItemDataSourceAction testConnectForItemDataSourceAction = base.Service.TestConnectForItemDataSourceAction;
				testConnectForItemDataSourceAction.ActionParameters.ItemPath = Item;
				testConnectForItemDataSourceAction.ActionParameters.DataSourceName = DataSourceName;
				testConnectForItemDataSourceAction.ActionParameters.UserName = UserName;
				testConnectForItemDataSourceAction.ActionParameters.Password = Password;
				testConnectForItemDataSourceAction.Execute();
				ConnectError = testConnectForItemDataSourceAction.ActionParameters.ConnectionError;
				connectionSuccess = testConnectForItemDataSourceAction.ActionParameters.ConnectionSuccess;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return connectionSuccess;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0003FA1C File Offset: 0x0003DC1C
		internal void CreateRole(string Name, string Description, string[] TaskIDs)
		{
			try
			{
				CreateRoleAction createRoleAction = base.Service.CreateRoleAction;
				createRoleAction.ActionParameters.RoleName = Name;
				createRoleAction.ActionParameters.Description = Description;
				createRoleAction.ActionParameters.TaskIDs = TaskIDs;
				createRoleAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003FA80 File Offset: 0x0003DC80
		internal void SetRoleProperties(string Name, string Description, string[] TaskIDs)
		{
			try
			{
				SetRolePropertiesAction setRolePropertiesAction = base.Service.SetRolePropertiesAction;
				setRolePropertiesAction.ActionParameters.RoleName = Name;
				setRolePropertiesAction.ActionParameters.Description = Description;
				setRolePropertiesAction.ActionParameters.TaskIDs = TaskIDs;
				setRolePropertiesAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0003FAE4 File Offset: 0x0003DCE4
		internal void GetRoleProperties(string Name, string Site, out string[] TaskIDs, out string Description)
		{
			try
			{
				GetRolePropertiesAction getRolePropertiesAction = base.Service.GetRolePropertiesAction;
				getRolePropertiesAction.ActionParameters.RoleName = Name;
				getRolePropertiesAction.ActionParameters.SiteName = Site;
				getRolePropertiesAction.Execute();
				Microsoft.ReportingServices.Library.Soap.Task[] tasks = getRolePropertiesAction.ActionParameters.Tasks;
				Description = getRolePropertiesAction.ActionParameters.Description;
				TaskIDs = Microsoft.ReportingServices.Library.Soap.Task.TaskIDArrayFromTaskArray(tasks);
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0003FB60 File Offset: 0x0003DD60
		internal void ListRoles(string SecurityScope, string Path, out Role[] Roles)
		{
			try
			{
				ListRolesAction listRolesAction = base.Service.ListRolesAction;
				listRolesAction.ActionParameters.Scope = (SecurityScopeEnum)this.StringToEnum(typeof(SecurityScopeEnum), SecurityScope, "SecurityScope");
				listRolesAction.ActionParameters.ItemPath = Path;
				listRolesAction.Execute();
				Roles = listRolesAction.ActionParameters.Roles;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		internal void ListTasks(string SecurityScope, out Microsoft.ReportingServices.Library.Soap.Task[] Tasks)
		{
			try
			{
				ListTasksAction listTasksAction = base.Service.ListTasksAction;
				listTasksAction.ActionParameters.Scope = (SecurityScopeEnum)this.StringToEnum(typeof(SecurityScopeEnum), SecurityScope, "SecurityScope");
				listTasksAction.Execute();
				Tasks = listTasksAction.ActionParameters.Tasks;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0003FC54 File Offset: 0x0003DE54
		private new void CreateModel(string Model, string Parent, byte[] Definition, Property[] Properties, out Microsoft.ReportingServices.Library.CatalogItem ItemInfo, out Warning[] Warnings)
		{
			try
			{
				Global.CheckItemName(Model, ItemType.Model, "Name");
				CreateModelAction createModelAction = base.Service.CreateModelAction;
				createModelAction.ActionParameters.ItemName = Model;
				createModelAction.ActionParameters.ParentPath = Parent;
				createModelAction.ActionParameters.ModelDefinition = Definition;
				createModelAction.ActionParameters.Properties = Properties;
				createModelAction.Execute();
				ItemInfo = createModelAction.ActionParameters.ItemInfo;
				Warnings = createModelAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0003FCEC File Offset: 0x0003DEEC
		internal new void GenerateModel(string DataSource, string Model, string Parent, Property[] Properties, out Microsoft.ReportingServices.Library.CatalogItem ItemInfo, out Warning[] Warnings)
		{
			try
			{
				Global.CheckItemName(Model, ItemType.Model, "Model");
				GenerateModelAction generateModelAction = base.Service.GenerateModelAction;
				generateModelAction.ActionParameters.DataSourcePath = DataSource;
				generateModelAction.ActionParameters.ItemName = Model;
				generateModelAction.ActionParameters.ParentPath = Parent;
				generateModelAction.ActionParameters.Properties = Properties;
				generateModelAction.Execute();
				ItemInfo = generateModelAction.ActionParameters.ItemInfo;
				Warnings = generateModelAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0003FD84 File Offset: 0x0003DF84
		internal string[] ListModelItemTypes()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(ModelItemTypeEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0003FDC4 File Offset: 0x0003DFC4
		public string[] ListScheduleStates()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(ScheduleStateEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0003FE04 File Offset: 0x0003E004
		internal void ListScheduledItems(string ScheduleID, out CatalogItemList Items)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				ListScheduledReportsAction listScheduledReportsAction = base.Service.ListScheduledReportsAction;
				listScheduledReportsAction.ActionParameters.ScheduleID = ScheduleID;
				listScheduledReportsAction.Execute();
				Items = listScheduledReportsAction.ActionParameters.Children;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0003FE6C File Offset: 0x0003E06C
		private void CreateReport(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, ItemType ItemType, out Microsoft.ReportingServices.Library.CatalogItem ItemInfo, out Warning[] Warnings)
		{
			try
			{
				Global.CheckItemName(Report, ItemType, "Name");
				if (ItemType == ItemType.Report)
				{
					CreateReportAction createReportAction = base.Service.CreateReportAction;
					createReportAction.ActionParameters.ItemName = Report;
					createReportAction.ActionParameters.ParentPath = Parent;
					createReportAction.ActionParameters.Overwrite = Overwrite;
					createReportAction.ActionParameters.ReportDefinition = Definition;
					createReportAction.ActionParameters.Properties = Properties;
					createReportAction.Execute();
					ItemInfo = createReportAction.ActionParameters.ItemInfo;
					Warnings = createReportAction.ActionParameters.Warnings;
				}
				else if (ItemType == ItemType.RdlxReport)
				{
					CreateRdlxReportAction createRdlxReportAction = base.Service.CreateRdlxReportAction;
					createRdlxReportAction.ActionParameters.ItemName = Report;
					createRdlxReportAction.ActionParameters.ParentPath = Parent;
					createRdlxReportAction.ActionParameters.Overwrite = Overwrite;
					createRdlxReportAction.ActionParameters.ReportDefinition = Definition;
					createRdlxReportAction.ActionParameters.Properties = Properties;
					createRdlxReportAction.Execute();
					ItemInfo = createRdlxReportAction.ActionParameters.ItemInfo;
					Warnings = createRdlxReportAction.ActionParameters.Warnings;
				}
				else
				{
					Global.m_Tracer.Assert(false, "Unexpected item type {0} in CreateReport.", new object[] { ItemType });
					ItemInfo = null;
					Warnings = null;
				}
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0003FFC4 File Offset: 0x0003E1C4
		internal void SetItemParameters(string ItemPath, ParameterInfoCollection Parameters)
		{
			try
			{
				Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemType = this.GetItemType(ItemPath, false, "ItemPath", false);
				switch (itemType)
				{
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown:
					throw new ItemNotFoundException(ItemPath);
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Report:
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.LinkedReport:
					this.SetReportParameters(ItemPath, Parameters);
					return;
				}
				throw new WrongItemTypeException(itemType.ToString());
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00040040 File Offset: 0x0003E240
		private new void SetReportParameters(string ItemPath, ParameterInfoCollection Parameters)
		{
			SetReportParametersAction setReportParametersAction = base.Service.SetReportParametersAction;
			setReportParametersAction.ActionParameters.ItemPath = ItemPath;
			setReportParametersAction.ActionParameters.Parameters = Parameters;
			setReportParametersAction.Execute();
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0004006C File Offset: 0x0003E26C
		internal void GetItemParameters(string ItemPath, string HistoryID, bool ForRendering, Microsoft.ReportingServices.Library.Soap.ParameterValue[] Values, DataSourceCredentials[] Credentials, out ItemParameter[] Parameters)
		{
			try
			{
				Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum itemType = this.GetItemType(ItemPath, false, "ItemPath", false);
				switch (itemType)
				{
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Unknown:
					throw new ItemNotFoundException(ItemPath);
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Folder:
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Resource:
					break;
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.Report:
				case Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.LinkedReport:
					this.GetReportParameters(ItemPath, HistoryID, ForRendering, Values, Credentials, out Parameters);
					goto IL_006B;
				default:
					if (itemType == Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum.DataSet)
					{
						this.GetDataSetParameters(ItemPath, HistoryID, ForRendering, Values, Credentials, out Parameters);
						goto IL_006B;
					}
					break;
				}
				throw new WrongItemTypeException(itemType.ToString());
				IL_006B:;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00040104 File Offset: 0x0003E304
		private void GetReportParameters(string ItemPath, string HistoryID, bool ForRendering, Microsoft.ReportingServices.Library.Soap.ParameterValue[] Values, DataSourceCredentials[] Credentials, out ItemParameter[] Parameters)
		{
			GetReportParametersAction getReportParametersAction = base.Service.GetReportParametersAction;
			getReportParametersAction.ActionParameters.ItemPath = ItemPath;
			getReportParametersAction.ActionParameters.HistoryID = HistoryID;
			getReportParametersAction.ActionParameters.ForRendering = ForRendering;
			getReportParametersAction.ActionParameters.ParameterValidationValues = Microsoft.ReportingServices.Library.Soap.ParameterValue.ThisArrayToNameValueCollection(Values);
			getReportParametersAction.ActionParameters.DatasourceCredentials = DataSourceCredentials.ThisArrayToDatasourcesCredentials(Credentials);
			getReportParametersAction.Execute();
			Parameters = ItemParameter.CollectionToParameterArray(getReportParametersAction.ActionParameters.Parameters);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00040180 File Offset: 0x0003E380
		private void GetDataSetParameters(string ItemPath, string HistoryID, bool ForRendering, Microsoft.ReportingServices.Library.Soap.ParameterValue[] Values, DataSourceCredentials[] Credentials, out ItemParameter[] Parameters)
		{
			if (HistoryID != null)
			{
				throw new InvalidParameterException("HistoryID");
			}
			if (Credentials != null)
			{
				throw new InvalidParameterException("Credentials");
			}
			GetDataSetParametersAction getDataSetParametersAction = base.Service.GetDataSetParametersAction;
			getDataSetParametersAction.ActionParameters.ItemPath = ItemPath;
			getDataSetParametersAction.ActionParameters.ForRendering = ForRendering;
			getDataSetParametersAction.ActionParameters.ParameterValidationValues = Microsoft.ReportingServices.Library.Soap.ParameterValue.ThisArrayToNameValueCollection(Values);
			getDataSetParametersAction.Execute();
			Parameters = ItemParameter.CollectionToParameterArray(getDataSetParametersAction.ActionParameters.Parameters, true);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000401FC File Offset: 0x0003E3FC
		internal string[] ListParameterTypes()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(ParameterTypeEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0004023C File Offset: 0x0003E43C
		internal string[] ListParameterStates()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(ParameterStateEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0004027C File Offset: 0x0003E47C
		internal void CreateReportEditSession(string Report, string Parent, byte[] Definition, out string EditSessionID, out Warning[] Warnings)
		{
			try
			{
				CreateReportEditSessionAction createEditSessionAction = base.Service.CreateEditSessionAction;
				createEditSessionAction.ActionParameters.ParentPath = Parent;
				createEditSessionAction.ActionParameters.ItemName = Report;
				createEditSessionAction.ActionParameters.ReportDefinition = Definition;
				createEditSessionAction.Execute();
				EditSessionID = createEditSessionAction.ActionParameters.EditSessionID;
				Warnings = createEditSessionAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000402FC File Offset: 0x0003E4FC
		private void CreateDataSet(string dataSet, string parent, bool overwrite, byte[] definition, Property[] properties, out Microsoft.ReportingServices.Library.CatalogItem itemInfo, out Warning[] warnings)
		{
			try
			{
				Global.CheckItemName(dataSet, ItemType.DataSet, "Name");
				CreateDataSetAction createDataSetAction = base.Service.CreateDataSetAction;
				createDataSetAction.ActionParameters.ItemName = dataSet;
				createDataSetAction.ActionParameters.ParentPath = parent;
				createDataSetAction.ActionParameters.DataSetDefinition = definition;
				createDataSetAction.ActionParameters.Properties = properties;
				createDataSetAction.ActionParameters.Overwrite = overwrite;
				createDataSetAction.Execute();
				itemInfo = createDataSetAction.ActionParameters.ItemInfo;
				warnings = createDataSetAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000403A4 File Offset: 0x0003E5A4
		private void SetDataSetDefinition(string DataSet, byte[] Definition, out Warning[] Warnings)
		{
			try
			{
				SetDataSetDefinitionAction setDataSetDefinitionAction = base.Service.SetDataSetDefinitionAction;
				setDataSetDefinitionAction.ActionParameters.ItemPath = DataSet;
				setDataSetDefinitionAction.ActionParameters.DataSetDefinition = Definition;
				setDataSetDefinitionAction.Execute();
				Warnings = setDataSetDefinitionAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00040408 File Offset: 0x0003E608
		private void GetDataSetDefinition(string DataSet, out byte[] Definition)
		{
			try
			{
				GetDataSetDefinitionAction getDataSetDefinitionAction = base.Service.GetDataSetDefinitionAction;
				getDataSetDefinitionAction.ActionParameters.ItemPath = DataSet;
				getDataSetDefinitionAction.Execute();
				Definition = getDataSetDefinitionAction.ActionParameters.DataSetDefinition;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00040460 File Offset: 0x0003E660
		private void CreateComponent(string Component, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, out Microsoft.ReportingServices.Library.CatalogItem ItemInfo)
		{
			try
			{
				Global.CheckItemName(Component, ItemType.Component, "Name");
				CreateComponentAction createComponentAction = base.Service.CreateComponentAction;
				createComponentAction.ActionParameters.ItemName = Component;
				createComponentAction.ActionParameters.ParentPath = Parent;
				createComponentAction.ActionParameters.ComponentDefinition = Definition;
				createComponentAction.ActionParameters.Properties = Properties;
				createComponentAction.ActionParameters.Overwrite = Overwrite;
				createComponentAction.Execute();
				ItemInfo = createComponentAction.ActionParameters.ItemInfo;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000404F8 File Offset: 0x0003E6F8
		private void SetComponentDefinition(string Component, byte[] Definition)
		{
			try
			{
				SetComponentDefinitionAction setComponentDefinitionAction = base.Service.SetComponentDefinitionAction;
				setComponentDefinitionAction.ActionParameters.ItemPath = Component;
				setComponentDefinitionAction.ActionParameters.ComponentDefinition = Definition;
				setComponentDefinitionAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00040550 File Offset: 0x0003E750
		private void GetComponentDefinition(string Component, out byte[] Definition)
		{
			try
			{
				GetComponentDefinitionAction getComponentDefinitionAction = base.Service.GetComponentDefinitionAction;
				getComponentDefinitionAction.ActionParameters.ItemPath = Component;
				getComponentDefinitionAction.Execute();
				Definition = getComponentDefinitionAction.ActionParameters.ComponentDefinition;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000405A8 File Offset: 0x0003E7A8
		internal string[] ListExecutionSettings()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(ExecutionSettingEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000405E8 File Offset: 0x0003E7E8
		internal void SetExecutionOptions(string Report, string ExecutionSetting, [XmlElement(typeof(ScheduleDefinition))] [XmlElement(typeof(ScheduleReference))] [XmlElement(typeof(NoSchedule))] ScheduleDefinitionOrReference Schedule)
		{
			try
			{
				ExecutionSettingEnum executionSettingEnum = (ExecutionSettingEnum)this.StringToEnum(typeof(ExecutionSettingEnum), ExecutionSetting, "ExecutionSetting");
				if (executionSettingEnum != ExecutionSettingEnum.Live)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.ExecutionSnapshots);
				}
				if (Schedule != null)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				}
				SetExecutionOptionsAction setExecutionOptionsAction = base.Service.SetExecutionOptionsAction;
				setExecutionOptionsAction.ActionParameters.ReportPath = Report;
				setExecutionOptionsAction.ActionParameters.ExecutionSettings = executionSettingEnum;
				setExecutionOptionsAction.ActionParameters.Schedule = Schedule;
				setExecutionOptionsAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0004068C File Offset: 0x0003E88C
		internal void GetReportServerConfigInfo(bool scaleOut, out string ServerConfigInfo)
		{
			try
			{
				GetReportServerConfigInfoAction getReportServerConfigInfoAction = base.Service.GetReportServerConfigInfoAction;
				getReportServerConfigInfoAction.ActionParameters.ScaleOut = scaleOut;
				getReportServerConfigInfoAction.Execute();
				ServerConfigInfo = ServerConfigInfo.ThisArrayToXml(getReportServerConfigInfoAction.ActionParameters.ServerConfigInfo);
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x000406EC File Offset: 0x0003E8EC
		internal bool IsSSLRequired()
		{
			bool flag;
			try
			{
				flag = Globals.Configuration.RequireHttpsLevel > 0;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return flag;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00040728 File Offset: 0x0003E928
		internal void ListExtensions(string ExtensionType, out Microsoft.ReportingServices.Library.Soap2010.Extension[] Extensions)
		{
			try
			{
				ExtensionTypeEnum extensionTypeEnum = (ExtensionTypeEnum)this.StringToEnum(typeof(ExtensionTypeEnum), ExtensionType, "ExtensionType");
				Extensions = Microsoft.ReportingServices.Library.Soap2010.Extension.Soap2005ExtensionToThisArray(base.Service.ListExtensions(extensionTypeEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00040784 File Offset: 0x0003E984
		public string[] ListExtensionTypes()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(ExtensionTypeEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000407C4 File Offset: 0x0003E9C4
		internal new void FireEvent(string EventType, string EventData, string SiteUrl)
		{
			try
			{
				if (string.Compare(EventType, "TimedSubscription", StringComparison.OrdinalIgnoreCase) == 0)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Subscriptions);
				}
				if (string.Compare(EventType, "RefreshCache", StringComparison.OrdinalIgnoreCase) == 0)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Caching);
				}
				FireEventAction fireEventAction = base.Service.FireEventAction;
				fireEventAction.ActionParameters.EventType = EventType;
				fireEventAction.ActionParameters.EventData = EventData;
				fireEventAction.ActionParameters.Site = SiteUrl;
				fireEventAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00040864 File Offset: 0x0003EA64
		internal string[] ListJobTypes()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(JobTypeEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000408A4 File Offset: 0x0003EAA4
		internal string[] ListJobActions()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(Microsoft.ReportingServices.Diagnostics.JobActionEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000408E4 File Offset: 0x0003EAE4
		internal string[] ListJobStates()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(JobStatusEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00040924 File Offset: 0x0003EB24
		internal void CreateCacheRefreshPlan(string ItemPath, string Description, string EventType, string MatchData, Microsoft.ReportingServices.Library.Soap.ParameterValue[] Parameters, out string CacheRefreshPlanID)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Caching);
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				ItemType libraryItemType = this.GetLibraryItemType(ItemPath, "ItemPath", false);
				if (libraryItemType == ItemType.PowerBIReport)
				{
					Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.PowerBIRendering);
				}
				CacheRefreshPlanID = string.Empty;
				CreateCacheRefreshPlanAction createCacheRefreshPlanAction = base.Service.CreateCacheRefreshPlanAction;
				createCacheRefreshPlanAction.ActionParameters.ItemType = libraryItemType;
				createCacheRefreshPlanAction.ActionParameters.ItemPath = ItemPath;
				createCacheRefreshPlanAction.ActionParameters.Description = Description;
				createCacheRefreshPlanAction.ActionParameters.EventType = EventType;
				createCacheRefreshPlanAction.ActionParameters.MatchData = MatchData;
				createCacheRefreshPlanAction.ActionParameters.Parameters = Parameters;
				createCacheRefreshPlanAction.Execute();
				CacheRefreshPlanID = createCacheRefreshPlanAction.ActionParameters.CacheRefreshPlanID;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00040A0C File Offset: 0x0003EC0C
		internal void SetCacheRefreshPlanProperties(string CacheRefreshPlanID, string Description, string EventType, string MatchData, Microsoft.ReportingServices.Library.Soap.ParameterValue[] Parameters)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Caching);
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				SetCacheRefreshPlanPropertiesAction setCacheRefreshPlanPropertiesAction = base.Service.SetCacheRefreshPlanPropertiesAction;
				setCacheRefreshPlanPropertiesAction.ActionParameters.CacheRefreshPlanID = CacheRefreshPlanID;
				setCacheRefreshPlanPropertiesAction.ActionParameters.Description = Description;
				setCacheRefreshPlanPropertiesAction.ActionParameters.EventType = EventType;
				setCacheRefreshPlanPropertiesAction.ActionParameters.MatchData = MatchData;
				setCacheRefreshPlanPropertiesAction.ActionParameters.Parameters = Parameters;
				setCacheRefreshPlanPropertiesAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00040AAC File Offset: 0x0003ECAC
		internal void GetCacheRefreshPlanProperties(string CacheRefreshPlanID, out string Description, out string LastRunStatus, out CacheRefreshPlanState State, out string EventType, out string MatchData, out Microsoft.ReportingServices.Library.Soap.ParameterValue[] Parameters)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Caching);
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				GetCacheRefreshPlanPropertiesAction getCacheRefreshPlanPropertiesAction = base.Service.GetCacheRefreshPlanPropertiesAction;
				getCacheRefreshPlanPropertiesAction.ActionParameters.CacheRefreshPlanID = CacheRefreshPlanID;
				getCacheRefreshPlanPropertiesAction.Execute();
				Description = getCacheRefreshPlanPropertiesAction.ActionParameters.Description;
				LastRunStatus = getCacheRefreshPlanPropertiesAction.ActionParameters.Status;
				State = new CacheRefreshPlanState(getCacheRefreshPlanPropertiesAction.ActionParameters.State);
				EventType = getCacheRefreshPlanPropertiesAction.ActionParameters.EventType;
				MatchData = getCacheRefreshPlanPropertiesAction.ActionParameters.MatchData;
				Parameters = (Microsoft.ReportingServices.Library.Soap.ParameterValue[])getCacheRefreshPlanPropertiesAction.ActionParameters.Parameters;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00040B74 File Offset: 0x0003ED74
		public void DeleteCacheRefreshPlan(string CacheRefreshPlanID)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Caching);
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				DeleteSubscriptionAction deleteSubscriptionAction = base.Service.DeleteSubscriptionAction;
				deleteSubscriptionAction.ActionParameters.SubscriptionID = CacheRefreshPlanID;
				deleteSubscriptionAction.ActionParameters.IsCacheRefreshPlanExpected = true;
				deleteSubscriptionAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00040BEC File Offset: 0x0003EDEC
		internal void ListCacheRefreshPlans(string ItemPath, out SubscriptionImpl[] CacheRefreshPlans)
		{
			try
			{
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Caching);
				Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Scheduling);
				ListSubscriptionsAction listSubscriptionsAction = base.Service.ListSubscriptionsAction;
				listSubscriptionsAction.ActionParameters.Path = ItemPath;
				listSubscriptionsAction.ActionParameters.SubscriptionType = SubscriptionType.CacheRefreshPlan;
				listSubscriptionsAction.Execute();
				CacheRefreshPlans = listSubscriptionsAction.ActionParameters.Children;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00040C70 File Offset: 0x0003EE70
		internal string[] ListSecurityScopes()
		{
			string[] names;
			try
			{
				names = Enum.GetNames(typeof(SecurityScopeEnum));
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return names;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00040CB0 File Offset: 0x0003EEB0
		internal Enum StringToEnum(Type enumType, string enumString, string enumParamName)
		{
			Enum @enum = null;
			if (!EnumHelpers.TryStringToEnum(enumType, enumString, ref @enum))
			{
				throw new InvalidParameterException(enumParamName);
			}
			return @enum;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00040CD2 File Offset: 0x0003EED2
		private ItemType GetLibraryItemType(string itemPath, string parameterName, bool allowEditSessionSyntax)
		{
			GetItemTypeAction getItemTypeAction = base.Service.GetItemTypeAction;
			getItemTypeAction.ActionParameters.ItemPath = itemPath;
			getItemTypeAction.ActionParameters.AllowEditSessions = allowEditSessionSyntax;
			getItemTypeAction.Execute();
			return (ItemType)getItemTypeAction.ActionParameters.ItemType;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00040D07 File Offset: 0x0003EF07
		private Microsoft.ReportingServices.Library.Soap2010.ItemTypeEnum GetItemType(string itemPath, bool fromStore, string parameterName, bool allowEditSessionSyntax)
		{
			return Microsoft.ReportingServices.Library.Soap2010.CatalogItem.ItemTypeToSoapType(this.GetLibraryItemType(itemPath, parameterName, allowEditSessionSyntax));
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00040D18 File Offset: 0x0003EF18
		private DataSourceDefinition ByteArrayToDataSourceDefinition(byte[] definition, string parameterName)
		{
			if (definition == null || definition.Length == 0)
			{
				throw new MissingParameterException(parameterName);
			}
			DataSourceDefinition dataSourceDefinition;
			try
			{
				XsdDataContractExporter xsdDataContractExporter = new XsdDataContractExporter();
				xsdDataContractExporter.Export(typeof(DataSourceDefinition));
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.IgnoreWhitespace = true;
				xmlReaderSettings.Schemas = xsdDataContractExporter.Schemas;
				XmlReader xmlReader = XmlReader.Create(new MemoryStream(definition), xmlReaderSettings);
				xmlReader.MoveToContent();
				dataSourceDefinition = DataSourceDefinition.XmlToThis(xmlReader.ReadOuterXml());
			}
			catch (XmlException ex)
			{
				throw new InvalidParameterException(parameterName, ex);
			}
			return dataSourceDefinition;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00040DA0 File Offset: 0x0003EFA0
		private string MimeTypeFromProperties(ref Property[] Properties)
		{
			string text = null;
			List<Property> list = null;
			if (Properties != null && Properties.Length != 0)
			{
				foreach (Property property in Properties)
				{
					if (text == null && property != null && string.Compare("MimeType", property.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = property.Value;
					}
					else
					{
						if (list == null)
						{
							list = new List<Property>();
						}
						list.Add(property);
					}
				}
			}
			if (list != null)
			{
				Properties = list.ToArray();
			}
			else
			{
				Properties = null;
			}
			return text;
		}

		// Token: 0x04000684 RID: 1668
		private readonly FileSizeRestrictions _fileSizeRestrictions;
	}
}
