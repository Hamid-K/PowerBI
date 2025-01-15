using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200004B RID: 75
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetItemDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000644 RID: 1604 RVA: 0x0000D7FE File Offset: 0x0000B9FE
		internal GetItemDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000D811 File Offset: 0x0000BA11
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte[])this.results[0];
			}
		}

		// Token: 0x04000224 RID: 548
		private object[] results;
	}
}
