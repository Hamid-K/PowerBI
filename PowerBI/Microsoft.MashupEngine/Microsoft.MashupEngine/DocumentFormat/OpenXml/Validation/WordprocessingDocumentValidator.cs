using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Internal.SchemaValidation;
using DocumentFormat.OpenXml.Internal.SemanticValidation;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Validation
{
	// Token: 0x02003153 RID: 12627
	internal class WordprocessingDocumentValidator : DocumentValidator
	{
		// Token: 0x0601B5EA RID: 112106 RVA: 0x0036ED4E File Offset: 0x0036CF4E
		internal WordprocessingDocumentValidator(ValidationSettings settings, SchemaValidator schemaValidator, SemanticValidator semanticValidator)
			: base(settings, schemaValidator, semanticValidator)
		{
		}

		// Token: 0x170099BB RID: 39355
		// (get) Token: 0x0601B5EB RID: 112107 RVA: 0x00376FBA File Offset: 0x003751BA
		// (set) Token: 0x0601B5EC RID: 112108 RVA: 0x00376FC2 File Offset: 0x003751C2
		protected override OpenXmlPackage TargetDocument
		{
			get
			{
				return this._wordprossingDocument;
			}
			set
			{
				this._wordprossingDocument = (WordprocessingDocument)value;
			}
		}

		// Token: 0x170099BC RID: 39356
		// (get) Token: 0x0601B5ED RID: 112109 RVA: 0x00376FD0 File Offset: 0x003751D0
		protected override IEnumerable<OpenXmlPart> PartsToBeValidated
		{
			get
			{
				MainDocumentPart mainPart = this._wordprossingDocument.MainDocumentPart;
				if (mainPart != null)
				{
					Dictionary<OpenXmlPart, bool> parts = new Dictionary<OpenXmlPart, bool>();
					this._wordprossingDocument.FindAllReachableParts(parts);
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

		// Token: 0x0400B55E RID: 46430
		private WordprocessingDocument _wordprossingDocument;
	}
}
