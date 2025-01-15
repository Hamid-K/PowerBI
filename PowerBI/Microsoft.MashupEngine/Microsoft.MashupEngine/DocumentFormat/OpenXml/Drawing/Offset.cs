using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200278C RID: 10124
	[GeneratedCode("DomGen", "2.0")]
	internal class Offset : Point2DType
	{
		// Token: 0x170061DC RID: 25052
		// (get) Token: 0x060138F0 RID: 80112 RVA: 0x00308473 File Offset: 0x00306673
		public override string LocalName
		{
			get
			{
				return "off";
			}
		}

		// Token: 0x170061DD RID: 25053
		// (get) Token: 0x060138F1 RID: 80113 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061DE RID: 25054
		// (get) Token: 0x060138F2 RID: 80114 RVA: 0x0030847A File Offset: 0x0030667A
		internal override int ElementTypeId
		{
			get
			{
				return 10161;
			}
		}

		// Token: 0x060138F3 RID: 80115 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138F5 RID: 80117 RVA: 0x00308489 File Offset: 0x00306689
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Offset>(deep);
		}

		// Token: 0x040086BF RID: 34495
		private const string tagName = "off";

		// Token: 0x040086C0 RID: 34496
		private const byte tagNsId = 10;

		// Token: 0x040086C1 RID: 34497
		internal const int ElementTypeIdConst = 10161;
	}
}
