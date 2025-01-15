using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002C1 RID: 705
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListScheduledReportsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600174A RID: 5962 RVA: 0x00022FA4 File Offset: 0x000211A4
		internal ListScheduledReportsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00022FB7 File Offset: 0x000211B7
		public CatalogItem[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (CatalogItem[])this.results[0];
			}
		}

		// Token: 0x04000701 RID: 1793
		private object[] results;
	}
}
