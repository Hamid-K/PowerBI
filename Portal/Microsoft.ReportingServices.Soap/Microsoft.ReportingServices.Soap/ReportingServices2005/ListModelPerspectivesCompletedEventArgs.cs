using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000255 RID: 597
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelPerspectivesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015DC RID: 5596 RVA: 0x00022890 File Offset: 0x00020A90
		internal ListModelPerspectivesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000228A3 File Offset: 0x00020AA3
		public ModelCatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelCatalogItem[])this.results[0];
			}
		}

		// Token: 0x040006DA RID: 1754
		private object[] results;
	}
}
