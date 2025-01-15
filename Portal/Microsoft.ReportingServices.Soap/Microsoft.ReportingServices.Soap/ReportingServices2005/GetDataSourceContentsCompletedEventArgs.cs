using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002A0 RID: 672
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDataSourceContentsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060016D7 RID: 5847 RVA: 0x00022D31 File Offset: 0x00020F31
		internal GetDataSourceContentsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00022D44 File Offset: 0x00020F44
		public DataSourceDefinition Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSourceDefinition)this.results[0];
			}
		}

		// Token: 0x040006F5 RID: 1781
		private object[] results;
	}
}
