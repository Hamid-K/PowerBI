using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000169 RID: 361
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportServerConfigInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x000178B6 File Offset: 0x00015AB6
		internal GetReportServerConfigInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x000178C9 File Offset: 0x00015AC9
		public ServerConfigInfo[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ServerConfigInfo[])this.results[0];
			}
		}

		// Token: 0x04000479 RID: 1145
		private object[] results;
	}
}
