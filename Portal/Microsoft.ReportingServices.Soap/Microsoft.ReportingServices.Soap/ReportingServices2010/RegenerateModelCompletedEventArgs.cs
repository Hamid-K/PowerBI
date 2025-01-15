using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000B1 RID: 177
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class RegenerateModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0000E021 File Offset: 0x0000C221
		internal RegenerateModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0000E034 File Offset: 0x0000C234
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x0400024C RID: 588
		private object[] results;
	}
}
