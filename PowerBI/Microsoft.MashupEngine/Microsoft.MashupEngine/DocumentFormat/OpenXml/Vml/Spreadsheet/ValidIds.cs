using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D0 RID: 8656
	[GeneratedCode("DomGen", "2.0")]
	internal class ValidIds : OpenXmlLeafTextElement
	{
		// Token: 0x1700377D RID: 14205
		// (get) Token: 0x0600DC32 RID: 56370 RVA: 0x002BCAE8 File Offset: 0x002BACE8
		public override string LocalName
		{
			get
			{
				return "ValidIds";
			}
		}

		// Token: 0x1700377E RID: 14206
		// (get) Token: 0x0600DC33 RID: 56371 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700377F RID: 14207
		// (get) Token: 0x0600DC34 RID: 56372 RVA: 0x002BCAEF File Offset: 0x002BACEF
		internal override int ElementTypeId
		{
			get
			{
				return 12467;
			}
		}

		// Token: 0x0600DC35 RID: 56373 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC36 RID: 56374 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ValidIds()
		{
		}

		// Token: 0x0600DC37 RID: 56375 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ValidIds(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC38 RID: 56376 RVA: 0x002BCAF8 File Offset: 0x002BACF8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC39 RID: 56377 RVA: 0x002BCB13 File Offset: 0x002BAD13
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ValidIds>(deep);
		}

		// Token: 0x04006CA4 RID: 27812
		private const string tagName = "ValidIds";

		// Token: 0x04006CA5 RID: 27813
		private const byte tagNsId = 29;

		// Token: 0x04006CA6 RID: 27814
		internal const int ElementTypeIdConst = 12467;
	}
}
