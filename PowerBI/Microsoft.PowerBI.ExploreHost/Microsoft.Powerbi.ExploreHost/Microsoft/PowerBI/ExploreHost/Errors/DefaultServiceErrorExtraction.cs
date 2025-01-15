using System;
using System.Globalization;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ExploreHost.Errors
{
	// Token: 0x02000089 RID: 137
	internal class DefaultServiceErrorExtraction : IServiceErrorExtractor
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public bool CanExtractFromException(Exception e)
		{
			return true;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public bool TryExtractServiceError(Exception ex, ServiceErrorExtractor extractor, out ServiceError error)
		{
			string text = string.Format(CultureInfo.CurrentCulture, "An exception of type {0} occured.", ex.GetType().ToString());
			error = new ServiceError
			{
				Message = text,
				StackTrace = ex.ToString().ToOriginalString(),
				PowerBIExceptionCulprit = ErrorSource.PowerBI.ToString()
			};
			return true;
		}
	}
}
