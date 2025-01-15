using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200008E RID: 142
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class TestConnectForItemDataSourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0000DD88 File Offset: 0x0000BF88
		internal TestConnectForItemDataSourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0000DD9B File Offset: 0x0000BF9B
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public string ConnectError
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x0400023E RID: 574
		private object[] results;
	}
}
