using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000046 RID: 70
	[Flags]
	[DataContract]
	public enum DiscoverDataSourcesResultCode
	{
		// Token: 0x04000187 RID: 391
		[EnumMember]
		Success = 0,
		// Token: 0x04000188 RID: 392
		[EnumMember]
		FailedWithUnknownOrUnsupportedDataSources = 1,
		// Token: 0x04000189 RID: 393
		[EnumMember]
		FailedWithUnknownNativeQueries = 2,
		// Token: 0x0400018A RID: 394
		[EnumMember]
		FailedWithUnknownFunctions = 4,
		// Token: 0x0400018B RID: 395
		[EnumMember]
		FailedDueToMissingExtension = 8,
		// Token: 0x0400018C RID: 396
		[EnumMember]
		FailedDueToInvalidFormat = 16,
		// Token: 0x0400018D RID: 397
		[EnumMember]
		FailedDueToUnknownError = 32,
		// Token: 0x0400018E RID: 398
		[EnumMember]
		FailedDueToGatewayUnreachableException = 64
	}
}
