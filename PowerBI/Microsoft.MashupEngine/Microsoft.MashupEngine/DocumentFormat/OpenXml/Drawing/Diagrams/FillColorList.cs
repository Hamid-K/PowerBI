using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002653 RID: 9811
	[GeneratedCode("DomGen", "2.0")]
	internal class FillColorList : ColorsType
	{
		// Token: 0x17005B57 RID: 23383
		// (get) Token: 0x06012A4A RID: 76362 RVA: 0x002FDA43 File Offset: 0x002FBC43
		public override string LocalName
		{
			get
			{
				return "fillClrLst";
			}
		}

		// Token: 0x17005B58 RID: 23384
		// (get) Token: 0x06012A4B RID: 76363 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B59 RID: 23385
		// (get) Token: 0x06012A4C RID: 76364 RVA: 0x002FDA4A File Offset: 0x002FBC4A
		internal override int ElementTypeId
		{
			get
			{
				return 10628;
			}
		}

		// Token: 0x06012A4D RID: 76365 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012A4E RID: 76366 RVA: 0x002FDA51 File Offset: 0x002FBC51
		public FillColorList()
		{
		}

		// Token: 0x06012A4F RID: 76367 RVA: 0x002FDA59 File Offset: 0x002FBC59
		public FillColorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A50 RID: 76368 RVA: 0x002FDA62 File Offset: 0x002FBC62
		public FillColorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A51 RID: 76369 RVA: 0x002FDA6B File Offset: 0x002FBC6B
		public FillColorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A52 RID: 76370 RVA: 0x002FDA74 File Offset: 0x002FBC74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillColorList>(deep);
		}

		// Token: 0x04008103 RID: 33027
		private const string tagName = "fillClrLst";

		// Token: 0x04008104 RID: 33028
		private const byte tagNsId = 14;

		// Token: 0x04008105 RID: 33029
		internal const int ElementTypeIdConst = 10628;
	}
}
