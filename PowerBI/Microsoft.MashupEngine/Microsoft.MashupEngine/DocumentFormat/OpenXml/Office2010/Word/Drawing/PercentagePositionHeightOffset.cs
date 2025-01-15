using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word.Drawing
{
	// Token: 0x020024E7 RID: 9447
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PercentagePositionHeightOffset : OpenXmlLeafTextElement
	{
		// Token: 0x17005329 RID: 21289
		// (get) Token: 0x06011813 RID: 71699 RVA: 0x002EF2C4 File Offset: 0x002ED4C4
		public override string LocalName
		{
			get
			{
				return "pctPosHOffset";
			}
		}

		// Token: 0x1700532A RID: 21290
		// (get) Token: 0x06011814 RID: 71700 RVA: 0x002EF2CB File Offset: 0x002ED4CB
		internal override byte NamespaceId
		{
			get
			{
				return 51;
			}
		}

		// Token: 0x1700532B RID: 21291
		// (get) Token: 0x06011815 RID: 71701 RVA: 0x002EF2CF File Offset: 0x002ED4CF
		internal override int ElementTypeId
		{
			get
			{
				return 12822;
			}
		}

		// Token: 0x06011816 RID: 71702 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011817 RID: 71703 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PercentagePositionHeightOffset()
		{
		}

		// Token: 0x06011818 RID: 71704 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PercentagePositionHeightOffset(string text)
			: base(text)
		{
		}

		// Token: 0x06011819 RID: 71705 RVA: 0x002EF2D8 File Offset: 0x002ED4D8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x0601181A RID: 71706 RVA: 0x002EF2F3 File Offset: 0x002ED4F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PercentagePositionHeightOffset>(deep);
		}

		// Token: 0x04007B02 RID: 31490
		private const string tagName = "pctPosHOffset";

		// Token: 0x04007B03 RID: 31491
		private const byte tagNsId = 51;

		// Token: 0x04007B04 RID: 31492
		internal const int ElementTypeIdConst = 12822;
	}
}
