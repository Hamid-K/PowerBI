using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200003A RID: 58
	internal struct LabelInstruction
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00008913 File Offset: 0x00006B13
		public LabelInstruction(int sourceIndex, string labelName)
		{
			this.SourceIndex = sourceIndex;
			this.LabelName = labelName;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00008923 File Offset: 0x00006B23
		public readonly int SourceIndex { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000892B File Offset: 0x00006B2B
		public readonly string LabelName { get; }
	}
}
