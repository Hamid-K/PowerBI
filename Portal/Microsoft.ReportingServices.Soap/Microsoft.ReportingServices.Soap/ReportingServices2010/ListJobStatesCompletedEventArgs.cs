using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000FC RID: 252
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListJobStatesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x0000E529 File Offset: 0x0000C729
		internal ListJobStatesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0000E53C File Offset: 0x0000C73C
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000268 RID: 616
		private object[] results;
	}
}
