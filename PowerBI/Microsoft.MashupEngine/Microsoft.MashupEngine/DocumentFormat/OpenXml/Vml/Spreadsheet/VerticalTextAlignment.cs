using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021DE RID: 8670
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalTextAlignment : OpenXmlLeafTextElement
	{
		// Token: 0x170037A7 RID: 14247
		// (get) Token: 0x0600DCA2 RID: 56482 RVA: 0x002BCDC0 File Offset: 0x002BAFC0
		public override string LocalName
		{
			get
			{
				return "TextVAlign";
			}
		}

		// Token: 0x170037A8 RID: 14248
		// (get) Token: 0x0600DCA3 RID: 56483 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037A9 RID: 14249
		// (get) Token: 0x0600DCA4 RID: 56484 RVA: 0x002BCDC7 File Offset: 0x002BAFC7
		internal override int ElementTypeId
		{
			get
			{
				return 12449;
			}
		}

		// Token: 0x0600DCA5 RID: 56485 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCA6 RID: 56486 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VerticalTextAlignment()
		{
		}

		// Token: 0x0600DCA7 RID: 56487 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VerticalTextAlignment(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCA8 RID: 56488 RVA: 0x002BCDD0 File Offset: 0x002BAFD0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCA9 RID: 56489 RVA: 0x002BCDEB File Offset: 0x002BAFEB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalTextAlignment>(deep);
		}

		// Token: 0x04006CCE RID: 27854
		private const string tagName = "TextVAlign";

		// Token: 0x04006CCF RID: 27855
		private const byte tagNsId = 29;

		// Token: 0x04006CD0 RID: 27856
		internal const int ElementTypeIdConst = 12449;
	}
}
