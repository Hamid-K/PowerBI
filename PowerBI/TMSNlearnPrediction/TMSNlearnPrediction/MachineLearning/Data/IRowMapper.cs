using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200001B RID: 27
	public interface IRowMapper : ICanSaveModel
	{
		// Token: 0x06000081 RID: 129
		Func<int, bool> GetDependencies(Func<int, bool> activeOutput);

		// Token: 0x06000082 RID: 130
		Delegate[] CreateGetters(IRowCursor input, Func<int, bool> activeOutput);

		// Token: 0x06000083 RID: 131
		RowMapperColumnInfo[] GetOutputColumns();
	}
}
