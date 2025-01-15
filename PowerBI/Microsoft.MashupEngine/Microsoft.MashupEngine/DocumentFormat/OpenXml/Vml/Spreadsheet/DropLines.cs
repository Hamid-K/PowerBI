using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F3 RID: 8691
	[GeneratedCode("DomGen", "2.0")]
	internal class DropLines : OpenXmlLeafTextElement
	{
		// Token: 0x170037E6 RID: 14310
		// (get) Token: 0x0600DD4A RID: 56650 RVA: 0x002BD204 File Offset: 0x002BB404
		public override string LocalName
		{
			get
			{
				return "DropLines";
			}
		}

		// Token: 0x170037E7 RID: 14311
		// (get) Token: 0x0600DD4B RID: 56651 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037E8 RID: 14312
		// (get) Token: 0x0600DD4C RID: 56652 RVA: 0x002BD20B File Offset: 0x002BB40B
		internal override int ElementTypeId
		{
			get
			{
				return 12478;
			}
		}

		// Token: 0x0600DD4D RID: 56653 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD4E RID: 56654 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DropLines()
		{
		}

		// Token: 0x0600DD4F RID: 56655 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DropLines(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD50 RID: 56656 RVA: 0x002BD214 File Offset: 0x002BB414
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD51 RID: 56657 RVA: 0x002BD22F File Offset: 0x002BB42F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropLines>(deep);
		}

		// Token: 0x04006D0D RID: 27917
		private const string tagName = "DropLines";

		// Token: 0x04006D0E RID: 27918
		private const byte tagNsId = 29;

		// Token: 0x04006D0F RID: 27919
		internal const int ElementTypeIdConst = 12478;
	}
}
