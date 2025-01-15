using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema
{
	// Token: 0x020000C4 RID: 196
	public class QueryExtensionSchemaBuilder<TParent> : BaseSchemaExtensionBuilder<QueryExtensionSchema, TParent>
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x0000BD13 File Offset: 0x00009F13
		public QueryExtensionSchemaBuilder(QueryExtensionSchema activeObject, TParent parent)
			: base(activeObject, parent)
		{
			this._entityBuildersByName = new Dictionary<string, QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<TParent>>>(ConceptualNameComparer.Instance);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000BD30 File Offset: 0x00009F30
		public QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<TParent>> WithEntity(string name, string extends = null, QueryExtensionNamingBehavior namingBehavior = QueryExtensionNamingBehavior.Preserve)
		{
			if (base.ActiveObject.Entities == null)
			{
				base.ActiveObject.Entities = new List<QueryExtensionEntity>();
			}
			QueryExtensionEntity queryExtensionEntity = new QueryExtensionEntity
			{
				Name = name,
				Extends = extends,
				NamingBehavior = namingBehavior
			};
			base.ActiveObject.Entities.Add(queryExtensionEntity);
			QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<TParent>> queryExtensionEntityBuilder = new QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<TParent>>(queryExtensionEntity, this);
			if (!this._entityBuildersByName.ContainsKey(name))
			{
				this._entityBuildersByName.Add(name, queryExtensionEntityBuilder);
			}
			return queryExtensionEntityBuilder;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000BDAA File Offset: 0x00009FAA
		public bool TryGetEntity(string name, out QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<TParent>> entityBuilder)
		{
			return this._entityBuildersByName.TryGetValue(name, out entityBuilder);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000BDB9 File Offset: 0x00009FB9
		public QueryExtensionSchema ToExtensionSchema()
		{
			return base.ActiveObject;
		}

		// Token: 0x04000225 RID: 549
		private Dictionary<string, QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<TParent>>> _entityBuildersByName;
	}
}
