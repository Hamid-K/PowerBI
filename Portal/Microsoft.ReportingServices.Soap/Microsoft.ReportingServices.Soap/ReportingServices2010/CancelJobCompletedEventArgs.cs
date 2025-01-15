using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000FE RID: 254
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CancelJobCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x0000E551 File Offset: 0x0000C751
		internal CancelJobCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0000E564 File Offset: 0x0000C764
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x04000269 RID: 617
		private object[] results;
	}
}
