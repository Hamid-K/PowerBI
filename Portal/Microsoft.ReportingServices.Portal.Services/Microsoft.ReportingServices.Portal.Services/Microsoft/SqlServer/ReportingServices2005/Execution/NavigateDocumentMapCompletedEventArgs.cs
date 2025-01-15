using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000C8 RID: 200
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class NavigateDocumentMapCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x0001BDF5 File Offset: 0x00019FF5
		internal NavigateDocumentMapCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001BE08 File Offset: 0x0001A008
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x040001E7 RID: 487
		private object[] results;
	}
}
