using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020030CB RID: 12491
	internal static class DocumentTypeDetector
	{
		// Token: 0x1700983F RID: 38975
		// (get) Token: 0x0601B1E5 RID: 111077 RVA: 0x0036C558 File Offset: 0x0036A758
		internal static Dictionary<string, OpenXmlDocumentType> MainPartContentTypes
		{
			get
			{
				if (DocumentTypeDetector._mainPartContentTypes == null)
				{
					DocumentTypeDetector._mainPartContentTypes = new Dictionary<string, OpenXmlDocumentType>
					{
						{
							"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml",
							OpenXmlDocumentType.Wordprocessing
						},
						{
							"application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml",
							OpenXmlDocumentType.Wordprocessing
						},
						{
							"application/vnd.ms-word.document.macroEnabled.main+xml",
							OpenXmlDocumentType.Wordprocessing
						},
						{
							"application/vnd.ms-word.template.macroEnabledTemplate.main+xml",
							OpenXmlDocumentType.Wordprocessing
						},
						{
							"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml",
							OpenXmlDocumentType.Spreadsheet
						},
						{
							"application/vnd.openxmlformats-officedocument.spreadsheetml.template.main+xml",
							OpenXmlDocumentType.Spreadsheet
						},
						{
							"application/vnd.ms-excel.sheet.macroEnabled.main+xml",
							OpenXmlDocumentType.Spreadsheet
						},
						{
							"application/vnd.ms-excel.template.macroEnabled.main+xml",
							OpenXmlDocumentType.Spreadsheet
						},
						{
							"application/vnd.ms-excel.addin.macroEnabled.main+xml",
							OpenXmlDocumentType.Spreadsheet
						},
						{
							"application/vnd.openxmlformats-officedocument.presentationml.presentation.main+xml",
							OpenXmlDocumentType.Presentation
						},
						{
							"application/vnd.openxmlformats-officedocument.presentationml.template.main+xml",
							OpenXmlDocumentType.Presentation
						},
						{
							"application/vnd.openxmlformats-officedocument.presentationml.slideshow.main+xml",
							OpenXmlDocumentType.Presentation
						},
						{
							"application/vnd.ms-powerpoint.presentation.macroEnabled.main+xml",
							OpenXmlDocumentType.Presentation
						},
						{
							"application/vnd.ms-powerpoint.template.macroEnabled.main+xml",
							OpenXmlDocumentType.Presentation
						},
						{
							"application/vnd.ms-powerpoint.slideshow.macroEnabled.main+xml",
							OpenXmlDocumentType.Presentation
						},
						{
							"application/vnd.ms-powerpoint.addin.macroEnabled.main+xml",
							OpenXmlDocumentType.Presentation
						}
					};
				}
				return DocumentTypeDetector._mainPartContentTypes;
			}
		}

		// Token: 0x0601B1E6 RID: 111078 RVA: 0x0036C640 File Offset: 0x0036A840
		internal static OpenXmlDocumentType GetDocumentType(OpenXmlPackage openXmlPackage)
		{
			if (openXmlPackage is WordprocessingDocument)
			{
				return OpenXmlDocumentType.Wordprocessing;
			}
			if (openXmlPackage is SpreadsheetDocument)
			{
				return OpenXmlDocumentType.Spreadsheet;
			}
			if (openXmlPackage is PresentationDocument)
			{
				return OpenXmlDocumentType.Presentation;
			}
			return OpenXmlDocumentType.Invalid;
		}

		// Token: 0x0400B3C8 RID: 46024
		private const string _mainPartRelationshipType = "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";

		// Token: 0x0400B3C9 RID: 46025
		private static Dictionary<string, OpenXmlDocumentType> _mainPartContentTypes;
	}
}
