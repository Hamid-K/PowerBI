using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000DD RID: 221
	[OriginalName("SystemResourceSingle")]
	public class SystemResourceSingle : DataServiceQuerySingle<SystemResource>
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x00014228 File Offset: 0x00012428
		public SystemResourceSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00014232 File Offset: 0x00012432
		public SystemResourceSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0001423D File Offset: 0x0001243D
		public SystemResourceSingle(DataServiceQuerySingle<SystemResource> query)
			: base(query)
		{
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00014246 File Offset: 0x00012446
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

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00014285 File Offset: 0x00012485
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

		// Token: 0x0400049C RID: 1180
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemSingle _PackageContent;

		// Token: 0x0400049D RID: 1181
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemResourceItem> _Items;
	}
}
