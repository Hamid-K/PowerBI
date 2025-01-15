using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A27 RID: 6695
	public class MultiLevelPersistentCache : DualPersistentCache
	{
		// Token: 0x0600A95A RID: 43354 RVA: 0x002306D8 File Offset: 0x0022E8D8
		public MultiLevelPersistentCache(PersistentCache primaryCache, PersistentCache secondaryCache)
			: base(primaryCache, secondaryCache)
		{
			bool? userSpecific = primaryCache.UserSpecific;
			bool? userSpecific2 = secondaryCache.UserSpecific;
			if (!((userSpecific.GetValueOrDefault() == userSpecific2.GetValueOrDefault()) & (userSpecific != null == (userSpecific2 != null))) || primaryCache.UserSpecific == null)
			{
				throw new InvalidOperationException("invalid configuration for multi-level persistent cache");
			}
			this.misses = new HashSet<string>();
			this.DeserializeMisses();
		}

		// Token: 0x17002B09 RID: 11017
		// (get) Token: 0x0600A95B RID: 43355 RVA: 0x0023074A File Offset: 0x0022E94A
		// (set) Token: 0x0600A95C RID: 43356 RVA: 0x00230780 File Offset: 0x0022E980
		public override DateTime Staleness
		{
			get
			{
				if (this.primaryCache.Staleness <= this.secondaryCache.Staleness)
				{
					return this.primaryCache.Staleness;
				}
				return this.secondaryCache.Staleness;
			}
			set
			{
				this.primaryCache.Staleness = value;
			}
		}

		// Token: 0x0600A95D RID: 43357 RVA: 0x00230790 File Offset: 0x0022E990
		protected override bool TryGetStorage(bool migrate, string key, DateTime maxStaleness, CacheVersion minVersion1, CacheVersion minVersion2, out IStorage storage)
		{
			HashSet<string> hashSet = this.misses;
			lock (hashSet)
			{
				if (this.misses.Contains(key))
				{
					storage = null;
					return false;
				}
			}
			CacheVersion cacheVersion = this.primaryCache.CacheClock.Current;
			if (this.primaryCache.TryGetStorage(key, maxStaleness, minVersion1, out storage))
			{
				return true;
			}
			if (!this.secondaryCache.TryGetStorage(key, maxStaleness, minVersion2, out storage))
			{
				hashSet = this.misses;
				lock (hashSet)
				{
					this.misses.Add(key);
				}
				storage = null;
				return false;
			}
			if (!migrate)
			{
				return true;
			}
			using (storage)
			{
				MultiLevelPersistentCache.SyncStorage(key, this.secondaryCache, storage, this.primaryCache, cacheVersion);
			}
			return this.TryGetStorage(false, key, maxStaleness, minVersion1, minVersion2, out storage);
		}

		// Token: 0x0600A95E RID: 43358 RVA: 0x002308A4 File Offset: 0x0022EAA4
		public override IStorage CreateStorage()
		{
			return this.primaryCache.CreateStorage();
		}

		// Token: 0x0600A95F RID: 43359 RVA: 0x002308B4 File Offset: 0x0022EAB4
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			CacheVersion version = MultiLevelCacheVersion.GetVersion1(maxVersion);
			CacheVersion version2 = MultiLevelCacheVersion.GetVersion2(maxVersion);
			this.primaryCache.CommitStorage(key, version, storage);
			HashSet<string> hashSet = this.misses;
			lock (hashSet)
			{
				this.misses.Remove(key);
			}
			MultiLevelPersistentCache.SyncStorage(key, this.primaryCache, storage, this.secondaryCache, version2);
		}

		// Token: 0x0600A960 RID: 43360 RVA: 0x0023092C File Offset: 0x0022EB2C
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			return this.secondaryCache.OpenStorage(key, maxStaleness, MultiLevelCacheVersion.GetVersion2(minVersion), pageSize, maxPageCount);
		}

		// Token: 0x0600A961 RID: 43361 RVA: 0x00230945 File Offset: 0x0022EB45
		public override void Dispose()
		{
			this.SerializeMisses();
			base.Dispose();
		}

		// Token: 0x0600A962 RID: 43362 RVA: 0x00230954 File Offset: 0x0022EB54
		private void DeserializeMisses()
		{
			IStorage storage;
			if (this.primaryCache.TryGetStorage("MultiLevelPersistentCache/1/misses", DateTime.MinValue, null, out storage))
			{
				using (storage)
				{
					using (Stream stream = storage.OpenStream(0))
					{
						using (BinaryReader binaryReader = new BinaryReader(stream))
						{
							string[] array = binaryReader.ReadArray((BinaryReader r) => r.ReadNullableString());
							HashSet<string> hashSet = this.misses;
							lock (hashSet)
							{
								this.misses.UnionWith(array);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600A963 RID: 43363 RVA: 0x00230A38 File Offset: 0x0022EC38
		private void SerializeMisses()
		{
			using (IStorage storage = this.primaryCache.CreateStorage())
			{
				using (Stream stream = storage.CreateStream())
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(stream))
					{
						HashSet<string> hashSet = this.misses;
						string[] array;
						lock (hashSet)
						{
							array = this.misses.ToArray<string>();
						}
						binaryWriter.WriteArray(array, delegate(BinaryWriter w, string v)
						{
							w.WriteNullableString(v);
						});
						binaryWriter.Flush();
						storage.CommitStream(0, stream);
						this.primaryCache.CommitStorage("MultiLevelPersistentCache/1/misses", this.primaryCache.CacheClock.Current, storage);
					}
				}
			}
		}

		// Token: 0x0600A964 RID: 43364 RVA: 0x00230B38 File Offset: 0x0022ED38
		private static void SyncStorage(string key, PersistentCache fromCache, IStorage fromStorage, PersistentCache toCache, CacheVersion toMaxVersion)
		{
			using (IStorage storage = toCache.CreateStorage())
			{
				foreach (int num in fromStorage.StreamIds)
				{
					Stream stream = storage.CreateStream();
					try
					{
						using (Stream stream2 = fromStorage.OpenStream(num))
						{
							stream2.CopyTo(stream);
							stream = storage.CommitStream(num, stream);
						}
					}
					finally
					{
						stream.Dispose();
					}
					toCache.CommitStorage(key, toMaxVersion, storage);
				}
			}
			toCache.Staleness = fromCache.Staleness;
		}

		// Token: 0x04005823 RID: 22563
		private const string missesKey = "MultiLevelPersistentCache/1/misses";

		// Token: 0x04005824 RID: 22564
		private readonly HashSet<string> misses;
	}
}
