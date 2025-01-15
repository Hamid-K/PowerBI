using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E2 RID: 10722
	[GeneratedCode("DomGen", "2.0")]
	internal class PresenterSlideMode : EmptyType
	{
		// Token: 0x17006E3F RID: 28223
		// (get) Token: 0x0601556D RID: 87405 RVA: 0x0031E16B File Offset: 0x0031C36B
		public override string LocalName
		{
			get
			{
				return "present";
			}
		}

		// Token: 0x17006E40 RID: 28224
		// (get) Token: 0x0601556E RID: 87406 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E41 RID: 28225
		// (get) Token: 0x0601556F RID: 87407 RVA: 0x0031E172 File Offset: 0x0031C372
		internal override int ElementTypeId
		{
			get
			{
				return 12166;
			}
		}

		// Token: 0x06015570 RID: 87408 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015572 RID: 87410 RVA: 0x0031E179 File Offset: 0x0031C379
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresenterSlideMode>(deep);
		}

		// Token: 0x04009300 RID: 37632
		private const string tagName = "present";

		// Token: 0x04009301 RID: 37633
		private const byte tagNsId = 24;

		// Token: 0x04009302 RID: 37634
		internal const int ElementTypeIdConst = 12166;
	}
}
