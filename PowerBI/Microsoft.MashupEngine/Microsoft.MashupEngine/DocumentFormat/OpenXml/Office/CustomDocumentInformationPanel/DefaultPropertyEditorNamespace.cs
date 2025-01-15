using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomDocumentInformationPanel
{
	// Token: 0x020022AC RID: 8876
	[GeneratedCode("DomGen", "2.0")]
	internal class DefaultPropertyEditorNamespace : OpenXmlLeafTextElement
	{
		// Token: 0x17004136 RID: 16694
		// (get) Token: 0x0600F0DD RID: 61661 RVA: 0x002D0F2C File Offset: 0x002CF12C
		public override string LocalName
		{
			get
			{
				return "defaultPropertyEditorNamespace";
			}
		}

		// Token: 0x17004137 RID: 16695
		// (get) Token: 0x0600F0DE RID: 61662 RVA: 0x002D0E0F File Offset: 0x002CF00F
		internal override byte NamespaceId
		{
			get
			{
				return 37;
			}
		}

		// Token: 0x17004138 RID: 16696
		// (get) Token: 0x0600F0DF RID: 61663 RVA: 0x002D0F33 File Offset: 0x002CF133
		internal override int ElementTypeId
		{
			get
			{
				return 12632;
			}
		}

		// Token: 0x0600F0E0 RID: 61664 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0E1 RID: 61665 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DefaultPropertyEditorNamespace()
		{
		}

		// Token: 0x0600F0E2 RID: 61666 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DefaultPropertyEditorNamespace(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0E3 RID: 61667 RVA: 0x002D0F3C File Offset: 0x002CF13C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0E4 RID: 61668 RVA: 0x002D0F57 File Offset: 0x002CF157
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefaultPropertyEditorNamespace>(deep);
		}

		// Token: 0x0400709C RID: 28828
		private const string tagName = "defaultPropertyEditorNamespace";

		// Token: 0x0400709D RID: 28829
		private const byte tagNsId = 37;

		// Token: 0x0400709E RID: 28830
		internal const int ElementTypeIdConst = 12632;
	}
}
