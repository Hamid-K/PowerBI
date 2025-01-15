using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200008C RID: 140
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class TestConnectForDataSourceDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0000DD4B File Offset: 0x0000BF4B
		internal TestConnectForDataSourceDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0000DD5E File Offset: 0x0000BF5E
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0000DD73 File Offset: 0x0000BF73
		public string ConnectError
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x0400023D RID: 573
		private object[] results;
	}
}
