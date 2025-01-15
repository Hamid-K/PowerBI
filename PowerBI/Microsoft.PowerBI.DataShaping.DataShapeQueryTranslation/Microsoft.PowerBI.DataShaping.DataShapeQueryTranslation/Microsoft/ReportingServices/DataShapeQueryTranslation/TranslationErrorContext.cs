using System;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	internal sealed class TranslationErrorContext : EngineErrorContextBase<TranslationMessage>
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		public void Register(TranslationMessage message)
		{
			base.Add(message);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		internal void TraceSanitizedMessages(ITracer tracer)
		{
			foreach (TranslationMessage translationMessage in base.Messages)
			{
				tracer.SanitizedTrace(translationMessage);
			}
		}
	}
}
