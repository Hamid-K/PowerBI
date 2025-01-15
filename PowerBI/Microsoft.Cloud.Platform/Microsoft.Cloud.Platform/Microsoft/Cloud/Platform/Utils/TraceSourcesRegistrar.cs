using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Cloud.Platform.ConfigurationClasses.Tracing;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000163 RID: 355
	public class TraceSourcesRegistrar
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0001FF09 File Offset: 0x0001E109
		internal IEnumerable<TraceSourceIdentifier> TraceSourceIdentifiers
		{
			get
			{
				return this.m_traceSources.Keys;
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001FF16 File Offset: 0x0001E116
		public TraceSourcesRegistrar()
		{
			this.m_traceSources = new ConcurrentDictionary<TraceSourceIdentifier, Action<TraceSourceConfig>>();
			this.m_configLock = new object();
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001FF34 File Offset: 0x0001E134
		internal void RegisterSource(TraceSourceIdentifier id, Action<TraceSourceConfig> configUpdateMethod)
		{
			if (!this.m_traceSources.TryAdd(id, configUpdateMethod))
			{
				throw new TraceSourceAlreadyExistsException(id);
			}
			this.UpdateTraceSourceConfig(id, configUpdateMethod);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001FF54 File Offset: 0x0001E154
		public void UpdateConfig(TraceSourcesConfiguration config)
		{
			config.ValidateConfiguration();
			object configLock = this.m_configLock;
			lock (configLock)
			{
				this.m_config = config;
				foreach (KeyValuePair<TraceSourceIdentifier, Action<TraceSourceConfig>> keyValuePair in this.m_traceSources)
				{
					this.UpdateTraceSourceConfig(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001FFE4 File Offset: 0x0001E1E4
		public override string ToString()
		{
			IEnumerable<string> enumerable = this.m_traceSources.Keys.Select((TraceSourceIdentifier s) => s.ToString());
			string text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				Environment.NewLine,
				string.Join(Environment.NewLine, enumerable.ToArray<string>())
			});
			return string.Format(CultureInfo.InvariantCulture, "TraceSourceRegistrar with the following trace sources registered: {0}. \nConfig is {1}", new object[] { text, this.m_config });
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00020078 File Offset: 0x0001E278
		private void UpdateTraceSourceConfig(TraceSourceIdentifier id, Action<TraceSourceConfig> configUpdateMethod)
		{
			object configLock = this.m_configLock;
			lock (configLock)
			{
				if (this.m_config != null)
				{
					TraceSourceConfig traceSourceConfig2 = this.m_config.TraceSourceConfigList.Where((TraceSourceConfig traceSourceConfig) => traceSourceConfig.ID.Equals(id)).FirstOrDefault<TraceSourceConfig>();
					if (traceSourceConfig2 != null)
					{
						configUpdateMethod(traceSourceConfig2);
					}
				}
			}
		}

		// Token: 0x0400037D RID: 893
		private ConcurrentDictionary<TraceSourceIdentifier, Action<TraceSourceConfig>> m_traceSources;

		// Token: 0x0400037E RID: 894
		private volatile TraceSourcesConfiguration m_config;

		// Token: 0x0400037F RID: 895
		private object m_configLock;
	}
}
