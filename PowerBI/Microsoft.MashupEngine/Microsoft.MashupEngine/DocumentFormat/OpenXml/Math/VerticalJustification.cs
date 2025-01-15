using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200299C RID: 10652
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalJustification : TopBottomType
	{
		// Token: 0x17006CF0 RID: 27888
		// (get) Token: 0x06015295 RID: 86677 RVA: 0x0031C480 File Offset: 0x0031A680
		public override string LocalName
		{
			get
			{
				return "vertJc";
			}
		}

		// Token: 0x17006CF1 RID: 27889
		// (get) Token: 0x06015296 RID: 86678 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CF2 RID: 27890
		// (get) Token: 0x06015297 RID: 86679 RVA: 0x0031C487 File Offset: 0x0031A687
		internal override int ElementTypeId
		{
			get
			{
				return 10907;
			}
		}

		// Token: 0x06015298 RID: 86680 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601529A RID: 86682 RVA: 0x0031C48E File Offset: 0x0031A68E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalJustification>(deep);
		}

		// Token: 0x040091E5 RID: 37349
		private const string tagName = "vertJc";

		// Token: 0x040091E6 RID: 37350
		private const byte tagNsId = 21;

		// Token: 0x040091E7 RID: 37351
		internal const int ElementTypeIdConst = 10907;
	}
}
