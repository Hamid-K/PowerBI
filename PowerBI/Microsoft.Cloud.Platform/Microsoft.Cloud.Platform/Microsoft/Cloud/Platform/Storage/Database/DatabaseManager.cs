using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200003A RID: 58
	[BlockServiceProvider(typeof(IDatabaseManager), PublishWhen = BlockServicePublish.Default)]
	[BlockServiceProvider(typeof(IDynamicDatabaseManager), PublishWhen = BlockServicePublish.Default)]
	public sealed class DatabaseManager : Block, IDynamicDatabaseManager, IDatabaseManager, IDatabaseSpecificationProxyQuery
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00005184 File Offset: 0x00003384
		public DatabaseManager()
			: this(typeof(DatabaseManager).Name)
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000519B File Offset: 0x0000339B
		public DatabaseManager(string name)
			: base(name)
		{
			this.m_maps = new VolatileRef<DatabaseManager.Maps>(new DatabaseManager.Maps(new Dictionary<string, IDatabaseSpecification>(), new Dictionary<string, IDatabaseSpecification>()));
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600015C RID: 348 RVA: 0x000051CC File Offset: 0x000033CC
		// (remove) Token: 0x0600015D RID: 349 RVA: 0x00005204 File Offset: 0x00003404
		public event DatabaseSpecificationEnabledStateChanged DatabaseSpecificationStateChanged;

		// Token: 0x0600015E RID: 350 RVA: 0x0000523C File Offset: 0x0000343C
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = base.OnInitialize();
			if (blockInitializationStatus == BlockInitializationStatus.Done)
			{
				this.m_eventsKit = this.m_eventsKitFactory.CreateEventsKit<IDatabaseEventsKit>();
				this.m_throttlers = new DatabaseThrottlerManager(this.m_eventsKitFactory);
				this.m_configurationManager = this.m_cfgMgrFactory.GetConfigurationManager();
				this.m_configurationManager.Subscribe(new Type[] { typeof(DatabaseConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
			}
			return blockInitializationStatus;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000052B4 File Offset: 0x000034B4
		protected override void OnStart()
		{
			base.OnStart();
			object modificationLock = this.m_modificationLock;
			lock (modificationLock)
			{
				if (this.DatabaseSpecificationStateChanged != null)
				{
					foreach (KeyValuePair<string, IDatabaseSpecification> keyValuePair in this.m_maps.VolatileRead().Static)
					{
						this.DatabaseSpecificationStateChanged(this, new DatabaseSpecificationEnabledStateChangedEventArgs(keyValuePair.Value.OperationMode, keyValuePair.Key));
					}
				}
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005368 File Offset: 0x00003568
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			base.OnStop();
		}

		// Token: 0x1700004E RID: 78
		public IDatabaseSpecificationProxy this[string key]
		{
			get
			{
				DatabaseManager.Maps maps = this.m_maps.VolatileRead();
				this.GetEntry(maps, key);
				return new DatabaseSpecificationProxy(key, this);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000053B1 File Offset: 0x000035B1
		public IEnumerable<string> GetDatabaseIdentifiers(Func<string, bool> compareFunc)
		{
			return DatabaseManager.TryGetDatabaseIdentifiers(this.m_maps.VolatileRead(), compareFunc);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000053C4 File Offset: 0x000035C4
		public ReplicatedDatabaseSpecificationProxy GetReplicatedProxy(string key)
		{
			DatabaseManager.Maps maps = this.m_maps.VolatileRead();
			IDatabaseSpecification entry = this.GetEntry(maps, key);
			IDatabaseSpecification entry2 = this.GetEntry(maps, DatabaseManager.GetPrimarySpecificationKey(key));
			bool flag = DatabaseManager.TryGetEntry(maps, DatabaseManager.GetSecondarySpecificationKey(key)) != null;
			DatabaseSpecificationProxy databaseSpecificationProxy = new DatabaseSpecificationProxy(DatabaseManager.GetPrimarySpecificationKey(key), this);
			DatabaseSpecificationProxy databaseSpecificationProxy2 = (flag ? new DatabaseSpecificationProxy(DatabaseManager.GetSecondarySpecificationKey(key), this) : null);
			DatabaseSpecificationProxy databaseSpecificationProxy3 = (entry.ConnectionString.Equals(entry2.ConnectionString) ? databaseSpecificationProxy : databaseSpecificationProxy2);
			return new ReplicatedDatabaseSpecificationProxy(databaseSpecificationProxy, databaseSpecificationProxy2, databaseSpecificationProxy3);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005440 File Offset: 0x00003640
		public void AddDatabaseSpecification(DatabaseSpecificationConfiguration specification)
		{
			object modificationLock = this.m_modificationLock;
			lock (modificationLock)
			{
				DatabaseManager.Maps maps = this.m_maps.VolatileRead();
				DatabaseManager.Maps maps2 = new DatabaseManager.Maps(maps.Static, new Dictionary<string, IDatabaseSpecification>(maps.Dynamic));
				DatabaseStorageUnitConfiguration unit = specification.Unit;
				maps2.Dynamic.Add(specification.Key, new DatabaseSpecification(unit.Primary.ConnectionString, unit.Primary.OperationMode, specification.RetryProfiles, specification.CommandTimeout, this.m_throttlers.Create(specification.Key, specification.ThrottlerConfiguration), specification.BulkInsertBatchSize));
				maps2.Dynamic.Add(DatabaseManager.GetPrimarySpecificationKey(specification.Key), new DatabaseSpecification(unit.Primary.ConnectionString, unit.Primary.OperationMode, specification.RetryProfiles, specification.CommandTimeout, this.m_throttlers.Create(DatabaseManager.GetPrimarySpecificationKey(specification.Key), specification.ThrottlerConfiguration), specification.BulkInsertBatchSize));
				if (!string.IsNullOrWhiteSpace(specification.Unit.Secondary.ConnectionString))
				{
					maps2.Dynamic.Add(DatabaseManager.GetSecondarySpecificationKey(specification.Key), new DatabaseSpecification(unit.Secondary.ConnectionString, unit.Secondary.OperationMode, specification.RetryProfiles, specification.CommandTimeout, this.m_throttlers.Create(DatabaseManager.GetSecondarySpecificationKey(specification.Key), specification.ThrottlerConfiguration), specification.BulkInsertBatchSize));
				}
				this.m_maps.VolatileWrite(maps2);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000055F0 File Offset: 0x000037F0
		public void RemoveDatabaseSpecification(string key)
		{
			object modificationLock = this.m_modificationLock;
			lock (modificationLock)
			{
				DatabaseManager.Maps maps = this.m_maps.VolatileRead();
				DatabaseManager.Maps maps2 = new DatabaseManager.Maps(maps.Static, new Dictionary<string, IDatabaseSpecification>(maps.Dynamic));
				if (!maps2.Dynamic.Remove(key))
				{
					throw new KeyNotFoundException(key);
				}
				this.m_maps.VolatileWrite(maps2);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005670 File Offset: 0x00003870
		public void ClearDatabaseSpecifications()
		{
			object modificationLock = this.m_modificationLock;
			lock (modificationLock)
			{
				DatabaseManager.Maps maps = new DatabaseManager.Maps(this.m_maps.VolatileRead().Static, new Dictionary<string, IDatabaseSpecification>());
				this.m_maps.VolatileWrite(maps);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000056D4 File Offset: 0x000038D4
		public IDatabaseSpecification GetSpecification(string key)
		{
			DatabaseManager.Maps maps = this.m_maps.VolatileRead();
			return this.GetEntry(maps, key);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000056F8 File Offset: 0x000038F8
		public IDatabaseSpecification GetSpecificationPreferSecondary(string key)
		{
			DatabaseManager.Maps maps = this.m_maps.VolatileRead();
			IDatabaseSpecification entry = this.GetEntry(maps, DatabaseManager.GetPrimarySpecificationKey(key));
			IDatabaseSpecification databaseSpecification = DatabaseManager.TryGetEntry(maps, DatabaseManager.GetSecondarySpecificationKey(key));
			IDatabaseSpecification databaseSpecification2;
			if (databaseSpecification != null)
			{
				TraceSourceBase<StorageTrace>.Tracer.TraceInformation("Inside GetSpecificationPreferSecondary: Using Secondary");
				databaseSpecification2 = databaseSpecification;
			}
			else
			{
				TraceSourceBase<StorageTrace>.Tracer.TraceInformation("Inside GetSpecificationPreferSecondary: Using Primary");
				databaseSpecification2 = entry;
			}
			StorageOperationMode storageOperationMode = ((databaseSpecification2.OperationMode == StorageOperationMode.None) ? StorageOperationMode.None : StorageOperationMode.Read);
			return new DatabaseSpecification(databaseSpecification2.ConnectionString, storageOperationMode, databaseSpecification2.RetryProfile, databaseSpecification2.CommandTimeout, databaseSpecification2.Throttler, databaseSpecification2.BulkInsertBatchSize);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005788 File Offset: 0x00003988
		private void OnConfigurationChange(IConfigurationContainer configContainer)
		{
			object modificationLock = this.m_modificationLock;
			lock (modificationLock)
			{
				DatabaseManager.Maps maps = this.m_maps.VolatileRead();
				DatabaseManager.Maps maps2 = new DatabaseManager.Maps(new Dictionary<string, IDatabaseSpecification>(), maps.Dynamic);
				foreach (DatabaseSpecificationConfiguration databaseSpecificationConfiguration in configContainer.GetConfiguration<DatabaseConfiguration>().Specifications)
				{
					maps2.Static.Add(DatabaseManager.GetPrimarySpecificationKey(databaseSpecificationConfiguration.Key), new DatabaseSpecification(databaseSpecificationConfiguration.Unit.Primary.ConnectionString, databaseSpecificationConfiguration.Unit.Primary.OperationMode, databaseSpecificationConfiguration.RetryProfiles, databaseSpecificationConfiguration.CommandTimeout, this.m_throttlers.Create(DatabaseManager.GetPrimarySpecificationKey(databaseSpecificationConfiguration.Key), databaseSpecificationConfiguration.ThrottlerConfiguration), databaseSpecificationConfiguration.BulkInsertBatchSize));
					if (!string.IsNullOrWhiteSpace(databaseSpecificationConfiguration.Unit.Secondary.ConnectionString))
					{
						maps2.Static.Add(DatabaseManager.GetSecondarySpecificationKey(databaseSpecificationConfiguration.Key), new DatabaseSpecification(databaseSpecificationConfiguration.Unit.Secondary.ConnectionString, databaseSpecificationConfiguration.Unit.Secondary.OperationMode, databaseSpecificationConfiguration.RetryProfiles, databaseSpecificationConfiguration.CommandTimeout, this.m_throttlers.Create(DatabaseManager.GetSecondarySpecificationKey(databaseSpecificationConfiguration.Key), databaseSpecificationConfiguration.ThrottlerConfiguration), databaseSpecificationConfiguration.BulkInsertBatchSize));
					}
					StorageOperationMode storageOperationMode = databaseSpecificationConfiguration.Unit.Primary.OperationMode;
					string text;
					IThrottler throttler;
					if (storageOperationMode != StorageOperationMode.None || string.IsNullOrWhiteSpace(databaseSpecificationConfiguration.Unit.Secondary.ConnectionString))
					{
						text = databaseSpecificationConfiguration.Unit.Primary.ConnectionString;
						throttler = maps2.Static[DatabaseManager.GetPrimarySpecificationKey(databaseSpecificationConfiguration.Key)].Throttler;
					}
					else
					{
						storageOperationMode = databaseSpecificationConfiguration.Unit.Secondary.OperationMode;
						text = databaseSpecificationConfiguration.Unit.Secondary.ConnectionString;
						throttler = maps2.Static[DatabaseManager.GetSecondarySpecificationKey(databaseSpecificationConfiguration.Key)].Throttler;
					}
					DatabaseSpecification databaseSpecification = new DatabaseSpecification(text, storageOperationMode, databaseSpecificationConfiguration.RetryProfiles, databaseSpecificationConfiguration.CommandTimeout, throttler, databaseSpecificationConfiguration.BulkInsertBatchSize);
					TraceSourceBase<StorageTrace>.Tracer.TraceInformation("Specification: {0} -> {1}", new object[] { databaseSpecificationConfiguration.Key, databaseSpecification });
					maps2.Static.Add(databaseSpecificationConfiguration.Key, databaseSpecification);
					if (this.DatabaseSpecificationStateChanged != null)
					{
						this.DatabaseSpecificationStateChanged(this, new DatabaseSpecificationEnabledStateChangedEventArgs(storageOperationMode, databaseSpecificationConfiguration.Key));
					}
				}
				this.m_maps.VolatileWrite(maps2);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005A68 File Offset: 0x00003C68
		private IDatabaseSpecification GetEntry(DatabaseManager.Maps maps, string key)
		{
			IDatabaseSpecification databaseSpecification = DatabaseManager.TryGetEntry(maps, key);
			if (databaseSpecification == null)
			{
				this.OnStorageUnreachable(key);
			}
			return databaseSpecification;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005A7C File Offset: 0x00003C7C
		private static IDatabaseSpecification TryGetEntry(DatabaseManager.Maps maps, string key)
		{
			IDatabaseSpecification databaseSpecification;
			if (!maps.Static.TryGetValue(key, out databaseSpecification))
			{
				maps.Dynamic.TryGetValue(key, out databaseSpecification);
			}
			return databaseSpecification;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005AAC File Offset: 0x00003CAC
		private static IEnumerable<string> TryGetDatabaseIdentifiers(DatabaseManager.Maps maps, Func<string, bool> compareFunc)
		{
			ExtendedDiagnostics.EnsureNotNull<Func<string, bool>>(compareFunc, "compareFunc");
			IEnumerable<string> enumerable = maps.Static.Keys.Where((string i) => compareFunc(i));
			if (!enumerable.IsNullOrEmpty<string>())
			{
				return enumerable;
			}
			return maps.Dynamic.Keys.Where((string i) => compareFunc(i));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005B1C File Offset: 0x00003D1C
		private void OnStorageUnreachable(string key)
		{
			DatabaseUnreachableException ex = new DatabaseUnreachableException("The requested storage type {0} is not available.".FormatWithInvariantCulture(new object[] { key }));
			this.m_eventsKit.NotifyMissingStorageSpecification(key, ex);
			throw ex;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005B51 File Offset: 0x00003D51
		private static string GetPrimarySpecificationKey(string key)
		{
			return "P.{0}".FormatWithInvariantCulture(new object[] { key });
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005B67 File Offset: 0x00003D67
		private static string GetSecondarySpecificationKey(string key)
		{
			return "S.{0}".FormatWithInvariantCulture(new object[] { key });
		}

		// Token: 0x0400009D RID: 157
		[BlockServiceDependency]
		private readonly IConfigurationManagerFactory m_cfgMgrFactory;

		// Token: 0x0400009E RID: 158
		private IConfigurationManager m_configurationManager;

		// Token: 0x0400009F RID: 159
		[BlockServiceDependency]
		private readonly IEventsKitFactory m_eventsKitFactory;

		// Token: 0x040000A0 RID: 160
		private IDatabaseEventsKit m_eventsKit;

		// Token: 0x040000A1 RID: 161
		[AutoShuttable]
		private DatabaseThrottlerManager m_throttlers;

		// Token: 0x040000A2 RID: 162
		private VolatileRef<DatabaseManager.Maps> m_maps;

		// Token: 0x040000A3 RID: 163
		private readonly object m_modificationLock = new object();

		// Token: 0x02000586 RID: 1414
		private sealed class Maps
		{
			// Token: 0x06002A86 RID: 10886 RVA: 0x000987A4 File Offset: 0x000969A4
			public Maps(Dictionary<string, IDatabaseSpecification> staticMap, Dictionary<string, IDatabaseSpecification> dynamicMap)
			{
				this.Static = staticMap;
				this.Dynamic = dynamicMap;
			}

			// Token: 0x170006D8 RID: 1752
			// (get) Token: 0x06002A87 RID: 10887 RVA: 0x000987BA File Offset: 0x000969BA
			// (set) Token: 0x06002A88 RID: 10888 RVA: 0x000987C2 File Offset: 0x000969C2
			public Dictionary<string, IDatabaseSpecification> Static { get; private set; }

			// Token: 0x170006D9 RID: 1753
			// (get) Token: 0x06002A89 RID: 10889 RVA: 0x000987CB File Offset: 0x000969CB
			// (set) Token: 0x06002A8A RID: 10890 RVA: 0x000987D3 File Offset: 0x000969D3
			public Dictionary<string, IDatabaseSpecification> Dynamic { get; private set; }
		}
	}
}
