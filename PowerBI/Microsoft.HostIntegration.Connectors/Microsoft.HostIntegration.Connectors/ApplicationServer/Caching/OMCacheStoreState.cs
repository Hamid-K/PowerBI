using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200028C RID: 652
	internal class OMCacheStoreState
	{
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000482BD File Offset: 0x000464BD
		public bool WriteInProgress
		{
			get
			{
				return this._writeInProgress != 0;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x000482CB File Offset: 0x000464CB
		public DataCacheStoreProvider Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000482D3 File Offset: 0x000464D3
		public BackingStoreConfig StoreConfig
		{
			get
			{
				return this._storeConfig;
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x000482DB File Offset: 0x000464DB
		public bool TryEnterWriteLock()
		{
			return Interlocked.CompareExchange(ref this._writeInProgress, 1, 0) == 0;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x000482ED File Offset: 0x000464ED
		public void ReleaseWriteLock()
		{
			ReleaseAssert.IsTrue(Interlocked.CompareExchange(ref this._writeInProgress, 0, 1) == 1, "Lock released by multiple threads");
			this._lastWriteTime = DateTime.Now;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x00048314 File Offset: 0x00046514
		public bool WriteBehindEnabled
		{
			get
			{
				return this._storeConfig.WriteBehind.Enabled;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x00048326 File Offset: 0x00046526
		public bool ReadThroughEnabled
		{
			get
			{
				return this._storeConfig.ReadThrough.Enabled;
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00048338 File Offset: 0x00046538
		public bool LastWriteOlderThan(int minutes)
		{
			return this._lastWriteTime.AddMinutes((double)minutes) < DateTime.Now;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00048351 File Offset: 0x00046551
		internal OMCacheStoreState(string cacheName, BackingStoreConfig storeConfig)
		{
			this._storeConfig = storeConfig;
			this.InitializeProvider(cacheName, this._storeConfig);
			OMCacheStoreState.InitializeCacheObjectSerializationProperties(cacheName, this._storeConfig);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00048384 File Offset: 0x00046584
		internal OMCacheStoreState(string cacheName, BackingStoreConfig storeConfig, OMCacheStoreState oldState)
		{
			this._storeConfig = storeConfig;
			this._lastWriteTime = oldState._lastWriteTime;
			this._writeInProgress = oldState._writeInProgress;
			this.InitializeProvider(cacheName, this._storeConfig);
			OMCacheStoreState.InitializeCacheObjectSerializationProperties(cacheName, this._storeConfig);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000483DC File Offset: 0x000465DC
		private static void InitializeCacheObjectSerializationProperties(string cacheName, BackingStoreConfig storeConfig)
		{
			DataCacheSerializationProperties dataCacheSerializationProperties = new DataCacheSerializationProperties(storeConfig.SerializationConfig.SerializerType, storeConfig.SerializationConfig.CustomSerializerTypeName);
			DataCacheItemFactory.SetDataCacheSerializationProperties(cacheName, dataCacheSerializationProperties);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0004840C File Offset: 0x0004660C
		private void InitializeProvider(string cacheName, BackingStoreConfig storeConfig)
		{
			WriteBehindConfig writeBehind = storeConfig.WriteBehind;
			ReadThroughConfig readThrough = storeConfig.ReadThrough;
			if (!writeBehind.Enabled)
			{
				if (!readThrough.Enabled)
				{
					return;
				}
			}
			Type type;
			try
			{
				type = Type.GetType(storeConfig.Provider.Type, true);
			}
			catch (Exception ex)
			{
				if (Utility.IsExpectedDuringTypeLoad(ex))
				{
					throw new DataCacheException(ObjectManager.LogSource, 9007, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9007, storeConfig.Provider.Type, ex.Message), ex, true);
				}
				throw;
			}
			Dictionary<string, string> providerConfig = OMCacheStoreState.GetProviderConfig(storeConfig);
			try
			{
				this._provider = (DataCacheStoreProvider)Activator.CreateInstance(type, new object[] { cacheName, providerConfig });
			}
			catch (Exception ex2)
			{
				if (Utility.IsExpectedDuringReflection(ex2) || ex2 is InvalidCastException)
				{
					throw new DataCacheException(ObjectManager.LogSource, 9007, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9007, string.Format(CultureInfo.CurrentCulture, "{0}:{1}", new object[]
					{
						storeConfig.Provider.Type,
						type.AssemblyQualifiedName
					}), ex2.Message), ex2, true);
				}
				throw;
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00048548 File Offset: 0x00046748
		private static Dictionary<string, string> GetProviderConfig(BackingStoreConfig config)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			ProviderSettings settings = config.Provider.Settings;
			foreach (object obj in settings)
			{
				ProviderSetting providerSetting = (ProviderSetting)obj;
				dictionary[providerSetting.Key] = providerSetting.Value;
			}
			return dictionary;
		}

		// Token: 0x04000D33 RID: 3379
		private int _writeInProgress;

		// Token: 0x04000D34 RID: 3380
		private DateTime _lastWriteTime = DateTime.MinValue;

		// Token: 0x04000D35 RID: 3381
		private DataCacheStoreProvider _provider;

		// Token: 0x04000D36 RID: 3382
		private BackingStoreConfig _storeConfig;
	}
}
