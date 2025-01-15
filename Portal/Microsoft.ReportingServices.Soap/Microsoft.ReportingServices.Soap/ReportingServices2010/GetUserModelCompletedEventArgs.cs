using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000A5 RID: 165
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUserModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x0000DF59 File Offset: 0x0000C159
		internal GetUserModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x04000247 RID: 583
		private object[] results;
	}
}
