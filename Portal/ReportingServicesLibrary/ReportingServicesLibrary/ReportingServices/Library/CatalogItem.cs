using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000CB RID: 203
	internal abstract class CatalogItem
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x00022E1C File Offset: 0x0002101C
		protected CatalogItem(RSService service)
		{
			this.m_service = service;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00022E6D File Offset: 0x0002106D
		internal void Initialize(CatalogItemContext itemContext, Guid itemID, byte[] securityDescriptor)
		{
			this.m_itemContext = itemContext;
			this.m_itemID = itemID;
			this.m_securityDescriptor = securityDescriptor;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00022E84 File Offset: 0x00021084
		internal void LoadProperties()
		{
			using (MonitoredScope.New("CatalogItem.LoadProperties"))
			{
				ItemType itemType;
				string text;
				if (!this.Service.Storage.GetAllProperties(this.ItemContext.ItemPath, out this.m_properties, out this.m_itemID, out this.m_linkID, out itemType, out this.m_securityDescriptor, out this.m_executionOptions, out this.m_historyLimit, out text))
				{
					throw new ItemNotFoundException(this.ItemContext.OriginalItemPath.Value);
				}
				this.UnprotectProperties();
				if (!string.IsNullOrEmpty(text))
				{
					this.m_itemMetadata.SubType = text;
				}
				this.ThisItemType = itemType;
				this.SetInitFlag();
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void ProtectProperties()
		{
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal virtual void UnprotectProperties()
		{
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00022F3C File Offset: 0x0002113C
		internal virtual void LoadStoredAndDerivedProperties()
		{
			this.LoadProperties();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00022F44 File Offset: 0x00021144
		internal void LoadDefinition()
		{
			this.LoadDefinition(false);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00022F50 File Offset: 0x00021150
		internal void LoadDefinition(bool skipAccessCheck)
		{
			ItemType itemType;
			string text;
			if (!this.Service.Storage.GetObjectContent(this.ItemContext.ItemPath, out itemType, out this.m_content, out this.m_linkID, out text, out this.m_securityDescriptor, out this.m_itemID))
			{
				throw new ItemNotFoundException(this.ItemContext.OriginalItemPath.Value);
			}
			this.ThisItemType = itemType;
			if (text != null)
			{
				this.m_itemMetadata.MimeType = text;
			}
			if (!skipAccessCheck)
			{
				this.ContentLoadSecurityCheck();
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00022FCC File Offset: 0x000211CC
		internal static ItemType ExtractCatalogTypeFromRuntimeType(Type t)
		{
			ItemType itemType = ItemType.Unknown;
			object[] customAttributes = t.GetCustomAttributes(typeof(CatalogItemTypeAttribute), false);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				itemType = (customAttributes[0] as CatalogItemTypeAttribute).Type;
			}
			else
			{
				RSTrace.CatalogTrace.Assert(false, "runtime instance did not have ItemType attribute");
			}
			RSTrace.CatalogTrace.Assert(itemType > ItemType.Unknown, "catItemType");
			return itemType;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00023028 File Offset: 0x00021228
		internal void CombineProperties(ItemProperties suppliedProperties)
		{
			if (this.m_properties == null)
			{
				this.m_properties = new ItemProperties();
			}
			if (suppliedProperties != null)
			{
				this.m_properties.CombineProperties(suppliedProperties);
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0002304C File Offset: 0x0002124C
		internal void AdjustModificationInfo(DateTime modificationDate)
		{
			if (this.m_modifiedBy == null)
			{
				this.m_modifiedBy = this.m_service.UserName;
			}
			this.Service.Storage.SetLastModified(this.m_itemContext.CatalogItemPath, this.m_modifiedBy, modificationDate);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002308C File Offset: 0x0002128C
		protected void AdjustModificationInfo()
		{
			if (this.m_modificationDate == DateTime.MinValue)
			{
				this.m_modificationDate = DateTime.Now;
			}
			if (this.m_modifiedBy == null)
			{
				this.m_modifiedBy = this.m_service.UserName;
			}
			this.Service.Storage.SetLastModified(this.m_itemContext.CatalogItemPath, this.m_modifiedBy, this.m_modificationDate);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000230F8 File Offset: 0x000212F8
		protected DataSourceInfoCollection GetSyncedItemDataSources(Guid id)
		{
			RSTrace.CatalogTrace.Assert(this.ThisItemType == ItemType.Model || this.ThisItemType == ItemType.Report || this.ThisItemType == ItemType.LinkedReport || this.ThisItemType == ItemType.DataSet || this.ThisItemType == ItemType.RdlxReport);
			bool flag;
			DataSourceInfoCollection dataSources = this.Service.Storage.GetDataSources(id, out flag);
			foreach (object obj in dataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				if (dataSourceInfo.IsReference && dataSourceInfo.ReferenceIsValid)
				{
					try
					{
						CatalogItemContext catalogItemContext = new CatalogItemContext(this.Service, new CatalogItemPath(dataSourceInfo.DataSourceReference), "");
						bool flag2;
						CatalogItem catalogItem = this.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, out flag2);
						if (!flag && dataSourceInfo.IsModel)
						{
							bool flag3;
							DataSourceInfoCollection dataSources2 = this.Service.Storage.GetDataSources(dataSourceInfo.LinkID, out flag3);
							if (!flag3)
							{
								throw new InternalCatalogException("Found invalid model data source structure.");
							}
							DataSourceInfo theOnlyDataSource = dataSources2.GetTheOnlyDataSource();
							try
							{
								CatalogItemContext catalogItemContext2 = new CatalogItemContext(this.Service, new CatalogItemPath(theOnlyDataSource.DataSourceReference), "");
								CatalogItem catalogItem2 = this.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext2, true);
								catalogItem2.ThrowIfWrongItemType(new ItemType[] { ItemType.DataSource });
								DataSourceInfo dataSourceInfo2 = (catalogItem2 as DataSourceCatalogItem).DataSourceInfo;
								dataSourceInfo.LinkModelToDataSource(dataSourceInfo2, dataSourceInfo.LinkID);
								continue;
							}
							catch (ItemNotFoundException)
							{
								if (RSTrace.CatalogTrace.TraceWarning)
								{
									RSTrace.CatalogTrace.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "The shared datasource {0} referenced by {1} does not exist.", theOnlyDataSource.DataSourceReference, dataSourceInfo.DataSourceReference));
								}
								dataSourceInfo.ReferenceIsValid = false;
								continue;
							}
						}
						if (flag2)
						{
							if (catalogItem.ItemID != dataSourceInfo.LinkID)
							{
								throw new ItemNotFoundException(dataSourceInfo.DataSourceReference);
							}
							catalogItem.ThrowIfWrongItemType(new ItemType[] { ItemType.DataSource });
							DataSourceInfo dataSourceInfo3 = (catalogItem as DataSourceCatalogItem).DataSourceInfo;
							dataSourceInfo.LinkToStandAlone(dataSourceInfo3, dataSourceInfo.DataSourceReference, catalogItem.ItemID);
						}
					}
					catch (ItemNotFoundException)
					{
						if (RSTrace.CatalogTrace.TraceWarning)
						{
							RSTrace.CatalogTrace.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "The shared datasource {0} referenced by {1} does not exist.", dataSourceInfo.DataSourceReference, dataSourceInfo.Name));
						}
						dataSourceInfo.ReferenceIsValid = false;
						dataSourceInfo.DataSourceReference = null;
					}
				}
			}
			return dataSources;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000233A4 File Offset: 0x000215A4
		protected DataSetInfoCollection GetSyncedItemSharedDataSets(Guid id)
		{
			RSTrace.CatalogTrace.Assert(this.ThisItemType == ItemType.Report || this.ThisItemType == ItemType.LinkedReport || this.ThisItemType == ItemType.Kpi || this.ThisItemType == ItemType.MobileReport);
			DataSetInfoCollection sharedDataSets = this.Service.Storage.GetSharedDataSets(id);
			foreach (DataSetInfo dataSetInfo in sharedDataSets)
			{
				if (dataSetInfo.IsValidReference())
				{
					try
					{
						CatalogItemContext catalogItemContext = new CatalogItemContext(this.Service, new CatalogItemPath(dataSetInfo.AbsolutePath), "");
						if (this.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true).ThisItemType != ItemType.DataSet)
						{
							throw new ItemNotFoundException(dataSetInfo.AbsolutePath);
						}
					}
					catch (ItemNotFoundException)
					{
						if (RSTrace.CatalogTrace.TraceWarning)
						{
							RSTrace.CatalogTrace.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "The shared dataset {0} referenced by {1} does not exist.", dataSetInfo.AbsolutePath, dataSetInfo.DataSetName));
						}
						dataSetInfo.LinkedSharedDataSetID = Guid.Empty;
					}
				}
			}
			return sharedDataSets;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000234D0 File Offset: 0x000216D0
		protected void SetInitFlag()
		{
			this.m_linkIDInit = (this.m_executionOptionsInit = (this.m_historyLimitInit = true));
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x000234F6 File Offset: 0x000216F6
		internal RSService Service
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x000234FE File Offset: 0x000216FE
		internal CatalogItemContext ItemContext
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_itemContext;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00023506 File Offset: 0x00021706
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0002350E File Offset: 0x0002170E
		internal Guid ItemID
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_itemID;
			}
			set
			{
				this.m_itemID = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00023517 File Offset: 0x00021717
		internal byte[] SecurityDescriptor
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_securityDescriptor;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00023520 File Offset: 0x00021720
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00023560 File Offset: 0x00021760
		internal ItemType ThisItemType
		{
			get
			{
				if (this.m_itemType == null)
				{
					ItemType itemType = CatalogItem.ExtractCatalogTypeFromRuntimeType(base.GetType());
					this.m_itemType = new ItemType?(itemType);
				}
				return this.m_itemType.Value;
			}
			set
			{
				ItemType itemType = CatalogItem.ExtractCatalogTypeFromRuntimeType(base.GetType());
				RSService.EnsureItemType(value, this.ItemContext.OriginalItemPath.Value, new ItemType[] { itemType });
				this.m_itemType = new ItemType?(value);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x000235A5 File Offset: 0x000217A5
		internal ItemProperties Properties
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000235AD File Offset: 0x000217AD
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x000235B5 File Offset: 0x000217B5
		internal CatalogItem Parent
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x000235BE File Offset: 0x000217BE
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x000235C6 File Offset: 0x000217C6
		internal string CreatedBy
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_createdBy;
			}
			set
			{
				this.m_createdBy = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000235CF File Offset: 0x000217CF
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x000235D7 File Offset: 0x000217D7
		internal DateTime CreationDate
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_creationDate;
			}
			set
			{
				this.m_creationDate = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000235E0 File Offset: 0x000217E0
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x000235E8 File Offset: 0x000217E8
		internal string ModifiedBy
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_modifiedBy;
			}
			set
			{
				this.m_modifiedBy = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000235F1 File Offset: 0x000217F1
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x000235F9 File Offset: 0x000217F9
		internal DateTime ModificationDate
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_modificationDate;
			}
			set
			{
				this.m_modificationDate = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00023602 File Offset: 0x00021802
		internal virtual byte[] Content
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_content;
			}
			set
			{
				throw new InternalCatalogException("Content property is not valid for this type: " + this.ThisItemType);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0002361E File Offset: 0x0002181E
		internal string Description
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00023626 File Offset: 0x00021826
		internal ItemMetadata ItemMetadata
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_itemMetadata;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0002362E File Offset: 0x0002182E
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x00023636 File Offset: 0x00021836
		internal bool InternalUsePermissionForExecution
		{
			get
			{
				return this.m_internalUsePermissionForExecution;
			}
			set
			{
				this.m_internalUsePermissionForExecution = value;
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002363F File Offset: 0x0002183F
		internal void ThrowIfWrongItemType(params ItemType[] expectedItemTypes)
		{
			RSService.EnsureItemType(this.ThisItemType, this.ItemContext.OriginalItemPath.Value, expectedItemTypes);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002365D File Offset: 0x0002185D
		internal void ThrowIfNoAccess(CommonOperation operation)
		{
			if (!this.Service.SecMgr.CheckAccess(this.ThisItemType, this.SecurityDescriptor, operation, this.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(this.Service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002369C File Offset: 0x0002189C
		internal void ThrowIfNoAccess(ReportOperation operation)
		{
			if (!this.Service.SecMgr.CheckAccess(this.ThisItemType, this.SecurityDescriptor, operation, this.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(this.Service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x060008E1 RID: 2273
		protected abstract void ContentLoadSecurityCheck();

		// Token: 0x060008E2 RID: 2274 RVA: 0x000236DC File Offset: 0x000218DC
		internal virtual void Create()
		{
			if (this.m_parent == null)
			{
				throw new ItemNotFoundException(this.Service.CatalogToExternal(this.m_itemContext.CatalogItemPath).Value);
			}
			try
			{
				this.ProtectProperties();
				Guid guid = this.Service.Storage.CreateObject(this.m_itemID, this.m_itemContext.ItemName, this.m_itemContext.CatalogItemPath, this.m_parent.m_itemContext.ItemPath, this.m_parent.m_itemID, this.ThisItemType, this.m_content, (this.m_compiledDefinition == null) ? Guid.Empty : this.m_compiledDefinition.SnapshotDataID, this.m_linkID, ItemPathBase.SafeValue(this.m_linkPath), this.m_properties, this.m_parametersXml, this.m_createdBy, this.m_creationDate, this.m_modificationDate, this.m_itemMetadata.MimeType, this.m_itemMetadata.SubType, this.m_itemMetadata.ComponentID);
				if (guid == Guid.Empty)
				{
					throw new ItemAlreadyExistsException(this.m_itemContext.ItemPath.Value);
				}
				this.m_itemID = guid;
				this.FinalizeCreation();
				this.UnprotectProperties();
			}
			catch (Exception)
			{
				try
				{
					this.Service.ServiceHelper.AbortCreation(this);
				}
				catch (Exception ex)
				{
					if (RSTrace.CatalogTrace.TraceWarning)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "Fail to cleanup when there is error creating catalog item '{0}' to external storage. Exception is {1}.", this.ItemContext.ItemPath, ex.Message));
					}
				}
				throw;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002387C File Offset: 0x00021A7C
		internal virtual void Save(bool preventCreate)
		{
			if (ItemPathBase.IsNullOrEmpty(this.Service.Storage.GetPathById(this.ItemID)) && !preventCreate)
			{
				this.Create();
				return;
			}
			byte[] array = null;
			try
			{
				this.Update();
			}
			catch (Exception)
			{
				try
				{
					this.Service.ServiceHelper.AbortUpdate(this, array);
				}
				catch (Exception ex)
				{
					if (RSTrace.CatalogTrace.TraceWarning)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Warning, string.Format(CultureInfo.InvariantCulture, "Fail to cleanup when there is error updating catalog item '{0}' to external storage. Exception is {1}.", this.ItemContext.ItemPath, ex.Message));
					}
				}
				throw;
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00023928 File Offset: 0x00021B28
		protected virtual void Update()
		{
			throw new InternalCatalogException("update is not supported for this item type: " + this.ThisItemType);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void FinalizeCreation()
		{
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00023944 File Offset: 0x00021B44
		internal void SaveProperties()
		{
			this.ProtectProperties();
			this.Service.Storage.SetAllProperties(this, this.Properties, this.Service.UserContext.IsInitialized ? this.Service.UserName : UserUtil.GetCurrentWindowsUserName(), this.ModificationDate);
			this.UnprotectProperties();
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002399E File Offset: 0x00021B9E
		internal virtual void ThrowIfRdceNotSupported()
		{
			throw new RdceInvalidItemTypeException(this.m_itemType.ToString());
		}

		// Token: 0x04000435 RID: 1077
		protected CatalogItemContext m_itemContext;

		// Token: 0x04000436 RID: 1078
		protected Guid m_itemID = Guid.Empty;

		// Token: 0x04000437 RID: 1079
		protected byte[] m_securityDescriptor;

		// Token: 0x04000438 RID: 1080
		private ItemType? m_itemType;

		// Token: 0x04000439 RID: 1081
		private ItemProperties m_properties;

		// Token: 0x0400043A RID: 1082
		private CatalogItem m_parent;

		// Token: 0x0400043B RID: 1083
		private string m_createdBy;

		// Token: 0x0400043C RID: 1084
		private DateTime m_creationDate = DateTime.MinValue;

		// Token: 0x0400043D RID: 1085
		private string m_modifiedBy;

		// Token: 0x0400043E RID: 1086
		private DateTime m_modificationDate = DateTime.MinValue;

		// Token: 0x0400043F RID: 1087
		protected string m_description;

		// Token: 0x04000440 RID: 1088
		protected readonly ItemMetadata m_itemMetadata = new ItemMetadata();

		// Token: 0x04000441 RID: 1089
		protected Guid m_linkID = Guid.Empty;

		// Token: 0x04000442 RID: 1090
		protected bool m_linkIDInit;

		// Token: 0x04000443 RID: 1091
		protected CatalogItemPath m_linkPath;

		// Token: 0x04000444 RID: 1092
		protected bool m_linkPathInit;

		// Token: 0x04000445 RID: 1093
		protected int m_executionOptions;

		// Token: 0x04000446 RID: 1094
		protected bool m_executionOptionsInit;

		// Token: 0x04000447 RID: 1095
		protected int m_historyLimit;

		// Token: 0x04000448 RID: 1096
		protected bool m_historyLimitInit;

		// Token: 0x04000449 RID: 1097
		protected ServerSnapshot m_compiledDefinition;

		// Token: 0x0400044A RID: 1098
		protected string m_parametersXml;

		// Token: 0x0400044B RID: 1099
		protected byte[] m_content;

		// Token: 0x0400044C RID: 1100
		protected DataSourceInfoCollection m_dataSources;

		// Token: 0x0400044D RID: 1101
		private RSService m_service;

		// Token: 0x0400044E RID: 1102
		private bool m_internalUsePermissionForExecution;
	}
}
