using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000257 RID: 599
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetUserModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x000228B8 File Offset: 0x00020AB8
		internal GetUserModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x000228CB File Offset: 0x00020ACB
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x040006DB RID: 1755
		private object[] results;
	}
}
