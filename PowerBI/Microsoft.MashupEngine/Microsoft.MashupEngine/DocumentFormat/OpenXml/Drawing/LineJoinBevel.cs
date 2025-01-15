using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200272A RID: 10026
	[GeneratedCode("DomGen", "2.0")]
	internal class LineJoinBevel : OpenXmlLeafElement
	{
		// Token: 0x17005FE6 RID: 24550
		// (get) Token: 0x06013441 RID: 78913 RVA: 0x002ECFCD File Offset: 0x002EB1CD
		public override string LocalName
		{
			get
			{
				return "bevel";
			}
		}

		// Token: 0x17005FE7 RID: 24551
		// (get) Token: 0x06013442 RID: 78914 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FE8 RID: 24552
		// (get) Token: 0x06013443 RID: 78915 RVA: 0x00305956 File Offset: 0x00303B56
		internal override int ElementTypeId
		{
			get
			{
				return 10089;
			}
		}

		// Token: 0x06013444 RID: 78916 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013446 RID: 78918 RVA: 0x0030595D File Offset: 0x00303B5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineJoinBevel>(deep);
		}

		// Token: 0x0400855F RID: 34143
		private const string tagName = "bevel";

		// Token: 0x04008560 RID: 34144
		private const byte tagNsId = 10;

		// Token: 0x04008561 RID: 34145
		internal const int ElementTypeIdConst = 10089;
	}
}
