using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Internal.SchemaValidation;
using DocumentFormat.OpenXml.Internal.SemanticValidation;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x020030D6 RID: 12502
	internal class PresentationDocumentValidator : DocumentValidator
	{
		// Token: 0x0601B291 RID: 111249 RVA: 0x0036ED4E File Offset: 0x0036CF4E
		internal PresentationDocumentValidator(ValidationSettings settings, SchemaValidator schemaValidator, SemanticValidator semanticValidator)
			: base(settings, schemaValidator, semanticValidator)
		{
		}

		// Token: 0x17009872 RID: 39026
		// (get) Token: 0x0601B292 RID: 111250 RVA: 0x0036ED59 File Offset: 0x0036CF59
		// (set) Token: 0x0601B293 RID: 111251 RVA: 0x0036ED61 File Offset: 0x0036CF61
		protected override OpenXmlPackage TargetDocument
		{
			get
			{
				return this._presentationDocument;
			}
			set
			{
				this._presentationDocument = (PresentationDocument)value;
			}
		}

		// Token: 0x17009873 RID: 39027
		// (get) Token: 0x0601B294 RID: 111252 RVA: 0x0036ED70 File Offset: 0x0036CF70
		protected override IEnumerable<OpenXmlPart> PartsToBeValidated
		{
			get
			{
				PresentationPart mainPart = this._presentationDocument.PresentationPart;
				if (mainPart != null)
				{
					Dictionary<OpenXmlPart, bool> parts = new Dictionary<OpenXmlPart, bool>();
					this._presentationDocument.FindAllReachableParts(parts);
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

		// Token: 0x0400B3F2 RID: 46066
		private PresentationDocument _presentationDocument;
	}
}
