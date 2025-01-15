using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000085 RID: 133
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDataSourceContentsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x0000DCD3 File Offset: 0x0000BED3
		internal GetDataSourceContentsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000DCE6 File Offset: 0x0000BEE6
		public DataSourceDefinition Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSourceDefinition)this.results[0];
			}
		}

		// Token: 0x0400023A RID: 570
		private object[] results;
	}
}
