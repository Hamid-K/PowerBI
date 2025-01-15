using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001117 RID: 4375
	internal class ExpressionQueryDomain : INativeQueryDomain, IQueryDomain
	{
		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x06007277 RID: 29303 RVA: 0x00002139 File Offset: 0x00000339
		public bool CanIndex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007278 RID: 29304 RVA: 0x00189A8D File Offset: 0x00187C8D
		public bool IsCompatibleWith(IQueryDomain domain)
		{
			return domain == ExpressionQueryDomain.Instance;
		}

		// Token: 0x06007279 RID: 29305 RVA: 0x00184A2E File Offset: 0x00182C2E
		public Query Optimize(Query query)
		{
			return QueryFolder.Fold(query);
		}

		// Token: 0x0600727A RID: 29306 RVA: 0x00189A98 File Offset: 0x00187C98
		public bool TryGetNativeQuery(Query query, out IResource resource, out Value nativeQuery, out RecordValue options)
		{
			TableQuery tableQuery = query as TableQuery;
			if (tableQuery != null)
			{
				QueryResultTableValue queryResultTableValue = tableQuery.Table as QueryResultTableValue;
				if (queryResultTableValue != null)
				{
					DbValueBuilder dbValueBuilder = queryResultTableValue.ValueBuilder as DbValueBuilder;
					if (dbValueBuilder != null)
					{
						resource = dbValueBuilder.DbEnvironment.Resource;
						nativeQuery = TextValue.New(dbValueBuilder.DbQueryPlan.ExternalQuery);
						options = dbValueBuilder.DbEnvironment.OptionsRecord;
						return true;
					}
				}
			}
			resource = null;
			nativeQuery = null;
			options = RecordValue.Empty;
			return false;
		}

		// Token: 0x04003F1F RID: 16159
		public static readonly INativeQueryDomain Instance = new ExpressionQueryDomain();
	}
}
