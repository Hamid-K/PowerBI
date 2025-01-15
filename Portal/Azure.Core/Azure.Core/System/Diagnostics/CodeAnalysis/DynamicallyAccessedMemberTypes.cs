using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000017 RID: 23
	[Flags]
	internal enum DynamicallyAccessedMemberTypes
	{
		// Token: 0x0400001A RID: 26
		None = 0,
		// Token: 0x0400001B RID: 27
		PublicParameterlessConstructor = 1,
		// Token: 0x0400001C RID: 28
		PublicConstructors = 3,
		// Token: 0x0400001D RID: 29
		NonPublicConstructors = 4,
		// Token: 0x0400001E RID: 30
		PublicMethods = 8,
		// Token: 0x0400001F RID: 31
		NonPublicMethods = 16,
		// Token: 0x04000020 RID: 32
		PublicFields = 32,
		// Token: 0x04000021 RID: 33
		NonPublicFields = 64,
		// Token: 0x04000022 RID: 34
		PublicNestedTypes = 128,
		// Token: 0x04000023 RID: 35
		NonPublicNestedTypes = 256,
		// Token: 0x04000024 RID: 36
		PublicProperties = 512,
		// Token: 0x04000025 RID: 37
		NonPublicProperties = 1024,
		// Token: 0x04000026 RID: 38
		PublicEvents = 2048,
		// Token: 0x04000027 RID: 39
		NonPublicEvents = 4096,
		// Token: 0x04000028 RID: 40
		Interfaces = 8192,
		// Token: 0x04000029 RID: 41
		All = -1
	}
}
