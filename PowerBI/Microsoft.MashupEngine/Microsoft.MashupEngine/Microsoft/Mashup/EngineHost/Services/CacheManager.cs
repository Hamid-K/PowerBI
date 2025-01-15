using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x0200198D RID: 6541
	public class CacheManager : ICacheManager, IDisposable
	{
		// Token: 0x0600A5F7 RID: 42487 RVA: 0x002253A4 File Offset: 0x002235A4
		public CacheManager(CacheGroupManager groupManager, IEngineHost engineHost, IEngine engine, string cacheGroup)
		{
			this.groupManager = groupManager;
			this.engineHost = engineHost;
			this.engine = engine;
			this.cacheGroup = cacheGroup;
			this.transientCaches = new Dictionary<string, CacheManagerCacheInfo>();
		}

		// Token: 0x17002A62 RID: 10850
		// (get) Token: 0x0600A5F8 RID: 42488 RVA: 0x002253D4 File Offset: 0x002235D4
		public string CacheGroup
		{
			get
			{
				return this.cacheGroup;
			}
		}

		// Token: 0x0600A5F9 RID: 42489 RVA: 0x002253DC File Offset: 0x002235DC
		public CacheManagerCacheInfo CreateCache(IRecordValue configuration)
		{
			CompoundCacheConfig compoundCacheConfig = this.ValidateCacheConfig(configuration);
			CacheManagerCacheInfo cacheManagerCacheInfo = new CacheManagerCacheInfo
			{
				Identifier = compoundCacheConfig.Id,
				CacheConfig = compoundCacheConfig
			};
			this.transientCaches.Add(cacheManagerCacheInfo.Identifier, cacheManagerCacheInfo);
			return cacheManagerCacheInfo;
		}

		// Token: 0x0600A5FA RID: 42490 RVA: 0x00225423 File Offset: 0x00223623
		public CacheManagerCacheInfo[] ListCaches()
		{
			return this.groupManager.Caches;
		}

		// Token: 0x0600A5FB RID: 42491 RVA: 0x00225430 File Offset: 0x00223630
		public void UpdateCache(string identifier, string newIdentifier, bool? readOnly)
		{
			CacheGroupManager cacheGroupManager = this.groupManager;
			lock (cacheGroupManager)
			{
				if ((newIdentifier != null && this.groupManager.CheckCache(newIdentifier)) || this.transientCaches.ContainsKey(newIdentifier))
				{
					throw this.ExpressionError("Cache identifier already in use", null);
				}
				CacheManagerCacheInfo cacheManagerCacheInfo;
				if (newIdentifier != null && this.transientCaches.TryGetValue(identifier, out cacheManagerCacheInfo) && this.transientCaches.Remove(identifier))
				{
					cacheManagerCacheInfo.Identifier = newIdentifier;
					this.groupManager.AddCache(newIdentifier, cacheManagerCacheInfo);
				}
			}
		}

		// Token: 0x0600A5FC RID: 42492 RVA: 0x002254CC File Offset: 0x002236CC
		public void DeleteCache(string identifier)
		{
			this.groupManager.DeleteCache(identifier);
		}

		// Token: 0x0600A5FD RID: 42493 RVA: 0x002254DC File Offset: 0x002236DC
		public bool TryCreateCache(string identifier, out CompoundCacheConfig cacheConfig, out CacheSet cacheSet)
		{
			CacheManagerCacheInfo cacheManagerCacheInfo;
			if (this.transientCaches.TryGetValue(identifier, out cacheManagerCacheInfo) || this.groupManager.TryGetCacheConfig(identifier, out cacheManagerCacheInfo))
			{
				cacheConfig = (CompoundCacheConfig)cacheManagerCacheInfo.CacheConfig;
				cacheSet = this.CreateCache(cacheConfig);
				return true;
			}
			cacheConfig = null;
			cacheSet = null;
			return false;
		}

		// Token: 0x0600A5FE RID: 42494 RVA: 0x0022552C File Offset: 0x0022372C
		public ICacheSet GetCache(string identifier)
		{
			CompoundCacheConfig compoundCacheConfig;
			CacheSet cacheSet;
			if (this.TryCreateCache(identifier, out compoundCacheConfig, out cacheSet))
			{
				return cacheSet;
			}
			return null;
		}

		// Token: 0x0600A5FF RID: 42495 RVA: 0x0022554C File Offset: 0x0022374C
		public CacheManagerCacheInfo GetCacheFromDirectory(string directory)
		{
			CacheManagerCacheInfo cacheManagerCacheInfo;
			if (this.groupManager.TryGetCacheConfigFromDirectory(directory, out cacheManagerCacheInfo))
			{
				return cacheManagerCacheInfo;
			}
			return default(CacheManagerCacheInfo);
		}

		// Token: 0x0600A600 RID: 42496 RVA: 0x00225574 File Offset: 0x00223774
		private CacheSet CreateCache(CompoundCacheConfig cacheConfig)
		{
			IEvaluationConstants constants = this.engineHost.QueryService<IEvaluationConstants>();
			ITempPageService tempPageService = this.engineHost.QueryService<ITempPageService>();
			PersistentCachePolicy.CacheKind cacheKind = PersistentCachePolicy.CacheKind.Metadata;
			CacheVersion diskMinVersion = null;
			PersistentCacheConfig persistentCacheConfig = null;
			PersistentCache persistentCache = CompoundCacheConfig.CreatePersistentCache(cacheConfig, delegate(PersistentCacheConfig config)
			{
				CacheVersion cacheVersion;
				PersistentCache persistentCache2 = PersistentCachePolicy.CreatePersistentCache(cacheKind, config, constants, tempPageService, out cacheVersion);
				diskMinVersion = diskMinVersion ?? cacheVersion;
				persistentCacheConfig = persistentCacheConfig ?? config;
				return persistentCache2;
			});
			PersistentCachePolicy.CacheKind cacheKind2 = PersistentCachePolicy.CacheKind.Metadata;
			IEvaluationConstants evaluationConstants = this.engineHost.QueryService<IEvaluationConstants>();
			ClearablePersistentCache clearablePersistentCache = new ClearablePersistentCache(persistentCache);
			IClearableObjectCache clearableObjectCache = new ClearableObjectCache(PersistentCachePolicy.CreateObjectCache(cacheKind2, persistentCacheConfig, evaluationConstants));
			IPersistentObjectCache persistentObjectCache = new PersistentObjectCache(clearableObjectCache, clearablePersistentCache);
			return new CacheSet
			{
				ObjectCache = clearableObjectCache,
				PersistentCache = clearablePersistentCache,
				PersistentObjectCache = persistentObjectCache
			};
		}

		// Token: 0x0600A601 RID: 42497 RVA: 0x0022561E File Offset: 0x0022381E
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.groupManager.Finished();
				this.disposed = true;
			}
		}

		// Token: 0x0600A602 RID: 42498 RVA: 0x0022563C File Offset: 0x0022383C
		private CompoundCacheConfig ValidateCacheConfig(IRecordValue configuration)
		{
			IValue value;
			if (!configuration.TryGetValue("Kind", out value) || !value.IsText)
			{
				throw this.ExpressionError("Unable to get cache kind", null);
			}
			string text = Guid.NewGuid().ToString();
			int num = 1;
			string asString = value.AsString;
			CompoundCacheConfig compoundCacheConfig;
			if (!(asString == "Simple"))
			{
				if (!(asString == "Compound"))
				{
					if (!(asString == "ReadOnly"))
					{
						throw this.ExpressionError("Unknown cache kind", value);
					}
					CompoundCacheConfig cacheConfig = null;
					this.ValidateItem(configuration, "Cache", (IValue x) => this.ValidateReference(x, out cacheConfig), ref num);
					compoundCacheConfig = cacheConfig.AsReadOnly();
				}
				else
				{
					CompoundCacheConfig primaryConfig = null;
					this.ValidateItem(configuration, "Primary", (IValue x) => this.ValidateConfigOrReference(x, out primaryConfig), ref num);
					CompoundCacheConfig fallbackConfig = null;
					this.ValidateItem(configuration, "Fallback", (IValue x) => this.ValidateConfigOrReference(x, out fallbackConfig), ref num);
					compoundCacheConfig = CompoundCacheConfig.NewCompound(text, primaryConfig, fallbackConfig);
				}
			}
			else
			{
				this.ValidateItem(configuration, "TimeToLive", (IValue x) => x.IsDuration || x.IsNull, ref num);
				IValue value2 = this.ValidateItem(configuration, "MaxSize", (IValue x) => x.IsNumber, ref num);
				IValue value3 = this.ValidateItem(configuration, "InMemory", (IValue x) => x.IsLogical, ref num);
				IValue value4 = this.ValidateItem(configuration, "UserSpecific", (IValue x) => x.IsLogical, ref num);
				long asInteger = value2.AsNumber.AsInteger64;
				int num2 = Math.Min(1048576, (int)asInteger);
				compoundCacheConfig = CompoundCacheConfig.New(text, new PersistentCacheConfig(value3.AsBoolean ? (PersistentCacheMode.Memory | PersistentCacheMode.Remote) : PersistentCacheMode.Disk, this.groupManager.MakeDirectory(text), asInteger, asInteger / 2L, asInteger / 10L, num2, num2 / 2, false, true, value4.AsBoolean, DateTime.MinValue, null, null, 1));
			}
			if (num != configuration.Keys.Length)
			{
				throw this.ExpressionError("Invalid configuration", configuration);
			}
			return compoundCacheConfig;
		}

		// Token: 0x0600A603 RID: 42499 RVA: 0x002258A4 File Offset: 0x00223AA4
		private bool ValidateConfigOrReference(IValue value, out CompoundCacheConfig config)
		{
			if (value.IsText)
			{
				return this.ValidateReference(value, out config);
			}
			if (value.IsRecord)
			{
				config = this.ValidateCacheConfig(value.AsRecord);
				return true;
			}
			throw this.ExpressionError("Invalid cache reference", value);
		}

		// Token: 0x0600A604 RID: 42500 RVA: 0x002258E0 File Offset: 0x00223AE0
		private bool ValidateReference(IValue value, out CompoundCacheConfig config)
		{
			CacheManagerCacheInfo cacheManagerCacheInfo;
			if (!value.IsText || (!this.groupManager.TryGetCacheConfig(value.AsString, out cacheManagerCacheInfo) && !this.transientCaches.TryGetValue(value.AsString, out cacheManagerCacheInfo)))
			{
				throw this.ExpressionError("Referenced cache not found", value);
			}
			config = (CompoundCacheConfig)cacheManagerCacheInfo.CacheConfig;
			return true;
		}

		// Token: 0x0600A605 RID: 42501 RVA: 0x0022593C File Offset: 0x00223B3C
		private ValueException2 ExpressionError(string message, IValue value = null)
		{
			string invalidCacheConfiguration = Strings.InvalidCacheConfiguration;
			return this.engine.Exception(this.engine.ExceptionRecord(ValueException2.MarkPii(this.engine, this.engine.Text("Expression.Error"), false).AsText, ValueException2.MarkPii(this.engine, this.engine.Text(invalidCacheConfiguration ?? message), false).AsText, value ?? this.engine.Null));
		}

		// Token: 0x0600A606 RID: 42502 RVA: 0x002259B8 File Offset: 0x00223BB8
		private IValue ValidateItem(IRecordValue configuration, string key, Func<IValue, bool> predicate, ref int count)
		{
			IValue value;
			if (!configuration.TryGetValue(key, out value))
			{
				throw this.ExpressionError("Unable to find key", this.engine.Text(key));
			}
			if (!predicate(value))
			{
				throw this.ExpressionError("Key has invalid value", this.engine.Text(key));
			}
			count++;
			return value;
		}

		// Token: 0x04005659 RID: 22105
		private readonly CacheGroupManager groupManager;

		// Token: 0x0400565A RID: 22106
		private readonly string cacheGroup;

		// Token: 0x0400565B RID: 22107
		private readonly Dictionary<string, CacheManagerCacheInfo> transientCaches;

		// Token: 0x0400565C RID: 22108
		private readonly IEngineHost engineHost;

		// Token: 0x0400565D RID: 22109
		private readonly IEngine engine;

		// Token: 0x0400565E RID: 22110
		private bool disposed;
	}
}
