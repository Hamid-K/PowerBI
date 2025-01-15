using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x0200171F RID: 5919
	internal class UserCredentialError
	{
		// Token: 0x06009664 RID: 38500 RVA: 0x001F2360 File Offset: 0x001F0560
		public static void Check(RecordValue errorValue)
		{
			Value value;
			Value value2;
			Value value3;
			if (errorValue.TryGetValue(ErrorRecord.DetailKey, out value) && value.IsRecord && value.AsRecord.TryGetValue("Key", out value2) && value2.IsText && value2.AsString == UserCredentialError.key && value.AsRecord.TryGetValue("Resource", out value3) && value3.IsList && value3.AsList.Count == 3)
			{
				IEngineHost engineHost = null;
				IResource resource = new Resource(value3[0].AsString, value3[1].AsString, value3[2].AsString);
				Value value4;
				string text;
				if (errorValue.TryGetValue(ErrorRecord.MessageKey, out value4) && value4.IsText)
				{
					text = value4.AsString;
				}
				else
				{
					text = Strings.GenericCredentialError;
				}
				Value value5;
				if (errorValue.TryGetValue(ErrorRecord.ReasonKey, out value5) && value5.IsText)
				{
					if (value5.AsString == "Credential.AccessForbidden")
					{
						throw DataSourceException.NewAccessForbiddenError(engineHost, resource, resource.Kind, text, null);
					}
					if (value5.AsString == "Credential.EncryptionNotSupported")
					{
						throw DataSourceException.NewEncryptedConnectionError(engineHost, resource, resource.Kind, text, null);
					}
					if (value5.AsString == "Credential.QueryPermission")
					{
						int num = 0;
						string[] array = null;
						string text2;
						if (!NativeQueryRecord.TryGetNativeQuery(value, out text2, out num, out array))
						{
							text2 = Strings.NativeQuery_Unspecified;
						}
						throw DataSourceException.NewQueryPermissionError(engineHost, resource, QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted, text2, num, array);
					}
				}
				throw DataSourceException.NewAccessAuthorizationError(engineHost, resource, resource.Kind, text, null);
			}
		}

		// Token: 0x06009665 RID: 38501 RVA: 0x001F2510 File Offset: 0x001F0710
		internal static RecordValue New(IResource resource, TextValue reason, Value message, Value detail)
		{
			RecordValue recordValue = RecordValue.New(UserCredentialError.Fields, new Value[]
			{
				ListValue.New(new Value[]
				{
					TextValue.New(resource.Kind),
					TextValue.New(resource.Path),
					TextValue.New(resource.NonNormalizedPath)
				}),
				TextValue.New(UserCredentialError.key)
			});
			detail = (detail.IsNull ? recordValue : detail.Concatenate(recordValue));
			return ErrorRecord.New(reason, message, detail);
		}

		// Token: 0x04004FF8 RID: 20472
		private static readonly string key = Guid.NewGuid().ToString();

		// Token: 0x04004FF9 RID: 20473
		private const string ResourceKey = "Resource";

		// Token: 0x04004FFA RID: 20474
		private const string KeyKey = "Key";

		// Token: 0x04004FFB RID: 20475
		private static readonly Keys Fields = Keys.New("Resource", "Key");

		// Token: 0x04004FFC RID: 20476
		public const string AccessDenied = "Credential.AccessDenied";

		// Token: 0x04004FFD RID: 20477
		public const string AccessForbidden = "Credential.AccessForbidden";

		// Token: 0x04004FFE RID: 20478
		public const string EncryptionNotSupported = "Credential.EncryptionNotSupported";

		// Token: 0x04004FFF RID: 20479
		public const string NativeQueryPermission = "Credential.QueryPermission";
	}
}
