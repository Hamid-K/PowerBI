using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.ComponentLibrary.Engine;
using Microsoft.ReportingServices.ComponentLibrary.Engine.Common.Exceptions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000CE RID: 206
	[CatalogItemType(ItemType.Component)]
	internal class ComponentCatalogItem : CatalogItem
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x00004F8E File Offset: 0x0000318E
		internal ComponentCatalogItem(RSService service)
			: base(service)
		{
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00023D5A File Offset: 0x00021F5A
		internal Guid ComponentID
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_itemMetadata.ComponentID;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00023D67 File Offset: 0x00021F67
		internal string SubType
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_itemMetadata.SubType;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00004FDD File Offset: 0x000031DD
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00004FE5 File Offset: 0x000031E5
		internal override byte[] Content
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00023D74 File Offset: 0x00021F74
		internal void LoadFromDefinition(byte[] definition)
		{
			byte[] array = ComponentLibraryUpgrader.UpgradeToCurrent(definition);
			using (Stream stream = new MemoryStream(array))
			{
				try
				{
					ComponentItem componentItem = ComponentItemFactory.CreateFromXml(stream);
					this.m_itemMetadata.SubType = componentItem.Type.Name;
					ComponentMetadata metadata = componentItem.Metadata;
					this.m_description = metadata.Description;
					if (metadata.ComponentId != null)
					{
						this.m_itemMetadata.ComponentID = metadata.ComponentId.Value;
					}
				}
				catch (ComponentEngineException ex)
				{
					throw new ComponentPublishingError(ex);
				}
			}
			this.m_content = ((array == null) ? definition : array);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000050E1 File Offset: 0x000032E1
		internal void ThrowIfNoAccess(ResourceOperation operation)
		{
			if (!base.Service.SecMgr.CheckAccess(base.ThisItemType, base.SecurityDescriptor, operation, base.ItemContext.ItemPath))
			{
				throw new AccessDeniedException(base.Service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00023E28 File Offset: 0x00022028
		protected override void ContentLoadSecurityCheck()
		{
			this.ThrowIfNoAccess(ResourceOperation.ReadContent);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00023E34 File Offset: 0x00022034
		protected override void Update()
		{
			base.Service.Storage.SetObjectContent(base.ItemContext.CatalogItemPath, ItemType.Component, this.Content, Guid.Empty, null, Guid.Empty, null, null, this.SubType, this.ComponentID);
		}
	}
}
