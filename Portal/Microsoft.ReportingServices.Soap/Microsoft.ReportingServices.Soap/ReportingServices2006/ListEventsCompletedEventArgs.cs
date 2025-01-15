using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001DB RID: 475
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListEventsCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x0001820A File Offset: 0x0001640A
		internal ListEventsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x0001821D File Offset: 0x0001641D
		public Event[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Event[])this.results[0];
			}
		}

		// Token: 0x040004A6 RID: 1190
		private object[] results;
	}
}
