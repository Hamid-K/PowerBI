using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200016B RID: 363
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSecureMethodsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E3F RID: 3647 RVA: 0x000178DE File Offset: 0x00015ADE
		internal ListSecureMethodsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x000178F1 File Offset: 0x00015AF1
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x0400047A RID: 1146
		private object[] results;
	}
}
