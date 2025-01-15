using System;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000032 RID: 50
	internal sealed class BatchedJoinBlockTargetSharedResources
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000776C File Offset: 0x0000596C
		internal BatchedJoinBlockTargetSharedResources(int batchSize, GroupingDataflowBlockOptions dataflowBlockOptions, Action batchSizeReachedAction, Action allTargetsDecliningAction, Action<Exception> exceptionAction, Action completionAction)
		{
			BatchedJoinBlockTargetSharedResources <>4__this = this;
			this._incomingLock = new object();
			this._batchSize = batchSize;
			this._remainingAliveTargets = 0;
			this._remainingItemsInBatch = batchSize;
			this._allTargetsDecliningPermanentlyAction = delegate
			{
				allTargetsDecliningAction();
				<>4__this._decliningPermanently = true;
			};
			this._batchSizeReachedAction = delegate
			{
				batchSizeReachedAction();
				<>4__this._batchesCreated += 1L;
				if (<>4__this._batchesCreated >= dataflowBlockOptions.ActualMaxNumberOfGroups)
				{
					<>4__this._allTargetsDecliningPermanentlyAction();
					return;
				}
				<>4__this._remainingItemsInBatch = <>4__this._batchSize;
			};
			this._exceptionAction = exceptionAction;
			this._completionAction = completionAction;
		}

		// Token: 0x04000066 RID: 102
		internal readonly object _incomingLock;

		// Token: 0x04000067 RID: 103
		internal readonly int _batchSize;

		// Token: 0x04000068 RID: 104
		internal readonly Action _batchSizeReachedAction;

		// Token: 0x04000069 RID: 105
		internal readonly Action _allTargetsDecliningPermanentlyAction;

		// Token: 0x0400006A RID: 106
		internal readonly Action<Exception> _exceptionAction;

		// Token: 0x0400006B RID: 107
		internal readonly Action _completionAction;

		// Token: 0x0400006C RID: 108
		internal int _remainingItemsInBatch;

		// Token: 0x0400006D RID: 109
		internal int _remainingAliveTargets;

		// Token: 0x0400006E RID: 110
		internal bool _decliningPermanently;

		// Token: 0x0400006F RID: 111
		internal long _batchesCreated;
	}
}
