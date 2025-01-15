using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000049 RID: 73
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetItemDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600063E RID: 1598 RVA: 0x0000D7D6 File Offset: 0x0000B9D6
		internal SetItemDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0000D7E9 File Offset: 0x0000B9E9
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x04000223 RID: 547
		private object[] results;
	}
}
