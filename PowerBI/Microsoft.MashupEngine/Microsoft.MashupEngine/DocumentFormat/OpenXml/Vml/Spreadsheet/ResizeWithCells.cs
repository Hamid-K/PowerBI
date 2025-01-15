using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021BC RID: 8636
	[GeneratedCode("DomGen", "2.0")]
	internal class ResizeWithCells : OpenXmlLeafTextElement
	{
		// Token: 0x17003741 RID: 14145
		// (get) Token: 0x0600DB92 RID: 56210 RVA: 0x002BC6D8 File Offset: 0x002BA8D8
		public override string LocalName
		{
			get
			{
				return "SizeWithCells";
			}
		}

		// Token: 0x17003742 RID: 14146
		// (get) Token: 0x0600DB93 RID: 56211 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003743 RID: 14147
		// (get) Token: 0x0600DB94 RID: 56212 RVA: 0x002BC6DF File Offset: 0x002BA8DF
		internal override int ElementTypeId
		{
			get
			{
				return 12438;
			}
		}

		// Token: 0x0600DB95 RID: 56213 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DB96 RID: 56214 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ResizeWithCells()
		{
		}

		// Token: 0x0600DB97 RID: 56215 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ResizeWithCells(string text)
			: base(text)
		{
		}

		// Token: 0x0600DB98 RID: 56216 RVA: 0x002BC6E8 File Offset: 0x002BA8E8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DB99 RID: 56217 RVA: 0x002BC703 File Offset: 0x002BA903
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ResizeWithCells>(deep);
		}

		// Token: 0x04006C68 RID: 27752
		private const string tagName = "SizeWithCells";

		// Token: 0x04006C69 RID: 27753
		private const byte tagNsId = 29;

		// Token: 0x04006C6A RID: 27754
		internal const int ElementTypeIdConst = 12438;
	}
}
