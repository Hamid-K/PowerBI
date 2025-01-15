using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200008D RID: 141
	internal class AttributeCache<TAttribute> where TAttribute : Attribute
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public AttributeCache()
		{
			this.m_attributeCache = new Dictionary<Guid, TAttribute>();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		public bool TryGetAttribute(WireEventBase publishedEvent, IEventsKitExplorer explorer, out TAttribute attribute)
		{
			Guid eventId = publishedEvent.Id.EventId;
			if (!this.m_attributeCache.TryGetValue(eventId, out attribute))
			{
				attribute = explorer.GetAttributes<TAttribute>(new EventsKitIdentifiers(publishedEvent.Id.EventId)).FirstOrDefault<TAttribute>();
				this.m_attributeCache.Add(eventId, attribute);
			}
			return attribute != null;
		}

		// Token: 0x0400015A RID: 346
		private readonly Dictionary<Guid, TAttribute> m_attributeCache;
	}
}
