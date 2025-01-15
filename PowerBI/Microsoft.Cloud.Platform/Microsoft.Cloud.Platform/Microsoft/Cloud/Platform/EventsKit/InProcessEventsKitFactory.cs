using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200035D RID: 861
	[BlockServiceProvider(typeof(IEventsKitFactory), BlockServiceProviderIdentity.Default)]
	public class InProcessEventsKitFactory : Block, IEventsKitFactory
	{
		// Token: 0x06001973 RID: 6515 RVA: 0x0005E594 File Offset: 0x0005C794
		public InProcessEventsKitFactory()
			: base(typeof(InProcessEventsKitFactory).Name)
		{
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0005E5B8 File Offset: 0x0005C7B8
		public T CreateEventsKit<T>() where T : class
		{
			string name = typeof(T).Name;
			Dictionary<string, object> eventKitsCache = this.m_eventKitsCache;
			T t;
			lock (eventKitsCache)
			{
				object obj;
				if (!this.m_eventKitsCache.TryGetValue(name, out obj))
				{
					GeneratedEventsKitImplementationTypes generatedEventsKitImplementationTypes = null;
					Dictionary<string, GeneratedEventsKitImplementationTypes> dictionary = InProcessEventsKitFactory.s_eventKitTypesCache;
					lock (dictionary)
					{
						if (!InProcessEventsKitFactory.s_eventKitTypesCache.TryGetValue(name, out generatedEventsKitImplementationTypes))
						{
							generatedEventsKitImplementationTypes = EventsKitFactoryUtils.GetImplementationTypes<T>(EventsKitFactoryOptions.EmitEventingServerEvents, null, EventsKitType.InProc);
							InProcessEventsKitFactory.s_eventKitTypesCache.Add(name, generatedEventsKitImplementationTypes);
						}
					}
					obj = Activator.CreateInstance(generatedEventsKitImplementationTypes.EventsKitType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[] { this.m_eventsPublisher }, null);
					this.m_eventKitsCache.Add(name, obj);
				}
				t = obj as T;
			}
			return t;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0005E6A8 File Offset: 0x0005C8A8
		public T CreateEventsKit<T>(ActivityType activityType) where T : class
		{
			return this.CreateEventsKit<T>();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0005E6B0 File Offset: 0x0005C8B0
		public T CreateEventsKit<T>(string performanceCountersInstanceName, PerformanceCounterPrefixSetting performanceCounterPrefixSetting) where T : class
		{
			return this.CreateEventsKit<T>();
		}

		// Token: 0x040008CA RID: 2250
		[BlockServiceDependency]
		private IEventsPublisher m_eventsPublisher;

		// Token: 0x040008CB RID: 2251
		private readonly Dictionary<string, object> m_eventKitsCache = new Dictionary<string, object>();

		// Token: 0x040008CC RID: 2252
		private static readonly Dictionary<string, GeneratedEventsKitImplementationTypes> s_eventKitTypesCache = new Dictionary<string, GeneratedEventsKitImplementationTypes>();
	}
}
