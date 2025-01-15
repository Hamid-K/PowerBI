using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000014 RID: 20
	internal static class QueryExtensionErrorExtractor
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00002C40 File Offset: 0x00000E40
		public static bool TryExtract(DataExtensionException providerException, IReadOnlyList<ItemSourceLocation> querySourceMap, out DataExtensionEngineException wrappedException)
		{
			wrappedException = null;
			if (querySourceMap.IsNullOrEmpty<ItemSourceLocation>())
			{
				return false;
			}
			if (providerException == null)
			{
				return false;
			}
			int num;
			int num2;
			string text;
			if (!QueryExtensionErrorExtractor.TryGetQueryErrorLocation(providerException, out num, out num2, out text))
			{
				return false;
			}
			List<AdditionalMessage> list = new List<AdditionalMessage>();
			for (int i = 0; i < querySourceMap.Count; i++)
			{
				ItemSourceLocation itemSourceLocation = querySourceMap[i];
				AdditionalMessage additionalMessage;
				if (QueryExtensionErrorExtractor.TryGetSourceErrorMessage(itemSourceLocation, num, num2, out additionalMessage) || QueryExtensionErrorExtractor.TryGetWrapperErrorMessage(itemSourceLocation, num, out additionalMessage))
				{
					list.Add(additionalMessage);
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			string code = list[0].Code;
			string message = list[0].Message;
			uint providerErrorCode = providerException.ProviderErrorCode;
			string providerMessage = providerException.ProviderMessage;
			string providerGenericMessage = providerException.ProviderGenericMessage;
			int hresult = providerException.HResult;
			bool hasUserSafeProviderMessage = providerException.HasUserSafeProviderMessage;
			string onPremErrorCode = providerException.OnPremErrorCode;
			bool containsPii = providerException.ContainsPii;
			Exception innerException = providerException.InnerException;
			ErrorSource errorSource = providerException.ErrorSource;
			string errorSourceOrigin = providerException.ErrorSourceOrigin;
			wrappedException = new DataExtensionEngineException(code, message, new DataExtensionException(providerException.ErrorCode, providerException.Message, providerErrorCode, providerMessage, providerGenericMessage, hresult, hasUserSafeProviderMessage, onPremErrorCode, containsPii, errorSource, errorSourceOrigin, innerException, text.MarkAsCustomerContent()), list);
			return true;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002D5C File Offset: 0x00000F5C
		private static bool TryGetWrapperErrorMessage(ItemSourceLocation source, int absoluteLineNumber, out AdditionalMessage sourceErrorMessage)
		{
			sourceErrorMessage = null;
			if (source.SourceLineEnd > absoluteLineNumber || source.WrapperLineEnd <= absoluteLineNumber)
			{
				return false;
			}
			ItemSourceType sourceType = source.SourceType;
			string text;
			string text2;
			if (sourceType != ItemSourceType.QueryExtensionMeasure)
			{
				if (sourceType != ItemSourceType.QueryExtensionColumn)
				{
					throw new InvalidOperationException(StringUtil.FormatInvariant("Unexpected Source item type '{0}'.", new object[] { source.SourceType.ToString() }));
				}
				text = "QueryExtensionColumnUnexpectedEndOfUserInput";
				text2 = ProcessingErrorMessages.QueryExtensionColumnUnexpectedEndOfUserInput(source.QuerySourceName);
			}
			else
			{
				text = "QueryExtensionMeasureUnexpectedEndOfUserInput";
				text2 = ProcessingErrorMessages.QueryExtensionMeasureUnexpectedEndOfUserInput(source.QuerySourceName);
			}
			sourceErrorMessage = QueryExtensionErrorExtractor.CreateQuerySourceItemAdditionalMessage(source, text, text2, null, null);
			return true;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002E04 File Offset: 0x00001004
		private static bool TryGetSourceErrorMessage(ItemSourceLocation source, int absoluteLineNumber, int absolutePosition, out AdditionalMessage sourceErrorMessage)
		{
			sourceErrorMessage = null;
			if (source.SourceLineStart > absoluteLineNumber || source.SourceLineEnd <= absoluteLineNumber)
			{
				return false;
			}
			int num = absoluteLineNumber - source.SourceLineStart + 1;
			int num2 = absolutePosition - source.SourceIndent;
			ItemSourceType sourceType = source.SourceType;
			string text;
			string text2;
			if (sourceType != ItemSourceType.QueryExtensionMeasure)
			{
				if (sourceType != ItemSourceType.QueryExtensionColumn)
				{
					throw new InvalidOperationException(StringUtil.FormatInvariant("Unexpected Source item type '{0}'.", new object[] { source.SourceType.ToString() }));
				}
				text = "QueryExtensionColumnError";
				text2 = ProcessingErrorMessages.QueryExtensionColumnError(source.QuerySourceName, num, num2);
			}
			else
			{
				text = "QueryExtensionMeasureError";
				text2 = ProcessingErrorMessages.QueryExtensionMeasureError(source.QuerySourceName, num, num2);
			}
			sourceErrorMessage = QueryExtensionErrorExtractor.CreateQuerySourceItemAdditionalMessage(source, text, text2, new int?(num), new int?(num2));
			return true;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002EC0 File Offset: 0x000010C0
		public static AdditionalMessage CreateQuerySourceItemAdditionalMessage(ItemSourceLocation source, string code, string message, int? relativeLineNumber, int? relativePosition)
		{
			return new AdditionalMessage(code, EngineMessageSeverity.Error.ToString(), message, null, null, null, new string[] { source.QuerySourceName }, relativeLineNumber, relativePosition);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002EF8 File Offset: 0x000010F8
		private static bool TryGetQueryErrorLocation(DataExtensionException providerException, out int absoluteLineNumber, out int absolutePosition, out string messageWithoutLocationInfo)
		{
			absoluteLineNumber = -1;
			absolutePosition = -1;
			messageWithoutLocationInfo = null;
			string text = providerException.ProviderMessage.RemovePrivateAndInternalMarkup();
			MatchCollection matchCollection = QueryExtensionErrorExtractor.ErrorWithLocationRegex.Matches(text);
			if (matchCollection.Count != 1)
			{
				return false;
			}
			GroupCollection groups = matchCollection[0].Groups;
			if (groups.Count != 3)
			{
				return false;
			}
			if (!int.TryParse(groups[1].Value, out absoluteLineNumber))
			{
				return false;
			}
			if (!int.TryParse(groups[2].Value, out absolutePosition))
			{
				return false;
			}
			messageWithoutLocationInfo = text.Substring(groups[0].Length);
			return true;
		}

		// Token: 0x04000078 RID: 120
		private static readonly Regex ErrorWithLocationRegex = new Regex("^Query \\(([0-9]+), ([0-9]+)\\) ");
	}
}
