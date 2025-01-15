using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000269 RID: 617
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSecureMethodsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600161D RID: 5661 RVA: 0x000229E5 File Offset: 0x00020BE5
		internal ListSecureMethodsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x000229F8 File Offset: 0x00020BF8
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x040006E2 RID: 1762
		private object[] results;
	}
}
