using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D51 RID: 11601
	[GeneratedCode("DomGen", "2.0")]
	internal class ParagraphStyleId : StringType
	{
		// Token: 0x1700868F RID: 34447
		// (get) Token: 0x06018BDD RID: 101341 RVA: 0x0034476C File Offset: 0x0034296C
		public override string LocalName
		{
			get
			{
				return "pStyle";
			}
		}

		// Token: 0x17008690 RID: 34448
		// (get) Token: 0x06018BDE RID: 101342 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008691 RID: 34449
		// (get) Token: 0x06018BDF RID: 101343 RVA: 0x00344773 File Offset: 0x00342973
		internal override int ElementTypeId
		{
			get
			{
				return 11492;
			}
		}

		// Token: 0x06018BE0 RID: 101344 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BE2 RID: 101346 RVA: 0x00344782 File Offset: 0x00342982
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphStyleId>(deep);
		}

		// Token: 0x0400A45F RID: 42079
		private const string tagName = "pStyle";

		// Token: 0x0400A460 RID: 42080
		private const byte tagNsId = 23;

		// Token: 0x0400A461 RID: 42081
		internal const int ElementTypeIdConst = 11492;
	}
}
