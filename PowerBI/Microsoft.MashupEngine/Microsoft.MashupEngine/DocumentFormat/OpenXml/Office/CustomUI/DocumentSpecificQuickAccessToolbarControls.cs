using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002291 RID: 8849
	[GeneratedCode("DomGen", "2.0")]
	internal class DocumentSpecificQuickAccessToolbarControls : QatItemsType
	{
		// Token: 0x170040A3 RID: 16547
		// (get) Token: 0x0600EF7E RID: 61310 RVA: 0x002D0004 File Offset: 0x002CE204
		public override string LocalName
		{
			get
			{
				return "documentControls";
			}
		}

		// Token: 0x170040A4 RID: 16548
		// (get) Token: 0x0600EF7F RID: 61311 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040A5 RID: 16549
		// (get) Token: 0x0600EF80 RID: 61312 RVA: 0x002D000B File Offset: 0x002CE20B
		internal override int ElementTypeId
		{
			get
			{
				return 12607;
			}
		}

		// Token: 0x0600EF81 RID: 61313 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600EF82 RID: 61314 RVA: 0x002CFFD8 File Offset: 0x002CE1D8
		public DocumentSpecificQuickAccessToolbarControls()
		{
		}

		// Token: 0x0600EF83 RID: 61315 RVA: 0x002CFFE0 File Offset: 0x002CE1E0
		public DocumentSpecificQuickAccessToolbarControls(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EF84 RID: 61316 RVA: 0x002CFFE9 File Offset: 0x002CE1E9
		public DocumentSpecificQuickAccessToolbarControls(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EF85 RID: 61317 RVA: 0x002CFFF2 File Offset: 0x002CE1F2
		public DocumentSpecificQuickAccessToolbarControls(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EF86 RID: 61318 RVA: 0x002D0012 File Offset: 0x002CE212
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentSpecificQuickAccessToolbarControls>(deep);
		}

		// Token: 0x0400702F RID: 28719
		private const string tagName = "documentControls";

		// Token: 0x04007030 RID: 28720
		private const byte tagNsId = 34;

		// Token: 0x04007031 RID: 28721
		internal const int ElementTypeIdConst = 12607;
	}
}
