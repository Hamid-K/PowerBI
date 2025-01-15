using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001F7 RID: 503
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetAuthenticationModeCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001033 RID: 4147 RVA: 0x0001881F File Offset: 0x00016A1F
		internal GetAuthenticationModeCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00018832 File Offset: 0x00016A32
		public AuthenticationMode Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (AuthenticationMode)this.results[0];
			}
		}

		// Token: 0x040004BC RID: 1212
		private object[] results;
	}
}
