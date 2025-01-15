using System;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200014A RID: 330
	internal class SmiEventSink_DeferedProcessing : SmiEventSink_Default
	{
		// Token: 0x060019B0 RID: 6576 RVA: 0x0006B6BB File Offset: 0x000698BB
		internal SmiEventSink_DeferedProcessing(SmiEventSink parent)
			: base(parent)
		{
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected override void DispatchMessages(bool ignoreNonFatalMessages)
		{
		}
	}
}
