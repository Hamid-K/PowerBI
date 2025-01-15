using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000026 RID: 38
	public interface ITransposeDataView : IDataView, ISchematized
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C4 RID: 196
		ITransposeSchema TransposeSchema { get; }

		// Token: 0x060000C5 RID: 197
		ISlotCursor GetSlotCursor(int col);
	}
}
