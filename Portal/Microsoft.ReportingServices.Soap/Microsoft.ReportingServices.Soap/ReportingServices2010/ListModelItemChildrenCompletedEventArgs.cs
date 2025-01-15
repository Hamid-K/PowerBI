using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000AB RID: 171
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelItemChildrenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x0000DFA9 File Offset: 0x0000C1A9
		internal ListModelItemChildrenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		public ModelItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelItem[])this.results[0];
			}
		}

		// Token: 0x04000249 RID: 585
		private object[] results;
	}
}
