using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B7 RID: 1207
	public class EntityContainer : GlobalItem
	{
		// Token: 0x06003B9D RID: 15261 RVA: 0x000C5D50 File Offset: 0x000C3F50
		internal EntityContainer()
		{
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x000C5D64 File Offset: 0x000C3F64
		public EntityContainer(string name, DataSpace dataSpace)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this.DataSpace = dataSpace;
			this._baseEntitySets = new ReadOnlyMetadataCollection<EntitySetBase>(new EntitySetBaseCollection(this));
			this._functionImports = new ReadOnlyMetadataCollection<EdmFunction>(new MetadataCollection<EdmFunction>());
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x000C5DBD File Offset: 0x000C3FBD
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntityContainer;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000C5DC1 File Offset: 0x000C3FC1
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x000C5DC9 File Offset: 0x000C3FC9
		// (set) Token: 0x06003BA2 RID: 15266 RVA: 0x000C5DD1 File Offset: 0x000C3FD1
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				this._name = value;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000C5DEC File Offset: 0x000C3FEC
		[MetadataProperty(BuiltInTypeKind.EntitySetBase, true)]
		public ReadOnlyMetadataCollection<EntitySetBase> BaseEntitySets
		{
			get
			{
				return this._baseEntitySets;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000C5DF4 File Offset: 0x000C3FF4
		public ReadOnlyMetadataCollection<AssociationSet> AssociationSets
		{
			get
			{
				ReadOnlyMetadataCollection<AssociationSet> readOnlyMetadataCollection = this._associationSetsCache;
				if (readOnlyMetadataCollection == null)
				{
					object baseEntitySetsLock = this._baseEntitySetsLock;
					lock (baseEntitySetsLock)
					{
						if (this._associationSetsCache == null)
						{
							this._baseEntitySets.SourceAccessed += this.ResetAssociationSetsCache;
							this._associationSetsCache = new FilteredReadOnlyMetadataCollection<AssociationSet, EntitySetBase>(this._baseEntitySets, new Predicate<EntitySetBase>(Helper.IsAssociationSet));
						}
						readOnlyMetadataCollection = this._associationSetsCache;
					}
				}
				return readOnlyMetadataCollection;
			}
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000C5E7C File Offset: 0x000C407C
		private void ResetAssociationSetsCache(object sender, EventArgs e)
		{
			if (this._associationSetsCache != null)
			{
				object baseEntitySetsLock = this._baseEntitySetsLock;
				lock (baseEntitySetsLock)
				{
					if (this._associationSetsCache != null)
					{
						this._associationSetsCache = null;
						this._baseEntitySets.SourceAccessed -= this.ResetAssociationSetsCache;
					}
				}
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000C5EE4 File Offset: 0x000C40E4
		public ReadOnlyMetadataCollection<EntitySet> EntitySets
		{
			get
			{
				ReadOnlyMetadataCollection<EntitySet> readOnlyMetadataCollection = this._entitySetsCache;
				if (readOnlyMetadataCollection == null)
				{
					object baseEntitySetsLock = this._baseEntitySetsLock;
					lock (baseEntitySetsLock)
					{
						if (this._entitySetsCache == null)
						{
							this._baseEntitySets.SourceAccessed += this.ResetEntitySetsCache;
							this._entitySetsCache = new FilteredReadOnlyMetadataCollection<EntitySet, EntitySetBase>(this._baseEntitySets, new Predicate<EntitySetBase>(Helper.IsEntitySet));
						}
						readOnlyMetadataCollection = this._entitySetsCache;
					}
				}
				return readOnlyMetadataCollection;
			}
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000C5F6C File Offset: 0x000C416C
		private void ResetEntitySetsCache(object sender, EventArgs e)
		{
			if (this._entitySetsCache != null)
			{
				object baseEntitySetsLock = this._baseEntitySetsLock;
				lock (baseEntitySetsLock)
				{
					if (this._entitySetsCache != null)
					{
						this._entitySetsCache = null;
						this._baseEntitySets.SourceAccessed -= this.ResetEntitySetsCache;
					}
				}
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000C5FD4 File Offset: 0x000C41D4
		[MetadataProperty(BuiltInTypeKind.EdmFunction, true)]
		public ReadOnlyMetadataCollection<EdmFunction> FunctionImports
		{
			get
			{
				return this._functionImports;
			}
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000C5FDC File Offset: 0x000C41DC
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.BaseEntitySets.Source.SetReadOnly();
				this.FunctionImports.Source.SetReadOnly();
			}
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x000C6010 File Offset: 0x000C4210
		public EntitySet GetEntitySetByName(string name, bool ignoreCase)
		{
			EntitySet entitySet = this.BaseEntitySets.GetValue(name, ignoreCase) as EntitySet;
			if (entitySet != null)
			{
				return entitySet;
			}
			throw new ArgumentException(Strings.InvalidEntitySetName(name));
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x000C6040 File Offset: 0x000C4240
		public bool TryGetEntitySetByName(string name, bool ignoreCase, out EntitySet entitySet)
		{
			Check.NotNull<string>(name, "name");
			EntitySetBase entitySetBase = null;
			entitySet = null;
			if (this.BaseEntitySets.TryGetValue(name, ignoreCase, out entitySetBase) && Helper.IsEntitySet(entitySetBase))
			{
				entitySet = (EntitySet)entitySetBase;
				return true;
			}
			return false;
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000C6084 File Offset: 0x000C4284
		public RelationshipSet GetRelationshipSetByName(string name, bool ignoreCase)
		{
			RelationshipSet relationshipSet;
			if (!this.TryGetRelationshipSetByName(name, ignoreCase, out relationshipSet))
			{
				throw new ArgumentException(Strings.InvalidRelationshipSetName(name));
			}
			return relationshipSet;
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000C60AC File Offset: 0x000C42AC
		public bool TryGetRelationshipSetByName(string name, bool ignoreCase, out RelationshipSet relationshipSet)
		{
			Check.NotNull<string>(name, "name");
			EntitySetBase entitySetBase = null;
			relationshipSet = null;
			if (this.BaseEntitySets.TryGetValue(name, ignoreCase, out entitySetBase) && Helper.IsRelationshipSet(entitySetBase))
			{
				relationshipSet = (RelationshipSet)entitySetBase;
				return true;
			}
			return false;
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x000C60EE File Offset: 0x000C42EE
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000C60F6 File Offset: 0x000C42F6
		public void AddEntitySetBase(EntitySetBase entitySetBase)
		{
			Check.NotNull<EntitySetBase>(entitySetBase, "entitySetBase");
			Util.ThrowIfReadOnly(this);
			this._baseEntitySets.Source.Add(entitySetBase);
			entitySetBase.ChangeEntityContainerWithoutCollectionFixup(this);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000C6122 File Offset: 0x000C4322
		public void RemoveEntitySetBase(EntitySetBase entitySetBase)
		{
			Check.NotNull<EntitySetBase>(entitySetBase, "entitySetBase");
			Util.ThrowIfReadOnly(this);
			this._baseEntitySets.Source.Remove(entitySetBase);
			entitySetBase.ChangeEntityContainerWithoutCollectionFixup(null);
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000C614F File Offset: 0x000C434F
		public void AddFunctionImport(EdmFunction function)
		{
			Check.NotNull<EdmFunction>(function, "function");
			Util.ThrowIfReadOnly(this);
			if (!function.IsFunctionImport)
			{
				throw new ArgumentException(Strings.OnlyFunctionImportsCanBeAddedToEntityContainer(function.Name));
			}
			this._functionImports.Source.Add(function);
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000C6190 File Offset: 0x000C4390
		public static EntityContainer Create(string name, DataSpace dataSpace, IEnumerable<EntitySetBase> entitySets, IEnumerable<EdmFunction> functionImports, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			EntityContainer entityContainer = new EntityContainer(name, dataSpace);
			if (entitySets != null)
			{
				foreach (EntitySetBase entitySetBase in entitySets)
				{
					entityContainer.AddEntitySetBase(entitySetBase);
				}
			}
			if (functionImports != null)
			{
				foreach (EdmFunction edmFunction in functionImports)
				{
					if (!edmFunction.IsFunctionImport)
					{
						throw new ArgumentException(Strings.OnlyFunctionImportsCanBeAddedToEntityContainer(edmFunction.Name));
					}
					entityContainer.AddFunctionImport(edmFunction);
				}
			}
			if (metadataProperties != null)
			{
				entityContainer.AddMetadataProperties(metadataProperties);
			}
			entityContainer.SetReadOnly();
			return entityContainer;
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000C625C File Offset: 0x000C445C
		internal virtual void NotifyItemIdentityChanged(EntitySetBase item, string initialIdentity)
		{
			this._baseEntitySets.Source.HandleIdentityChange(item, initialIdentity);
		}

		// Token: 0x0400148B RID: 5259
		private string _name;

		// Token: 0x0400148C RID: 5260
		private readonly ReadOnlyMetadataCollection<EntitySetBase> _baseEntitySets;

		// Token: 0x0400148D RID: 5261
		private readonly ReadOnlyMetadataCollection<EdmFunction> _functionImports;

		// Token: 0x0400148E RID: 5262
		private readonly object _baseEntitySetsLock = new object();

		// Token: 0x0400148F RID: 5263
		private ReadOnlyMetadataCollection<AssociationSet> _associationSetsCache;

		// Token: 0x04001490 RID: 5264
		private ReadOnlyMetadataCollection<EntitySet> _entitySetsCache;
	}
}
