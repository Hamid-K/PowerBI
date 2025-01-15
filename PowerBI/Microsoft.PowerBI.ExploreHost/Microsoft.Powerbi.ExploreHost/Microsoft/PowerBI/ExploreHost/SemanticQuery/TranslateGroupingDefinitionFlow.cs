using System;
using System.Threading;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000048 RID: 72
	internal sealed class TranslateGroupingDefinitionFlow : SemanticTranslationBaseFlow
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00007420 File Offset: 0x00005620
		internal TranslateGroupingDefinitionFlow(ExploreClientHandlerContext context, string databaseID, GroupingDefinition definition)
			: base(context, databaseID)
		{
			this._definition = definition;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00007431 File Offset: 0x00005631
		internal TranslatedGroupingDefinition Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000743C File Offset: 0x0000563C
		protected override void Translate(EngineDataModel engineDataModel)
		{
			SemanticQueryToDaxTranslationResult semanticQueryToDaxTranslationResult = DataShapeEngineHost.TranslateGroupingQuery(this.Context.DataShapeEngine, new TranslateGroupingQueryCommand
			{
				GroupingDefinition = this._definition
			}, this.Context.FeatureSwitchProvider, engineDataModel, CancellationToken.None, this.Context.AnalyticsFeatureSwitchProvider);
			this._result = TranslatedGroupingDefinition.Create(semanticQueryToDaxTranslationResult);
		}

		// Token: 0x040000DF RID: 223
		private readonly GroupingDefinition _definition;

		// Token: 0x040000E0 RID: 224
		private TranslatedGroupingDefinition _result;
	}
}
