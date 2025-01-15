using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000005 RID: 5
	[Flags]
	internal enum DynamicallyAccessedMemberTypes
	{
		// Token: 0x0400000B RID: 11
		None = 0,
		// Token: 0x0400000C RID: 12
		PublicParameterlessConstructor = 1,
		// Token: 0x0400000D RID: 13
		PublicConstructors = 3,
		// Token: 0x0400000E RID: 14
		NonPublicConstructors = 4,
		// Token: 0x0400000F RID: 15
		PublicMethods = 8,
		// Token: 0x04000010 RID: 16
		NonPublicMethods = 16,
		// Token: 0x04000011 RID: 17
		PublicFields = 32,
		// Token: 0x04000012 RID: 18
		NonPublicFields = 64,
		// Token: 0x04000013 RID: 19
		PublicNestedTypes = 128,
		// Token: 0x04000014 RID: 20
		NonPublicNestedTypes = 256,
		// Token: 0x04000015 RID: 21
		PublicProperties = 512,
		// Token: 0x04000016 RID: 22
		NonPublicProperties = 1024,
		// Token: 0x04000017 RID: 23
		PublicEvents = 2048,
		// Token: 0x04000018 RID: 24
		NonPublicEvents = 4096,
		// Token: 0x04000019 RID: 25
		Interfaces = 8192,
		// Token: 0x0400001A RID: 26
		All = -1
	}
}
