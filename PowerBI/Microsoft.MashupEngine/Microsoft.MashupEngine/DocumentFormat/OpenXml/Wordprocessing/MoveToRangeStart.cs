using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D49 RID: 11593
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveToRangeStart : MoveBookmarkType
	{
		// Token: 0x17008677 RID: 34423
		// (get) Token: 0x06018BAB RID: 101291 RVA: 0x00344633 File Offset: 0x00342833
		public override string LocalName
		{
			get
			{
				return "moveToRangeStart";
			}
		}

		// Token: 0x17008678 RID: 34424
		// (get) Token: 0x06018BAC RID: 101292 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008679 RID: 34425
		// (get) Token: 0x06018BAD RID: 101293 RVA: 0x0034463A File Offset: 0x0034283A
		internal override int ElementTypeId
		{
			get
			{
				return 11482;
			}
		}

		// Token: 0x06018BAE RID: 101294 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BB0 RID: 101296 RVA: 0x00344641 File Offset: 0x00342841
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveToRangeStart>(deep);
		}

		// Token: 0x0400A449 RID: 42057
		private const string tagName = "moveToRangeStart";

		// Token: 0x0400A44A RID: 42058
		private const byte tagNsId = 23;

		// Token: 0x0400A44B RID: 42059
		internal const int ElementTypeIdConst = 11482;
	}
}
