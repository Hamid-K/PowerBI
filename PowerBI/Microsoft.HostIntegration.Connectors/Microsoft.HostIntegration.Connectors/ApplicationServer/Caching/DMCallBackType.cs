using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200022F RID: 559
	internal enum DMCallBackType
	{
		// Token: 0x04000B3A RID: 2874
		PreAdd,
		// Token: 0x04000B3B RID: 2875
		PostAdd,
		// Token: 0x04000B3C RID: 2876
		PreUpsert,
		// Token: 0x04000B3D RID: 2877
		PostUpsert,
		// Token: 0x04000B3E RID: 2878
		PreDelete,
		// Token: 0x04000B3F RID: 2879
		PostDelete,
		// Token: 0x04000B40 RID: 2880
		PreGetAndLock,
		// Token: 0x04000B41 RID: 2881
		PostGetAndLock,
		// Token: 0x04000B42 RID: 2882
		PrePutAndUnlock,
		// Token: 0x04000B43 RID: 2883
		PostPutAndUnlock,
		// Token: 0x04000B44 RID: 2884
		PreUnlock,
		// Token: 0x04000B45 RID: 2885
		PostUnlock,
		// Token: 0x04000B46 RID: 2886
		PreResetTimeOut,
		// Token: 0x04000B47 RID: 2887
		PostResetTimeOut,
		// Token: 0x04000B48 RID: 2888
		PreInternalUpsert,
		// Token: 0x04000B49 RID: 2889
		PostInternalUpsert,
		// Token: 0x04000B4A RID: 2890
		PostForcedUpsert,
		// Token: 0x04000B4B RID: 2891
		PostInternalDelete,
		// Token: 0x04000B4C RID: 2892
		PostForcedDelete,
		// Token: 0x04000B4D RID: 2893
		PostCommitDelete,
		// Token: 0x04000B4E RID: 2894
		PostInternalLockUpdate,
		// Token: 0x04000B4F RID: 2895
		PostForcedUnlock,
		// Token: 0x04000B50 RID: 2896
		PreReadThroughLock,
		// Token: 0x04000B51 RID: 2897
		PostReadThroughLock,
		// Token: 0x04000B52 RID: 2898
		PostReadThroughUnlock,
		// Token: 0x04000B53 RID: 2899
		PreReadThroughPutAndUnlock,
		// Token: 0x04000B54 RID: 2900
		PostReadThroughPutAndUnlock,
		// Token: 0x04000B55 RID: 2901
		PreInternalPutAndUnlock,
		// Token: 0x04000B56 RID: 2902
		PostInternalPutAndUnlock,
		// Token: 0x04000B57 RID: 2903
		PreIncrementDecrement,
		// Token: 0x04000B58 RID: 2904
		PostIncrementDecrement,
		// Token: 0x04000B59 RID: 2905
		PreConcatenate,
		// Token: 0x04000B5A RID: 2906
		PostConcatenate,
		// Token: 0x04000B5B RID: 2907
		MaxType
	}
}
