using System;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200006F RID: 111
	internal interface IRenderStreamFinishNotification : IRenderStream
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000459 RID: 1113
		// (remove) Token: 0x0600045A RID: 1114
		event EventHandler StreamFinished;
	}
}
