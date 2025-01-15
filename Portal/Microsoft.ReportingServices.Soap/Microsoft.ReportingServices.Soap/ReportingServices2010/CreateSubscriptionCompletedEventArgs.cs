using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200006F RID: 111
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateSubscriptionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0000DB41 File Offset: 0x0000BD41
		internal CreateSubscriptionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000231 RID: 561
		private object[] results;
	}
}
