using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200296B RID: 10603
	[GeneratedCode("DomGen", "2.0")]
	internal class Alignment : OnOffType
	{
		// Token: 0x17006C4C RID: 27724
		// (get) Token: 0x06015126 RID: 86310 RVA: 0x0031B402 File Offset: 0x00319602
		public override string LocalName
		{
			get
			{
				return "aln";
			}
		}

		// Token: 0x17006C4D RID: 27725
		// (get) Token: 0x06015127 RID: 86311 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C4E RID: 27726
		// (get) Token: 0x06015128 RID: 86312 RVA: 0x0031B409 File Offset: 0x00319609
		internal override int ElementTypeId
		{
			get
			{
				return 10867;
			}
		}

		// Token: 0x06015129 RID: 86313 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601512B RID: 86315 RVA: 0x0031B410 File Offset: 0x00319610
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Alignment>(deep);
		}

		// Token: 0x0400914D RID: 37197
		private const string tagName = "aln";

		// Token: 0x0400914E RID: 37198
		private const byte tagNsId = 21;

		// Token: 0x0400914F RID: 37199
		internal const int ElementTypeIdConst = 10867;
	}
}
