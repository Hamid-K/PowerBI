using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200006F RID: 111
	[DataContract]
	public enum DatabaseType
	{
		// Token: 0x040001BC RID: 444
		[EnumMember(Value = "Ephemeral")]
		Ephemeral,
		// Token: 0x040001BD RID: 445
		[EnumMember(Value = "Persisted")]
		Persisted,
		// Token: 0x040001BE RID: 446
		[EnumMember(Value = "BIPro")]
		BIPro,
		// Token: 0x040001BF RID: 447
		[EnumMember(Value = "ScaleOut")]
		ScaleOut,
		// Token: 0x040001C0 RID: 448
		[EnumMember(Value = "PowerBIEnterprise")]
		PowerBIEnterprise
	}
}
