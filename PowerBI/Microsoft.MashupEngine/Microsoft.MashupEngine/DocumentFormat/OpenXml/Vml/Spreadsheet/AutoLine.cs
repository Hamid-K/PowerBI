using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021C2 RID: 8642
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoLine : OpenXmlLeafTextElement
	{
		// Token: 0x17003753 RID: 14163
		// (get) Token: 0x0600DBC2 RID: 56258 RVA: 0x002BC810 File Offset: 0x002BAA10
		public override string LocalName
		{
			get
			{
				return "AutoLine";
			}
		}

		// Token: 0x17003754 RID: 14164
		// (get) Token: 0x0600DBC3 RID: 56259 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003755 RID: 14165
		// (get) Token: 0x0600DBC4 RID: 56260 RVA: 0x002BC817 File Offset: 0x002BAA17
		internal override int ElementTypeId
		{
			get
			{
				return 12445;
			}
		}

		// Token: 0x0600DBC5 RID: 56261 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DBC6 RID: 56262 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AutoLine()
		{
		}

		// Token: 0x0600DBC7 RID: 56263 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AutoLine(string text)
			: base(text)
		{
		}

		// Token: 0x0600DBC8 RID: 56264 RVA: 0x002BC820 File Offset: 0x002BAA20
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DBC9 RID: 56265 RVA: 0x002BC83B File Offset: 0x002BAA3B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoLine>(deep);
		}

		// Token: 0x04006C7A RID: 27770
		private const string tagName = "AutoLine";

		// Token: 0x04006C7B RID: 27771
		private const byte tagNsId = 29;

		// Token: 0x04006C7C RID: 27772
		internal const int ElementTypeIdConst = 12445;
	}
}
