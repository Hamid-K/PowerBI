using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021EB RID: 8683
	[GeneratedCode("DomGen", "2.0")]
	internal class FormulaMacro : OpenXmlLeafTextElement
	{
		// Token: 0x170037CE RID: 14286
		// (get) Token: 0x0600DD0A RID: 56586 RVA: 0x002BD064 File Offset: 0x002BB264
		public override string LocalName
		{
			get
			{
				return "FmlaMacro";
			}
		}

		// Token: 0x170037CF RID: 14287
		// (get) Token: 0x0600DD0B RID: 56587 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037D0 RID: 14288
		// (get) Token: 0x0600DD0C RID: 56588 RVA: 0x002BD06B File Offset: 0x002BB26B
		internal override int ElementTypeId
		{
			get
			{
				return 12447;
			}
		}

		// Token: 0x0600DD0D RID: 56589 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD0E RID: 56590 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FormulaMacro()
		{
		}

		// Token: 0x0600DD0F RID: 56591 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FormulaMacro(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD10 RID: 56592 RVA: 0x002BD074 File Offset: 0x002BB274
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD11 RID: 56593 RVA: 0x002BD08F File Offset: 0x002BB28F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormulaMacro>(deep);
		}

		// Token: 0x04006CF5 RID: 27893
		private const string tagName = "FmlaMacro";

		// Token: 0x04006CF6 RID: 27894
		private const byte tagNsId = 29;

		// Token: 0x04006CF7 RID: 27895
		internal const int ElementTypeIdConst = 12447;
	}
}
