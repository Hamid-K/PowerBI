using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001F4 RID: 500
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LogonUserCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x000187E2 File Offset: 0x000169E2
		internal LogonUserCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x000187F5 File Offset: 0x000169F5
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0001880A File Offset: 0x00016A0A
		public string cookieName
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040004BB RID: 1211
		private object[] results;
	}
}
