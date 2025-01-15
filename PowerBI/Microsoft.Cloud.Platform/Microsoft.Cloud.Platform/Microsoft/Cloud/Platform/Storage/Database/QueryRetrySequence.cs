using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000044 RID: 68
	internal sealed class QueryRetrySequence : RetrySequence
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000606D File Offset: 0x0000426D
		public QueryRetrySequence(IThrottler throttler, DatabaseCommand command, CommandBehavior behavior, DatabaseMonitoringContext monitoringContext)
			: base(throttler, command, monitoringContext)
		{
			this.m_behavior = behavior;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006080 File Offset: 0x00004280
		protected override IAsyncResult BeginQuery(AsyncCallback asyncCallback, object asyncState)
		{
			return base.Command.Command.BeginExecuteReader(asyncCallback, asyncState, this.m_behavior);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000609A File Offset: 0x0000429A
		protected override bool EndQuery(IAsyncResult asyncResult)
		{
			this.Reader = base.Command.Command.EndExecuteReader(asyncResult);
			return true;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000060B4 File Offset: 0x000042B4
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000060BC File Offset: 0x000042BC
		public SqlDataReader Reader { get; private set; }

		// Token: 0x040000BD RID: 189
		private readonly CommandBehavior m_behavior;
	}
}
