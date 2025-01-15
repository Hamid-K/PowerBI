using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200000C RID: 12
	internal class FilterFromManager : IFromManager
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000024DB File Offset: 0x000006DB
		internal FilterFromManager(ReadOnlyCollection<EntitySource> selectEntitySources, List<EntitySource> allFilterEntitySources, List<EntitySource> currentFilterEntitySources, IConceptualSchema conceptualSchema)
		{
			this._selectEntitySources = selectEntitySources;
			this._allFilterEntitySources = allFilterEntitySources;
			this._currentFilterEntitySources = currentFilterEntitySources;
			this._conceptualSchema = conceptualSchema;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002500 File Offset: 0x00000700
		public IConceptualSchema ConceptualSchema
		{
			get
			{
				return this._conceptualSchema;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002508 File Offset: 0x00000708
		public EntitySource GetOrAddEntitySource(string entityName)
		{
			entityName = FromManagerUtils.RewriteEntityName(entityName, this._conceptualSchema);
			EntitySource entitySource;
			if (this._selectEntitySources != null)
			{
				entitySource = FromManagerUtils.FirstOrDefault(this._selectEntitySources, entityName);
				if (entitySource != null)
				{
					if (FromManagerUtils.FirstOrDefault(this._currentFilterEntitySources, entityName) == null)
					{
						this._currentFilterEntitySources.Add(entitySource);
					}
					return entitySource;
				}
			}
			entitySource = FromManagerUtils.FirstOrDefault(this._allFilterEntitySources, entityName);
			if (entitySource != null)
			{
				if (FromManagerUtils.FirstOrDefault(this._currentFilterEntitySources, entityName) == null)
				{
					this._currentFilterEntitySources.Add(entitySource);
				}
				return entitySource;
			}
			List<EntitySource> list = new List<EntitySource>(this._allFilterEntitySources);
			if (this._selectEntitySources != null)
			{
				list.AddRange(this._selectEntitySources);
			}
			EntitySource entitySource2 = FromManagerUtils.CreateEntitySource(list, entityName);
			this._allFilterEntitySources.Add(entitySource2);
			this._currentFilterEntitySources.Add(entitySource2);
			return entitySource2;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025DC File Offset: 0x000007DC
		public string RewritePropertyName(EntitySource entitySource, string propertyName, out bool isMeasure)
		{
			return FromManagerUtils.RewritePropertyName(entitySource, propertyName, this._conceptualSchema, out isMeasure);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025EC File Offset: 0x000007EC
		public string RewriteHierarchyName(EntitySource entitySource, string hierarchyName)
		{
			return FromManagerUtils.RewriteHierarchyName(entitySource, hierarchyName, this._conceptualSchema);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025FB File Offset: 0x000007FB
		public string RewriteHierarchyLevelName(EntitySource entitySource, string hierarchyName, string hierarchyLevelName)
		{
			return FromManagerUtils.RewriteHierarchyLevelName(entitySource, hierarchyName, hierarchyLevelName, this._conceptualSchema);
		}

		// Token: 0x0400004E RID: 78
		private readonly IConceptualSchema _conceptualSchema;

		// Token: 0x0400004F RID: 79
		private readonly ReadOnlyCollection<EntitySource> _selectEntitySources;

		// Token: 0x04000050 RID: 80
		private readonly List<EntitySource> _allFilterEntitySources;

		// Token: 0x04000051 RID: 81
		private readonly List<EntitySource> _currentFilterEntitySources;
	}
}
