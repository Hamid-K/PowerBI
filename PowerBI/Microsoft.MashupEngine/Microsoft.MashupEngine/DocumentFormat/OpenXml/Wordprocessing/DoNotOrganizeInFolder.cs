using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA7 RID: 11687
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotOrganizeInFolder : OnOffType
	{
		// Token: 0x17008791 RID: 34705
		// (get) Token: 0x06018DE2 RID: 101858 RVA: 0x00344F0E File Offset: 0x0034310E
		public override string LocalName
		{
			get
			{
				return "doNotOrganizeInFolder";
			}
		}

		// Token: 0x17008792 RID: 34706
		// (get) Token: 0x06018DE3 RID: 101859 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008793 RID: 34707
		// (get) Token: 0x06018DE4 RID: 101860 RVA: 0x00344F15 File Offset: 0x00343115
		internal override int ElementTypeId
		{
			get
			{
				return 11843;
			}
		}

		// Token: 0x06018DE5 RID: 101861 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DE7 RID: 101863 RVA: 0x00344F1C File Offset: 0x0034311C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotOrganizeInFolder>(deep);
		}

		// Token: 0x0400A560 RID: 42336
		private const string tagName = "doNotOrganizeInFolder";

		// Token: 0x0400A561 RID: 42337
		private const byte tagNsId = 23;

		// Token: 0x0400A562 RID: 42338
		internal const int ElementTypeIdConst = 11843;
	}
}
