using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF7 RID: 11767
	[GeneratedCode("DomGen", "2.0")]
	internal class SuppressSpacingBeforeAfterPageBreak : OnOffType
	{
		// Token: 0x17008881 RID: 34945
		// (get) Token: 0x06018FC2 RID: 102338 RVA: 0x0034563E File Offset: 0x0034383E
		public override string LocalName
		{
			get
			{
				return "suppressSpBfAfterPgBrk";
			}
		}

		// Token: 0x17008882 RID: 34946
		// (get) Token: 0x06018FC3 RID: 102339 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008883 RID: 34947
		// (get) Token: 0x06018FC4 RID: 102340 RVA: 0x00345645 File Offset: 0x00343845
		internal override int ElementTypeId
		{
			get
			{
				return 12077;
			}
		}

		// Token: 0x06018FC5 RID: 102341 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FC7 RID: 102343 RVA: 0x0034564C File Offset: 0x0034384C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuppressSpacingBeforeAfterPageBreak>(deep);
		}

		// Token: 0x0400A650 RID: 42576
		private const string tagName = "suppressSpBfAfterPgBrk";

		// Token: 0x0400A651 RID: 42577
		private const byte tagNsId = 23;

		// Token: 0x0400A652 RID: 42578
		internal const int ElementTypeIdConst = 12077;
	}
}
