using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E23 RID: 7715
	public interface IPartitionedDocumentDisplayNameService
	{
		// Token: 0x0600BE20 RID: 48672
		IPartitionDisplayNameService GetServiceForDocument(IPartitionedDocument document);
	}
}
