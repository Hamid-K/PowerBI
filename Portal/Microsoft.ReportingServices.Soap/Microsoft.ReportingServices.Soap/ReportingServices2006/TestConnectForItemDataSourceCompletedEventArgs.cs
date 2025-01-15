using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001A9 RID: 425
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class TestConnectForItemDataSourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F0C RID: 3852 RVA: 0x00017D07 File Offset: 0x00015F07
		internal TestConnectForItemDataSourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00017D1A File Offset: 0x00015F1A
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00017D2F File Offset: 0x00015F2F
		public string ConnectError
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x04000492 RID: 1170
		private object[] results;
	}
}
