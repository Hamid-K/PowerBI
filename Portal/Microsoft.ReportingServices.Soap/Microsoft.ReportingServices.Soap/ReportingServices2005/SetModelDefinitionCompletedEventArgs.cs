using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000253 RID: 595
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetModelDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015D6 RID: 5590 RVA: 0x00022868 File Offset: 0x00020A68
		internal SetModelDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0002287B File Offset: 0x00020A7B
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x040006D9 RID: 1753
		private object[] results;
	}
}
