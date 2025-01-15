using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002745 RID: 10053
	[GeneratedCode("DomGen", "2.0")]
	internal class Highlight : ColorType
	{
		// Token: 0x1700606D RID: 24685
		// (get) Token: 0x06013587 RID: 79239 RVA: 0x00306352 File Offset: 0x00304552
		public override string LocalName
		{
			get
			{
				return "highlight";
			}
		}

		// Token: 0x1700606E RID: 24686
		// (get) Token: 0x06013588 RID: 79240 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700606F RID: 24687
		// (get) Token: 0x06013589 RID: 79241 RVA: 0x00306359 File Offset: 0x00304559
		internal override int ElementTypeId
		{
			get
			{
				return 10324;
			}
		}

		// Token: 0x0601358A RID: 79242 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601358B RID: 79243 RVA: 0x003062AA File Offset: 0x003044AA
		public Highlight()
		{
		}

		// Token: 0x0601358C RID: 79244 RVA: 0x003062B2 File Offset: 0x003044B2
		public Highlight(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601358D RID: 79245 RVA: 0x003062BB File Offset: 0x003044BB
		public Highlight(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601358E RID: 79246 RVA: 0x003062C4 File Offset: 0x003044C4
		public Highlight(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601358F RID: 79247 RVA: 0x00306360 File Offset: 0x00304560
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Highlight>(deep);
		}

		// Token: 0x040085BD RID: 34237
		private const string tagName = "highlight";

		// Token: 0x040085BE RID: 34238
		private const byte tagNsId = 10;

		// Token: 0x040085BF RID: 34239
		internal const int ElementTypeIdConst = 10324;
	}
}
