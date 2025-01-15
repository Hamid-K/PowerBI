using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000157 RID: 343
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUserModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000DFD RID: 3581 RVA: 0x00017774 File Offset: 0x00015974
		internal GetUserModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00017787 File Offset: 0x00015987
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x04000472 RID: 1138
		private object[] results;
	}
}
