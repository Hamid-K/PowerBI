using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002744 RID: 10052
	[GeneratedCode("DomGen", "2.0")]
	internal class BackgroundColor : ColorType
	{
		// Token: 0x1700606A RID: 24682
		// (get) Token: 0x0601357E RID: 79230 RVA: 0x0030633B File Offset: 0x0030453B
		public override string LocalName
		{
			get
			{
				return "bgClr";
			}
		}

		// Token: 0x1700606B RID: 24683
		// (get) Token: 0x0601357F RID: 79231 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700606C RID: 24684
		// (get) Token: 0x06013580 RID: 79232 RVA: 0x00306342 File Offset: 0x00304542
		internal override int ElementTypeId
		{
			get
			{
				return 10214;
			}
		}

		// Token: 0x06013581 RID: 79233 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013582 RID: 79234 RVA: 0x003062AA File Offset: 0x003044AA
		public BackgroundColor()
		{
		}

		// Token: 0x06013583 RID: 79235 RVA: 0x003062B2 File Offset: 0x003044B2
		public BackgroundColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013584 RID: 79236 RVA: 0x003062BB File Offset: 0x003044BB
		public BackgroundColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013585 RID: 79237 RVA: 0x003062C4 File Offset: 0x003044C4
		public BackgroundColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013586 RID: 79238 RVA: 0x00306349 File Offset: 0x00304549
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundColor>(deep);
		}

		// Token: 0x040085BA RID: 34234
		private const string tagName = "bgClr";

		// Token: 0x040085BB RID: 34235
		private const byte tagNsId = 10;

		// Token: 0x040085BC RID: 34236
		internal const int ElementTypeIdConst = 10214;
	}
}
