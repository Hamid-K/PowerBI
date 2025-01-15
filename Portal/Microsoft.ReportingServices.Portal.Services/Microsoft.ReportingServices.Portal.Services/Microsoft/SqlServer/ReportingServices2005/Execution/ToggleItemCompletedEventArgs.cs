using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000C6 RID: 198
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ToggleItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x0001BDCD File Offset: 0x00019FCD
		internal ToggleItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		public bool Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[0];
			}
		}

		// Token: 0x040001E6 RID: 486
		private object[] results;
	}
}
