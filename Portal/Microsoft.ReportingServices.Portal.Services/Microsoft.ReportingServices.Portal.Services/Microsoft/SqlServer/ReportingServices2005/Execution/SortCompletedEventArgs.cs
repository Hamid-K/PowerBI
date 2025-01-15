using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000CE RID: 206
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SortCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x0001BE82 File Offset: 0x0001A082
		internal SortCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001BE95 File Offset: 0x0001A095
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001BEAA File Offset: 0x0001A0AA
		public string ReportItem
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001BEBF File Offset: 0x0001A0BF
		public int NumPages
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[2];
			}
		}

		// Token: 0x040001EA RID: 490
		private object[] results;
	}
}
