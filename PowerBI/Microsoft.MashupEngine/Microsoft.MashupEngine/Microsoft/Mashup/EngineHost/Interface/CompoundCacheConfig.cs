using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.EngineHost.Services;

namespace Microsoft.Mashup.EngineHost.Interface
{
	// Token: 0x02001B5F RID: 7007
	public abstract class CompoundCacheConfig
	{
		// Token: 0x0600AF66 RID: 44902 RVA: 0x0023E9E9 File Offset: 0x0023CBE9
		protected CompoundCacheConfig(string id)
		{
			this.id = id;
		}

		// Token: 0x17002BF9 RID: 11257
		// (get) Token: 0x0600AF67 RID: 44903 RVA: 0x0023E9F8 File Offset: 0x0023CBF8
		public virtual string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0600AF68 RID: 44904 RVA: 0x0023EA00 File Offset: 0x0023CC00
		public static CompoundCacheConfig New(string id, PersistentCacheConfig config)
		{
			return new CompoundCacheConfig.SimpleCacheConfig(id, config);
		}

		// Token: 0x0600AF69 RID: 44905 RVA: 0x0023EA0C File Offset: 0x0023CC0C
		public static CompoundCacheConfig Deserialize(BinaryReader reader)
		{
			byte b = reader.ReadByte();
			string text = reader.ReadNullableString();
			switch (b)
			{
			case 1:
				return new CompoundCacheConfig.SimpleCacheConfig(text, RemotePersistentCacheFactory.ReadPersistentCacheConfig(reader));
			case 2:
				return new CompoundCacheConfig.ReadOnlyCacheConfig(text, CompoundCacheConfig.Deserialize(reader));
			case 3:
				return new CompoundCacheConfig.MultiLevelCacheConfig(text, CompoundCacheConfig.Deserialize(reader), CompoundCacheConfig.Deserialize(reader));
			case 4:
				return new CompoundCacheConfig.PartiallyReadOnlyCacheConfig(text, CompoundCacheConfig.Deserialize(reader), CompoundCacheConfig.Deserialize(reader));
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600AF6A RID: 44906
		public abstract void Serialize(BinaryWriter writer);

		// Token: 0x0600AF6B RID: 44907 RVA: 0x0023EA86 File Offset: 0x0023CC86
		public static CompoundCacheConfig NewCompound(string id, CompoundCacheConfig primaryConfig, CompoundCacheConfig fallbackConfig)
		{
			if (fallbackConfig is CompoundCacheConfig.ReadOnlyCacheConfig)
			{
				throw new InvalidOperationException("Not supported");
			}
			if (primaryConfig is CompoundCacheConfig.ReadOnlyCacheConfig)
			{
				return new CompoundCacheConfig.PartiallyReadOnlyCacheConfig(id, primaryConfig, fallbackConfig);
			}
			return new CompoundCacheConfig.MultiLevelCacheConfig(id, primaryConfig, fallbackConfig);
		}

		// Token: 0x0600AF6C RID: 44908 RVA: 0x0023EAB4 File Offset: 0x0023CCB4
		public static PersistentCache CreatePersistentCache(object config, Func<PersistentCacheConfig, PersistentCache> ctor)
		{
			PersistentCacheConfig persistentCacheConfig = config as PersistentCacheConfig;
			if (persistentCacheConfig != null)
			{
				return ctor(persistentCacheConfig);
			}
			CompoundCacheConfig compoundCacheConfig = config as CompoundCacheConfig;
			if (compoundCacheConfig != null)
			{
				return compoundCacheConfig.CreatePersistentCache(ctor);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600AF6D RID: 44909 RVA: 0x0023EAEA File Offset: 0x0023CCEA
		public CompoundCacheConfig AsReadOnly()
		{
			return new CompoundCacheConfig.ReadOnlyCacheConfig(null, this);
		}

		// Token: 0x0600AF6E RID: 44910
		public abstract bool TryGetByDirectory(string directory, out CompoundCacheConfig config);

		// Token: 0x0600AF6F RID: 44911
		protected abstract PersistentCache CreatePersistentCache(Func<PersistentCacheConfig, PersistentCache> ctor);

		// Token: 0x04005A66 RID: 23142
		private readonly string id;

		// Token: 0x02001B60 RID: 7008
		private sealed class SimpleCacheConfig : CompoundCacheConfig
		{
			// Token: 0x0600AF70 RID: 44912 RVA: 0x0023EAF3 File Offset: 0x0023CCF3
			public SimpleCacheConfig(string id, PersistentCacheConfig config)
				: base(id)
			{
				this.config = config;
			}

			// Token: 0x17002BFA RID: 11258
			// (get) Token: 0x0600AF71 RID: 44913 RVA: 0x0023EB03 File Offset: 0x0023CD03
			public override string Id
			{
				get
				{
					return this.config.Directory;
				}
			}

			// Token: 0x0600AF72 RID: 44914 RVA: 0x0023EB10 File Offset: 0x0023CD10
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(1);
				writer.WriteNullableString(this.id);
				RemotePersistentCacheFactory.WritePersistentCacheConfig(writer, this.config);
			}

			// Token: 0x0600AF73 RID: 44915 RVA: 0x0023EB31 File Offset: 0x0023CD31
			public override bool TryGetByDirectory(string directory, out CompoundCacheConfig config)
			{
				if (this.config.Directory == directory)
				{
					config = this;
					return true;
				}
				config = null;
				return false;
			}

			// Token: 0x0600AF74 RID: 44916 RVA: 0x0023EB4F File Offset: 0x0023CD4F
			protected override PersistentCache CreatePersistentCache(Func<PersistentCacheConfig, PersistentCache> ctor)
			{
				return ctor(this.config);
			}

			// Token: 0x04005A67 RID: 23143
			private readonly PersistentCacheConfig config;
		}

		// Token: 0x02001B61 RID: 7009
		private sealed class MultiLevelCacheConfig : CompoundCacheConfig
		{
			// Token: 0x0600AF75 RID: 44917 RVA: 0x0023EB5D File Offset: 0x0023CD5D
			public MultiLevelCacheConfig(string id, CompoundCacheConfig primaryConfig, CompoundCacheConfig fallbackConfig)
				: base(id)
			{
				this.primaryConfig = primaryConfig;
				this.fallbackConfig = fallbackConfig;
			}

			// Token: 0x0600AF76 RID: 44918 RVA: 0x0023EB74 File Offset: 0x0023CD74
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(3);
				writer.WriteNullableString(this.id);
				this.primaryConfig.Serialize(writer);
				this.fallbackConfig.Serialize(writer);
			}

			// Token: 0x0600AF77 RID: 44919 RVA: 0x0023EBA1 File Offset: 0x0023CDA1
			public override bool TryGetByDirectory(string directory, out CompoundCacheConfig config)
			{
				return this.primaryConfig.TryGetByDirectory(directory, out config) || this.fallbackConfig.TryGetByDirectory(directory, out config);
			}

			// Token: 0x0600AF78 RID: 44920 RVA: 0x0023EBC4 File Offset: 0x0023CDC4
			protected override PersistentCache CreatePersistentCache(Func<PersistentCacheConfig, PersistentCache> ctor)
			{
				PersistentCache persistentCache = this.primaryConfig.CreatePersistentCache(ctor);
				PersistentCache persistentCache2 = this.fallbackConfig.CreatePersistentCache(ctor);
				return new MultiLevelPersistentCache(persistentCache, persistentCache2);
			}

			// Token: 0x04005A68 RID: 23144
			private readonly CompoundCacheConfig primaryConfig;

			// Token: 0x04005A69 RID: 23145
			private readonly CompoundCacheConfig fallbackConfig;
		}

		// Token: 0x02001B62 RID: 7010
		private sealed class ReadOnlyCacheConfig : CompoundCacheConfig
		{
			// Token: 0x0600AF79 RID: 44921 RVA: 0x0023EBF0 File Offset: 0x0023CDF0
			public ReadOnlyCacheConfig(string id, CompoundCacheConfig compoundCacheConfig)
				: base(id)
			{
				this.cacheConfig = compoundCacheConfig;
			}

			// Token: 0x0600AF7A RID: 44922 RVA: 0x0023EC00 File Offset: 0x0023CE00
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(2);
				writer.WriteNullableString(this.id);
				this.cacheConfig.Serialize(writer);
			}

			// Token: 0x0600AF7B RID: 44923 RVA: 0x0023EC21 File Offset: 0x0023CE21
			public override bool TryGetByDirectory(string directory, out CompoundCacheConfig config)
			{
				return this.cacheConfig.TryGetByDirectory(directory, out config);
			}

			// Token: 0x0600AF7C RID: 44924 RVA: 0x0023EC30 File Offset: 0x0023CE30
			protected override PersistentCache CreatePersistentCache(Func<PersistentCacheConfig, PersistentCache> ctor)
			{
				return this.cacheConfig.CreatePersistentCache(ctor);
			}

			// Token: 0x04005A6A RID: 23146
			private readonly CompoundCacheConfig cacheConfig;
		}

		// Token: 0x02001B63 RID: 7011
		private sealed class PartiallyReadOnlyCacheConfig : CompoundCacheConfig
		{
			// Token: 0x0600AF7D RID: 44925 RVA: 0x0023EC3E File Offset: 0x0023CE3E
			public PartiallyReadOnlyCacheConfig(string id, CompoundCacheConfig primaryConfig, CompoundCacheConfig fallbackConfig)
				: base(id)
			{
				this.primaryConfig = primaryConfig;
				this.fallbackConfig = fallbackConfig;
			}

			// Token: 0x0600AF7E RID: 44926 RVA: 0x0023EC55 File Offset: 0x0023CE55
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(4);
				writer.WriteNullableString(this.id);
				this.primaryConfig.Serialize(writer);
				this.fallbackConfig.Serialize(writer);
			}

			// Token: 0x0600AF7F RID: 44927 RVA: 0x0023EC82 File Offset: 0x0023CE82
			public override bool TryGetByDirectory(string directory, out CompoundCacheConfig config)
			{
				return this.primaryConfig.TryGetByDirectory(directory, out config) || this.fallbackConfig.TryGetByDirectory(directory, out config);
			}

			// Token: 0x0600AF80 RID: 44928 RVA: 0x0023ECA4 File Offset: 0x0023CEA4
			protected override PersistentCache CreatePersistentCache(Func<PersistentCacheConfig, PersistentCache> ctor)
			{
				PersistentCache persistentCache = this.primaryConfig.CreatePersistentCache(ctor);
				PersistentCache persistentCache2 = this.fallbackConfig.CreatePersistentCache(ctor);
				return new PartiallyReadOnlyPersistentCache(persistentCache, persistentCache2);
			}

			// Token: 0x04005A6B RID: 23147
			private readonly CompoundCacheConfig primaryConfig;

			// Token: 0x04005A6C RID: 23148
			private readonly CompoundCacheConfig fallbackConfig;
		}

		// Token: 0x02001B64 RID: 7012
		private static class CacheType
		{
			// Token: 0x04005A6D RID: 23149
			public const byte Simple = 1;

			// Token: 0x04005A6E RID: 23150
			public const byte ReadOnly = 2;

			// Token: 0x04005A6F RID: 23151
			public const byte MultiLevel = 3;

			// Token: 0x04005A70 RID: 23152
			public const byte PartiallyReadOnly = 4;
		}
	}
}
