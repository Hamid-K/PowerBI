using System;
using System.Collections.Generic;
using AngleSharp.Dom.Events;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000049 RID: 73
	internal sealed class EventFactory : IEventFactory
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00009968 File Offset: 0x00007B68
		public EventFactory()
		{
			Dictionary<string, EventFactory.Creator> dictionary = new Dictionary<string, EventFactory.Creator>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add("event", () => new Event());
			dictionary.Add("uievent", () => new UiEvent());
			dictionary.Add("focusevent", () => new FocusEvent());
			dictionary.Add("keyboardevent", () => new KeyboardEvent());
			dictionary.Add("mouseevent", () => new MouseEvent());
			dictionary.Add("wheelevent", () => new WheelEvent());
			dictionary.Add("customevent", () => new CustomEvent());
			this.creators = dictionary;
			base..ctor();
			this.AddEventAlias("events", "event");
			this.AddEventAlias("htmlevents", "event");
			this.AddEventAlias("uievents", "uievent");
			this.AddEventAlias("keyevents", "keyboardevent");
			this.AddEventAlias("mouseevents", "mouseevent");
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00009B01 File Offset: 0x00007D01
		private void AddEventAlias(string aliasName, string aliasFor)
		{
			this.creators.Add(aliasName, this.creators[aliasFor]);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00009B1C File Offset: 0x00007D1C
		public Event Create(string name)
		{
			EventFactory.Creator creator = null;
			if (name != null && this.creators.TryGetValue(name, out creator))
			{
				return creator();
			}
			return null;
		}

		// Token: 0x040001C7 RID: 455
		private readonly Dictionary<string, EventFactory.Creator> creators;

		// Token: 0x02000428 RID: 1064
		// (Invoke) Token: 0x060021FE RID: 8702
		private delegate Event Creator();
	}
}
