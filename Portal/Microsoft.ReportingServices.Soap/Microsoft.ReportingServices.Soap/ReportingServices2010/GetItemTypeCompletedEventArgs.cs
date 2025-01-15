using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200004D RID: 77
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemTypeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x0000D826 File Offset: 0x0000BA26
		internal GetItemTypeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000D839 File Offset: 0x0000BA39
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000225 RID: 549
		private object[] results;
	}
}
