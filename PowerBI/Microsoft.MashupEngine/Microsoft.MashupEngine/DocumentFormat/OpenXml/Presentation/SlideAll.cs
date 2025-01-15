using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E1 RID: 10721
	[GeneratedCode("DomGen", "2.0")]
	internal class SlideAll : EmptyType
	{
		// Token: 0x17006E3C RID: 28220
		// (get) Token: 0x06015567 RID: 87399 RVA: 0x0031E14C File Offset: 0x0031C34C
		public override string LocalName
		{
			get
			{
				return "sldAll";
			}
		}

		// Token: 0x17006E3D RID: 28221
		// (get) Token: 0x06015568 RID: 87400 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E3E RID: 28222
		// (get) Token: 0x06015569 RID: 87401 RVA: 0x0031E153 File Offset: 0x0031C353
		internal override int ElementTypeId
		{
			get
			{
				return 12162;
			}
		}

		// Token: 0x0601556A RID: 87402 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601556C RID: 87404 RVA: 0x0031E162 File Offset: 0x0031C362
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideAll>(deep);
		}

		// Token: 0x040092FD RID: 37629
		private const string tagName = "sldAll";

		// Token: 0x040092FE RID: 37630
		private const byte tagNsId = 24;

		// Token: 0x040092FF RID: 37631
		internal const int ElementTypeIdConst = 12162;
	}
}
