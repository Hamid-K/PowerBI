using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000EC RID: 236
	[DataContract]
	[Flags]
	public enum SynonymLookupWarning
	{
		// Token: 0x04000513 RID: 1299
		[EnumMember]
		None = 0,
		// Token: 0x04000514 RID: 1300
		[EnumMember]
		UnsupportedTokenLength = 1,
		// Token: 0x04000515 RID: 1301
		[EnumMember]
		MalformedSearchDefinition = 2,
		// Token: 0x04000516 RID: 1302
		[EnumMember]
		UnsupportedTokenCount = 4,
		// Token: 0x04000517 RID: 1303
		[EnumMember]
		UnsupportedLanguage = 8,
		// Token: 0x04000518 RID: 1304
		[EnumMember]
		UnsupportedRequestCount = 16,
		// Token: 0x04000519 RID: 1305
		[EnumMember]
		NoInputTokens = 32,
		// Token: 0x0400051A RID: 1306
		[EnumMember]
		NoInputSearchDefinitions = 64
	}
}
