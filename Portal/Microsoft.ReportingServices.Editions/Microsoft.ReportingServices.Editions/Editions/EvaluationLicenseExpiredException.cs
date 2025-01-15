using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000006 RID: 6
	public class EvaluationLicenseExpiredException : Exception
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000027E0 File Offset: 0x000009E0
		public EvaluationLicenseExpiredException()
			: base("The evaluation license for this product has expired.")
		{
		}
	}
}
