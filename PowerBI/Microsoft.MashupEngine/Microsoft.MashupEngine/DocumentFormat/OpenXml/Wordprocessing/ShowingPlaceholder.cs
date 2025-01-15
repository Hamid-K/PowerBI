using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E22 RID: 11810
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowingPlaceholder : OnOffType
	{
		// Token: 0x17008902 RID: 35074
		// (get) Token: 0x060190C4 RID: 102596 RVA: 0x00345A1B File Offset: 0x00343C1B
		public override string LocalName
		{
			get
			{
				return "showingPlcHdr";
			}
		}

		// Token: 0x17008903 RID: 35075
		// (get) Token: 0x060190C5 RID: 102597 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008904 RID: 35076
		// (get) Token: 0x060190C6 RID: 102598 RVA: 0x00345A22 File Offset: 0x00343C22
		internal override int ElementTypeId
		{
			get
			{
				return 12142;
			}
		}

		// Token: 0x060190C7 RID: 102599 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190C9 RID: 102601 RVA: 0x00345A29 File Offset: 0x00343C29
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowingPlaceholder>(deep);
		}

		// Token: 0x0400A6D1 RID: 42705
		private const string tagName = "showingPlcHdr";

		// Token: 0x0400A6D2 RID: 42706
		private const byte tagNsId = 23;

		// Token: 0x0400A6D3 RID: 42707
		internal const int ElementTypeIdConst = 12142;
	}
}
