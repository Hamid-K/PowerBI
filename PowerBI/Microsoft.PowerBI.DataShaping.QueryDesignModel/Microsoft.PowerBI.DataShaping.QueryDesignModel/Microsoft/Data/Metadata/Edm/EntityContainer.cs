using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000086 RID: 134
	public sealed class EntityContainer : GlobalItem
	{
		// Token: 0x060009F0 RID: 2544 RVA: 0x00017ACC File Offset: 0x00015CCC
		internal EntityContainer(string name, DataSpace dataSpace)
		{
			EntityUtil.CheckStringArgument(name, "name");
			this._name = name;
			base.DataSpace = dataSpace;
			this._baseEntitySets = new ReadOnlyMetadataCollection<EntitySetBase>(new EntitySetBaseCollection(this));
			this._functionImports = new ReadOnlyMetadataCollection<EdmFunction>(new MetadataCollection<EdmFunction>());
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00017B19 File Offset: 0x00015D19
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntityContainer;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00017B1D File Offset: 0x00015D1D
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00017B25 File Offset: 0x00015D25
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00017B2D File Offset: 0x00015D2D
		[MetadataProperty(BuiltInTypeKind.EntitySetBase, true)]
		public ReadOnlyMetadataCollection<EntitySetBase> BaseEntitySets
		{
			get
			{
				return this._baseEntitySets;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00017B35 File Offset: 0x00015D35
		[MetadataProperty(BuiltInTypeKind.EdmFunction, true)]
		public ReadOnlyMetadataCollection<EdmFunction> FunctionImports
		{
			get
			{
				return this._functionImports;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00017B3D File Offset: 0x00015D3D
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.BaseEntitySets.Source.SetReadOnly();
				this.FunctionImports.Source.SetReadOnly();
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00017B70 File Offset: 0x00015D70
		public EntitySet GetEntitySetByName(string name, bool ignoreCase)
		{
			EntitySet entitySet = this.BaseEntitySets.GetValue(name, ignoreCase) as EntitySet;
			if (entitySet != null)
			{
				return entitySet;
			}
			throw EntityUtil.InvalidRelationshipSetName(name);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00017B9C File Offset: 0x00015D9C
		public bool TryGetEntitySetByName(string name, bool ignoreCase, out EntitySet entitySet)
		{
			EntityUtil.CheckArgumentNull<string>(name, "name");
			EntitySetBase entitySetBase = null;
			entitySet = null;
			if (this.BaseEntitySets.TryGetValue(name, ignoreCase, out entitySetBase) && Helper.IsEntitySet(entitySetBase))
			{
				entitySet = (EntitySet)entitySetBase;
				return true;
			}
			return false;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00017BE0 File Offset: 0x00015DE0
		public RelationshipSet GetRelationshipSetByName(string name, bool ignoreCase)
		{
			RelationshipSet relationshipSet;
			if (!this.TryGetRelationshipSetByName(name, ignoreCase, out relationshipSet))
			{
				throw EntityUtil.InvalidRelationshipSetName(name);
			}
			return relationshipSet;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00017C04 File Offset: 0x00015E04
		public bool TryGetRelationshipSetByName(string name, bool ignoreCase, out RelationshipSet relationshipSet)
		{
			EntityUtil.CheckArgumentNull<string>(name, "name");
			EntitySetBase entitySetBase = null;
			relationshipSet = null;
			if (this.BaseEntitySets.TryGetValue(name, ignoreCase, out entitySetBase) && Helper.IsRelationshipSet(entitySetBase))
			{
				relationshipSet = (RelationshipSet)entitySetBase;
				return true;
			}
			return false;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00017C46 File Offset: 0x00015E46
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00017C4E File Offset: 0x00015E4E
		internal void AddEntitySetBase(EntitySetBase entitySetBase)
		{
			this._baseEntitySets.Source.Add(entitySetBase);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00017C61 File Offset: 0x00015E61
		internal void AddFunctionImport(EdmFunction function)
		{
			this._functionImports.Source.Add(function);
		}

		// Token: 0x04000818 RID: 2072
		private readonly string _name;

		// Token: 0x04000819 RID: 2073
		private readonly ReadOnlyMetadataCollection<EntitySetBase> _baseEntitySets;

		// Token: 0x0400081A RID: 2074
		private readonly ReadOnlyMetadataCollection<EdmFunction> _functionImports;
	}
}
