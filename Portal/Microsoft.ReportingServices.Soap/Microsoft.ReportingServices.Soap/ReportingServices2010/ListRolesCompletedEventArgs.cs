using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000095 RID: 149
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListRolesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000748 RID: 1864 RVA: 0x0000DE02 File Offset: 0x0000C002
		internal ListRolesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x0000DE15 File Offset: 0x0000C015
		public Role[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Role[])this.results[0];
			}
		}

		// Token: 0x04000240 RID: 576
		private object[] results;
	}
}
