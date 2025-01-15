using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200035E RID: 862
	[BlockServiceProvider(typeof(IEventsKitFactory), BlockServiceProviderIdentity.Default, PublishWhen = BlockServicePublish.Default)]
	public class VoidEventsKitFactory : Block, IEventsKitFactory
	{
		// Token: 0x06001978 RID: 6520 RVA: 0x00010777 File Offset: 0x0000E977
		public VoidEventsKitFactory(string name)
			: base(name)
		{
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0005E6C4 File Offset: 0x0005C8C4
		public VoidEventsKitFactory()
			: this(typeof(VoidEventsKitFactory).Name)
		{
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0005E6DB File Offset: 0x0005C8DB
		public T CreateEventsKit<T>() where T : class
		{
			return this.CreateEventsKit<T>(null, "MockPC");
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0005E6E9 File Offset: 0x0005C8E9
		public T CreateEventsKit<T>(ActivityType activityType) where T : class
		{
			return this.CreateEventsKit<T>(activityType, "MockPC");
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0005E6F7 File Offset: 0x0005C8F7
		public T CreateEventsKit<T>(string performanceCountersInstanceName, PerformanceCounterPrefixSetting performanceCounterPrefixSetting) where T : class
		{
			return this.CreateEventsKit<T>(null, performanceCountersInstanceName);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0005E701 File Offset: 0x0005C901
		public static T GetInstance<T>() where T : class
		{
			return new VoidEventsKitFactory().CreateEventsKit<T>();
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0005E710 File Offset: 0x0005C910
		private T CreateEventsKit<T>(ActivityType activityType, string performanceCountersInstanceName) where T : class
		{
			string text = "{0}.{1}".FormatWithInvariantCulture(new object[]
			{
				typeof(T),
				performanceCountersInstanceName
			});
			Dictionary<string, EventsKitBase> dictionary = VoidEventsKitFactory.s_eventKitsCache;
			T t;
			lock (dictionary)
			{
				EventsKitBase eventsKitBase = null;
				if (!VoidEventsKitFactory.s_eventKitsCache.TryGetValue(text, out eventsKitBase))
				{
					eventsKitBase = EventsKitFactoryUtils.CreateGeneratedEventsKitInstances<T>(EventsKitFactoryOptions.None, ElementId.None, activityType, null, performanceCountersInstanceName, EventsKitType.Mock, null, null, null).EventsKitInstance as EventsKitBase;
					VoidEventsKitFactory.s_eventKitsCache.Add(text, eventsKitBase);
				}
				t = eventsKitBase as T;
			}
			return t;
		}

		// Token: 0x040008CD RID: 2253
		private static readonly Dictionary<string, EventsKitBase> s_eventKitsCache = new Dictionary<string, EventsKitBase>();
	}
}
