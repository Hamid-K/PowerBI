using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DF4 RID: 7668
	public interface IPartitionedDocumentSourceErrorExceptionService
	{
		// Token: 0x0600BDA9 RID: 48553
		ISourceErrorExceptionService GetServiceForDocument(IPartitionedDocument document);
	}
}
