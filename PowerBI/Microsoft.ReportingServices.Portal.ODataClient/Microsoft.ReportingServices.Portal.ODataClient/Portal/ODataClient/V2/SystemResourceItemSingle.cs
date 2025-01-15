using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200005B RID: 91
	[OriginalName("SystemResourceItemSingle")]
	public class SystemResourceItemSingle : DataServiceQuerySingle<SystemResourceItem>
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x000091F6 File Offset: 0x000073F6
		public SystemResourceItemSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00009200 File Offset: 0x00007400
		public SystemResourceItemSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000920B File Offset: 0x0000740B
		public SystemResourceItemSingle(DataServiceQuerySingle<SystemResourceItem> query)
			: base(query)
		{
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00009214 File Offset: 0x00007414
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ItemContent")]
		public CatalogItemSingle ItemContent
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._ItemContent == null)
				{
					this._ItemContent = new CatalogItemSingle(base.Context, base.GetPath("ItemContent"));
				}
				return this._ItemContent;
			}
		}

		// Token: 0x040001FA RID: 506
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemSingle _ItemContent;
	}
}
