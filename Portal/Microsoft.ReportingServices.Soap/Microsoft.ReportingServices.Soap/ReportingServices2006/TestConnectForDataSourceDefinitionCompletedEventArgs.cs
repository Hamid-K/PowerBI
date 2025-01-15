using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001A7 RID: 423
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class TestConnectForDataSourceDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F05 RID: 3845 RVA: 0x00017CCA File Offset: 0x00015ECA
		internal TestConnectForDataSourceDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00017CDD File Offset: 0x00015EDD
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00017CF2 File Offset: 0x00015EF2
		public string ConnectError
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x04000491 RID: 1169
		private object[] results;
	}
}
