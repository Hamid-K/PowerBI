using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000242 RID: 578
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSystemPoliciesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600159D RID: 5533 RVA: 0x00022763 File Offset: 0x00020963
		internal GetSystemPoliciesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00022776 File Offset: 0x00020976
		public Policy[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Policy[])this.results[0];
			}
		}

		// Token: 0x040006D3 RID: 1747
		private object[] results;
	}
}
