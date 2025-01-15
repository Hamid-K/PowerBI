using System;
using System.Xml;

namespace Microsoft.Data.OData
{
	// Token: 0x02000232 RID: 562
	internal static class ErrorUtils
	{
		// Token: 0x06001108 RID: 4360 RVA: 0x0003FB6C File Offset: 0x0003DD6C
		internal static void GetErrorDetails(ODataError error, out string code, out string message, out string messageLanguage)
		{
			code = error.ErrorCode ?? string.Empty;
			message = error.Message ?? string.Empty;
			messageLanguage = error.MessageLanguage ?? "en-US";
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		internal static void WriteXmlError(XmlWriter writer, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth)
		{
			string text;
			string text2;
			string text3;
			ErrorUtils.GetErrorDetails(error, out text, out text2, out text3);
			ODataInnerError odataInnerError = (includeDebugInformation ? error.InnerError : null);
			ErrorUtils.WriteXmlError(writer, text, text2, text3, odataInnerError, maxInnerErrorDepth);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0003FBD8 File Offset: 0x0003DDD8
		private static void WriteXmlError(XmlWriter writer, string code, string message, string messageLanguage, ODataInnerError innerError, int maxInnerErrorDepth)
		{
			writer.WriteStartElement("m", "error", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			writer.WriteElementString("m", "code", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", code);
			writer.WriteStartElement("m", "message", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", messageLanguage);
			writer.WriteString(message);
			writer.WriteEndElement();
			if (innerError != null)
			{
				ErrorUtils.WriteXmlInnerError(writer, innerError, "innererror", 0, maxInnerErrorDepth);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0003FC64 File Offset: 0x0003DE64
		private static void WriteXmlInnerError(XmlWriter writer, ODataInnerError innerError, string innerErrorElementName, int recursionDepth, int maxInnerErrorDepth)
		{
			recursionDepth++;
			if (recursionDepth > maxInnerErrorDepth)
			{
				throw new ODataException(Strings.ValidationUtils_RecursionDepthLimitReached(maxInnerErrorDepth));
			}
			writer.WriteStartElement("m", innerErrorElementName, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			string text = innerError.Message ?? string.Empty;
			writer.WriteStartElement("message", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			writer.WriteString(text);
			writer.WriteEndElement();
			string text2 = innerError.TypeName ?? string.Empty;
			writer.WriteStartElement("type", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			writer.WriteString(text2);
			writer.WriteEndElement();
			string text3 = innerError.StackTrace ?? string.Empty;
			writer.WriteStartElement("stacktrace", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			writer.WriteString(text3);
			writer.WriteEndElement();
			if (innerError.InnerError != null)
			{
				ErrorUtils.WriteXmlInnerError(writer, innerError.InnerError, "internalexception", recursionDepth, maxInnerErrorDepth);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0400068D RID: 1677
		internal const string ODataErrorMessageDefaultLanguage = "en-US";
	}
}
