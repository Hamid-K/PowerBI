using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000089 RID: 137
	[OriginalName("SystemResourcePackageSingle")]
	public class SystemResourcePackageSingle : DataServiceQuerySingle<SystemResourcePackage>
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x0000BB2E File Offset: 0x00009D2E
		public SystemResourcePackageSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000BB38 File Offset: 0x00009D38
		public SystemResourcePackageSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000BB43 File Offset: 0x00009D43
		public SystemResourcePackageSingle(DataServiceQuerySingle<SystemResourcePackage> query)
			: base(query)
		{
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000BB4C File Offset: 0x00009D4C
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000BB8B File Offset: 0x00009D8B
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

		// Token: 0x040002AA RID: 682
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemSingle _PackageContent;

		// Token: 0x040002AB RID: 683
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemResourceItem> _Items;
	}
}
