using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002741 RID: 10049
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorFrom : ColorType
	{
		// Token: 0x17006061 RID: 24673
		// (get) Token: 0x06013563 RID: 79203 RVA: 0x003062F6 File Offset: 0x003044F6
		public override string LocalName
		{
			get
			{
				return "clrFrom";
			}
		}

		// Token: 0x17006062 RID: 24674
		// (get) Token: 0x06013564 RID: 79204 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006063 RID: 24675
		// (get) Token: 0x06013565 RID: 79205 RVA: 0x003062FD File Offset: 0x003044FD
		internal override int ElementTypeId
		{
			get
			{
				return 10205;
			}
		}

		// Token: 0x06013566 RID: 79206 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013567 RID: 79207 RVA: 0x003062AA File Offset: 0x003044AA
		public ColorFrom()
		{
		}

		// Token: 0x06013568 RID: 79208 RVA: 0x003062B2 File Offset: 0x003044B2
		public ColorFrom(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013569 RID: 79209 RVA: 0x003062BB File Offset: 0x003044BB
		public ColorFrom(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601356A RID: 79210 RVA: 0x003062C4 File Offset: 0x003044C4
		public ColorFrom(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601356B RID: 79211 RVA: 0x00306304 File Offset: 0x00304504
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorFrom>(deep);
		}

		// Token: 0x040085B1 RID: 34225
		private const string tagName = "clrFrom";

		// Token: 0x040085B2 RID: 34226
		private const byte tagNsId = 10;

		// Token: 0x040085B3 RID: 34227
		internal const int ElementTypeIdConst = 10205;
	}
}
