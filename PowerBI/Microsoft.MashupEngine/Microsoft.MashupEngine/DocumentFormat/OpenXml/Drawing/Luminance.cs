using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E4 RID: 9956
	[GeneratedCode("DomGen", "2.0")]
	internal class Luminance : PercentageType
	{
		// Token: 0x17005DD4 RID: 24020
		// (get) Token: 0x06012FD4 RID: 77780 RVA: 0x002ECAB1 File Offset: 0x002EACB1
		public override string LocalName
		{
			get
			{
				return "lum";
			}
		}

		// Token: 0x17005DD5 RID: 24021
		// (get) Token: 0x06012FD5 RID: 77781 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DD6 RID: 24022
		// (get) Token: 0x06012FD6 RID: 77782 RVA: 0x00301887 File Offset: 0x002FFA87
		internal override int ElementTypeId
		{
			get
			{
				return 10020;
			}
		}

		// Token: 0x06012FD7 RID: 77783 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FD9 RID: 77785 RVA: 0x0030188E File Offset: 0x002FFA8E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Luminance>(deep);
		}

		// Token: 0x04008416 RID: 33814
		private const string tagName = "lum";

		// Token: 0x04008417 RID: 33815
		private const byte tagNsId = 10;

		// Token: 0x04008418 RID: 33816
		internal const int ElementTypeIdConst = 10020;
	}
}
