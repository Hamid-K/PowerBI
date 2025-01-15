using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Internal.SchemaValidation;
using DocumentFormat.OpenXml.Internal.SemanticValidation;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x020030FB RID: 12539
	internal class SpreadsheetDocumentValidator : DocumentValidator
	{
		// Token: 0x0601B32F RID: 111407 RVA: 0x0036ED4E File Offset: 0x0036CF4E
		internal SpreadsheetDocumentValidator(ValidationSettings settings, SchemaValidator schemaValidator, SemanticValidator semanticValidator)
			: base(settings, schemaValidator, semanticValidator)
		{
		}

		// Token: 0x1700988F RID: 39055
		// (get) Token: 0x0601B330 RID: 111408 RVA: 0x00371BC9 File Offset: 0x0036FDC9
		// (set) Token: 0x0601B331 RID: 111409 RVA: 0x00371BD1 File Offset: 0x0036FDD1
		protected override OpenXmlPackage TargetDocument
		{
			get
			{
				return this._spreadsheetDocument;
			}
			set
			{
				this._spreadsheetDocument = (SpreadsheetDocument)value;
			}
		}

		// Token: 0x17009890 RID: 39056
		// (get) Token: 0x0601B332 RID: 111410 RVA: 0x00371BE0 File Offset: 0x0036FDE0
		protected override IEnumerable<OpenXmlPart> PartsToBeValidated
		{
			get
			{
				WorkbookPart workbookPart = this._spreadsheetDocument.WorkbookPart;
				if (workbookPart != null)
				{
					Dictionary<OpenXmlPart, bool> parts = new Dictionary<OpenXmlPart, bool>();
					this._spreadsheetDocument.FindAllReachableParts(parts);
					foreach (OpenXmlPart part in parts.Keys)
					{
						if (part.IsInVersion(base.ValidationSettings.FileFormat))
						{
							yield return part;
						}
					}
				}
				yield break;
			}
		}

		// Token: 0x0400B468 RID: 46184
		private SpreadsheetDocument _spreadsheetDocument;
	}
}
