using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001E0 RID: 480
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListRolesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x0001825A File Offset: 0x0001645A
		internal ListRolesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0001826D File Offset: 0x0001646D
		public Role[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Role[])this.results[0];
			}
		}

		// Token: 0x040004A8 RID: 1192
		private object[] results;
	}
}
