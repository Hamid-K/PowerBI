using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029EB RID: 10731
	[GeneratedCode("DomGen", "2.0")]
	internal class PlusTransition : EmptyType
	{
		// Token: 0x17006E5A RID: 28250
		// (get) Token: 0x060155A3 RID: 87459 RVA: 0x002F4DB1 File Offset: 0x002F2FB1
		public override string LocalName
		{
			get
			{
				return "plus";
			}
		}

		// Token: 0x17006E5B RID: 28251
		// (get) Token: 0x060155A4 RID: 87460 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E5C RID: 28252
		// (get) Token: 0x060155A5 RID: 87461 RVA: 0x0031E233 File Offset: 0x0031C433
		internal override int ElementTypeId
		{
			get
			{
				return 12385;
			}
		}

		// Token: 0x060155A6 RID: 87462 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060155A8 RID: 87464 RVA: 0x0031E23A File Offset: 0x0031C43A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PlusTransition>(deep);
		}

		// Token: 0x0400931B RID: 37659
		private const string tagName = "plus";

		// Token: 0x0400931C RID: 37660
		private const byte tagNsId = 24;

		// Token: 0x0400931D RID: 37661
		internal const int ElementTypeIdConst = 12385;
	}
}
