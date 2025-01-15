using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E6 RID: 1254
	public class ObjectItemCollection : ItemCollection
	{
		// Token: 0x06003E7B RID: 15995 RVA: 0x000D0138 File Offset: 0x000CE338
		public ObjectItemCollection()
			: this(null)
		{
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x000D0144 File Offset: 0x000CE344
		internal ObjectItemCollection(KnownAssembliesSet knownAssembliesSet = null)
			: base(DataSpace.OSpace)
		{
			this._knownAssemblies = knownAssembliesSet ?? new KnownAssembliesSet();
			foreach (PrimitiveType primitiveType in ClrProviderManifest.Instance.GetStoreTypes())
			{
				base.AddInternal(primitiveType);
				this._primitiveTypeMaps.Add(primitiveType);
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003E7D RID: 15997 RVA: 0x000D01E4 File Offset: 0x000CE3E4
		// (set) Token: 0x06003E7E RID: 15998 RVA: 0x000D01EC File Offset: 0x000CE3EC
		internal bool OSpaceTypesLoaded { get; set; }

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06003E7F RID: 15999 RVA: 0x000D01F5 File Offset: 0x000CE3F5
		internal object LoadAssemblyLock
		{
			get
			{
				return this._loadAssemblyLock;
			}
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x000D01FD File Offset: 0x000CE3FD
		internal void ImplicitLoadAllReferencedAssemblies(Assembly assembly, EdmItemCollection edmItemCollection)
		{
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(assembly))
			{
				this.LoadAssemblyFromCache(assembly, true, edmItemCollection, null);
			}
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000D0212 File Offset: 0x000CE412
		public void LoadFromAssembly(Assembly assembly)
		{
			this.ExplicitLoadFromAssembly(assembly, null, null);
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000D021D File Offset: 0x000CE41D
		public void LoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			Check.NotNull<EdmItemCollection>(edmItemCollection, "edmItemCollection");
			Check.NotNull<Action<string>>(logLoadMessage, "logLoadMessage");
			this.ExplicitLoadFromAssembly(assembly, edmItemCollection, logLoadMessage);
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x000D024C File Offset: 0x000CE44C
		public void LoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			Check.NotNull<EdmItemCollection>(edmItemCollection, "edmItemCollection");
			this.ExplicitLoadFromAssembly(assembly, edmItemCollection, null);
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x000D026F File Offset: 0x000CE46F
		internal void ExplicitLoadFromAssembly(Assembly assembly, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			this.LoadAssemblyFromCache(assembly, false, edmItemCollection, logLoadMessage);
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x000D027C File Offset: 0x000CE47C
		internal bool ImplicitLoadAssemblyForType(Type type, EdmItemCollection edmItemCollection)
		{
			bool flag = false;
			if (!MetadataAssemblyHelper.ShouldFilterAssembly(type.Assembly()))
			{
				flag = this.LoadAssemblyFromCache(type.Assembly(), false, edmItemCollection, null);
			}
			if (type.IsGenericType())
			{
				foreach (Type type2 in type.GetGenericArguments())
				{
					flag |= this.ImplicitLoadAssemblyForType(type2, edmItemCollection);
				}
			}
			return flag;
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x000D02D8 File Offset: 0x000CE4D8
		internal AssociationType GetRelationshipType(string relationshipName)
		{
			AssociationType associationType;
			if (base.TryGetItem<AssociationType>(relationshipName, out associationType))
			{
				return associationType;
			}
			return null;
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x000D02F4 File Offset: 0x000CE4F4
		private bool LoadAssemblyFromCache(Assembly assembly, bool loadReferencedAssemblies, EdmItemCollection edmItemCollection, Action<string> logLoadMessage)
		{
			if (this.OSpaceTypesLoaded)
			{
				return true;
			}
			object obj;
			if (edmItemCollection != null)
			{
				ReadOnlyCollection<EntityContainer> items = edmItemCollection.GetItems<EntityContainer>();
				if (items.Any<EntityContainer>())
				{
					if (items.All((EntityContainer c) => c.Annotations.Any((MetadataProperty a) => a.Name == "http://schemas.microsoft.com/ado/2013/11/edm/customannotation:UseClrTypes" && ((string)a.Value).ToUpperInvariant() == "TRUE")))
					{
						obj = this.LoadAssemblyLock;
						lock (obj)
						{
							if (!this.OSpaceTypesLoaded)
							{
								new CodeFirstOSpaceLoader(null).LoadTypes(edmItemCollection, this);
							}
							return true;
						}
					}
				}
			}
			KnownAssemblyEntry knownAssemblyEntry;
			if (this._knownAssemblies.TryGetKnownAssembly(assembly, this._loaderCookie, edmItemCollection, out knownAssemblyEntry))
			{
				if (!loadReferencedAssemblies)
				{
					return knownAssemblyEntry.CacheEntry.TypesInAssembly.Count != 0;
				}
				if (knownAssemblyEntry.ReferencedAssembliesAreLoaded)
				{
					return true;
				}
			}
			obj = this.LoadAssemblyLock;
			bool flag2;
			lock (obj)
			{
				if (this._knownAssemblies.TryGetKnownAssembly(assembly, this._loaderCookie, edmItemCollection, out knownAssemblyEntry) && (!loadReferencedAssemblies || knownAssemblyEntry.ReferencedAssembliesAreLoaded))
				{
					flag2 = true;
				}
				else
				{
					KnownAssembliesSet knownAssembliesSet = new KnownAssembliesSet(this._knownAssemblies);
					Dictionary<string, EdmType> dictionary;
					List<EdmItemError> list;
					AssemblyCache.LoadAssembly(assembly, loadReferencedAssemblies, knownAssembliesSet, edmItemCollection, logLoadMessage, ref this._loaderCookie, out dictionary, out list);
					if (list.Count != 0)
					{
						throw EntityUtil.InvalidSchemaEncountered(Helper.CombineErrorMessage(list));
					}
					if (dictionary.Count != 0)
					{
						this.AddLoadedTypes(dictionary);
					}
					this._knownAssemblies = knownAssembliesSet;
					flag2 = dictionary.Count != 0;
				}
			}
			return flag2;
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x000D0474 File Offset: 0x000CE674
		internal virtual void AddLoadedTypes(Dictionary<string, EdmType> typesInLoading)
		{
			List<GlobalItem> list = new List<GlobalItem>();
			foreach (EdmType edmType in typesInLoading.Values)
			{
				list.Add(edmType);
				string text = "";
				try
				{
					if (Helper.IsEntityType(edmType))
					{
						text = ((ClrEntityType)edmType).CSpaceTypeName;
						this._ocMapping.Add(text, edmType);
					}
					else if (Helper.IsComplexType(edmType))
					{
						text = ((ClrComplexType)edmType).CSpaceTypeName;
						this._ocMapping.Add(text, edmType);
					}
					else if (Helper.IsEnumType(edmType))
					{
						text = ((ClrEnumType)edmType).CSpaceTypeName;
						this._ocMapping.Add(text, edmType);
					}
				}
				catch (ArgumentException ex)
				{
					throw new MappingException(Strings.Mapping_CannotMapCLRTypeMultipleTimes(text), ex);
				}
			}
			base.AddRange(list);
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x000D0568 File Offset: 0x000CE768
		public IEnumerable<PrimitiveType> GetPrimitiveTypes()
		{
			return this._primitiveTypeMaps.GetTypes();
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x000D0575 File Offset: 0x000CE775
		public Type GetClrType(StructuralType objectSpaceType)
		{
			return ObjectItemCollection.GetClrType(objectSpaceType);
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000D057D File Offset: 0x000CE77D
		public bool TryGetClrType(StructuralType objectSpaceType, out Type clrType)
		{
			return ObjectItemCollection.TryGetClrType(objectSpaceType, out clrType);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x000D0586 File Offset: 0x000CE786
		public Type GetClrType(EnumType objectSpaceType)
		{
			return ObjectItemCollection.GetClrType(objectSpaceType);
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x000D058E File Offset: 0x000CE78E
		public bool TryGetClrType(EnumType objectSpaceType, out Type clrType)
		{
			return ObjectItemCollection.TryGetClrType(objectSpaceType, out clrType);
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x000D0598 File Offset: 0x000CE798
		private static Type GetClrType(EdmType objectSpaceType)
		{
			Type type;
			if (!ObjectItemCollection.TryGetClrType(objectSpaceType, out type))
			{
				throw new ArgumentException(Strings.FailedToFindClrTypeMapping(objectSpaceType.Identity));
			}
			return type;
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x000D05C4 File Offset: 0x000CE7C4
		private static bool TryGetClrType(EdmType objectSpaceType, out Type clrType)
		{
			if (objectSpaceType.DataSpace != DataSpace.OSpace)
			{
				throw new ArgumentException(Strings.ArgumentMustBeOSpaceType, "objectSpaceType");
			}
			clrType = null;
			if (Helper.IsEntityType(objectSpaceType) || Helper.IsComplexType(objectSpaceType) || Helper.IsEnumType(objectSpaceType))
			{
				clrType = objectSpaceType.ClrType;
			}
			return clrType != null;
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x000D0614 File Offset: 0x000CE814
		internal override PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind modelType)
		{
			if (Helper.IsGeometricTypeKind(modelType))
			{
				modelType = PrimitiveTypeKind.Geometry;
			}
			else if (Helper.IsGeographicTypeKind(modelType))
			{
				modelType = PrimitiveTypeKind.Geography;
			}
			PrimitiveType primitiveType = null;
			this._primitiveTypeMaps.TryGetType(modelType, null, out primitiveType);
			return primitiveType;
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x000D064E File Offset: 0x000CE84E
		internal bool TryGetOSpaceType(EdmType cspaceType, out EdmType edmType)
		{
			if (Helper.IsEntityType(cspaceType) || Helper.IsComplexType(cspaceType) || Helper.IsEnumType(cspaceType))
			{
				return this._ocMapping.TryGetValue(cspaceType.Identity, out edmType);
			}
			return base.TryGetItem<EdmType>(cspaceType.Identity, out edmType);
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x000D0688 File Offset: 0x000CE888
		internal static string TryGetMappingCSpaceTypeIdentity(EdmType edmType)
		{
			if (Helper.IsEntityType(edmType))
			{
				return ((ClrEntityType)edmType).CSpaceTypeName;
			}
			if (Helper.IsComplexType(edmType))
			{
				return ((ClrComplexType)edmType).CSpaceTypeName;
			}
			if (Helper.IsEnumType(edmType))
			{
				return ((ClrEnumType)edmType).CSpaceTypeName;
			}
			return edmType.Identity;
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x000D06D7 File Offset: 0x000CE8D7
		public override ReadOnlyCollection<T> GetItems<T>()
		{
			return base.InternalGetItems(typeof(T)) as ReadOnlyCollection<T>;
		}

		// Token: 0x04001531 RID: 5425
		private readonly CacheForPrimitiveTypes _primitiveTypeMaps = new CacheForPrimitiveTypes();

		// Token: 0x04001532 RID: 5426
		private KnownAssembliesSet _knownAssemblies = new KnownAssembliesSet();

		// Token: 0x04001533 RID: 5427
		private readonly Dictionary<string, EdmType> _ocMapping = new Dictionary<string, EdmType>();

		// Token: 0x04001534 RID: 5428
		private object _loaderCookie;

		// Token: 0x04001535 RID: 5429
		private readonly object _loadAssemblyLock = new object();
	}
}
