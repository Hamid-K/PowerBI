using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001E2 RID: 482
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetRolePropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x00018282 File Offset: 0x00016482
		internal GetRolePropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00018295 File Offset: 0x00016495
		public Task[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Task[])this.results[0];
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x000182AA File Offset: 0x000164AA
		public string Description
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		// Token: 0x040004A9 RID: 1193
		private object[] results;
	}
}
