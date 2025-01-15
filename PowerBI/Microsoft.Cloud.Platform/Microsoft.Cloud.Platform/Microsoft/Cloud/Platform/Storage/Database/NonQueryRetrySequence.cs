using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000043 RID: 67
	internal sealed class NonQueryRetrySequence : RetrySequence
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00006039 File Offset: 0x00004239
		internal NonQueryRetrySequence(IThrottler throttler, DatabaseCommand command, DatabaseMonitoringContext monitoringContext)
			: base(throttler, command, monitoringContext)
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006044 File Offset: 0x00004244
		protected override IAsyncResult BeginQuery(AsyncCallback asyncCallback, object asyncState)
		{
			return base.Command.Command.BeginExecuteNonQuery(asyncCallback, asyncState);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006058 File Offset: 0x00004258
		protected override bool EndQuery(IAsyncResult asyncResult)
		{
			base.Command.Command.EndExecuteNonQuery(asyncResult);
			return false;
		}
	}
}
