using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002BB RID: 699
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CreateScheduleCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001736 RID: 5942 RVA: 0x00022F54 File Offset: 0x00021154
		internal CreateScheduleCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00022F67 File Offset: 0x00021167
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[0];
			}
		}

		// Token: 0x040006FF RID: 1791
		private object[] results;
	}
}
