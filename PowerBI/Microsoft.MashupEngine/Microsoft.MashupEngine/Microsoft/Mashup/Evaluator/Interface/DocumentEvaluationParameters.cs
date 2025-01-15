using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DEA RID: 7658
	public sealed class DocumentEvaluationParameters
	{
		// Token: 0x0600BD9B RID: 48539 RVA: 0x00266D78 File Offset: 0x00264F78
		public DocumentEvaluationParameters Clone()
		{
			return new DocumentEvaluationParameters
			{
				config = this.config.Clone(),
				document = this.document,
				firewallPlan = this.firewallPlan,
				partitionKey = this.partitionKey,
				expression = this.expression,
				executeAction = this.executeAction,
				uiCulture = this.uiCulture,
				reportRelationships = this.reportRelationships
			};
		}

		// Token: 0x040060C5 RID: 24773
		public DocumentEvaluationConfig config;

		// Token: 0x040060C6 RID: 24774
		public IPartitionedDocument document;

		// Token: 0x040060C7 RID: 24775
		public IFirewallPlan firewallPlan;

		// Token: 0x040060C8 RID: 24776
		public IPartitionKey partitionKey;

		// Token: 0x040060C9 RID: 24777
		public string expression;

		// Token: 0x040060CA RID: 24778
		public bool executeAction;

		// Token: 0x040060CB RID: 24779
		public string uiCulture;

		// Token: 0x040060CC RID: 24780
		public bool reportRelationships;
	}
}
