using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019CD RID: 6605
	internal sealed class EmbeddedValueLoggingService : IEmbeddedValueLoggingService
	{
		// Token: 0x0600A73D RID: 42813 RVA: 0x00229B21 File Offset: 0x00227D21
		public EmbeddedValueLoggingService(IEvaluationSession session)
		{
			this.session = session;
		}

		// Token: 0x0600A73E RID: 42814 RVA: 0x0000336E File Offset: 0x0000156E
		public void LogEmbeddedValue(string valueID)
		{
		}

		// Token: 0x0400570C RID: 22284
		private readonly IEvaluationSession session;
	}
}
