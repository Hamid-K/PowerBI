using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000249 RID: 585
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSystemPermissionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015B6 RID: 5558 RVA: 0x000227C8 File Offset: 0x000209C8
		internal GetSystemPermissionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000227DB File Offset: 0x000209DB
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x040006D5 RID: 1749
		private object[] results;
	}
}
