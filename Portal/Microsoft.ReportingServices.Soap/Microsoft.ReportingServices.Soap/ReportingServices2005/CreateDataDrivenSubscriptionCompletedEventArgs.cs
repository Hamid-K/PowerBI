using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002C9 RID: 713
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateDataDrivenSubscriptionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x0002301C File Offset: 0x0002121C
		internal CreateDataDrivenSubscriptionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0002302F File Offset: 0x0002122F
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000704 RID: 1796
		private object[] results;
	}
}
