using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000060 RID: 96
	[Flags]
	internal enum DynamicallyAccessedMemberTypes
	{
		// Token: 0x04000131 RID: 305
		None = 0,
		// Token: 0x04000132 RID: 306
		PublicParameterlessConstructor = 1,
		// Token: 0x04000133 RID: 307
		PublicConstructors = 3,
		// Token: 0x04000134 RID: 308
		NonPublicConstructors = 4,
		// Token: 0x04000135 RID: 309
		PublicMethods = 8,
		// Token: 0x04000136 RID: 310
		NonPublicMethods = 16,
		// Token: 0x04000137 RID: 311
		PublicFields = 32,
		// Token: 0x04000138 RID: 312
		NonPublicFields = 64,
		// Token: 0x04000139 RID: 313
		PublicNestedTypes = 128,
		// Token: 0x0400013A RID: 314
		NonPublicNestedTypes = 256,
		// Token: 0x0400013B RID: 315
		PublicProperties = 512,
		// Token: 0x0400013C RID: 316
		NonPublicProperties = 1024,
		// Token: 0x0400013D RID: 317
		PublicEvents = 2048,
		// Token: 0x0400013E RID: 318
		NonPublicEvents = 4096,
		// Token: 0x0400013F RID: 319
		Interfaces = 8192,
		// Token: 0x04000140 RID: 320
		All = -1
	}
}
