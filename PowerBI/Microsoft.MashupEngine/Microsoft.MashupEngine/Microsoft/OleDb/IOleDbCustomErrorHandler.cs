using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001EA8 RID: 7848
	public interface IOleDbCustomErrorHandler
	{
		// Token: 0x17002F65 RID: 12133
		// (get) Token: 0x0600C20E RID: 49678
		Guid InterfaceID { get; }

		// Token: 0x0600C20F RID: 49679
		OleDbError GetError(string source, string message, object customObject);
	}
}
