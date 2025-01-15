using System;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.Query.Contracts;
using MsolapWrapper;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x0200000C RID: 12
	internal sealed class DataExtensionErrorUtils
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002620 File Offset: 0x00000820
		internal static DataExtensionException CreateDataExtensionException(MsolapWrapperException ex, IPrivateInformationService piiService, string message, params object[] messageArgs)
		{
			string text = DataExtensionErrorUtils.MapErrorCode(ex.ErrorCode, "DataExtensionError");
			return DataExtensionErrorUtils.CreateDataExtensionException(ex, text, piiService, message, messageArgs);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002648 File Offset: 0x00000848
		internal static DataExtensionException CreateDataExtensionException(MsolapWrapperException ex, string engineErrorCode, IPrivateInformationService piiService, string message, params object[] messageArgs)
		{
			if (messageArgs != null)
			{
				message = Utilities.FormatInvariant(message, messageArgs);
			}
			string text = piiService.MarkAsPrivate(piiService.RemovePrivateAndInternalMarkup(ex.DataErrorInformation.Description));
			return new DataExtensionException(engineErrorCode, message, ex.DataErrorInformation.ErrorCode, text, ex.DataErrorInformation.GenericMessage, ex.DataErrorInformation.Hresult, ex.DataErrorInformation.HasUserSafeDescription, ex.DataErrorInformation.OnPremErrorCode, ex, DataExtensionErrorUtils.MapErrorSource(ex.ErrorSource), ex.DataErrorInformation.TypeOrigin.ToString("F"));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026E0 File Offset: 0x000008E0
		internal static DataExtensionException CreateDataExtensionException(string errorCode, string genericMessage, ErrorSource errorSource, Exception innerException)
		{
			return new DataExtensionException(errorCode, genericMessage, 0U, null, null, 0, false, null, innerException, errorSource, "MsolapDataExtension");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002704 File Offset: 0x00000904
		internal static string MapErrorCode(WrapperErrorCodes wrapperErrorCode, string defaultEngineCode)
		{
			switch (wrapperErrorCode)
			{
			case WrapperErrorCodes.QueryMemoryLimitExceeded:
				return "rsQueryMemoryLimitExceeded";
			case WrapperErrorCodes.QueryTimeoutExceeded:
				return "rsQueryTimeoutExceeded";
			case WrapperErrorCodes.OnPremiseServiceException:
				return "OnPremiseServiceException";
			case WrapperErrorCodes.OperationCancelled:
				return "OperationCanceled";
			case WrapperErrorCodes.ExclusivePercentileOutOfRange:
				return "ExclusivePercentileOutOfRange";
			case WrapperErrorCodes.QueryUserError:
				return "QueryUserError";
			case WrapperErrorCodes.QuerySystemError:
				return "QuerySystemError";
			case WrapperErrorCodes.QueryExternalError:
				return "QueryExternalError";
			case WrapperErrorCodes.InvalidDateTimeValue:
				return "InvalidDateTimeValue";
			case WrapperErrorCodes.ProxyModelChainLimitExceeded:
				return "ProxyModelChainLimitExceeded";
			case WrapperErrorCodes.InsecureLibraryLoadingPatchMissing:
				return "InsecureLibraryLoadingPatchMissing";
			default:
				return defaultEngineCode;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000278A File Offset: 0x0000098A
		internal static string MapConnectionErrorCode(WrapperErrorCodes wrapperErrorCode)
		{
			if (wrapperErrorCode - WrapperErrorCodes.QueryUserError <= 1)
			{
				return "OpenConnectionError";
			}
			return DataExtensionErrorUtils.MapErrorCode(wrapperErrorCode, "OpenConnectionError");
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027A4 File Offset: 0x000009A4
		internal static ErrorSource MapErrorSource(WrapperErrorSource wrapperErrorSource)
		{
			switch (wrapperErrorSource)
			{
			case WrapperErrorSource.Unknown:
				return ErrorSource.Unknown;
			case WrapperErrorSource.PowerBI:
				return ErrorSource.PowerBI;
			case WrapperErrorSource.External:
				return ErrorSource.External;
			case WrapperErrorSource.User:
				return ErrorSource.User;
			default:
				QueryContract.RetailFail("Unknown WrapperErrorSource kind: " + wrapperErrorSource.ToString());
				throw new InvalidOperationException("Unknown WrapperErrorSource kind: " + wrapperErrorSource.ToString());
			}
		}
	}
}
