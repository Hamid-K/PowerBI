using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021EA RID: 8682
	[GeneratedCode("DomGen", "2.0")]
	internal class FormulaTextBox : OpenXmlLeafTextElement
	{
		// Token: 0x170037CB RID: 14283
		// (get) Token: 0x0600DD02 RID: 56578 RVA: 0x002BD030 File Offset: 0x002BB230
		public override string LocalName
		{
			get
			{
				return "FmlaTxbx";
			}
		}

		// Token: 0x170037CC RID: 14284
		// (get) Token: 0x0600DD03 RID: 56579 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037CD RID: 14285
		// (get) Token: 0x0600DD04 RID: 56580 RVA: 0x002BD037 File Offset: 0x002BB237
		internal override int ElementTypeId
		{
			get
			{
				return 12503;
			}
		}

		// Token: 0x0600DD05 RID: 56581 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD06 RID: 56582 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FormulaTextBox()
		{
		}

		// Token: 0x0600DD07 RID: 56583 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FormulaTextBox(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD08 RID: 56584 RVA: 0x002BD040 File Offset: 0x002BB240
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD09 RID: 56585 RVA: 0x002BD05B File Offset: 0x002BB25B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormulaTextBox>(deep);
		}

		// Token: 0x04006CF2 RID: 27890
		private const string tagName = "FmlaTxbx";

		// Token: 0x04006CF3 RID: 27891
		private const byte tagNsId = 29;

		// Token: 0x04006CF4 RID: 27892
		internal const int ElementTypeIdConst = 12503;
	}
}
