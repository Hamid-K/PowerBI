using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x02001942 RID: 6466
	public interface IDocumentAnalysisInfo : IDisposable
	{
		// Token: 0x0600A426 RID: 42022
		IPartitionAnalysisInfo GetPartitionInfo(IPartitionKey partitionKey);
	}
}
