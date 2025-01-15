using System;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.ExploreServiceCommon;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ExploreHost.Errors
{
	// Token: 0x0200008E RID: 142
	public class ScriptHandlerErrorExtraction : IServiceErrorExtractor
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000BB44 File Offset: 0x00009D44
		public bool CanExtractFromException(Exception ex)
		{
			return ex is ScriptHandlerException;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000BB50 File Offset: 0x00009D50
		public bool TryExtractServiceError(Exception ex, ServiceErrorExtractor extractor, out ServiceError error)
		{
			ScriptHandlerException ex2 = ex as ScriptHandlerException;
			if (ex2 == null)
			{
				error = new ServiceError();
				return false;
			}
			error = new ServiceError
			{
				PowerBIErrorCode = ex2.ErrorCode,
				Message = ex2.Message,
				StackTrace = ex2.ToString().ToOriginalString()
			};
			return true;
		}
	}
}
