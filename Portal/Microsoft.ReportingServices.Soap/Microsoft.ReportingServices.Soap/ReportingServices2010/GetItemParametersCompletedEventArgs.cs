using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000C3 RID: 195
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemParametersCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x0000E111 File Offset: 0x0000C311
		internal GetItemParametersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0000E124 File Offset: 0x0000C324
		public ItemParameter[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ItemParameter[])this.results[0];
			}
		}

		// Token: 0x04000252 RID: 594
		private object[] results;
	}
}
