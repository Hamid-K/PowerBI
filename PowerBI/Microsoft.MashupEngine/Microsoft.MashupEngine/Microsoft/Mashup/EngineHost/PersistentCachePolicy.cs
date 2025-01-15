using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Interface;
using Microsoft.Mashup.EngineHost.Services;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001977 RID: 6519
	public static class PersistentCachePolicy
	{
		// Token: 0x0600A577 RID: 42359 RVA: 0x00223B48 File Offset: 0x00221D48
		public static IObjectCache CreateObjectCache(PersistentCachePolicy.CacheKind kind, PersistentCacheConfig config, IEvaluationConstants evaluationConstants)
		{
			if (!config.UserSpecific && kind != PersistentCachePolicy.CacheKind.Metadata)
			{
				throw new InvalidOperationException("Data caches must be user-specific");
			}
			object obj = PersistentCachePolicy.syncRoot;
			ObjectStorageSessions objectStorageSessions;
			lock (obj)
			{
				objectStorageSessions = PersistentCachePolicy.objectCacheSessionsByKind[(int)kind];
				if (objectStorageSessions == null || (long)config.MaxObjectCacheSize != objectStorageSessions.MaxTotalSize)
				{
					objectStorageSessions = new ObjectStorageSessions("ObjectCacheSessions/" + PersistentCachePolicy.GetCacheKindSubPath(kind), (long)config.MaxObjectCacheSize);
					PersistentCachePolicy.objectCacheSessionsByKind[(int)kind] = objectStorageSessions;
				}
			}
			ObjectStorageSessions.Session session = objectStorageSessions.NewSession(config.Directory, () => new MemoryObjectStorage());
			IObjectCache objectCache = new ObjectCache(new TracingObjectStorage(session.Storage, evaluationConstants, "ObjectCache/" + PersistentCachePolicy.GetCacheKindSubPath(kind)), (long)config.MaxObjectCacheSize, (long)config.TrimObjectCacheSize, config.UserSpecific);
			objectCache = new NotifyingObjectCache(objectCache, new Action(session.Dispose));
			objectCache = new KeyHashingObjectCache(objectCache, PersistentCachePolicy.keyHasher);
			if (config.MaxStaleness != DateTime.MinValue)
			{
				objectCache = new MaxStalenessObjectCache(objectCache, config.MaxStaleness);
			}
			if (config.RefreshData)
			{
				objectCache = new MinVersionObjectCache(objectCache, objectCache.CacheClock.Increment());
			}
			return objectCache;
		}

		// Token: 0x0600A578 RID: 42360 RVA: 0x00223C94 File Offset: 0x00221E94
		public static PersistentCache CreatePersistentCache(PersistentCachePolicy.CacheKind kind, PersistentCacheConfig config, IEvaluationConstants evaluationConstants, ITempPageService tempPageService, out CacheVersion newDiskMinVersion)
		{
			if (!config.UserSpecific && kind != PersistentCachePolicy.CacheKind.Metadata)
			{
				throw new InvalidOperationException("Data caches must be user-specific");
			}
			newDiskMinVersion = config.DiskMinVersion;
			PersistentCache persistentCache = null;
			PersistentCache persistentCache2 = null;
			if ((config.Mode & PersistentCacheMode.Memory) != (PersistentCacheMode)0)
			{
				persistentCache = PersistentCachePolicy.CreateMemoryCache(kind, config.Directory, config.MaxCacheSize, config.TrimCacheSize, config.MaxEntryLength, config.UserSpecific, evaluationConstants, tempPageService);
				if (config.RefreshData)
				{
					CacheVersion cacheVersion = persistentCache.CacheClock.Increment();
					persistentCache = new MinVersionPersistentCache(persistentCache, cacheVersion);
				}
			}
			if ((config.Mode & PersistentCacheMode.Disk) != (PersistentCacheMode)0)
			{
				persistentCache2 = PersistentCachePolicy.CreateDiskCache(new PersistentCachePolicy.CacheKind?(kind), config.Directory, config.MaxCacheSize, config.TrimCacheSize, config.MaxEntryLength, config.CancelCommitsOnDispose, config.EncryptionCertificateThumbprint, config.ImplementationVersion, config.UserSpecific, evaluationConstants);
				if (config.RefreshData && newDiskMinVersion == null)
				{
					newDiskMinVersion = persistentCache2.CacheClock.Increment();
				}
				persistentCache2 = new MinVersionPersistentCache(persistentCache2, newDiskMinVersion);
			}
			PersistentCache persistentCache3;
			switch (config.Mode & ~PersistentCacheMode.Remote)
			{
			case PersistentCacheMode.Disk:
				persistentCache3 = persistentCache2;
				break;
			case PersistentCacheMode.Memory:
				persistentCache3 = persistentCache;
				break;
			case PersistentCacheMode.Hybrid:
				persistentCache3 = new MultiLevelPersistentCache(persistentCache, persistentCache2);
				persistentCache3 = new TracingPersistentCache(persistentCache3, evaluationConstants, "HybridCache/" + PersistentCachePolicy.GetCacheKindSubPath(kind));
				break;
			default:
				throw new InvalidOperationException();
			}
			persistentCache3 = new KeyHashingPersistentCache(persistentCache3, PersistentCachePolicy.keyHasher);
			if (config.MaxStaleness != DateTime.MinValue)
			{
				persistentCache3 = new MaxStalenessPersistentCache(persistentCache3, config.MaxStaleness);
			}
			return new OverflowPersistentCache(persistentCache3, tempPageService);
		}

		// Token: 0x0600A579 RID: 42361 RVA: 0x00223E08 File Offset: 0x00222008
		private static PersistentCache CreateMemoryCache(PersistentCachePolicy.CacheKind kind, string directory, long maxCacheSize, long trimCacheSize, long maxEntryLength, bool userSpecific, IEvaluationConstants evaluationConstants, ITempPageService tempPageService)
		{
			object obj = PersistentCachePolicy.syncRoot;
			ObjectStorageSessions objectStorageSessions;
			lock (obj)
			{
				objectStorageSessions = PersistentCachePolicy.memoryPersistentCacheSessionsByKind[(int)kind];
				if (objectStorageSessions == null || maxCacheSize != objectStorageSessions.MaxTotalSize)
				{
					objectStorageSessions = new ObjectStorageSessions("MemoryCacheSessions/" + PersistentCachePolicy.GetCacheKindSubPath(kind), maxCacheSize);
					PersistentCachePolicy.memoryPersistentCacheSessionsByKind[(int)kind] = objectStorageSessions;
				}
			}
			ObjectStorageSessions.Session session = objectStorageSessions.NewSession(directory, () => new MemoryObjectStorage());
			return new TracingPersistentCache(new NotifyingPersistentCache(new MemoryPersistentCache(session.Storage, tempPageService, maxCacheSize, trimCacheSize, maxEntryLength, userSpecific), new Action(session.Dispose)), evaluationConstants, "MemoryCache/" + PersistentCachePolicy.GetCacheKindSubPath(kind));
		}

		// Token: 0x0600A57A RID: 42362 RVA: 0x00223ED8 File Offset: 0x002220D8
		private static PersistentCache CreateDiskCache(PersistentCachePolicy.CacheKind? kind, string directory, long maxCacheSize, long trimCacheSize, long maxEntryLength, bool cancelCommitsOnDispose, string certificateThumbprint, int implementationVersion, bool userSpecific, IEvaluationConstants evaluationConstants)
		{
			return new DelayedPersistentCache(maxEntryLength, delegate
			{
				SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithmExtensions.CreateAes();
				PersistentCache persistentCache = new DiskPersistentCache(directory, symmetricAlgorithm.GetEncryptedLength(65536, maxCacheSize), symmetricAlgorithm.GetEncryptedLength(65536, trimCacheSize), symmetricAlgorithm.GetEncryptedLength(65536, maxEntryLength), userSpecific, evaluationConstants);
				string text = Path.Combine(directory, "Cache.Key");
				if (certificateThumbprint == null)
				{
					persistentCache = new UserEncryptedPersistentCache(persistentCache, symmetricAlgorithm, text);
				}
				else
				{
					persistentCache = new CertificateEncryptedPersistentCache(persistentCache, symmetricAlgorithm, text, certificateThumbprint);
				}
				if (implementationVersion == 0)
				{
					string text2 = Path.Combine(directory, "Temp");
					persistentCache = new WriteBehindPersistentCache(persistentCache, text2, cancelCommitsOnDispose);
				}
				else
				{
					if (implementationVersion != 1)
					{
						throw new InvalidOperationException("Unsupported implementation version");
					}
					persistentCache = new WriteBufferedPersistentCache(persistentCache, cancelCommitsOnDispose, 1048576);
				}
				string text3 = ((kind != null) ? ("DiskCache/" + PersistentCachePolicy.GetCacheKindSubPath(kind.Value)) : "DiskCache");
				return new TracingPersistentCache(persistentCache, evaluationConstants, text3);
			});
		}

		// Token: 0x0600A57B RID: 42363 RVA: 0x00223F50 File Offset: 0x00222150
		public static void ClearPersistentCache(string directory)
		{
			if (directory != null)
			{
				EvaluationConstants evaluationConstants = new EvaluationConstants(Guid.Empty, string.Empty, null).AddTraceConstant("HostProcessId", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture), false);
				PersistentCachePolicy.CreateDiskCache(null, directory, 0L, 0L, 0L, false, null, 0, true, evaluationConstants).Purge();
			}
		}

		// Token: 0x0600A57C RID: 42364 RVA: 0x00223FB2 File Offset: 0x002221B2
		private static string GetCacheKindSubPath(PersistentCachePolicy.CacheKind kind)
		{
			if (kind == PersistentCachePolicy.CacheKind.Metadata)
			{
				return "Metadata";
			}
			if (kind != PersistentCachePolicy.CacheKind.Data)
			{
				throw new InvalidOperationException();
			}
			return "Data";
		}

		// Token: 0x0400561A RID: 22042
		private static readonly object syncRoot = new object();

		// Token: 0x0400561B RID: 22043
		private static readonly KeyHasher keyHasher = new KeyHasher();

		// Token: 0x0400561C RID: 22044
		private static ObjectStorageSessions[] memoryPersistentCacheSessionsByKind = new ObjectStorageSessions[2];

		// Token: 0x0400561D RID: 22045
		private static ObjectStorageSessions[] objectCacheSessionsByKind = new ObjectStorageSessions[2];

		// Token: 0x02001978 RID: 6520
		public enum CacheKind
		{
			// Token: 0x0400561F RID: 22047
			Metadata,
			// Token: 0x04005620 RID: 22048
			Data,
			// Token: 0x04005621 RID: 22049
			Count
		}
	}
}
