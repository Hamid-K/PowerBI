using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000F3 RID: 243
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListEventsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x0000E489 File Offset: 0x0000C689
		internal ListEventsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0000E49C File Offset: 0x0000C69C
		public Event[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Event[])this.results[0];
			}
		}

		// Token: 0x04000264 RID: 612
		private object[] results;
	}
}
