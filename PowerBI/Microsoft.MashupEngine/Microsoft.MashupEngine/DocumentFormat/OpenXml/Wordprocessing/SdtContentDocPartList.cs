using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x0200300B RID: 12299
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentDocPartList : SdtDocPartType
	{
		// Token: 0x1700965B RID: 38491
		// (get) Token: 0x0601AD87 RID: 109959 RVA: 0x0036866A File Offset: 0x0036686A
		public override string LocalName
		{
			get
			{
				return "docPartList";
			}
		}

		// Token: 0x1700965C RID: 38492
		// (get) Token: 0x0601AD88 RID: 109960 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700965D RID: 38493
		// (get) Token: 0x0601AD89 RID: 109961 RVA: 0x00368671 File Offset: 0x00366871
		internal override int ElementTypeId
		{
			get
			{
				return 12151;
			}
		}

		// Token: 0x0601AD8A RID: 109962 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AD8B RID: 109963 RVA: 0x0036863E File Offset: 0x0036683E
		public SdtContentDocPartList()
		{
		}

		// Token: 0x0601AD8C RID: 109964 RVA: 0x00368646 File Offset: 0x00366846
		public SdtContentDocPartList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD8D RID: 109965 RVA: 0x0036864F File Offset: 0x0036684F
		public SdtContentDocPartList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD8E RID: 109966 RVA: 0x00368658 File Offset: 0x00366858
		public SdtContentDocPartList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AD8F RID: 109967 RVA: 0x00368678 File Offset: 0x00366878
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentDocPartList>(deep);
		}

		// Token: 0x0400AE81 RID: 44673
		private const string tagName = "docPartList";

		// Token: 0x0400AE82 RID: 44674
		private const byte tagNsId = 23;

		// Token: 0x0400AE83 RID: 44675
		internal const int ElementTypeIdConst = 12151;
	}
}
