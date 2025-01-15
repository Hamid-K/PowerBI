using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200031E RID: 798
	internal class AsyncResult<TResult> : AsyncResultNoResult
	{
		// Token: 0x06001CF3 RID: 7411 RVA: 0x00057F94 File Offset: 0x00056194
		public AsyncResult(AsyncCallback asyncCallback, object state)
			: base(asyncCallback, state)
		{
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00057FAA File Offset: 0x000561AA
		public void SetAsCompleted(TResult result, bool completedSynchronously)
		{
			this._result = result;
			base.SetAsCompleted(null, completedSynchronously);
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00057FBB File Offset: 0x000561BB
		public new TResult EndInvoke()
		{
			base.EndInvoke();
			return this._result;
		}

		// Token: 0x04001019 RID: 4121
		private TResult _result = default(TResult);
	}
}
