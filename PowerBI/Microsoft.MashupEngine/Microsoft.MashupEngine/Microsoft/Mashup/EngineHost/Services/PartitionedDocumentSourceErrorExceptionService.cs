using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A3D RID: 6717
	public sealed class PartitionedDocumentSourceErrorExceptionService : IPartitionedDocumentSourceErrorExceptionService
	{
		// Token: 0x0600A9E0 RID: 43488 RVA: 0x0023172E File Offset: 0x0022F92E
		public PartitionedDocumentSourceErrorExceptionService(IEngine engine)
		{
			this.engine = engine;
		}

		// Token: 0x0600A9E1 RID: 43489 RVA: 0x0023173D File Offset: 0x0022F93D
		public ISourceErrorExceptionService GetServiceForDocument(IPartitionedDocument document)
		{
			return new SourceErrorExceptionService(this.engine, document);
		}

		// Token: 0x0400584B RID: 22603
		private readonly IEngine engine;
	}
}
