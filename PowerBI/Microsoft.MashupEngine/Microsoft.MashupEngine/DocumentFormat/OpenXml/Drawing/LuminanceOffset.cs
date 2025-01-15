using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E5 RID: 9957
	[GeneratedCode("DomGen", "2.0")]
	internal class LuminanceOffset : PercentageType
	{
		// Token: 0x17005DD7 RID: 24023
		// (get) Token: 0x06012FDA RID: 77786 RVA: 0x002ECAC8 File Offset: 0x002EACC8
		public override string LocalName
		{
			get
			{
				return "lumOff";
			}
		}

		// Token: 0x17005DD8 RID: 24024
		// (get) Token: 0x06012FDB RID: 77787 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DD9 RID: 24025
		// (get) Token: 0x06012FDC RID: 77788 RVA: 0x00301897 File Offset: 0x002FFA97
		internal override int ElementTypeId
		{
			get
			{
				return 10021;
			}
		}

		// Token: 0x06012FDD RID: 77789 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FDF RID: 77791 RVA: 0x0030189E File Offset: 0x002FFA9E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LuminanceOffset>(deep);
		}

		// Token: 0x04008419 RID: 33817
		private const string tagName = "lumOff";

		// Token: 0x0400841A RID: 33818
		private const byte tagNsId = 10;

		// Token: 0x0400841B RID: 33819
		internal const int ElementTypeIdConst = 10021;
	}
}
