using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C0 RID: 8640
	[GeneratedCode("DomGen", "2.0")]
	internal class Disabled : OpenXmlLeafTextElement
	{
		// Token: 0x1700374D RID: 14157
		// (get) Token: 0x0600DBB2 RID: 56242 RVA: 0x002BC7A8 File Offset: 0x002BA9A8
		public override string LocalName
		{
			get
			{
				return "Disabled";
			}
		}

		// Token: 0x1700374E RID: 14158
		// (get) Token: 0x0600DBB3 RID: 56243 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700374F RID: 14159
		// (get) Token: 0x0600DBB4 RID: 56244 RVA: 0x002BC7AF File Offset: 0x002BA9AF
		internal override int ElementTypeId
		{
			get
			{
				return 12443;
			}
		}

		// Token: 0x0600DBB5 RID: 56245 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBB6 RID: 56246 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Disabled()
		{
		}

		// Token: 0x0600DBB7 RID: 56247 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Disabled(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBB8 RID: 56248 RVA: 0x002BC7B8 File Offset: 0x002BA9B8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBB9 RID: 56249 RVA: 0x002BC7D3 File Offset: 0x002BA9D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Disabled>(deep);
		}

		// Token: 0x04006C74 RID: 27764
		private const string tagName = "Disabled";

		// Token: 0x04006C75 RID: 27765
		private const byte tagNsId = 29;

		// Token: 0x04006C76 RID: 27766
		internal const int ElementTypeIdConst = 12443;
	}
}
