using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000ED RID: 237
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSystemPoliciesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x0000E411 File Offset: 0x0000C611
		internal GetSystemPoliciesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0000E424 File Offset: 0x0000C624
		public Policy[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Policy[])this.results[0];
			}
		}

		// Token: 0x04000261 RID: 609
		private object[] results;
	}
}
