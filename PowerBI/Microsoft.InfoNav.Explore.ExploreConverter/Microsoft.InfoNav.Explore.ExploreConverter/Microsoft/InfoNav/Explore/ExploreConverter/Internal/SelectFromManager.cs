using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000BA RID: 186
	internal class SelectFromManager : IFromManager
	{
		// Token: 0x060003E6 RID: 998 RVA: 0x00014385 File Offset: 0x00012585
		internal SelectFromManager(List<EntitySource> entitySources, IConceptualSchema conceptualSchema)
		{
			this._entitySources = entitySources;
			this._conceptualSchema = conceptualSchema;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001439B File Offset: 0x0001259B
		public IConceptualSchema ConceptualSchema
		{
			get
			{
				return this._conceptualSchema;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000143A4 File Offset: 0x000125A4
		public EntitySource GetOrAddEntitySource(string entityName)
		{
			entityName = FromManagerUtils.RewriteEntityName(entityName, this._conceptualSchema);
			EntitySource entitySource = FromManagerUtils.FirstOrDefault(this._entitySources, entityName);
			if (entitySource != null)
			{
				return entitySource;
			}
			EntitySource entitySource2 = FromManagerUtils.CreateEntitySource(this._entitySources, entityName);
			this._entitySources.Add(entitySource2);
			return entitySource2;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000143F1 File Offset: 0x000125F1
		public string RewritePropertyName(EntitySource entitySource, string propertyName, out bool isMeasure)
		{
			return FromManagerUtils.RewritePropertyName(entitySource, propertyName, this._conceptualSchema, out isMeasure);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00014401 File Offset: 0x00012601
		public string RewriteHierarchyName(EntitySource entitySource, string hierarchyName)
		{
			return FromManagerUtils.RewriteHierarchyName(entitySource, hierarchyName, this._conceptualSchema);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00014410 File Offset: 0x00012610
		public string RewriteHierarchyLevelName(EntitySource entitySource, string hierarchyName, string hierarchyLevelName)
		{
			return FromManagerUtils.RewriteHierarchyLevelName(entitySource, hierarchyName, hierarchyLevelName, this._conceptualSchema);
		}

		// Token: 0x040002AA RID: 682
		private readonly IConceptualSchema _conceptualSchema;

		// Token: 0x040002AB RID: 683
		private readonly List<EntitySource> _entitySources;
	}
}
