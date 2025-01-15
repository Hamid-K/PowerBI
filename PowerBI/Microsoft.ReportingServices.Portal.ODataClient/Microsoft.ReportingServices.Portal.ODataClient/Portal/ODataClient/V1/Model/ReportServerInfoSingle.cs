using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D8 RID: 216
	[OriginalName("ReportServerInfoSingle")]
	public class ReportServerInfoSingle : DataServiceQuerySingle<ReportServerInfo>
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x00013585 File Offset: 0x00011785
		public ReportServerInfoSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001358F File Offset: 0x0001178F
		public ReportServerInfoSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001359A File Offset: 0x0001179A
		public ReportServerInfoSingle(DataServiceQuerySingle<ReportServerInfo> query)
			: base(query)
		{
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x000135A3 File Offset: 0x000117A3
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Policies")]
		public DataServiceQuery<SystemPolicy> Policies
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Policies == null)
				{
					this._Policies = base.Context.CreateQuery<SystemPolicy>(base.GetPath("Policies"));
				}
				return this._Policies;
			}
		}

		// Token: 0x04000480 RID: 1152
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemPolicy> _Policies;
	}
}
