using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D5 RID: 213
	internal enum InternalError
	{
		// Token: 0x040002FA RID: 762
		UnexpectedReadState = 4,
		// Token: 0x040002FB RID: 763
		UnvalidatedEntityState = 6,
		// Token: 0x040002FC RID: 764
		NullResponseStream,
		// Token: 0x040002FD RID: 765
		EntityNotDeleted,
		// Token: 0x040002FE RID: 766
		EntityNotAddedState,
		// Token: 0x040002FF RID: 767
		LinkNotAddedState,
		// Token: 0x04000300 RID: 768
		EntryNotModified,
		// Token: 0x04000301 RID: 769
		LinkBadState,
		// Token: 0x04000302 RID: 770
		UnexpectedBeginChangeSet,
		// Token: 0x04000303 RID: 771
		UnexpectedBatchState,
		// Token: 0x04000304 RID: 772
		ChangeResponseMissingContentID,
		// Token: 0x04000305 RID: 773
		ChangeResponseUnknownContentID,
		// Token: 0x04000306 RID: 774
		InvalidHandleOperationResponse = 18,
		// Token: 0x04000307 RID: 775
		InvalidEndGetRequestStream = 20,
		// Token: 0x04000308 RID: 776
		InvalidEndGetRequestCompleted,
		// Token: 0x04000309 RID: 777
		InvalidEndGetRequestStreamRequest,
		// Token: 0x0400030A RID: 778
		InvalidEndGetRequestStreamStream,
		// Token: 0x0400030B RID: 779
		InvalidEndGetRequestStreamContent,
		// Token: 0x0400030C RID: 780
		InvalidEndGetRequestStreamContentLength,
		// Token: 0x0400030D RID: 781
		InvalidEndWrite = 30,
		// Token: 0x0400030E RID: 782
		InvalidEndWriteCompleted,
		// Token: 0x0400030F RID: 783
		InvalidEndWriteRequest,
		// Token: 0x04000310 RID: 784
		InvalidEndWriteStream,
		// Token: 0x04000311 RID: 785
		InvalidEndGetResponse = 40,
		// Token: 0x04000312 RID: 786
		InvalidEndGetResponseCompleted,
		// Token: 0x04000313 RID: 787
		InvalidEndGetResponseRequest,
		// Token: 0x04000314 RID: 788
		InvalidEndGetResponseResponse,
		// Token: 0x04000315 RID: 789
		InvalidAsyncResponseStreamCopy,
		// Token: 0x04000316 RID: 790
		InvalidAsyncResponseStreamCopyBuffer,
		// Token: 0x04000317 RID: 791
		InvalidEndRead = 50,
		// Token: 0x04000318 RID: 792
		InvalidEndReadCompleted,
		// Token: 0x04000319 RID: 793
		InvalidEndReadStream,
		// Token: 0x0400031A RID: 794
		InvalidEndReadCopy,
		// Token: 0x0400031B RID: 795
		InvalidEndReadBuffer,
		// Token: 0x0400031C RID: 796
		InvalidSaveNextChange = 60,
		// Token: 0x0400031D RID: 797
		InvalidBeginNextChange,
		// Token: 0x0400031E RID: 798
		SaveNextChangeIncomplete,
		// Token: 0x0400031F RID: 799
		MaterializerReturningMoreThanOneEntity,
		// Token: 0x04000320 RID: 800
		InvalidGetResponse = 71,
		// Token: 0x04000321 RID: 801
		InvalidHandleCompleted,
		// Token: 0x04000322 RID: 802
		InvalidMethodCallWhenNotReadingJsonLight
	}
}
