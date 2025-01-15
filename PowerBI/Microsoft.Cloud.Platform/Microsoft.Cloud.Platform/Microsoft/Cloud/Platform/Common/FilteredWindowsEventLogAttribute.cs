using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000543 RID: 1347
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class FilteredWindowsEventLogAttribute : WindowsEventLogBaseAttribute
	{
		// Token: 0x060028EF RID: 10479 RVA: 0x00092836 File Offset: 0x00090A36
		public FilteredWindowsEventLogAttribute(int windowsEventLogId)
			: base(windowsEventLogId)
		{
		}
	}
}
