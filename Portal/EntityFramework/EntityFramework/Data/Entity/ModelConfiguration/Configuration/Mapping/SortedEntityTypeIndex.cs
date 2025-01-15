using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000218 RID: 536
	internal class SortedEntityTypeIndex
	{
		// Token: 0x06001C40 RID: 7232 RVA: 0x00050F9B File Offset: 0x0004F19B
		public SortedEntityTypeIndex()
		{
			this._entityTypes = new Dictionary<EntitySet, List<EntityType>>();
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00050FB0 File Offset: 0x0004F1B0
		public void Add(EntitySet entitySet, EntityType entityType)
		{
			int i = 0;
			List<EntityType> list;
			if (!this._entityTypes.TryGetValue(entitySet, out list))
			{
				list = new List<EntityType>();
				this._entityTypes.Add(entitySet, list);
			}
			while (i < list.Count)
			{
				if (list[i] == entityType)
				{
					return;
				}
				if (entityType.IsAncestorOf(list[i]))
				{
					break;
				}
				i++;
			}
			list.Insert(i, entityType);
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00051014 File Offset: 0x0004F214
		public bool Contains(EntitySet entitySet, EntityType entityType)
		{
			List<EntityType> list;
			return this._entityTypes.TryGetValue(entitySet, out list) && list.Contains(entityType);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0005103C File Offset: 0x0004F23C
		public bool IsRoot(EntitySet entitySet, EntityType entityType)
		{
			bool flag = true;
			foreach (EntityType entityType2 in this._entityTypes[entitySet])
			{
				if (entityType2 != entityType && entityType2.IsAncestorOf(entityType))
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000510A0 File Offset: 0x0004F2A0
		public IEnumerable<EntitySet> GetEntitySets()
		{
			return this._entityTypes.Keys;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000510B0 File Offset: 0x0004F2B0
		public IEnumerable<EntityType> GetEntityTypes(EntitySet entitySet)
		{
			List<EntityType> list;
			if (this._entityTypes.TryGetValue(entitySet, out list))
			{
				return list;
			}
			return SortedEntityTypeIndex._emptyTypes;
		}

		// Token: 0x04000AEA RID: 2794
		private static readonly EntityType[] _emptyTypes = new EntityType[0];

		// Token: 0x04000AEB RID: 2795
		private readonly Dictionary<EntitySet, List<EntityType>> _entityTypes;
	}
}
