using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F1 RID: 10737
	[GeneratedCode("DomGen", "2.0")]
	internal class ParagraphIndexRange : IndexRangeType
	{
		// Token: 0x17006E6D RID: 28269
		// (get) Token: 0x060155CA RID: 87498 RVA: 0x0031E325 File Offset: 0x0031C525
		public override string LocalName
		{
			get
			{
				return "pRg";
			}
		}

		// Token: 0x17006E6E RID: 28270
		// (get) Token: 0x060155CB RID: 87499 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E6F RID: 28271
		// (get) Token: 0x060155CC RID: 87500 RVA: 0x0031E32C File Offset: 0x0031C52C
		internal override int ElementTypeId
		{
			get
			{
				return 12194;
			}
		}

		// Token: 0x060155CD RID: 87501 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060155CF RID: 87503 RVA: 0x0031E333 File Offset: 0x0031C533
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphIndexRange>(deep);
		}

		// Token: 0x0400932C RID: 37676
		private const string tagName = "pRg";

		// Token: 0x0400932D RID: 37677
		private const byte tagNsId = 24;

		// Token: 0x0400932E RID: 37678
		internal const int ElementTypeIdConst = 12194;
	}
}
