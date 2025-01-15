using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200019D RID: 413
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDataSourceContentsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000EE3 RID: 3811 RVA: 0x00017C52 File Offset: 0x00015E52
		internal GetDataSourceContentsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00017C65 File Offset: 0x00015E65
		public DataSourceDefinition Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSourceDefinition)this.results[0];
			}
		}

		// Token: 0x0400048E RID: 1166
		private object[] results;
	}
}
