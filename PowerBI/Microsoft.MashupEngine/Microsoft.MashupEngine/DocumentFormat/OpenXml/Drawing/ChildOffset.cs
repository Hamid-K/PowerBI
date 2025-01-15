using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200278D RID: 10125
	[GeneratedCode("DomGen", "2.0")]
	internal class ChildOffset : Point2DType
	{
		// Token: 0x170061DF RID: 25055
		// (get) Token: 0x060138F6 RID: 80118 RVA: 0x00308492 File Offset: 0x00306692
		public override string LocalName
		{
			get
			{
				return "chOff";
			}
		}

		// Token: 0x170061E0 RID: 25056
		// (get) Token: 0x060138F7 RID: 80119 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061E1 RID: 25057
		// (get) Token: 0x060138F8 RID: 80120 RVA: 0x00308499 File Offset: 0x00306699
		internal override int ElementTypeId
		{
			get
			{
				return 10163;
			}
		}

		// Token: 0x060138F9 RID: 80121 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138FB RID: 80123 RVA: 0x003084A0 File Offset: 0x003066A0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChildOffset>(deep);
		}

		// Token: 0x040086C2 RID: 34498
		private const string tagName = "chOff";

		// Token: 0x040086C3 RID: 34499
		private const byte tagNsId = 10;

		// Token: 0x040086C4 RID: 34500
		internal const int ElementTypeIdConst = 10163;
	}
}
