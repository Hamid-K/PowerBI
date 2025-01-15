using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002742 RID: 10050
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorTo : ColorType
	{
		// Token: 0x17006064 RID: 24676
		// (get) Token: 0x0601356C RID: 79212 RVA: 0x0030630D File Offset: 0x0030450D
		public override string LocalName
		{
			get
			{
				return "clrTo";
			}
		}

		// Token: 0x17006065 RID: 24677
		// (get) Token: 0x0601356D RID: 79213 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006066 RID: 24678
		// (get) Token: 0x0601356E RID: 79214 RVA: 0x00306314 File Offset: 0x00304514
		internal override int ElementTypeId
		{
			get
			{
				return 10206;
			}
		}

		// Token: 0x0601356F RID: 79215 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013570 RID: 79216 RVA: 0x003062AA File Offset: 0x003044AA
		public ColorTo()
		{
		}

		// Token: 0x06013571 RID: 79217 RVA: 0x003062B2 File Offset: 0x003044B2
		public ColorTo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013572 RID: 79218 RVA: 0x003062BB File Offset: 0x003044BB
		public ColorTo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013573 RID: 79219 RVA: 0x003062C4 File Offset: 0x003044C4
		public ColorTo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013574 RID: 79220 RVA: 0x0030631B File Offset: 0x0030451B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorTo>(deep);
		}

		// Token: 0x040085B4 RID: 34228
		private const string tagName = "clrTo";

		// Token: 0x040085B5 RID: 34229
		private const byte tagNsId = 10;

		// Token: 0x040085B6 RID: 34230
		internal const int ElementTypeIdConst = 10206;
	}
}
