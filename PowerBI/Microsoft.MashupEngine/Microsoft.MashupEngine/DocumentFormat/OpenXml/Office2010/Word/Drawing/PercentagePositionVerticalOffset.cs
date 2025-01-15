using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word.Drawing
{
	// Token: 0x020024E8 RID: 9448
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PercentagePositionVerticalOffset : OpenXmlLeafTextElement
	{
		// Token: 0x1700532C RID: 21292
		// (get) Token: 0x0601181B RID: 71707 RVA: 0x002EF2FC File Offset: 0x002ED4FC
		public override string LocalName
		{
			get
			{
				return "pctPosVOffset";
			}
		}

		// Token: 0x1700532D RID: 21293
		// (get) Token: 0x0601181C RID: 71708 RVA: 0x002EF2CB File Offset: 0x002ED4CB
		internal override byte NamespaceId
		{
			get
			{
				return 51;
			}
		}

		// Token: 0x1700532E RID: 21294
		// (get) Token: 0x0601181D RID: 71709 RVA: 0x002EF303 File Offset: 0x002ED503
		internal override int ElementTypeId
		{
			get
			{
				return 12823;
			}
		}

		// Token: 0x0601181E RID: 71710 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601181F RID: 71711 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PercentagePositionVerticalOffset()
		{
		}

		// Token: 0x06011820 RID: 71712 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PercentagePositionVerticalOffset(string text)
			: base(text)
		{
		}

		// Token: 0x06011821 RID: 71713 RVA: 0x002EF30C File Offset: 0x002ED50C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06011822 RID: 71714 RVA: 0x002EF327 File Offset: 0x002ED527
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PercentagePositionVerticalOffset>(deep);
		}

		// Token: 0x04007B05 RID: 31493
		private const string tagName = "pctPosVOffset";

		// Token: 0x04007B06 RID: 31494
		private const byte tagNsId = 51;

		// Token: 0x04007B07 RID: 31495
		internal const int ElementTypeIdConst = 12823;
	}
}
