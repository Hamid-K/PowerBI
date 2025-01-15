using System;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000513 RID: 1299
	internal sealed class ObjectItemCachedAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06003FF3 RID: 16371 RVA: 0x000D553B File Offset: 0x000D373B
		private new ImmutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (ImmutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000D5548 File Offset: 0x000D3748
		internal ObjectItemCachedAssemblyLoader(Assembly assembly, ImmutableAssemblyCacheEntry cacheEntry, ObjectItemLoadingSessionData sessionData)
			: base(assembly, cacheEntry, sessionData)
		{
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000D5553 File Offset: 0x000D3753
		protected override void AddToAssembliesLoaded()
		{
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000D5558 File Offset: 0x000D3758
		protected override void LoadTypesFromAssembly()
		{
			foreach (EdmType edmType in this.CacheEntry.TypesInAssembly)
			{
				if (!base.SessionData.TypesInLoading.ContainsKey(edmType.Identity))
				{
					base.SessionData.TypesInLoading.Add(edmType.Identity, edmType);
				}
			}
		}
	}
}
