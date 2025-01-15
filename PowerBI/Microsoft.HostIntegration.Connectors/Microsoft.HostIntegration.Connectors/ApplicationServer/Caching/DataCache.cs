using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200003B RID: 59
	public sealed class DataCache
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000ABA7 File Offset: 0x00008DA7
		public string Name
		{
			get
			{
				return this._myName;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000ABAF File Offset: 0x00008DAF
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000ABB7 File Offset: 0x00008DB7
		internal string ClientVersion { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		internal NamedCacheConfiguration Configuration { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000ABD9 File Offset: 0x00008DD9
		internal bool UnitTestEnabled { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		internal DataCache.UnitTestParameters UnitTestParams
		{
			get
			{
				lock (this)
				{
					if (this._unitTestParameters == null)
					{
						this._unitTestParameters = new DataCache.UnitTestParameters();
					}
				}
				return this._unitTestParameters;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000AC34 File Offset: 0x00008E34
		private static TimeSpan DefaultTimeToLive
		{
			get
			{
				return TimeSpan.Zero;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000AC3B File Offset: 0x00008E3B
		internal NotificationManager NotificationManagerInstance
		{
			get
			{
				return this._parentFactory.NotificationManagerInstance;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000AC48 File Offset: 0x00008E48
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000AC50 File Offset: 0x00008E50
		internal bool IsRequestTrackingEnabled { get; set; }

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001F4 RID: 500 RVA: 0x0000AC5C File Offset: 0x00008E5C
		// (remove) Token: 0x060001F5 RID: 501 RVA: 0x0000AC94 File Offset: 0x00008E94
		internal event Action<long, ReqType> RequestCompletedEvent;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001F6 RID: 502 RVA: 0x0000ACCC File Offset: 0x00008ECC
		// (remove) Token: 0x060001F7 RID: 503 RVA: 0x0000AD04 File Offset: 0x00008F04
		internal event Action LocalCacheUsedForGetEvent;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001F8 RID: 504 RVA: 0x0000AD3C File Offset: 0x00008F3C
		// (remove) Token: 0x060001F9 RID: 505 RVA: 0x0000AD74 File Offset: 0x00008F74
		internal event Action<int, int> LocalCacheUsedForBulkGetEvent;

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000ADA9 File Offset: 0x00008FA9
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000ADB1 File Offset: 0x00008FB1
		private LocalCache LocalCache { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000ADBA File Offset: 0x00008FBA
		private bool IsLocalCacheEnabled
		{
			get
			{
				return this.LocalCache != null;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		private static DataCacheTag[] ValidateTags(IEnumerable<DataCacheTag> tags)
		{
			List<DataCacheTag> list = new List<DataCacheTag>(8);
			foreach (DataCacheTag dataCacheTag in tags)
			{
				if (dataCacheTag == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				list.Add(dataCacheTag);
			}
			DataCacheTag[] array = null;
			if (list.Count != 0)
			{
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000AE48 File Offset: 0x00009048
		private static void ValidateKeyAndRegion(string key, string region)
		{
			if (key != null && DataCache.encoding.GetMaxByteCount(key.Length) > DataCache.MaxKeyLength && DataCache.encoding.GetByteCount(key) > DataCache.MaxKeyLength)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "KeyTooLarge"), "key");
			}
			if (region != null && DataCache.encoding.GetMaxByteCount(region.Length) > DataCache.MaxRegionLength && DataCache.encoding.GetByteCount(region) > DataCache.MaxRegionLength)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "RegionTooLarge"), "region");
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000AEE1 File Offset: 0x000090E1
		internal IList<KeyValuePair<string, object>> GetNextBatch(string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener, ref object state, out bool more)
		{
			return this._protocolInUse.GetNextBatch(region, tags, op, listener, ref state, out more);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AEF7 File Offset: 0x000090F7
		internal IList<string> GetNextBatchOfKeys(string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener, ref object state, out bool more)
		{
			return this._protocolInUse.GetNextBatchOfKeys(region, tags, op, listener, ref state, out more);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000AF0D File Offset: 0x0000910D
		public DataCache(string cacheName)
			: this(cacheName, "default")
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AF1B File Offset: 0x0000911B
		public DataCache()
			: this("default", "default")
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AF30 File Offset: 0x00009130
		public DataCache(string cacheName, string clientConfigurationName)
		{
			if (cacheName == null)
			{
				throw new ArgumentNullException("cacheName");
			}
			if (string.IsNullOrEmpty(clientConfigurationName))
			{
				throw new ArgumentNullException("clientConfigurationName");
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.CacheAPI", "GenericCache: Constructing Cache: {0}", cacheName);
			}
			DataCacheFactory dataCacheFactory = DataCacheFactory.InitializeOrFetchSingletonFactoryInstance(clientConfigurationName);
			dataCacheFactory.GetCache(cacheName, new CreateNewCacheDelegate(this.InitializeCache), new DataCacheInitializationViaCopyDelegate(this.InitializeFromCache));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000AFA4 File Offset: 0x000091A4
		internal DataCache InitializeFromCache(DataCache cache)
		{
			this.InitializeCache(cache._myName, cache._protocolInUse, cache._parentFactory, cache.Configuration, cache._supportProvider, cache.cacheObjectSerializationProvider);
			if (this._parentFactory.Configuration.LocalCacheProperties.IsEnabled)
			{
				this.LocalCache = cache.LocalCache;
			}
			return this;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B000 File Offset: 0x00009200
		internal DataCache InitializeCache(string name, IClientProtocol protocolToUse, DataCacheFactory parentFactory, NamedCacheConfiguration cacheConfiguration, ClientOperationsSupportProvider supportProvider)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.CacheAPI", "GenericCache: Constructing Cache: {0}", name);
			}
			DataCacheObjectSerializationProvider dataCacheObjectSerializationProvider = new DataCacheObjectSerializationProvider(parentFactory.Configuration.SerializationProperties);
			this.InitializeCache(name, protocolToUse, parentFactory, cacheConfiguration, supportProvider, dataCacheObjectSerializationProvider);
			if (this._parentFactory.Configuration.LocalCacheProperties.IsEnabled)
			{
				this.LocalCache = new LocalCache(this, this._parentFactory);
			}
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				this.RequestCompletedEvent += ClientPerfCounterUpdate.OnRequestCompleted;
				if (this._parentFactory.Configuration.LocalCacheProperties.IsEnabled)
				{
					this.LocalCacheUsedForGetEvent += ClientPerfCounterUpdate.OnLocalCacheUsedForGet;
					this.LocalCacheUsedForBulkGetEvent += ClientPerfCounterUpdate.OnLocalCacheUsedForBulkGet;
				}
			}
			return this;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B0C4 File Offset: 0x000092C4
		internal DataCache InitializeCache(string name, IClientProtocol protocolToUse, DataCacheFactory parentFactory, NamedCacheConfiguration cacheConfiguration, ClientOperationsSupportProvider supportProvider, DataCacheObjectSerializationProvider serializationProvider)
		{
			this._parentFactory = parentFactory;
			this._myName = name;
			this._supportProvider = supportProvider;
			this._protocolInUse = protocolToUse;
			this.Configuration = cacheConfiguration;
			this.ClientVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
			this.cacheObjectSerializationProvider = serializationProvider;
			DataCacheItemFactory.SetDataCacheSerializationProperties(this._myName, this._parentFactory.Configuration.SerializationProperties);
			return this;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B134 File Offset: 0x00009334
		internal DataCache(string name, IClientProtocol protocolToUse, DataCacheFactory parentFactory, NamedCacheConfiguration cacheConfiguration, ClientOperationsSupportProvider supportProvider)
		{
			ReleaseAssert.IsTrue(protocolToUse != null);
			ReleaseAssert.IsTrue(supportProvider != null);
			ReleaseAssert.IsTrue(protocolToUse != null);
			ReleaseAssert.IsTrue(cacheConfiguration != null);
			this.InitializeCache(name, protocolToUse, parentFactory, cacheConfiguration, supportProvider);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B187 File Offset: 0x00009387
		internal static void ThrowException(ResponseBody respBody, EndpointID destination)
		{
			DataCache.ThrowException(respBody.ResponseCode, respBody.ResponseTrackingId, respBody.Exception, respBody.Value, destination);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B1A8 File Offset: 0x000093A8
		internal static void ThrowException(ErrStatus errStatus, Guid trackingId, Exception responseException, byte[][] payload, EndpointID destination)
		{
			int num = -1;
			int num2 = Utility.ConvertToDataCacheErrorCode(errStatus, out num);
			DataCacheException ex;
			if (responseException == null)
			{
				ex = DataCache.NewException(num2, num, trackingId);
				if (destination != null)
				{
					Utility.AddDestinationToException(ex, errStatus, destination.UriString);
				}
			}
			else
			{
				DataCacheException ex2 = responseException as DataCacheException;
				if (ex2 != null)
				{
					ex = DataCache.NewException(ex2.ErrorCode, ex2.SubStatus, trackingId, ex2, ex2.Data);
				}
				else
				{
					ex = DataCache.NewException(num2, num, trackingId, responseException, null);
					if (destination != null)
					{
						Utility.AddDestinationToException(ex, errStatus, destination.UriString);
					}
				}
			}
			Utility.AddInformationToException(ex, errStatus, payload);
			throw ex;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B22C File Offset: 0x0000942C
		internal static DataCacheException NewException(int errCode)
		{
			return DataCache.NewException(errCode, -1);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B235 File Offset: 0x00009435
		internal static DataCacheException NewException(int errCode, int substatus)
		{
			return Utility.CreateClientException("DistributedCache.CacheAPI", errCode, substatus, null);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B244 File Offset: 0x00009444
		internal static DataCacheException NewException(int errCode, int substatus, Guid trackingId)
		{
			DataCacheException ex = DataCache.NewException(errCode, substatus);
			ex.TrackingId = trackingId;
			return ex;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B264 File Offset: 0x00009464
		internal static DataCacheException NewException(int errCode, int substatus, Guid trackingId, Exception innerException, IDictionary exceptionData)
		{
			DataCacheException ex = Utility.CreateClientException("DistributedCache.CacheAPI", errCode, substatus, innerException);
			ex.TrackingId = trackingId;
			if (exceptionData != null)
			{
				foreach (object obj in exceptionData.Keys)
				{
					ex.Data.Add(obj, exceptionData[obj]);
				}
			}
			return ex;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B2E0 File Offset: 0x000094E0
		internal static string GetErrorMsg(int errCode)
		{
			return GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errCode);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B2ED File Offset: 0x000094ED
		internal static DataCacheException NewExceptionWithMessage(int errCode, string errMsg)
		{
			return new DataCacheException("DistributedCache.CacheAPI", errCode, errMsg, true);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000B2FC File Offset: 0x000094FC
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000B30E File Offset: 0x0000950E
		internal bool IsCompressionEnabled
		{
			get
			{
				return this._parentFactory.Configuration.IsCompressionEnabled;
			}
			set
			{
				this._parentFactory.Configuration.IsCompressionEnabled = value;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000212 RID: 530 RVA: 0x0000B324 File Offset: 0x00009524
		// (remove) Token: 0x06000213 RID: 531 RVA: 0x0000B35C File Offset: 0x0000955C
		private event EventHandler<CacheOperationStartedEventArgs> _cacheOperationStarted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000214 RID: 532 RVA: 0x0000B394 File Offset: 0x00009594
		// (remove) Token: 0x06000215 RID: 533 RVA: 0x0000B3CC File Offset: 0x000095CC
		private event EventHandler<CacheOperationCompletedEventArgs> _cacheOperationCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000216 RID: 534 RVA: 0x0000B401 File Offset: 0x00009601
		// (remove) Token: 0x06000217 RID: 535 RVA: 0x0000B40A File Offset: 0x0000960A
		public event EventHandler<CacheOperationStartedEventArgs> CacheOperationStarted
		{
			add
			{
				this._cacheOperationStarted += value;
			}
			remove
			{
				this._cacheOperationStarted -= value;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000218 RID: 536 RVA: 0x0000B413 File Offset: 0x00009613
		// (remove) Token: 0x06000219 RID: 537 RVA: 0x0000B41C File Offset: 0x0000961C
		public event EventHandler<CacheOperationCompletedEventArgs> CacheOperationCompleted
		{
			add
			{
				this._cacheOperationCompleted += value;
			}
			remove
			{
				this._cacheOperationCompleted -= value;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B428 File Offset: 0x00009628
		internal void OnCacheOperationStarted(CacheOperationStartedEventArgs e)
		{
			EventHandler<CacheOperationStartedEventArgs> cacheOperationStarted = this._cacheOperationStarted;
			if (cacheOperationStarted != null)
			{
				cacheOperationStarted(this, e);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B448 File Offset: 0x00009648
		internal void OnCacheOperationCompleted(CacheOperationCompletedEventArgs e)
		{
			EventHandler<CacheOperationCompletedEventArgs> cacheOperationCompleted = this._cacheOperationCompleted;
			if (cacheOperationCompleted != null)
			{
				cacheOperationCompleted(this, e);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000B467 File Offset: 0x00009667
		private bool AreMonitoringListenersAvailable
		{
			get
			{
				return this._cacheOperationStarted != null || this._cacheOperationCompleted != null;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B47F File Offset: 0x0000967F
		private IMonitoringListener GetMonitoringListener(CacheOperationType operationType)
		{
			return MonitoringListenerFactory.CreateListener(!this.AreMonitoringListenersAvailable, this, operationType);
		}

		// Token: 0x17000053 RID: 83
		public object this[string key]
		{
			get
			{
				return this.Get(key);
			}
			set
			{
				if (value == null)
				{
					this.Remove(key);
					return;
				}
				this.Put(key, value);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000B4B4 File Offset: 0x000096B4
		internal IEnumerable<string> GetKeys(string regionName)
		{
			if (regionName == null)
			{
				throw new ArgumentNullException("regionName");
			}
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetAllKeys);
			return listener.Listen<EnumerableCacheKeys>(() => new EnumerableCacheKeys(this, regionName, null, GetByTagsOperation.ByNone, listener));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000B510 File Offset: 0x00009710
		internal IEnumerable<string> GetKeysByAllTags(IEnumerable<DataCacheTag> tags, string region)
		{
			if (tags == null)
			{
				throw new ArgumentNullException("tags");
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			DataCacheTag[] newTags = DataCache.ValidateTags(tags);
			if (newTags == null)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
			}
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetAllKeys);
			return listener.Listen<EnumerableCacheKeys>(delegate
			{
				if (newTags.Length == 1)
				{
					return new EnumerableCacheKeys(this, region, newTags, GetByTagsOperation.ByNone, listener);
				}
				return new EnumerableCacheKeys(this, region, newTags, GetByTagsOperation.ByIntersection, listener);
			});
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000B5A8 File Offset: 0x000097A8
		internal IEnumerable<string> GetKeysByAnyTags(IEnumerable<DataCacheTag> tags, string region)
		{
			if (tags == null)
			{
				throw new ArgumentNullException("tags");
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			DataCacheTag[] newTags = DataCache.ValidateTags(tags);
			if (newTags == null)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
			}
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetAllKeys);
			return listener.Listen<EnumerableCacheKeys>(delegate
			{
				if (newTags.Length == 1)
				{
					return new EnumerableCacheKeys(this, region, newTags, GetByTagsOperation.ByNone, listener);
				}
				return new EnumerableCacheKeys(this, region, newTags, GetByTagsOperation.ByUnion, listener);
			});
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000B640 File Offset: 0x00009840
		public DataCacheItemVersion Add(string key, object value)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Add);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalAdd(key, value, DataCache.DefaultTimeToLive, null, null, listener);
			});
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000B68C File Offset: 0x0000988C
		public DataCacheItemVersion Add(string key, object value, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Add);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalAdd(key, value, DataCache.DefaultTimeToLive, null, region, listener);
			});
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public DataCacheItemVersion Add(string key, object value, TimeSpan timeout)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Add);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalAdd(key, value, timeout, null, null, listener);
			});
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000B734 File Offset: 0x00009934
		public DataCacheItemVersion Add(string key, object value, TimeSpan timeout, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Add);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalAdd(key, value, timeout, null, region, listener);
			});
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000B790 File Offset: 0x00009990
		public DataCacheItemVersion Add(string key, object value, IEnumerable<DataCacheTag> tags)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Add);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalAdd(key, value, DataCache.DefaultTimeToLive, array, null, listener);
			});
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000B7E4 File Offset: 0x000099E4
		public DataCacheItemVersion Add(string key, object value, IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Add);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalAdd(key, value, DataCache.DefaultTimeToLive, array, region, listener);
			});
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000B840 File Offset: 0x00009A40
		public DataCacheItemVersion Add(string key, object value, TimeSpan timeout, IEnumerable<DataCacheTag> tags)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Add);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"));
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalAdd(key, value, timeout, array, null, listener);
			});
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000B89C File Offset: 0x00009A9C
		public DataCacheItemVersion Add(string key, object value, TimeSpan timeout, IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Add);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalAdd(key, value, timeout, array, region, listener);
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B900 File Offset: 0x00009B00
		internal DataCacheItemVersion InternalAdd(string key, object value, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			DataCacheItemVersion dataCacheItemVersion;
			try
			{
				dataCacheItemVersion = this._protocolInUse.Add(key, value, timeout, tags, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
			return dataCacheItemVersion;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B978 File Offset: 0x00009B78
		internal DataCacheItemVersion InternalReplace(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			DataCacheItemVersion dataCacheItemVersion;
			try
			{
				dataCacheItemVersion = this._protocolInUse.Replace(key, value, oldVersion, timeout, tags, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
			return dataCacheItemVersion;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B9F4 File Offset: 0x00009BF4
		public DataCacheItemVersion Put(string key, object value)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, null, DataCache.DefaultTimeToLive, null, null, listener);
			});
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000BA40 File Offset: 0x00009C40
		public DataCacheItemVersion Put(string key, object value, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalPut(key, value, null, DataCache.DefaultTimeToLive, null, region, listener);
			});
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000BA94 File Offset: 0x00009C94
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (oldVersion == null)
				{
					throw new ArgumentNullException("oldVersion");
				}
				if (DataCacheItemVersion.IsEmpty(oldVersion))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "oldVersion");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, oldVersion, DataCache.DefaultTimeToLive, null, null, listener);
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (oldVersion == null)
				{
					throw new ArgumentNullException("oldVersion");
				}
				if (DataCacheItemVersion.IsEmpty(oldVersion))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "oldVersion");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalPut(key, value, oldVersion, DataCache.DefaultTimeToLive, null, region, listener);
			});
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000BB44 File Offset: 0x00009D44
		public DataCacheItemVersion Put(string key, object value, TimeSpan timeout)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, null, timeout, null, null, listener);
			});
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000BB98 File Offset: 0x00009D98
		public DataCacheItemVersion Put(string key, object value, TimeSpan timeout, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, null, timeout, null, region, listener);
			});
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		public DataCacheItemVersion Put(string key, object value, IEnumerable<DataCacheTag> tags)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, null, DataCache.DefaultTimeToLive, array, null, listener);
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000BC48 File Offset: 0x00009E48
		public DataCacheItemVersion Put(string key, object value, IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, null, DataCache.DefaultTimeToLive, array, region, listener);
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000BCA4 File Offset: 0x00009EA4
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (oldVersion == null)
				{
					throw new ArgumentNullException("oldVersion");
				}
				if (DataCacheItemVersion.IsEmpty(oldVersion))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "oldVersion");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, oldVersion, timeout, null, null, listener);
			});
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000BD00 File Offset: 0x00009F00
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (oldVersion == null)
				{
					throw new ArgumentNullException("oldVersion");
				}
				if (DataCacheItemVersion.IsEmpty(oldVersion))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "oldVersion");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalPut(key, value, oldVersion, timeout, null, region, listener);
			});
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000BD64 File Offset: 0x00009F64
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, IEnumerable<DataCacheTag> tags)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (oldVersion == null)
				{
					throw new ArgumentNullException("oldVersion");
				}
				if (DataCacheItemVersion.IsEmpty(oldVersion))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "oldVersion");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, oldVersion, DataCache.DefaultTimeToLive, array, null, listener);
			});
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (oldVersion == null)
				{
					throw new ArgumentNullException("oldVersion");
				}
				if (DataCacheItemVersion.IsEmpty(oldVersion))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "oldVersion");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalPut(key, value, oldVersion, DataCache.DefaultTimeToLive, array, region, listener);
			});
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000BE24 File Offset: 0x0000A024
		public DataCacheItemVersion Put(string key, object value, TimeSpan timeout, IEnumerable<DataCacheTag> tags)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, null, timeout, array, null, listener);
			});
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000BE80 File Offset: 0x0000A080
		public DataCacheItemVersion Put(string key, object value, TimeSpan timeout, IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalPut(key, value, null, timeout, array, region, listener);
			});
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, IEnumerable<DataCacheTag> tags)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (oldVersion == null)
				{
					throw new ArgumentNullException("oldVersion");
				}
				if (DataCacheItemVersion.IsEmpty(oldVersion))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "oldVersion");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPut(key, value, oldVersion, timeout, array, null, listener);
			});
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000BF48 File Offset: 0x0000A148
		public DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Put);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (oldVersion == null)
				{
					throw new ArgumentNullException("oldVersion");
				}
				if (DataCacheItemVersion.IsEmpty(oldVersion))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "oldVersion");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalPut(key, value, oldVersion, timeout, array, region, listener);
			});
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		internal DataCacheItemVersion InternalPut(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			Stopwatch stopwatch = null;
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				stopwatch = new Stopwatch();
				stopwatch.Start();
			}
			DataCacheItemVersion dataCacheItemVersion;
			try
			{
				dataCacheItemVersion = this._protocolInUse.Put(key, value, oldVersion, timeout, tags, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
				if (ClientPerfCounterUpdate.IsPerfCounterEnabled && stopwatch != null)
				{
					stopwatch.Stop();
					if (this.RequestCompletedEvent != null)
					{
						this.RequestCompletedEvent((long)(stopwatch.Elapsed.TotalMilliseconds * 1000.0), ReqType.PUT);
					}
				}
			}
			return dataCacheItemVersion;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000C080 File Offset: 0x0000A280
		public object Get(string key)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Get);
			return listener.Listen<object>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				DataCacheItemVersion dataCacheItemVersion;
				return this.InternalGet(key, out dataCacheItemVersion, null, listener);
			});
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public object Get(string key, out DataCacheItemVersion version)
		{
			DataCacheItemVersion innerVersion = null;
			object obj;
			try
			{
				IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Get);
				obj = listener.Listen<object>(delegate
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					return this.InternalGet(key, out innerVersion, null, listener);
				});
			}
			finally
			{
				version = innerVersion;
			}
			return obj;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000C140 File Offset: 0x0000A340
		public object Get(string key, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Get);
			return listener.Listen<object>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheItemVersion dataCacheItemVersion;
				return this.InternalGet(key, out dataCacheItemVersion, region, listener);
			});
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000C18C File Offset: 0x0000A38C
		public IEnumerable<KeyValuePair<string, object>> BulkGet(IEnumerable<string> keys, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.BulkGet);
			return listener.Listen<IEnumerable<KeyValuePair<string, object>>>(delegate
			{
				if (keys == null)
				{
					throw new ArgumentNullException("keys");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				HashSet<string> hashSet = new HashSet<string>();
				foreach (string text in keys)
				{
					if (text == null)
					{
						throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneKeyNull"), "keys");
					}
					hashSet.Add(text);
				}
				return this.InternalBulkGet(region, hashSet, listener);
			});
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		public IEnumerable<KeyValuePair<string, object>> BulkGet(IEnumerable<string> keys)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.BulkGet);
			return listener.Listen<IEnumerable<KeyValuePair<string, object>>>(delegate
			{
				if (keys == null)
				{
					throw new ArgumentNullException("keys");
				}
				HashSet<string> hashSet = new HashSet<string>();
				foreach (string text in keys)
				{
					if (text == null)
					{
						throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneKeyNull"), "keys");
					}
					hashSet.Add(text);
				}
				return this.InternalBulkGet(null, hashSet, listener);
			});
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000C220 File Offset: 0x0000A420
		public object Get(string key, out DataCacheItemVersion version, string region)
		{
			DataCacheItemVersion innerVersion = null;
			object obj;
			try
			{
				IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Get);
				obj = listener.Listen<object>(delegate
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					if (region == null)
					{
						throw new ArgumentNullException("region");
					}
					return this.InternalGet(key, out innerVersion, region, listener);
				});
			}
			finally
			{
				version = innerVersion;
			}
			return obj;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		private object InternalGet(string key, out DataCacheItemVersion version, string region, IMonitoringListener listener)
		{
			Stopwatch stopwatch = null;
			if (ClientPerfCounterUpdate.IsPerfCounterEnabled)
			{
				stopwatch = new Stopwatch();
				stopwatch.Start();
			}
			object obj;
			try
			{
				if (region != null && RegionNameProvider.IsSystemRegion(region))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
				}
				if (!this.IsLocalCacheEnabled)
				{
					TimeSpan timeSpan;
					ErrStatus errStatus;
					obj = this._protocolInUse.Get(key, out version, out timeSpan, out errStatus, region, listener);
				}
				else
				{
					Key key2 = new Key(key);
					InternalCacheItemVersion @null = InternalCacheItemVersion.Null;
					object obj2 = this.LocalCache.Get(region, key2, ref @null);
					if (obj2 != null)
					{
						if (this.LocalCacheUsedForGetEvent != null)
						{
							this.LocalCacheUsedForGetEvent();
						}
						if (Provider.IsEnabled(TraceLevel.Verbose))
						{
							EventLogWriter.WriteVerbose<string, string>("DistributedCache.CacheAPI", "Object found in local cache: {0}, {1}", region, key);
						}
						version = new DataCacheItemVersion(@null);
					}
					else
					{
						if (Provider.IsEnabled(TraceLevel.Verbose))
						{
							EventLogWriter.WriteVerbose<string, string>("DistributedCache.CacheAPI", "Object not found in local cache: {0}, {1}", region, key);
						}
						try
						{
							DataCacheLockHandle dataCacheLockHandle = this.LocalCache.Lock(region, key2);
							DateTime utcNow = DateTime.UtcNow;
							TimeSpan timeSpan2;
							ErrStatus errStatus2;
							obj2 = this._protocolInUse.Get(key, out version, out timeSpan2, out errStatus2, region, listener);
							if (this.UnitTestEnabled)
							{
								this.UnitTestParams.WaitEvent1.Set();
								this.UnitTestParams.WaitEvent2.WaitOne();
							}
							this.UpdateLocalCache(errStatus2, region, key2, obj2, timeSpan2, version, DateTime.UtcNow - utcNow, dataCacheLockHandle);
						}
						catch (DataCacheException)
						{
							this.LocalCache.Remove(region, key2);
							throw;
						}
					}
					obj = obj2;
				}
			}
			finally
			{
				if (ClientPerfCounterUpdate.IsPerfCounterEnabled && stopwatch != null)
				{
					stopwatch.Stop();
					if (this.RequestCompletedEvent != null)
					{
						this.RequestCompletedEvent((long)(stopwatch.Elapsed.TotalMilliseconds * 1000.0), ReqType.GET);
					}
				}
			}
			return obj;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C484 File Offset: 0x0000A684
		private void UpdateLocalCache(ErrStatus errStatus, string region, Key key, object value, TimeSpan timeout, DataCacheItemVersion version, TimeSpan roundTripTime, DataCacheLockHandle dataCacheLockHandle)
		{
			if (errStatus != ErrStatus.UNINITIALIZED_ERROR)
			{
				this.UpdateLocalCache(region, key, errStatus);
				return;
			}
			if (dataCacheLockHandle != null)
			{
				if (value != null && version != null)
				{
					this.LocalCache.PutAndUnlock(region, key, value, version.InternalVersion, timeout, roundTripTime, dataCacheLockHandle);
					return;
				}
				this.LocalCache.Unlock(region, key);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		private void UpdateLocalCache(string region, Key key, ErrStatus errStatus)
		{
			if (errStatus == ErrStatus.KEY_DOES_NOT_EXIST)
			{
				this.LocalCache.Remove(region, key);
			}
			if (errStatus == ErrStatus.REGION_DOES_NOT_EXIST)
			{
				ReleaseAssert.IsTrue(!RegionNameProvider.IsSystemRegion(region));
				this.LocalCache.DeleteRegion(region);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C50C File Offset: 0x0000A70C
		private bool InternalContainsKey(string key, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			return this._protocolInUse.ContainsKey(key, region, listener);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C544 File Offset: 0x0000A744
		internal long InternalIncrement(string key, long value, long initialValue, TimeSpan ttl, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			long num;
			try
			{
				num = this._protocolInUse.IncrementDecrement(key, value, initialValue, ttl, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
			return num;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		private void InternalConcatenate(string key, string value, bool isAppend, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			try
			{
				this._protocolInUse.Concatenate(key, value, isAppend, DataCache.DefaultTimeToLive, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C638 File Offset: 0x0000A838
		private IEnumerable<KeyValuePair<string, object>> InternalBulkGet(string regionName, HashSet<string> keySet, IMonitoringListener listener)
		{
			foreach (LocalCacheItem localCacheItem in this.InternalBulkGetCacheItems(regionName, keySet, listener))
			{
				yield return new KeyValuePair<string, object>(localCacheItem.Key, localCacheItem.DeserializedValue);
			}
			yield break;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C66C File Offset: 0x0000A86C
		internal IEnumerable<LocalCacheItem> InternalBulkGetCacheItems(string regionName, HashSet<string> keySet, IMonitoringListener listener)
		{
			int num = 0;
			List<LocalCacheItem> list = new List<LocalCacheItem>(keySet.Count);
			Dictionary<string, DataCacheLockHandle> dictionary = new Dictionary<string, DataCacheLockHandle>();
			while (keySet.Count > 0)
			{
				DateTime utcNow;
				IList<LocalCacheItem> list2;
				try
				{
					num += this.GetItemsFromLocalCache(regionName, keySet, list, dictionary);
					if (keySet.Count == 0)
					{
						break;
					}
					Key[] array = new Key[keySet.Count];
					int num2 = 0;
					foreach (string text in keySet)
					{
						array[num2++] = new Key(text);
					}
					utcNow = DateTime.UtcNow;
					if (regionName == null)
					{
						list2 = this._protocolInUse.BulkGet(array, listener);
					}
					else
					{
						list2 = this._protocolInUse.BulkGet(array, regionName, listener);
					}
				}
				catch (DataCacheException)
				{
					foreach (string text2 in dictionary.Keys)
					{
						this.LocalCache.Remove(regionName, new Key(text2));
					}
					throw;
				}
				foreach (LocalCacheItem localCacheItem in list2)
				{
					keySet.Remove(localCacheItem.Key);
					list.Add(localCacheItem);
				}
				this.UpdateLocalCacheItems(regionName, list2, dictionary, utcNow);
			}
			if (this.LocalCacheUsedForBulkGetEvent != null)
			{
				this.LocalCacheUsedForBulkGetEvent(num, list.Count);
			}
			return list;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000C818 File Offset: 0x0000AA18
		private int GetItemsFromLocalCache(string regionName, HashSet<string> keySet, List<LocalCacheItem> retVal, Dictionary<string, DataCacheLockHandle> keyHandles)
		{
			if (this.IsLocalCacheEnabled)
			{
				List<string> list = new List<string>(keySet.Count);
				InternalCacheItemVersion @null = InternalCacheItemVersion.Null;
				foreach (string text in keySet)
				{
					Key key = new Key(text);
					object obj = this.LocalCache.Get(regionName, key, ref @null);
					if (obj != null)
					{
						retVal.Add(new LocalCacheItem(text, obj, @null));
						list.Add(text);
					}
					else if (!keyHandles.ContainsKey(text))
					{
						DataCacheLockHandle dataCacheLockHandle = this.LocalCache.Lock(regionName, key);
						keyHandles.Add(text, dataCacheLockHandle);
					}
				}
				foreach (string text2 in list)
				{
					keySet.Remove(text2);
				}
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.CacheAPI", "CacheBulkGet - {0} Keys reamining after local cache retrieval", new object[] { keySet.Count });
				}
				return list.Count;
			}
			return 0;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000C950 File Offset: 0x0000AB50
		private void UpdateLocalCacheItems(string regionName, IEnumerable<LocalCacheItem> items, Dictionary<string, DataCacheLockHandle> handles, DateTime startTime)
		{
			if (this.IsLocalCacheEnabled)
			{
				foreach (LocalCacheItem localCacheItem in items)
				{
					ErrStatus errStatus = ((localCacheItem.Value == null) ? ErrStatus.KEY_DOES_NOT_EXIST : ErrStatus.UNINITIALIZED_ERROR);
					this.UpdateLocalCache(errStatus, regionName, new Key(localCacheItem.Key), localCacheItem.DeserializedValue, localCacheItem.Ttl, new DataCacheItemVersion(localCacheItem.Version), DateTime.UtcNow - startTime, handles[localCacheItem.Key]);
					handles.Remove(localCacheItem.Key);
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		public bool Remove(string key)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Remove);
			return listener.Listen<bool>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return this.InternalRemove(key, null, null, listener);
			});
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000CA44 File Offset: 0x0000AC44
		public bool Remove(string key, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Remove);
			return listener.Listen<bool>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				return this.InternalRemove(key, null, region, listener);
			});
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public bool Remove(string key, DataCacheItemVersion version)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Remove);
			return listener.Listen<bool>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (version == null)
				{
					throw new ArgumentNullException("version");
				}
				if (DataCacheItemVersion.IsEmpty(version))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "version");
				}
				return this.InternalRemove(key, version, null, listener);
			});
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000CADC File Offset: 0x0000ACDC
		public bool Remove(string key, DataCacheItemVersion version, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Remove);
			return listener.Listen<bool>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (version == null)
				{
					throw new ArgumentNullException("version");
				}
				if (DataCacheItemVersion.IsEmpty(version))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "version");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				return this.InternalRemove(key, version, region, listener);
			});
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000CB30 File Offset: 0x0000AD30
		internal bool InternalRemove(string key, DataCacheItemVersion version, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			bool flag;
			try
			{
				flag = this._protocolInUse.Remove(key, version, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
			return flag;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
		public void ResetObjectTimeout(string key, TimeSpan newTimeout)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.ResetObjectTimeout);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (newTimeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "newTimeout");
				}
				this.InternalResetObjectTimeout(key, newTimeout, null, listener);
			});
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		public void ResetObjectTimeout(string key, TimeSpan newTimeout, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.ResetObjectTimeout);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (newTimeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "newTimeout");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				this.InternalResetObjectTimeout(key, newTimeout, region, listener);
			});
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CC44 File Offset: 0x0000AE44
		private void InternalResetObjectTimeout(string key, TimeSpan newTimeout, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			try
			{
				this._protocolInUse.ResetObjectTimeout(key, newTimeout, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public object GetIfNewer(string key, ref DataCacheItemVersion version)
		{
			DataCacheItemVersion innerVersion = version;
			object obj;
			try
			{
				IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetIfNewer);
				obj = listener.Listen<object>(delegate
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					if (innerVersion == null)
					{
						throw new ArgumentNullException("version");
					}
					if (DataCacheItemVersion.IsEmpty(innerVersion))
					{
						throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "version");
					}
					return this.InternalGetIfNewer(key, ref innerVersion, null, listener);
				});
			}
			finally
			{
				version = innerVersion;
			}
			return obj;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		public object GetIfNewer(string key, ref DataCacheItemVersion version, string region)
		{
			DataCacheItemVersion innerVersion = version;
			object obj;
			try
			{
				IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetIfNewer);
				obj = listener.Listen<object>(delegate
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					if (innerVersion == null)
					{
						throw new ArgumentNullException("version");
					}
					if (DataCacheItemVersion.IsEmpty(innerVersion))
					{
						throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "DataCacheItemVersionInvalid"), "version");
					}
					if (region == null)
					{
						throw new ArgumentNullException("region");
					}
					return this.InternalGetIfNewer(key, ref innerVersion, region, listener);
				});
			}
			finally
			{
				version = innerVersion;
			}
			return obj;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		private object InternalGetIfNewer(string key, ref DataCacheItemVersion version, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			if (!this.IsLocalCacheEnabled)
			{
				TimeSpan timeSpan;
				ErrStatus errStatus;
				return this._protocolInUse.GetIfNewer(key, ref version, out timeSpan, out errStatus, region, listener);
			}
			Key key2 = new Key(key);
			object obj;
			try
			{
				DataCacheLockHandle dataCacheLockHandle = this.LocalCache.Lock(region, key2);
				DateTime utcNow = DateTime.UtcNow;
				TimeSpan timeSpan2;
				ErrStatus errStatus2;
				object ifNewer = this._protocolInUse.GetIfNewer(key, ref version, out timeSpan2, out errStatus2, region, listener);
				this.UpdateLocalCache(errStatus2, region, key2, ifNewer, timeSpan2, version, DateTime.UtcNow - utcNow, dataCacheLockHandle);
				obj = ifNewer;
			}
			catch (DataCacheException)
			{
				this.LocalCache.Remove(region, key2);
				throw;
			}
			return obj;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000CE74 File Offset: 0x0000B074
		public object GetAndLock(string key, TimeSpan timeout, out DataCacheLockHandle lockHandle)
		{
			DataCacheLockHandle innerLockHandle = null;
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetAndLock);
			object obj = listener.Listen<object>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalGetAndLock(key, timeout, out innerLockHandle, null, false, listener);
			});
			lockHandle = innerLockHandle;
			return obj;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		public object GetAndLock(string key, TimeSpan timeout, out DataCacheLockHandle lockHandle, bool forceLock)
		{
			DataCacheLockHandle innerLockHandle = null;
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetAndLock);
			object obj = listener.Listen<object>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalGetAndLock(key, timeout, out innerLockHandle, null, forceLock, listener);
			});
			lockHandle = innerLockHandle;
			return obj;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000CF3C File Offset: 0x0000B13C
		public object GetAndLock(string key, TimeSpan timeout, out DataCacheLockHandle lockHandle, string region, bool forceLock)
		{
			DataCacheLockHandle innerLockHandle = null;
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetAndLock);
			object obj = listener.Listen<object>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalGetAndLock(key, timeout, out innerLockHandle, region, forceLock, listener);
			});
			lockHandle = innerLockHandle;
			return obj;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		public object GetAndLock(string key, TimeSpan timeout, out DataCacheLockHandle lockHandle, string region)
		{
			DataCacheLockHandle innerLockHandle = null;
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetAndLock);
			object obj = listener.Listen<object>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalGetAndLock(key, timeout, out innerLockHandle, region, false, listener);
			});
			lockHandle = innerLockHandle;
			return obj;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D011 File Offset: 0x0000B211
		private object InternalGetAndLock(string key, TimeSpan timeout, out DataCacheLockHandle lockHandle, string region, bool lockKey, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			return this._protocolInUse.GetAndLock(key, timeout, out lockHandle, region, lockKey, listener);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D050 File Offset: 0x0000B250
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.PutAndUnlock);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPutAndUnlock(key, value, lockHandle, TimeSpan.Zero, null, null, listener);
			});
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.PutAndUnlock);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalPutAndUnlock(key, value, lockHandle, TimeSpan.Zero, null, region, listener);
			});
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D100 File Offset: 0x0000B300
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, TimeSpan timeout)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.PutAndUnlock);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPutAndUnlock(key, value, lockHandle, timeout, null, null, listener);
			});
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D15C File Offset: 0x0000B35C
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, TimeSpan timeout, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.PutAndUnlock);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCache.ValidateKeyAndRegion(key, region);
				return this.InternalPutAndUnlock(key, value, lockHandle, timeout, null, region, listener);
			});
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, IEnumerable<DataCacheTag> tags)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.PutAndUnlock);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPutAndUnlock(key, value, lockHandle, TimeSpan.Zero, array, null, listener);
			});
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D21C File Offset: 0x0000B41C
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.PutAndUnlock);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPutAndUnlock(key, value, lockHandle, TimeSpan.Zero, array, region, listener);
			});
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D280 File Offset: 0x0000B480
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, TimeSpan timeout, IEnumerable<DataCacheTag> tags)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.PutAndUnlock);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPutAndUnlock(key, value, lockHandle, timeout, array, null, listener);
			});
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		public DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, TimeSpan timeout, IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.PutAndUnlock);
			return listener.Listen<DataCacheItemVersion>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				DataCache.ValidateKeyAndRegion(key, null);
				return this.InternalPutAndUnlock(key, value, lockHandle, timeout, array, region, listener);
			});
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D350 File Offset: 0x0000B550
		private DataCacheItemVersion InternalPutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			DataCacheItemVersion dataCacheItemVersion;
			try
			{
				dataCacheItemVersion = this._protocolInUse.PutAndUnlock(key, value, lockHandle, timeout, tags, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
			return dataCacheItemVersion;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D3CC File Offset: 0x0000B5CC
		public void Unlock(string key, DataCacheLockHandle lockHandle)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Unlock);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				this.InternalUnlock(key, lockHandle, TimeSpan.Zero, null, listener);
			});
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D41C File Offset: 0x0000B61C
		public void Unlock(string key, DataCacheLockHandle lockHandle, TimeSpan timeout)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Unlock);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				this.InternalUnlock(key, lockHandle, timeout, null, listener);
			});
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D470 File Offset: 0x0000B670
		public void Unlock(string key, DataCacheLockHandle lockHandle, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Unlock);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				this.InternalUnlock(key, lockHandle, TimeSpan.Zero, region, listener);
			});
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		public void Unlock(string key, DataCacheLockHandle lockHandle, TimeSpan timeout, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Unlock);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (timeout <= TimeSpan.Zero)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TimeoutZeroOrLess"), "timeout");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				this.InternalUnlock(key, lockHandle, timeout, region, listener);
			});
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D520 File Offset: 0x0000B720
		public bool Remove(string key, DataCacheLockHandle lockHandle)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.LockedRemove);
			return listener.Listen<bool>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				return this.InternalLockedRemove(key, lockHandle, null, listener);
			});
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D570 File Offset: 0x0000B770
		public bool Remove(string key, DataCacheLockHandle lockHandle, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.LockedRemove);
			return listener.Listen<bool>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (lockHandle == null)
				{
					throw new ArgumentNullException("lockHandle");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				return this.InternalLockedRemove(key, lockHandle, region, listener);
			});
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		private bool InternalLockedRemove(string key, DataCacheLockHandle lockHandle, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			bool flag;
			try
			{
				flag = this._protocolInUse.LockedRemove(key, lockHandle, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
			return flag;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D638 File Offset: 0x0000B838
		private void InternalUnlock(string key, DataCacheLockHandle lockHandle, TimeSpan timeout, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			try
			{
				this._protocolInUse.Unlock(key, lockHandle, timeout, region, listener);
			}
			finally
			{
				if (this.IsLocalCacheEnabled)
				{
					this.LocalCache.Remove(region, new Key(key));
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		internal bool ContainsKey(string key)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.ContainsKey);
			return listener.Listen<bool>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return this.InternalContainsKey(key, null, listener);
			});
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
		internal bool ContainsKey(string key, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.ContainsKey);
			return listener.Listen<bool>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				return this.InternalContainsKey(key, region, listener);
			});
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D744 File Offset: 0x0000B944
		public long Increment(string key, long value, long initialValue)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Increment);
			return listener.Listen<long>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return this.InternalIncrement(key, value, initialValue, DataCache.DefaultTimeToLive, null, listener);
			});
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D798 File Offset: 0x0000B998
		public long Decrement(string key, long value, long initialValue)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Increment);
			return listener.Listen<long>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return this.InternalIncrement(key, -1L * value, initialValue, DataCache.DefaultTimeToLive, null, listener);
			});
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public void Append(string key, string value)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Concatenate);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.InternalConcatenate(key, value, true, null, listener);
			});
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D83C File Offset: 0x0000BA3C
		public void Prepend(string key, string value)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Concatenate);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.InternalConcatenate(key, value, false, null, listener);
			});
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D88C File Offset: 0x0000BA8C
		public long Increment(string key, long value, long initialValue, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Increment);
			return listener.Listen<long>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				return this.InternalIncrement(key, value, initialValue, DataCache.DefaultTimeToLive, region, listener);
			});
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		public long Decrement(string key, long value, long initialValue, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Increment);
			return listener.Listen<long>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				return this.InternalIncrement(key, -1L * value, initialValue, DataCache.DefaultTimeToLive, region, listener);
			});
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D944 File Offset: 0x0000BB44
		public void Append(string key, string value, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Concatenate);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				this.InternalConcatenate(key, value, true, region, listener);
			});
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D998 File Offset: 0x0000BB98
		public void Prepend(string key, string value, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.Concatenate);
			listener.Listen(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				this.InternalConcatenate(key, value, false, region, listener);
			});
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D9EC File Offset: 0x0000BBEC
		public DataCacheItem GetCacheItem(string key)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetCacheItem);
			return listener.Listen<DataCacheItem>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return this.InternalGetCacheItem(key, null, listener);
			});
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DA34 File Offset: 0x0000BC34
		public DataCacheItem GetCacheItem(string key, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.GetCacheItem);
			return listener.Listen<DataCacheItem>(delegate
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				return this.InternalGetCacheItem(key, region, listener);
			});
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000DA84 File Offset: 0x0000BC84
		private DataCacheItem InternalGetCacheItem(string key, string region, IMonitoringListener listener)
		{
			if (region != null && RegionNameProvider.IsSystemRegion(region))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
			}
			if (!this.IsLocalCacheEnabled)
			{
				ErrStatus errStatus;
				return this._protocolInUse.GetCacheItem(key, region, out errStatus, listener);
			}
			Key key2 = new Key(key);
			DataCacheItem dataCacheItem;
			try
			{
				DataCacheLockHandle dataCacheLockHandle = this.LocalCache.Lock(region, key2);
				DateTime utcNow = DateTime.UtcNow;
				ErrStatus errStatus2;
				DataCacheItem cacheItem = this._protocolInUse.GetCacheItem(key, region, out errStatus2, listener);
				if (cacheItem != null)
				{
					this.UpdateLocalCache(errStatus2, region, key2, cacheItem.Value, cacheItem.Timeout, cacheItem.Version, DateTime.UtcNow - utcNow, dataCacheLockHandle);
				}
				else
				{
					this.UpdateLocalCache(region, key2, errStatus2);
				}
				dataCacheItem = cacheItem;
			}
			catch (DataCacheException)
			{
				this.LocalCache.Remove(region, key2);
				throw;
			}
			return dataCacheItem;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000DB60 File Offset: 0x0000BD60
		public bool CreateRegion(string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.CreateRegion);
			return listener.Listen<bool>(delegate
			{
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				if (RegionNameProvider.IsSystemRegion(region))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
				}
				if (!Utility.ValidateName(region))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidRegionName"), "region");
				}
				if (!this._supportProvider.AreRegionsSupported())
				{
					DataCacheFactory.ThrowNotSupportedException();
				}
				DataCache.ValidateKeyAndRegion(null, region);
				return this.InternalCreateRegion(region, listener);
			});
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000DBA6 File Offset: 0x0000BDA6
		private bool InternalCreateRegion(string region, IMonitoringListener listener)
		{
			return this._protocolInUse.CreateRegion(region, listener);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
		public bool RemoveRegion(string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.RemoveRegion);
			return listener.Listen<bool>(delegate
			{
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				if (RegionNameProvider.IsSystemRegion(region))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotPermittedForDefaultRegion"), "region");
				}
				if (!this._supportProvider.AreRegionsSupported())
				{
					DataCacheFactory.ThrowNotSupportedException();
				}
				return this.InternalRemoveRegion(region, listener);
			});
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000DC00 File Offset: 0x0000BE00
		private bool InternalRemoveRegion(string region, IMonitoringListener listener)
		{
			bool flag = this._protocolInUse.RemoveRegion(region, listener);
			if (this.IsLocalCacheEnabled)
			{
				this.LocalCache.DeleteRegion(region);
			}
			return flag;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000DC30 File Offset: 0x0000BE30
		public void ClearRegion(string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.ClearRegion);
			listener.Listen(delegate
			{
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				if (!this._supportProvider.AreRegionsSupported())
				{
					DataCacheFactory.ThrowNotSupportedException();
				}
				this.InternalClearRegion(region, listener);
			});
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000DC76 File Offset: 0x0000BE76
		private void InternalClearRegion(string region, IMonitoringListener listener)
		{
			this._protocolInUse.ClearRegion(region, listener);
			if (this.IsLocalCacheEnabled)
			{
				this.LocalCache.DeleteRegion(region);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public IEnumerable<KeyValuePair<string, object>> GetObjectsInRegion(string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.AsyncGet);
			return listener.Listen<EnumerableCacheObjects>(delegate
			{
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				return new EnumerableCacheObjects(this, region, null, GetByTagsOperation.ByNone, listener);
			});
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		public IEnumerable<KeyValuePair<string, object>> GetObjectsByAnyTag(IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.AsyncGet);
			return listener.Listen<EnumerableCacheObjects>(delegate
			{
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				if (array.Length == 1)
				{
					return new EnumerableCacheObjects(this, region, array, GetByTagsOperation.ByNone, listener);
				}
				return new EnumerableCacheObjects(this, region, array, GetByTagsOperation.ByUnion, listener);
			});
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000DD34 File Offset: 0x0000BF34
		public IEnumerable<KeyValuePair<string, object>> GetObjectsByAllTags(IEnumerable<DataCacheTag> tags, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.AsyncGet);
			return listener.Listen<EnumerableCacheObjects>(delegate
			{
				if (tags == null)
				{
					throw new ArgumentNullException("tags");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = DataCache.ValidateTags(tags);
				if (array == null)
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "OneTagNull"), "tags");
				}
				if (array.Length == 1)
				{
					return new EnumerableCacheObjects(this, region, array, GetByTagsOperation.ByNone, listener);
				}
				return new EnumerableCacheObjects(this, region, array, GetByTagsOperation.ByIntersection, listener);
			});
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public IEnumerable<KeyValuePair<string, object>> GetObjectsByTag(DataCacheTag tag, string region)
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.AsyncGet);
			return listener.Listen<EnumerableCacheObjects>(delegate
			{
				if (tag == null)
				{
					throw new ArgumentNullException("tag");
				}
				if (region == null)
				{
					throw new ArgumentNullException("region");
				}
				DataCacheTag[] array = new DataCacheTag[] { tag };
				return new EnumerableCacheObjects(this, region, array, GetByTagsOperation.ByNone, listener);
			});
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000DDD1 File Offset: 0x0000BFD1
		public string GetSystemRegionName(string key)
		{
			if (!this._supportProvider.AreRegionsSupported())
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return RegionNameProvider.GetSystemRegionName(key, this.Configuration.SystemRegionCount);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000DE04 File Offset: 0x0000C004
		public IEnumerable<string> GetSystemRegions()
		{
			if (!this._supportProvider.AreRegionsSupported())
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			return RegionNameProvider.GetSystemRegionList(this.Configuration.SystemRegionCount);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000DE28 File Offset: 0x0000C028
		public void Clear()
		{
			IMonitoringListener listener = this.GetMonitoringListener(CacheOperationType.ClearCache);
			listener.Listen(delegate
			{
				this.InternalClear(listener);
			});
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000DE67 File Offset: 0x0000C067
		private void InternalClear(IMonitoringListener listener)
		{
			this._protocolInUse.Clear(listener);
			if (this.IsLocalCacheEnabled)
			{
				this.LocalCache.Clear();
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000DE88 File Offset: 0x0000C088
		public DataCacheNotificationDescriptor AddCacheLevelCallback(DataCacheOperations filter, DataCacheNotificationCallback clientCallback)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.PartitionClearCache))
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled || this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			if (!DataCache.IsValidCacheLevelMask(filter))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidOperationMask"), "filter");
			}
			if (clientCallback == null)
			{
				throw new ArgumentNullException("clientCallback");
			}
			return this.NotificationManagerInstance.AddNotificationCallback(this._myName, (int)filter, clientCallback);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000DF20 File Offset: 0x0000C120
		public DataCacheNotificationDescriptor AddCacheLevelBulkCallback(DataCacheBulkNotificationCallback clientCallback)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.AllCacheEvents))
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled || this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			if (clientCallback == null)
			{
				throw new ArgumentNullException("clientCallback");
			}
			return this.NotificationManagerInstance.AddBulkNotificationCallback(this._myName, 63, clientCallback);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000DF94 File Offset: 0x0000C194
		public DataCacheNotificationDescriptor AddRegionLevelCallback(string region, DataCacheOperations filter, DataCacheNotificationCallback clientCallback)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.CreateRegionEvent))
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled || this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			if (!DataCache.IsValidRegionLevelMask(filter))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidOperationMask"), "filter");
			}
			if (clientCallback == null)
			{
				throw new ArgumentNullException("clientCallback");
			}
			return this.NotificationManagerInstance.AddNotificationCallback(this._myName, region, (int)filter, clientCallback);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E038 File Offset: 0x0000C238
		public DataCacheNotificationDescriptor AddItemLevelCallback(string key, DataCacheOperations filter, DataCacheNotificationCallback clientCallback)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.AddEvent))
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled || this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!DataCache.IsValidItemLevelMask(filter))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidItemLevelMask"), "filter");
			}
			if (clientCallback == null)
			{
				throw new ArgumentNullException("clientCallback");
			}
			string systemRegionName = RegionNameProvider.GetSystemRegionName(key, this.Configuration.SystemRegionCount);
			return this.NotificationManagerInstance.AddNotificationCallback(this._myName, systemRegionName, key, (int)filter, clientCallback);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		public DataCacheNotificationDescriptor AddItemLevelCallback(string key, DataCacheOperations filter, DataCacheNotificationCallback clientCallback, string region)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.AddEvent))
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled || this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!DataCache.IsValidItemLevelMask(filter))
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "InvalidItemLevelMask"), "filter");
			}
			if (clientCallback == null)
			{
				throw new ArgumentNullException("clientCallback");
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			return this.NotificationManagerInstance.AddNotificationCallback(this._myName, region, key, (int)filter, clientCallback);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		public void RemoveCallback(DataCacheNotificationDescriptor notificationDescriptor)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.AllCacheEvents))
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled && this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			if (notificationDescriptor == null)
			{
				throw new ArgumentNullException("notificationDescriptor");
			}
			this.NotificationManagerInstance.RemoveCallback(notificationDescriptor);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E210 File Offset: 0x0000C410
		public DataCacheNotificationDescriptor AddFailureNotificationCallback(DataCacheFailureNotificationCallback failureCallback)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.NotificationMissEvent))
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled || this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			if (failureCallback == null)
			{
				throw new ArgumentNullException("failureCallback");
			}
			return this.NotificationManagerInstance.AddFailureNotificationCallback(this._myName, failureCallback);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E280 File Offset: 0x0000C480
		internal static Guid GetLastRequestTrackingId()
		{
			return SimpleSendReceiveModule.LastRequestTrackingId;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000E288 File Offset: 0x0000C488
		internal DataCacheItemVersion GetNextPollVersionForKey(string key)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.AllCacheEvents))
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled || this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			return this.NotificationManagerInstance.GetNextPollVersionForKey(this._myName, key);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		internal DataCacheItemVersion GetNextPollVersion(string regionName)
		{
			if (!this._supportProvider.AreNotificationsSupported(CacheEventType.AllCacheEvents) || !this._supportProvider.AreRegionsSupported())
			{
				DataCacheFactory.ThrowNotSupportedException();
			}
			if (!this.Configuration.Notification.IsEnabled || this.NotificationManagerInstance == null)
			{
				throw new NotSupportedException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "NotificationNotSupported"));
			}
			return this.NotificationManagerInstance.GetNextPollVersion(this._myName, regionName);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E35B File Offset: 0x0000C55B
		internal void AggregateProperties(IEnumerable<VelocityPacketProperty> properties, Action<VelocityPacketProperty, byte[]> callback)
		{
			this._protocolInUse.AggregateProperties(properties, callback);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000E36C File Offset: 0x0000C56C
		internal static bool IsValidCacheLevelMask(DataCacheOperations filterMask)
		{
			DataCacheOperations dataCacheOperations = ~(DataCacheOperations.AddItem | DataCacheOperations.ReplaceItem | DataCacheOperations.RemoveItem | DataCacheOperations.CreateRegion | DataCacheOperations.RemoveRegion | DataCacheOperations.ClearRegion);
			return filterMask != (DataCacheOperations)0 && (filterMask & dataCacheOperations) == (DataCacheOperations)0;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000E388 File Offset: 0x0000C588
		internal static bool IsValidRegionLevelMask(DataCacheOperations filterMask)
		{
			DataCacheOperations dataCacheOperations = ~(DataCacheOperations.AddItem | DataCacheOperations.ReplaceItem | DataCacheOperations.RemoveItem | DataCacheOperations.CreateRegion | DataCacheOperations.RemoveRegion | DataCacheOperations.ClearRegion);
			return filterMask != (DataCacheOperations)0 && (filterMask & dataCacheOperations) == (DataCacheOperations)0;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		internal static bool IsValidItemLevelMask(DataCacheOperations filterMask)
		{
			DataCacheOperations dataCacheOperations = ~(DataCacheOperations.AddItem | DataCacheOperations.ReplaceItem | DataCacheOperations.RemoveItem);
			return filterMask != (DataCacheOperations)0 && (filterMask & dataCacheOperations) == (DataCacheOperations)0;
		}

		// Token: 0x040000E9 RID: 233
		private const string Source = "DistributedCache.CacheAPI";

		// Token: 0x040000EA RID: 234
		private const int BULK_KEY_COUNT = 16;

		// Token: 0x040000EB RID: 235
		private const int DATA_CACHE_TAG_COUNT = 8;

		// Token: 0x040000EC RID: 236
		internal string _myName;

		// Token: 0x040000ED RID: 237
		internal DataCacheFactory _parentFactory;

		// Token: 0x040000EE RID: 238
		private ClientOperationsSupportProvider _supportProvider;

		// Token: 0x040000EF RID: 239
		private static Encoding encoding = new UTF8Encoding(false, false);

		// Token: 0x040000F0 RID: 240
		private DataCache.UnitTestParameters _unitTestParameters;

		// Token: 0x040000F1 RID: 241
		internal DataCacheObjectSerializationProvider cacheObjectSerializationProvider;

		// Token: 0x040000F2 RID: 242
		private IClientProtocol _protocolInUse;

		// Token: 0x040000F3 RID: 243
		internal static readonly int MaxKeyLength = 65535;

		// Token: 0x040000F4 RID: 244
		internal static readonly int MaxRegionLength = 65535;

		// Token: 0x0200003C RID: 60
		internal class UnitTestParameters
		{
			// Token: 0x06000299 RID: 665 RVA: 0x0000E3E1 File Offset: 0x0000C5E1
			internal UnitTestParameters()
			{
				this.WaitEvent1 = new AutoResetEvent(false);
				this.WaitEvent2 = new AutoResetEvent(false);
			}

			// Token: 0x040000FF RID: 255
			internal AutoResetEvent WaitEvent1;

			// Token: 0x04000100 RID: 256
			internal AutoResetEvent WaitEvent2;
		}
	}
}
