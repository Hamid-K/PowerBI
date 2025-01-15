using System;
using System.IO;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000D9 RID: 217
	public interface IDataSaver
	{
		// Token: 0x06000493 RID: 1171
		bool IsColumnSavable(ColumnType type);

		// Token: 0x06000494 RID: 1172
		void SaveData(Stream stream, IDataView data, params int[] cols);
	}
}
