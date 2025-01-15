using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.SessionState;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200003A RID: 58
	[Obsolete("This type is obsolete and will be removed in a future release. Please use Microsoft.Web.DistributedCache.DistributedCacheSessionStateStoreProvider in assembly Microsoft.Web.DistributedCache.dll instead", false)]
	public sealed class DataCacheSessionStoreProvider : SessionStateStoreProviderBase
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00009E60 File Offset: 0x00008060
		public string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009E68 File Offset: 0x00008068
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "Initialize Null config received", new object[0]);
				}
				throw new ArgumentNullException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 6001));
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, string, string>("DistributedCache.DataCacheSessionStoreProvider", "Entered Initialize: Parameters: name = {0} cacheName = {1} applicationName= {2}", name, config["cacheName"], config["sharedId"]);
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "DataCacheSessionStoreProvider";
			}
			if (string.IsNullOrEmpty(config["description"]))
			{
				config.Remove("description");
				config.Add("description", "SessionStateProvider");
			}
			base.Initialize(name, config);
			string text = config["retryCount"];
			int num;
			if (text != null && int.TryParse(text, out num) && num > 0)
			{
				this._maxRetryCount = num;
			}
			string text2 = config["retryInterval"];
			TimeSpan timeSpan;
			if (text2 != null && TimeSpan.TryParse(text2, out timeSpan) && timeSpan >= TimeSpan.Zero)
			{
				this._retryInterval = timeSpan;
			}
			this._cacheName = config["cacheName"];
			if (this._cacheName != null && this._cacheName.Trim().Length == 0)
			{
				throw new DataCacheException("DistributedCache.DataCacheSessionStoreProvider", 6005, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 6005), true);
			}
			this._applicationName = config["sharedId"];
			if (this._applicationName != null && this._applicationName.Trim().Length == 0)
			{
				throw new DataCacheException("DistributedCache.DataCacheSessionStoreProvider", 6006, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 6006), true);
			}
			if (this._applicationName == null)
			{
				this._applicationName = HostingEnvironment.ApplicationVirtualPath;
			}
			SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
			this._sessionTimeout = sessionStateSection.Timeout;
			config.Remove("cacheName");
			config.Remove("sharedId");
			config.Remove("retryCount");
			config.Remove("retryInterval");
			if (config.Count > 0)
			{
				string key = config.GetKey(0);
				if (!string.IsNullOrEmpty(key))
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "Initialize unknown parameter in config", new object[0]);
					}
					throw new ProviderException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 6003) + key);
				}
			}
			try
			{
				if (DataCacheSessionStoreProvider._cacheFactory == null)
				{
					lock (DataCacheSessionStoreProvider._lockObjct)
					{
						if (DataCacheSessionStoreProvider._cacheFactory == null)
						{
							DataCacheSessionStoreProvider._cacheFactory = new DataCacheFactory(new DataCacheFactoryConfiguration
							{
								LocalCacheProperties = new DataCacheLocalCacheProperties()
							});
						}
					}
				}
				if (this._cacheName == null)
				{
					this._cache = DataCacheSessionStoreProvider._cacheFactory.GetDefaultCache();
				}
				else
				{
					this._cache = DataCacheSessionStoreProvider._cacheFactory.GetCache(this._cacheName);
				}
			}
			catch (DataCacheException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "Initialize: Get Cache Failed : {0}", new object[] { ex });
				}
				throw;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("DistributedCache.DataCacheSessionStoreProvider", "Exiting Initialize");
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void Dispose()
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			return false;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A194 File Offset: 0x00008394
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			string sessionKey = this.getSessionKey(id);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, object, bool>("DistributedCache.DataCacheSessionStoreProvider", "Entered SetAndReleaseItemExclusive: Parameters: SessionId = {0}, lockId = {1}, newItem = {2}", sessionKey, lockId, newItem);
			}
			TimeSpan timeSpan = new TimeSpan(0, item.Timeout, 0);
			SessionStoreProviderData sessionStoreProviderData = new SessionStoreProviderData(SessionStateActions.None, item);
			bool flag = false;
			int num = 0;
			do
			{
				try
				{
					if (newItem)
					{
						this._cache.Put(sessionKey, sessionStoreProviderData, timeSpan);
					}
					else
					{
						this._cache.PutAndUnlock(sessionKey, sessionStoreProviderData, (DataCacheLockHandle)lockId, timeSpan);
					}
					flag = true;
				}
				catch (Exception ex)
				{
					if (!newItem && ex is HttpException)
					{
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "SetAndReleaseItemExclusive: Exception Received : {0}: SessionId {1}", new object[] { ex, sessionKey });
						}
						this.ReleaseItemExclusive(context, id, lockId);
					}
					DataCacheException ex2 = ex as DataCacheException;
					if (ex2 == null || !ex2.IsRetryable() || num >= this._maxRetryCount)
					{
						if (Provider.IsEnabled(TraceLevel.Error))
						{
							EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "SetAndReleaseItemExclusive: Exception thrown : {0}: SessionId {1}", new object[] { ex, sessionKey });
						}
						throw;
					}
					if (Provider.IsEnabled(TraceLevel.Warning))
					{
						EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "SetAndReleaseItemExclusive:Retrying: Exception: {0}: SessionId {1}", new object[] { ex, sessionKey });
					}
					num++;
					Thread.Sleep(this._retryInterval);
				}
			}
			while (!flag);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "SetAndReleaseItemExclusive: Exiting SessionId:{0}", sessionKey);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A314 File Offset: 0x00008514
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			SessionStoreProviderData sessionStoreProviderData = null;
			SessionStateStoreData sessionStateStoreData = null;
			TimeSpan sessionTimeout = this._sessionTimeout;
			string sessionKey = this.getSessionKey(id);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "Entered GetItem Input Parameters: Input Parameters SessionId = {0}", sessionKey);
			}
			lockAge = TimeSpan.Zero;
			lockId = null;
			locked = false;
			actions = SessionStateActions.None;
			bool flag = false;
			int num = 0;
			do
			{
				try
				{
					this._cache.ResetObjectTimeout(sessionKey, sessionTimeout);
					DataCacheItem cacheItem = this._cache.GetCacheItem(sessionKey);
					if (cacheItem != null)
					{
						sessionStoreProviderData = (SessionStoreProviderData)cacheItem.Value;
					}
					if (sessionStoreProviderData != null)
					{
						actions = sessionStoreProviderData.ActionFlag;
						if (sessionStoreProviderData.ActionFlag == SessionStateActions.None)
						{
							sessionStateStoreData = sessionStoreProviderData.Item;
							if (sessionStateStoreData.StaticObjects == null)
							{
								sessionStateStoreData = new SessionStateStoreData(sessionStateStoreData.Items, SessionStateUtility.GetSessionStaticObjects(context), sessionStateStoreData.Timeout);
							}
						}
						else
						{
							sessionStateStoreData = this.CreateNewStoreData(context, (int)this._sessionTimeout.TotalMinutes);
						}
					}
					flag = true;
				}
				catch (Exception ex)
				{
					DataCacheException ex2 = ex as DataCacheException;
					if (ex2 != null && ex2.ErrorCode == 6)
					{
						flag = true;
					}
					else
					{
						if (ex2 == null || !ex2.IsRetryable() || num >= this._maxRetryCount)
						{
							if (Provider.IsEnabled(TraceLevel.Error))
							{
								EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "GetItem: Exception thrown : {0}: SessionId:{1}", new object[] { ex, sessionKey });
							}
							throw;
						}
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "GetItem:Retrying: Exception: {0}: SessionId:{1}", new object[] { ex, sessionKey });
						}
						num++;
						Thread.Sleep(this._retryInterval);
					}
				}
			}
			while (!flag);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "GetItem: Exiting SessionId:{0}", sessionKey);
			}
			return sessionStateStoreData;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			TimeSpan timeSpan = new TimeSpan(0, 0, context.Server.ScriptTimeout);
			string sessionKey = this.getSessionKey(id);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "Entered GetItemExclusive: Input Parameters: SessionId = {0}", sessionKey);
			}
			SessionStateStoreData sessionStateStoreData = null;
			lockAge = TimeSpan.Zero;
			lockId = null;
			locked = false;
			actions = SessionStateActions.None;
			bool flag = false;
			int num = 0;
			do
			{
				try
				{
					DataCacheLockHandle dataCacheLockHandle;
					SessionStoreProviderData sessionStoreProviderData = (SessionStoreProviderData)this._cache.GetAndLock(sessionKey, timeSpan, out dataCacheLockHandle);
					lockId = dataCacheLockHandle;
					actions = sessionStoreProviderData.ActionFlag;
					if (sessionStoreProviderData.ActionFlag == SessionStateActions.None)
					{
						sessionStateStoreData = sessionStoreProviderData.Item;
						if (sessionStateStoreData.StaticObjects == null)
						{
							sessionStateStoreData = new SessionStateStoreData(sessionStateStoreData.Items, SessionStateUtility.GetSessionStaticObjects(context), sessionStateStoreData.Timeout);
						}
					}
					else
					{
						sessionStateStoreData = this.CreateNewStoreData(context, (int)this._sessionTimeout.TotalMinutes);
					}
					flag = true;
				}
				catch (Exception ex)
				{
					DataCacheException ex2 = ex as DataCacheException;
					if (ex2 != null && ex2.ErrorCode == 6)
					{
						flag = true;
					}
					else if (ex2 != null && ex2.ErrorCode == 11)
					{
						locked = true;
						lockId = new object();
						flag = true;
					}
					else
					{
						if (ex2 == null || !ex2.IsRetryable() || num >= this._maxRetryCount)
						{
							if (Provider.IsEnabled(TraceLevel.Error))
							{
								EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "GetItemExclusive: Exception thrown : {0}: SessionId:{1}", new object[] { ex, sessionKey });
							}
							throw;
						}
						num++;
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "GetItemExclusive:Retrying: Exception: {0}: SessionId:{1}", new object[] { ex, sessionKey });
						}
						Thread.Sleep(this._retryInterval);
					}
				}
			}
			while (!flag);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "GetItemExclusive: SessionId: {0}", sessionKey);
			}
			return sessionStateStoreData;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A694 File Offset: 0x00008894
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			TimeSpan sessionTimeout = this._sessionTimeout;
			string sessionKey = this.getSessionKey(id);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, object>("DistributedCache.DataCacheSessionStoreProvider", "Entered ReleaseItemExclusive: Input Parameters: SessionId = {0}, lockId = {1}", sessionKey, lockId);
			}
			DataCacheLockHandle dataCacheLockHandle = (DataCacheLockHandle)lockId;
			int num = 0;
			bool flag = false;
			do
			{
				try
				{
					this._cache.Unlock(sessionKey, dataCacheLockHandle, sessionTimeout);
					flag = true;
				}
				catch (Exception ex)
				{
					DataCacheException ex2 = ex as DataCacheException;
					if (ex2 != null && (ex2.ErrorCode == 6 || ex2.ErrorCode == 13 || ex2.ErrorCode == 12))
					{
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "ReleaseItemExclusive: Exception: {0}: SessionId:{1}", new object[] { ex, sessionKey });
						}
						flag = true;
					}
					else
					{
						if (ex2 == null || !ex2.IsRetryable() || num >= this._maxRetryCount)
						{
							if (Provider.IsEnabled(TraceLevel.Error))
							{
								EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "ReleaseItemExclusive: Exception thrown : {0}: SessionId:{1}", new object[] { ex, sessionKey });
							}
							throw;
						}
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "ReleaseItemExclusive:Retrying: Exception: {0}: SessionId:{1}", new object[] { ex, sessionKey });
						}
						num++;
						Thread.Sleep(this._retryInterval);
					}
				}
			}
			while (!flag);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "ReleaseItemExclusive: SessionId:{0}", sessionKey);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A800 File Offset: 0x00008A00
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			string sessionKey = this.getSessionKey(id);
			bool flag = false;
			int num = 0;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, object>("DistributedCache.DataCacheSessionStoreProvider", "Entered RemoveItem: Input Parameters: SessionId = {0}, lockId = {1}", sessionKey, lockId);
			}
			do
			{
				try
				{
					this._cache.Remove(sessionKey, (DataCacheLockHandle)lockId);
					flag = true;
				}
				catch (Exception ex)
				{
					DataCacheException ex2 = ex as DataCacheException;
					if (ex2 == null || !ex2.IsRetryable() || num >= this._maxRetryCount)
					{
						if (Provider.IsEnabled(TraceLevel.Error))
						{
							EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "RemoveItem: Exception thrown : {0}: SessionId:{1}", new object[] { ex, sessionKey });
						}
						throw;
					}
					if (Provider.IsEnabled(TraceLevel.Warning))
					{
						EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "RemoveItem:Retrying: Exception: {0}: SessionId:{1}", new object[] { ex, sessionKey });
					}
					num++;
					Thread.Sleep(this._retryInterval);
				}
			}
			while (!flag);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "RemoveItem: Exiting SessionId:{0}", sessionKey);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A904 File Offset: 0x00008B04
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			string sessionKey = this.getSessionKey(id);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string, int>("DistributedCache.DataCacheSessionStoreProvider", "Entered CreateUninitializedItem Input Parameters: SessionId= {0} Timeout= {1}", sessionKey, timeout);
			}
			SessionStateStoreData sessionStateStoreData = this.CreateNewStoreData(context, timeout);
			TimeSpan timeSpan = new TimeSpan(0, timeout, 0);
			SessionStoreProviderData sessionStoreProviderData = new SessionStoreProviderData(SessionStateActions.InitializeItem, sessionStateStoreData);
			bool flag = false;
			int num = 0;
			do
			{
				try
				{
					this._cache.Add(sessionKey, sessionStoreProviderData, timeSpan);
					flag = true;
				}
				catch (Exception ex)
				{
					DataCacheException ex2 = ex as DataCacheException;
					if (ex2 != null && ex2.ErrorCode == 8)
					{
						flag = true;
					}
					else
					{
						if (ex2 == null || !ex2.IsRetryable() || num >= this._maxRetryCount)
						{
							if (Provider.IsEnabled(TraceLevel.Error))
							{
								EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "CreateUninitializedItem: Exception thrown : {0}: SessionId {1}", new object[] { ex, sessionKey });
							}
							throw;
						}
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "CreateUninitializedItem:Retrying: Exception: {0}: SessionId {1}", new object[] { ex, sessionKey });
						}
						num++;
					}
				}
			}
			while (!flag);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "Exiting CreateUninitializedItem: SessionId {0}", sessionKey);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000AA30 File Offset: 0x00008C30
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return new SessionStateStoreData(new SessionStateItemCollection(), SessionStateUtility.GetSessionStaticObjects(context), timeout);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000AA44 File Offset: 0x00008C44
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			string sessionKey = this.getSessionKey(id);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "Entered ResetItemTimeout Input Parameters: SessionId={0}", sessionKey);
			}
			bool flag = false;
			int num = 0;
			do
			{
				try
				{
					this._cache.ResetObjectTimeout(sessionKey, this._sessionTimeout);
					flag = true;
				}
				catch (Exception ex)
				{
					DataCacheException ex2 = ex as DataCacheException;
					if (ex2 != null && (ex2.ErrorCode == 6 || (ex2.ErrorCode == 17 && ex2.SubStatus == 4)))
					{
						flag = true;
					}
					else
					{
						if (ex2 == null || !ex2.IsRetryable() || num >= this._maxRetryCount)
						{
							if (Provider.IsEnabled(TraceLevel.Error))
							{
								EventLogWriter.WriteError("DistributedCache.DataCacheSessionStoreProvider", "ResetItemTimeout:Exception thrown : {0}: SessionId:{1}", new object[] { ex, sessionKey });
							}
							throw;
						}
						num++;
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning("DistributedCache.DataCacheSessionStoreProvider", "ResetItemTimeout:Retrying: Exception thrown : {0}: SessionId:{1}", new object[] { ex, sessionKey });
						}
						Thread.Sleep(this._retryInterval);
					}
				}
			}
			while (!flag);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.DataCacheSessionStoreProvider", "Exiting ResetItemTimeout: SessionId {0}", sessionKey);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void InitializeRequest(HttpContext context)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void EndRequest(HttpContext context)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AB70 File Offset: 0x00008D70
		private string getSessionKey(string sessionID)
		{
			return this.ApplicationName + sessionID;
		}

		// Token: 0x040000E0 RID: 224
		private const string _logSource = "DistributedCache.DataCacheSessionStoreProvider";

		// Token: 0x040000E1 RID: 225
		private TimeSpan _sessionTimeout;

		// Token: 0x040000E2 RID: 226
		private string _applicationName;

		// Token: 0x040000E3 RID: 227
		private string _cacheName;

		// Token: 0x040000E4 RID: 228
		private DataCache _cache;

		// Token: 0x040000E5 RID: 229
		private static object _lockObjct = new object();

		// Token: 0x040000E6 RID: 230
		private static DataCacheFactory _cacheFactory;

		// Token: 0x040000E7 RID: 231
		private int _maxRetryCount = 2;

		// Token: 0x040000E8 RID: 232
		private TimeSpan _retryInterval = new TimeSpan(0, 0, 1);
	}
}
