using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002654 RID: 9812
	[GeneratedCode("DomGen", "2.0")]
	internal class LineColorList : ColorsType
	{
		// Token: 0x17005B5A RID: 23386
		// (get) Token: 0x06012A53 RID: 76371 RVA: 0x002FDA7D File Offset: 0x002FBC7D
		public override string LocalName
		{
			get
			{
				return "linClrLst";
			}
		}

		// Token: 0x17005B5B RID: 23387
		// (get) Token: 0x06012A54 RID: 76372 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B5C RID: 23388
		// (get) Token: 0x06012A55 RID: 76373 RVA: 0x002FDA84 File Offset: 0x002FBC84
		internal override int ElementTypeId
		{
			get
			{
				return 10629;
			}
		}

		// Token: 0x06012A56 RID: 76374 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012A57 RID: 76375 RVA: 0x002FDA51 File Offset: 0x002FBC51
		public LineColorList()
		{
		}

		// Token: 0x06012A58 RID: 76376 RVA: 0x002FDA59 File Offset: 0x002FBC59
		public LineColorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A59 RID: 76377 RVA: 0x002FDA62 File Offset: 0x002FBC62
		public LineColorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A5A RID: 76378 RVA: 0x002FDA6B File Offset: 0x002FBC6B
		public LineColorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A5B RID: 76379 RVA: 0x002FDA8B File Offset: 0x002FBC8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineColorList>(deep);
		}

		// Token: 0x04008106 RID: 33030
		private const string tagName = "linClrLst";

		// Token: 0x04008107 RID: 33031
		private const byte tagNsId = 14;

		// Token: 0x04008108 RID: 33032
		internal const int ElementTypeIdConst = 10629;
	}
}
