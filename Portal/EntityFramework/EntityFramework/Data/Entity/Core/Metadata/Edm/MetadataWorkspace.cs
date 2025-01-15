using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Common.QueryCache;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E0 RID: 1248
	public class MetadataWorkspace
	{
		// Token: 0x06003DEB RID: 15851 RVA: 0x000CD8A8 File Offset: 0x000CBAA8
		public MetadataWorkspace()
		{
			this._itemsOSpace = new Lazy<ObjectItemCollection>(() => new ObjectItemCollection(), true);
			this.MetadataOptimization = new MetadataOptimization(this);
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000CD908 File Offset: 0x000CBB08
		public MetadataWorkspace(Func<EdmItemCollection> cSpaceLoader, Func<StoreItemCollection> sSpaceLoader, Func<StorageMappingItemCollection> csMappingLoader, Func<ObjectItemCollection> oSpaceLoader)
		{
			MetadataWorkspace <>4__this = this;
			Check.NotNull<Func<EdmItemCollection>>(cSpaceLoader, "cSpaceLoader");
			Check.NotNull<Func<StoreItemCollection>>(sSpaceLoader, "sSpaceLoader");
			Check.NotNull<Func<StorageMappingItemCollection>>(csMappingLoader, "csMappingLoader");
			Check.NotNull<Func<ObjectItemCollection>>(oSpaceLoader, "oSpaceLoader");
			this._itemsCSpace = new Lazy<EdmItemCollection>(() => <>4__this.LoadAndCheckItemCollection<EdmItemCollection>(cSpaceLoader), true);
			this._itemsSSpace = new Lazy<StoreItemCollection>(() => <>4__this.LoadAndCheckItemCollection<StoreItemCollection>(sSpaceLoader), true);
			this._itemsOSpace = new Lazy<ObjectItemCollection>(oSpaceLoader, true);
			this._itemsCSSpace = new Lazy<StorageMappingItemCollection>(() => <>4__this.LoadAndCheckItemCollection<StorageMappingItemCollection>(csMappingLoader), true);
			this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(<>4__this._itemsCSpace.Value, <>4__this._itemsOSpace.Value), true);
			this.MetadataOptimization = new MetadataOptimization(this);
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x000CDA10 File Offset: 0x000CBC10
		public MetadataWorkspace(Func<EdmItemCollection> cSpaceLoader, Func<StoreItemCollection> sSpaceLoader, Func<StorageMappingItemCollection> csMappingLoader)
		{
			MetadataWorkspace <>4__this = this;
			Check.NotNull<Func<EdmItemCollection>>(cSpaceLoader, "cSpaceLoader");
			Check.NotNull<Func<StoreItemCollection>>(sSpaceLoader, "sSpaceLoader");
			Check.NotNull<Func<StorageMappingItemCollection>>(csMappingLoader, "csMappingLoader");
			this._itemsCSpace = new Lazy<EdmItemCollection>(() => <>4__this.LoadAndCheckItemCollection<EdmItemCollection>(cSpaceLoader), true);
			this._itemsSSpace = new Lazy<StoreItemCollection>(() => <>4__this.LoadAndCheckItemCollection<StoreItemCollection>(sSpaceLoader), true);
			this._itemsOSpace = new Lazy<ObjectItemCollection>(() => new ObjectItemCollection(), true);
			this._itemsCSSpace = new Lazy<StorageMappingItemCollection>(() => <>4__this.LoadAndCheckItemCollection<StorageMappingItemCollection>(csMappingLoader), true);
			this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(<>4__this._itemsCSpace.Value, <>4__this._itemsOSpace.Value), true);
			this.MetadataOptimization = new MetadataOptimization(this);
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000CDB28 File Offset: 0x000CBD28
		public MetadataWorkspace(IEnumerable<string> paths, IEnumerable<Assembly> assembliesToConsider)
		{
			Check.NotNull<IEnumerable<string>>(paths, "paths");
			Check.NotNull<IEnumerable<Assembly>>(assembliesToConsider, "assembliesToConsider");
			EntityUtil.CheckArgumentContainsNull<string>(ref paths, "paths");
			EntityUtil.CheckArgumentContainsNull<Assembly>(ref assembliesToConsider, "assembliesToConsider");
			Func<AssemblyName, Assembly> func = delegate(AssemblyName referenceName)
			{
				foreach (Assembly assembly in assembliesToConsider)
				{
					if (AssemblyName.ReferenceMatchesDefinition(referenceName, new AssemblyName(assembly.FullName)))
					{
						return assembly;
					}
				}
				throw new ArgumentException(Strings.AssemblyMissingFromAssembliesToConsider(referenceName.FullName), "assembliesToConsider");
			};
			this.CreateMetadataWorkspaceWithResolver(paths, () => assembliesToConsider, func);
			this.MetadataOptimization = new MetadataOptimization(this);
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x000CDBC8 File Offset: 0x000CBDC8
		private void CreateMetadataWorkspaceWithResolver(IEnumerable<string> paths, Func<IEnumerable<Assembly>> wildcardAssemblies, Func<AssemblyName, Assembly> resolveReference)
		{
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(paths.ToArray<string>(), "", new CustomAssemblyResolver(wildcardAssemblies, resolveReference));
			this._itemsOSpace = new Lazy<ObjectItemCollection>(() => new ObjectItemCollection(), true);
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(DataSpace.CSpace)))
			{
				if (disposableCollectionWrapper.Any<XmlReader>())
				{
					EdmItemCollection itemCollection = new EdmItemCollection(disposableCollectionWrapper, metadataArtifactLoader.GetPaths(DataSpace.CSpace), false);
					this._itemsCSpace = new Lazy<EdmItemCollection>(() => itemCollection, true);
					this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(itemCollection, this._itemsOSpace.Value), true);
				}
			}
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper2 = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(DataSpace.SSpace)))
			{
				if (disposableCollectionWrapper2.Any<XmlReader>())
				{
					StoreItemCollection itemCollection2 = new StoreItemCollection(disposableCollectionWrapper2, metadataArtifactLoader.GetPaths(DataSpace.SSpace));
					this._itemsSSpace = new Lazy<StoreItemCollection>(() => itemCollection2, true);
				}
			}
			using (DisposableCollectionWrapper<XmlReader> disposableCollectionWrapper3 = new DisposableCollectionWrapper<XmlReader>(metadataArtifactLoader.CreateReaders(DataSpace.CSSpace)))
			{
				if (disposableCollectionWrapper3.Any<XmlReader>() && this._itemsCSpace != null && this._itemsSSpace != null)
				{
					StorageMappingItemCollection mapping = new StorageMappingItemCollection(this._itemsCSpace.Value, this._itemsSSpace.Value, disposableCollectionWrapper3, metadataArtifactLoader.GetPaths(DataSpace.CSSpace));
					this._itemsCSSpace = new Lazy<StorageMappingItemCollection>(() => mapping, true);
				}
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x000CDD88 File Offset: 0x000CBF88
		private static IEnumerable<double> SupportedEdmVersions
		{
			get
			{
				yield return 0.0;
				yield return 1.0;
				yield return 2.0;
				yield return 3.0;
				yield break;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06003DF1 RID: 15857 RVA: 0x000CDD91 File Offset: 0x000CBF91
		public static double MaximumEdmVersionSupported
		{
			get
			{
				return MetadataWorkspace._maximumEdmVersionSupported;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06003DF2 RID: 15858 RVA: 0x000CDD98 File Offset: 0x000CBF98
		internal virtual Guid MetadataWorkspaceId
		{
			get
			{
				return this._metadataWorkspaceId;
			}
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x000CDDA0 File Offset: 0x000CBFA0
		public virtual EntitySqlParser CreateEntitySqlParser()
		{
			return new EntitySqlParser(new ModelPerspective(this));
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x000CDDAD File Offset: 0x000CBFAD
		public virtual DbQueryCommandTree CreateQueryCommandTree(DbExpression query)
		{
			return new DbQueryCommandTree(this, DataSpace.CSpace, query);
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x000CDDB7 File Offset: 0x000CBFB7
		public virtual ItemCollection GetItemCollection(DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true);
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000CDDC4 File Offset: 0x000CBFC4
		[Obsolete("Construct MetadataWorkspace using constructor that accepts metadata loading delegates.")]
		public virtual void RegisterItemCollection(ItemCollection collection)
		{
			Check.NotNull<ItemCollection>(collection, "collection");
			try
			{
				switch (collection.DataSpace)
				{
				case DataSpace.OSpace:
					this._itemsOSpace = new Lazy<ObjectItemCollection>(() => (ObjectItemCollection)collection, true);
					if (this._itemsOCSpace == null && this._itemsCSpace != null)
					{
						this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(this._itemsCSpace.Value, this._itemsOSpace.Value));
						goto IL_0217;
					}
					goto IL_0217;
				case DataSpace.CSpace:
				{
					EdmItemCollection edmCollection = (EdmItemCollection)collection;
					if (!MetadataWorkspace.SupportedEdmVersions.Contains(edmCollection.EdmVersion))
					{
						throw new InvalidOperationException(Strings.EdmVersionNotSupportedByRuntime(edmCollection.EdmVersion, Helper.GetCommaDelimitedString(from e in MetadataWorkspace.SupportedEdmVersions
							where e != 0.0
							select e.ToString(CultureInfo.InvariantCulture))));
					}
					this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
					this._itemsCSpace = new Lazy<EdmItemCollection>(() => edmCollection, true);
					if (this._itemsOCSpace == null)
					{
						this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => new DefaultObjectMappingItemCollection(edmCollection, this._itemsOSpace.Value));
						goto IL_0217;
					}
					goto IL_0217;
				}
				case DataSpace.SSpace:
					this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
					this._itemsSSpace = new Lazy<StoreItemCollection>(() => (StoreItemCollection)collection, true);
					goto IL_0217;
				case DataSpace.CSSpace:
					this.CheckAndSetItemCollectionVersionInWorkSpace(collection);
					this._itemsCSSpace = new Lazy<StorageMappingItemCollection>(() => (StorageMappingItemCollection)collection, true);
					goto IL_0217;
				}
				this._itemsOCSpace = new Lazy<DefaultObjectMappingItemCollection>(() => (DefaultObjectMappingItemCollection)collection, true);
				IL_0217:;
			}
			catch (InvalidCastException)
			{
				throw new MetadataException(Strings.InvalidCollectionForMapping(collection.DataSpace.ToString()));
			}
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000CE02C File Offset: 0x000CC22C
		private T LoadAndCheckItemCollection<T>(Func<T> itemCollectionLoader) where T : ItemCollection
		{
			T t = itemCollectionLoader();
			if (t != null)
			{
				this.CheckAndSetItemCollectionVersionInWorkSpace(t);
			}
			return t;
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x000CE058 File Offset: 0x000CC258
		private void CheckAndSetItemCollectionVersionInWorkSpace(ItemCollection itemCollectionToRegister)
		{
			double num = 0.0;
			string text = null;
			switch (itemCollectionToRegister.DataSpace)
			{
			case DataSpace.CSpace:
				num = ((EdmItemCollection)itemCollectionToRegister).EdmVersion;
				text = "EdmItemCollection";
				break;
			case DataSpace.SSpace:
				num = ((StoreItemCollection)itemCollectionToRegister).StoreSchemaVersion;
				text = "StoreItemCollection";
				break;
			case DataSpace.CSSpace:
				num = ((StorageMappingItemCollection)itemCollectionToRegister).MappingVersion;
				text = "StorageMappingItemCollection";
				break;
			}
			object schemaVersionLock = this._schemaVersionLock;
			lock (schemaVersionLock)
			{
				if (num != this._schemaVersion && num != 0.0 && this._schemaVersion != 0.0)
				{
					throw new MetadataException(Strings.DifferentSchemaVersionInCollection(text, num, this._schemaVersion));
				}
				this._schemaVersion = num;
			}
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x000CE148 File Offset: 0x000CC348
		public virtual void LoadFromAssembly(Assembly assembly)
		{
			this.LoadFromAssembly(assembly, null);
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000CE154 File Offset: 0x000CC354
		public virtual void LoadFromAssembly(Assembly assembly, Action<string> logLoadMessage)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			ObjectItemCollection objectItemCollection = (ObjectItemCollection)this.GetItemCollection(DataSpace.OSpace);
			this.ExplicitLoadFromAssembly(assembly, objectItemCollection, logLoadMessage);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000CE184 File Offset: 0x000CC384
		private void ExplicitLoadFromAssembly(Assembly assembly, ObjectItemCollection collection, Action<string> logLoadMessage)
		{
			ItemCollection itemCollection;
			if (!this.TryGetItemCollection(DataSpace.CSpace, out itemCollection))
			{
				itemCollection = null;
			}
			collection.ExplicitLoadFromAssembly(assembly, (EdmItemCollection)itemCollection, logLoadMessage);
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000CE1AC File Offset: 0x000CC3AC
		private void ImplicitLoadFromAssembly(Assembly assembly, ObjectItemCollection collection)
		{
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
			{
				this.ExplicitLoadFromAssembly(assembly, collection, null);
			}
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000CE1C0 File Offset: 0x000CC3C0
		internal virtual void ImplicitLoadAssemblyForType(Type type, Assembly callingAssembly)
		{
			ItemCollection itemCollection;
			if (this.TryGetItemCollection(DataSpace.OSpace, out itemCollection))
			{
				ObjectItemCollection objectItemCollection = (ObjectItemCollection)itemCollection;
				ItemCollection itemCollection2;
				this.TryGetItemCollection(DataSpace.CSpace, out itemCollection2);
				EdmItemCollection edmItemCollection = (EdmItemCollection)itemCollection2;
				if (!objectItemCollection.ImplicitLoadAssemblyForType(type, edmItemCollection) && null != callingAssembly)
				{
					if (ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(callingAssembly) || this._foundAssemblyWithAttribute || MetadataAssemblyHelper.GetNonSystemReferencedAssemblies(callingAssembly).Any(new Func<Assembly, bool>(ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent)))
					{
						this._foundAssemblyWithAttribute = true;
						objectItemCollection.ImplicitLoadAllReferencedAssemblies(callingAssembly, edmItemCollection);
						return;
					}
					this.ImplicitLoadFromAssembly(callingAssembly, objectItemCollection);
				}
			}
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000CE244 File Offset: 0x000CC444
		internal virtual void ImplicitLoadFromEntityType(EntityType type, Assembly callingAssembly)
		{
			MappingBase mappingBase;
			if (!this.TryGetMap(type, DataSpace.OCSpace, out mappingBase))
			{
				this.ImplicitLoadAssemblyForType(typeof(IEntityWithKey), callingAssembly);
				ObjectItemCollection objectItemCollection = this.GetItemCollection(DataSpace.OSpace) as ObjectItemCollection;
				EdmType edmType;
				if (objectItemCollection == null || !objectItemCollection.TryGetOSpaceType(type, out edmType))
				{
					throw new InvalidOperationException(Strings.Mapping_Object_InvalidType(type.Identity));
				}
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x000CE29A File Offset: 0x000CC49A
		public virtual T GetItem<T>(string identity, DataSpace dataSpace) where T : GlobalItem
		{
			return this.GetItemCollection(dataSpace, true).GetItem<T>(identity, false);
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x000CE2AC File Offset: 0x000CC4AC
		public virtual bool TryGetItem<T>(string identity, DataSpace space, out T item) where T : GlobalItem
		{
			item = default(T);
			ItemCollection itemCollection = this.GetItemCollection(space, false);
			return itemCollection != null && itemCollection.TryGetItem<T>(identity, false, out item);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000CE2D7 File Offset: 0x000CC4D7
		public virtual T GetItem<T>(string identity, bool ignoreCase, DataSpace dataSpace) where T : GlobalItem
		{
			return this.GetItemCollection(dataSpace, true).GetItem<T>(identity, ignoreCase);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000CE2E8 File Offset: 0x000CC4E8
		public virtual bool TryGetItem<T>(string identity, bool ignoreCase, DataSpace dataSpace, out T item) where T : GlobalItem
		{
			item = default(T);
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetItem<T>(identity, ignoreCase, out item);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000CE315 File Offset: 0x000CC515
		public virtual ReadOnlyCollection<T> GetItems<T>(DataSpace dataSpace) where T : GlobalItem
		{
			return this.GetItemCollection(dataSpace, true).GetItems<T>();
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x000CE324 File Offset: 0x000CC524
		public virtual EdmType GetType(string name, string namespaceName, DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true).GetType(name, namespaceName, false);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000CE338 File Offset: 0x000CC538
		public virtual bool TryGetType(string name, string namespaceName, DataSpace dataSpace, out EdmType type)
		{
			type = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetType(name, namespaceName, false, out type);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x000CE362 File Offset: 0x000CC562
		public virtual EdmType GetType(string name, string namespaceName, bool ignoreCase, DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true).GetType(name, namespaceName, ignoreCase);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000CE378 File Offset: 0x000CC578
		public virtual bool TryGetType(string name, string namespaceName, bool ignoreCase, DataSpace dataSpace, out EdmType type)
		{
			type = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetType(name, namespaceName, ignoreCase, out type);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000CE3A3 File Offset: 0x000CC5A3
		public virtual EntityContainer GetEntityContainer(string name, DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true).GetEntityContainer(name);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000CE3B4 File Offset: 0x000CC5B4
		public virtual bool TryGetEntityContainer(string name, DataSpace dataSpace, out EntityContainer entityContainer)
		{
			entityContainer = null;
			Check.NotNull<string>(name, "name");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetEntityContainer(name, out entityContainer);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000CE3E6 File Offset: 0x000CC5E6
		public virtual EntityContainer GetEntityContainer(string name, bool ignoreCase, DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true).GetEntityContainer(name, ignoreCase);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000CE3F8 File Offset: 0x000CC5F8
		public virtual bool TryGetEntityContainer(string name, bool ignoreCase, DataSpace dataSpace, out EntityContainer entityContainer)
		{
			entityContainer = null;
			Check.NotNull<string>(name, "name");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetEntityContainer(name, ignoreCase, out entityContainer);
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x000CE42D File Offset: 0x000CC62D
		public virtual ReadOnlyCollection<EdmFunction> GetFunctions(string name, string namespaceName, DataSpace dataSpace)
		{
			return this.GetFunctions(name, namespaceName, dataSpace, false);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x000CE439 File Offset: 0x000CC639
		public virtual ReadOnlyCollection<EdmFunction> GetFunctions(string name, string namespaceName, DataSpace dataSpace, bool ignoreCase)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			return this.GetItemCollection(dataSpace, true).GetFunctions(namespaceName + "." + name, ignoreCase);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000CE470 File Offset: 0x000CC670
		internal virtual bool TryGetFunction(string name, string namespaceName, TypeUsage[] parameterTypes, bool ignoreCase, DataSpace dataSpace, out EdmFunction function)
		{
			function = null;
			Check.NotNull<string>(name, "name");
			Check.NotNull<string>(namespaceName, "namespaceName");
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && itemCollection.TryGetFunction(namespaceName + "." + name, parameterTypes, ignoreCase, out function);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000CE4BF File Offset: 0x000CC6BF
		public virtual ReadOnlyCollection<PrimitiveType> GetPrimitiveTypes(DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true).GetItems<PrimitiveType>();
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000CE4CE File Offset: 0x000CC6CE
		public virtual ReadOnlyCollection<GlobalItem> GetItems(DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true).GetItems<GlobalItem>();
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000CE4DD File Offset: 0x000CC6DD
		internal virtual PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind, DataSpace dataSpace)
		{
			return this.GetItemCollection(dataSpace, true).GetMappedPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000CE4F0 File Offset: 0x000CC6F0
		internal virtual bool TryGetMap(string typeIdentity, DataSpace typeSpace, bool ignoreCase, DataSpace mappingSpace, out MappingBase map)
		{
			map = null;
			ItemCollection itemCollection = this.GetItemCollection(mappingSpace, false);
			return itemCollection != null && ((MappingItemCollection)itemCollection).TryGetMap(typeIdentity, typeSpace, ignoreCase, out map);
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000CE520 File Offset: 0x000CC720
		internal virtual MappingBase GetMap(string identity, DataSpace typeSpace, DataSpace dataSpace)
		{
			return ((MappingItemCollection)this.GetItemCollection(dataSpace, true)).GetMap(identity, typeSpace);
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x000CE536 File Offset: 0x000CC736
		internal virtual MappingBase GetMap(GlobalItem item, DataSpace dataSpace)
		{
			return ((MappingItemCollection)this.GetItemCollection(dataSpace, true)).GetMap(item);
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x000CE54C File Offset: 0x000CC74C
		internal virtual bool TryGetMap(GlobalItem item, DataSpace dataSpace, out MappingBase map)
		{
			map = null;
			ItemCollection itemCollection = this.GetItemCollection(dataSpace, false);
			return itemCollection != null && ((MappingItemCollection)itemCollection).TryGetMap(item, out map);
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000CE577 File Offset: 0x000CC777
		public virtual bool TryGetItemCollection(DataSpace dataSpace, out ItemCollection collection)
		{
			collection = this.GetItemCollection(dataSpace, false);
			return collection != null;
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x000CE588 File Offset: 0x000CC788
		internal virtual ItemCollection GetItemCollection(DataSpace dataSpace, bool required)
		{
			ItemCollection itemCollection;
			switch (dataSpace)
			{
			case DataSpace.OSpace:
				itemCollection = this._itemsOSpace.Value;
				break;
			case DataSpace.CSpace:
				itemCollection = ((this._itemsCSpace == null) ? null : this._itemsCSpace.Value);
				break;
			case DataSpace.SSpace:
				itemCollection = ((this._itemsSSpace == null) ? null : this._itemsSSpace.Value);
				break;
			case DataSpace.OCSpace:
				itemCollection = ((this._itemsOCSpace == null) ? null : this._itemsOCSpace.Value);
				break;
			case DataSpace.CSSpace:
				itemCollection = ((this._itemsCSSpace == null) ? null : this._itemsCSSpace.Value);
				break;
			default:
				itemCollection = null;
				break;
			}
			if (required && itemCollection == null)
			{
				throw new InvalidOperationException(Strings.NoCollectionForSpace(dataSpace.ToString()));
			}
			return itemCollection;
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000CE646 File Offset: 0x000CC846
		public virtual StructuralType GetObjectSpaceType(StructuralType edmSpaceType)
		{
			return this.GetObjectSpaceType<StructuralType>(edmSpaceType);
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000CE64F File Offset: 0x000CC84F
		public virtual bool TryGetObjectSpaceType(StructuralType edmSpaceType, out StructuralType objectSpaceType)
		{
			return this.TryGetObjectSpaceType<StructuralType>(edmSpaceType, out objectSpaceType);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x000CE659 File Offset: 0x000CC859
		public virtual EnumType GetObjectSpaceType(EnumType edmSpaceType)
		{
			return this.GetObjectSpaceType<EnumType>(edmSpaceType);
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x000CE662 File Offset: 0x000CC862
		public virtual bool TryGetObjectSpaceType(EnumType edmSpaceType, out EnumType objectSpaceType)
		{
			return this.TryGetObjectSpaceType<EnumType>(edmSpaceType, out objectSpaceType);
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x000CE66C File Offset: 0x000CC86C
		private T GetObjectSpaceType<T>(T edmSpaceType) where T : EdmType
		{
			T t;
			if (!this.TryGetObjectSpaceType<T>(edmSpaceType, out t))
			{
				throw new ArgumentException(Strings.FailedToFindOSpaceTypeMapping(edmSpaceType.Identity));
			}
			return t;
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x000CE69C File Offset: 0x000CC89C
		private bool TryGetObjectSpaceType<T>(T edmSpaceType, out T objectSpaceType) where T : EdmType
		{
			if (edmSpaceType.DataSpace != DataSpace.CSpace)
			{
				throw new ArgumentException(Strings.ArgumentMustBeCSpaceType, "edmSpaceType");
			}
			objectSpaceType = default(T);
			MappingBase mappingBase;
			if (this.TryGetMap(edmSpaceType, DataSpace.OCSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = mappingBase as ObjectTypeMapping;
				if (objectTypeMapping != null)
				{
					objectSpaceType = (T)((object)objectTypeMapping.ClrType);
				}
			}
			return objectSpaceType != null;
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x000CE708 File Offset: 0x000CC908
		public virtual StructuralType GetEdmSpaceType(StructuralType objectSpaceType)
		{
			return this.GetEdmSpaceType<StructuralType>(objectSpaceType);
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x000CE711 File Offset: 0x000CC911
		public virtual bool TryGetEdmSpaceType(StructuralType objectSpaceType, out StructuralType edmSpaceType)
		{
			return this.TryGetEdmSpaceType<StructuralType>(objectSpaceType, out edmSpaceType);
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x000CE71B File Offset: 0x000CC91B
		public virtual EnumType GetEdmSpaceType(EnumType objectSpaceType)
		{
			return this.GetEdmSpaceType<EnumType>(objectSpaceType);
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000CE724 File Offset: 0x000CC924
		public virtual bool TryGetEdmSpaceType(EnumType objectSpaceType, out EnumType edmSpaceType)
		{
			return this.TryGetEdmSpaceType<EnumType>(objectSpaceType, out edmSpaceType);
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x000CE730 File Offset: 0x000CC930
		private T GetEdmSpaceType<T>(T objectSpaceType) where T : EdmType
		{
			T t;
			if (!this.TryGetEdmSpaceType<T>(objectSpaceType, out t))
			{
				throw new ArgumentException(Strings.FailedToFindCSpaceTypeMapping(objectSpaceType.Identity));
			}
			return t;
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x000CE760 File Offset: 0x000CC960
		private bool TryGetEdmSpaceType<T>(T objectSpaceType, out T edmSpaceType) where T : EdmType
		{
			if (objectSpaceType.DataSpace != DataSpace.OSpace)
			{
				throw new ArgumentException(Strings.ArgumentMustBeOSpaceType, "objectSpaceType");
			}
			edmSpaceType = default(T);
			MappingBase mappingBase;
			if (this.TryGetMap(objectSpaceType, DataSpace.OCSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = mappingBase as ObjectTypeMapping;
				if (objectTypeMapping != null)
				{
					edmSpaceType = (T)((object)objectTypeMapping.EdmType);
				}
			}
			return edmSpaceType != null;
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x000CE7CB File Offset: 0x000CC9CB
		internal virtual DbQueryCommandTree GetCqtView(EntitySetBase extent)
		{
			return this.GetGeneratedView(extent).GetCommandTree();
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x000CE7D9 File Offset: 0x000CC9D9
		internal virtual GeneratedView GetGeneratedView(EntitySetBase extent)
		{
			return ((StorageMappingItemCollection)this.GetItemCollection(DataSpace.CSSpace, true)).GetGeneratedView(extent, this);
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x000CE7EF File Offset: 0x000CC9EF
		internal virtual bool TryGetGeneratedViewOfType(EntitySetBase extent, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
		{
			return ((StorageMappingItemCollection)this.GetItemCollection(DataSpace.CSSpace, true)).TryGetGeneratedViewOfType(extent, type, includeSubtypes, out generatedView);
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x000CE808 File Offset: 0x000CCA08
		internal virtual DbLambda GetGeneratedFunctionDefinition(EdmFunction function)
		{
			return ((EdmItemCollection)this.GetItemCollection(DataSpace.CSpace, true)).GetGeneratedFunctionDefinition(function);
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x000CE820 File Offset: 0x000CCA20
		internal virtual bool TryGetFunctionImportMapping(EdmFunction functionImport, out FunctionImportMapping targetFunctionMapping)
		{
			using (IEnumerator<EntityContainerMapping> enumerator = this.GetItems<EntityContainerMapping>(DataSpace.CSSpace).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.TryGetFunctionImportMapping(functionImport, out targetFunctionMapping))
					{
						return true;
					}
				}
			}
			targetFunctionMapping = null;
			return false;
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x000CE87C File Offset: 0x000CCA7C
		internal virtual ViewLoader GetUpdateViewLoader()
		{
			if (this._itemsCSSpace == null || this._itemsCSSpace.Value == null)
			{
				return null;
			}
			return this._itemsCSSpace.Value.GetUpdateViewLoader();
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x000CE8A8 File Offset: 0x000CCAA8
		internal virtual TypeUsage GetOSpaceTypeUsage(TypeUsage edmSpaceTypeUsage)
		{
			EdmType edmType;
			if (Helper.IsPrimitiveType(edmSpaceTypeUsage.EdmType))
			{
				edmType = this.GetItemCollection(DataSpace.OSpace, true).GetMappedPrimitiveType(((PrimitiveType)edmSpaceTypeUsage.EdmType).PrimitiveTypeKind);
			}
			else
			{
				edmType = ((ObjectTypeMapping)((DefaultObjectMappingItemCollection)this.GetItemCollection(DataSpace.OCSpace, true)).GetMap(edmSpaceTypeUsage.EdmType)).ClrType;
			}
			return TypeUsage.Create(edmType, edmSpaceTypeUsage.Facets);
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x000CE914 File Offset: 0x000CCB14
		internal virtual bool IsItemCollectionAlreadyRegistered(DataSpace dataSpace)
		{
			ItemCollection itemCollection;
			return this.TryGetItemCollection(dataSpace, out itemCollection);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x000CE92A File Offset: 0x000CCB2A
		internal virtual bool IsMetadataWorkspaceCSCompatible(MetadataWorkspace other)
		{
			return this.GetItemCollection(DataSpace.CSSpace, false).MetadataEquals(other.GetItemCollection(DataSpace.CSSpace, false));
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x000CE944 File Offset: 0x000CCB44
		public static void ClearCache()
		{
			MetadataCache.Instance.Clear();
			using (LockedAssemblyCache lockedAssemblyCache = AssemblyCache.AcquireLockedAssemblyCache())
			{
				lockedAssemblyCache.Clear();
			}
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x000CE984 File Offset: 0x000CCB84
		internal static TypeUsage GetCanonicalModelTypeUsage(PrimitiveTypeKind primitiveTypeKind)
		{
			return EdmProviderManifest.Instance.GetCanonicalModelTypeUsage(primitiveTypeKind);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x000CE991 File Offset: 0x000CCB91
		internal static PrimitiveType GetModelPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			return EdmProviderManifest.Instance.GetPrimitiveType(primitiveTypeKind);
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x000CE99E File Offset: 0x000CCB9E
		[Obsolete("Use MetadataWorkspace.GetRelevantMembersForUpdate(EntitySetBase, EntityTypeBase, bool) instead")]
		public virtual IEnumerable<EdmMember> GetRequiredOriginalValueMembers(EntitySetBase entitySet, EntityTypeBase entityType)
		{
			return this.GetInterestingMembers(entitySet, entityType, StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers);
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x000CE9A9 File Offset: 0x000CCBA9
		public virtual ReadOnlyCollection<EdmMember> GetRelevantMembersForUpdate(EntitySetBase entitySet, EntityTypeBase entityType, bool partialUpdateSupported)
		{
			return this.GetInterestingMembers(entitySet, entityType, partialUpdateSupported ? StorageMappingItemCollection.InterestingMembersKind.PartialUpdate : StorageMappingItemCollection.InterestingMembersKind.FullUpdate);
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000CE9BC File Offset: 0x000CCBBC
		private ReadOnlyCollection<EdmMember> GetInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			AssociationSet associationSet = entitySet as AssociationSet;
			if (entitySet.EntityContainer.DataSpace != DataSpace.CSpace)
			{
				throw new ArgumentException(Strings.EntitySetNotInCSPace(entitySet.Name));
			}
			if (entitySet.ElementType.IsAssignableFrom(entityType))
			{
				return ((StorageMappingItemCollection)this.GetItemCollection(DataSpace.CSSpace, true)).GetInterestingMembers(entitySet, entityType, interestingMembersKind);
			}
			if (associationSet != null)
			{
				throw new ArgumentException(Strings.TypeNotInAssociationSet(entityType.FullName, entitySet.ElementType.FullName, entitySet.Name));
			}
			throw new ArgumentException(Strings.TypeNotInEntitySet(entityType.FullName, entitySet.ElementType.FullName, entitySet.Name));
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x000CEA5B File Offset: 0x000CCC5B
		internal virtual QueryCacheManager GetQueryCacheManager()
		{
			return this._itemsSSpace.Value.QueryCacheManager;
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x000CEA6D File Offset: 0x000CCC6D
		internal bool TryDetermineCSpaceModelType<T>(out EdmType modelEdmType)
		{
			return this.TryDetermineCSpaceModelType(typeof(T), out modelEdmType);
		}

		// Token: 0x06003E35 RID: 15925 RVA: 0x000CEA80 File Offset: 0x000CCC80
		internal virtual bool TryDetermineCSpaceModelType(Type type, out EdmType modelEdmType)
		{
			Type nonNullableType = TypeSystem.GetNonNullableType(type);
			this.ImplicitLoadAssemblyForType(nonNullableType, Assembly.GetCallingAssembly());
			EdmType edmType;
			MappingBase mappingBase;
			if (((ObjectItemCollection)this.GetItemCollection(DataSpace.OSpace)).TryGetItem<EdmType>(nonNullableType.FullNameWithNesting(), out edmType) && this.TryGetMap(edmType, DataSpace.OCSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = (ObjectTypeMapping)mappingBase;
				modelEdmType = objectTypeMapping.EdmType;
				return true;
			}
			modelEdmType = null;
			return false;
		}

		// Token: 0x04001518 RID: 5400
		private Lazy<EdmItemCollection> _itemsCSpace;

		// Token: 0x04001519 RID: 5401
		private Lazy<StoreItemCollection> _itemsSSpace;

		// Token: 0x0400151A RID: 5402
		private Lazy<ObjectItemCollection> _itemsOSpace;

		// Token: 0x0400151B RID: 5403
		private Lazy<StorageMappingItemCollection> _itemsCSSpace;

		// Token: 0x0400151C RID: 5404
		private Lazy<DefaultObjectMappingItemCollection> _itemsOCSpace;

		// Token: 0x0400151D RID: 5405
		private bool _foundAssemblyWithAttribute;

		// Token: 0x0400151E RID: 5406
		private double _schemaVersion;

		// Token: 0x0400151F RID: 5407
		private readonly object _schemaVersionLock = new object();

		// Token: 0x04001520 RID: 5408
		private readonly Guid _metadataWorkspaceId = Guid.NewGuid();

		// Token: 0x04001521 RID: 5409
		internal readonly MetadataOptimization MetadataOptimization;

		// Token: 0x04001522 RID: 5410
		private static readonly double _maximumEdmVersionSupported = MetadataWorkspace.SupportedEdmVersions.Last<double>();
	}
}
