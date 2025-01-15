using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Concat
{
	// Token: 0x02001765 RID: 5989
	public abstract class DynamicOutputToken : OutputToken
	{
		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x0600C6B0 RID: 50864 RVA: 0x002ABC88 File Offset: 0x002A9E88
		// (set) Token: 0x0600C6B1 RID: 50865 RVA: 0x002ABC90 File Offset: 0x002A9E90
		public bool Partial { get; set; }
	}
}
