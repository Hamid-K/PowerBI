using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002790 RID: 10128
	[GeneratedCode("DomGen", "2.0")]
	internal class ChildExtents : PositiveSize2DType
	{
		// Token: 0x170061E9 RID: 25065
		// (get) Token: 0x0601390B RID: 80139 RVA: 0x00308507 File Offset: 0x00306707
		public override string LocalName
		{
			get
			{
				return "chExt";
			}
		}

		// Token: 0x170061EA RID: 25066
		// (get) Token: 0x0601390C RID: 80140 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061EB RID: 25067
		// (get) Token: 0x0601390D RID: 80141 RVA: 0x0030850E File Offset: 0x0030670E
		internal override int ElementTypeId
		{
			get
			{
				return 10164;
			}
		}

		// Token: 0x0601390E RID: 80142 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013910 RID: 80144 RVA: 0x00308515 File Offset: 0x00306715
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChildExtents>(deep);
		}

		// Token: 0x040086CA RID: 34506
		private const string tagName = "chExt";

		// Token: 0x040086CB RID: 34507
		private const byte tagNsId = 10;

		// Token: 0x040086CC RID: 34508
		internal const int ElementTypeIdConst = 10164;
	}
}
