using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001BF RID: 447
	public static class ChainedAsyncOperationInvoker
	{
		// Token: 0x06000B85 RID: 2949 RVA: 0x00027ECC File Offset: 0x000260CC
		public static ChainedAsyncResult<WorkTicket, T> Begin<T>(T item, WorkTicket ticket, Func<AsyncCallback, object, IAsyncResult> action, AsyncCallback asyncCallback, object asyncState)
		{
			ChainedAsyncResult<WorkTicket, T> chainedAsyncResult = new ChainedAsyncResult<WorkTicket, T>(asyncCallback, asyncState, ticket, item);
			using (DisposeController disposeController = new DisposeController(chainedAsyncResult.WorkTicket))
			{
				chainedAsyncResult.InnerResult = action(new AsyncCallback(chainedAsyncResult.BeginAsyncFunctionCallback), null);
				disposeController.PreventDispose();
			}
			return chainedAsyncResult;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00027F2C File Offset: 0x0002612C
		public static void End<T>(IAsyncResult asyncResult, Action<T, IAsyncResult> action)
		{
			ChainedAsyncResult<WorkTicket, T> chainedAsyncResult = (ChainedAsyncResult<WorkTicket, T>)asyncResult;
			using (chainedAsyncResult.WorkTicket)
			{
				chainedAsyncResult.End();
				action(chainedAsyncResult.Data, chainedAsyncResult.InnerResult);
			}
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00027F7C File Offset: 0x0002617C
		public static TResult End<T, TResult>(IAsyncResult asyncResult, Func<T, IAsyncResult, TResult> action)
		{
			ChainedAsyncResult<WorkTicket, T> chainedAsyncResult = (ChainedAsyncResult<WorkTicket, T>)asyncResult;
			TResult tresult;
			using (chainedAsyncResult.WorkTicket)
			{
				chainedAsyncResult.End();
				tresult = action(chainedAsyncResult.Data, chainedAsyncResult.InnerResult);
			}
			return tresult;
		}
	}
}
