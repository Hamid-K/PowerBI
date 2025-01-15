using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000EF RID: 239
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListExtensionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x0000E439 File Offset: 0x0000C639
		internal ListExtensionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0000E44C File Offset: 0x0000C64C
		public Extension[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Extension[])this.results[0];
			}
		}

		// Token: 0x04000262 RID: 610
		private object[] results;
	}
}
