using System;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000019 RID: 25
	internal static class ErrorSourceCategory
	{
		// Token: 0x04000046 RID: 70
		internal static readonly ErrorSource FallbackCondition = ErrorSource.PowerBI;

		// Token: 0x04000047 RID: 71
		internal static readonly ErrorSource InputDoesNotMatchModel = ErrorSource.User;

		// Token: 0x04000048 RID: 72
		internal static readonly ErrorSource MalformedExternalInput = ErrorSource.PowerBI;

		// Token: 0x04000049 RID: 73
		internal static readonly ErrorSource MalformedInternalInput = ErrorSource.PowerBI;

		// Token: 0x0400004A RID: 74
		internal static readonly ErrorSource UnexpectedError = ErrorSource.PowerBI;

		// Token: 0x0400004B RID: 75
		internal static readonly ErrorSource UnsupportedFeature = ErrorSource.PowerBI;

		// Token: 0x0400004C RID: 76
		internal static readonly ErrorSource UserInput = ErrorSource.User;
	}
}
