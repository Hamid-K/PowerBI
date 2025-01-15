using System;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000026 RID: 38
	public interface IMetadataAccess
	{
		// Token: 0x060000A1 RID: 161
		IEventMetadata GetEventMetadata(IntPtr hEvent);

		// Token: 0x060000A2 RID: 162
		IMapMetadata GetMapMetadata(IntPtr hMap);

		// Token: 0x060000A3 RID: 163
		IActionMetadata GetActionMetadata(IntPtr hAction);
	}
}
