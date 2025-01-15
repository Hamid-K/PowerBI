using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200026F RID: 623
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSystemPropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x00022A35 File Offset: 0x00020C35
		internal GetSystemPropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x00022A48 File Offset: 0x00020C48
		public Property[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Property[])this.results[0];
			}
		}

		// Token: 0x040006E4 RID: 1764
		private object[] results;
	}
}
