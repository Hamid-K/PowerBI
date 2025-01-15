using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000024 RID: 36
	public abstract class DatabaseClientFactoryBase : Block, IDatabaseClientFactory
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004350 File Offset: 0x00002550
		protected DatabaseClientFactoryBase(string name)
			: base(name)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004364 File Offset: 0x00002564
		public TInterface Create<TInterface>() where TInterface : IDatabaseClient
		{
			return this.Create<TInterface>(typeof(TInterface).Name);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000437B File Offset: 0x0000257B
		public ReplicatedDatabaseClient<TInterface> CreateReplicated<TInterface>() where TInterface : IDatabaseClient
		{
			return this.CreateReplicated<TInterface>(typeof(TInterface).Name);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004392 File Offset: 0x00002592
		public TInterface Create<TInterface>(string identification) where TInterface : IDatabaseClient
		{
			return this.Create<TInterface>(identification, this.GetInstance<TInterface>(), this.m_databaseManager[identification]);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000043B0 File Offset: 0x000025B0
		public ReplicatedDatabaseClient<TInterface> CreateReplicated<TInterface>(string identification) where TInterface : IDatabaseClient
		{
			object instance = this.GetInstance<TInterface>();
			ReplicatedDatabaseSpecificationProxy replicatedProxy = this.m_databaseManager.GetReplicatedProxy(identification);
			TInterface tinterface = this.Create<TInterface>(identification, instance, replicatedProxy.Primary);
			TInterface tinterface2 = ((replicatedProxy.Secondary != null) ? this.Create<TInterface>(identification, instance, replicatedProxy.Secondary) : ((TInterface)((object)null)));
			TInterface tinterface3 = ((replicatedProxy.Primary == replicatedProxy.Active) ? tinterface : tinterface2);
			return new ReplicatedDatabaseClient<TInterface>(tinterface, tinterface2, tinterface3);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000441C File Offset: 0x0000261C
		protected void Register<TInterface>(Func<DatabaseClientCreationContext, TInterface> constructor) where TInterface : IDatabaseClient
		{
			Type typeFromHandle = typeof(TInterface);
			Dictionary<Type, object> clientMap = this.m_clientMap;
			lock (clientMap)
			{
				this.m_clientMap.Add(typeFromHandle, constructor);
				TraceSourceBase<StorageTrace>.Tracer.TraceInformation("Added constructor for {0}", new object[] { typeFromHandle.Name });
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000448C File Offset: 0x0000268C
		private object GetInstance<TInterface>()
		{
			Type typeFromHandle = typeof(TInterface);
			object obj;
			if (this.m_clientMap.TryGetValue(typeFromHandle, out obj))
			{
				return obj;
			}
			throw new DatabaseClientNotRegisteredException(typeFromHandle.Name);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000044C1 File Offset: 0x000026C1
		private TInterface Create<TInterface>(string identification, object instance, IDatabaseSpecificationProxy proxy)
		{
			return ((Func<DatabaseClientCreationContext, TInterface>)instance)(new DatabaseClientCreationContext(identification, proxy, base.WorkTicketManager, this.m_eventsKitFactory, this.m_activityFactory, this.m_modelFactory));
		}

		// Token: 0x04000066 RID: 102
		[BlockServiceDependency]
		private readonly IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000067 RID: 103
		[BlockServiceDependency]
		private readonly IDatabaseManager m_databaseManager;

		// Token: 0x04000068 RID: 104
		[BlockServiceDependency]
		private readonly IActivityFactory m_activityFactory;

		// Token: 0x04000069 RID: 105
		[BlockServiceDependency]
		private readonly IMonitoredActivityCompletionModelFactory m_modelFactory;

		// Token: 0x0400006A RID: 106
		private readonly Dictionary<Type, object> m_clientMap = new Dictionary<Type, object>();
	}
}
