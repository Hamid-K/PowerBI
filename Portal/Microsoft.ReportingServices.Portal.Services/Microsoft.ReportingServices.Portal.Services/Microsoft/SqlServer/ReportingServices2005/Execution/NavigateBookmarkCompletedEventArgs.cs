using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000CA RID: 202
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class NavigateBookmarkCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0001BE1D File Offset: 0x0001A01D
		internal NavigateBookmarkCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001BE30 File Offset: 0x0001A030
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001BE45 File Offset: 0x0001A045
		public string UniqueName
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040001E8 RID: 488
		private object[] results;
	}
}
