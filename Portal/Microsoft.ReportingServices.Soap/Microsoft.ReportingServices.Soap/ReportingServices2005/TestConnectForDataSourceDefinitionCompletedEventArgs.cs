using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002AA RID: 682
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class TestConnectForDataSourceDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016F9 RID: 5881 RVA: 0x00022DA9 File Offset: 0x00020FA9
		internal TestConnectForDataSourceDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x00022DBC File Offset: 0x00020FBC
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00022DD1 File Offset: 0x00020FD1
		public string ConnectError
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040006F8 RID: 1784
		private object[] results;
	}
}
