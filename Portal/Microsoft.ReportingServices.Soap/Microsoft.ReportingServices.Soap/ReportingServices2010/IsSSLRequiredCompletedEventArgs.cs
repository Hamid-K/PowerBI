using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000E4 RID: 228
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class IsSSLRequiredCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000853 RID: 2131 RVA: 0x0000E399 File Offset: 0x0000C599
		internal IsSSLRequiredCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x0400025E RID: 606
		private object[] results;
	}
}
