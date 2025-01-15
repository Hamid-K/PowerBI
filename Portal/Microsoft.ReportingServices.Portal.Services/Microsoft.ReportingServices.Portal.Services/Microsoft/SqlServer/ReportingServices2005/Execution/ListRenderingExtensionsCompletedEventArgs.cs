using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x020000D6 RID: 214
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ListRenderingExtensionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x0001BFB5 File Offset: 0x0001A1B5
		internal ListRenderingExtensionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
		public Extension[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Extension[])this.results[0];
			}
		}

		// Token: 0x040001EE RID: 494
		private object[] results;
	}
}
