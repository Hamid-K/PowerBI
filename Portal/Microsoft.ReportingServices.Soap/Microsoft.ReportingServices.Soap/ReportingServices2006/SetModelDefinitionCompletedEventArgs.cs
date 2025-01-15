using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001EE RID: 494
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetModelDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x00018389 File Offset: 0x00016589
		internal SetModelDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0001839C File Offset: 0x0001659C
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x040004AE RID: 1198
		private object[] results;
	}
}
