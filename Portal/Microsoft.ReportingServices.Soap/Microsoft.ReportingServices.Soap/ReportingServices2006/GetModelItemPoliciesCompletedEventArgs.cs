using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200015D RID: 349
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetModelItemPoliciesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x000177EC File Offset: 0x000159EC
		internal GetModelItemPoliciesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x000177FF File Offset: 0x000159FF
		public Policy[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Policy[])this.results[0];
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00017814 File Offset: 0x00015A14
		public bool InheritParent
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x04000475 RID: 1141
		private object[] results;
	}
}
