using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word.Drawing
{
	// Token: 0x020024EB RID: 9451
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PercentageWidth : OpenXmlLeafTextElement
	{
		// Token: 0x17005343 RID: 21315
		// (get) Token: 0x0601184D RID: 71757 RVA: 0x002EF4FE File Offset: 0x002ED6FE
		public override string LocalName
		{
			get
			{
				return "pctWidth";
			}
		}

		// Token: 0x17005344 RID: 21316
		// (get) Token: 0x0601184E RID: 71758 RVA: 0x002EF2CB File Offset: 0x002ED4CB
		internal override byte NamespaceId
		{
			get
			{
				return 51;
			}
		}

		// Token: 0x17005345 RID: 21317
		// (get) Token: 0x0601184F RID: 71759 RVA: 0x002EF505 File Offset: 0x002ED705
		internal override int ElementTypeId
		{
			get
			{
				return 12826;
			}
		}

		// Token: 0x06011850 RID: 71760 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011851 RID: 71761 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PercentageWidth()
		{
		}

		// Token: 0x06011852 RID: 71762 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PercentageWidth(string text)
			: base(text)
		{
		}

		// Token: 0x06011853 RID: 71763 RVA: 0x002EF50C File Offset: 0x002ED70C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06011854 RID: 71764 RVA: 0x002EF527 File Offset: 0x002ED727
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PercentageWidth>(deep);
		}

		// Token: 0x04007B16 RID: 31510
		private const string tagName = "pctWidth";

		// Token: 0x04007B17 RID: 31511
		private const byte tagNsId = 51;

		// Token: 0x04007B18 RID: 31512
		internal const int ElementTypeIdConst = 12826;
	}
}
