using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030C7 RID: 12487
	internal static class AlternativeFormatImportPartTypeInfo
	{
		// Token: 0x0601B1E1 RID: 111073 RVA: 0x0036C3C0 File Offset: 0x0036A5C0
		internal static string GetContentType(AlternativeFormatImportPartType partType)
		{
			switch (partType)
			{
			case AlternativeFormatImportPartType.Xhtml:
				return "application/xhtml+xml";
			case AlternativeFormatImportPartType.Mht:
				return "message/rfc822";
			case AlternativeFormatImportPartType.Xml:
				return "application/xml";
			case AlternativeFormatImportPartType.TextPlain:
				return "text/plain";
			case AlternativeFormatImportPartType.WordprocessingML:
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml";
			case AlternativeFormatImportPartType.OfficeWordMacroEnabled:
				return "application/vnd.ms-word.document.macroEnabled.main+xml";
			case AlternativeFormatImportPartType.OfficeWordTemplate:
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml";
			case AlternativeFormatImportPartType.OfficeWordMacroEnabledTemplate:
				return "application/vnd.ms-word.template.macroEnabledTemplate.main+xml";
			case AlternativeFormatImportPartType.Rtf:
				return "application/rtf";
			case AlternativeFormatImportPartType.Html:
				return "text/html";
			default:
				throw new ArgumentOutOfRangeException("partType");
			}
		}

		// Token: 0x0601B1E2 RID: 111074 RVA: 0x0036C448 File Offset: 0x0036A648
		internal static string GetTargetExtension(AlternativeFormatImportPartType imageType)
		{
			switch (imageType)
			{
			case AlternativeFormatImportPartType.Xhtml:
				return ".xhtml";
			case AlternativeFormatImportPartType.Mht:
				return ".mht";
			case AlternativeFormatImportPartType.Xml:
				return ".xml";
			case AlternativeFormatImportPartType.TextPlain:
				return ".txt";
			case AlternativeFormatImportPartType.WordprocessingML:
				return ".docx";
			case AlternativeFormatImportPartType.OfficeWordMacroEnabled:
				return ".docm";
			case AlternativeFormatImportPartType.OfficeWordTemplate:
				return ".dotx";
			case AlternativeFormatImportPartType.OfficeWordMacroEnabledTemplate:
				return ".dotm";
			case AlternativeFormatImportPartType.Rtf:
				return ".rtf";
			case AlternativeFormatImportPartType.Html:
				return ".htm";
			default:
				return ".dat";
			}
		}
	}
}
