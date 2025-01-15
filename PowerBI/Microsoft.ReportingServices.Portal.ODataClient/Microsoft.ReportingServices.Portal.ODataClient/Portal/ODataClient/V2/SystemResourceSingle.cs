using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000059 RID: 89
	[OriginalName("SystemResourceSingle")]
	public class SystemResourceSingle : DataServiceQuerySingle<SystemResource>
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x00008FBD File Offset: 0x000071BD
		public SystemResourceSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00008FC7 File Offset: 0x000071C7
		public SystemResourceSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00008FD2 File Offset: 0x000071D2
		public SystemResourceSingle(DataServiceQuerySingle<SystemResource> query)
			: base(query)
		{
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00008FDB File Offset: 0x000071DB
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

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000901A File Offset: 0x0000721A
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

		// Token: 0x040001EF RID: 495
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemSingle _PackageContent;

		// Token: 0x040001F0 RID: 496
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemResourceItem> _Items;
	}
}
