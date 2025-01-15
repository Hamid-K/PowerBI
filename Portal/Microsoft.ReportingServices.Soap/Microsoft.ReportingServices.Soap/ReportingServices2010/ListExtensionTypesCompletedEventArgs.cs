using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000F1 RID: 241
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListExtensionTypesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x0000E461 File Offset: 0x0000C661
		internal ListExtensionTypesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0000E474 File Offset: 0x0000C674
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x04000263 RID: 611
		private object[] results;
	}
}
