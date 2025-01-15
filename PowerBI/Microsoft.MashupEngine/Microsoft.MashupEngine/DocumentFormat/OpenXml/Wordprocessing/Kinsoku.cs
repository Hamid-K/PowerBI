using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D70 RID: 11632
	[GeneratedCode("DomGen", "2.0")]
	internal class Kinsoku : OnOffType
	{
		// Token: 0x170086EC RID: 34540
		// (get) Token: 0x06018C98 RID: 101528 RVA: 0x003285BE File Offset: 0x003267BE
		public override string LocalName
		{
			get
			{
				return "kinsoku";
			}
		}

		// Token: 0x170086ED RID: 34541
		// (get) Token: 0x06018C99 RID: 101529 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086EE RID: 34542
		// (get) Token: 0x06018C9A RID: 101530 RVA: 0x00344A6A File Offset: 0x00342C6A
		internal override int ElementTypeId
		{
			get
			{
				return 11504;
			}
		}

		// Token: 0x06018C9B RID: 101531 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C9D RID: 101533 RVA: 0x00344A71 File Offset: 0x00342C71
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Kinsoku>(deep);
		}

		// Token: 0x0400A4BB RID: 42171
		private const string tagName = "kinsoku";

		// Token: 0x0400A4BC RID: 42172
		private const byte tagNsId = 23;

		// Token: 0x0400A4BD RID: 42173
		internal const int ElementTypeIdConst = 11504;
	}
}
