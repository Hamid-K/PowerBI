using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000BE RID: 190
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class GetDocumentMapCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0001BD2D File Offset: 0x00019F2D
		internal GetDocumentMapCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001BD40 File Offset: 0x00019F40
		public DocumentMapNode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DocumentMapNode)this.results[0];
			}
		}

		// Token: 0x040001E2 RID: 482
		private object[] results;
	}
}
