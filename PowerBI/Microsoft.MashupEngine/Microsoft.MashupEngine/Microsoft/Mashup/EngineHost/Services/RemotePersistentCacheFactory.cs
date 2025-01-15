using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.EngineHost.Interface;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001AC8 RID: 6856
	internal class RemotePersistentCacheFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AC56 RID: 44118 RVA: 0x00237174 File Offset: 0x00235374
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			CacheSetsConfig cacheSetsConfig = engineHost.QueryService<CacheSetsConfig>();
			ICacheSets cacheSets = engineHost.QueryService<ICacheSets>();
			Dictionary<string, IPersistentCache> dictionary = new Dictionary<string, IPersistentCache>(StringComparer.OrdinalIgnoreCase);
			cacheSetsConfig.Data.Serialize(proxyInitArgs);
			dictionary.Add(cacheSetsConfig.Data.Id, cacheSets.Data.PersistentCache);
			proxyInitArgs.WriteBool(cacheSetsConfig.Metadata != null);
			if (cacheSetsConfig.Metadata != null)
			{
				cacheSetsConfig.Metadata.Serialize(proxyInitArgs);
				dictionary.Add(cacheSetsConfig.Metadata.Id, cacheSets.Metadata.PersistentCache);
			}
			ICacheManager cacheManager = engineHost.QueryService<ICacheManager>();
			proxyInitArgs.WriteNullableString((cacheManager != null) ? cacheManager.CacheGroup : null);
			return new RemotePersistentCacheFactory.Stub(engineHost, dictionary, cacheManager, messenger);
		}

		// Token: 0x0600AC57 RID: 44119 RVA: 0x00237224 File Offset: 0x00235424
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			CompoundCacheConfig compoundCacheConfig = CompoundCacheConfig.Deserialize(proxyInitArgs);
			CompoundCacheConfig compoundCacheConfig2 = null;
			if (proxyInitArgs.ReadBool())
			{
				compoundCacheConfig2 = CompoundCacheConfig.Deserialize(proxyInitArgs);
			}
			CacheSetsConfig cacheSetsConfig = new CacheSetsConfig
			{
				Metadata = compoundCacheConfig2,
				Data = compoundCacheConfig
			};
			CacheSet cacheSet = RemotePersistentCacheFactory.CreateCacheSet(PersistentCachePolicy.CacheKind.Data, cacheSetsConfig.Data, engineHost, messenger, out compoundCacheConfig);
			CacheSet cacheSet2 = cacheSet;
			if (compoundCacheConfig2 != null)
			{
				cacheSet2 = RemotePersistentCacheFactory.CreateCacheSet(PersistentCachePolicy.CacheKind.Metadata, cacheSetsConfig.Metadata, engineHost, messenger, out compoundCacheConfig2);
			}
			cacheSetsConfig = new CacheSetsConfig
			{
				Metadata = compoundCacheConfig2,
				Data = compoundCacheConfig
			};
			CacheSets cacheSets = new CacheSets
			{
				Metadata = new RemotePersistentCacheFactory.ReplaceableCacheSet(cacheSet2),
				Data = cacheSet
			};
			string text = proxyInitArgs.ReadNullableString();
			return new RemotePersistentCacheFactory.Proxy(cacheSetsConfig, cacheSets, new RemotePersistentCacheFactory.CacheManagerProxy(engineHost, messenger, text));
		}

		// Token: 0x0600AC58 RID: 44120 RVA: 0x002372D0 File Offset: 0x002354D0
		private static CacheSet CreateCacheSet(PersistentCachePolicy.CacheKind kind, CompoundCacheConfig cacheConfig, IEngineHost engineHost, IMessenger messenger, out CompoundCacheConfig newCacheConfig)
		{
			newCacheConfig = cacheConfig;
			return RemotePersistentCacheFactory.SetupCache(new CacheManagerCacheInfo
			{
				Identifier = cacheConfig.Id,
				CacheConfig = cacheConfig
			}, engineHost, messenger);
		}

		// Token: 0x0600AC59 RID: 44121 RVA: 0x00237308 File Offset: 0x00235508
		private static PersistentCache CreatePersistentCache(PersistentCachePolicy.CacheKind kind, PersistentCacheConfig cacheConfig, IEngineHost engineHost, IMessenger messenger, out CacheVersion diskMinVersion)
		{
			IEvaluationConstants evaluationConstants = engineHost.GetEvaluationConstants();
			ITempPageService tempPageService = engineHost.QueryService<ITempPageService>();
			PersistentCache persistentCache = PersistentCachePolicy.CreatePersistentCache(kind, cacheConfig.ToLocalConfig(), evaluationConstants, tempPageService, out diskMinVersion);
			if ((cacheConfig.Mode & PersistentCacheMode.Remote) != (PersistentCacheMode)0)
			{
				PersistentCache persistentCache2 = new RemotePersistentCacheFactory.RemotePersistentCache(engineHost, messenger, cacheConfig.Directory, cacheConfig.MaxEntryLength, cacheConfig.UserSpecific);
				persistentCache2 = new TracingPersistentCache(persistentCache2, evaluationConstants, "RemoteCache");
				persistentCache = new MultiLevelPersistentCache(persistentCache, persistentCache2);
				persistentCache = new TracingPersistentCache(persistentCache, evaluationConstants, "HybridRemoteCache");
			}
			return persistentCache;
		}

		// Token: 0x0600AC5A RID: 44122 RVA: 0x0023737C File Offset: 0x0023557C
		private static CacheSet CreateCacheSet(PersistentCachePolicy.CacheKind kind, PersistentCache cache, PersistentCacheConfig cacheConfig, CacheVersion diskMinVersion, IEngineHost engineHost, out PersistentCacheConfig newCacheConfig)
		{
			IEvaluationConstants evaluationConstants = engineHost.GetEvaluationConstants();
			ClearablePersistentCache clearablePersistentCache = new ClearablePersistentCache(cache);
			ClearableObjectCache clearableObjectCache = new ClearableObjectCache(PersistentCachePolicy.CreateObjectCache(kind, cacheConfig, evaluationConstants));
			IPersistentObjectCache persistentObjectCache = new PersistentObjectCache(clearableObjectCache, clearablePersistentCache);
			newCacheConfig = cacheConfig.ReplaceMinVersion(diskMinVersion);
			return new CacheSet
			{
				PersistentCache = clearablePersistentCache,
				PersistentObjectCache = persistentObjectCache,
				ObjectCache = clearableObjectCache
			};
		}

		// Token: 0x0600AC5B RID: 44123 RVA: 0x002373D4 File Offset: 0x002355D4
		private static CacheSet SetupCache(CacheManagerCacheInfo cacheInfo, IEngineHost engineHost, IMessenger messenger)
		{
			PersistentCachePolicy.CacheKind cacheKind = PersistentCachePolicy.CacheKind.Metadata;
			CacheVersion diskMinVersion = null;
			PersistentCacheConfig persistentCacheConfig = null;
			PersistentCache persistentCache = CompoundCacheConfig.CreatePersistentCache(cacheInfo.CacheConfig, delegate(PersistentCacheConfig config)
			{
				CacheVersion cacheVersion;
				PersistentCache persistentCache2 = RemotePersistentCacheFactory.CreatePersistentCache(cacheKind, config, engineHost, messenger, out cacheVersion);
				diskMinVersion = diskMinVersion ?? cacheVersion;
				persistentCacheConfig = persistentCacheConfig ?? config;
				return persistentCache2;
			});
			PersistentCacheConfig persistentCacheConfig2;
			return RemotePersistentCacheFactory.CreateCacheSet(cacheKind, persistentCache, persistentCacheConfig, diskMinVersion, engineHost, out persistentCacheConfig2);
		}

		// Token: 0x0600AC5C RID: 44124 RVA: 0x00237444 File Offset: 0x00235644
		public static PersistentCacheConfig ReadPersistentCacheConfig(BinaryReader reader)
		{
			PersistentCacheMode persistentCacheMode = (PersistentCacheMode)reader.ReadInt32();
			string text = reader.ReadString();
			long num = reader.ReadInt64();
			long num2 = reader.ReadInt64();
			long num3 = reader.ReadInt64();
			int num4 = reader.ReadInt32();
			int num5 = reader.ReadInt32();
			bool flag = reader.ReadBoolean();
			bool flag2 = reader.ReadBoolean();
			bool flag3 = reader.ReadBoolean();
			DateTime dateTime = reader.ReadDateTime();
			CacheVersion cacheVersion = reader.ReadCacheVersion();
			string text2 = reader.ReadNullableString();
			int num6 = reader.ReadInt32();
			return new PersistentCacheConfig(persistentCacheMode, text, num, num2, num3, num4, num5, flag, flag2, flag3, dateTime, cacheVersion, text2, num6);
		}

		// Token: 0x0600AC5D RID: 44125 RVA: 0x002374D8 File Offset: 0x002356D8
		public static void WritePersistentCacheConfig(BinaryWriter writer, PersistentCacheConfig config)
		{
			writer.WriteInt32((int)config.Mode);
			writer.WriteString(config.Directory);
			writer.WriteInt64(config.MaxCacheSize);
			writer.WriteInt64(config.TrimCacheSize);
			writer.WriteInt64(config.MaxEntryLength);
			writer.WriteInt32(config.MaxObjectCacheSize);
			writer.WriteInt32(config.TrimObjectCacheSize);
			writer.WriteBool(config.RefreshData);
			writer.WriteBool(config.CancelCommitsOnDispose);
			writer.WriteBool(config.UserSpecific);
			writer.WriteDateTime(config.MaxStaleness);
			writer.WriteCacheVersion(config.DiskMinVersion);
			writer.WriteNullableString(config.EncryptionCertificateThumbprint);
			writer.WriteInt32(config.ImplementationVersion);
		}

		// Token: 0x0600AC5E RID: 44126 RVA: 0x00237590 File Offset: 0x00235790
		private static CacheManagerCacheInfo ReadCacheInfo(BinaryReader reader)
		{
			string text = reader.ReadString();
			byte b = reader.ReadByte();
			object obj;
			if (b != 1)
			{
				if (b != 2)
				{
					obj = null;
				}
				else
				{
					obj = CompoundCacheConfig.Deserialize(reader);
				}
			}
			else
			{
				obj = RemotePersistentCacheFactory.ReadPersistentCacheConfig(reader);
			}
			return new CacheManagerCacheInfo
			{
				Identifier = text,
				CacheConfig = obj
			};
		}

		// Token: 0x0600AC5F RID: 44127 RVA: 0x002375E4 File Offset: 0x002357E4
		private static void WriteCacheInfo(BinaryWriter writer, CacheManagerCacheInfo info)
		{
			writer.WriteString(info.Identifier);
			PersistentCacheConfig persistentCacheConfig = info.CacheConfig as PersistentCacheConfig;
			if (persistentCacheConfig != null)
			{
				writer.WriteByte(1);
				RemotePersistentCacheFactory.WritePersistentCacheConfig(writer, persistentCacheConfig);
				return;
			}
			CompoundCacheConfig compoundCacheConfig = info.CacheConfig as CompoundCacheConfig;
			if (compoundCacheConfig != null)
			{
				writer.WriteByte(2);
				compoundCacheConfig.Serialize(writer);
				return;
			}
			writer.WriteByte(0);
		}

		// Token: 0x02001AC9 RID: 6857
		private sealed class ReplaceableCacheSet : ICacheSet, IDisposable, IScopedReplaceable<ICacheSet>
		{
			// Token: 0x0600AC61 RID: 44129 RVA: 0x00237640 File Offset: 0x00235840
			public ReplaceableCacheSet(ICacheSet value)
			{
				this.value = value;
			}

			// Token: 0x17002B69 RID: 11113
			// (get) Token: 0x0600AC62 RID: 44130 RVA: 0x0023764F File Offset: 0x0023584F
			public IPersistentCache PersistentCache
			{
				get
				{
					return this.value.PersistentCache;
				}
			}

			// Token: 0x17002B6A RID: 11114
			// (get) Token: 0x0600AC63 RID: 44131 RVA: 0x0023765C File Offset: 0x0023585C
			public IPersistentObjectCache PersistentObjectCache
			{
				get
				{
					return this.value.PersistentObjectCache;
				}
			}

			// Token: 0x17002B6B RID: 11115
			// (get) Token: 0x0600AC64 RID: 44132 RVA: 0x00237669 File Offset: 0x00235869
			public IObjectCache ObjectCache
			{
				get
				{
					return this.value.ObjectCache;
				}
			}

			// Token: 0x0600AC65 RID: 44133 RVA: 0x00237676 File Offset: 0x00235876
			public void Dispose()
			{
				if (this.value != null)
				{
					this.value.Dispose();
					this.value = null;
				}
			}

			// Token: 0x0600AC66 RID: 44134 RVA: 0x00237692 File Offset: 0x00235892
			public IDisposable ReplaceWith(ICacheSet value)
			{
				IDisposable disposable = new RemotePersistentCacheFactory.ReplaceableCacheSet.PreviousCacheSet(this, this.value);
				this.value = value;
				return disposable;
			}

			// Token: 0x0400592C RID: 22828
			private ICacheSet value;

			// Token: 0x02001ACA RID: 6858
			private sealed class PreviousCacheSet : IDisposable
			{
				// Token: 0x0600AC67 RID: 44135 RVA: 0x002376A7 File Offset: 0x002358A7
				public PreviousCacheSet(RemotePersistentCacheFactory.ReplaceableCacheSet parent, ICacheSet oldValue)
				{
					this.parent = parent;
					this.oldValue = oldValue;
				}

				// Token: 0x0600AC68 RID: 44136 RVA: 0x002376C0 File Offset: 0x002358C0
				public void Dispose()
				{
					if (this.parent != null)
					{
						if (this.parent.value != null)
						{
							this.parent.value.Dispose();
							this.parent.value = this.oldValue;
						}
						else
						{
							this.oldValue.Dispose();
						}
						this.parent = null;
						this.oldValue = null;
					}
				}

				// Token: 0x0400592D RID: 22829
				private RemotePersistentCacheFactory.ReplaceableCacheSet parent;

				// Token: 0x0400592E RID: 22830
				private ICacheSet oldValue;
			}
		}

		// Token: 0x02001ACB RID: 6859
		private sealed class Proxy : IRemoteServiceProxy, IEngineHost, IDisposable
		{
			// Token: 0x0600AC69 RID: 44137 RVA: 0x0023771E File Offset: 0x0023591E
			public Proxy(CacheSetsConfig cacheSetsConfig, ICacheSets cacheSets, ICacheManager cacheManager)
			{
				this.cacheSetsConfig = cacheSetsConfig;
				this.cacheSets = cacheSets;
				this.cacheManager = cacheManager;
			}

			// Token: 0x0600AC6A RID: 44138 RVA: 0x0023773C File Offset: 0x0023593C
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(CacheSetsConfig))
				{
					return (T)((object)this.cacheSetsConfig);
				}
				if (typeof(T) == typeof(ICacheSets))
				{
					return (T)((object)this.cacheSets);
				}
				if (typeof(T) == typeof(ICacheManager))
				{
					return (T)((object)this.cacheManager);
				}
				return default(T);
			}

			// Token: 0x0600AC6B RID: 44139 RVA: 0x002377C8 File Offset: 0x002359C8
			public void Dispose()
			{
				if (this.cacheSets != null)
				{
					this.cacheSets.Dispose();
					this.cacheSets = null;
				}
				IDisposable disposable = this.cacheManager as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this.cacheSetsConfig = null;
				this.cacheManager = null;
			}

			// Token: 0x0400592F RID: 22831
			private CacheSetsConfig cacheSetsConfig;

			// Token: 0x04005930 RID: 22832
			private ICacheSets cacheSets;

			// Token: 0x04005931 RID: 22833
			private ICacheManager cacheManager;
		}

		// Token: 0x02001ACC RID: 6860
		public sealed class RemotePersistentCache : PersistentCache
		{
			// Token: 0x0600AC6C RID: 44140 RVA: 0x00237814 File Offset: 0x00235A14
			public RemotePersistentCache(IEngineHost engineHost, IMessenger messenger, string directory, long maxEntryLength, bool userSpecific)
			{
				this.directory = directory;
				this.maxEntryLength = maxEntryLength;
				this.overflowCache = new OverflowPersistentCache(new NullPersistentCache(), engineHost.QueryService<ITempPageService>());
				this.engineHost = engineHost;
				this.messenger = messenger;
				this.userSpecific = userSpecific;
			}

			// Token: 0x17002B6C RID: 11116
			// (get) Token: 0x0600AC6D RID: 44141 RVA: 0x00237862 File Offset: 0x00235A62
			public override long MaxEntryLength
			{
				get
				{
					return this.maxEntryLength;
				}
			}

			// Token: 0x17002B6D RID: 11117
			// (get) Token: 0x0600AC6E RID: 44142 RVA: 0x0023786C File Offset: 0x00235A6C
			// (set) Token: 0x0600AC6F RID: 44143 RVA: 0x0000336E File Offset: 0x0000156E
			public override DateTime Staleness
			{
				get
				{
					DateTime staleness;
					using (IMessageChannel messageChannel = this.messenger.CreateChannel())
					{
						messageChannel.Post(new RemotePersistentCacheFactory.GetStalenessRequestMessage
						{
							Directory = this.directory
						});
						staleness = messageChannel.WaitFor<RemotePersistentCacheFactory.GetStalenessResponseMessage>().Staleness;
					}
					return staleness;
				}
				set
				{
				}
			}

			// Token: 0x17002B6E RID: 11118
			// (get) Token: 0x0600AC70 RID: 44144 RVA: 0x002378C8 File Offset: 0x00235AC8
			public override CacheSize CacheSize
			{
				get
				{
					CacheSize cacheSize;
					using (IMessageChannel messageChannel = this.messenger.CreateChannel())
					{
						messageChannel.Post(new RemotePersistentCacheFactory.GetCacheSizeRequestMessage
						{
							Directory = this.directory
						});
						cacheSize = messageChannel.WaitFor<RemotePersistentCacheFactory.GetCacheSizeResponseMessage>().CacheSize;
					}
					return cacheSize;
				}
			}

			// Token: 0x17002B6F RID: 11119
			// (get) Token: 0x0600AC71 RID: 44145 RVA: 0x00237924 File Offset: 0x00235B24
			public override bool? UserSpecific
			{
				get
				{
					return new bool?(this.userSpecific);
				}
			}

			// Token: 0x0600AC72 RID: 44146 RVA: 0x00237934 File Offset: 0x00235B34
			public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					string text = Guid.NewGuid().ToString();
					messageChannel.Post(new RemotePersistentCacheFactory.OpenStorageRequestMessage
					{
						Directory = this.directory,
						Handle = text,
						Key = key,
						MaxStaleness = maxStaleness,
						MinVersion = minVersion
					});
					if (messageChannel.WaitFor<RemotePersistentCacheFactory.OpenStorageResponseMessage>().Opened)
					{
						storage = new RemotePersistentCacheFactory.RemotePersistentCache.Storage(this, text);
						return true;
					}
				}
				storage = null;
				return false;
			}

			// Token: 0x0600AC73 RID: 44147 RVA: 0x002379D4 File Offset: 0x00235BD4
			public override IStorage CreateStorage()
			{
				string text = Guid.NewGuid().ToString();
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePersistentCacheFactory.CreateStorageMessage
					{
						Directory = this.directory,
						Handle = text
					});
					messageChannel.WaitFor<RemotePersistentCacheFactory.SyncOpCompleteMessage>();
				}
				return new RemotePersistentCacheFactory.RemotePersistentCache.Storage(this, text);
			}

			// Token: 0x0600AC74 RID: 44148 RVA: 0x00237A4C File Offset: 0x00235C4C
			public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePersistentCacheFactory.CommitStorageMessage
					{
						Directory = this.directory,
						Handle = ((RemotePersistentCacheFactory.RemotePersistentCache.Storage)storage).Handle,
						Key = key,
						MaxVersion = maxVersion
					});
					messageChannel.WaitFor<RemotePersistentCacheFactory.SyncOpCompleteMessage>();
				}
			}

			// Token: 0x0600AC75 RID: 44149 RVA: 0x00237AC0 File Offset: 0x00235CC0
			public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
			{
				return this.overflowCache.OpenStorage(key, maxStaleness, minVersion, pageSize, maxPageCount);
			}

			// Token: 0x0600AC76 RID: 44150 RVA: 0x00237AD4 File Offset: 0x00235CD4
			public override void Dispose()
			{
				this.messenger = null;
				this.engineHost = null;
				this.overflowCache.Dispose();
			}

			// Token: 0x0600AC77 RID: 44151 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Purge()
			{
			}

			// Token: 0x17002B70 RID: 11120
			// (get) Token: 0x0600AC78 RID: 44152 RVA: 0x00237AF0 File Offset: 0x00235CF0
			public override CacheVersion Current
			{
				get
				{
					CacheVersion version;
					using (IMessageChannel messageChannel = this.messenger.CreateChannel())
					{
						messageChannel.Post(new RemotePersistentCacheFactory.GetCurrentVersionRequestMessage
						{
							Directory = this.directory
						});
						version = messageChannel.WaitFor<RemotePersistentCacheFactory.CacheVersionResponseMessage>().Version;
					}
					return version;
				}
			}

			// Token: 0x0600AC79 RID: 44153 RVA: 0x00237B4C File Offset: 0x00235D4C
			public override CacheVersion Increment()
			{
				CacheVersion version;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePersistentCacheFactory.IncrementVersionRequestMessage
					{
						Directory = this.directory
					});
					version = messageChannel.WaitFor<RemotePersistentCacheFactory.CacheVersionResponseMessage>().Version;
				}
				return version;
			}

			// Token: 0x0600AC7A RID: 44154 RVA: 0x00237BA8 File Offset: 0x00235DA8
			private bool HandleException(Exception exception, bool disposing)
			{
				bool flag;
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemotePersistentCacheFactory/RemotePersistentCache/HandleException", this.engineHost, TraceEventType.Information, null))
				{
					flag = SafeExceptions.TraceIsSafeException(hostTrace, exception) && disposing;
				}
				return flag;
			}

			// Token: 0x04005932 RID: 22834
			private const string tracePrefix = "RemotePersistentCacheFactory/RemotePersistentCache";

			// Token: 0x04005933 RID: 22835
			private readonly string directory;

			// Token: 0x04005934 RID: 22836
			private readonly long maxEntryLength;

			// Token: 0x04005935 RID: 22837
			private readonly bool userSpecific;

			// Token: 0x04005936 RID: 22838
			private readonly IPersistentCache overflowCache;

			// Token: 0x04005937 RID: 22839
			private IEngineHost engineHost;

			// Token: 0x04005938 RID: 22840
			private IMessenger messenger;

			// Token: 0x02001ACD RID: 6861
			private sealed class Storage : IStorage, IDisposable
			{
				// Token: 0x0600AC7B RID: 44155 RVA: 0x00237BF0 File Offset: 0x00235DF0
				public Storage(RemotePersistentCacheFactory.RemotePersistentCache proxy, string handle)
				{
					this.handle = handle;
					this.proxy = proxy;
				}

				// Token: 0x17002B71 RID: 11121
				// (get) Token: 0x0600AC7C RID: 44156 RVA: 0x00237C06 File Offset: 0x00235E06
				public string Handle
				{
					get
					{
						return this.handle;
					}
				}

				// Token: 0x17002B72 RID: 11122
				// (get) Token: 0x0600AC7D RID: 44157 RVA: 0x00237C10 File Offset: 0x00235E10
				public IEnumerable<int> StreamIds
				{
					get
					{
						IEnumerable<int> streamIds;
						using (IMessageChannel messageChannel = this.proxy.messenger.CreateChannel())
						{
							messageChannel.Post(new RemotePersistentCacheFactory.GetStreamIdsRequestMessage
							{
								Handle = this.handle
							});
							streamIds = messageChannel.WaitFor<RemotePersistentCacheFactory.GetStreamIdsResponseMessage>().StreamIds;
						}
						return streamIds;
					}
				}

				// Token: 0x0600AC7E RID: 44158 RVA: 0x00237C70 File Offset: 0x00235E70
				public Stream OpenStream(int id)
				{
					IMessageChannel messageChannel = this.proxy.messenger.CreateChannel();
					Stream stream;
					try
					{
						messageChannel.Post(new RemotePersistentCacheFactory.OpenStreamMessage
						{
							Handle = this.handle,
							Id = id
						});
						stream = RemoteStream.CreateReaderProxy(this.proxy.engineHost, messageChannel, new ExceptionHandler(this.proxy.HandleException)).AfterDispose(new Action(messageChannel.Dispose));
					}
					catch
					{
						messageChannel.Dispose();
						throw;
					}
					return stream;
				}

				// Token: 0x0600AC7F RID: 44159 RVA: 0x0023017C File Offset: 0x0022E37C
				public Stream CreateStream()
				{
					return new MemoryPagesStream();
				}

				// Token: 0x0600AC80 RID: 44160 RVA: 0x00237D00 File Offset: 0x00235F00
				public Stream CommitStream(int id, Stream stream)
				{
					Stream stream2;
					using (IMessageChannel messageChannel = this.proxy.messenger.CreateChannel())
					{
						messageChannel.Post(new RemotePersistentCacheFactory.CommitStreamMessage
						{
							Handle = this.handle,
							Id = id
						});
						stream.Position = 0L;
						RemoteStream.RunStub(this.proxy.engineHost, messageChannel, () => stream.NonDisposable());
						messageChannel.WaitFor<RemotePersistentCacheFactory.SyncOpCompleteMessage>();
						stream.Position = 0L;
						stream2 = stream;
					}
					return stream2;
				}

				// Token: 0x0600AC81 RID: 44161 RVA: 0x00237DAC File Offset: 0x00235FAC
				public void Close()
				{
					this.Dispose();
				}

				// Token: 0x0600AC82 RID: 44162 RVA: 0x00237DB4 File Offset: 0x00235FB4
				public void Dispose()
				{
					if (this.proxy != null)
					{
						using (IMessageChannel messageChannel = this.proxy.messenger.CreateChannel())
						{
							messageChannel.Post(new RemotePersistentCacheFactory.CloseStorageMessage
							{
								Handle = this.handle
							});
							messageChannel.WaitFor<RemotePersistentCacheFactory.SyncOpCompleteMessage>();
						}
						this.proxy = null;
					}
				}

				// Token: 0x04005939 RID: 22841
				private readonly string handle;

				// Token: 0x0400593A RID: 22842
				private RemotePersistentCacheFactory.RemotePersistentCache proxy;
			}
		}

		// Token: 0x02001ACF RID: 6863
		private sealed class CacheManagerProxy : ICacheManager, IDisposable
		{
			// Token: 0x0600AC85 RID: 44165 RVA: 0x00237E29 File Offset: 0x00236029
			public CacheManagerProxy(IEngineHost engineHost, IMessenger messenger, string cacheGroup)
			{
				this.engineHost = engineHost;
				this.messenger = messenger;
				this.cacheGroup = cacheGroup;
				this.syncRoot = new object();
				this.cacheSets = new Dictionary<string, KeyValuePair<CacheManagerCacheInfo, ICacheSet>>(StringComparer.OrdinalIgnoreCase);
			}

			// Token: 0x17002B73 RID: 11123
			// (get) Token: 0x0600AC86 RID: 44166 RVA: 0x00237E61 File Offset: 0x00236061
			public string CacheGroup
			{
				get
				{
					return this.cacheGroup;
				}
			}

			// Token: 0x0600AC87 RID: 44167 RVA: 0x00237E6C File Offset: 0x0023606C
			public CacheManagerCacheInfo CreateCache(IRecordValue configuration)
			{
				CacheManagerCacheInfo cacheManagerCacheInfo = default(CacheManagerCacheInfo);
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePersistentCacheFactory.CreateCacheRequestMessage
					{
						Configuration = new SerializableValue(configuration, 4096, this.syncRoot)
					});
					cacheManagerCacheInfo = messageChannel.WaitFor<RemotePersistentCacheFactory.CreateCacheResponseMessage>().CacheInfo;
				}
				this.SetupCache(cacheManagerCacheInfo);
				return cacheManagerCacheInfo;
			}

			// Token: 0x0600AC88 RID: 44168 RVA: 0x00237EE0 File Offset: 0x002360E0
			private CacheSet SetupCache(CacheManagerCacheInfo cacheInfo)
			{
				CacheSet cacheSet = RemotePersistentCacheFactory.SetupCache(cacheInfo, this.engineHost, this.messenger);
				object obj = this.syncRoot;
				lock (obj)
				{
					this.cacheSets[cacheInfo.Identifier] = new KeyValuePair<CacheManagerCacheInfo, ICacheSet>(cacheInfo, cacheSet);
				}
				return cacheSet;
			}

			// Token: 0x0600AC89 RID: 44169 RVA: 0x00237F48 File Offset: 0x00236148
			public CacheManagerCacheInfo[] ListCaches()
			{
				CacheManagerCacheInfo[] caches;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePersistentCacheFactory.ListCachesRequestMessage());
					caches = messageChannel.WaitFor<RemotePersistentCacheFactory.ListCachesResponseMessage>().Caches;
				}
				object obj = this.syncRoot;
				lock (obj)
				{
					foreach (CacheManagerCacheInfo cacheManagerCacheInfo in caches)
					{
						KeyValuePair<CacheManagerCacheInfo, ICacheSet> keyValuePair;
						if (!this.cacheSets.TryGetValue(cacheManagerCacheInfo.Identifier, out keyValuePair))
						{
							this.cacheSets[cacheManagerCacheInfo.Identifier] = new KeyValuePair<CacheManagerCacheInfo, ICacheSet>(cacheManagerCacheInfo, null);
						}
					}
				}
				return caches;
			}

			// Token: 0x0600AC8A RID: 44170 RVA: 0x00238010 File Offset: 0x00236210
			public void UpdateCache(string identifier, string newIdentifier, bool? readOnly)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePersistentCacheFactory.UpdateCacheRequestMessage
					{
						Identifier = identifier,
						NewIdentifier = newIdentifier,
						ReadOnly = readOnly
					});
					messageChannel.WaitFor<RemotePersistentCacheFactory.CacheOperationResponseMessage>();
					Dictionary<string, KeyValuePair<CacheManagerCacheInfo, ICacheSet>> dictionary = this.cacheSets;
					lock (dictionary)
					{
						KeyValuePair<CacheManagerCacheInfo, ICacheSet> keyValuePair;
						if (this.cacheSets.TryGetValue(identifier, out keyValuePair))
						{
							this.cacheSets[newIdentifier] = keyValuePair;
						}
					}
				}
			}

			// Token: 0x0600AC8B RID: 44171 RVA: 0x002380B0 File Offset: 0x002362B0
			public void DeleteCache(string identifier)
			{
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePersistentCacheFactory.DeleteCacheRequestMessage
					{
						Identifier = identifier
					});
					messageChannel.WaitFor<RemotePersistentCacheFactory.CacheOperationResponseMessage>();
				}
			}

			// Token: 0x0600AC8C RID: 44172 RVA: 0x00238100 File Offset: 0x00236300
			public ICacheSet GetCache(string identifier)
			{
				object obj = this.syncRoot;
				ICacheSet cacheSet;
				lock (obj)
				{
					KeyValuePair<CacheManagerCacheInfo, ICacheSet> keyValuePair;
					if (!this.cacheSets.TryGetValue(identifier, out keyValuePair))
					{
						cacheSet = null;
					}
					else
					{
						if (keyValuePair.Value == null)
						{
							keyValuePair = new KeyValuePair<CacheManagerCacheInfo, ICacheSet>(keyValuePair.Key, this.SetupCache(keyValuePair.Key));
							this.cacheSets[identifier] = keyValuePair;
						}
						cacheSet = keyValuePair.Value;
					}
				}
				return cacheSet;
			}

			// Token: 0x0600AC8D RID: 44173 RVA: 0x00238188 File Offset: 0x00236388
			public CacheManagerCacheInfo GetCacheFromDirectory(string directory)
			{
				CacheManagerCacheInfo cache;
				using (IMessageChannel messageChannel = this.messenger.CreateChannel())
				{
					messageChannel.Post(new RemotePersistentCacheFactory.CacheFromDirectoryRequestMessage
					{
						Directory = directory
					});
					cache = messageChannel.WaitFor<RemotePersistentCacheFactory.CacheFromDirectoryResponseMessage>().Cache;
				}
				return cache;
			}

			// Token: 0x0600AC8E RID: 44174 RVA: 0x002381DC File Offset: 0x002363DC
			public void Dispose()
			{
				foreach (KeyValuePair<CacheManagerCacheInfo, ICacheSet> keyValuePair in this.cacheSets.Values)
				{
					ICacheSet value = keyValuePair.Value;
					if (value != null)
					{
						value.Dispose();
					}
				}
				this.cacheSets.Clear();
			}

			// Token: 0x0400593C RID: 22844
			private const int maxBinaryLength = 4096;

			// Token: 0x0400593D RID: 22845
			private readonly IEngineHost engineHost;

			// Token: 0x0400593E RID: 22846
			private readonly IMessenger messenger;

			// Token: 0x0400593F RID: 22847
			private readonly string cacheGroup;

			// Token: 0x04005940 RID: 22848
			private readonly object syncRoot;

			// Token: 0x04005941 RID: 22849
			private Dictionary<string, KeyValuePair<CacheManagerCacheInfo, ICacheSet>> cacheSets;
		}

		// Token: 0x02001AD0 RID: 6864
		public sealed class Stub : IRemoteServiceStub, IDisposable
		{
			// Token: 0x0600AC8F RID: 44175 RVA: 0x0023824C File Offset: 0x0023644C
			public Stub(IEngineHost engineHost, Dictionary<string, IPersistentCache> caches, ICacheManager cacheManager, IMessenger messenger)
			{
				this.engineHost = engineHost;
				this.caches = caches;
				this.messenger = messenger;
				this.cacheManager = cacheManager;
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.GetStalenessRequestMessage>(this.OnGetStalenessRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.GetCacheSizeRequestMessage>(this.OnGetCacheSizeRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.OpenStorageRequestMessage>(this.OnOpenStorageRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.CreateStorageMessage>(this.OnCreateStorage));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.CommitStorageMessage>(this.OnCommitStorage));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.CloseStorageMessage>(this.OnCloseStorage));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.GetStreamIdsRequestMessage>(this.OnGetStreamIdsRequest));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.OpenStreamMessage>(this.OnOpenStream));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.CommitStreamMessage>(this.OnCommitStream));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.GetCurrentVersionRequestMessage>(this.OnGetCurrentVersion));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.IncrementVersionRequestMessage>(this.OnIncrementVersion));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.CreateCacheRequestMessage>(this.OnCreateCache));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.ListCachesRequestMessage>(this.OnListCaches));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.UpdateCacheRequestMessage>(this.OnUpdateCache));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.DeleteCacheRequestMessage>(this.OnDeleteCache));
				this.messenger.AddHandler(new Action<IMessageChannel, RemotePersistentCacheFactory.CacheFromDirectoryRequestMessage>(this.OnGetCacheFromDirectory));
				this.openStorage = new Dictionary<string, IStorage>();
			}

			// Token: 0x0600AC90 RID: 44176 RVA: 0x002383F8 File Offset: 0x002365F8
			public void Dispose()
			{
				foreach (IStorage storage in this.openStorage.Values)
				{
					storage.Dispose();
				}
				this.openStorage = null;
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.GetStalenessRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.GetCacheSizeRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.OpenStorageRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.CreateStorageMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.CommitStorageMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.CloseStorageMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.GetStreamIdsRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.OpenStreamMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.CommitStreamMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.GetCurrentVersionRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.IncrementVersionRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.CreateCacheRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.ListCachesRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.UpdateCacheRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.DeleteCacheRequestMessage>();
				this.messenger.RemoveHandler<RemotePersistentCacheFactory.CacheFromDirectoryRequestMessage>();
				this.messenger = null;
				this.caches = null;
				this.engineHost = null;
			}

			// Token: 0x0600AC91 RID: 44177 RVA: 0x0023851C File Offset: 0x0023671C
			private IPersistentCache GetCache(RemotePersistentCacheFactory.CacheMessage message)
			{
				Dictionary<string, IPersistentCache> dictionary = this.caches;
				IPersistentCache persistentCache2;
				lock (dictionary)
				{
					IPersistentCache persistentCache;
					if (!this.caches.TryGetValue(message.Directory, out persistentCache))
					{
						CacheManagerCacheInfo cacheFromDirectory = this.cacheManager.GetCacheFromDirectory(message.Directory);
						if (cacheFromDirectory.Identifier != null)
						{
							ICacheSet cache = this.cacheManager.GetCache(cacheFromDirectory.Identifier);
							this.caches[message.Directory] = cache.PersistentCache;
						}
					}
					persistentCache2 = this.caches[message.Directory];
				}
				return persistentCache2;
			}

			// Token: 0x0600AC92 RID: 44178 RVA: 0x002385C8 File Offset: 0x002367C8
			private void OnGetStalenessRequest(IMessageChannel channel, RemotePersistentCacheFactory.GetStalenessRequestMessage message)
			{
				channel.Post(new RemotePersistentCacheFactory.GetStalenessResponseMessage
				{
					Staleness = this.GetCache(message).Staleness
				});
			}

			// Token: 0x0600AC93 RID: 44179 RVA: 0x002385E8 File Offset: 0x002367E8
			private void OnGetStreamIdsRequest(IMessageChannel channel, RemotePersistentCacheFactory.GetStreamIdsRequestMessage message)
			{
				Dictionary<string, IStorage> dictionary = this.openStorage;
				IStorage storage;
				lock (dictionary)
				{
					storage = this.openStorage[message.Handle];
				}
				channel.Post(new RemotePersistentCacheFactory.GetStreamIdsResponseMessage
				{
					StreamIds = ((storage != null) ? storage.StreamIds.ToArray<int>() : EmptyArray<int>.Instance)
				});
			}

			// Token: 0x0600AC94 RID: 44180 RVA: 0x0023865C File Offset: 0x0023685C
			private void OnGetCacheSizeRequest(IMessageChannel channel, RemotePersistentCacheFactory.GetCacheSizeRequestMessage message)
			{
				channel.Post(new RemotePersistentCacheFactory.GetCacheSizeResponseMessage
				{
					CacheSize = this.GetCache(message).CacheSize
				});
			}

			// Token: 0x0600AC95 RID: 44181 RVA: 0x0023867C File Offset: 0x0023687C
			private void OnOpenStorageRequest(IMessageChannel channel, RemotePersistentCacheFactory.OpenStorageRequestMessage message)
			{
				IPersistentCache cache = this.GetCache(message);
				IStorage storage;
				bool flag = ((message.Key != null) ? cache.TryGetStorage(message.Key, message.MaxStaleness, message.MinVersion, out storage) : cache.TryGetStorage(message.StructuredKey, message.MaxStaleness, message.MinVersion, out storage));
				if (flag)
				{
					Dictionary<string, IStorage> dictionary = this.openStorage;
					lock (dictionary)
					{
						this.openStorage.Add(message.Handle, storage);
					}
				}
				channel.Post(new RemotePersistentCacheFactory.OpenStorageResponseMessage
				{
					Opened = flag
				});
			}

			// Token: 0x0600AC96 RID: 44182 RVA: 0x00238728 File Offset: 0x00236928
			private void OnCreateStorage(IMessageChannel channel, RemotePersistentCacheFactory.CreateStorageMessage message)
			{
				IStorage storage = this.GetCache(message).CreateStorage();
				Dictionary<string, IStorage> dictionary = this.openStorage;
				lock (dictionary)
				{
					this.openStorage.Add(message.Handle, storage);
				}
				channel.Post(RemotePersistentCacheFactory.SyncOpCompleteMessage.Instance);
			}

			// Token: 0x0600AC97 RID: 44183 RVA: 0x0023878C File Offset: 0x0023698C
			private void OnCommitStorage(IMessageChannel channel, RemotePersistentCacheFactory.CommitStorageMessage message)
			{
				Dictionary<string, IStorage> dictionary = this.openStorage;
				IStorage storage;
				lock (dictionary)
				{
					storage = this.openStorage[message.Handle];
				}
				IPersistentCache cache = this.GetCache(message);
				if (message.Key != null)
				{
					cache.CommitStorage(message.Key, message.MaxVersion, storage);
				}
				else
				{
					cache.CommitStorage(message.StructuredKey, message.MaxVersion, storage);
				}
				channel.Post(RemotePersistentCacheFactory.SyncOpCompleteMessage.Instance);
			}

			// Token: 0x0600AC98 RID: 44184 RVA: 0x0023881C File Offset: 0x00236A1C
			private void OnCloseStorage(IMessageChannel channel, RemotePersistentCacheFactory.CloseStorageMessage message)
			{
				Dictionary<string, IStorage> dictionary = this.openStorage;
				IStorage storage;
				lock (dictionary)
				{
					storage = this.openStorage[message.Handle];
					this.openStorage.Remove(message.Handle);
				}
				storage.Dispose();
				channel.Post(RemotePersistentCacheFactory.SyncOpCompleteMessage.Instance);
			}

			// Token: 0x0600AC99 RID: 44185 RVA: 0x0023888C File Offset: 0x00236A8C
			private void OnOpenStream(IMessageChannel channel, RemotePersistentCacheFactory.OpenStreamMessage message)
			{
				Dictionary<string, IStorage> dictionary = this.openStorage;
				IStorage storage;
				lock (dictionary)
				{
					storage = this.openStorage[message.Handle];
				}
				RemoteStream.RunStub(this.engineHost, channel, () => storage.OpenStream(message.Id));
			}

			// Token: 0x0600AC9A RID: 44186 RVA: 0x00238908 File Offset: 0x00236B08
			private void OnCommitStream(IMessageChannel channel, RemotePersistentCacheFactory.CommitStreamMessage message)
			{
				using (Stream stream = RemoteStream.CreateReaderProxy(this.engineHost, channel, new ExceptionHandler(this.HandleException)))
				{
					Dictionary<string, IStorage> dictionary = this.openStorage;
					IStorage storage;
					lock (dictionary)
					{
						storage = this.openStorage[message.Handle];
					}
					Stream stream2 = storage.CreateStream();
					stream.CopyTo(stream2);
					stream2 = storage.CommitStream(message.Id, stream2);
					stream2.Dispose();
				}
				channel.Post(RemotePersistentCacheFactory.SyncOpCompleteMessage.Instance);
			}

			// Token: 0x0600AC9B RID: 44187 RVA: 0x002389B4 File Offset: 0x00236BB4
			private void OnGetCurrentVersion(IMessageChannel channel, RemotePersistentCacheFactory.GetCurrentVersionRequestMessage message)
			{
				channel.Post(new RemotePersistentCacheFactory.CacheVersionResponseMessage
				{
					Version = this.GetCache(message).CacheClock.Current
				});
			}

			// Token: 0x0600AC9C RID: 44188 RVA: 0x002389D8 File Offset: 0x00236BD8
			private void OnIncrementVersion(IMessageChannel channel, RemotePersistentCacheFactory.IncrementVersionRequestMessage message)
			{
				channel.Post(new RemotePersistentCacheFactory.CacheVersionResponseMessage
				{
					Version = this.GetCache(message).CacheClock.Increment()
				});
			}

			// Token: 0x0600AC9D RID: 44189 RVA: 0x002389FC File Offset: 0x00236BFC
			private void OnCreateCache(IMessageChannel channel, RemotePersistentCacheFactory.CreateCacheRequestMessage request)
			{
				EvaluationHost.ReportExceptions("RemotePersistentCacheFactory/Stub/OnCreateCache", this.engineHost, channel, delegate
				{
					CacheManagerCacheInfo cacheManagerCacheInfo = this.cacheManager.CreateCache(request.Configuration.GetValue().AsRecord);
					ICacheSet cache = this.cacheManager.GetCache(cacheManagerCacheInfo.Identifier);
					Dictionary<string, IPersistentCache> dictionary = this.caches;
					lock (dictionary)
					{
						this.caches[cacheManagerCacheInfo.Identifier] = cache.PersistentCache;
					}
					channel.Post(new RemotePersistentCacheFactory.CreateCacheResponseMessage
					{
						CacheInfo = cacheManagerCacheInfo
					});
				});
			}

			// Token: 0x0600AC9E RID: 44190 RVA: 0x00238A46 File Offset: 0x00236C46
			private void OnListCaches(IMessageChannel channel, RemotePersistentCacheFactory.ListCachesRequestMessage request)
			{
				channel.Post(new RemotePersistentCacheFactory.ListCachesResponseMessage
				{
					Caches = this.cacheManager.ListCaches()
				});
			}

			// Token: 0x0600AC9F RID: 44191 RVA: 0x00238A64 File Offset: 0x00236C64
			private void OnUpdateCache(IMessageChannel channel, RemotePersistentCacheFactory.UpdateCacheRequestMessage request)
			{
				EvaluationHost.ReportExceptions("RemotePersistentCacheFactory/Stub/OnUpdateCache", this.engineHost, channel, delegate
				{
					this.cacheManager.UpdateCache(request.Identifier, request.NewIdentifier, request.ReadOnly);
					Dictionary<string, IPersistentCache> dictionary = this.caches;
					lock (dictionary)
					{
						IPersistentCache persistentCache;
						if (this.caches.TryGetValue(request.Identifier, out persistentCache))
						{
							this.caches[request.Identifier] = persistentCache;
						}
					}
					channel.Post(new RemotePersistentCacheFactory.CacheOperationResponseMessage());
				});
			}

			// Token: 0x0600ACA0 RID: 44192 RVA: 0x00238AB0 File Offset: 0x00236CB0
			private void OnDeleteCache(IMessageChannel channel, RemotePersistentCacheFactory.DeleteCacheRequestMessage request)
			{
				EvaluationHost.ReportExceptions("RemotePersistentCacheFactory/Stub/OnUpdateCache", this.engineHost, channel, delegate
				{
					this.cacheManager.DeleteCache(request.Identifier);
					channel.Post(new RemotePersistentCacheFactory.CacheOperationResponseMessage());
				});
			}

			// Token: 0x0600ACA1 RID: 44193 RVA: 0x00238AFC File Offset: 0x00236CFC
			private void OnGetCacheFromDirectory(IMessageChannel channel, RemotePersistentCacheFactory.CacheFromDirectoryRequestMessage request)
			{
				EvaluationHost.ReportExceptions("RemotePersistentCacheFactory/Stub/OnGetCacheFromDirectory", this.engineHost, channel, delegate
				{
					CacheManagerCacheInfo cacheFromDirectory = this.cacheManager.GetCacheFromDirectory(request.Directory);
					channel.Post(new RemotePersistentCacheFactory.CacheFromDirectoryResponseMessage
					{
						Cache = cacheFromDirectory
					});
				});
			}

			// Token: 0x0600ACA2 RID: 44194 RVA: 0x00238B48 File Offset: 0x00236D48
			private bool HandleException(Exception exception, bool disposing)
			{
				bool flag;
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemotePersistentCacheFactory/Stub/HandleException", this.engineHost, TraceEventType.Information, null))
				{
					flag = SafeExceptions.TraceIsSafeException(hostTrace, exception) && disposing;
				}
				return flag;
			}

			// Token: 0x04005942 RID: 22850
			private const string tracePrefix = "RemotePersistentCacheFactory/Stub";

			// Token: 0x04005943 RID: 22851
			private IEngineHost engineHost;

			// Token: 0x04005944 RID: 22852
			private IMessenger messenger;

			// Token: 0x04005945 RID: 22853
			private ICacheManager cacheManager;

			// Token: 0x04005946 RID: 22854
			private Dictionary<string, IPersistentCache> caches;

			// Token: 0x04005947 RID: 22855
			private Dictionary<string, IStorage> openStorage;
		}

		// Token: 0x02001AD6 RID: 6870
		private class CacheMessage : BufferedMessage
		{
			// Token: 0x17002B74 RID: 11124
			// (get) Token: 0x0600ACAD RID: 44205 RVA: 0x00238D84 File Offset: 0x00236F84
			// (set) Token: 0x0600ACAE RID: 44206 RVA: 0x00238D8C File Offset: 0x00236F8C
			public string Directory { get; set; }

			// Token: 0x0600ACAF RID: 44207 RVA: 0x00238D95 File Offset: 0x00236F95
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullableString(this.Directory);
			}

			// Token: 0x0600ACB0 RID: 44208 RVA: 0x00238DA3 File Offset: 0x00236FA3
			public override void Deserialize(BinaryReader reader)
			{
				this.Directory = reader.ReadNullableString();
			}
		}

		// Token: 0x02001AD7 RID: 6871
		private sealed class GetStalenessRequestMessage : RemotePersistentCacheFactory.CacheMessage
		{
		}

		// Token: 0x02001AD8 RID: 6872
		private sealed class GetStalenessResponseMessage : BufferedMessage
		{
			// Token: 0x17002B75 RID: 11125
			// (get) Token: 0x0600ACB3 RID: 44211 RVA: 0x00238DB9 File Offset: 0x00236FB9
			// (set) Token: 0x0600ACB4 RID: 44212 RVA: 0x00238DC1 File Offset: 0x00236FC1
			public DateTime Staleness { get; set; }

			// Token: 0x0600ACB5 RID: 44213 RVA: 0x00238DCA File Offset: 0x00236FCA
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteDateTime(this.Staleness);
			}

			// Token: 0x0600ACB6 RID: 44214 RVA: 0x00238DD8 File Offset: 0x00236FD8
			public override void Deserialize(BinaryReader reader)
			{
				this.Staleness = reader.ReadDateTime();
			}
		}

		// Token: 0x02001AD9 RID: 6873
		private sealed class GetCacheSizeRequestMessage : RemotePersistentCacheFactory.CacheMessage
		{
		}

		// Token: 0x02001ADA RID: 6874
		private sealed class GetCacheSizeResponseMessage : BufferedMessage
		{
			// Token: 0x17002B76 RID: 11126
			// (get) Token: 0x0600ACB9 RID: 44217 RVA: 0x00238DE6 File Offset: 0x00236FE6
			// (set) Token: 0x0600ACBA RID: 44218 RVA: 0x00238DEE File Offset: 0x00236FEE
			public CacheSize CacheSize { get; set; }

			// Token: 0x0600ACBB RID: 44219 RVA: 0x00238DF8 File Offset: 0x00236FF8
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.CacheSize.EntryCount);
				writer.WriteInt64(this.CacheSize.TotalSize);
			}

			// Token: 0x0600ACBC RID: 44220 RVA: 0x00238E2D File Offset: 0x0023702D
			public override void Deserialize(BinaryReader reader)
			{
				this.CacheSize = new CacheSize(reader.ReadInt32(), reader.ReadInt64());
			}
		}

		// Token: 0x02001ADB RID: 6875
		private sealed class GetStreamIdsRequestMessage : RemotePersistentCacheFactory.StorageMessage
		{
		}

		// Token: 0x02001ADC RID: 6876
		private sealed class GetStreamIdsResponseMessage : BufferedMessage
		{
			// Token: 0x17002B77 RID: 11127
			// (get) Token: 0x0600ACBF RID: 44223 RVA: 0x00238E4E File Offset: 0x0023704E
			// (set) Token: 0x0600ACC0 RID: 44224 RVA: 0x00238E56 File Offset: 0x00237056
			public int[] StreamIds { get; set; }

			// Token: 0x0600ACC1 RID: 44225 RVA: 0x00238E5F File Offset: 0x0023705F
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteArray(this.StreamIds, delegate(BinaryWriter itemWriter, int item)
				{
					itemWriter.WriteInt32(item);
				});
			}

			// Token: 0x0600ACC2 RID: 44226 RVA: 0x00238E8C File Offset: 0x0023708C
			public override void Deserialize(BinaryReader reader)
			{
				this.StreamIds = reader.ReadArray((BinaryReader item) => item.ReadInt32());
			}
		}

		// Token: 0x02001ADE RID: 6878
		private sealed class OpenStorageResponseMessage : BufferedMessage
		{
			// Token: 0x17002B78 RID: 11128
			// (get) Token: 0x0600ACC8 RID: 44232 RVA: 0x00238ED6 File Offset: 0x002370D6
			// (set) Token: 0x0600ACC9 RID: 44233 RVA: 0x00238EDE File Offset: 0x002370DE
			public bool Opened { get; set; }

			// Token: 0x0600ACCA RID: 44234 RVA: 0x00238EE7 File Offset: 0x002370E7
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteBool(this.Opened);
			}

			// Token: 0x0600ACCB RID: 44235 RVA: 0x00238EF5 File Offset: 0x002370F5
			public override void Deserialize(BinaryReader reader)
			{
				this.Opened = reader.ReadBool();
			}
		}

		// Token: 0x02001ADF RID: 6879
		private abstract class StorageMessage : RemotePersistentCacheFactory.CacheMessage
		{
			// Token: 0x17002B79 RID: 11129
			// (get) Token: 0x0600ACCD RID: 44237 RVA: 0x00238F03 File Offset: 0x00237103
			// (set) Token: 0x0600ACCE RID: 44238 RVA: 0x00238F0B File Offset: 0x0023710B
			public string Handle { get; set; }

			// Token: 0x0600ACCF RID: 44239 RVA: 0x00238F14 File Offset: 0x00237114
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteString(this.Handle);
			}

			// Token: 0x0600ACD0 RID: 44240 RVA: 0x00238F29 File Offset: 0x00237129
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.Handle = reader.ReadString();
			}
		}

		// Token: 0x02001AE0 RID: 6880
		private abstract class KeyedStorageMessage : RemotePersistentCacheFactory.StorageMessage
		{
			// Token: 0x17002B7A RID: 11130
			// (get) Token: 0x0600ACD2 RID: 44242 RVA: 0x00238F3E File Offset: 0x0023713E
			// (set) Token: 0x0600ACD3 RID: 44243 RVA: 0x00238F46 File Offset: 0x00237146
			public string Key { get; set; }

			// Token: 0x17002B7B RID: 11131
			// (get) Token: 0x0600ACD4 RID: 44244 RVA: 0x00238F4F File Offset: 0x0023714F
			// (set) Token: 0x0600ACD5 RID: 44245 RVA: 0x00238F57 File Offset: 0x00237157
			public StructuredCacheKey StructuredKey { get; set; }

			// Token: 0x0600ACD6 RID: 44246 RVA: 0x00238F60 File Offset: 0x00237160
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteNullableString(this.Key);
				writer.WriteNullable(this.StructuredKey, new Action<BinaryWriter, StructuredCacheKey>(StructuredCacheKeySerializationExtensions.WriteStructuredCacheKey));
			}

			// Token: 0x0600ACD7 RID: 44247 RVA: 0x00238F8D File Offset: 0x0023718D
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.Key = reader.ReadNullableString();
				this.StructuredKey = reader.ReadNullable(new Func<BinaryReader, StructuredCacheKey>(StructuredCacheKeySerializationExtensions.ReadStructuredCacheKey));
			}
		}

		// Token: 0x02001AE1 RID: 6881
		private sealed class OpenStorageRequestMessage : RemotePersistentCacheFactory.KeyedStorageMessage
		{
			// Token: 0x17002B7C RID: 11132
			// (get) Token: 0x0600ACD9 RID: 44249 RVA: 0x00238FBA File Offset: 0x002371BA
			// (set) Token: 0x0600ACDA RID: 44250 RVA: 0x00238FC2 File Offset: 0x002371C2
			public DateTime MaxStaleness { get; set; }

			// Token: 0x17002B7D RID: 11133
			// (get) Token: 0x0600ACDB RID: 44251 RVA: 0x00238FCB File Offset: 0x002371CB
			// (set) Token: 0x0600ACDC RID: 44252 RVA: 0x00238FD3 File Offset: 0x002371D3
			public CacheVersion MinVersion { get; set; }

			// Token: 0x0600ACDD RID: 44253 RVA: 0x00238FDC File Offset: 0x002371DC
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteDateTime(this.MaxStaleness);
				writer.WriteCacheVersion(this.MinVersion);
			}

			// Token: 0x0600ACDE RID: 44254 RVA: 0x00238FFD File Offset: 0x002371FD
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.MaxStaleness = reader.ReadDateTime();
				this.MinVersion = reader.ReadCacheVersion();
			}
		}

		// Token: 0x02001AE2 RID: 6882
		private sealed class CreateStorageMessage : RemotePersistentCacheFactory.StorageMessage
		{
		}

		// Token: 0x02001AE3 RID: 6883
		private sealed class CloseStorageMessage : RemotePersistentCacheFactory.StorageMessage
		{
		}

		// Token: 0x02001AE4 RID: 6884
		private sealed class CommitStorageMessage : RemotePersistentCacheFactory.KeyedStorageMessage
		{
			// Token: 0x17002B7E RID: 11134
			// (get) Token: 0x0600ACE2 RID: 44258 RVA: 0x00239026 File Offset: 0x00237226
			// (set) Token: 0x0600ACE3 RID: 44259 RVA: 0x0023902E File Offset: 0x0023722E
			public CacheVersion MaxVersion { get; set; }

			// Token: 0x0600ACE4 RID: 44260 RVA: 0x00239037 File Offset: 0x00237237
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteCacheVersion(this.MaxVersion);
			}

			// Token: 0x0600ACE5 RID: 44261 RVA: 0x0023904C File Offset: 0x0023724C
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.MaxVersion = reader.ReadCacheVersion();
			}
		}

		// Token: 0x02001AE5 RID: 6885
		private abstract class StreamMessage : RemotePersistentCacheFactory.StorageMessage
		{
			// Token: 0x17002B7F RID: 11135
			// (get) Token: 0x0600ACE7 RID: 44263 RVA: 0x00239061 File Offset: 0x00237261
			// (set) Token: 0x0600ACE8 RID: 44264 RVA: 0x00239069 File Offset: 0x00237269
			public int Id { get; set; }

			// Token: 0x0600ACE9 RID: 44265 RVA: 0x00239072 File Offset: 0x00237272
			public override void Serialize(BinaryWriter writer)
			{
				base.Serialize(writer);
				writer.WriteInt32(this.Id);
			}

			// Token: 0x0600ACEA RID: 44266 RVA: 0x00239087 File Offset: 0x00237287
			public override void Deserialize(BinaryReader reader)
			{
				base.Deserialize(reader);
				this.Id = reader.ReadInt32();
			}
		}

		// Token: 0x02001AE6 RID: 6886
		private sealed class OpenStreamMessage : RemotePersistentCacheFactory.StreamMessage
		{
		}

		// Token: 0x02001AE7 RID: 6887
		private sealed class CommitStreamMessage : RemotePersistentCacheFactory.StreamMessage
		{
		}

		// Token: 0x02001AE8 RID: 6888
		private sealed class GetCurrentVersionRequestMessage : RemotePersistentCacheFactory.CacheMessage
		{
		}

		// Token: 0x02001AE9 RID: 6889
		private sealed class IncrementVersionRequestMessage : RemotePersistentCacheFactory.CacheMessage
		{
		}

		// Token: 0x02001AEA RID: 6890
		private sealed class CacheVersionResponseMessage : BufferedMessage
		{
			// Token: 0x17002B80 RID: 11136
			// (get) Token: 0x0600ACF0 RID: 44272 RVA: 0x002390A4 File Offset: 0x002372A4
			// (set) Token: 0x0600ACF1 RID: 44273 RVA: 0x002390AC File Offset: 0x002372AC
			public CacheVersion Version { get; set; }

			// Token: 0x0600ACF2 RID: 44274 RVA: 0x002390B5 File Offset: 0x002372B5
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteCacheVersion(this.Version);
			}

			// Token: 0x0600ACF3 RID: 44275 RVA: 0x002390C3 File Offset: 0x002372C3
			public override void Deserialize(BinaryReader reader)
			{
				this.Version = reader.ReadCacheVersion();
			}
		}

		// Token: 0x02001AEB RID: 6891
		private sealed class SyncOpCompleteMessage : BufferedMessage
		{
			// Token: 0x0600ACF5 RID: 44277 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600ACF6 RID: 44278 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}

			// Token: 0x04005966 RID: 22886
			public static readonly RemotePersistentCacheFactory.SyncOpCompleteMessage Instance = new RemotePersistentCacheFactory.SyncOpCompleteMessage();
		}

		// Token: 0x02001AEC RID: 6892
		private sealed class CreateCacheRequestMessage : BufferedMessage
		{
			// Token: 0x17002B81 RID: 11137
			// (get) Token: 0x0600ACF9 RID: 44281 RVA: 0x002390DD File Offset: 0x002372DD
			// (set) Token: 0x0600ACFA RID: 44282 RVA: 0x002390E5 File Offset: 0x002372E5
			public ISerializableValue Configuration { get; set; }

			// Token: 0x0600ACFB RID: 44283 RVA: 0x002390EE File Offset: 0x002372EE
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteByteArray(this.Configuration.GetBytes());
			}

			// Token: 0x0600ACFC RID: 44284 RVA: 0x00239101 File Offset: 0x00237301
			public override void Deserialize(BinaryReader reader)
			{
				this.Configuration = new SerializableValue(reader.ReadByteArray());
			}
		}

		// Token: 0x02001AED RID: 6893
		private sealed class CreateCacheResponseMessage : BufferedMessage
		{
			// Token: 0x17002B82 RID: 11138
			// (get) Token: 0x0600ACFE RID: 44286 RVA: 0x00239114 File Offset: 0x00237314
			// (set) Token: 0x0600ACFF RID: 44287 RVA: 0x0023911C File Offset: 0x0023731C
			public CacheManagerCacheInfo CacheInfo { get; set; }

			// Token: 0x0600AD00 RID: 44288 RVA: 0x00239125 File Offset: 0x00237325
			public override void Serialize(BinaryWriter writer)
			{
				RemotePersistentCacheFactory.WriteCacheInfo(writer, this.CacheInfo);
			}

			// Token: 0x0600AD01 RID: 44289 RVA: 0x00239133 File Offset: 0x00237333
			public override void Deserialize(BinaryReader reader)
			{
				this.CacheInfo = RemotePersistentCacheFactory.ReadCacheInfo(reader);
			}
		}

		// Token: 0x02001AEE RID: 6894
		private sealed class ListCachesRequestMessage : BufferedMessage
		{
			// Token: 0x0600AD03 RID: 44291 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AD04 RID: 44292 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}

		// Token: 0x02001AEF RID: 6895
		private sealed class ListCachesResponseMessage : BufferedMessage
		{
			// Token: 0x17002B83 RID: 11139
			// (get) Token: 0x0600AD06 RID: 44294 RVA: 0x00239141 File Offset: 0x00237341
			// (set) Token: 0x0600AD07 RID: 44295 RVA: 0x00239149 File Offset: 0x00237349
			public CacheManagerCacheInfo[] Caches { get; set; }

			// Token: 0x0600AD08 RID: 44296 RVA: 0x00239152 File Offset: 0x00237352
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteArray(this.Caches, new Action<BinaryWriter, CacheManagerCacheInfo>(RemotePersistentCacheFactory.WriteCacheInfo));
			}

			// Token: 0x0600AD09 RID: 44297 RVA: 0x0023916C File Offset: 0x0023736C
			public override void Deserialize(BinaryReader reader)
			{
				this.Caches = reader.ReadArray(new Func<BinaryReader, CacheManagerCacheInfo>(RemotePersistentCacheFactory.ReadCacheInfo));
			}
		}

		// Token: 0x02001AF0 RID: 6896
		private sealed class UpdateCacheRequestMessage : BufferedMessage
		{
			// Token: 0x17002B84 RID: 11140
			// (get) Token: 0x0600AD0B RID: 44299 RVA: 0x00239186 File Offset: 0x00237386
			// (set) Token: 0x0600AD0C RID: 44300 RVA: 0x0023918E File Offset: 0x0023738E
			public string Identifier { get; set; }

			// Token: 0x17002B85 RID: 11141
			// (get) Token: 0x0600AD0D RID: 44301 RVA: 0x00239197 File Offset: 0x00237397
			// (set) Token: 0x0600AD0E RID: 44302 RVA: 0x0023919F File Offset: 0x0023739F
			public string NewIdentifier { get; set; }

			// Token: 0x17002B86 RID: 11142
			// (get) Token: 0x0600AD0F RID: 44303 RVA: 0x002391A8 File Offset: 0x002373A8
			// (set) Token: 0x0600AD10 RID: 44304 RVA: 0x002391B0 File Offset: 0x002373B0
			public bool? ReadOnly { get; set; }

			// Token: 0x0600AD11 RID: 44305 RVA: 0x002391B9 File Offset: 0x002373B9
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.Identifier);
				writer.WriteNullableString(this.NewIdentifier);
				writer.WriteNullableBool(this.ReadOnly);
			}

			// Token: 0x0600AD12 RID: 44306 RVA: 0x002391DF File Offset: 0x002373DF
			public override void Deserialize(BinaryReader reader)
			{
				this.Identifier = reader.ReadString();
				this.NewIdentifier = reader.ReadNullableString();
				this.ReadOnly = reader.ReadNullableBool();
			}
		}

		// Token: 0x02001AF1 RID: 6897
		private sealed class CacheOperationResponseMessage : BufferedMessage
		{
			// Token: 0x0600AD14 RID: 44308 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600AD15 RID: 44309 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}

		// Token: 0x02001AF2 RID: 6898
		private sealed class DeleteCacheRequestMessage : BufferedMessage
		{
			// Token: 0x17002B87 RID: 11143
			// (get) Token: 0x0600AD17 RID: 44311 RVA: 0x00239205 File Offset: 0x00237405
			// (set) Token: 0x0600AD18 RID: 44312 RVA: 0x0023920D File Offset: 0x0023740D
			public string Identifier { get; set; }

			// Token: 0x0600AD19 RID: 44313 RVA: 0x00239216 File Offset: 0x00237416
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.Identifier);
			}

			// Token: 0x0600AD1A RID: 44314 RVA: 0x00239224 File Offset: 0x00237424
			public override void Deserialize(BinaryReader reader)
			{
				this.Identifier = reader.ReadString();
			}
		}

		// Token: 0x02001AF3 RID: 6899
		private sealed class CacheFromDirectoryRequestMessage : BufferedMessage
		{
			// Token: 0x17002B88 RID: 11144
			// (get) Token: 0x0600AD1C RID: 44316 RVA: 0x00239232 File Offset: 0x00237432
			// (set) Token: 0x0600AD1D RID: 44317 RVA: 0x0023923A File Offset: 0x0023743A
			public string Directory { get; set; }

			// Token: 0x0600AD1E RID: 44318 RVA: 0x00239243 File Offset: 0x00237443
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.Directory);
			}

			// Token: 0x0600AD1F RID: 44319 RVA: 0x00239251 File Offset: 0x00237451
			public override void Deserialize(BinaryReader reader)
			{
				this.Directory = reader.ReadString();
			}
		}

		// Token: 0x02001AF4 RID: 6900
		private sealed class CacheFromDirectoryResponseMessage : BufferedMessage
		{
			// Token: 0x17002B89 RID: 11145
			// (get) Token: 0x0600AD21 RID: 44321 RVA: 0x0023925F File Offset: 0x0023745F
			// (set) Token: 0x0600AD22 RID: 44322 RVA: 0x00239267 File Offset: 0x00237467
			public CacheManagerCacheInfo Cache { get; set; }

			// Token: 0x0600AD23 RID: 44323 RVA: 0x00239270 File Offset: 0x00237470
			public override void Serialize(BinaryWriter writer)
			{
				RemotePersistentCacheFactory.WriteCacheInfo(writer, this.Cache);
			}

			// Token: 0x0600AD24 RID: 44324 RVA: 0x0023927E File Offset: 0x0023747E
			public override void Deserialize(BinaryReader reader)
			{
				this.Cache = RemotePersistentCacheFactory.ReadCacheInfo(reader);
			}
		}
	}
}
