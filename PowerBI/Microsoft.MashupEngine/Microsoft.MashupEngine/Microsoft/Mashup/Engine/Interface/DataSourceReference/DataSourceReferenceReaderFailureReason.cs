using System;

namespace Microsoft.Mashup.Engine.Interface.DataSourceReference
{
	// Token: 0x02000143 RID: 323
	public enum DataSourceReferenceReaderFailureReason
	{
		// Token: 0x0400038F RID: 911
		None,
		// Token: 0x04000390 RID: 912
		Cancelled,
		// Token: 0x04000391 RID: 913
		CreateMashupFailure,
		// Token: 0x04000392 RID: 914
		InvalidAuthenticationType,
		// Token: 0x04000393 RID: 915
		InvalidBinaryContent,
		// Token: 0x04000394 RID: 916
		InvalidDataSourceLocationUrl,
		// Token: 0x04000395 RID: 917
		InvalidDataSourceReference,
		// Token: 0x04000396 RID: 918
		UnrecognizedContentType,
		// Token: 0x04000397 RID: 919
		UnrecognizedDataSourceType,
		// Token: 0x04000398 RID: 920
		IncompatiblePackage,
		// Token: 0x04000399 RID: 921
		RequestFailure,
		// Token: 0x0400039A RID: 922
		InvalidOption
	}
}
