using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E7 RID: 8679
	[GeneratedCode("DomGen", "2.0")]
	internal class FormulaGroup : OpenXmlLeafTextElement
	{
		// Token: 0x170037C2 RID: 14274
		// (get) Token: 0x0600DCEA RID: 56554 RVA: 0x002BCF94 File Offset: 0x002BB194
		public override string LocalName
		{
			get
			{
				return "FmlaGroup";
			}
		}

		// Token: 0x170037C3 RID: 14275
		// (get) Token: 0x0600DCEB RID: 56555 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037C4 RID: 14276
		// (get) Token: 0x0600DCEC RID: 56556 RVA: 0x002BCF9B File Offset: 0x002BB19B
		internal override int ElementTypeId
		{
			get
			{
				return 12484;
			}
		}

		// Token: 0x0600DCED RID: 56557 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCEE RID: 56558 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FormulaGroup()
		{
		}

		// Token: 0x0600DCEF RID: 56559 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FormulaGroup(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCF0 RID: 56560 RVA: 0x002BCFA4 File Offset: 0x002BB1A4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCF1 RID: 56561 RVA: 0x002BCFBF File Offset: 0x002BB1BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormulaGroup>(deep);
		}

		// Token: 0x04006CE9 RID: 27881
		private const string tagName = "FmlaGroup";

		// Token: 0x04006CEA RID: 27882
		private const byte tagNsId = 29;

		// Token: 0x04006CEB RID: 27883
		internal const int ElementTypeIdConst = 12484;
	}
}
