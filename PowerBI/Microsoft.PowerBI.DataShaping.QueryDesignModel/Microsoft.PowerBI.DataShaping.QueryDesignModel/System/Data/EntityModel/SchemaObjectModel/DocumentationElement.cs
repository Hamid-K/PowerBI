using System;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200001C RID: 28
	internal sealed class DocumentationElement : SchemaElement
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x00009E7C File Offset: 0x0000807C
		public DocumentationElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00009E90 File Offset: 0x00008090
		public Documentation MetadataDocumentation
		{
			get
			{
				this._metdataDocumentation.SetReadOnly();
				return this._metdataDocumentation;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00009EA3 File Offset: 0x000080A3
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.CanHandleElement(reader, "Summary"))
			{
				this.HandleSummaryElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "LongDescription"))
			{
				this.HandleLongDescriptionElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00009EDF File Offset: 0x000080DF
		protected override bool HandleText(XmlReader reader)
		{
			if (!StringUtil.IsNullOrEmptyOrWhiteSpace(reader.Value))
			{
				base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, Strings.InvalidDocumentationBothTextAndStructure);
			}
			return true;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00009F00 File Offset: 0x00008100
		private void HandleSummaryElement(XmlReader reader)
		{
			TextElement textElement = new TextElement(this);
			textElement.Parse(reader);
			this._metdataDocumentation.Summary = textElement.Value;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00009F2C File Offset: 0x0000812C
		private void HandleLongDescriptionElement(XmlReader reader)
		{
			TextElement textElement = new TextElement(this);
			textElement.Parse(reader);
			this._metdataDocumentation.LongDescription = textElement.Value;
		}

		// Token: 0x040005A4 RID: 1444
		private Documentation _metdataDocumentation = new Documentation();
	}
}
