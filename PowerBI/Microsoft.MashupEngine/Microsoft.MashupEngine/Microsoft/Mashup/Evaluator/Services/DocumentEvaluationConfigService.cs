using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D81 RID: 7553
	public class DocumentEvaluationConfigService : IDocumentEvaluationConfigService
	{
		// Token: 0x0600BBAD RID: 48045 RVA: 0x0025FA65 File Offset: 0x0025DC65
		public DocumentEvaluationConfigService(DocumentEvaluationConfig config)
		{
			this.config = config;
		}

		// Token: 0x17002E5A RID: 11866
		// (get) Token: 0x0600BBAE RID: 48046 RVA: 0x0025FA74 File Offset: 0x0025DC74
		public DocumentEvaluationConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x04005F78 RID: 24440
		private DocumentEvaluationConfig config;
	}
}
