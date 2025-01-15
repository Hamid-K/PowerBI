using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200052D RID: 1325
	public class EntityTypeMapping : TypeMapping
	{
		// Token: 0x06004161 RID: 16737 RVA: 0x000DD104 File Offset: 0x000DB304
		public EntityTypeMapping(EntitySetMapping entitySetMapping)
		{
			this._entitySetMapping = entitySetMapping;
			this._fragments = new List<MappingFragment>();
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x000DD13E File Offset: 0x000DB33E
		public EntitySetMapping EntitySetMapping
		{
			get
			{
				return this._entitySetMapping;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x000DD146 File Offset: 0x000DB346
		internal override EntitySetBaseMapping SetMapping
		{
			get
			{
				return this.EntitySetMapping;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x000DD150 File Offset: 0x000DB350
		public EntityType EntityType
		{
			get
			{
				EntityType entityType;
				if ((entityType = this._entityType) == null)
				{
					entityType = (this._entityType = this.m_entityTypes.Values.SingleOrDefault<EntityType>());
				}
				return entityType;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x000DD180 File Offset: 0x000DB380
		public bool IsHierarchyMapping
		{
			get
			{
				return this.m_isOfEntityTypes.Count > 0 || this.m_entityTypes.Count > 1;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004166 RID: 16742 RVA: 0x000DD1A0 File Offset: 0x000DB3A0
		public ReadOnlyCollection<MappingFragment> Fragments
		{
			get
			{
				return new ReadOnlyCollection<MappingFragment>(this._fragments);
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x000DD1AD File Offset: 0x000DB3AD
		internal override ReadOnlyCollection<MappingFragment> MappingFragments
		{
			get
			{
				return this.Fragments;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004168 RID: 16744 RVA: 0x000DD1B5 File Offset: 0x000DB3B5
		public ReadOnlyCollection<EntityTypeBase> EntityTypes
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeBase>(new List<EntityTypeBase>(this.m_entityTypes.Values));
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x000DD1CC File Offset: 0x000DB3CC
		internal override ReadOnlyCollection<EntityTypeBase> Types
		{
			get
			{
				return this.EntityTypes;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600416A RID: 16746 RVA: 0x000DD1D4 File Offset: 0x000DB3D4
		public ReadOnlyCollection<EntityTypeBase> IsOfEntityTypes
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeBase>(new List<EntityTypeBase>(this.m_isOfEntityTypes.Values));
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x000DD1EB File Offset: 0x000DB3EB
		internal override ReadOnlyCollection<EntityTypeBase> IsOfTypes
		{
			get
			{
				return this.IsOfEntityTypes;
			}
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000DD1F3 File Offset: 0x000DB3F3
		public void AddType(EntityType type)
		{
			Check.NotNull<EntityType>(type, "type");
			base.ThrowIfReadOnly();
			this.m_entityTypes.Add(type.FullName, type);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000DD219 File Offset: 0x000DB419
		public void RemoveType(EntityType type)
		{
			Check.NotNull<EntityType>(type, "type");
			base.ThrowIfReadOnly();
			this.m_entityTypes.Remove(type.FullName);
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000DD23F File Offset: 0x000DB43F
		public void AddIsOfType(EntityType type)
		{
			Check.NotNull<EntityType>(type, "type");
			base.ThrowIfReadOnly();
			this.m_isOfEntityTypes.Add(type.FullName, type);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000DD265 File Offset: 0x000DB465
		public void RemoveIsOfType(EntityType type)
		{
			Check.NotNull<EntityType>(type, "type");
			base.ThrowIfReadOnly();
			this.m_isOfEntityTypes.Remove(type.FullName);
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x000DD28B File Offset: 0x000DB48B
		public void AddFragment(MappingFragment fragment)
		{
			Check.NotNull<MappingFragment>(fragment, "fragment");
			base.ThrowIfReadOnly();
			this._fragments.Add(fragment);
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x000DD2AB File Offset: 0x000DB4AB
		public void RemoveFragment(MappingFragment fragment)
		{
			Check.NotNull<MappingFragment>(fragment, "fragment");
			base.ThrowIfReadOnly();
			this._fragments.Remove(fragment);
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000DD2CC File Offset: 0x000DB4CC
		internal override void SetReadOnly()
		{
			this._fragments.TrimExcess();
			MappingItem.SetReadOnly(this._fragments);
			base.SetReadOnly();
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000DD2EC File Offset: 0x000DB4EC
		internal EntityType GetContainerType(string memberName)
		{
			foreach (EntityType entityType in this.m_entityTypes.Values)
			{
				if (entityType.Properties.Contains(memberName))
				{
					return entityType;
				}
			}
			foreach (EntityType entityType2 in this.m_isOfEntityTypes.Values)
			{
				if (entityType2.Properties.Contains(memberName))
				{
					return entityType2;
				}
			}
			return null;
		}

		// Token: 0x040016AC RID: 5804
		private readonly EntitySetMapping _entitySetMapping;

		// Token: 0x040016AD RID: 5805
		private readonly List<MappingFragment> _fragments;

		// Token: 0x040016AE RID: 5806
		private readonly Dictionary<string, EntityType> m_entityTypes = new Dictionary<string, EntityType>(StringComparer.Ordinal);

		// Token: 0x040016AF RID: 5807
		private readonly Dictionary<string, EntityType> m_isOfEntityTypes = new Dictionary<string, EntityType>(StringComparer.Ordinal);

		// Token: 0x040016B0 RID: 5808
		private EntityType _entityType;
	}
}
