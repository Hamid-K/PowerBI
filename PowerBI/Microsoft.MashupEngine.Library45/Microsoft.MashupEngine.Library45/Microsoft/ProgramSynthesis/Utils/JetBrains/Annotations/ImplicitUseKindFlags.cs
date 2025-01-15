using System;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000556 RID: 1366
	[Flags]
	public enum ImplicitUseKindFlags
	{
		// Token: 0x04000ECF RID: 3791
		Default = 7,
		// Token: 0x04000ED0 RID: 3792
		Access = 1,
		// Token: 0x04000ED1 RID: 3793
		Assign = 2,
		// Token: 0x04000ED2 RID: 3794
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x04000ED3 RID: 3795
		InstantiatedNoFixedConstructorSignature = 8
	}
}
