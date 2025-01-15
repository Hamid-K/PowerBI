using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001F0 RID: 496
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelPerspectivesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001008 RID: 4104 RVA: 0x000183B1 File Offset: 0x000165B1
		internal ListModelPerspectivesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x000183C4 File Offset: 0x000165C4
		public ModelCatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelCatalogItem[])this.results[0];
			}
		}

		// Token: 0x040004AF RID: 1199
		private object[] results;
	}
}
