using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200009E RID: 158
	internal sealed class AspNetRequestInspector : IRSRequestInspector
	{
		// Token: 0x0600064E RID: 1614 RVA: 0x0001A428 File Offset: 0x00018628
		internal AspNetRequestInspector()
		{
			RSTrace.CatalogTrace.Assert(Globals.CurrentApplication != RunningApplication.ReportServerWebApp, "AspNetRequestInspector cannot run in webapp");
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001A44A File Offset: 0x0001864A
		public bool IsAnonymous()
		{
			return Globals.IsAnonymous;
		}
	}
}
