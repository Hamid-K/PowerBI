using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001EC RID: 492
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetModelDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x00018361 File Offset: 0x00016561
		internal GetModelDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00018374 File Offset: 0x00016574
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x040004AD RID: 1197
		private object[] results;
	}
}
