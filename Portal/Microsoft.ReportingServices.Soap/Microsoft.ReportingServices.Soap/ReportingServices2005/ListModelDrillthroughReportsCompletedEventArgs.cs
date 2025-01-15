using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000263 RID: 611
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelDrillthroughReportsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600160B RID: 5643 RVA: 0x0002296D File Offset: 0x00020B6D
		internal ListModelDrillthroughReportsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00022980 File Offset: 0x00020B80
		public ModelDrillthroughReport[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelDrillthroughReport[])this.results[0];
			}
		}

		// Token: 0x040006DF RID: 1759
		private object[] results;
	}
}
