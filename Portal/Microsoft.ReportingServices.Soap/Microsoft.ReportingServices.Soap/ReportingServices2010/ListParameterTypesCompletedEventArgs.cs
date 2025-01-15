using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000C5 RID: 197
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListParameterTypesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x0000E139 File Offset: 0x0000C339
		internal ListParameterTypesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0000E14C File Offset: 0x0000C34C
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000253 RID: 595
		private object[] results;
	}
}
