using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002DE RID: 734
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListEventsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060017B7 RID: 6071 RVA: 0x000232E9 File Offset: 0x000214E9
		internal ListEventsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000232FC File Offset: 0x000214FC
		public Event[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Event[])this.results[0];
			}
		}

		// Token: 0x0400070D RID: 1805
		private object[] results;
	}
}
