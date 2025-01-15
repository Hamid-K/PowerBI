using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x0200171E RID: 5918
	internal static class PermissionConversion
	{
		// Token: 0x06009660 RID: 38496 RVA: 0x001F20C0 File Offset: 0x001F02C0
		public static bool TryConvertToRecord(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames, out RecordValue permissionRecord)
		{
			if (type == QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted)
			{
				permissionRecord = RecordValue.New(PermissionConversion.nativeQueryKeys, new Value[]
				{
					TextValue.New(resource.Kind),
					TextValue.New(resource.Path),
					TextValue.New("NativeQuery"),
					TextValue.New(query),
					(parameterNames == null) ? NumberValue.New(parameterCount) : ListValue.New(parameterNames)
				});
				return true;
			}
			if (type != QueryPermissionChallengeType.EvaluateExchangeRedirectUnpermitted)
			{
				permissionRecord = null;
				return false;
			}
			permissionRecord = RecordValue.New(PermissionConversion.redirectKeys, new Value[]
			{
				TextValue.New(resource.Kind),
				TextValue.New(resource.Path),
				TextValue.New("Redirect"),
				TextValue.New(query)
			});
			return true;
		}

		// Token: 0x06009661 RID: 38497 RVA: 0x001F2184 File Offset: 0x001F0384
		public static void VerifyQueryPermission(IEngineHost engineHost, IResource resource, RecordValue record)
		{
			Dictionary<string, Value> dictionary = record.ToDictionary();
			QueryPermissionChallengeType queryPermissionChallengeType = QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted;
			TextValue asText = dictionary.GetAndRemoveOrDefault("PermissionKind", Value.Null).AsText;
			TextValue textValue = null;
			Value value = Value.Null;
			string @string = asText.String;
			if (!(@string == "NativeQuery"))
			{
				if (@string == "Redirect")
				{
					queryPermissionChallengeType = QueryPermissionChallengeType.EvaluateExchangeRedirectUnpermitted;
					textValue = dictionary.GetAndRemoveOrDefault("Value", Value.Null).AsText;
				}
			}
			else
			{
				queryPermissionChallengeType = QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted;
				textValue = dictionary.GetAndRemoveOrDefault("Value", Value.Null).AsText;
				value = dictionary.GetAndRemoveOrDefault("Parameters", Value.Null);
			}
			int num;
			string[] array;
			if (textValue == null || dictionary.Count > 0 || !PermissionConversion.TryGetParameters(value, out num, out array))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.InvalidPermission, record, null);
			}
			HostResourceQueryPermissionService.VerifyQueryPermission(engineHost, resource, queryPermissionChallengeType, textValue.String, num, array);
		}

		// Token: 0x06009662 RID: 38498 RVA: 0x001F2258 File Offset: 0x001F0458
		private static bool TryGetParameters(Value parameters, out int parameterCount, out string[] parameterNames)
		{
			parameterCount = 0;
			parameterNames = null;
			ValueKind kind = parameters.Kind;
			if (kind == ValueKind.Null)
			{
				return true;
			}
			if (kind == ValueKind.Number)
			{
				parameterCount = parameters.AsNumber.AsInteger32;
				return true;
			}
			if (kind != ValueKind.List)
			{
				return false;
			}
			parameterNames = new string[parameters.AsList.Count];
			int num = 0;
			foreach (IValueReference valueReference in parameters.AsList)
			{
				parameterNames[num++] = valueReference.Value.AsText.String;
			}
			return true;
		}

		// Token: 0x04004FF3 RID: 20467
		public const string PermissionKindKey = "PermissionKind";

		// Token: 0x04004FF4 RID: 20468
		private const string ValueKey = "Value";

		// Token: 0x04004FF5 RID: 20469
		private const string ParametersKey = "Parameters";

		// Token: 0x04004FF6 RID: 20470
		private static readonly Keys nativeQueryKeys = Keys.New(new string[] { "DataSource.Kind", "DataSource.Path", "PermissionKind", "Value", "Parameters" });

		// Token: 0x04004FF7 RID: 20471
		private static readonly Keys redirectKeys = Keys.New("DataSource.Kind", "DataSource.Path", "PermissionKind", "Value");
	}
}
