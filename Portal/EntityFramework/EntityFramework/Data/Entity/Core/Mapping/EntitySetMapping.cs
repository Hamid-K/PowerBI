using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200052C RID: 1324
	public class EntitySetMapping : EntitySetBaseMapping
	{
		// Token: 0x06004151 RID: 16721 RVA: 0x000DCE18 File Offset: 0x000DB018
		public EntitySetMapping(EntitySet entitySet, EntityContainerMapping containerMapping)
			: base(containerMapping)
		{
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			this._entitySet = entitySet;
			this._entityTypeMappings = new List<EntityTypeMapping>();
			this._modificationFunctionMappings = new List<EntityTypeModificationFunctionMapping>();
			this._implicitlyMappedAssociationSetEnds = new Lazy<List<AssociationSetEnd>>(new Func<List<AssociationSetEnd>>(this.InitializeImplicitlyMappedAssociationSetEnds));
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x000DCE6C File Offset: 0x000DB06C
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x000DCE74 File Offset: 0x000DB074
		internal override EntitySetBase Set
		{
			get
			{
				return this.EntitySet;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x000DCE7C File Offset: 0x000DB07C
		public ReadOnlyCollection<EntityTypeMapping> EntityTypeMappings
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeMapping>(this._entityTypeMappings);
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x000DCE89 File Offset: 0x000DB089
		internal override IEnumerable<TypeMapping> TypeMappings
		{
			get
			{
				return this._entityTypeMappings;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004156 RID: 16726 RVA: 0x000DCE91 File Offset: 0x000DB091
		public ReadOnlyCollection<EntityTypeModificationFunctionMapping> ModificationFunctionMappings
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeModificationFunctionMapping>(this._modificationFunctionMappings);
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x000DCE9E File Offset: 0x000DB09E
		internal IEnumerable<AssociationSetEnd> ImplicitlyMappedAssociationSetEnds
		{
			get
			{
				return this._implicitlyMappedAssociationSetEnds.Value;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x000DCEAB File Offset: 0x000DB0AB
		internal override bool HasNoContent
		{
			get
			{
				return this._modificationFunctionMappings.Count == 0 && base.HasNoContent;
			}
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x000DCEC2 File Offset: 0x000DB0C2
		public void AddTypeMapping(EntityTypeMapping typeMapping)
		{
			Check.NotNull<EntityTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._entityTypeMappings.Add(typeMapping);
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x000DCEE2 File Offset: 0x000DB0E2
		public void RemoveTypeMapping(EntityTypeMapping typeMapping)
		{
			Check.NotNull<EntityTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._entityTypeMappings.Remove(typeMapping);
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000DCF03 File Offset: 0x000DB103
		internal void ClearModificationFunctionMappings()
		{
			this._modificationFunctionMappings.Clear();
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x000DCF10 File Offset: 0x000DB110
		public void AddModificationFunctionMapping(EntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			Check.NotNull<EntityTypeModificationFunctionMapping>(modificationFunctionMapping, "modificationFunctionMapping");
			base.ThrowIfReadOnly();
			this._modificationFunctionMappings.Add(modificationFunctionMapping);
			if (this._implicitlyMappedAssociationSetEnds.IsValueCreated)
			{
				this._implicitlyMappedAssociationSetEnds = new Lazy<List<AssociationSetEnd>>(new Func<List<AssociationSetEnd>>(this.InitializeImplicitlyMappedAssociationSetEnds));
			}
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000DCF60 File Offset: 0x000DB160
		public void RemoveModificationFunctionMapping(EntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			Check.NotNull<EntityTypeModificationFunctionMapping>(modificationFunctionMapping, "modificationFunctionMapping");
			base.ThrowIfReadOnly();
			this._modificationFunctionMappings.Remove(modificationFunctionMapping);
			if (this._implicitlyMappedAssociationSetEnds.IsValueCreated)
			{
				this._implicitlyMappedAssociationSetEnds = new Lazy<List<AssociationSetEnd>>(new Func<List<AssociationSetEnd>>(this.InitializeImplicitlyMappedAssociationSetEnds));
			}
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000DCFB0 File Offset: 0x000DB1B0
		internal override void SetReadOnly()
		{
			this._entityTypeMappings.TrimExcess();
			this._modificationFunctionMappings.TrimExcess();
			if (this._implicitlyMappedAssociationSetEnds.IsValueCreated)
			{
				this._implicitlyMappedAssociationSetEnds.Value.TrimExcess();
			}
			MappingItem.SetReadOnly(this._entityTypeMappings);
			MappingItem.SetReadOnly(this._modificationFunctionMappings);
			base.SetReadOnly();
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000DD00C File Offset: 0x000DB20C
		[Conditional("DEBUG")]
		private void AssertModificationFunctionMappingInvariants(EntityTypeModificationFunctionMapping modificationFunctionMapping)
		{
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in this._modificationFunctionMappings)
			{
			}
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x000DD058 File Offset: 0x000DB258
		private List<AssociationSetEnd> InitializeImplicitlyMappedAssociationSetEnds()
		{
			List<AssociationSetEnd> list = new List<AssociationSetEnd>();
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in this._modificationFunctionMappings)
			{
				if (entityTypeModificationFunctionMapping.DeleteFunctionMapping != null)
				{
					list.AddRange(entityTypeModificationFunctionMapping.DeleteFunctionMapping.CollocatedAssociationSetEnds);
				}
				if (entityTypeModificationFunctionMapping.InsertFunctionMapping != null)
				{
					list.AddRange(entityTypeModificationFunctionMapping.InsertFunctionMapping.CollocatedAssociationSetEnds);
				}
				if (entityTypeModificationFunctionMapping.UpdateFunctionMapping != null)
				{
					list.AddRange(entityTypeModificationFunctionMapping.UpdateFunctionMapping.CollocatedAssociationSetEnds);
				}
			}
			if (base.IsReadOnly)
			{
				list.TrimExcess();
			}
			return list;
		}

		// Token: 0x040016A8 RID: 5800
		private readonly EntitySet _entitySet;

		// Token: 0x040016A9 RID: 5801
		private readonly List<EntityTypeMapping> _entityTypeMappings;

		// Token: 0x040016AA RID: 5802
		private readonly List<EntityTypeModificationFunctionMapping> _modificationFunctionMappings;

		// Token: 0x040016AB RID: 5803
		private Lazy<List<AssociationSetEnd>> _implicitlyMappedAssociationSetEnds;
	}
}
