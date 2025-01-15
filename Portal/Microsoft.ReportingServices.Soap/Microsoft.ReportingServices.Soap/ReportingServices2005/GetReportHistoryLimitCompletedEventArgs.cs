using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020002B4 RID: 692
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetReportHistoryLimitCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x00022EB2 File Offset: 0x000210B2
		internal GetReportHistoryLimitCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: base(exception, cancelled, userState)
		{
			this.results = results;
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x00022EC5 File Offset: 0x000210C5
		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x00022EDA File Offset: 0x000210DA
		public bool IsSystem
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (bool)this.results[1];
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00022EEF File Offset: 0x000210EF
		public int SystemLimit
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[2];
			}
		}

		// Token: 0x040006FC RID: 1788
		private object[] results;
	}
}
