using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000163 RID: 355
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelDrillthroughReportsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x00017829 File Offset: 0x00015A29
		internal ListModelDrillthroughReportsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0001783C File Offset: 0x00015A3C
		public ModelDrillthroughReport[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelDrillthroughReport[])this.results[0];
			}
		}

		// Token: 0x04000476 RID: 1142
		private object[] results;
	}
}
