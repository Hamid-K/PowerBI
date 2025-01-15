using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C7 RID: 10183
	[GeneratedCode("DomGen", "2.0")]
	internal class Point : AdjustPoint2DType
	{
		// Token: 0x17006386 RID: 25478
		// (get) Token: 0x06013C7F RID: 81023 RVA: 0x002F359C File Offset: 0x002F179C
		public override string LocalName
		{
			get
			{
				return "pt";
			}
		}

		// Token: 0x17006387 RID: 25479
		// (get) Token: 0x06013C80 RID: 81024 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006388 RID: 25480
		// (get) Token: 0x06013C81 RID: 81025 RVA: 0x0030BA66 File Offset: 0x00309C66
		internal override int ElementTypeId
		{
			get
			{
				return 10220;
			}
		}

		// Token: 0x06013C82 RID: 81026 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013C84 RID: 81028 RVA: 0x0030BA6D File Offset: 0x00309C6D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Point>(deep);
		}

		// Token: 0x040087C1 RID: 34753
		private const string tagName = "pt";

		// Token: 0x040087C2 RID: 34754
		private const byte tagNsId = 10;

		// Token: 0x040087C3 RID: 34755
		internal const int ElementTypeIdConst = 10220;
	}
}
