using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E0D RID: 11789
	[GeneratedCode("DomGen", "2.0")]
	internal class ApplyBreakingRules : OnOffType
	{
		// Token: 0x170088C3 RID: 35011
		// (get) Token: 0x06019046 RID: 102470 RVA: 0x00345838 File Offset: 0x00343A38
		public override string LocalName
		{
			get
			{
				return "applyBreakingRules";
			}
		}

		// Token: 0x170088C4 RID: 35012
		// (get) Token: 0x06019047 RID: 102471 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088C5 RID: 35013
		// (get) Token: 0x06019048 RID: 102472 RVA: 0x0034583F File Offset: 0x00343A3F
		internal override int ElementTypeId
		{
			get
			{
				return 12099;
			}
		}

		// Token: 0x06019049 RID: 102473 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601904B RID: 102475 RVA: 0x00345846 File Offset: 0x00343A46
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplyBreakingRules>(deep);
		}

		// Token: 0x0400A692 RID: 42642
		private const string tagName = "applyBreakingRules";

		// Token: 0x0400A693 RID: 42643
		private const byte tagNsId = 23;

		// Token: 0x0400A694 RID: 42644
		internal const int ElementTypeIdConst = 12099;
	}
}
