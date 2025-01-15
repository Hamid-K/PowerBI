using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200004E RID: 78
	[DataContract]
	public enum DatabaseMigrationFailureType
	{
		// Token: 0x0400012F RID: 303
		[EnumMember]
		Transient,
		// Token: 0x04000130 RID: 304
		[EnumMember]
		Permanent
	}
}
