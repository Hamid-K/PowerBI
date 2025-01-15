using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000D RID: 13
	[Flags]
	internal enum DynamicallyAccessedMemberTypes
	{
		// Token: 0x04000016 RID: 22
		None = 0,
		// Token: 0x04000017 RID: 23
		PublicParameterlessConstructor = 1,
		// Token: 0x04000018 RID: 24
		PublicConstructors = 3,
		// Token: 0x04000019 RID: 25
		NonPublicConstructors = 4,
		// Token: 0x0400001A RID: 26
		PublicMethods = 8,
		// Token: 0x0400001B RID: 27
		NonPublicMethods = 16,
		// Token: 0x0400001C RID: 28
		PublicFields = 32,
		// Token: 0x0400001D RID: 29
		NonPublicFields = 64,
		// Token: 0x0400001E RID: 30
		PublicNestedTypes = 128,
		// Token: 0x0400001F RID: 31
		NonPublicNestedTypes = 256,
		// Token: 0x04000020 RID: 32
		PublicProperties = 512,
		// Token: 0x04000021 RID: 33
		NonPublicProperties = 1024,
		// Token: 0x04000022 RID: 34
		PublicEvents = 2048,
		// Token: 0x04000023 RID: 35
		NonPublicEvents = 4096,
		// Token: 0x04000024 RID: 36
		Interfaces = 8192,
		// Token: 0x04000025 RID: 37
		All = -1
	}
}
