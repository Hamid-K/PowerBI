using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000259 RID: 601
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelItemChildrenCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015E8 RID: 5608 RVA: 0x000228E0 File Offset: 0x00020AE0
		internal ListModelItemChildrenCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x000228F3 File Offset: 0x00020AF3
		public ModelItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelItem[])this.results[0];
			}
		}

		// Token: 0x040006DC RID: 1756
		private object[] results;
	}
}
