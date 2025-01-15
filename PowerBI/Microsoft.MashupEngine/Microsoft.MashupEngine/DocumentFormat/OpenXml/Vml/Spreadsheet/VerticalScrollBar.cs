using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021CF RID: 8655
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalScrollBar : OpenXmlLeafTextElement
	{
		// Token: 0x1700377A RID: 14202
		// (get) Token: 0x0600DC2A RID: 56362 RVA: 0x002BCAB4 File Offset: 0x002BACB4
		public override string LocalName
		{
			get
			{
				return "VScroll";
			}
		}

		// Token: 0x1700377B RID: 14203
		// (get) Token: 0x0600DC2B RID: 56363 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700377C RID: 14204
		// (get) Token: 0x0600DC2C RID: 56364 RVA: 0x002BCABB File Offset: 0x002BACBB
		internal override int ElementTypeId
		{
			get
			{
				return 12466;
			}
		}

		// Token: 0x0600DC2D RID: 56365 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC2E RID: 56366 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VerticalScrollBar()
		{
		}

		// Token: 0x0600DC2F RID: 56367 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VerticalScrollBar(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC30 RID: 56368 RVA: 0x002BCAC4 File Offset: 0x002BACC4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC31 RID: 56369 RVA: 0x002BCADF File Offset: 0x002BACDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalScrollBar>(deep);
		}

		// Token: 0x04006CA1 RID: 27809
		private const string tagName = "VScroll";

		// Token: 0x04006CA2 RID: 27810
		private const byte tagNsId = 29;

		// Token: 0x04006CA3 RID: 27811
		internal const int ElementTypeIdConst = 12466;
	}
}
