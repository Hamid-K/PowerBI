using System;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000084 RID: 132
	internal abstract class MonitoredErrorEventBase : MonitoredEventBase, IMonitoredError, IContainsPrivateInformation
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0000E555 File Offset: 0x0000C755
		protected MonitoredErrorEventBase(WireEventBase publishedEvent, int windowsEventLogId, IMonitoredError monitoredError, string friendlyName)
			: base(publishedEvent, windowsEventLogId, friendlyName)
		{
			this.m_monitoredError = monitoredError;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsFatal()
		{
			return false;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsBenign()
		{
			return false;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsPermanent()
		{
			return false;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000E56B File Offset: 0x0000C76B
		public string ErrorShortName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000E578 File Offset: 0x0000C778
		public ErrorCorrelationId ErrorCorrelationId
		{
			get
			{
				return this.m_monitoredError.ErrorCorrelationId;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000E585 File Offset: 0x0000C785
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000E592 File Offset: 0x0000C792
		public MonitoringScopeId MonitoringScope
		{
			get
			{
				return this.m_monitoredError.MonitoringScope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000E599 File Offset: 0x0000C799
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000E5A1 File Offset: 0x0000C7A1
		public long ErrorEventId { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000E5AA File Offset: 0x0000C7AA
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000E5B2 File Offset: 0x0000C7B2
		public string ErrorEventName { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000E5BB File Offset: 0x0000C7BB
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000E5C3 File Offset: 0x0000C7C3
		public long ErrorEventsKitId { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public string ErrorEventsKitName { get; set; }

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E5DD File Offset: 0x0000C7DD
		public override string ToString()
		{
			return WindowsEventLogMessageFormatter.Format(base.PublishedEvent, base.FriendlyName, this.m_monitoredError.ToString(), this.ErrorCorrelationId);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E601 File Offset: 0x0000C801
		public string ToPrivateString()
		{
			return this.ToOriginalString();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E601 File Offset: 0x0000C801
		public string ToInternalString()
		{
			return this.ToOriginalString();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E609 File Offset: 0x0000C809
		public string ToOriginalString()
		{
			return this.ToString();
		}

		// Token: 0x0400014C RID: 332
		private readonly IMonitoredError m_monitoredError;
	}
}
