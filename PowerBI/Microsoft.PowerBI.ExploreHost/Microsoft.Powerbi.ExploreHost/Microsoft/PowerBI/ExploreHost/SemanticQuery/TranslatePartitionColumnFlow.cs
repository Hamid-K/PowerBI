using System;
using System.Threading;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000049 RID: 73
	internal sealed class TranslatePartitionColumnFlow : SemanticTranslationBaseFlow
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00007493 File Offset: 0x00005693
		internal TranslatePartitionColumnFlow(ExploreClientHandlerContext context, string databaseID, GroupingDefinition definition)
			: base(context, databaseID)
		{
			this._definition = definition;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000074A4 File Offset: 0x000056A4
		internal TranslatedPartitionColumn Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000074AC File Offset: 0x000056AC
		protected override void Translate(EngineDataModel engineDataModel)
		{
			SemanticQueryToDaxTranslationResult semanticQueryToDaxTranslationResult = DataShapeEngineHost.TranslatePartitionColumn(this.Context.DataShapeEngine, new TranslateGroupingQueryCommand
			{
				GroupingDefinition = this._definition
			}, this.Context.FeatureSwitchProvider, engineDataModel, CancellationToken.None, this.Context.AnalyticsFeatureSwitchProvider);
			this._result = new TranslatedPartitionColumn(semanticQueryToDaxTranslationResult.ClusteringColumnTranslationResult);
		}

		// Token: 0x040000E1 RID: 225
		private readonly GroupingDefinition _definition;

		// Token: 0x040000E2 RID: 226
		private TranslatedPartitionColumn _result;
	}
}
