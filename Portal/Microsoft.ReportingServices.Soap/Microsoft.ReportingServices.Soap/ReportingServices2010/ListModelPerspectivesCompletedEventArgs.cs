using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000AF RID: 175
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelPerspectivesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0000DFF9 File Offset: 0x0000C1F9
		internal ListModelPerspectivesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0000E00C File Offset: 0x0000C20C
		public ModelCatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (ModelCatalogItem[])this.results[0];
			}
		}

		// Token: 0x0400024B RID: 587
		private object[] results;
	}
}
