using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A8 RID: 168
	internal abstract class CreateItemAction<TParameterType, TCreatedType> : RSSoapAction<TParameterType> where TParameterType : CreateItemActionParameters, new() where TCreatedType : CatalogItem
	{
		// Token: 0x060007CA RID: 1994 RVA: 0x00020047 File Offset: 0x0001E247
		public CreateItemAction(string actionName, RSService service)
			: base(actionName, service)
		{
			if (base.ActionParameters.Overwrite && !this.IsUpdateSupported)
			{
				throw new InternalCatalogException("overwrite is true but update is not supported");
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00020076 File Offset: 0x0001E276
		protected ItemType ItemType
		{
			get
			{
				return CatalogItem.ExtractCatalogTypeFromRuntimeType(typeof(TCreatedType));
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected virtual bool IsUpdateSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected virtual bool AllowVirtualItems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00020087 File Offset: 0x0001E287
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0002008F File Offset: 0x0001E28F
		private protected ItemType ResolvedItemType { protected get; private set; }

		// Token: 0x060007D0 RID: 2000 RVA: 0x00020098 File Offset: 0x0001E298
		private void InitializeNewItem(TCreatedType item, bool newItem)
		{
			string userName = base.Service.UserName;
			DateTime now = DateTime.Now;
			item.ModifiedBy = userName;
			item.ModificationDate = now;
			if (newItem)
			{
				item.CreatedBy = userName;
				item.CreationDate = now;
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000200EC File Offset: 0x0001E2EC
		internal override void PerformActionNow()
		{
			this.InitAndCheckParams();
			string text = base.ActionParameters.ParentPath;
			bool flag = false;
			ItemType itemType = ItemType.Unknown;
			Guid guid = Guid.Empty;
			byte[] array = null;
			ItemType itemType2;
			if (this.AllowVirtualItems)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = base.Service.CatalogToExternal(new CatalogItemPath("/")).Value;
					flag = true;
				}
				else if (WebRequestUtil.IsViaPortal())
				{
					Guid empty = Guid.Empty;
					byte[] array2 = null;
					string text2 = CatalogItemNameUtility.BuildChildPath(new CatalogItemContext(base.Service, text, "parent").ItemPath.Value, base.ActionParameters.ItemName);
					CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, text2, "ItemPath");
					if (base.Service.Storage.ObjectExists(catalogItemContext.ItemPath, out itemType, out empty, out array2) && itemType == ItemType.DataSet)
					{
						text = base.ActionParameters.ParentPath;
						itemType2 = itemType;
						guid = empty;
						array = array2;
						flag = true;
					}
					else
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "The api call to EditSession did not came from the same machine, or was not sent from RSPortal.exe. Only report ItemType will be allowed.");
					}
				}
			}
			this.ResolvedItemType = itemType;
			CatalogItemContext catalogItemContext2 = new CatalogItemContext(base.Service, text, "parent");
			base.ActionParameters.ItemName = CatalogItemNameUtility.ValidateAndTrimItemName(base.ActionParameters.ItemName, "name");
			string text3 = CatalogItemNameUtility.BuildChildPath(catalogItemContext2.ItemPath.Value, base.ActionParameters.ItemName);
			CatalogItemContext catalogItemContext3;
			if (flag)
			{
				catalogItemContext3 = new CatalogItemContext(base.Service);
				catalogItemContext3.SetPath(text3, ItemPathOptions.IgnoreValidateEditSession);
			}
			else
			{
				catalogItemContext3 = new CatalogItemContext(base.Service, text3, "ItemPath");
			}
			TCreatedType tcreatedType = CatalogItemFactory.CreateCatalogItem(this.ItemType, base.Service) as TCreatedType;
			RSTrace.CatalogTrace.Assert(tcreatedType != null, "item is not TCreatedType");
			if (flag || !base.Service.Storage.ObjectExists(catalogItemContext3.ItemPath, out itemType2, out guid, out array))
			{
				tcreatedType.Initialize(catalogItemContext3, guid, array);
				this.PerformCreateActions(catalogItemContext2, catalogItemContext3, tcreatedType, flag);
				return;
			}
			if (this.IsUpdateSupported)
			{
				if (tcreatedType is IEditSessionAware)
				{
					CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext2);
					if (!base.ActionParameters.SkipSecurityCheck && !base.Service.SecMgr.CheckAccess(ItemType.Folder, catalogItem.SecurityDescriptor, FolderOperation.CreateReport, catalogItemContext2.ItemPath))
					{
						throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
					}
				}
				tcreatedType.Initialize(catalogItemContext3, guid, array);
				this.PerformUpdateActions(catalogItemContext3, tcreatedType, itemType2, guid, array);
				return;
			}
			throw new ItemAlreadyExistsException(text3);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000203B3 File Offset: 0x0001E5B3
		private void PerformCreateActions(CatalogItemContext parentContext, CatalogItemContext itemContext, TCreatedType item, bool isVirtualItem)
		{
			this.InitializeNewItem(item, true);
			this.CreateNew(item, parentContext, isVirtualItem);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000203C8 File Offset: 0x0001E5C8
		private void PerformUpdateActions(CatalogItemContext itemContext, TCreatedType item, ItemType actualItemType, Guid actualItemID, byte[] actualSecurityDescriptor)
		{
			if (!base.ActionParameters.Overwrite)
			{
				throw new ItemAlreadyExistsException(item.ItemContext.ItemPath.Value);
			}
			item.ThrowIfWrongItemType(new ItemType[] { actualItemType });
			this.InitializeNewItem(item, false);
			this.CreateOverwrite(item);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00020428 File Offset: 0x0001E628
		protected virtual void CreateOverwrite(TCreatedType item)
		{
			if (base.ActionParameters.Properties != null && base.ActionParameters.Properties.Length != 0)
			{
				item.ThrowIfNoAccess(CommonOperation.UpdateProperties);
				ItemProperties itemProperties = new ItemProperties(base.ActionParameters.Properties, this.ItemType);
				base.Service.SetPropertiesAction.SetProperties(item, itemProperties);
			}
			this.UpdateExistingItem(item);
			this.CollectAndReturnItemInfo(item);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000204A7 File Offset: 0x0001E6A7
		internal void CreateNew(TCreatedType itemToCreate, CatalogItemContext parentContext)
		{
			this.CreateNew(itemToCreate, parentContext, false);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000204B4 File Offset: 0x0001E6B4
		private void CreateNew(TCreatedType itemToCreate, CatalogItemContext parentContext, bool isVirtualItem)
		{
			if (!isVirtualItem)
			{
				CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(parentContext);
				catalogItem.ThrowIfWrongItemType(new ItemType[]
				{
					ItemType.Folder,
					ItemType.Site,
					ItemType.MobileReport,
					ItemType.DataSource,
					ItemType.Report,
					ItemType.LinkedReport,
					ItemType.MobileReport,
					ItemType.Kpi,
					ItemType.PowerBIReport,
					ItemType.ExcelWorkbook
				});
				itemToCreate.Parent = catalogItem;
				if (!base.ActionParameters.SkipSecurityCheck)
				{
					base.Service.EnsureAllowedAsSubitem(catalogItem.ThisItemType, itemToCreate.ThisItemType, catalogItem.SecurityDescriptor, catalogItem.ItemContext.ItemPath, itemToCreate.ItemContext.ItemName);
				}
				this.ResolveItemLinks();
				ItemProperties itemProperties = new ItemProperties(base.ActionParameters.Properties, itemToCreate.ThisItemType);
				itemProperties.EnsurePropertiesWritable();
				itemToCreate.CombineProperties(itemProperties);
			}
			if (!base.ActionParameters.SkipSecurityCheck)
			{
				this.PerformVirtualItemSecurityCheck(itemToCreate);
			}
			this.PrepareForNewItem(itemToCreate);
			this.CreateItem(itemToCreate);
			this.FinalizeNewItem(itemToCreate);
			this.CollectAndReturnItemInfo(itemToCreate);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void PerformVirtualItemSecurityCheck(TCreatedType item)
		{
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000205BA File Offset: 0x0001E7BA
		protected virtual void CreateItem(TCreatedType item)
		{
			item.Create();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void InitAndCheckParams()
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void UpdateExistingItem(TCreatedType item)
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void ResolveItemLinks()
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void PrepareForNewItem(TCreatedType item)
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void FinalizeNewItem(TCreatedType item)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000205C7 File Offset: 0x0001E7C7
		protected virtual void CollectAndReturnItemInfo(TCreatedType item)
		{
			if (item.Properties == null)
			{
				item.LoadProperties();
			}
			base.ActionParameters.ItemInfo = item;
		}
	}
}
