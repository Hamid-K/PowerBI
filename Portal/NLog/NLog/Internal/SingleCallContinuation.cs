using System;
using System.Threading;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x0200013F RID: 319
	internal class SingleCallContinuation
	{
		// Token: 0x06000F95 RID: 3989 RVA: 0x00027A97 File Offset: 0x00025C97
		public SingleCallContinuation(AsyncContinuation asyncContinuation)
		{
			this._asyncContinuation = asyncContinuation;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00027AA8 File Offset: 0x00025CA8
		public void Function(Exception exception)
		{
			try
			{
				AsyncContinuation asyncContinuation = Interlocked.Exchange<AsyncContinuation>(ref this._asyncContinuation, null);
				if (asyncContinuation != null)
				{
					asyncContinuation(exception);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Exception in asynchronous handler.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x0400042F RID: 1071
		private AsyncContinuation _asyncContinuation;
	}
}
