using System;
using System.Diagnostics;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000540 RID: 1344
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class WindowsEventLogAttribute : WindowsEventLogBaseAttribute
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x00092815 File Offset: 0x00090A15
		// (set) Token: 0x060028EB RID: 10475 RVA: 0x0009281D File Offset: 0x00090A1D
		public EventLogEntryType Severity { get; private set; }

		// Token: 0x060028EC RID: 10476 RVA: 0x00092826 File Offset: 0x00090A26
		public WindowsEventLogAttribute(EventLogEntryType windowsEventLogEntryType, int windowsEventLogId)
			: base(windowsEventLogId)
		{
			this.Severity = windowsEventLogEntryType;
		}
	}
}
