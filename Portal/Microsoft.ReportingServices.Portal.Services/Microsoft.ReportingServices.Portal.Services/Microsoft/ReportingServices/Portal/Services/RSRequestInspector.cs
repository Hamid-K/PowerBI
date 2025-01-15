using System;
using Microsoft.ReportingServices.Library;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x0200001C RID: 28
	internal sealed class RSRequestInspector : IRSRequestInspector
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000C404 File Offset: 0x0000A604
		public bool IsAnonymous()
		{
			return false;
		}

		// Token: 0x0400007B RID: 123
		internal static IRSRequestInspector Instance = new RSRequestInspector();
	}
}
