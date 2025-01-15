using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001E4 RID: 484
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPoliciesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FE0 RID: 4064 RVA: 0x000182BF File Offset: 0x000164BF
		internal GetPoliciesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x000182D2 File Offset: 0x000164D2
		public Policy[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Policy[])this.results[0];
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000182E7 File Offset: 0x000164E7
		public bool InheritParent
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x040004AA RID: 1194
		private object[] results;
	}
}
