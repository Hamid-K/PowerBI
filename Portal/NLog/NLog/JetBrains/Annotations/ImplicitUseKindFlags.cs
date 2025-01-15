using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001CD RID: 461
	[Flags]
	internal enum ImplicitUseKindFlags
	{
		// Token: 0x0400056B RID: 1387
		Default = 7,
		// Token: 0x0400056C RID: 1388
		Access = 1,
		// Token: 0x0400056D RID: 1389
		Assign = 2,
		// Token: 0x0400056E RID: 1390
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x0400056F RID: 1391
		InstantiatedNoFixedConstructorSignature = 8
	}
}
