using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000563 RID: 1379
	[Flags]
	public enum ImplicitUseKindFlags
	{
		// Token: 0x04000ED9 RID: 3801
		Default = 7,
		// Token: 0x04000EDA RID: 3802
		Access = 1,
		// Token: 0x04000EDB RID: 3803
		Assign = 2,
		// Token: 0x04000EDC RID: 3804
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x04000EDD RID: 3805
		InstantiatedNoFixedConstructorSignature = 8
	}
}
