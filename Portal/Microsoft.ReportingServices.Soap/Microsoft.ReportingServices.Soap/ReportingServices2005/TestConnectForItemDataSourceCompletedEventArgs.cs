using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002AC RID: 684
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class TestConnectForItemDataSourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001700 RID: 5888 RVA: 0x00022DE6 File Offset: 0x00020FE6
		internal TestConnectForItemDataSourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00022DF9 File Offset: 0x00020FF9
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00022E0E File Offset: 0x0002100E
		public string ConnectError
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040006F9 RID: 1785
		private object[] results;
	}
}
