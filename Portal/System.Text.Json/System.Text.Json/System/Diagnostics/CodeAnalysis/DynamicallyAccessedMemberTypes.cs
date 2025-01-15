using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000012 RID: 18
	[Flags]
	internal enum DynamicallyAccessedMemberTypes
	{
		// Token: 0x0400007F RID: 127
		None = 0,
		// Token: 0x04000080 RID: 128
		PublicParameterlessConstructor = 1,
		// Token: 0x04000081 RID: 129
		PublicConstructors = 3,
		// Token: 0x04000082 RID: 130
		NonPublicConstructors = 4,
		// Token: 0x04000083 RID: 131
		PublicMethods = 8,
		// Token: 0x04000084 RID: 132
		NonPublicMethods = 16,
		// Token: 0x04000085 RID: 133
		PublicFields = 32,
		// Token: 0x04000086 RID: 134
		NonPublicFields = 64,
		// Token: 0x04000087 RID: 135
		PublicNestedTypes = 128,
		// Token: 0x04000088 RID: 136
		NonPublicNestedTypes = 256,
		// Token: 0x04000089 RID: 137
		PublicProperties = 512,
		// Token: 0x0400008A RID: 138
		NonPublicProperties = 1024,
		// Token: 0x0400008B RID: 139
		PublicEvents = 2048,
		// Token: 0x0400008C RID: 140
		NonPublicEvents = 4096,
		// Token: 0x0400008D RID: 141
		Interfaces = 8192,
		// Token: 0x0400008E RID: 142
		All = -1
	}
}
