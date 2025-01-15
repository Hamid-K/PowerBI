using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000029 RID: 41
	internal sealed class QueryDefinitionBuilder
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000063C8 File Offset: 0x000045C8
		internal QueryDefinitionBuilder(IConceptualSchema conceptualSchema)
		{
			this._query = new QueryDefinition();
			this._query.Version = new int?(2);
			this._from = new SelectFromManager(this._query.From, conceptualSchema);
			this._selectNames = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000641E File Offset: 0x0000461E
		internal IConceptualSchema ConceptualSchema
		{
			get
			{
				return this._from.ConceptualSchema;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000642B File Offset: 0x0000462B
		internal IFromManager GetFromClause()
		{
			return this._from;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006433 File Offset: 0x00004633
		internal void AddOrderBy(QuerySortClause sortClause)
		{
			this._query.OrderBy.Add(sortClause);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006448 File Offset: 0x00004648
		internal string AddSelect(QueryExpressionContainer expression)
		{
			string text = StringUtil.MakeUniqueName("select", this._selectNames);
			this._selectNames.Add(text);
			expression.Name = text;
			this._query.Select.Add(expression);
			return text;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000648C File Offset: 0x0000468C
		internal bool TryGetSelectName(QueryExpressionContainer expression, out string name)
		{
			QueryExpressionContainer queryExpressionContainer = this._query.Select.FirstOrDefault((QueryExpressionContainer expr) => expression.Expression.Equals(expr.Expression));
			if (queryExpressionContainer != null)
			{
				name = queryExpressionContainer.Name;
				return true;
			}
			name = null;
			return false;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000064DA File Offset: 0x000046DA
		internal QueryDefinition ToQueryDefinition()
		{
			return this._query;
		}

		// Token: 0x040000B7 RID: 183
		private readonly QueryDefinition _query;

		// Token: 0x040000B8 RID: 184
		private readonly IFromManager _from;

		// Token: 0x040000B9 RID: 185
		private readonly HashSet<string> _selectNames;
	}
}
