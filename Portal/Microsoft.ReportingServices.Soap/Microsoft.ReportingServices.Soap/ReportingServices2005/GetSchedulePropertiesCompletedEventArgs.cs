using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002BF RID: 703
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSchedulePropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06001744 RID: 5956 RVA: 0x00022F7C File Offset: 0x0002117C
		internal GetSchedulePropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00022F8F File Offset: 0x0002118F
		public Schedule Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Schedule)this.results[0];
			}
		}

		// Token: 0x04000700 RID: 1792
		private object[] results;
	}
}
