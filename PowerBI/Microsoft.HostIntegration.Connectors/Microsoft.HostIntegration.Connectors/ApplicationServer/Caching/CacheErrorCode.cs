using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200039F RID: 927
	internal enum CacheErrorCode
	{
		// Token: 0x0400135A RID: 4954
		Uninitialized,
		// Token: 0x0400135B RID: 4955
		CacheItemVersionMismatch,
		// Token: 0x0400135C RID: 4956
		InvalidArgument,
		// Token: 0x0400135D RID: 4957
		RegionAlreadyExists,
		// Token: 0x0400135E RID: 4958
		RegionDoesNotExist,
		// Token: 0x0400135F RID: 4959
		ReadThroughRegionDoesNotExist,
		// Token: 0x04001360 RID: 4960
		KeyDoesNotExist,
		// Token: 0x04001361 RID: 4961
		KeyAlreadyExists,
		// Token: 0x04001362 RID: 4962
		MaxNamedCacheCountExceeded,
		// Token: 0x04001363 RID: 4963
		NamedCacheDoesNotExist,
		// Token: 0x04001364 RID: 4964
		ObjectLocked,
		// Token: 0x04001365 RID: 4965
		ObjectNotLocked,
		// Token: 0x04001366 RID: 4966
		InvalidCacheLockHandle,
		// Token: 0x04001367 RID: 4967
		InvalidEnumerator,
		// Token: 0x04001368 RID: 4968
		ConnectionTerminated,
		// Token: 0x04001369 RID: 4969
		ReadThroughProviderFailure,
		// Token: 0x0400136A RID: 4970
		ReadThroughProviderDidNotReturnResult,
		// Token: 0x0400136B RID: 4971
		ReadThroughProviderNotFound,
		// Token: 0x0400136C RID: 4972
		CacheRedirected,
		// Token: 0x0400136D RID: 4973
		RetryLater,
		// Token: 0x0400136E RID: 4974
		ClientServerVersionMismatch,
		// Token: 0x0400136F RID: 4975
		CacheDisabled,
		// Token: 0x04001370 RID: 4976
		ServiceMemoryShortage,
		// Token: 0x04001371 RID: 4977
		UndefinedError,
		// Token: 0x04001372 RID: 4978
		MalformedHttpRequest,
		// Token: 0x04001373 RID: 4979
		MissingServiceVersion,
		// Token: 0x04001374 RID: 4980
		ServiceVersionNotSupported,
		// Token: 0x04001375 RID: 4981
		AccessDenied,
		// Token: 0x04001376 RID: 4982
		MissingStoreName,
		// Token: 0x04001377 RID: 4983
		InvalidStoreName,
		// Token: 0x04001378 RID: 4984
		MissingCacheNamespace,
		// Token: 0x04001379 RID: 4985
		MissingCacheKey,
		// Token: 0x0400137A RID: 4986
		MissingCacheRegion,
		// Token: 0x0400137B RID: 4987
		InvalidCacheNamespace,
		// Token: 0x0400137C RID: 4988
		MissingValue,
		// Token: 0x0400137D RID: 4989
		UnsupportedOperationOnStore,
		// Token: 0x0400137E RID: 4990
		MissingCacheConfigurationProperty,
		// Token: 0x0400137F RID: 4991
		KeyNotFound,
		// Token: 0x04001380 RID: 4992
		InvalidSerializationForCacheItem,
		// Token: 0x04001381 RID: 4993
		InvalidSerializationForCacheItemLockHandle,
		// Token: 0x04001382 RID: 4994
		MissingLockKeyword,
		// Token: 0x04001383 RID: 4995
		Timeout,
		// Token: 0x04001384 RID: 4996
		ConvertSimpleClient,
		// Token: 0x04001385 RID: 4997
		InvalidValue,
		// Token: 0x04001386 RID: 4998
		OverflowException,
		// Token: 0x04001387 RID: 4999
		MessageLargerThanConfiguredSize,
		// Token: 0x04001388 RID: 5000
		UnsupportedOperationAttemptedOnPort,
		// Token: 0x04001389 RID: 5001
		ChannelAuthenticationFailed
	}
}
