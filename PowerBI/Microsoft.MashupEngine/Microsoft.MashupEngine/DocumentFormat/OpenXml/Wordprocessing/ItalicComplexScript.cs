using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D83 RID: 11651
	[GeneratedCode("DomGen", "2.0")]
	internal class ItalicComplexScript : OnOffType
	{
		// Token: 0x17008725 RID: 34597
		// (get) Token: 0x06018D0A RID: 101642 RVA: 0x00344C0A File Offset: 0x00342E0A
		public override string LocalName
		{
			get
			{
				return "iCs";
			}
		}

		// Token: 0x17008726 RID: 34598
		// (get) Token: 0x06018D0B RID: 101643 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008727 RID: 34599
		// (get) Token: 0x06018D0C RID: 101644 RVA: 0x00344C11 File Offset: 0x00342E11
		internal override int ElementTypeId
		{
			get
			{
				return 11580;
			}
		}

		// Token: 0x06018D0D RID: 101645 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D0F RID: 101647 RVA: 0x00344C18 File Offset: 0x00342E18
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ItalicComplexScript>(deep);
		}

		// Token: 0x0400A4F4 RID: 42228
		private const string tagName = "iCs";

		// Token: 0x0400A4F5 RID: 42229
		private const byte tagNsId = 23;

		// Token: 0x0400A4F6 RID: 42230
		internal const int ElementTypeIdConst = 11580;
	}
}
