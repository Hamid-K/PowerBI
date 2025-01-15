using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000036 RID: 54
	internal enum TargetedModelObjectsStorage
	{
		// Token: 0x040000CA RID: 202
		[EnumMember(Value = "None")]
		None,
		// Token: 0x040000CB RID: 203
		[EnumMember(Value = "Tables")]
		Tables,
		// Token: 0x040000CC RID: 204
		[EnumMember(Value = "Measures")]
		Measures,
		// Token: 0x040000CD RID: 205
		[EnumMember(Value = "All")]
		All
	}
}
