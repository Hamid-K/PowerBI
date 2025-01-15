using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002743 RID: 10051
	[GeneratedCode("DomGen", "2.0")]
	internal class ForegroundColor : ColorType
	{
		// Token: 0x17006067 RID: 24679
		// (get) Token: 0x06013575 RID: 79221 RVA: 0x00306324 File Offset: 0x00304524
		public override string LocalName
		{
			get
			{
				return "fgClr";
			}
		}

		// Token: 0x17006068 RID: 24680
		// (get) Token: 0x06013576 RID: 79222 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006069 RID: 24681
		// (get) Token: 0x06013577 RID: 79223 RVA: 0x0030632B File Offset: 0x0030452B
		internal override int ElementTypeId
		{
			get
			{
				return 10213;
			}
		}

		// Token: 0x06013578 RID: 79224 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013579 RID: 79225 RVA: 0x003062AA File Offset: 0x003044AA
		public ForegroundColor()
		{
		}

		// Token: 0x0601357A RID: 79226 RVA: 0x003062B2 File Offset: 0x003044B2
		public ForegroundColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601357B RID: 79227 RVA: 0x003062BB File Offset: 0x003044BB
		public ForegroundColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601357C RID: 79228 RVA: 0x003062C4 File Offset: 0x003044C4
		public ForegroundColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601357D RID: 79229 RVA: 0x00306332 File Offset: 0x00304532
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ForegroundColor>(deep);
		}

		// Token: 0x040085B7 RID: 34231
		private const string tagName = "fgClr";

		// Token: 0x040085B8 RID: 34232
		private const byte tagNsId = 10;

		// Token: 0x040085B9 RID: 34233
		internal const int ElementTypeIdConst = 10213;
	}
}
