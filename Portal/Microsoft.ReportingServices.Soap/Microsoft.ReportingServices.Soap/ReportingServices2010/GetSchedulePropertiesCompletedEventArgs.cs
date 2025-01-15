using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x020000B9 RID: 185
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetSchedulePropertiesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x0000E099 File Offset: 0x0000C299
		internal GetSchedulePropertiesCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		public Schedule Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (Schedule)this.results[0];
			}
		}

		// Token: 0x0400024F RID: 591
		private object[] results;
	}
}
