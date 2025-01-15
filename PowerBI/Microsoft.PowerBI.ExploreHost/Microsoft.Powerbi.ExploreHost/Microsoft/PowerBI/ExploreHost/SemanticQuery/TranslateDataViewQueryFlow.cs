using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.Engine.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryTranslation;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000047 RID: 71
	internal sealed class TranslateDataViewQueryFlow : SemanticTranslationBaseFlow
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000730C File Offset: 0x0000550C
		internal TranslateDataViewQueryFlow(ExploreClientHandlerContext context, string databaseID, DataViewQueryDefinition definition)
			: base(context, databaseID)
		{
			this._definition = definition;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000731D File Offset: 0x0000551D
		internal DataViewQueryTranslationResult Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007328 File Offset: 0x00005528
		protected override void Translate(EngineDataModel engineDataModel)
		{
			TranslateQueryCommand translateQueryCommand = new TranslateQueryCommandBuilder(TranslateQueryCommandVersions.Version0).WithQuery(this._definition.Query).Build();
			TranslateSemanticQueryResult translateSemanticQueryResult = DataShapeEngineHost.TranslateDataViewQuery(this.Context.DataShapeEngine, translateQueryCommand, this.Context.FeatureSwitchProvider, engineDataModel, this.Context.AnalyticsFeatureSwitchProvider);
			this._result = new DataViewQueryTranslationResult(translateSemanticQueryResult.TranslatedQuery.Query, TranslateDataViewQueryFlow.ExtractNameToDaxColumnNameMap(translateSemanticQueryResult.TranslatedQuery));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000073A0 File Offset: 0x000055A0
		private static IReadOnlyDictionary<string, string> ExtractNameToDaxColumnNameMap(TranslatedQuery translatedQuery)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(QueryNameComparer.Instance);
			TranslatedQuerySchema schema = translatedQuery.Schema;
			IList<TranslatedSelect> list = ((schema != null) ? schema.Selects : null);
			if (list != null)
			{
				foreach (TranslatedSelect translatedSelect in list)
				{
					if (translatedSelect.Name != null)
					{
						dictionary.Add(translatedSelect.Name, translatedSelect.ColumnName);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x040000DD RID: 221
		private readonly DataViewQueryDefinition _definition;

		// Token: 0x040000DE RID: 222
		private DataViewQueryTranslationResult _result;
	}
}
