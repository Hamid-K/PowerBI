using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E5 RID: 10725
	[GeneratedCode("DomGen", "2.0")]
	internal class SlideTarget : EmptyType
	{
		// Token: 0x17006E48 RID: 28232
		// (get) Token: 0x0601557F RID: 87423 RVA: 0x0031E1B0 File Offset: 0x0031C3B0
		public override string LocalName
		{
			get
			{
				return "sldTgt";
			}
		}

		// Token: 0x17006E49 RID: 28233
		// (get) Token: 0x06015580 RID: 87424 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E4A RID: 28234
		// (get) Token: 0x06015581 RID: 87425 RVA: 0x0031E1B7 File Offset: 0x0031C3B7
		internal override int ElementTypeId
		{
			get
			{
				return 12366;
			}
		}

		// Token: 0x06015582 RID: 87426 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015584 RID: 87428 RVA: 0x0031E1BE File Offset: 0x0031C3BE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideTarget>(deep);
		}

		// Token: 0x04009309 RID: 37641
		private const string tagName = "sldTgt";

		// Token: 0x0400930A RID: 37642
		private const byte tagNsId = 24;

		// Token: 0x0400930B RID: 37643
		internal const int ElementTypeIdConst = 12366;
	}
}
