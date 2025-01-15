using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A3C RID: 6716
	public sealed class PartitionedDocumentDisplayNameService : IPartitionedDocumentDisplayNameService, IPartitionDisplayNameService
	{
		// Token: 0x0600A9DD RID: 43485 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public IPartitionDisplayNameService GetServiceForDocument(IPartitionedDocument document)
		{
			return this;
		}

		// Token: 0x0600A9DE RID: 43486 RVA: 0x0013CF66 File Offset: 0x0013B166
		public string GetDisplayNameForPartition(IPartitionKey partitionKey)
		{
			return partitionKey.ToString();
		}
	}
}
