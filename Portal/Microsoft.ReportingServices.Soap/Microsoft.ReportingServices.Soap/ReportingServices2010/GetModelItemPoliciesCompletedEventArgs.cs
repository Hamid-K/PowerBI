using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000A3 RID: 163
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetModelItemPoliciesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0000DF1C File Offset: 0x0000C11C
		internal GetModelItemPoliciesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0000DF2F File Offset: 0x0000C12F
		public Policy[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Policy[])this.results[0];
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0000DF44 File Offset: 0x0000C144
		public bool InheritParent
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x04000246 RID: 582
		private object[] results;
	}
}
