using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000251 RID: 593
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetModelDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x00022840 File Offset: 0x00020A40
		internal GetModelDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00022853 File Offset: 0x00020A53
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x040006D8 RID: 1752
		private object[] results;
	}
}
