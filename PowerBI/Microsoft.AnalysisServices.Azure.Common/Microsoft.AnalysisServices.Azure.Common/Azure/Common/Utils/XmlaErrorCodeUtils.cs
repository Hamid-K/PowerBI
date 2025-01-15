using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ASAzure.ASCommon.Xmla;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.PowerBI.ServiceContracts;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x0200014A RID: 330
	public static class XmlaErrorCodeUtils
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x00047334 File Offset: 0x00045534
		public static XmlaErrorCodeUtils.XmlaErrorType GetExpectedXmlaErrorType(Exception ex)
		{
			if (ex is XmlaFaultException)
			{
				XmlaFaultException ex2 = ex as XmlaFaultException;
				bool flag = false;
				foreach (XmlaError xmlaError in XmlaFaultUtils.GetErrors(ex2))
				{
					XmlaErrorCodeUtils.XmlaErrorType xmlaErrorType;
					if ((xmlaErrorType = XmlaErrorCodeUtils.GetXmlaErrorType(xmlaError.ErrorCode)) != XmlaErrorCodeUtils.XmlaErrorType.None)
					{
						return xmlaErrorType;
					}
					if (xmlaError.ErrorType == 1)
					{
						flag = true;
					}
				}
				if (flag)
				{
					return XmlaErrorCodeUtils.XmlaErrorType.InvalidUserdata;
				}
				return XmlaErrorCodeUtils.XmlaErrorType.None;
			}
			return XmlaErrorCodeUtils.XmlaErrorType.None;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000473B4 File Offset: 0x000455B4
		public static XmlaErrorCodeUtils.XmlaErrorType GetExpectedXmlaErrorTypeFromOperationException(OperationException ex)
		{
			XmlaErrorCodeUtils.XmlaErrorType xmlaErrorType = XmlaErrorCodeUtils.XmlaErrorType.None;
			bool flag = false;
			foreach (XmlaError xmlaError in ex.GetErrorsFromOperationException())
			{
				XmlaErrorCodeUtils.XmlaErrorType xmlaErrorType2;
				if ((xmlaErrorType2 = XmlaErrorCodeUtils.GetXmlaErrorType(xmlaError.ErrorCode)) != XmlaErrorCodeUtils.XmlaErrorType.None)
				{
					if (xmlaErrorType2 == XmlaErrorCodeUtils.XmlaErrorType.Throttled)
					{
						return xmlaErrorType2;
					}
					if (xmlaErrorType == XmlaErrorCodeUtils.XmlaErrorType.None)
					{
						xmlaErrorType = xmlaErrorType2;
					}
				}
				if (xmlaError.ErrorType == 1)
				{
					flag = true;
				}
			}
			if (xmlaErrorType == XmlaErrorCodeUtils.XmlaErrorType.None && flag)
			{
				return XmlaErrorCodeUtils.XmlaErrorType.InvalidUserdata;
			}
			return xmlaErrorType;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0004743C File Offset: 0x0004563C
		public static IEnumerable<XmlaError> GetErrorsFromOperationException(this OperationException operationException)
		{
			List<XmlaError> list = new List<XmlaError>();
			if (operationException != null)
			{
				XmlaResultCollection results = operationException.Results;
				bool? flag = ((results != null) ? new bool?(results.ContainsErrors) : null);
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					for (int i = 0; i < operationException.Results.Count; i++)
					{
						XmlaResult xmlaResult = operationException.Results[i];
						int num = 0;
						for (;;)
						{
							int num2 = num;
							int? num3;
							if (xmlaResult == null)
							{
								num3 = null;
							}
							else
							{
								XmlaMessageCollection messages = xmlaResult.Messages;
								num3 = ((messages != null) ? new int?(messages.Count) : null);
							}
							int? num4 = num3;
							if (!((num2 < num4.GetValueOrDefault()) & (num4 != null)))
							{
								break;
							}
							XmlaError xmlaError = xmlaResult.Messages[num] as XmlaError;
							if (xmlaError != null)
							{
								XmlaError xmlaError2 = new XmlaError((uint)xmlaError.ErrorCode, xmlaError.Description, xmlaError.HelpFile, xmlaError.CallStack, xmlaError.ErrorType, false);
								list.Add(xmlaError2);
							}
							num++;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00047560 File Offset: 0x00045760
		public static Exception TranslateException(Exception ex, string databaseId, bool isTabularModel, int maxParallel, bool isOOLBindingProcessing)
		{
			return XmlaErrorCodeUtils.TranslateException(XmlaErrorCodeUtils.GetExpectedXmlaErrorType(ex), ex, databaseId, isTabularModel, maxParallel, isOOLBindingProcessing);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00047573 File Offset: 0x00045773
		public static Exception TranslateOperationException(OperationException ex, string databaseId, bool isTabularModel, int maxParallel, bool isOOLBindingProcessing)
		{
			return XmlaErrorCodeUtils.TranslateException(XmlaErrorCodeUtils.GetExpectedXmlaErrorTypeFromOperationException(ex), ex, databaseId, isTabularModel, maxParallel, isOOLBindingProcessing);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00047588 File Offset: 0x00045788
		private static Exception TranslateException(XmlaErrorCodeUtils.XmlaErrorType xmlaErrorType, Exception ex, string databaseId, bool isTabularModel, int maxParallel, bool isOOLBindingProcessing)
		{
			switch (xmlaErrorType)
			{
			case XmlaErrorCodeUtils.XmlaErrorType.ManagedProvider:
				return new ProcessDatabaseFailedManagerProviderException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), "Processing for {0} failed due to managed provider error {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			case XmlaErrorCodeUtils.XmlaErrorType.SessionCancellation:
				return new ProcessDatabaseFailedSessionCancelledException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), "Processing for {0} failed due to session cancellation {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			case XmlaErrorCodeUtils.XmlaErrorType.DatatypeConversion:
				return new ProcessDatabaseFailedDatatypeConversionException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), "Processing for {0} failed due to data type conversion error {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			case XmlaErrorCodeUtils.XmlaErrorType.InvalidMetadata:
				return new ProcessDatabaseFailedInvalidMetadataException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), "Processing for {0} failed due to invalid metadata error {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			case XmlaErrorCodeUtils.XmlaErrorType.InvalidUserdata:
				return new ProcessDatabaseFailedInvalidUserdataException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), "Processing for {0} database failed due to incorrect usage error: \"{1}\"".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			case XmlaErrorCodeUtils.XmlaErrorType.OnPremiseError:
				return new ProcessDatabaseFailedOnPremiseErrorException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), "Processing for {0} failed due to on-premise model error {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			case XmlaErrorCodeUtils.XmlaErrorType.ResourceGovernanceError:
				return new ProcessDatabaseFailedResourceGovernanceException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), maxParallel.ToString(CultureInfo.InvariantCulture), "Processing for {0} failed due to resource governance error {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			case XmlaErrorCodeUtils.XmlaErrorType.DatabaseDisabled:
				return new ProcessDatabaseFailedDatabaseDisabledException(databaseId, "Processing database {0} failed because it is disabled for scoped connections. error {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			case XmlaErrorCodeUtils.XmlaErrorType.Throttled:
				return new ProcessDatabaseThrottledInPremiumCapacityException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), "Processing database {0} failed because it is throttled. error {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			default:
				return new ProcessDatabaseFailedException(databaseId, isOOLBindingProcessing.ToString(), isTabularModel.ToString(), "Processing for {0} failed due to {1}".FormatWithInvariantCulture(new object[] { databaseId, ex.Message }), ex);
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000477C4 File Offset: 0x000459C4
		private static XmlaErrorCodeUtils.XmlaErrorType GetXmlaErrorType(uint errorCode)
		{
			if (XmlaErrorCodeUtils.ManagedProviderErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.ManagedProvider;
			}
			if (XmlaErrorCodeUtils.SessionCancelledErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.SessionCancellation;
			}
			if (XmlaErrorCodeUtils.DatatypeConversionErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.DatatypeConversion;
			}
			if (XmlaErrorCodeUtils.InvalidMetadataErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.InvalidMetadata;
			}
			if (XmlaErrorCodeUtils.InvalidUserdataErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.InvalidUserdata;
			}
			if (XmlaErrorCodeUtils.OnPremiseErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.OnPremiseError;
			}
			if (XmlaErrorCodeUtils.ResourceGovernanceErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.ResourceGovernanceError;
			}
			if (XmlaErrorCodeUtils.DatabaseDisabledErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.DatabaseDisabled;
			}
			if (XmlaErrorCodeUtils.PBIDedicatedThrottleErrorCodes.ContainsKey(errorCode))
			{
				return XmlaErrorCodeUtils.XmlaErrorType.Throttled;
			}
			return XmlaErrorCodeUtils.XmlaErrorType.None;
		}

		// Token: 0x04000403 RID: 1027
		internal static Dictionary<uint, string> ManagedProviderErrorCodes = new Dictionary<uint, string>
		{
			{ 3239182362U, "MANAGED_READER_UNEXPECTED" },
			{ 3239182363U, "MANAGED_CONN_UNEXPECTED" },
			{ 3239182364U, "MANAGED_CMD_UNEXPECTED" },
			{ 3239182365U, "MANAGED_CMD_PARAMS_NOT_SUPPORT_ILIST" },
			{ 3239116925U, "PROVIDER_NOT_REGISTERED" }
		};

		// Token: 0x04000404 RID: 1028
		internal static Dictionary<uint, string> PBIDedicatedThrottleErrorCodes = new Dictionary<uint, string>
		{
			{ 3238068262U, "PFE_PBIDEDICATED_THROTTLED_TOO_MUCH_CONCURRENCY" },
			{ 3238068263U, "PFE_PBIDEDICATED_THROTTLED_CANNOT_SECURE_MEMORY_GRANT" },
			{ 3238068264U, "PFE_PBIDEDICATED_THROTTLED_OUT_OF_MEMORY" },
			{ 3238068265U, "PFE_PBIDEDICATED_THROTTLED_PANIC" },
			{ 3238068268U, "PFE_PBIDEDICATED_THROTTLED_CANNOT_SECURE_MEMORY_WITHIN_HIGH_LIMIT" },
			{ 3239837725U, "PFE_SERVERBASE_SESSION_CANCELLED_DUE_TO_DATABASE_EVICTION" }
		};

		// Token: 0x04000405 RID: 1029
		internal static Dictionary<uint, string> SessionCancelledErrorCodes = new Dictionary<uint, string>
		{
			{ 3239837706U, "SERVERBASE_OPERATION_CANCELLED_BY_USER" },
			{ 3239837707U, "SERVERBASE_SESSION_CANCELLED_DUE_TO_BALANCE" },
			{ 3239837701U, "SERVERBASE_OPERATION_CANCELLED_DUE_TO_MEMORY_PRESSURE" },
			{ 3239837719U, "SERVERBASE_SESSION_CANCELLED_SERVER_SHUTDOWN" },
			{ 3239837718U, "SERVERBASE_END_SESSION_HEADER" },
			{ 3239837717U, "SERVERBASE_TEMPORARY_SESSION_CANCELLED" }
		};

		// Token: 0x04000406 RID: 1030
		internal static Dictionary<uint, string> DatatypeConversionErrorCodes = new Dictionary<uint, string>
		{
			{ 3241541706U, "DATATYPE_CONVERSION_FAILED" },
			{ 3239116909U, "OLEDB_CONVERT_FAILED" }
		};

		// Token: 0x04000407 RID: 1031
		internal static Dictionary<uint, string> InvalidMetadataErrorCodes = new Dictionary<uint, string>
		{
			{ 3239313410U, "METADATA_NAME_ALREADY_EXISTS" },
			{ 3239313477U, "METADATA_INVALID_MEMBER_PROPERTY_NAME" }
		};

		// Token: 0x04000408 RID: 1032
		internal static Dictionary<uint, string> InvalidUserdataErrorCodes = new Dictionary<uint, string>
		{
			{ 3239182512U, "INVALID_COLUMN_BINDING" },
			{ 3241541694U, "COLUMN_UNIQUE_CONSTRAINT" },
			{ 3238199305U, "STORE_RECORD_BIGGER_THAN_PAGE" },
			{ 3242262687U, "NUMBER_OF_COLUMNS_EXCEEDS_LIMITS" }
		};

		// Token: 0x04000409 RID: 1033
		internal static Dictionary<uint, string> OnPremiseErrorCodes = new Dictionary<uint, string> { { 3242524690U, "ON_PREMISE_POWERBI_SERVICE_EXCEPTION" } };

		// Token: 0x0400040A RID: 1034
		internal static Dictionary<uint, string> ResourceGovernanceErrorCodes = new Dictionary<uint, string> { { 3238199297U, "MEMORY_ALLOCATION_EXCEPTION" } };

		// Token: 0x0400040B RID: 1035
		internal static Dictionary<uint, string> DatabaseDisabledErrorCodes = new Dictionary<uint, string> { { 3239314044U, "DATABASE_DISABLED_EXCEPTION" } };

		// Token: 0x02000169 RID: 361
		public enum XmlaErrorType
		{
			// Token: 0x04000450 RID: 1104
			None,
			// Token: 0x04000451 RID: 1105
			ManagedProvider,
			// Token: 0x04000452 RID: 1106
			SessionCancellation,
			// Token: 0x04000453 RID: 1107
			DatatypeConversion,
			// Token: 0x04000454 RID: 1108
			InvalidMetadata,
			// Token: 0x04000455 RID: 1109
			InvalidUserdata,
			// Token: 0x04000456 RID: 1110
			OnPremiseError,
			// Token: 0x04000457 RID: 1111
			ResourceGovernanceError,
			// Token: 0x04000458 RID: 1112
			DatabaseDisabled,
			// Token: 0x04000459 RID: 1113
			Throttled
		}
	}
}
