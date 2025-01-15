using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021BB RID: 8635
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveWithCells : OpenXmlLeafTextElement
	{
		// Token: 0x1700373E RID: 14142
		// (get) Token: 0x0600DB8A RID: 56202 RVA: 0x002BC693 File Offset: 0x002BA893
		public override string LocalName
		{
			get
			{
				return "MoveWithCells";
			}
		}

		// Token: 0x1700373F RID: 14143
		// (get) Token: 0x0600DB8B RID: 56203 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003740 RID: 14144
		// (get) Token: 0x0600DB8C RID: 56204 RVA: 0x002BC69A File Offset: 0x002BA89A
		internal override int ElementTypeId
		{
			get
			{
				return 12437;
			}
		}

		// Token: 0x0600DB8D RID: 56205 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DB8E RID: 56206 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public MoveWithCells()
		{
		}

		// Token: 0x0600DB8F RID: 56207 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public MoveWithCells(string text)
			: base(text)
		{
		}

		// Token: 0x0600DB90 RID: 56208 RVA: 0x002BC6B4 File Offset: 0x002BA8B4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DB91 RID: 56209 RVA: 0x002BC6CF File Offset: 0x002BA8CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveWithCells>(deep);
		}

		// Token: 0x04006C65 RID: 27749
		private const string tagName = "MoveWithCells";

		// Token: 0x04006C66 RID: 27750
		private const byte tagNsId = 29;

		// Token: 0x04006C67 RID: 27751
		internal const int ElementTypeIdConst = 12437;
	}
}
