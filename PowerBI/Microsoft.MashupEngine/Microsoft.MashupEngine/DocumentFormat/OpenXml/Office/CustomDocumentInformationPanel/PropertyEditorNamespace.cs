using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomDocumentInformationPanel
{
	// Token: 0x020022AB RID: 8875
	[GeneratedCode("DomGen", "2.0")]
	internal class PropertyEditorNamespace : OpenXmlLeafTextElement
	{
		// Token: 0x17004133 RID: 16691
		// (get) Token: 0x0600F0D5 RID: 61653 RVA: 0x002D0EF8 File Offset: 0x002CF0F8
		public override string LocalName
		{
			get
			{
				return "XMLNamespace";
			}
		}

		// Token: 0x17004134 RID: 16692
		// (get) Token: 0x0600F0D6 RID: 61654 RVA: 0x002D0E0F File Offset: 0x002CF00F
		internal override byte NamespaceId
		{
			get
			{
				return 37;
			}
		}

		// Token: 0x17004135 RID: 16693
		// (get) Token: 0x0600F0D7 RID: 61655 RVA: 0x002D0EFF File Offset: 0x002CF0FF
		internal override int ElementTypeId
		{
			get
			{
				return 12629;
			}
		}

		// Token: 0x0600F0D8 RID: 61656 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0D9 RID: 61657 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PropertyEditorNamespace()
		{
		}

		// Token: 0x0600F0DA RID: 61658 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PropertyEditorNamespace(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0DB RID: 61659 RVA: 0x002D0F08 File Offset: 0x002CF108
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0DC RID: 61660 RVA: 0x002D0F23 File Offset: 0x002CF123
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PropertyEditorNamespace>(deep);
		}

		// Token: 0x04007099 RID: 28825
		private const string tagName = "XMLNamespace";

		// Token: 0x0400709A RID: 28826
		private const byte tagNsId = 37;

		// Token: 0x0400709B RID: 28827
		internal const int ElementTypeIdConst = 12629;
	}
}
