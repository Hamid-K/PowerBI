using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200010C RID: 268
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSystemPermissionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0000E682 File Offset: 0x0000C882
		internal GetSystemPermissionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0000E695 File Offset: 0x0000C895
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x0400026E RID: 622
		private object[] results;
	}
}
