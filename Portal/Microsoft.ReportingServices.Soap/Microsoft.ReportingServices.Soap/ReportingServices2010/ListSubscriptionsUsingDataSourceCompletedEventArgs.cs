using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200007B RID: 123
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSubscriptionsUsingDataSourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x0000DC31 File Offset: 0x0000BE31
		internal ListSubscriptionsUsingDataSourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public Subscription[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Subscription[])this.results[0];
			}
		}

		// Token: 0x04000237 RID: 567
		private object[] results;
	}
}
