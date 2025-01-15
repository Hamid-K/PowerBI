using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D6 RID: 9942
	[GeneratedCode("DomGen", "2.0")]
	internal class Alpha : PositiveFixedPercentageType
	{
		// Token: 0x17005DA1 RID: 23969
		// (get) Token: 0x06012F6C RID: 77676 RVA: 0x002EC9AE File Offset: 0x002EABAE
		public override string LocalName
		{
			get
			{
				return "alpha";
			}
		}

		// Token: 0x17005DA2 RID: 23970
		// (get) Token: 0x06012F6D RID: 77677 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DA3 RID: 23971
		// (get) Token: 0x06012F6E RID: 77678 RVA: 0x0030164B File Offset: 0x002FF84B
		internal override int ElementTypeId
		{
			get
			{
				return 10011;
			}
		}

		// Token: 0x06012F6F RID: 77679 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F71 RID: 77681 RVA: 0x00301652 File Offset: 0x002FF852
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Alpha>(deep);
		}

		// Token: 0x040083E8 RID: 33768
		private const string tagName = "alpha";

		// Token: 0x040083E9 RID: 33769
		private const byte tagNsId = 10;

		// Token: 0x040083EA RID: 33770
		internal const int ElementTypeIdConst = 10011;
	}
}
