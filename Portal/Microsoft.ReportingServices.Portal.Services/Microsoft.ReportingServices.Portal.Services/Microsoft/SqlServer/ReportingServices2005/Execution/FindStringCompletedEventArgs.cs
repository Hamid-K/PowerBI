using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000CC RID: 204
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class FindStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x0001BE5A File Offset: 0x0001A05A
		internal FindStringCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001BE6D File Offset: 0x0001A06D
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x040001E9 RID: 489
		private object[] results;
	}
}
