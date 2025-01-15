using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D5 RID: 9941
	[GeneratedCode("DomGen", "2.0")]
	internal class Shade : PositiveFixedPercentageType
	{
		// Token: 0x17005D9E RID: 23966
		// (get) Token: 0x06012F66 RID: 77670 RVA: 0x002EC997 File Offset: 0x002EAB97
		public override string LocalName
		{
			get
			{
				return "shade";
			}
		}

		// Token: 0x17005D9F RID: 23967
		// (get) Token: 0x06012F67 RID: 77671 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DA0 RID: 23968
		// (get) Token: 0x06012F68 RID: 77672 RVA: 0x0030163B File Offset: 0x002FF83B
		internal override int ElementTypeId
		{
			get
			{
				return 10007;
			}
		}

		// Token: 0x06012F69 RID: 77673 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F6B RID: 77675 RVA: 0x00301642 File Offset: 0x002FF842
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shade>(deep);
		}

		// Token: 0x040083E5 RID: 33765
		private const string tagName = "shade";

		// Token: 0x040083E6 RID: 33766
		private const byte tagNsId = 10;

		// Token: 0x040083E7 RID: 33767
		internal const int ElementTypeIdConst = 10007;
	}
}
