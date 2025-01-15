using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200053F RID: 1343
	public abstract class WindowsEventLogBaseAttribute : Attribute
	{
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000927E4 File Offset: 0x000909E4
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x000927EC File Offset: 0x000909EC
		public int WindowsEventLogId { get; set; }

		// Token: 0x060028E9 RID: 10473 RVA: 0x000927F5 File Offset: 0x000909F5
		protected internal WindowsEventLogBaseAttribute(int windowsEventLogId)
		{
			ExtendedDiagnostics.EnsureArgumentIsBetween(windowsEventLogId, 1, 65535, "id");
			this.WindowsEventLogId = windowsEventLogId;
		}
	}
}
