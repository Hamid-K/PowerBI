using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D1 RID: 8657
	[GeneratedCode("DomGen", "2.0")]
	internal class Disable3DForListBoxAndDropDown : OpenXmlLeafTextElement
	{
		// Token: 0x17003780 RID: 14208
		// (get) Token: 0x0600DC3A RID: 56378 RVA: 0x002BCB1C File Offset: 0x002BAD1C
		public override string LocalName
		{
			get
			{
				return "NoThreeD2";
			}
		}

		// Token: 0x17003781 RID: 14209
		// (get) Token: 0x0600DC3B RID: 56379 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003782 RID: 14210
		// (get) Token: 0x0600DC3C RID: 56380 RVA: 0x002BCB23 File Offset: 0x002BAD23
		internal override int ElementTypeId
		{
			get
			{
				return 12471;
			}
		}

		// Token: 0x0600DC3D RID: 56381 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC3E RID: 56382 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Disable3DForListBoxAndDropDown()
		{
		}

		// Token: 0x0600DC3F RID: 56383 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Disable3DForListBoxAndDropDown(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC40 RID: 56384 RVA: 0x002BCB2C File Offset: 0x002BAD2C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC41 RID: 56385 RVA: 0x002BCB47 File Offset: 0x002BAD47
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Disable3DForListBoxAndDropDown>(deep);
		}

		// Token: 0x04006CA7 RID: 27815
		private const string tagName = "NoThreeD2";

		// Token: 0x04006CA8 RID: 27816
		private const byte tagNsId = 29;

		// Token: 0x04006CA9 RID: 27817
		internal const int ElementTypeIdConst = 12471;
	}
}
