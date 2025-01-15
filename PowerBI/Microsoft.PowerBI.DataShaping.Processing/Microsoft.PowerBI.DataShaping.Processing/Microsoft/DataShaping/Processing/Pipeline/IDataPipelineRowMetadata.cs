using System;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200009B RID: 155
	internal interface IDataPipelineRowMetadata
	{
		// Token: 0x06000416 RID: 1046
		bool CountsForLimiting(IDataRow row, int resultSetIndex);
	}
}
