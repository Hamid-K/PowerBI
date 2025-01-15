using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001E8 RID: 488
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetPermissionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FEF RID: 4079 RVA: 0x000182FC File Offset: 0x000164FC
		internal GetPermissionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0001830F File Offset: 0x0001650F
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x040004AB RID: 1195
		private object[] results;
	}
}
