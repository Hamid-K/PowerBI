using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001BD RID: 445
	internal static class JsonSerializationUtil
	{
		// Token: 0x06001BBD RID: 7101 RVA: 0x000C2DB4 File Offset: 0x000C0FB4
		internal static JsonSerializationException CreateException(string message, Exception innerException = null)
		{
			return new JsonSerializationException(message, innerException);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000C2DBD File Offset: 0x000C0FBD
		internal static JsonSerializationException CreateException(string message, JsonReaderException readerEx)
		{
			return JsonSerializationUtil.CreateException(message, readerEx.Path, readerEx.LineNumber, readerEx.LinePosition, readerEx);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000C2DD8 File Offset: 0x000C0FD8
		internal static JsonSerializationException CreateException(string message, JsonTextReader reader, Exception innerException = null)
		{
			return JsonSerializationUtil.CreateException(message, reader.Path, reader, innerException);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000C2DE8 File Offset: 0x000C0FE8
		internal static JsonSerializationException CreateException(string message, JToken badJsonToken, Exception innerException = null)
		{
			return JsonSerializationUtil.CreateException(message, badJsonToken.Path, badJsonToken, innerException);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000C2DF8 File Offset: 0x000C0FF8
		internal static JsonSerializationException CreateException(string message, string jsonPath, IJsonLineInfo lineInfo, Exception innerException = null)
		{
			if (lineInfo.HasLineInfo())
			{
				return JsonSerializationUtil.CreateException(message, jsonPath, lineInfo.LineNumber, lineInfo.LinePosition, innerException);
			}
			return new JsonSerializationException(message, innerException);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000C2E1E File Offset: 0x000C101E
		private static JsonSerializationException CreateException(string message, string jsonPath, int lineNumber, int linePosition, Exception innerException = null)
		{
			return new JsonSerializationException(TomSR.Exception_ErrorWithPathAndLineInfo(message, jsonPath, lineNumber.ToString(), linePosition.ToString()), innerException)
			{
				HasLineInfo = true,
				LineNumber = lineNumber,
				LinePosition = linePosition
			};
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000C2E51 File Offset: 0x000C1051
		internal static JsonSerializationException CreateCannotDeserializeObjectException(ObjectType type, string errorDetails = null, Exception innerException = null)
		{
			if (string.IsNullOrEmpty(errorDetails))
			{
				return new JsonSerializationException(TomSR.Exception_CannotDeserializeObject(Utils.GetUserFriendlyNameOfObjectType(type)), innerException);
			}
			return new JsonSerializationException(TomSR.Exception_CannotDeserializeObjectWithDetails(Utils.GetUserFriendlyNameOfObjectType(type), errorDetails), innerException);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x000C2E7F File Offset: 0x000C107F
		internal static JsonSerializationException CreateCannotDeserializeObjectException(ObjectType type, JsonTextReader reader, string errorDetails = null, Exception innerException = null)
		{
			if (string.IsNullOrEmpty(errorDetails))
			{
				return JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObject(Utils.GetUserFriendlyNameOfObjectType(type)), reader, innerException);
			}
			return JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectWithDetails(Utils.GetUserFriendlyNameOfObjectType(type), errorDetails), reader, innerException);
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000C2EAF File Offset: 0x000C10AF
		internal static JsonSerializationException CreateCannotResolvePathsWhileDeserializeObjectException(ObjectType type, ICollection<string> linksFailedToResolve, Exception innerException = null)
		{
			if (linksFailedToResolve == null)
			{
				return JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectResolvePathsFailed(Utils.GetUserFriendlyNameOfObjectType(type)), innerException);
			}
			return JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectResolvePathsFailedWithList(Utils.GetUserFriendlyNameOfObjectType(type), ClientHostingManager.MarkAsRestrictedInformation(string.Join(", ", linksFailedToResolve), InfoRestrictionType.CCON)), innerException);
		}
	}
}
