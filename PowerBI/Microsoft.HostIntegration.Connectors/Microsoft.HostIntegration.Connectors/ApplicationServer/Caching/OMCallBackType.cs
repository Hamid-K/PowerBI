using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002F5 RID: 757
	internal enum OMCallBackType
	{
		// Token: 0x04000F21 RID: 3873
		MinType = -1,
		// Token: 0x04000F22 RID: 3874
		PreAdd,
		// Token: 0x04000F23 RID: 3875
		PostAdd,
		// Token: 0x04000F24 RID: 3876
		PrePut,
		// Token: 0x04000F25 RID: 3877
		PostPut,
		// Token: 0x04000F26 RID: 3878
		PreDelete,
		// Token: 0x04000F27 RID: 3879
		PostDelete,
		// Token: 0x04000F28 RID: 3880
		PreNamedCacheCreate,
		// Token: 0x04000F29 RID: 3881
		PostNamedCacheCreate,
		// Token: 0x04000F2A RID: 3882
		PreNamedCacheDelete,
		// Token: 0x04000F2B RID: 3883
		PostNamedCacheDelete,
		// Token: 0x04000F2C RID: 3884
		PreNamedCacheClear,
		// Token: 0x04000F2D RID: 3885
		PostNamedCacheClear,
		// Token: 0x04000F2E RID: 3886
		PreRegionCreate,
		// Token: 0x04000F2F RID: 3887
		PostRegionCreate,
		// Token: 0x04000F30 RID: 3888
		PreRegionDelete,
		// Token: 0x04000F31 RID: 3889
		PostRegionDelete,
		// Token: 0x04000F32 RID: 3890
		PreRegionClear,
		// Token: 0x04000F33 RID: 3891
		PostRegionClear,
		// Token: 0x04000F34 RID: 3892
		PreGetAndLock,
		// Token: 0x04000F35 RID: 3893
		PostGetAndLock,
		// Token: 0x04000F36 RID: 3894
		PrePutAndUnlock,
		// Token: 0x04000F37 RID: 3895
		PostPutAndUnlock,
		// Token: 0x04000F38 RID: 3896
		PreResetTimeOut,
		// Token: 0x04000F39 RID: 3897
		PostResetTimeOut,
		// Token: 0x04000F3A RID: 3898
		PreUnlock,
		// Token: 0x04000F3B RID: 3899
		PostUnlock,
		// Token: 0x04000F3C RID: 3900
		PostForcedUpsert,
		// Token: 0x04000F3D RID: 3901
		PostInternalDelete,
		// Token: 0x04000F3E RID: 3902
		PostForcedDelete,
		// Token: 0x04000F3F RID: 3903
		PreInternalUpsert,
		// Token: 0x04000F40 RID: 3904
		PostInternalUpsert,
		// Token: 0x04000F41 RID: 3905
		PostInternalLockUpdate,
		// Token: 0x04000F42 RID: 3906
		PostForcedUnlock,
		// Token: 0x04000F43 RID: 3907
		PostReadThroughLock,
		// Token: 0x04000F44 RID: 3908
		PostReadThroughUnlock,
		// Token: 0x04000F45 RID: 3909
		PostReadThroughPutAndUnlock,
		// Token: 0x04000F46 RID: 3910
		PreInternalPutAndUnlock,
		// Token: 0x04000F47 RID: 3911
		PostInternalPutAndUnlock,
		// Token: 0x04000F48 RID: 3912
		PreIncrementDecrement,
		// Token: 0x04000F49 RID: 3913
		PostIncrementDecrement,
		// Token: 0x04000F4A RID: 3914
		PreConcatenate,
		// Token: 0x04000F4B RID: 3915
		PostConcatenate,
		// Token: 0x04000F4C RID: 3916
		MaxType
	}
}
