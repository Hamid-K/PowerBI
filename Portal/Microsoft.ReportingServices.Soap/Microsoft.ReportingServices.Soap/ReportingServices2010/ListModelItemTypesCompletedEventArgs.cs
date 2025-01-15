using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000AD RID: 173
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListModelItemTypesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x0000DFD1 File Offset: 0x0000C1D1
		internal ListModelItemTypesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x0400024A RID: 586
		private object[] results;
	}
}
