using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026E1 RID: 9953
	[GeneratedCode("DomGen", "2.0")]
	internal class Saturation : PercentageType
	{
		// Token: 0x17005DCB RID: 24011
		// (get) Token: 0x06012FC2 RID: 77762 RVA: 0x002ECA64 File Offset: 0x002EAC64
		public override string LocalName
		{
			get
			{
				return "sat";
			}
		}

		// Token: 0x17005DCC RID: 24012
		// (get) Token: 0x06012FC3 RID: 77763 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DCD RID: 24013
		// (get) Token: 0x06012FC4 RID: 77764 RVA: 0x0030184F File Offset: 0x002FFA4F
		internal override int ElementTypeId
		{
			get
			{
				return 10017;
			}
		}

		// Token: 0x06012FC5 RID: 77765 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012FC7 RID: 77767 RVA: 0x0030185E File Offset: 0x002FFA5E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Saturation>(deep);
		}

		// Token: 0x0400840D RID: 33805
		private const string tagName = "sat";

		// Token: 0x0400840E RID: 33806
		private const byte tagNsId = 10;

		// Token: 0x0400840F RID: 33807
		internal const int ElementTypeIdConst = 10017;
	}
}
