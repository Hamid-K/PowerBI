using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200006E RID: 110
	[DataContract]
	public enum DatabaseStorageMode : byte
	{
		// Token: 0x040001B8 RID: 440
		[EnumMember]
		Unknown,
		// Token: 0x040001B9 RID: 441
		[EnumMember]
		Abf,
		// Token: 0x040001BA RID: 442
		[EnumMember]
		PremiumFiles
	}
}
