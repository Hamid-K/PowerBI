using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002E3 RID: 739
	internal class CancelableRequestWrapper
	{
		// Token: 0x06001A49 RID: 6729 RVA: 0x00069448 File Offset: 0x00067648
		public void ExecuteCancelableProgressiveRequest(CancelablePhaseBase cancelableStep)
		{
			try
			{
				cancelableStep.ExecuteWrapper();
			}
			catch (JobCanceledException)
			{
			}
		}
	}
}
