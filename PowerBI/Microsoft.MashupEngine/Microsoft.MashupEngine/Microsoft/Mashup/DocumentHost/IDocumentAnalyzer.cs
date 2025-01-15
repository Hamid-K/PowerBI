using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x02001944 RID: 6468
	public interface IDocumentAnalyzer
	{
		// Token: 0x0600A431 RID: 42033
		IEvaluation BeginAnalyzeDocumentPartitions(DocumentEvaluationConfig config, IPartitionedDocument document, IEnumerable<IPartitionKey> partitionKeys, IDocumentAnalysisInfo documentInfo, Action<Exception> callback);
	}
}
