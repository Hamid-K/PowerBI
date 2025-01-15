using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002E6 RID: 742
	internal sealed class DocumentationElement : SchemaElement
	{
		// Token: 0x06002360 RID: 9056 RVA: 0x00063B9F File Offset: 0x00061D9F
		public DocumentationElement(SchemaElement parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x00063BB4 File Offset: 0x00061DB4
		public Documentation MetadataDocumentation
		{
			get
			{
				this._metdataDocumentation.SetReadOnly();
				return this._metdataDocumentation;
			}
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x00063BC7 File Offset: 0x00061DC7
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

		// Token: 0x06002363 RID: 9059 RVA: 0x00063C03 File Offset: 0x00061E03
		protected override bool HandleText(XmlReader reader)
		{
			if (!string.IsNullOrWhiteSpace(reader.Value))
			{
				base.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, Strings.InvalidDocumentationBothTextAndStructure);
			}
			return true;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x00063C24 File Offset: 0x00061E24
		private void HandleSummaryElement(XmlReader reader)
		{
			TextElement textElement = new TextElement(this);
			textElement.Parse(reader);
			this._metdataDocumentation.Summary = textElement.Value;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00063C50 File Offset: 0x00061E50
		private void HandleLongDescriptionElement(XmlReader reader)
		{
			TextElement textElement = new TextElement(this);
			textElement.Parse(reader);
			this._metdataDocumentation.LongDescription = textElement.Value;
		}

		// Token: 0x04000C17 RID: 3095
		private readonly Documentation _metdataDocumentation = new Documentation();
	}
}
