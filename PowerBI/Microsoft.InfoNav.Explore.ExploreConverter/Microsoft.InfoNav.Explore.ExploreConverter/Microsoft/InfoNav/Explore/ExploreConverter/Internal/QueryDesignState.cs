using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000075 RID: 117
	internal sealed class QueryDesignState
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000BADA File Offset: 0x00009CDA
		internal QueryDesignState(QueryDesignQueryDefinition queryDefinition)
		{
			this._queryDefinition = queryDefinition;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000BAE9 File Offset: 0x00009CE9
		public QueryDesignQueryDefinition QueryDefinition
		{
			get
			{
				return this._queryDefinition;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		internal IRdmQueryExpression FindByName(string name)
		{
			QueryDesignGroup queryDesignGroup;
			if (this._queryDefinition.Groups.TryGetValue(name, out queryDesignGroup))
			{
				return queryDesignGroup.Key;
			}
			QueryDesignMeasure queryDesignMeasure;
			if (this._queryDefinition.Measures.TryGetValue(name, out queryDesignMeasure))
			{
				return queryDesignMeasure.Expression;
			}
			return null;
		}

		// Token: 0x0400018C RID: 396
		private readonly QueryDesignQueryDefinition _queryDefinition;
	}
}
