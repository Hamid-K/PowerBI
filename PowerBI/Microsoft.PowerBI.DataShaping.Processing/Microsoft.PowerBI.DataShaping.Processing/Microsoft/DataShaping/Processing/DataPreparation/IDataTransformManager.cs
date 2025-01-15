using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.DataPreparation
{
	// Token: 0x02000085 RID: 133
	internal interface IDataTransformManager
	{
		// Token: 0x0600035F RID: 863
		int GetInputResultSetIndex(ResultTableLookupInfo tableLookupInfo);

		// Token: 0x06000360 RID: 864
		Task<IResultSet> GetResultSetAsync(IResultSet input, ResultTableLookupInfo tableLookupInfo);

		// Token: 0x06000361 RID: 865
		IReadOnlyList<Message> GetMessages();
	}
}
