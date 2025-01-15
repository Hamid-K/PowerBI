using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000091 RID: 145
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ListSecureMethodsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x0001B882 File Offset: 0x00019A82
		internal ListSecureMethodsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001B895 File Offset: 0x00019A95
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x040001CC RID: 460
		private object[] results;
	}
}
