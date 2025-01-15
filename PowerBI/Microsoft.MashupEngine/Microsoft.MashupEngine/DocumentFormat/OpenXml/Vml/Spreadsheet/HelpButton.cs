using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C8 RID: 8648
	[GeneratedCode("DomGen", "2.0")]
	internal class HelpButton : OpenXmlLeafTextElement
	{
		// Token: 0x17003765 RID: 14181
		// (get) Token: 0x0600DBF2 RID: 56306 RVA: 0x002BC948 File Offset: 0x002BAB48
		public override string LocalName
		{
			get
			{
				return "Help";
			}
		}

		// Token: 0x17003766 RID: 14182
		// (get) Token: 0x0600DBF3 RID: 56307 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003767 RID: 14183
		// (get) Token: 0x0600DBF4 RID: 56308 RVA: 0x002BC94F File Offset: 0x002BAB4F
		internal override int ElementTypeId
		{
			get
			{
				return 12454;
			}
		}

		// Token: 0x0600DBF5 RID: 56309 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBF6 RID: 56310 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public HelpButton()
		{
		}

		// Token: 0x0600DBF7 RID: 56311 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public HelpButton(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBF8 RID: 56312 RVA: 0x002BC958 File Offset: 0x002BAB58
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBF9 RID: 56313 RVA: 0x002BC973 File Offset: 0x002BAB73
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HelpButton>(deep);
		}

		// Token: 0x04006C8C RID: 27788
		private const string tagName = "Help";

		// Token: 0x04006C8D RID: 27789
		private const byte tagNsId = 29;

		// Token: 0x04006C8E RID: 27790
		internal const int ElementTypeIdConst = 12454;
	}
}
