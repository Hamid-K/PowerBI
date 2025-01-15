using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200299B RID: 10651
	[GeneratedCode("DomGen", "2.0")]
	internal class Position : TopBottomType
	{
		// Token: 0x17006CED RID: 27885
		// (get) Token: 0x0601528F RID: 86671 RVA: 0x0030BA47 File Offset: 0x00309C47
		public override string LocalName
		{
			get
			{
				return "pos";
			}
		}

		// Token: 0x17006CEE RID: 27886
		// (get) Token: 0x06015290 RID: 86672 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CEF RID: 27887
		// (get) Token: 0x06015291 RID: 86673 RVA: 0x0031C468 File Offset: 0x0031A668
		internal override int ElementTypeId
		{
			get
			{
				return 10874;
			}
		}

		// Token: 0x06015292 RID: 86674 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015294 RID: 86676 RVA: 0x0031C477 File Offset: 0x0031A677
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Position>(deep);
		}

		// Token: 0x040091E2 RID: 37346
		private const string tagName = "pos";

		// Token: 0x040091E3 RID: 37347
		private const byte tagNsId = 21;

		// Token: 0x040091E4 RID: 37348
		internal const int ElementTypeIdConst = 10874;
	}
}
