using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200025B RID: 603
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetModelItemPermissionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015EE RID: 5614 RVA: 0x00022908 File Offset: 0x00020B08
		internal GetModelItemPermissionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0002291B File Offset: 0x00020B1B
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x040006DD RID: 1757
		private object[] results;
	}
}
