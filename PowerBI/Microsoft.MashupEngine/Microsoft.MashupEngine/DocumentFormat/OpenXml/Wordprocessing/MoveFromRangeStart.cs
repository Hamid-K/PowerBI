using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D48 RID: 11592
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveFromRangeStart : MoveBookmarkType
	{
		// Token: 0x17008674 RID: 34420
		// (get) Token: 0x06018BA5 RID: 101285 RVA: 0x00344614 File Offset: 0x00342814
		public override string LocalName
		{
			get
			{
				return "moveFromRangeStart";
			}
		}

		// Token: 0x17008675 RID: 34421
		// (get) Token: 0x06018BA6 RID: 101286 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008676 RID: 34422
		// (get) Token: 0x06018BA7 RID: 101287 RVA: 0x0034461B File Offset: 0x0034281B
		internal override int ElementTypeId
		{
			get
			{
				return 11480;
			}
		}

		// Token: 0x06018BA8 RID: 101288 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BAA RID: 101290 RVA: 0x0034462A File Offset: 0x0034282A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveFromRangeStart>(deep);
		}

		// Token: 0x0400A446 RID: 42054
		private const string tagName = "moveFromRangeStart";

		// Token: 0x0400A447 RID: 42055
		private const byte tagNsId = 23;

		// Token: 0x0400A448 RID: 42056
		internal const int ElementTypeIdConst = 11480;
	}
}
