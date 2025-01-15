using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200011A RID: 282
	[OriginalName("SystemResourcePackageSingle")]
	public class SystemResourcePackageSingle : DataServiceQuerySingle<SystemResourcePackage>
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x000177B3 File Offset: 0x000159B3
		public SystemResourcePackageSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000177BD File Offset: 0x000159BD
		public SystemResourcePackageSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000177C8 File Offset: 0x000159C8
		public SystemResourcePackageSingle(DataServiceQuerySingle<SystemResourcePackage> query)
			: base(query)
		{
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x000177D1 File Offset: 0x000159D1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("PackageContent")]
		public CatalogItemSingle PackageContent
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._PackageContent == null)
				{
					this._PackageContent = new CatalogItemSingle(base.Context, base.GetPath("PackageContent"));
				}
				return this._PackageContent;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00017810 File Offset: 0x00015A10
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Items")]
		public DataServiceQuery<SystemResourceItem> Items
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Items == null)
				{
					this._Items = base.Context.CreateQuery<SystemResourceItem>(base.GetPath("Items"));
				}
				return this._Items;
			}
		}

		// Token: 0x0400057E RID: 1406
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemSingle _PackageContent;

		// Token: 0x0400057F RID: 1407
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemResourceItem> _Items;
	}
}
