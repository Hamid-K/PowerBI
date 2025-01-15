using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200015B RID: 347
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetModelItemPermissionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E09 RID: 3593 RVA: 0x000177C4 File Offset: 0x000159C4
		internal GetModelItemPermissionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x000177D7 File Offset: 0x000159D7
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000474 RID: 1140
		private object[] results;
	}
}
