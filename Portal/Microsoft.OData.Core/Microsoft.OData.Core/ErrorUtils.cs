using System;
using System.Xml;

namespace Microsoft.OData
{
	// Token: 0x02000031 RID: 49
	internal static class ErrorUtils
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00004F67 File Offset: 0x00003167
		internal static void GetErrorDetails(ODataError error, out string code, out string message)
		{
			code = error.ErrorCode ?? string.Empty;
			message = error.Message ?? string.Empty;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00004F8C File Offset: 0x0000318C
		internal static void WriteXmlError(XmlWriter writer, ODataError error, bool includeDebugInformation, int maxInnerErrorDepth)
		{
			string text;
			string text2;
			ErrorUtils.GetErrorDetails(error, out text, out text2);
			ODataInnerError odataInnerError = (includeDebugInformation ? error.InnerError : null);
			ErrorUtils.WriteXmlError(writer, text, text2, odataInnerError, maxInnerErrorDepth);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00004FBC File Offset: 0x000031BC
		private static void WriteXmlError(XmlWriter writer, string code, string message, ODataInnerError innerError, int maxInnerErrorDepth)
		{
			writer.WriteStartElement("m", "error", "http://docs.oasis-open.org/odata/ns/metadata");
			writer.WriteElementString("m", "code", "http://docs.oasis-open.org/odata/ns/metadata", code);
			writer.WriteElementString("m", "message", "http://docs.oasis-open.org/odata/ns/metadata", message);
			if (innerError != null)
			{
				ErrorUtils.WriteXmlInnerError(writer, innerError, "innererror", 0, maxInnerErrorDepth);
			}
			writer.WriteEndElement();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005024 File Offset: 0x00003224
		private static void WriteXmlInnerError(XmlWriter writer, ODataInnerError innerError, string innerErrorElementName, int recursionDepth, int maxInnerErrorDepth)
		{
			recursionDepth++;
			if (recursionDepth > maxInnerErrorDepth)
			{
				throw new ODataException(Strings.ValidationUtils_RecursionDepthLimitReached(maxInnerErrorDepth));
			}
			writer.WriteStartElement("m", innerErrorElementName, "http://docs.oasis-open.org/odata/ns/metadata");
			string text = innerError.Message ?? string.Empty;
			writer.WriteStartElement("message", "http://docs.oasis-open.org/odata/ns/metadata");
			writer.WriteString(text);
			writer.WriteEndElement();
			string text2 = innerError.TypeName ?? string.Empty;
			writer.WriteStartElement("type", "http://docs.oasis-open.org/odata/ns/metadata");
			writer.WriteString(text2);
			writer.WriteEndElement();
			string text3 = innerError.StackTrace ?? string.Empty;
			writer.WriteStartElement("stacktrace", "http://docs.oasis-open.org/odata/ns/metadata");
			writer.WriteString(text3);
			writer.WriteEndElement();
			if (innerError.InnerError != null)
			{
				ErrorUtils.WriteXmlInnerError(writer, innerError.InnerError, "internalexception", recursionDepth, maxInnerErrorDepth);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000092 RID: 146
		internal const string ODataErrorMessageDefaultLanguage = "en-US";
	}
}
