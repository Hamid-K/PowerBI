using System;
using System.Collections.Generic;
using System.Threading;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x0200010D RID: 269
	internal class AsyncOperationCounter
	{
		// Token: 0x06000E6B RID: 3691 RVA: 0x00023DBD File Offset: 0x00021FBD
		public void BeginOperation()
		{
			Interlocked.Increment(ref this._pendingOperationCounter);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00023DCC File Offset: 0x00021FCC
		public void CompleteOperation(Exception exception)
		{
			if (this._pendingCompletionList.Count > 0)
			{
				LinkedList<AsyncContinuation> pendingCompletionList = this._pendingCompletionList;
				lock (pendingCompletionList)
				{
					Interlocked.Decrement(ref this._pendingOperationCounter);
					LinkedListNode<AsyncContinuation> linkedListNode = this._pendingCompletionList.First;
					while (linkedListNode != null)
					{
						AsyncContinuation value = linkedListNode.Value;
						linkedListNode = linkedListNode.Next;
						value(exception);
					}
					return;
				}
			}
			Interlocked.Decrement(ref this._pendingOperationCounter);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00023E54 File Offset: 0x00022054
		public AsyncContinuation RegisterCompletionNotification(AsyncContinuation asyncContinuation)
		{
			if (this._pendingOperationCounter == 0)
			{
				return asyncContinuation;
			}
			LinkedList<AsyncContinuation> pendingCompletionList = this._pendingCompletionList;
			bool flag = false;
			AsyncContinuation asyncContinuation2;
			try
			{
				Monitor.Enter(pendingCompletionList, ref flag);
				LinkedListNode<AsyncContinuation> pendingCompletion = new LinkedListNode<AsyncContinuation>(null);
				this._pendingCompletionList.AddLast(pendingCompletion);
				int remainingCompletionCounter = Interlocked.Increment(ref this._pendingOperationCounter);
				if (remainingCompletionCounter <= 0)
				{
					Interlocked.Exchange(ref this._pendingOperationCounter, 0);
					this._pendingCompletionList.Remove(pendingCompletion);
					asyncContinuation2 = asyncContinuation;
				}
				else
				{
					pendingCompletion.Value = delegate(Exception ex)
					{
						if (Interlocked.Decrement(ref remainingCompletionCounter) == 0)
						{
							LinkedList<AsyncContinuation> pendingCompletionList2 = this._pendingCompletionList;
							lock (pendingCompletionList2)
							{
								Interlocked.Decrement(ref this._pendingOperationCounter);
								this._pendingCompletionList.Remove(pendingCompletion);
								LinkedListNode<AsyncContinuation> linkedListNode = this._pendingCompletionList.First;
								while (linkedListNode != null)
								{
									AsyncContinuation value = linkedListNode.Value;
									linkedListNode = linkedListNode.Next;
									value(ex);
								}
							}
							asyncContinuation(ex);
						}
					};
					asyncContinuation2 = pendingCompletion.Value;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(pendingCompletionList);
				}
			}
			return asyncContinuation2;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00023F48 File Offset: 0x00022148
		public void Clear()
		{
			this._pendingCompletionList.Clear();
		}

		// Token: 0x040003E0 RID: 992
		private int _pendingOperationCounter;

		// Token: 0x040003E1 RID: 993
		private readonly LinkedList<AsyncContinuation> _pendingCompletionList = new LinkedList<AsyncContinuation>();
	}
}
