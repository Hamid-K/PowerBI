using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D6E RID: 11630
	[GeneratedCode("DomGen", "2.0")]
	internal class SuppressLineNumbers : OnOffType
	{
		// Token: 0x170086E6 RID: 34534
		// (get) Token: 0x06018C8C RID: 101516 RVA: 0x00344A3C File Offset: 0x00342C3C
		public override string LocalName
		{
			get
			{
				return "suppressLineNumbers";
			}
		}

		// Token: 0x170086E7 RID: 34535
		// (get) Token: 0x06018C8D RID: 101517 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086E8 RID: 34536
		// (get) Token: 0x06018C8E RID: 101518 RVA: 0x00344A43 File Offset: 0x00342C43
		internal override int ElementTypeId
		{
			get
			{
				return 11499;
			}
		}

		// Token: 0x06018C8F RID: 101519 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C91 RID: 101521 RVA: 0x00344A4A File Offset: 0x00342C4A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuppressLineNumbers>(deep);
		}

		// Token: 0x0400A4B5 RID: 42165
		private const string tagName = "suppressLineNumbers";

		// Token: 0x0400A4B6 RID: 42166
		private const byte tagNsId = 23;

		// Token: 0x0400A4B7 RID: 42167
		internal const int ElementTypeIdConst = 11499;
	}
}
