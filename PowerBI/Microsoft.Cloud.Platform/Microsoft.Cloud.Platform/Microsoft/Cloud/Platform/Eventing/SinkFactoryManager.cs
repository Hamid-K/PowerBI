using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000390 RID: 912
	internal class SinkFactoryManager
	{
		// Token: 0x06001C27 RID: 7207 RVA: 0x0006B423 File Offset: 0x00069623
		internal SinkFactoryManager()
		{
			this.m_factories = new Dictionary<string, SinkFactoryManager.FactoryEntry>();
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0006B438 File Offset: 0x00069638
		internal void Register(ISinkFactory factory)
		{
			Dictionary<string, SinkFactoryManager.FactoryEntry> factories = this.m_factories;
			lock (factories)
			{
				string fullName = factory.GetType().FullName;
				if (this.m_factories.ContainsKey(fullName))
				{
					throw new SinkFactoryAlreadyExitsException(factory);
				}
				this.m_factories.Add(fullName, new SinkFactoryManager.FactoryEntry(factory));
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Info, "SinkFactoryManager: Factory {0} registered", new object[] { fullName });
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0006B4C0 File Offset: 0x000696C0
		internal void Unregister(ISinkFactory factory)
		{
			Dictionary<string, SinkFactoryManager.FactoryEntry> factories = this.m_factories;
			lock (factories)
			{
				string fullName = factory.GetType().FullName;
				SinkFactoryManager.FactoryEntry factoryEntry = null;
				if (!this.m_factories.TryGetValue(fullName, out factoryEntry))
				{
					throw new SinkFactoryNotFoundException(factory);
				}
				if (factoryEntry.CreatedSinkCount > 0)
				{
					throw new SinkFactoryStillInUseException(factoryEntry.Factory, factoryEntry.CreatedSinkCount);
				}
				this.m_factories.Remove(fullName);
				TraceSourceBase<EventingTrace>.Tracer.Trace(TraceVerbosity.Info, "SinkFactoryManager: Factory {0} unregistered", new object[] { fullName });
			}
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0006B564 File Offset: 0x00069764
		internal ISink Create(SinkIdentifier sid)
		{
			SinkFactoryManager.FactoryEntry factoryEntry = null;
			Dictionary<string, SinkFactoryManager.FactoryEntry> factories = this.m_factories;
			lock (factories)
			{
				if (this.m_factories.TryGetValue(sid.CreatorType, out factoryEntry))
				{
					return factoryEntry.Create(sid);
				}
			}
			throw new SinkFactoryNotFoundException();
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0006B5C8 File Offset: 0x000697C8
		internal void Destroy(ISink sink)
		{
			SinkFactoryManager.FactoryEntry factoryEntry = null;
			Dictionary<string, SinkFactoryManager.FactoryEntry> factories = this.m_factories;
			lock (factories)
			{
				if (this.m_factories.TryGetValue(sink.Id.CreatorType, out factoryEntry))
				{
					factoryEntry.Destroy(sink);
					return;
				}
			}
			throw new SinkFactoryNotFoundException();
		}

		// Token: 0x0400097F RID: 2431
		private Dictionary<string, SinkFactoryManager.FactoryEntry> m_factories;

		// Token: 0x020007BD RID: 1981
		private class FactoryEntry
		{
			// Token: 0x0600316D RID: 12653 RVA: 0x000A7DC6 File Offset: 0x000A5FC6
			internal FactoryEntry(ISinkFactory factory)
			{
				this.m_factory = factory;
				this.m_createdSinkCount = 0;
			}

			// Token: 0x0600316E RID: 12654 RVA: 0x000A7DDC File Offset: 0x000A5FDC
			internal ISink Create(SinkIdentifier sid)
			{
				ISink sink = this.m_factory.Create(sid);
				if (!sid.SinkType.Equals(sink.GetType().FullName, StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidSinkParameterException(sid, "Sink type is not fully qualified name");
				}
				this.m_createdSinkCount++;
				return sink;
			}

			// Token: 0x0600316F RID: 12655 RVA: 0x000A7E2A File Offset: 0x000A602A
			internal void Destroy(ISink sink)
			{
				this.m_createdSinkCount--;
				this.m_factory.Destroy(sink);
			}

			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x06003170 RID: 12656 RVA: 0x000A7E46 File Offset: 0x000A6046
			internal int CreatedSinkCount
			{
				get
				{
					return this.m_createdSinkCount;
				}
			}

			// Token: 0x1700076D RID: 1901
			// (get) Token: 0x06003171 RID: 12657 RVA: 0x000A7E4E File Offset: 0x000A604E
			internal ISinkFactory Factory
			{
				get
				{
					return this.m_factory;
				}
			}

			// Token: 0x040016D1 RID: 5841
			private ISinkFactory m_factory;

			// Token: 0x040016D2 RID: 5842
			private int m_createdSinkCount;
		}
	}
}
