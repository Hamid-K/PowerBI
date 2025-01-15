using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002290 RID: 8848
	[GeneratedCode("DomGen", "2.0")]
	internal class SharedQatControls : QatItemsType
	{
		// Token: 0x170040A0 RID: 16544
		// (get) Token: 0x0600EF75 RID: 61301 RVA: 0x002CFFCA File Offset: 0x002CE1CA
		public override string LocalName
		{
			get
			{
				return "sharedControls";
			}
		}

		// Token: 0x170040A1 RID: 16545
		// (get) Token: 0x0600EF76 RID: 61302 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040A2 RID: 16546
		// (get) Token: 0x0600EF77 RID: 61303 RVA: 0x002CFFD1 File Offset: 0x002CE1D1
		internal override int ElementTypeId
		{
			get
			{
				return 12606;
			}
		}

		// Token: 0x0600EF78 RID: 61304 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600EF79 RID: 61305 RVA: 0x002CFFD8 File Offset: 0x002CE1D8
		public SharedQatControls()
		{
		}

		// Token: 0x0600EF7A RID: 61306 RVA: 0x002CFFE0 File Offset: 0x002CE1E0
		public SharedQatControls(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EF7B RID: 61307 RVA: 0x002CFFE9 File Offset: 0x002CE1E9
		public SharedQatControls(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EF7C RID: 61308 RVA: 0x002CFFF2 File Offset: 0x002CE1F2
		public SharedQatControls(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EF7D RID: 61309 RVA: 0x002CFFFB File Offset: 0x002CE1FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharedQatControls>(deep);
		}

		// Token: 0x0400702C RID: 28716
		private const string tagName = "sharedControls";

		// Token: 0x0400702D RID: 28717
		private const byte tagNsId = 34;

		// Token: 0x0400702E RID: 28718
		internal const int ElementTypeIdConst = 12606;
	}
}
