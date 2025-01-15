using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200009A RID: 154
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPoliciesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0000DE52 File Offset: 0x0000C052
		internal GetPoliciesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0000DE65 File Offset: 0x0000C065
		public Policy[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Policy[])this.results[0];
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0000DE7A File Offset: 0x0000C07A
		public bool InheritParent
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x04000242 RID: 578
		private object[] results;
	}
}
