using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000C9 RID: 201
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateReportEditSessionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x0000E189 File Offset: 0x0000C389
		internal CreateReportEditSessionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0000E19C File Offset: 0x0000C39C
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0000E1B1 File Offset: 0x0000C3B1
		public Warning[] Warnings
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[1];
			}
		}

		// Token: 0x04000255 RID: 597
		private object[] results;
	}
}
