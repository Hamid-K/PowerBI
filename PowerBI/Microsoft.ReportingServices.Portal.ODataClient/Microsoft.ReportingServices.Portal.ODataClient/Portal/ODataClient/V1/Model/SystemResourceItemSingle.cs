using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000DF RID: 223
	[OriginalName("SystemResourceItemSingle")]
	public class SystemResourceItemSingle : DataServiceQuerySingle<SystemResourceItem>
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0001445E File Offset: 0x0001265E
		public SystemResourceItemSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00014468 File Offset: 0x00012668
		public SystemResourceItemSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00014473 File Offset: 0x00012673
		public SystemResourceItemSingle(DataServiceQuerySingle<SystemResourceItem> query)
			: base(query)
		{
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0001447C File Offset: 0x0001267C
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

		// Token: 0x040004A7 RID: 1191
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private CatalogItemSingle _ItemContent;
	}
}
