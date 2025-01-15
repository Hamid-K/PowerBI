using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000506 RID: 1286
	internal static class AssemblyCache
	{
		// Token: 0x06003F8E RID: 16270 RVA: 0x000D3975 File Offset: 0x000D1B75
		internal static LockedAssemblyCache AcquireLockedAssemblyCache()
		{
			return new LockedAssemblyCache(AssemblyCache._assemblyCacheLock, AssemblyCache._globalAssemblyCache);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x000D3988 File Offset: 0x000D1B88
		internal static void LoadAssembly(Assembly assembly, bool loadReferencedAssemblies, KnownAssembliesSet knownAssemblies, out Dictionary<string, EdmType> typesInLoading, out List<EdmItemError> errors)
		{
			object obj = null;
			AssemblyCache.LoadAssembly(assembly, loadReferencedAssemblies, knownAssemblies, null, null, ref obj, out typesInLoading, out errors);
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x000D39A8 File Offset: 0x000D1BA8
		internal static void LoadAssembly(Assembly assembly, bool loadReferencedAssemblies, KnownAssembliesSet knownAssemblies, EdmItemCollection edmItemCollection, Action<string> logLoadMessage, ref object loaderCookie, out Dictionary<string, EdmType> typesInLoading, out List<EdmItemError> errors)
		{
			typesInLoading = null;
			errors = null;
			using (LockedAssemblyCache lockedAssemblyCache = AssemblyCache.AcquireLockedAssemblyCache())
			{
				ObjectItemLoadingSessionData objectItemLoadingSessionData = new ObjectItemLoadingSessionData(knownAssemblies, lockedAssemblyCache, edmItemCollection, logLoadMessage, loaderCookie);
				AssemblyCache.LoadAssembly(assembly, loadReferencedAssemblies, objectItemLoadingSessionData);
				loaderCookie = objectItemLoadingSessionData.LoaderCookie;
				objectItemLoadingSessionData.CompleteSession();
				if (objectItemLoadingSessionData.EdmItemErrors.Count == 0)
				{
					new EdmValidator
					{
						SkipReadOnlyItems = true
					}.Validate<EdmType>(objectItemLoadingSessionData.TypesInLoading.Values, objectItemLoadingSessionData.EdmItemErrors);
					if (objectItemLoadingSessionData.EdmItemErrors.Count == 0)
					{
						if (ObjectItemAssemblyLoader.IsAttributeLoader(objectItemLoadingSessionData.ObjectItemAssemblyLoaderFactory))
						{
							AssemblyCache.UpdateCache(lockedAssemblyCache, objectItemLoadingSessionData.AssembliesLoaded);
						}
						else if (objectItemLoadingSessionData.EdmItemCollection != null && ObjectItemAssemblyLoader.IsConventionLoader(objectItemLoadingSessionData.ObjectItemAssemblyLoaderFactory))
						{
							AssemblyCache.UpdateCache(objectItemLoadingSessionData.EdmItemCollection, objectItemLoadingSessionData.AssembliesLoaded);
						}
					}
				}
				if (objectItemLoadingSessionData.TypesInLoading.Count > 0)
				{
					foreach (EdmType edmType in objectItemLoadingSessionData.TypesInLoading.Values)
					{
						edmType.SetReadOnly();
					}
				}
				typesInLoading = objectItemLoadingSessionData.TypesInLoading;
				errors = objectItemLoadingSessionData.EdmItemErrors;
			}
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x000D3AE8 File Offset: 0x000D1CE8
		private static void LoadAssembly(Assembly assembly, bool loadReferencedAssemblies, ObjectItemLoadingSessionData loadingData)
		{
			KnownAssemblyEntry knownAssemblyEntry;
			bool flag;
			if (loadingData.KnownAssemblies.TryGetKnownAssembly(assembly, loadingData.ObjectItemAssemblyLoaderFactory, loadingData.EdmItemCollection, out knownAssemblyEntry))
			{
				flag = !knownAssemblyEntry.ReferencedAssembliesAreLoaded && loadReferencedAssemblies;
			}
			else
			{
				ObjectItemAssemblyLoader.CreateLoader(assembly, loadingData).Load();
				flag = loadReferencedAssemblies;
			}
			if (flag)
			{
				if ((knownAssemblyEntry == null && loadingData.KnownAssemblies.TryGetKnownAssembly(assembly, loadingData.ObjectItemAssemblyLoaderFactory, loadingData.EdmItemCollection, out knownAssemblyEntry)) || knownAssemblyEntry != null)
				{
					knownAssemblyEntry.ReferencedAssembliesAreLoaded = true;
				}
				foreach (Assembly assembly2 in MetadataAssemblyHelper.GetNonSystemReferencedAssemblies(assembly))
				{
					AssemblyCache.LoadAssembly(assembly2, loadReferencedAssemblies, loadingData);
				}
			}
		}

		// Token: 0x06003F92 RID: 16274 RVA: 0x000D3B9C File Offset: 0x000D1D9C
		private static void UpdateCache(EdmItemCollection edmItemCollection, Dictionary<Assembly, MutableAssemblyCacheEntry> assemblies)
		{
			foreach (KeyValuePair<Assembly, MutableAssemblyCacheEntry> keyValuePair in assemblies)
			{
				edmItemCollection.ConventionalOcCache.AddAssemblyToOcCacheFromAssemblyCache(keyValuePair.Key, new ImmutableAssemblyCacheEntry(keyValuePair.Value));
			}
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x000D3C04 File Offset: 0x000D1E04
		private static void UpdateCache(LockedAssemblyCache lockedAssemblyCache, Dictionary<Assembly, MutableAssemblyCacheEntry> assemblies)
		{
			foreach (KeyValuePair<Assembly, MutableAssemblyCacheEntry> keyValuePair in assemblies)
			{
				lockedAssemblyCache.Add(keyValuePair.Key, new ImmutableAssemblyCacheEntry(keyValuePair.Value));
			}
		}

		// Token: 0x04001632 RID: 5682
		private static readonly Dictionary<Assembly, ImmutableAssemblyCacheEntry> _globalAssemblyCache = new Dictionary<Assembly, ImmutableAssemblyCacheEntry>();

		// Token: 0x04001633 RID: 5683
		private static readonly object _assemblyCacheLock = new object();
	}
}
