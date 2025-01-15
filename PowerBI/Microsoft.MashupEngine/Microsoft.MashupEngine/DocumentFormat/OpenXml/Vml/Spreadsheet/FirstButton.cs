using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D4 RID: 8660
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstButton : OpenXmlLeafTextElement
	{
		// Token: 0x17003789 RID: 14217
		// (get) Token: 0x0600DC52 RID: 56402 RVA: 0x002BCBB8 File Offset: 0x002BADB8
		public override string LocalName
		{
			get
			{
				return "FirstButton";
			}
		}

		// Token: 0x1700378A RID: 14218
		// (get) Token: 0x0600DC53 RID: 56403 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700378B RID: 14219
		// (get) Token: 0x0600DC54 RID: 56404 RVA: 0x002BCBBF File Offset: 0x002BADBF
		internal override int ElementTypeId
		{
			get
			{
				return 12483;
			}
		}

		// Token: 0x0600DC55 RID: 56405 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC56 RID: 56406 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FirstButton()
		{
		}

		// Token: 0x0600DC57 RID: 56407 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FirstButton(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC58 RID: 56408 RVA: 0x002BCBC8 File Offset: 0x002BADC8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC59 RID: 56409 RVA: 0x002BCBE3 File Offset: 0x002BADE3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstButton>(deep);
		}

		// Token: 0x04006CB0 RID: 27824
		private const string tagName = "FirstButton";

		// Token: 0x04006CB1 RID: 27825
		private const byte tagNsId = 29;

		// Token: 0x04006CB2 RID: 27826
		internal const int ElementTypeIdConst = 12483;
	}
}
