using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000515 RID: 1301
	internal class ObjectItemLoadingSessionData
	{
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004001 RID: 16385 RVA: 0x000D5959 File Offset: 0x000D3B59
		internal virtual Dictionary<string, EdmType> TypesInLoading
		{
			get
			{
				return this._typesInLoading;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x000D5961 File Offset: 0x000D3B61
		internal Dictionary<Assembly, MutableAssemblyCacheEntry> AssembliesLoaded
		{
			get
			{
				return this._listOfAssembliesLoaded;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x000D5969 File Offset: 0x000D3B69
		internal virtual List<EdmItemError> EdmItemErrors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000D5971 File Offset: 0x000D3B71
		internal KnownAssembliesSet KnownAssemblies
		{
			get
			{
				return this._knownAssemblies;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x000D5979 File Offset: 0x000D3B79
		internal LockedAssemblyCache LockedAssemblyCache
		{
			get
			{
				return this._lockedAssemblyCache;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x000D5981 File Offset: 0x000D3B81
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this._edmItemCollection;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004007 RID: 16391 RVA: 0x000D5989 File Offset: 0x000D3B89
		internal virtual Dictionary<EdmType, EdmType> CspaceToOspace
		{
			get
			{
				return this._cspaceToOspace;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x000D5991 File Offset: 0x000D3B91
		// (set) Token: 0x06004009 RID: 16393 RVA: 0x000D5999 File Offset: 0x000D3B99
		internal bool ConventionBasedRelationshipsAreLoaded { get; set; }

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x000D59A2 File Offset: 0x000D3BA2
		internal virtual LoadMessageLogger LoadMessageLogger
		{
			get
			{
				return this._loadMessageLogger;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600400B RID: 16395 RVA: 0x000D59AC File Offset: 0x000D3BAC
		internal Dictionary<string, KeyValuePair<EdmType, int>> ConventionCSpaceTypeNames
		{
			get
			{
				if (this._edmItemCollection != null && this._conventionCSpaceTypeNames == null)
				{
					this._conventionCSpaceTypeNames = new Dictionary<string, KeyValuePair<EdmType, int>>();
					foreach (EdmType edmType in this._edmItemCollection.GetItems<EdmType>())
					{
						if ((edmType is StructuralType && edmType.BuiltInTypeKind != BuiltInTypeKind.AssociationType) || Helper.IsEnumType(edmType))
						{
							KeyValuePair<EdmType, int> keyValuePair;
							if (this._conventionCSpaceTypeNames.TryGetValue(edmType.Name, out keyValuePair))
							{
								this._conventionCSpaceTypeNames[edmType.Name] = new KeyValuePair<EdmType, int>(keyValuePair.Key, keyValuePair.Value + 1);
							}
							else
							{
								keyValuePair = new KeyValuePair<EdmType, int>(edmType, 1);
								this._conventionCSpaceTypeNames.Add(edmType.Name, keyValuePair);
							}
						}
					}
				}
				return this._conventionCSpaceTypeNames;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000D5A90 File Offset: 0x000D3C90
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x000D5A98 File Offset: 0x000D3C98
		internal Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> ObjectItemAssemblyLoaderFactory
		{
			get
			{
				return this._loaderFactory;
			}
			set
			{
				if (this._loaderFactory != value)
				{
					this._loaderFactory = value;
				}
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x000D5AAF File Offset: 0x000D3CAF
		internal object LoaderCookie
		{
			get
			{
				if (this._originalLoaderCookie != null)
				{
					return this._originalLoaderCookie;
				}
				return this._loaderFactory;
			}
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x000D5AC6 File Offset: 0x000D3CC6
		internal ObjectItemLoadingSessionData()
		{
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x000D5AF0 File Offset: 0x000D3CF0
		internal ObjectItemLoadingSessionData(KnownAssembliesSet knownAssemblies, LockedAssemblyCache lockedAssemblyCache, EdmItemCollection edmItemCollection, Action<string> logLoadMessage, object loaderCookie)
		{
			this._typesInLoading = new Dictionary<string, EdmType>(StringComparer.Ordinal);
			this._errors = new List<EdmItemError>();
			this._knownAssemblies = knownAssemblies;
			this._lockedAssemblyCache = lockedAssemblyCache;
			this._edmItemCollection = edmItemCollection;
			this._loadMessageLogger = new LoadMessageLogger(logLoadMessage);
			this._cspaceToOspace = new Dictionary<EdmType, EdmType>();
			this._loaderFactory = (Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>)loaderCookie;
			this._originalLoaderCookie = loaderCookie;
			if (this._loaderFactory == new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create) && this._edmItemCollection != null)
			{
				foreach (KnownAssemblyEntry knownAssemblyEntry in this._knownAssemblies.GetEntries(this._loaderFactory, edmItemCollection))
				{
					foreach (EdmType edmType in knownAssemblyEntry.CacheEntry.TypesInAssembly.OfType<EdmType>())
					{
						if (Helper.IsEntityType(edmType))
						{
							ClrEntityType clrEntityType = (ClrEntityType)edmType;
							this._cspaceToOspace.Add(this._edmItemCollection.GetItem<StructuralType>(clrEntityType.CSpaceTypeName), clrEntityType);
						}
						else if (Helper.IsComplexType(edmType))
						{
							ClrComplexType clrComplexType = (ClrComplexType)edmType;
							this._cspaceToOspace.Add(this._edmItemCollection.GetItem<StructuralType>(clrComplexType.CSpaceTypeName), clrComplexType);
						}
						else if (Helper.IsEnumType(edmType))
						{
							ClrEnumType clrEnumType = (ClrEnumType)edmType;
							this._cspaceToOspace.Add(this._edmItemCollection.GetItem<EnumType>(clrEnumType.CSpaceTypeName), clrEnumType);
						}
						else
						{
							this._cspaceToOspace.Add(this._edmItemCollection.GetItem<StructuralType>(edmType.FullName), edmType);
						}
					}
				}
			}
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x000D5CE4 File Offset: 0x000D3EE4
		internal void RegisterForLevel1PostSessionProcessing(ObjectItemAssemblyLoader loader)
		{
			this._loadersThatNeedLevel1PostSessionProcessing.Add(loader);
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x000D5CF3 File Offset: 0x000D3EF3
		internal void RegisterForLevel2PostSessionProcessing(ObjectItemAssemblyLoader loader)
		{
			this._loadersThatNeedLevel2PostSessionProcessing.Add(loader);
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000D5D04 File Offset: 0x000D3F04
		internal void CompleteSession()
		{
			foreach (ObjectItemAssemblyLoader objectItemAssemblyLoader in this._loadersThatNeedLevel1PostSessionProcessing)
			{
				objectItemAssemblyLoader.OnLevel1SessionProcessing();
			}
			foreach (ObjectItemAssemblyLoader objectItemAssemblyLoader2 in this._loadersThatNeedLevel2PostSessionProcessing)
			{
				objectItemAssemblyLoader2.OnLevel2SessionProcessing();
			}
		}

		// Token: 0x04001650 RID: 5712
		private Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> _loaderFactory;

		// Token: 0x04001651 RID: 5713
		private readonly Dictionary<string, EdmType> _typesInLoading;

		// Token: 0x04001652 RID: 5714
		private readonly LoadMessageLogger _loadMessageLogger;

		// Token: 0x04001653 RID: 5715
		private readonly List<EdmItemError> _errors;

		// Token: 0x04001654 RID: 5716
		private readonly Dictionary<Assembly, MutableAssemblyCacheEntry> _listOfAssembliesLoaded = new Dictionary<Assembly, MutableAssemblyCacheEntry>();

		// Token: 0x04001655 RID: 5717
		private readonly KnownAssembliesSet _knownAssemblies;

		// Token: 0x04001656 RID: 5718
		private readonly LockedAssemblyCache _lockedAssemblyCache;

		// Token: 0x04001657 RID: 5719
		private readonly HashSet<ObjectItemAssemblyLoader> _loadersThatNeedLevel1PostSessionProcessing = new HashSet<ObjectItemAssemblyLoader>();

		// Token: 0x04001658 RID: 5720
		private readonly HashSet<ObjectItemAssemblyLoader> _loadersThatNeedLevel2PostSessionProcessing = new HashSet<ObjectItemAssemblyLoader>();

		// Token: 0x04001659 RID: 5721
		private readonly EdmItemCollection _edmItemCollection;

		// Token: 0x0400165A RID: 5722
		private Dictionary<string, KeyValuePair<EdmType, int>> _conventionCSpaceTypeNames;

		// Token: 0x0400165B RID: 5723
		private readonly Dictionary<EdmType, EdmType> _cspaceToOspace;

		// Token: 0x0400165C RID: 5724
		private readonly object _originalLoaderCookie;
	}
}
