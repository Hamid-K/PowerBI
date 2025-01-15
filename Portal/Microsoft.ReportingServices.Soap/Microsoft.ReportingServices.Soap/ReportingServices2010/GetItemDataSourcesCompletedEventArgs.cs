using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200008A RID: 138
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemDataSourcesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x0000DD23 File Offset: 0x0000BF23
		internal GetItemDataSourcesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0000DD36 File Offset: 0x0000BF36
		public DataSource[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSource[])this.results[0];
			}
		}

		// Token: 0x0400023C RID: 572
		private object[] results;
	}
}
