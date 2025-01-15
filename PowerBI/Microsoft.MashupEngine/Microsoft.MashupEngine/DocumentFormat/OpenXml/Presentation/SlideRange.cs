using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029EF RID: 10735
	[GeneratedCode("DomGen", "2.0")]
	internal class SlideRange : IndexRangeType
	{
		// Token: 0x17006E67 RID: 28263
		// (get) Token: 0x060155BE RID: 87486 RVA: 0x0031E2EF File Offset: 0x0031C4EF
		public override string LocalName
		{
			get
			{
				return "sldRg";
			}
		}

		// Token: 0x17006E68 RID: 28264
		// (get) Token: 0x060155BF RID: 87487 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E69 RID: 28265
		// (get) Token: 0x060155C0 RID: 87488 RVA: 0x0031E2F6 File Offset: 0x0031C4F6
		internal override int ElementTypeId
		{
			get
			{
				return 12163;
			}
		}

		// Token: 0x060155C1 RID: 87489 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060155C3 RID: 87491 RVA: 0x0031E305 File Offset: 0x0031C505
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideRange>(deep);
		}

		// Token: 0x04009326 RID: 37670
		private const string tagName = "sldRg";

		// Token: 0x04009327 RID: 37671
		private const byte tagNsId = 24;

		// Token: 0x04009328 RID: 37672
		internal const int ElementTypeIdConst = 12163;
	}
}
