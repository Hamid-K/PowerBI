using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000A1 RID: 161
	public interface IColumnAggregator<T>
	{
		// Token: 0x060002E9 RID: 745
		void ProcessValue(ref T val);

		// Token: 0x060002EA RID: 746
		void Finish();
	}
}
