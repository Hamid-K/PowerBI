using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200029D RID: 669
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CancelJobCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016CD RID: 5837 RVA: 0x00022D09 File Offset: 0x00020F09
		internal CancelJobCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00022D1C File Offset: 0x00020F1C
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x040006F4 RID: 1780
		private object[] results;
	}
}
