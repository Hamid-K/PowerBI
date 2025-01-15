using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE1 RID: 11745
	[GeneratedCode("DomGen", "2.0")]
	internal class UseSingleBorderForContiguousCells : OnOffType
	{
		// Token: 0x1700883F RID: 34879
		// (get) Token: 0x06018F3E RID: 102206 RVA: 0x00345444 File Offset: 0x00343644
		public override string LocalName
		{
			get
			{
				return "useSingleBorderforContiguousCells";
			}
		}

		// Token: 0x17008840 RID: 34880
		// (get) Token: 0x06018F3F RID: 102207 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008841 RID: 34881
		// (get) Token: 0x06018F40 RID: 102208 RVA: 0x0034544B File Offset: 0x0034364B
		internal override int ElementTypeId
		{
			get
			{
				return 12055;
			}
		}

		// Token: 0x06018F41 RID: 102209 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F43 RID: 102211 RVA: 0x00345452 File Offset: 0x00343652
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseSingleBorderForContiguousCells>(deep);
		}

		// Token: 0x0400A60E RID: 42510
		private const string tagName = "useSingleBorderforContiguousCells";

		// Token: 0x0400A60F RID: 42511
		private const byte tagNsId = 23;

		// Token: 0x0400A610 RID: 42512
		internal const int ElementTypeIdConst = 12055;
	}
}
