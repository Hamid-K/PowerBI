using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000275 RID: 629
	public class FilterDefinitionBuilder<TParent> : BaseBuilder<FilterDefinition, TParent>
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x00022525 File Offset: 0x00020725
		public FilterDefinitionBuilder(TParent parent)
			: base(parent)
		{
			this._from = new List<EntitySource>();
			this._where = new List<QueryFilter>();
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00022544 File Offset: 0x00020744
		public FilterDefinitionBuilder<TParent> WithVersion(int? version)
		{
			this._version = version;
			return this;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0002254E File Offset: 0x0002074E
		public FilterDefinitionBuilder<TParent> WithWhere(QueryExpressionContainer condition)
		{
			return this.WithWhere(new QueryFilter
			{
				Condition = condition
			});
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00022562 File Offset: 0x00020762
		public FilterDefinitionBuilder<TParent> WithWhere(QueryFilter filter)
		{
			this._where.Add(filter);
			return this;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00022571 File Offset: 0x00020771
		public FilterDefinitionBuilder<TParent> WithFrom(string name, string entity, string schema = null)
		{
			return this.WithFrom(new EntitySource
			{
				Name = name,
				Entity = entity,
				Schema = schema
			});
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00022593 File Offset: 0x00020793
		public FilterDefinitionBuilder<TParent> WithFrom(EntitySource from)
		{
			if (!this._from.Contains(from))
			{
				this._from.Add(from);
			}
			return this;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000225B0 File Offset: 0x000207B0
		public FilterDefinitionBuilder<TParent> WithFrom(List<EntitySource> from)
		{
			foreach (EntitySource entitySource in from)
			{
				this.WithFrom(entitySource);
			}
			return this;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00022600 File Offset: 0x00020800
		public override FilterDefinition Build()
		{
			return new FilterDefinition
			{
				Version = this._version,
				From = this._from,
				Where = this._where
			};
		}

		// Token: 0x040007DE RID: 2014
		private int? _version;

		// Token: 0x040007DF RID: 2015
		private List<EntitySource> _from;

		// Token: 0x040007E0 RID: 2016
		private List<QueryFilter> _where;
	}
}
