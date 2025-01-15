using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000C7 RID: 199
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListParameterStatesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x0000E161 File Offset: 0x0000C361
		internal ListParameterStatesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0000E174 File Offset: 0x0000C374
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000254 RID: 596
		private object[] results;
	}
}
