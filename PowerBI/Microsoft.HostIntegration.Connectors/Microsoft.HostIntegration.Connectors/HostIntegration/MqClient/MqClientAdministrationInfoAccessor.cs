using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.HostIntegration.ConfigurationSectionHandlers;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient;
using Microsoft.HostIntegration.MqClient.StrictResources.RuntimeAdministration;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BD4 RID: 3028
	internal class MqClientAdministrationInfoAccessor
	{
		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x06005E3C RID: 24124 RVA: 0x0018034B File Offset: 0x0017E54B
		// (set) Token: 0x06005E3D RID: 24125 RVA: 0x00180353 File Offset: 0x0017E553
		internal List<QueueManagerInformation> QueueManagers { get; private set; }

		// Token: 0x1700172E RID: 5934
		// (get) Token: 0x06005E3E RID: 24126 RVA: 0x0018035C File Offset: 0x0017E55C
		// (set) Token: 0x06005E3F RID: 24127 RVA: 0x00180364 File Offset: 0x0017E564
		internal List<QueueInformation> Queues { get; private set; }

		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06005E40 RID: 24128 RVA: 0x0018036D File Offset: 0x0017E56D
		// (set) Token: 0x06005E41 RID: 24129 RVA: 0x00180375 File Offset: 0x0017E575
		internal PoolingInformation Pooling { get; private set; }

		// Token: 0x06005E42 RID: 24130 RVA: 0x00180380 File Offset: 0x0017E580
		internal MqClientAdministrationInfoAccessor(AdministrationTracePoint administrationTracePoint)
		{
			this.tracePoint = administrationTracePoint;
			if (this.tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this.tracePoint.Trace(TraceFlags.Debug, "Reading the Configuration File, looking for the section: hostIntegration.mqClient");
			}
			this.appConfigHandler = ConfigurationManager.GetSection("hostIntegration.mqClient") as MqClientConfigurationSectionHandler;
			if (this.appConfigHandler == null)
			{
				this.Pooling = new PoolingInformation();
				this.QueueManagers = new List<QueueManagerInformation>();
				this.Queues = new List<QueueInformation>();
				if (this.tracePoint.IsEnabled(TraceFlags.Verbose))
				{
					this.tracePoint.Trace(TraceFlags.Verbose, "Configuration not found in config file");
				}
				return;
			}
			this.appConfigOrder = this.appConfigHandler.ReadOrder.AppConfig;
			this.cacheOrder = this.appConfigHandler.ReadOrder.Cache;
			if (!HisConfigurationSectionHandler.IsCachingAvailable)
			{
				this.cacheOrder = MqClientConfigurationPriority.Unused;
			}
			bool flag = true;
			if (this.appConfigOrder != MqClientConfigurationPriority.Unused && this.appConfigOrder == this.cacheOrder)
			{
				flag = false;
			}
			if (!flag)
			{
				ApplicationException ex = new ApplicationException(SR.DuplicateOrdering);
				if (this.tracePoint.IsEnabled(TraceFlags.Fatal))
				{
					this.tracePoint.Trace(TraceFlags.Fatal, ex);
				}
				throw ex;
			}
			if (this.tracePoint.IsEnabled(TraceFlags.Information))
			{
				this.tracePoint.Trace(TraceFlags.Information, "App.Config Order = " + this.appConfigOrder.ToString());
				this.tracePoint.Trace(TraceFlags.Information, "Cache Order = " + this.cacheOrder.ToString());
			}
			if (this.appConfigOrder == MqClientConfigurationPriority.Unused && this.cacheOrder == MqClientConfigurationPriority.Unused)
			{
				this.Pooling = new PoolingInformation();
				this.QueueManagers = new List<QueueManagerInformation>();
				this.Queues = new List<QueueInformation>();
				return;
			}
			if (this.appConfigOrder != MqClientConfigurationPriority.Unused)
			{
				this.configQMs = this.GetQMsFromConfigFile();
				this.configQs = this.GetQsFromConfigFile();
				this.configPooling = this.GetPoolingFromConfigFile();
			}
			if (this.cacheOrder != MqClientConfigurationPriority.Unused)
			{
				string cacheName = this.appConfigHandler.Cache.CacheName;
				string key = this.appConfigHandler.Cache.Key;
				string region = this.appConfigHandler.Cache.Region;
				this.cacheHandler = MqClientConfigurationSectionHandler.LoadFromCache(cacheName, key, region);
				this.cacheQMs = this.GetQMsFromCache();
				this.cacheQs = this.GetQsFromCache();
				this.cachePooling = this.GetPoolingFromCache();
			}
			this.Consolidate();
		}

		// Token: 0x06005E43 RID: 24131 RVA: 0x001805FC File Offset: 0x0017E7FC
		private void Consolidate()
		{
			this.ConsolidateQueueManagers();
			this.ConsolidateQueues();
			this.ConsolidatePooling();
		}

		// Token: 0x06005E44 RID: 24132 RVA: 0x00180610 File Offset: 0x0017E810
		private void ConsolidateQueueManagers()
		{
			List<QueueManagerInformation>[] array = new List<QueueManagerInformation>[3];
			array[(int)this.appConfigOrder] = this.configQMs;
			array[(int)this.cacheOrder] = this.cacheQMs;
			Dictionary<string, QueueManagerInformation> dictionary = new Dictionary<string, QueueManagerInformation>();
			for (int i = 2; i > 0; i--)
			{
				if (array[i] != null)
				{
					foreach (QueueManagerInformation queueManagerInformation in array[i])
					{
						dictionary[queueManagerInformation.Alias] = queueManagerInformation;
					}
				}
			}
			this.QueueManagers = new List<QueueManagerInformation>(dictionary.Count);
			foreach (QueueManagerInformation queueManagerInformation2 in dictionary.Values)
			{
				this.QueueManagers.Add(queueManagerInformation2);
			}
			Dictionary<string, Dictionary<int, bool>> dictionary2 = new Dictionary<string, Dictionary<int, bool>>();
			foreach (QueueManagerInformation queueManagerInformation3 in this.QueueManagers)
			{
				Dictionary<int, bool> dictionary3 = null;
				if (!dictionary2.TryGetValue(queueManagerInformation3.Host, out dictionary3))
				{
					dictionary3 = new Dictionary<int, bool>();
					dictionary2.Add(queueManagerInformation3.Host, dictionary3);
				}
				bool useSsl;
				if (!dictionary3.TryGetValue(queueManagerInformation3.Port, out useSsl))
				{
					useSsl = queueManagerInformation3.UseSsl;
					dictionary3.Add(queueManagerInformation3.Port, useSsl);
				}
				if (useSsl != queueManagerInformation3.UseSsl)
				{
					ApplicationException ex = new ApplicationException(SR.DifferentSslSettings(queueManagerInformation3.Host, queueManagerInformation3.Port));
					if (this.tracePoint.IsEnabled(TraceFlags.Fatal))
					{
						this.tracePoint.Trace(TraceFlags.Fatal, ex);
					}
					throw ex;
				}
			}
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x001807E8 File Offset: 0x0017E9E8
		private void ConsolidateQueues()
		{
			List<QueueInformation>[] array = new List<QueueInformation>[3];
			array[(int)this.appConfigOrder] = this.configQs;
			array[(int)this.cacheOrder] = this.cacheQs;
			Dictionary<string, QueueInformation> dictionary = new Dictionary<string, QueueInformation>();
			for (int i = 2; i > 0; i--)
			{
				if (array[i] != null)
				{
					foreach (QueueInformation queueInformation in array[i])
					{
						dictionary[queueInformation.Alias] = queueInformation;
					}
				}
			}
			this.Queues = new List<QueueInformation>(dictionary.Count);
			foreach (QueueInformation queueInformation2 in dictionary.Values)
			{
				this.Queues.Add(queueInformation2);
			}
			foreach (QueueInformation queueInformation3 in this.Queues)
			{
				bool flag = false;
				foreach (QueueManagerInformation queueManagerInformation in this.QueueManagers)
				{
					if (queueInformation3.QueueManagerAlias == queueManagerInformation.Alias)
					{
						queueInformation3.QueueManager = queueManagerInformation;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ApplicationException ex = new ApplicationException(SR.QueueWrongManager(queueInformation3.Alias, queueInformation3.QueueManagerAlias));
					if (this.tracePoint.IsEnabled(TraceFlags.Fatal))
					{
						this.tracePoint.Trace(TraceFlags.Fatal, ex);
					}
					throw ex;
				}
			}
		}

		// Token: 0x06005E46 RID: 24134 RVA: 0x001809B8 File Offset: 0x0017EBB8
		private void ConsolidatePooling()
		{
			if (this.configPooling == null && this.cachePooling == null)
			{
				this.Pooling = new PoolingInformation();
				return;
			}
			PoolingInformation[] array = new PoolingInformation[3];
			array[(int)this.appConfigOrder] = this.configPooling;
			array[(int)this.cacheOrder] = this.cachePooling;
			PoolingInformation.QueueManagerBehaviorInformation queueManagerBehaviorInformation = null;
			for (int i = 2; i > 0; i--)
			{
				if (array[i] != null)
				{
					PoolingInformation poolingInformation = array[i];
					if (poolingInformation.QueueManagerBehavior != null)
					{
						queueManagerBehaviorInformation = poolingInformation.QueueManagerBehavior;
					}
				}
			}
			this.Pooling = new PoolingInformation(queueManagerBehaviorInformation);
		}

		// Token: 0x06005E47 RID: 24135 RVA: 0x00180A36 File Offset: 0x0017EC36
		private List<QueueManagerInformation> GetQMsFromConfigFile()
		{
			return this.GetQMsFromConfigurationSection(this.appConfigHandler);
		}

		// Token: 0x06005E48 RID: 24136 RVA: 0x00180A44 File Offset: 0x0017EC44
		private List<QueueInformation> GetQsFromConfigFile()
		{
			return this.GetQsFromConfigurationSection(this.appConfigHandler);
		}

		// Token: 0x06005E49 RID: 24137 RVA: 0x00180A52 File Offset: 0x0017EC52
		private PoolingInformation GetPoolingFromConfigFile()
		{
			return this.GetPoolingFromConfigurationSection(this.appConfigHandler);
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x00180A60 File Offset: 0x0017EC60
		private List<QueueManagerInformation> GetQMsFromCache()
		{
			return this.GetQMsFromConfigurationSection(this.cacheHandler);
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x00180A6E File Offset: 0x0017EC6E
		private List<QueueInformation> GetQsFromCache()
		{
			return this.GetQsFromConfigurationSection(this.cacheHandler);
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x00180A7C File Offset: 0x0017EC7C
		private PoolingInformation GetPoolingFromCache()
		{
			return this.GetPoolingFromConfigurationSection(this.cacheHandler);
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x00180A8C File Offset: 0x0017EC8C
		private List<QueueManagerInformation> GetQMsFromConfigurationSection(MqClientConfigurationSectionHandler handler)
		{
			List<QueueManagerInformation> list = new List<QueueManagerInformation>();
			if (handler == null)
			{
				return list;
			}
			List<string> list2 = new List<string>();
			foreach (object obj in handler.QueueManagers)
			{
				QueueManager queueManager = (QueueManager)obj;
				if (list2.Contains(queueManager.Alias))
				{
					ApplicationException ex = new ApplicationException(SR.DuplicateManager(queueManager.Alias));
					if (this.tracePoint.IsEnabled(TraceFlags.Fatal))
					{
						this.tracePoint.Trace(TraceFlags.Fatal, ex);
					}
					throw ex;
				}
				list.Add(new QueueManagerInformation(queueManager.Alias, queueManager.Name, queueManager.Channel, queueManager.Host, queueManager.Port, queueManager.UseSsl, queueManager.ConnectAs, queueManager.DynamicQueueNamePrefix));
				list2.Add(queueManager.Alias);
			}
			return list;
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x00180B80 File Offset: 0x0017ED80
		private List<QueueInformation> GetQsFromConfigurationSection(MqClientConfigurationSectionHandler handler)
		{
			List<QueueInformation> list = new List<QueueInformation>();
			if (handler == null)
			{
				return list;
			}
			List<string> list2 = new List<string>();
			foreach (object obj in handler.Queues)
			{
				Queue queue = (Queue)obj;
				if (list2.Contains(queue.Alias))
				{
					ApplicationException ex = new ApplicationException(SR.DuplicateQueue(queue.Alias));
					if (this.tracePoint.IsEnabled(TraceFlags.Fatal))
					{
						this.tracePoint.Trace(TraceFlags.Fatal, ex);
					}
					throw ex;
				}
				list.Add(new QueueInformation(queue.Alias, queue.Name, queue.QueueManagerAlias));
				list2.Add(queue.Alias);
			}
			return list;
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x00180C50 File Offset: 0x0017EE50
		private PoolingInformation GetPoolingFromConfigurationSection(MqClientConfigurationSectionHandler handler)
		{
			if (!handler.Pooling.ElementInformation.IsPresent)
			{
				return null;
			}
			PoolingInformation.QueueManagerBehaviorInformation queueManagerBehaviorInformation = null;
			if (handler.Pooling.QueueManagerBehavior.ElementInformation.IsPresent)
			{
				QueueManagerBehavior queueManagerBehavior = handler.Pooling.QueueManagerBehavior;
				queueManagerBehaviorInformation = new PoolingInformation.QueueManagerBehaviorInformation(queueManagerBehavior.Pool, queueManagerBehavior.Timeout, queueManagerBehavior.AllowDifferentChannels, queueManagerBehavior.DifferentUserDifferentConversation, queueManagerBehavior.QueueManagersPerConversation);
			}
			if (queueManagerBehaviorInformation == null)
			{
				return null;
			}
			return new PoolingInformation(queueManagerBehaviorInformation);
		}

		// Token: 0x04004FB7 RID: 20407
		private MqClientConfigurationSectionHandler appConfigHandler;

		// Token: 0x04004FB8 RID: 20408
		private MqClientConfigurationSectionHandler cacheHandler;

		// Token: 0x04004FB9 RID: 20409
		private MqClientConfigurationPriority appConfigOrder = MqClientConfigurationPriority.First;

		// Token: 0x04004FBA RID: 20410
		private MqClientConfigurationPriority cacheOrder;

		// Token: 0x04004FBB RID: 20411
		private List<QueueManagerInformation> configQMs = new List<QueueManagerInformation>();

		// Token: 0x04004FBC RID: 20412
		private List<QueueManagerInformation> cacheQMs = new List<QueueManagerInformation>();

		// Token: 0x04004FBD RID: 20413
		private List<QueueInformation> configQs = new List<QueueInformation>();

		// Token: 0x04004FBE RID: 20414
		private List<QueueInformation> cacheQs = new List<QueueInformation>();

		// Token: 0x04004FBF RID: 20415
		private PoolingInformation configPooling;

		// Token: 0x04004FC0 RID: 20416
		private PoolingInformation cachePooling;

		// Token: 0x04004FC1 RID: 20417
		private const string HostIntegrationMqClientNode = "hostIntegration.mqClient";

		// Token: 0x04004FC5 RID: 20421
		private AdministrationTracePoint tracePoint;
	}
}
