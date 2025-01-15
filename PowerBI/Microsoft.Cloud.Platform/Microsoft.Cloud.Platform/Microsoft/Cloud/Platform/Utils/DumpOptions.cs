using System;
using System.Runtime.Serialization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001DA RID: 474
	[Flags]
	[DataContract]
	public enum DumpOptions
	{
		// Token: 0x040004BE RID: 1214
		[EnumMember]
		None = 0,
		// Token: 0x040004BF RID: 1215
		[EnumMember]
		FullMemory = 1,
		// Token: 0x040004C0 RID: 1216
		[EnumMember]
		DetachDebugger = 16,
		// Token: 0x040004C1 RID: 1217
		[EnumMember]
		KillProcess = 32,
		// Token: 0x040004C2 RID: 1218
		[EnumMember]
		WatsonEnabled = 256
	}
}
