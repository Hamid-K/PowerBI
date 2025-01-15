using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027BA RID: 10170
	[GeneratedCode("DomGen", "2.0")]
	internal class BevelTop : BevelType
	{
		// Token: 0x1700634B RID: 25419
		// (get) Token: 0x06013C02 RID: 80898 RVA: 0x002EED48 File Offset: 0x002ECF48
		public override string LocalName
		{
			get
			{
				return "bevelT";
			}
		}

		// Token: 0x1700634C RID: 25420
		// (get) Token: 0x06013C03 RID: 80899 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700634D RID: 25421
		// (get) Token: 0x06013C04 RID: 80900 RVA: 0x0030B63F File Offset: 0x0030983F
		internal override int ElementTypeId
		{
			get
			{
				return 10201;
			}
		}

		// Token: 0x06013C05 RID: 80901 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C07 RID: 80903 RVA: 0x0030B64E File Offset: 0x0030984E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BevelTop>(deep);
		}

		// Token: 0x04008796 RID: 34710
		private const string tagName = "bevelT";

		// Token: 0x04008797 RID: 34711
		private const byte tagNsId = 10;

		// Token: 0x04008798 RID: 34712
		internal const int ElementTypeIdConst = 10201;
	}
}
