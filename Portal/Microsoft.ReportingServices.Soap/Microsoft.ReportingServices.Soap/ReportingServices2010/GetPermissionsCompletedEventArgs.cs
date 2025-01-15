using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200010A RID: 266
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPermissionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x0000E65A File Offset: 0x0000C85A
		internal GetPermissionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0000E66D File Offset: 0x0000C86D
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x0400026D RID: 621
		private object[] results;
	}
}
