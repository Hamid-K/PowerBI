using System;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000168 RID: 360
	internal sealed class QueryCommandTree
	{
		// Token: 0x0600144B RID: 5195 RVA: 0x0003AB38 File Offset: 0x00038D38
		internal QueryCommandTree(EntityDataModel entityDataModel, IConceptualSchema schema, QueryExpression query, DaxCapabilities languageCapabilities)
		{
			this._entityDataModel = entityDataModel;
			this._schema = schema;
			this._query = query;
			this._languageCapabilities = languageCapabilities;
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0003AB5D File Offset: 0x00038D5D
		public EntityDataModel EntityDataModel
		{
			get
			{
				return this._entityDataModel;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0003AB65 File Offset: 0x00038D65
		public IConceptualSchema ConceptualSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0003AB6D File Offset: 0x00038D6D
		public QueryExpression Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0003AB75 File Offset: 0x00038D75
		public DaxCapabilities LanguageCapabilities
		{
			get
			{
				return this._languageCapabilities;
			}
		}

		// Token: 0x04000B17 RID: 2839
		private readonly EntityDataModel _entityDataModel;

		// Token: 0x04000B18 RID: 2840
		private readonly IConceptualSchema _schema;

		// Token: 0x04000B19 RID: 2841
		private readonly QueryExpression _query;

		// Token: 0x04000B1A RID: 2842
		private readonly DaxCapabilities _languageCapabilities;
	}
}
