using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000359 RID: 857
	public interface IColumnFunctionBuilder
	{
		// Token: 0x060012B6 RID: 4790
		bool ProcessValue();

		// Token: 0x060012B7 RID: 4791
		IColumnFunction CreateColumnFunction();
	}
}
