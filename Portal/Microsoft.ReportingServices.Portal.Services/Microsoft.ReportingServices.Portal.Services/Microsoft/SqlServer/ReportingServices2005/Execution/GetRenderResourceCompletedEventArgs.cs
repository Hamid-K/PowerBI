using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000D4 RID: 212
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class GetRenderResourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001BF78 File Offset: 0x0001A178
		internal GetRenderResourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001BF8B File Offset: 0x0001A18B
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public string MimeType
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040001ED RID: 493
		private object[] results;
	}
}
