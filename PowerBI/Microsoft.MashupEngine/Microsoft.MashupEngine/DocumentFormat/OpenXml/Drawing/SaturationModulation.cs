using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E3 RID: 9955
	[GeneratedCode("DomGen", "2.0")]
	internal class SaturationModulation : PercentageType
	{
		// Token: 0x17005DD1 RID: 24017
		// (get) Token: 0x06012FCE RID: 77774 RVA: 0x002ECA9A File Offset: 0x002EAC9A
		public override string LocalName
		{
			get
			{
				return "satMod";
			}
		}

		// Token: 0x17005DD2 RID: 24018
		// (get) Token: 0x06012FCF RID: 77775 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DD3 RID: 24019
		// (get) Token: 0x06012FD0 RID: 77776 RVA: 0x00301877 File Offset: 0x002FFA77
		internal override int ElementTypeId
		{
			get
			{
				return 10019;
			}
		}

		// Token: 0x06012FD1 RID: 77777 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FD3 RID: 77779 RVA: 0x0030187E File Offset: 0x002FFA7E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SaturationModulation>(deep);
		}

		// Token: 0x04008413 RID: 33811
		private const string tagName = "satMod";

		// Token: 0x04008414 RID: 33812
		private const byte tagNsId = 10;

		// Token: 0x04008415 RID: 33813
		internal const int ElementTypeIdConst = 10019;
	}
}
