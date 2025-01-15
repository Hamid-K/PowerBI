using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E2 RID: 9954
	[GeneratedCode("DomGen", "2.0")]
	internal class SaturationOffset : PercentageType
	{
		// Token: 0x17005DCE RID: 24014
		// (get) Token: 0x06012FC8 RID: 77768 RVA: 0x002ECA83 File Offset: 0x002EAC83
		public override string LocalName
		{
			get
			{
				return "satOff";
			}
		}

		// Token: 0x17005DCF RID: 24015
		// (get) Token: 0x06012FC9 RID: 77769 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DD0 RID: 24016
		// (get) Token: 0x06012FCA RID: 77770 RVA: 0x00301867 File Offset: 0x002FFA67
		internal override int ElementTypeId
		{
			get
			{
				return 10018;
			}
		}

		// Token: 0x06012FCB RID: 77771 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FCD RID: 77773 RVA: 0x0030186E File Offset: 0x002FFA6E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaturationOffset>(deep);
		}

		// Token: 0x04008410 RID: 33808
		private const string tagName = "satOff";

		// Token: 0x04008411 RID: 33809
		private const byte tagNsId = 10;

		// Token: 0x04008412 RID: 33810
		internal const int ElementTypeIdConst = 10018;
	}
}
