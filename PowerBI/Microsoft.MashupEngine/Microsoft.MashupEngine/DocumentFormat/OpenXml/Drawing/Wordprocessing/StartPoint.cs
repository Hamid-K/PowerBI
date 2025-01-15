using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A4 RID: 10404
	[GeneratedCode("DomGen", "2.0")]
	internal class StartPoint : Point2DType
	{
		// Token: 0x17006869 RID: 26729
		// (get) Token: 0x060147CF RID: 83919 RVA: 0x00313F27 File Offset: 0x00312127
		public override string LocalName
		{
			get
			{
				return "start";
			}
		}

		// Token: 0x1700686A RID: 26730
		// (get) Token: 0x060147D0 RID: 83920 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700686B RID: 26731
		// (get) Token: 0x060147D1 RID: 83921 RVA: 0x00313F2E File Offset: 0x0031212E
		internal override int ElementTypeId
		{
			get
			{
				return 10701;
			}
		}

		// Token: 0x060147D2 RID: 83922 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060147D4 RID: 83924 RVA: 0x00313F3D File Offset: 0x0031213D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StartPoint>(deep);
		}

		// Token: 0x04008E4C RID: 36428
		private const string tagName = "start";

		// Token: 0x04008E4D RID: 36429
		private const byte tagNsId = 16;

		// Token: 0x04008E4E RID: 36430
		internal const int ElementTypeIdConst = 10701;
	}
}
