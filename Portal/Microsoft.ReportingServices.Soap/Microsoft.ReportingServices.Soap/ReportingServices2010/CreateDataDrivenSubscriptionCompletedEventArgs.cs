using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000071 RID: 113
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateDataDrivenSubscriptionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0000DB69 File Offset: 0x0000BD69
		internal CreateDataDrivenSubscriptionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000232 RID: 562
		private object[] results;
	}
}
