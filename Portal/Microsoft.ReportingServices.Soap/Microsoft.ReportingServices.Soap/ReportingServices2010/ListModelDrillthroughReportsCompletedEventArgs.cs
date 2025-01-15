using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000A9 RID: 169
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelDrillthroughReportsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x0000DF81 File Offset: 0x0000C181
		internal ListModelDrillthroughReportsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0000DF94 File Offset: 0x0000C194
		public ModelDrillthroughReport[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelDrillthroughReport[])this.results[0];
			}
		}

		// Token: 0x04000248 RID: 584
		private object[] results;
	}
}
