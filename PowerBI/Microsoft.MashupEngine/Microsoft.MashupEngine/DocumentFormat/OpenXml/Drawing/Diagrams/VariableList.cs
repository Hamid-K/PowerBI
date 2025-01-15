using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200266F RID: 9839
	[GeneratedCode("DomGen", "2.0")]
	internal class VariableList : LayoutVariablePropertySetType
	{
		// Token: 0x17005C2C RID: 23596
		// (get) Token: 0x06012C36 RID: 76854 RVA: 0x002FEFEA File Offset: 0x002FD1EA
		public override string LocalName
		{
			get
			{
				return "varLst";
			}
		}

		// Token: 0x17005C2D RID: 23597
		// (get) Token: 0x06012C37 RID: 76855 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C2E RID: 23598
		// (get) Token: 0x06012C38 RID: 76856 RVA: 0x002FEFF1 File Offset: 0x002FD1F1
		internal override int ElementTypeId
		{
			get
			{
				return 10655;
			}
		}

		// Token: 0x06012C39 RID: 76857 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012C3A RID: 76858 RVA: 0x002FEFF8 File Offset: 0x002FD1F8
		public VariableList()
		{
		}

		// Token: 0x06012C3B RID: 76859 RVA: 0x002FF000 File Offset: 0x002FD200
		public VariableList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C3C RID: 76860 RVA: 0x002FF009 File Offset: 0x002FD209
		public VariableList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C3D RID: 76861 RVA: 0x002FF012 File Offset: 0x002FD212
		public VariableList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012C3E RID: 76862 RVA: 0x002FF01B File Offset: 0x002FD21B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VariableList>(deep);
		}

		// Token: 0x0400817E RID: 33150
		private const string tagName = "varLst";

		// Token: 0x0400817F RID: 33151
		private const byte tagNsId = 14;

		// Token: 0x04008180 RID: 33152
		internal const int ElementTypeIdConst = 10655;
	}
}
