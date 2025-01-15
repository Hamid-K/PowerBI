using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000064 RID: 100
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListItemTypesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0000D98E File Offset: 0x0000BB8E
		internal ListItemTypesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000D9A1 File Offset: 0x0000BBA1
		public string[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string[])this.results[0];
			}
		}

		// Token: 0x0400022E RID: 558
		private object[] results;
	}
}
