using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D4 RID: 9940
	[GeneratedCode("DomGen", "2.0")]
	internal class Tint : PositiveFixedPercentageType
	{
		// Token: 0x17005D9B RID: 23963
		// (get) Token: 0x06012F60 RID: 77664 RVA: 0x002EC978 File Offset: 0x002EAB78
		public override string LocalName
		{
			get
			{
				return "tint";
			}
		}

		// Token: 0x17005D9C RID: 23964
		// (get) Token: 0x06012F61 RID: 77665 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005D9D RID: 23965
		// (get) Token: 0x06012F62 RID: 77666 RVA: 0x00301623 File Offset: 0x002FF823
		internal override int ElementTypeId
		{
			get
			{
				return 10006;
			}
		}

		// Token: 0x06012F63 RID: 77667 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F65 RID: 77669 RVA: 0x00301632 File Offset: 0x002FF832
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tint>(deep);
		}

		// Token: 0x040083E2 RID: 33762
		private const string tagName = "tint";

		// Token: 0x040083E3 RID: 33763
		private const byte tagNsId = 10;

		// Token: 0x040083E4 RID: 33764
		internal const int ElementTypeIdConst = 10006;
	}
}
