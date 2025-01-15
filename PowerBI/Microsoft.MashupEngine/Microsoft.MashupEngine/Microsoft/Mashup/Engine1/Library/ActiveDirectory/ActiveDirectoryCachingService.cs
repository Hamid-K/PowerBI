using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FBB RID: 4027
	internal class ActiveDirectoryCachingService
	{
		// Token: 0x060069CB RID: 27083 RVA: 0x0016BE2E File Offset: 0x0016A02E
		public ActiveDirectoryCachingService(IEngineHost host, IActiveDirectoryService service)
		{
			this.host = host;
			this.service = service;
			this.cache = this.host.GetPersistentCache();
		}

		// Token: 0x17001E65 RID: 7781
		// (get) Token: 0x060069CC RID: 27084 RVA: 0x0016BE55 File Offset: 0x0016A055
		public string ComputerDomainName
		{
			get
			{
				return this.service.ComputerDomainName;
			}
		}

		// Token: 0x060069CD RID: 27085 RVA: 0x0016BE64 File Offset: 0x0016A064
		public IEnumerable<ActiveDirectoryServiceSearchResult> FindAll(ResourceCredentialCollection credential, DirectoryEntry searchRoot, string filter, SortOption sortOption, RowCount rowCount, SearchScope searchScope, params string[] propertiesToLoad)
		{
			RowCount rowCount2;
			if (rowCount.IsInfinite)
			{
				rowCount2 = rowCount;
			}
			else
			{
				long num = rowCount.Value / (long)this.service.PageSize;
				if (rowCount.Value % (long)this.service.PageSize != 0L)
				{
					num += 1L;
				}
				rowCount2 = new RowCount(num * (long)this.service.PageSize);
			}
			string key = this.GetKey(credential, searchRoot, filter, sortOption, rowCount2, searchScope, propertiesToLoad);
			Stream stream;
			if (!this.cache.TryGetValue(key, out stream))
			{
				IEnumerable<ActiveDirectoryServiceSearchResult> enumerable = this.service.FindAll(searchRoot, filter, sortOption, rowCount2, searchScope, propertiesToLoad);
				if (!rowCount.IsInfinite)
				{
					enumerable = enumerable.Take((int)Math.Min(rowCount.Value, 2147483647L));
				}
				return new ActiveDirectoryCachingService.CachingEnumerable(this.service.PageSize, key, this.cache, enumerable);
			}
			IEnumerable<ActiveDirectoryServiceSearchResult> enumerable2 = ActiveDirectoryServiceSearchResultSerializer.Deserialize(stream);
			if (!rowCount.IsInfinite)
			{
				enumerable2 = enumerable2.Take((int)Math.Min(rowCount.Value, 2147483647L));
			}
			return enumerable2;
		}

		// Token: 0x060069CE RID: 27086 RVA: 0x0016BF70 File Offset: 0x0016A170
		public ActiveDirectoryServiceSearchResult FindOne(ResourceCredentialCollection credential, DirectoryEntry searchRoot, string filter, SearchScope searchScope, params string[] propertiesToLoad)
		{
			string key = this.GetKey(credential, searchRoot, filter, null, RowCount.One, searchScope, propertiesToLoad);
			Stream stream;
			if (!this.cache.TryGetValue(key, out stream))
			{
				ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult = this.service.FindOne(searchRoot, filter, searchScope, propertiesToLoad);
				ActiveDirectoryServiceSearchResultSerializer.Serialize(this.cache, key, activeDirectoryServiceSearchResult);
				return activeDirectoryServiceSearchResult;
			}
			ActiveDirectoryServiceSearchResult activeDirectoryServiceSearchResult2;
			using (stream)
			{
				activeDirectoryServiceSearchResult2 = ActiveDirectoryServiceSearchResultSerializer.Deserialize(stream).First<ActiveDirectoryServiceSearchResult>();
			}
			return activeDirectoryServiceSearchResult2;
		}

		// Token: 0x060069CF RID: 27087 RVA: 0x0016BFF4 File Offset: 0x0016A1F4
		public ActiveDirectoryRootServiceEntry GetRootServiceEntry(ResourceCredentialCollection credential, DirectoryEntry root)
		{
			string text = PersistentCacheKey.ActiveDirectory.Qualify(credential.GetHash(), root.Path);
			Stream stream;
			if (!this.cache.TryGetValue(text, out stream))
			{
				ActiveDirectoryRootServiceEntry rootServiceEntry = this.service.GetRootServiceEntry(root);
				ActiveDirectoryServiceSearchResultSerializer.SerializeActiveDirectoryRootServiceEntry(this.cache, text, rootServiceEntry);
				return rootServiceEntry;
			}
			ActiveDirectoryRootServiceEntry activeDirectoryRootServiceEntry;
			using (stream)
			{
				activeDirectoryRootServiceEntry = ActiveDirectoryServiceSearchResultSerializer.DeserializeActiveDirectoryRootServiceEntry(stream);
			}
			return activeDirectoryRootServiceEntry;
		}

		// Token: 0x060069D0 RID: 27088 RVA: 0x0016C078 File Offset: 0x0016A278
		private string GetKey(ResourceCredentialCollection credential, DirectoryEntry searchRoot, string filter, SortOption sortOption, RowCount rowCount, SearchScope searchScope, string[] propertiesToLoad)
		{
			Array.Sort<string>(propertiesToLoad);
			StringBuilder stringBuilder = new StringBuilder();
			if (sortOption != null && sortOption.PropertyName != null)
			{
				stringBuilder.Append((sortOption.Direction == SortDirection.Ascending) ? "Ascending" : "Descending");
				stringBuilder.Append(sortOption.PropertyName);
			}
			stringBuilder.Append("/");
			foreach (string text in propertiesToLoad)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("/");
			}
			stringBuilder.Append(rowCount);
			stringBuilder.Append("/");
			stringBuilder.Append(ActiveDirectoryCachingService.ToString(searchScope));
			return PersistentCacheKey.ActiveDirectory.Qualify(credential.GetHash(), searchRoot.Path, (filter == null) ? string.Empty : filter, stringBuilder.ToString());
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x0016C151 File Offset: 0x0016A351
		private static string ToString(SearchScope searchScope)
		{
			switch (searchScope)
			{
			case SearchScope.Base:
				return "Base";
			case SearchScope.OneLevel:
				return "OneLevel";
			case SearchScope.Subtree:
				return "Subtree";
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04003A9C RID: 15004
		private readonly IActiveDirectoryService service;

		// Token: 0x04003A9D RID: 15005
		private readonly IEngineHost host;

		// Token: 0x04003A9E RID: 15006
		private readonly IPersistentCache cache;

		// Token: 0x02000FBC RID: 4028
		private class CachingEnumerable : IEnumerable<ActiveDirectoryServiceSearchResult>, IEnumerable
		{
			// Token: 0x060069D2 RID: 27090 RVA: 0x0016C17E File Offset: 0x0016A37E
			public CachingEnumerable(int pageSize, string key, IPersistentCache persistentCache, IEnumerable<ActiveDirectoryServiceSearchResult> searchResults)
			{
				this.pageSize = pageSize;
				this.key = key;
				this.persistentCache = persistentCache;
				this.searchResults = searchResults;
			}

			// Token: 0x060069D3 RID: 27091 RVA: 0x0016C1A3 File Offset: 0x0016A3A3
			public IEnumerator<ActiveDirectoryServiceSearchResult> GetEnumerator()
			{
				return new ActiveDirectoryCachingService.CachingEnumerable.CachingEnumerator(this.pageSize, this.key, this.persistentCache, this.searchResults.GetEnumerator());
			}

			// Token: 0x060069D4 RID: 27092 RVA: 0x0016C1C7 File Offset: 0x0016A3C7
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04003A9F RID: 15007
			private readonly int pageSize;

			// Token: 0x04003AA0 RID: 15008
			private readonly string key;

			// Token: 0x04003AA1 RID: 15009
			private readonly IPersistentCache persistentCache;

			// Token: 0x04003AA2 RID: 15010
			private readonly IEnumerable<ActiveDirectoryServiceSearchResult> searchResults;

			// Token: 0x02000FBD RID: 4029
			private class CachingEnumerator : IEnumerator<ActiveDirectoryServiceSearchResult>, IDisposable, IEnumerator
			{
				// Token: 0x060069D5 RID: 27093 RVA: 0x0016C1D0 File Offset: 0x0016A3D0
				public CachingEnumerator(int pageSize, string key, IPersistentCache persistentCache, IEnumerator<ActiveDirectoryServiceSearchResult> enumerator)
				{
					this.key = key;
					this.persistentCache = persistentCache;
					this.enumerator = enumerator;
					this.pageSize = pageSize;
					this.writer = new ActiveDirectoryDataWriter(persistentCache, key, persistentCache.MaxEntryLength);
				}

				// Token: 0x17001E66 RID: 7782
				// (get) Token: 0x060069D6 RID: 27094 RVA: 0x0016C21E File Offset: 0x0016A41E
				public ActiveDirectoryServiceSearchResult Current
				{
					get
					{
						return this.current;
					}
				}

				// Token: 0x060069D7 RID: 27095 RVA: 0x0016C226 File Offset: 0x0016A426
				public void Dispose()
				{
					this.enumerator.Dispose();
					this.enumerator = null;
					this.current = null;
				}

				// Token: 0x17001E67 RID: 7783
				// (get) Token: 0x060069D8 RID: 27096 RVA: 0x0016C241 File Offset: 0x0016A441
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060069D9 RID: 27097 RVA: 0x0016C24C File Offset: 0x0016A44C
				public bool MoveNext()
				{
					if (this.currentPage.Count == 0)
					{
						if (this.isDoneReading)
						{
							this.current = null;
							return false;
						}
						while (this.currentPage.Count < this.pageSize)
						{
							if (!this.enumerator.MoveNext())
							{
								this.writer.WriteResultEnd();
								this.isDoneReading = true;
								break;
							}
							this.currentPage.Enqueue(this.enumerator.Current);
							this.writer.Write(this.enumerator.Current);
						}
						if (this.currentPage.Count == 0)
						{
							return false;
						}
					}
					this.current = this.currentPage.Dequeue();
					return true;
				}

				// Token: 0x060069DA RID: 27098 RVA: 0x000033E7 File Offset: 0x000015E7
				public void Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x04003AA3 RID: 15011
				private readonly IPersistentCache persistentCache;

				// Token: 0x04003AA4 RID: 15012
				private readonly Queue<ActiveDirectoryServiceSearchResult> currentPage = new Queue<ActiveDirectoryServiceSearchResult>();

				// Token: 0x04003AA5 RID: 15013
				private readonly string key;

				// Token: 0x04003AA6 RID: 15014
				private readonly ActiveDirectoryDataWriter writer;

				// Token: 0x04003AA7 RID: 15015
				private readonly int pageSize;

				// Token: 0x04003AA8 RID: 15016
				private IEnumerator<ActiveDirectoryServiceSearchResult> enumerator;

				// Token: 0x04003AA9 RID: 15017
				private ActiveDirectoryServiceSearchResult current;

				// Token: 0x04003AAA RID: 15018
				private bool isDoneReading;
			}
		}
	}
}
