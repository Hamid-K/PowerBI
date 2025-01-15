using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ExploreHost.Errors
{
	// Token: 0x0200008D RID: 141
	public sealed class PowerBIExploreExceptionExtractor : IServiceErrorExtractor
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0000BA33 File Offset: 0x00009C33
		public bool CanExtractFromException(Exception ex)
		{
			return ex is PowerBIExploreException;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000BA40 File Offset: 0x00009C40
		public bool TryExtractServiceError(Exception ex, ServiceErrorExtractor extractor, out ServiceError error)
		{
			PowerBIExploreException ex2 = ex as PowerBIExploreException;
			error = new ServiceError();
			if (ex2 == null)
			{
				return false;
			}
			error.Message = string.Format(CultureInfo.CurrentCulture, "{0}: {1}", ex2.ErrorCode, ex2.Message);
			error.StackTrace = ex2.StackTrace;
			error.PowerBIErrorCode = ex2.ErrorCode;
			error.PowerBIExceptionCulprit = ex2.ErrorSource.ToString();
			error.StatusCode = ex2.ErrorStatusCode;
			if (ex2.ErrorDetails != null)
			{
				List<ErrorDetail> list = new List<ErrorDetail>();
				foreach (KeyValuePair<string, string> keyValuePair in ex2.ErrorDetails)
				{
					list.Add(new ErrorDetail(keyValuePair.Key, new ErrorDetailValue(ErrorResourceType.EmbeddedString, keyValuePair.Value.ToOriginalString())));
				}
				error.ErrorDetails = list;
			}
			return true;
		}
	}
}
