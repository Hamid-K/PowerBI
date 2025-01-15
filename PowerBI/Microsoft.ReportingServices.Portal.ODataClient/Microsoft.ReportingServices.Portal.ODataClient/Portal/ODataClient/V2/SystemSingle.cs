using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000047 RID: 71
	[OriginalName("SystemSingle")]
	public class SystemSingle : DataServiceQuerySingle<Microsoft.ReportingServices.Portal.ODataClient.V2.System>
	{
		// Token: 0x06000329 RID: 809 RVA: 0x00007AE6 File Offset: 0x00005CE6
		public SystemSingle(DataServiceContext context, string path)
			: base(context, path)
		{
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public SystemSingle(DataServiceContext context, string path, bool isComposable)
			: base(context, path, isComposable)
		{
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00007AFB File Offset: 0x00005CFB
		public SystemSingle(DataServiceQuerySingle<Microsoft.ReportingServices.Portal.ODataClient.V2.System> query)
			: base(query)
		{
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00007B04 File Offset: 0x00005D04
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AllowedActions")]
		public DataServiceQuery<AllowedAction> AllowedActions
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._AllowedActions == null)
				{
					this._AllowedActions = base.Context.CreateQuery<AllowedAction>(base.GetPath("AllowedActions"));
				}
				return this._AllowedActions;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00007B43 File Offset: 0x00005D43
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00007B82 File Offset: 0x00005D82
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Properties")]
		public DataServiceQuery<Property> Properties
		{
			get
			{
				if (!base.IsComposable)
				{
					throw new NotSupportedException("The previous function is not composable.");
				}
				if (this._Properties == null)
				{
					this._Properties = base.Context.CreateQuery<Property>(base.GetPath("Properties"));
				}
				return this._Properties;
			}
		}

		// Token: 0x04000196 RID: 406
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<AllowedAction> _AllowedActions;

		// Token: 0x04000197 RID: 407
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<SystemPolicy> _Policies;

		// Token: 0x04000198 RID: 408
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DataServiceQuery<Property> _Properties;
	}
}
