using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B1 RID: 177
	[OriginalName("SubscriptionSingle")]
	public class SubscriptionSingle : DataServiceQuerySingle<Subscription>
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0000EFF2 File Offset: 0x0000D1F2
		public SubscriptionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		public SubscriptionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000F007 File Offset: 0x0000D207
		public SubscriptionSingle(DataServiceQuerySingle<Subscription> query)
			: base(query)
		{
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0000F010 File Offset: 0x0000D210
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DataSource")]
		public DataSourceSingle DataSource
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._DataSource == null)
				{
					this._DataSource = new DataSourceSingle(base.Context, base.GetPath("DataSource"));
				}
				return this._DataSource;
			}
		}

		// Token: 0x0400038A RID: 906
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataSourceSingle _DataSource;
	}
}
