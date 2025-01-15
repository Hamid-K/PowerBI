using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D7 RID: 1239
	internal class MetadataCache
	{
		// Token: 0x06003D60 RID: 15712 RVA: 0x000CB160 File Offset: 0x000C9360
		private static List<MetadataArtifactLoader> SplitPaths(string paths)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			List<string> list = new List<string>();
			for (int num = paths.IndexOf("|datadirectory|", StringComparison.OrdinalIgnoreCase); num != -1; num = paths.IndexOf("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				int num2 = ((num == 0) ? (-1) : paths.LastIndexOf("|", num - 1, StringComparison.Ordinal)) + 1;
				int num3 = paths.IndexOf("|", num + "|datadirectory|".Length, StringComparison.Ordinal);
				if (num3 == -1)
				{
					list.Add(paths.Substring(num2));
					paths = paths.Remove(num2);
					break;
				}
				list.Add(paths.Substring(num2, num3 - num2));
				paths = paths.Remove(num2, num3 - num2);
			}
			string[] array = paths.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
			if (list.Count > 0)
			{
				list.AddRange(array);
				array = list.ToArray();
			}
			List<MetadataArtifactLoader> list2 = new List<MetadataArtifactLoader>();
			List<MetadataArtifactLoader> list3 = new List<MetadataArtifactLoader>();
			List<MetadataArtifactLoader> list4 = new List<MetadataArtifactLoader>();
			List<MetadataArtifactLoader> list5 = new List<MetadataArtifactLoader>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				if (array[i].Length > 0)
				{
					MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.Create(array[i], MetadataArtifactLoader.ExtensionCheck.All, null, hashSet);
					if (array[i].EndsWith(".csdl", StringComparison.OrdinalIgnoreCase))
					{
						list2.Add(metadataArtifactLoader);
					}
					else if (array[i].EndsWith(".msl", StringComparison.OrdinalIgnoreCase))
					{
						list3.Add(metadataArtifactLoader);
					}
					else if (array[i].EndsWith(".ssdl", StringComparison.OrdinalIgnoreCase))
					{
						list4.Add(metadataArtifactLoader);
					}
					else
					{
						list5.Add(metadataArtifactLoader);
					}
				}
			}
			list5.AddRange(list4);
			list5.AddRange(list3);
			list5.AddRange(list2);
			return list5;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000CB31C File Offset: 0x000C951C
		public MetadataWorkspace GetMetadataWorkspace(DbConnectionOptions effectiveConnectionOptions)
		{
			MetadataArtifactLoader artifactLoader = this.GetArtifactLoader(effectiveConnectionOptions);
			string text = MetadataCache.CreateMetadataCacheKey(artifactLoader.GetPaths(), effectiveConnectionOptions["provider"]);
			return this.GetMetadataWorkspace(text, artifactLoader);
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x000CB350 File Offset: 0x000C9550
		public MetadataArtifactLoader GetArtifactLoader(DbConnectionOptions effectiveConnectionOptions)
		{
			string text = effectiveConnectionOptions["metadata"];
			if (!string.IsNullOrEmpty(text))
			{
				List<MetadataArtifactLoader> list = this._artifactLoaderCache.Evaluate(text);
				return MetadataArtifactLoader.Create(MetadataCache.ShouldRecalculateMetadataArtifactLoader(list) ? MetadataCache.SplitPaths(text) : list);
			}
			return MetadataArtifactLoader.Create(new List<MetadataArtifactLoader>());
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x000CB3A0 File Offset: 0x000C95A0
		public MetadataWorkspace GetMetadataWorkspace(string cacheKey, MetadataArtifactLoader artifactLoader)
		{
			return this._cachedWorkspaces.GetOrAdd(cacheKey, delegate(string k)
			{
				EdmItemCollection edmItemCollection = MetadataCache.LoadEdmItemCollection(artifactLoader);
				Lazy<StorageMappingItemCollection> mappingLoader = new Lazy<StorageMappingItemCollection>(() => MetadataCache.LoadStoreCollection(edmItemCollection, artifactLoader));
				return new MetadataWorkspace(() => edmItemCollection, () => mappingLoader.Value.StoreItemCollection, () => mappingLoader.Value);
			});
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x000CB3D2 File Offset: 0x000C95D2
		public void Clear()
		{
			this._cachedWorkspaces.Clear();
			Interlocked.CompareExchange<Memoizer<string, List<MetadataArtifactLoader>>>(ref this._artifactLoaderCache, new Memoizer<string, List<MetadataArtifactLoader>>(new Func<string, List<MetadataArtifactLoader>>(MetadataCache.SplitPaths), null), this._artifactLoaderCache);
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x000CB404 File Offset: 0x000C9604
		private static StorageMappingItemCollection LoadStoreCollection(EdmItemCollection edmItemCollection, MetadataArtifactLoader loader)
		{
			List<XmlReader> list = loader.CreateReaders(DataSpace.SSpace);
			StoreItemCollection storeItemCollection;
			try
			{
				storeItemCollection = new StoreItemCollection(list, loader.GetPaths(DataSpace.SSpace));
			}
			finally
			{
				Helper.DisposeXmlReaders(list);
			}
			List<XmlReader> list2 = loader.CreateReaders(DataSpace.CSSpace);
			StorageMappingItemCollection storageMappingItemCollection;
			try
			{
				storageMappingItemCollection = new StorageMappingItemCollection(edmItemCollection, storeItemCollection, list2, loader.GetPaths(DataSpace.CSSpace));
			}
			finally
			{
				Helper.DisposeXmlReaders(list2);
			}
			return storageMappingItemCollection;
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x000CB470 File Offset: 0x000C9670
		private static EdmItemCollection LoadEdmItemCollection(MetadataArtifactLoader loader)
		{
			List<XmlReader> list = loader.CreateReaders(DataSpace.CSpace);
			EdmItemCollection edmItemCollection;
			try
			{
				edmItemCollection = new EdmItemCollection(list, loader.GetPaths(DataSpace.CSpace), false);
			}
			finally
			{
				Helper.DisposeXmlReaders(list);
			}
			return edmItemCollection;
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x000CB4B0 File Offset: 0x000C96B0
		private static bool ShouldRecalculateMetadataArtifactLoader(IEnumerable<MetadataArtifactLoader> loaders)
		{
			return loaders.Any((MetadataArtifactLoader loader) => loader.GetType() == typeof(MetadataArtifactLoaderCompositeFile));
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x000CB4D8 File Offset: 0x000C96D8
		private static string CreateMetadataCacheKey(IList<string> paths, string providerName)
		{
			int num = 0;
			string text;
			MetadataCache.CreateMetadataCacheKeyWithCount(paths, providerName, false, ref num, out text);
			MetadataCache.CreateMetadataCacheKeyWithCount(paths, providerName, true, ref num, out text);
			return text;
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x000CB500 File Offset: 0x000C9700
		private static void CreateMetadataCacheKeyWithCount(IList<string> paths, string providerName, bool buildResult, ref int resultCount, out string result)
		{
			StringBuilder stringBuilder = (buildResult ? new StringBuilder(resultCount) : null);
			resultCount = 0;
			if (!string.IsNullOrEmpty(providerName))
			{
				resultCount += providerName.Length + 1;
				if (buildResult)
				{
					stringBuilder.Append(providerName);
					stringBuilder.Append(";");
				}
			}
			if (paths != null)
			{
				for (int i = 0; i < paths.Count; i++)
				{
					if (paths[i].Length > 0)
					{
						if (i > 0)
						{
							resultCount++;
							if (buildResult)
							{
								stringBuilder.Append("|");
							}
						}
						resultCount += paths[i].Length;
						if (buildResult)
						{
							stringBuilder.Append(paths[i]);
						}
					}
				}
				resultCount++;
				if (buildResult)
				{
					stringBuilder.Append(";");
				}
			}
			result = (buildResult ? stringBuilder.ToString() : null);
		}

		// Token: 0x040014F4 RID: 5364
		private const string DataDirectory = "|datadirectory|";

		// Token: 0x040014F5 RID: 5365
		private const string MetadataPathSeparator = "|";

		// Token: 0x040014F6 RID: 5366
		private const string SemicolonSeparator = ";";

		// Token: 0x040014F7 RID: 5367
		public static readonly MetadataCache Instance = new MetadataCache();

		// Token: 0x040014F8 RID: 5368
		private Memoizer<string, List<MetadataArtifactLoader>> _artifactLoaderCache = new Memoizer<string, List<MetadataArtifactLoader>>(new Func<string, List<MetadataArtifactLoader>>(MetadataCache.SplitPaths), null);

		// Token: 0x040014F9 RID: 5369
		private readonly ConcurrentDictionary<string, MetadataWorkspace> _cachedWorkspaces = new ConcurrentDictionary<string, MetadataWorkspace>();
	}
}
