using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C6 RID: 10182
	[GeneratedCode("DomGen", "2.0")]
	internal class Position : AdjustPoint2DType
	{
		// Token: 0x17006383 RID: 25475
		// (get) Token: 0x06013C79 RID: 81017 RVA: 0x0030BA47 File Offset: 0x00309C47
		public override string LocalName
		{
			get
			{
				return "pos";
			}
		}

		// Token: 0x17006384 RID: 25476
		// (get) Token: 0x06013C7A RID: 81018 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006385 RID: 25477
		// (get) Token: 0x06013C7B RID: 81019 RVA: 0x0030BA4E File Offset: 0x00309C4E
		internal override int ElementTypeId
		{
			get
			{
				return 10216;
			}
		}

		// Token: 0x06013C7C RID: 81020 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C7E RID: 81022 RVA: 0x0030BA5D File Offset: 0x00309C5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Position>(deep);
		}

		// Token: 0x040087BE RID: 34750
		private const string tagName = "pos";

		// Token: 0x040087BF RID: 34751
		private const byte tagNsId = 10;

		// Token: 0x040087C0 RID: 34752
		internal const int ElementTypeIdConst = 10216;
	}
}
