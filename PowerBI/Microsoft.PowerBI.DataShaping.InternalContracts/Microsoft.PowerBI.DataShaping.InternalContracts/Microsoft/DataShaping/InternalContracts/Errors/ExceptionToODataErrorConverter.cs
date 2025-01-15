using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common.Json;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.DataExtension.Contracts;

namespace Microsoft.DataShaping.InternalContracts.Errors
{
	// Token: 0x0200002F RID: 47
	internal static class ExceptionToODataErrorConverter
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00003C91 File Offset: 0x00001E91
		internal static ODataError Convert(HandledExceptionWrapper exceptionWrapper, bool enableRemoteErrors, IFeatureSwitchProvider featureSwitchProvider)
		{
			return ExceptionToODataErrorConverter.ConvertInternal(exceptionWrapper, enableRemoteErrors, featureSwitchProvider, false);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003C9C File Offset: 0x00001E9C
		private static ODataError ConvertInternal(HandledExceptionWrapper exceptionWrapper, bool enableRemoteErrors, IFeatureSwitchProvider featureSwitchProvider, bool isNested)
		{
			return new ODataError
			{
				Code = exceptionWrapper.ErrorCode,
				Source = exceptionWrapper.ErrorSource.ToString(),
				Message = ExceptionToODataErrorConverter.CreateMessage(exceptionWrapper.Language, exceptionWrapper.Message),
				AzureValues = ExceptionToODataErrorConverter.CreateAzureValues(exceptionWrapper, enableRemoteErrors, featureSwitchProvider, isNested)
			};
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003CFE File Offset: 0x00001EFE
		private static ErrorMessage CreateMessage(string language, string message)
		{
			return new ErrorMessage
			{
				Language = language,
				Value = message.RemovePrivateAndInternalMarkup()
			};
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003D18 File Offset: 0x00001F18
		private static AzureValue[] CreateAzureValues(HandledExceptionWrapper exceptionWrapper, bool enableRemoteErrors, IFeatureSwitchProvider featureSwitchProvider, bool isNested)
		{
			List<AzureValue> list = new List<AzureValue>();
			ExceptionToODataErrorConverter.AddIfNotNull<AzureValue>(list, ExceptionToODataErrorConverter.CreateTimestamp(isNested));
			ExceptionToODataErrorConverter.AddIfNotNull<AzureValue>(list, ExceptionToODataErrorConverter.CreateSource(exceptionWrapper.Source, isNested));
			ExceptionToODataErrorConverter.AddIfNotNull<AzureValue>(list, ExceptionToODataErrorConverter.CreateDetails(exceptionWrapper, enableRemoteErrors, featureSwitchProvider, isNested));
			ExceptionToODataErrorConverter.AddIfNotNull<AzureValue>(list, ExceptionToODataErrorConverter.CreatePowerBiErrorDetails(exceptionWrapper));
			ExceptionToODataErrorConverter.AddIfNotNull<AzureValue>(list, ExceptionToODataErrorConverter.CreateAdditionalMessages(exceptionWrapper));
			ExceptionToODataErrorConverter.AddIfNotNull<AzureValue>(list, ExceptionToODataErrorConverter.CreateMoreInformation(exceptionWrapper, enableRemoteErrors, featureSwitchProvider));
			return list.ToArray();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003D84 File Offset: 0x00001F84
		private static AzureValue CreateDetails(HandledExceptionWrapper exceptionWrapper, bool enableRemoteErrors, IFeatureSwitchProvider featureSwitchProvider, bool isNested)
		{
			string text = ExceptionToODataErrorConverter.ExtractDetailsMessage(exceptionWrapper, enableRemoteErrors, featureSwitchProvider, isNested);
			if (text == null)
			{
				return null;
			}
			return new AzureValue
			{
				Details = text
			};
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003DAC File Offset: 0x00001FAC
		private static AzureValue CreatePowerBiErrorDetails(HandledExceptionWrapper exceptionWrapper)
		{
			string text;
			if (!ExceptionToODataErrorConverter.TryExtractPowerBiErrorDetailsMessage(exceptionWrapper, out text))
			{
				return null;
			}
			return new AzureValue
			{
				PowerBiErrorDetails = text
			};
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003DD1 File Offset: 0x00001FD1
		private static AzureValue CreateSource(string source, bool isNested)
		{
			if (!isNested || source == null)
			{
				return null;
			}
			return new AzureValue
			{
				Source = source
			};
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003DE7 File Offset: 0x00001FE7
		private static AzureValue CreateTimestamp(bool isNested)
		{
			if (isNested)
			{
				return null;
			}
			return new AzureValue
			{
				Timestamp = JsonValueUtils.ToString(DateTimeOffset.UtcNow)
			};
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003E04 File Offset: 0x00002004
		private static AzureValue CreateAdditionalMessages(HandledExceptionWrapper exceptionWrapper)
		{
			DataShapeEngineException engineException = exceptionWrapper.EngineException;
			IReadOnlyList<Microsoft.DataShaping.ServiceContracts.AdditionalMessage> readOnlyList = ((engineException != null) ? engineException.AdditionalMessages : null);
			if (readOnlyList.IsNullOrEmpty<Microsoft.DataShaping.ServiceContracts.AdditionalMessage>())
			{
				return null;
			}
			Microsoft.InfoNav.Data.Contracts.DataShapeResult.AdditionalMessage[] array = new Microsoft.InfoNav.Data.Contracts.DataShapeResult.AdditionalMessage[readOnlyList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ExceptionToODataErrorConverter.CreateAdditionalMessage(readOnlyList[i]);
			}
			return new AzureValue
			{
				AdditionalMessages = array
			};
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003E64 File Offset: 0x00002064
		private static Microsoft.InfoNav.Data.Contracts.DataShapeResult.AdditionalMessage CreateAdditionalMessage(Microsoft.DataShaping.ServiceContracts.AdditionalMessage additionalMessage)
		{
			Microsoft.InfoNav.Data.Contracts.DataShapeResult.AdditionalMessage additionalMessage2 = new Microsoft.InfoNav.Data.Contracts.DataShapeResult.AdditionalMessage();
			additionalMessage2.Code = additionalMessage.Code;
			additionalMessage2.Severity = additionalMessage.Severity;
			additionalMessage2.Message = additionalMessage.Message.RemovePrivateAndInternalMarkup();
			additionalMessage2.ObjectType = additionalMessage.ObjectType;
			additionalMessage2.ObjectName = additionalMessage.ObjectName.RemovePrivateAndInternalMarkup();
			additionalMessage2.PropertyName = additionalMessage.PropertyName;
			additionalMessage2.Line = additionalMessage.Line;
			additionalMessage2.Position = additionalMessage.Position;
			if (!additionalMessage.AffectedItems.IsNullOrEmpty<string>())
			{
				string[] affectedItems = additionalMessage.AffectedItems;
				string[] array = new string[affectedItems.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = affectedItems[i].RemovePrivateAndInternalMarkup();
				}
				additionalMessage2.AffectedItems = array;
			}
			return additionalMessage2;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003F20 File Offset: 0x00002120
		private static AzureValue CreateMoreInformation(HandledExceptionWrapper exceptionWrapper, bool enableRemoteErrors, IFeatureSwitchProvider featureSwitchProvider)
		{
			HandledExceptionWrapper handledExceptionWrapper;
			if (!ExceptionToODataErrorConverter.TryExtractInnerException(exceptionWrapper, out handledExceptionWrapper))
			{
				return null;
			}
			return new AzureValue
			{
				MoreInformation = new MoreInformation
				{
					ODataError = ExceptionToODataErrorConverter.ConvertInternal(handledExceptionWrapper, enableRemoteErrors, featureSwitchProvider, true)
				}
			};
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003F58 File Offset: 0x00002158
		private static void AddIfNotNull<T>(List<T> list, T value) where T : class
		{
			if (value == null)
			{
				return;
			}
			list.Add(value);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003F6C File Offset: 0x0000216C
		internal static bool TryExtractDataExtensionException(HandledExceptionWrapper exceptionWrapper, out DataExtensionException deEx)
		{
			deEx = exceptionWrapper.DataExtensionException;
			if (deEx != null)
			{
				return true;
			}
			DataExtensionEngineException ex = exceptionWrapper.EngineException as DataExtensionEngineException;
			if (ex != null)
			{
				deEx = ex.ExtensionException;
				return true;
			}
			deEx = null;
			return false;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003FA8 File Offset: 0x000021A8
		internal static bool TryExtractInnerException(HandledExceptionWrapper exceptionWrapper, out HandledExceptionWrapper innerWrapper)
		{
			Exception innerException = exceptionWrapper.InnerException;
			DataShapeEngineException ex = innerException as DataShapeEngineException;
			DataExtensionException ex2 = innerException as DataExtensionException;
			if (ex == null && ex2 == null)
			{
				innerWrapper = default(HandledExceptionWrapper);
				return false;
			}
			innerWrapper = ((ex2 != null) ? ex2 : ex);
			return true;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003FF0 File Offset: 0x000021F0
		internal static bool TryExtractPowerBiErrorDetailsMessage(HandledExceptionWrapper exceptionWrapper, out string message)
		{
			message = null;
			DataExtensionException ex;
			if (!ExceptionToODataErrorConverter.TryExtractDataExtensionException(exceptionWrapper, out ex))
			{
				return false;
			}
			if (ex.ErrorCode != "OnPremiseServiceException")
			{
				return false;
			}
			message = ex.ProviderMessage.RemovePrivateAndInternalMarkup();
			return true;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004030 File Offset: 0x00002230
		internal static string ExtractDetailsMessage(HandledExceptionWrapper exceptionWrapper, bool enableRemoteErrors, IFeatureSwitchProvider featureSwitchProvider, bool isNested)
		{
			string text = null;
			DataExtensionException ex;
			ExceptionToODataErrorConverter.TryExtractDataExtensionException(exceptionWrapper, out ex);
			if (isNested)
			{
				text = exceptionWrapper.Message;
			}
			else if (enableRemoteErrors)
			{
				text = exceptionWrapper.ErrorDetails;
			}
			else if (ex != null && ex.HasUserSafeErrorDetails && featureSwitchProvider.IsEnabled(FeatureSwitchKind.IncludeSafeASQueryErrorsInDsr))
			{
				text = ex.GetErrorDetails();
			}
			return text.RemovePrivateAndInternalMarkup();
		}
	}
}
