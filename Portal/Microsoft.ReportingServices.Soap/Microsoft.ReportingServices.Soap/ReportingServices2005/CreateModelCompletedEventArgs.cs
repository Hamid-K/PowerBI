using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200024F RID: 591
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015CA RID: 5578 RVA: 0x00022818 File Offset: 0x00020A18
		internal CreateModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0002282B File Offset: 0x00020A2B
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x040006D7 RID: 1751
		private object[] results;
	}
}
