using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000245 RID: 581
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPoliciesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x0002278B File Offset: 0x0002098B
		internal GetPoliciesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0002279E File Offset: 0x0002099E
		public Policy[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Policy[])this.results[0];
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000227B3 File Offset: 0x000209B3
		public bool InheritParent
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x040006D4 RID: 1748
		private object[] results;
	}
}
