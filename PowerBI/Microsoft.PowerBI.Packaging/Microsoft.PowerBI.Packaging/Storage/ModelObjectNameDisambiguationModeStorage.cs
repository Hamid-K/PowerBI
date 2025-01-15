using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000037 RID: 55
	internal enum ModelObjectNameDisambiguationModeStorage
	{
		// Token: 0x040000CF RID: 207
		[EnumMember(Value = "Always")]
		Always,
		// Token: 0x040000D0 RID: 208
		[EnumMember(Value = "ConflictOnly")]
		ConflictOnly
	}
}
