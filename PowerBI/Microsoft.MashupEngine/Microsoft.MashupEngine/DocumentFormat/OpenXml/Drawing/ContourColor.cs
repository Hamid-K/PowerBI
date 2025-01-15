using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002740 RID: 10048
	[GeneratedCode("DomGen", "2.0")]
	internal class ContourColor : ColorType
	{
		// Token: 0x1700605E RID: 24670
		// (get) Token: 0x0601355A RID: 79194 RVA: 0x002EEE07 File Offset: 0x002ED007
		public override string LocalName
		{
			get
			{
				return "contourClr";
			}
		}

		// Token: 0x1700605F RID: 24671
		// (get) Token: 0x0601355B RID: 79195 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006060 RID: 24672
		// (get) Token: 0x0601355C RID: 79196 RVA: 0x003062E6 File Offset: 0x003044E6
		internal override int ElementTypeId
		{
			get
			{
				return 10204;
			}
		}

		// Token: 0x0601355D RID: 79197 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601355E RID: 79198 RVA: 0x003062AA File Offset: 0x003044AA
		public ContourColor()
		{
		}

		// Token: 0x0601355F RID: 79199 RVA: 0x003062B2 File Offset: 0x003044B2
		public ContourColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013560 RID: 79200 RVA: 0x003062BB File Offset: 0x003044BB
		public ContourColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013561 RID: 79201 RVA: 0x003062C4 File Offset: 0x003044C4
		public ContourColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013562 RID: 79202 RVA: 0x003062ED File Offset: 0x003044ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContourColor>(deep);
		}

		// Token: 0x040085AE RID: 34222
		private const string tagName = "contourClr";

		// Token: 0x040085AF RID: 34223
		private const byte tagNsId = 10;

		// Token: 0x040085B0 RID: 34224
		internal const int ElementTypeIdConst = 10204;
	}
}
