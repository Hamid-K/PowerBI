using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000265 RID: 613
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GenerateModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001611 RID: 5649 RVA: 0x00022995 File Offset: 0x00020B95
		internal GenerateModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x000229A8 File Offset: 0x00020BA8
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x040006E0 RID: 1760
		private object[] results;
	}
}
