using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000057 RID: 87
	[OriginalName("SubscriptionSingle")]
	public class SubscriptionSingle : DataServiceQuerySingle<Subscription>
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00008B65 File Offset: 0x00006D65
		public SubscriptionSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00008B6F File Offset: 0x00006D6F
		public SubscriptionSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00008B7A File Offset: 0x00006D7A
		public SubscriptionSingle(DataServiceQuerySingle<Subscription> query)
			: base(query)
		{
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00008B83 File Offset: 0x00006D83
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

		// Token: 0x040001DA RID: 474
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataSourceSingle _DataSource;
	}
}
