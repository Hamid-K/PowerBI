using System;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x02000426 RID: 1062
	public interface IMultiCountTableBuilder
	{
		// Token: 0x06001615 RID: 5653
		void IncrementOne(int iCol, uint key, uint labelKey, double value);

		// Token: 0x06001616 RID: 5654
		void IncrementSlot(int iCol, int iSlot, uint key, uint labelKey, double value);

		// Token: 0x06001617 RID: 5655
		IMultiCountTable CreateMultiCountTable();
	}
}
