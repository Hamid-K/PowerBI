using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021FA RID: 8698
	[GeneratedCode("DomGen", "2.0")]
	internal class ScrollBarWidth : OpenXmlLeafTextElement
	{
		// Token: 0x170037FB RID: 14331
		// (get) Token: 0x0600DD82 RID: 56706 RVA: 0x002BD370 File Offset: 0x002BB570
		public override string LocalName
		{
			get
			{
				return "Dx";
			}
		}

		// Token: 0x170037FC RID: 14332
		// (get) Token: 0x0600DD83 RID: 56707 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037FD RID: 14333
		// (get) Token: 0x0600DD84 RID: 56708 RVA: 0x002BD377 File Offset: 0x002BB577
		internal override int ElementTypeId
		{
			get
			{
				return 12491;
			}
		}

		// Token: 0x0600DD85 RID: 56709 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD86 RID: 56710 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ScrollBarWidth()
		{
		}

		// Token: 0x0600DD87 RID: 56711 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ScrollBarWidth(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD88 RID: 56712 RVA: 0x002BD380 File Offset: 0x002BB580
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD89 RID: 56713 RVA: 0x002BD39B File Offset: 0x002BB59B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScrollBarWidth>(deep);
		}

		// Token: 0x04006D22 RID: 27938
		private const string tagName = "Dx";

		// Token: 0x04006D23 RID: 27939
		private const byte tagNsId = 29;

		// Token: 0x04006D24 RID: 27940
		internal const int ElementTypeIdConst = 12491;
	}
}
