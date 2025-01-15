using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000167 RID: 359
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class RegenerateModelCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000E33 RID: 3635 RVA: 0x0001788E File Offset: 0x00015A8E
		internal RegenerateModelCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x000178A1 File Offset: 0x00015AA1
		public Warning[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Warning[])this.results[0];
			}
		}

		// Token: 0x04000478 RID: 1144
		private object[] results;
	}
}
