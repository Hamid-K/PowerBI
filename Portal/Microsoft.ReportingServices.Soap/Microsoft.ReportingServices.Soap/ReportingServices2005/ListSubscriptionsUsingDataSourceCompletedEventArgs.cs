using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002DA RID: 730
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSubscriptionsUsingDataSourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060017AB RID: 6059 RVA: 0x00023299 File Offset: 0x00021499
		internal ListSubscriptionsUsingDataSourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000232AC File Offset: 0x000214AC
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x0400070B RID: 1803
		private object[] results;
	}
}
