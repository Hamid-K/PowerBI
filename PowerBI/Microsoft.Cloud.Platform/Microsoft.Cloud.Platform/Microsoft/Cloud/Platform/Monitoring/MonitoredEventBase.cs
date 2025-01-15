using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000083 RID: 131
	internal abstract class MonitoredEventBase : IEventIdentifier, IWindowsEventLogId, IPerElementActivityType, IMonitoredEventHandlerVisitor, ITimestamp
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000E471 File Offset: 0x0000C671
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000E479 File Offset: 0x0000C679
		private protected WireEventBase PublishedEvent { protected get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000E482 File Offset: 0x0000C682
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000E48A File Offset: 0x0000C68A
		private protected string FriendlyName { protected get; private set; }

		// Token: 0x060003BD RID: 957 RVA: 0x0000E493 File Offset: 0x0000C693
		protected MonitoredEventBase(MonitoredEventBase otherEvent)
			: this(otherEvent.PublishedEvent, otherEvent.WindowsEventLogId, otherEvent.FriendlyName)
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E4AD File Offset: 0x0000C6AD
		protected MonitoredEventBase([NotNull] WireEventBase publishedEvent, int windowsEventLogId, string friendlyName)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<WireEventBase>(publishedEvent, "publishedEvent");
			this.EventId = publishedEvent.Id;
			this.Timestamp = publishedEvent.UtcTimestamp;
			this.WindowsEventLogId = windowsEventLogId;
			this.PublishedEvent = publishedEvent;
			this.FriendlyName = friendlyName;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000E4ED File Offset: 0x0000C6ED
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000E4F5 File Offset: 0x0000C6F5
		public DateTime Timestamp { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000E4FE File Offset: 0x0000C6FE
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000E506 File Offset: 0x0000C706
		public int WindowsEventLogId { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000E50F File Offset: 0x0000C70F
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000E517 File Offset: 0x0000C717
		public EventIdentifier EventId { get; private set; }

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E520 File Offset: 0x0000C720
		public override string ToString()
		{
			return WindowsEventLogMessageFormatter.Format(this.PublishedEvent, this.FriendlyName);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000E533 File Offset: 0x0000C733
		public PerElementActivityType PerElementActivityType
		{
			get
			{
				return new PerElementActivityType(this.EventId.ElementId, this.PublishedEvent.Activity.ActivityType);
			}
		}

		// Token: 0x060003C7 RID: 967
		public abstract void Visit(IMonitoredEventHandler eventHandler);
	}
}
