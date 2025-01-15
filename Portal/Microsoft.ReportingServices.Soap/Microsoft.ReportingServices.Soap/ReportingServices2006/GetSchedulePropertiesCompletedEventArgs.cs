using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001BA RID: 442
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSchedulePropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x00017E75 File Offset: 0x00016075
		internal GetSchedulePropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00017E88 File Offset: 0x00016088
		public Schedule Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Schedule)this.results[0];
			}
		}

		// Token: 0x04000498 RID: 1176
		private object[] results;
	}
}
