using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001EC RID: 492
	[DomName("MessageEvent")]
	public class MessageEvent : Event
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x00047068 File Offset: 0x00045268
		public MessageEvent()
		{
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00047634 File Offset: 0x00045834
		[DomConstructor]
		[DomInitDict(1, true)]
		public MessageEvent(string type, bool bubbles = false, bool cancelable = false, object data = null, string origin = null, string lastEventId = null, IWindow source = null, params IMessagePort[] ports)
		{
			this.Init(type, bubbles, cancelable, data, origin ?? string.Empty, lastEventId ?? string.Empty, source, ports);
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0004766C File Offset: 0x0004586C
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x00047674 File Offset: 0x00045874
		[DomName("data")]
		public object Data { get; private set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0004767D File Offset: 0x0004587D
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x00047685 File Offset: 0x00045885
		[DomName("origin")]
		public string Origin { get; private set; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0004768E File Offset: 0x0004588E
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x00047696 File Offset: 0x00045896
		[DomName("lastEventId")]
		public string LastEventId { get; private set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x0004769F File Offset: 0x0004589F
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x000476A7 File Offset: 0x000458A7
		[DomName("source")]
		public IWindow Source { get; private set; }

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x000476B0 File Offset: 0x000458B0
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x000476B8 File Offset: 0x000458B8
		[DomName("ports")]
		public IMessagePort[] Ports { get; private set; }

		// Token: 0x06001045 RID: 4165 RVA: 0x000476C1 File Offset: 0x000458C1
		[DomName("initMessageEvent")]
		public void Init(string type, bool bubbles, bool cancelable, object data, string origin, string lastEventId, IWindow source, params IMessagePort[] ports)
		{
			base.Init(type, bubbles, cancelable);
			this.Data = data;
			this.Origin = origin;
			this.LastEventId = lastEventId;
			this.Source = source;
			this.Ports = ports;
		}
	}
}
