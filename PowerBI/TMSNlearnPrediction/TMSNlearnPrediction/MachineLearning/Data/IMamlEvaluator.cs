using System;
using System.Collections.Generic;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000022 RID: 34
	public interface IMamlEvaluator : IEvaluator
	{
		// Token: 0x060000AB RID: 171
		void PrintFoldResults(IChannel ch, Dictionary<string, IDataView> metrics);

		// Token: 0x060000AC RID: 172
		void PrintOverallResults(IChannel ch, string filename, params Dictionary<string, IDataView>[] metrics);

		// Token: 0x060000AD RID: 173
		IDataView GetPerInstanceDataViewToSave(RoleMappedData perInstance);
	}
}
