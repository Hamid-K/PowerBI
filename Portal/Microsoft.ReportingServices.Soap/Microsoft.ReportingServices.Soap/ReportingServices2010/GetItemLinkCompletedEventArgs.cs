using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000CD RID: 205
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemLinkCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x0000E1C6 File Offset: 0x0000C3C6
		internal GetItemLinkCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0000E1D9 File Offset: 0x0000C3D9
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x04000256 RID: 598
		private object[] results;
	}
}
