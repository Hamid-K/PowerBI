using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000511 RID: 1297
	internal abstract class ObjectItemAssemblyLoader
	{
		// Token: 0x06003FCA RID: 16330 RVA: 0x000D42E9 File Offset: 0x000D24E9
		protected ObjectItemAssemblyLoader(Assembly assembly, AssemblyCacheEntry cacheEntry, ObjectItemLoadingSessionData sessionData)
		{
			this._assembly = assembly;
			this._cacheEntry = cacheEntry;
			this._sessionData = sessionData;
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x000D4306 File Offset: 0x000D2506
		internal virtual void Load()
		{
			this.AddToAssembliesLoaded();
			this.LoadTypesFromAssembly();
			this.AddToKnownAssemblies();
			this.LoadClosureAssemblies();
		}

		// Token: 0x06003FCC RID: 16332
		protected abstract void AddToAssembliesLoaded();

		// Token: 0x06003FCD RID: 16333
		protected abstract void LoadTypesFromAssembly();

		// Token: 0x06003FCE RID: 16334 RVA: 0x000D4320 File Offset: 0x000D2520
		protected virtual void LoadClosureAssemblies()
		{
			ObjectItemAssemblyLoader.LoadAssemblies(this.CacheEntry.ClosureAssemblies, this.SessionData);
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x000D4338 File Offset: 0x000D2538
		internal virtual void OnLevel1SessionProcessing()
		{
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x000D433A File Offset: 0x000D253A
		internal virtual void OnLevel2SessionProcessing()
		{
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x000D433C File Offset: 0x000D253C
		internal static ObjectItemAssemblyLoader CreateLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData)
		{
			if (sessionData.KnownAssemblies.Contains(assembly, sessionData.ObjectItemAssemblyLoaderFactory, sessionData.EdmItemCollection))
			{
				return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
			}
			ImmutableAssemblyCacheEntry immutableAssemblyCacheEntry;
			if (sessionData.LockedAssemblyCache.TryGetValue(assembly, out immutableAssemblyCacheEntry))
			{
				if (sessionData.ObjectItemAssemblyLoaderFactory == null)
				{
					if (immutableAssemblyCacheEntry.TypesInAssembly.Count != 0)
					{
						sessionData.ObjectItemAssemblyLoaderFactory = new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create);
					}
				}
				else if (sessionData.ObjectItemAssemblyLoaderFactory != new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create))
				{
					sessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AttributeAssemblyReferenced(assembly.FullName)));
				}
				return new ObjectItemCachedAssemblyLoader(assembly, immutableAssemblyCacheEntry, sessionData);
			}
			if (sessionData.EdmItemCollection != null && sessionData.EdmItemCollection.ConventionalOcCache.TryGetConventionalOcCacheFromAssemblyCache(assembly, out immutableAssemblyCacheEntry))
			{
				sessionData.ObjectItemAssemblyLoaderFactory = new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create);
				return new ObjectItemCachedAssemblyLoader(assembly, immutableAssemblyCacheEntry, sessionData);
			}
			if (sessionData.ObjectItemAssemblyLoaderFactory == null)
			{
				if (ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
				{
					sessionData.ObjectItemAssemblyLoaderFactory = new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create);
				}
				else if (ObjectItemConventionAssemblyLoader.SessionContainsConventionParameters(sessionData))
				{
					sessionData.ObjectItemAssemblyLoaderFactory = new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create);
				}
			}
			if (sessionData.ObjectItemAssemblyLoaderFactory != null)
			{
				return sessionData.ObjectItemAssemblyLoaderFactory(assembly, sessionData);
			}
			return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x000D4474 File Offset: 0x000D2674
		internal static bool IsAttributeLoader(object loaderCookie)
		{
			return ObjectItemAssemblyLoader.IsAttributeLoader(loaderCookie as Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>);
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000D4481 File Offset: 0x000D2681
		internal static bool IsAttributeLoader(Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> loaderFactory)
		{
			return loaderFactory != null && loaderFactory == new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemAttributeAssemblyLoader.Create);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x000D449A File Offset: 0x000D269A
		internal static bool IsConventionLoader(Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader> loaderFactory)
		{
			return loaderFactory != null && loaderFactory == new Func<Assembly, ObjectItemLoadingSessionData, ObjectItemAssemblyLoader>(ObjectItemConventionAssemblyLoader.Create);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x000D44B3 File Offset: 0x000D26B3
		protected virtual void AddToKnownAssemblies()
		{
			this._sessionData.KnownAssemblies.Add(this._assembly, new KnownAssemblyEntry(this.CacheEntry, this.SessionData.EdmItemCollection != null));
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000D44E4 File Offset: 0x000D26E4
		protected static void LoadAssemblies(IEnumerable<Assembly> assemblies, ObjectItemLoadingSessionData sessionData)
		{
			foreach (Assembly assembly in assemblies)
			{
				ObjectItemAssemblyLoader.CreateLoader(assembly, sessionData).Load();
			}
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000D4530 File Offset: 0x000D2730
		protected static bool TryGetPrimitiveType(Type type, out PrimitiveType primitiveType)
		{
			return ClrProviderManifest.Instance.TryGetPrimitiveType(Nullable.GetUnderlyingType(type) ?? type, out primitiveType);
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x000D4548 File Offset: 0x000D2748
		protected ObjectItemLoadingSessionData SessionData
		{
			get
			{
				return this._sessionData;
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x000D4550 File Offset: 0x000D2750
		protected Assembly SourceAssembly
		{
			get
			{
				return this._assembly;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x000D4558 File Offset: 0x000D2758
		protected AssemblyCacheEntry CacheEntry
		{
			get
			{
				return this._cacheEntry;
			}
		}

		// Token: 0x04001649 RID: 5705
		private readonly ObjectItemLoadingSessionData _sessionData;

		// Token: 0x0400164A RID: 5706
		private readonly Assembly _assembly;

		// Token: 0x0400164B RID: 5707
		private readonly AssemblyCacheEntry _cacheEntry;
	}
}
