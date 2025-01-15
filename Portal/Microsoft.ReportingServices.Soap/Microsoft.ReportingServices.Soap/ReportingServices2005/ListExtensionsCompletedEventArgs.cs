using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002DC RID: 732
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListExtensionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060017B1 RID: 6065 RVA: 0x000232C1 File Offset: 0x000214C1
		internal ListExtensionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000232D4 File Offset: 0x000214D4
		public Extension[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Extension[])this.results[0];
			}
		}

		// Token: 0x0400070C RID: 1804
		private object[] results;
	}
}
