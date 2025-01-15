using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000199 RID: 409
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CancelJobCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x00017C02 File Offset: 0x00015E02
		internal CancelJobCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00017C15 File Offset: 0x00015E15
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x0400048C RID: 1164
		private object[] results;
	}
}
