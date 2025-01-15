using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A73 RID: 10867
	[GeneratedCode("DomGen", "2.0")]
	internal class BodyStyle : TextListStyleType
	{
		// Token: 0x17007310 RID: 29456
		// (get) Token: 0x06016006 RID: 90118 RVA: 0x00325A5A File Offset: 0x00323C5A
		public override string LocalName
		{
			get
			{
				return "bodyStyle";
			}
		}

		// Token: 0x17007311 RID: 29457
		// (get) Token: 0x06016007 RID: 90119 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007312 RID: 29458
		// (get) Token: 0x06016008 RID: 90120 RVA: 0x00325A61 File Offset: 0x00323C61
		internal override int ElementTypeId
		{
			get
			{
				return 12284;
			}
		}

		// Token: 0x06016009 RID: 90121 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601600A RID: 90122 RVA: 0x00325A2E File Offset: 0x00323C2E
		public BodyStyle()
		{
		}

		// Token: 0x0601600B RID: 90123 RVA: 0x00325A36 File Offset: 0x00323C36
		public BodyStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601600C RID: 90124 RVA: 0x00325A3F File Offset: 0x00323C3F
		public BodyStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601600D RID: 90125 RVA: 0x00325A48 File Offset: 0x00323C48
		public BodyStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601600E RID: 90126 RVA: 0x00325A68 File Offset: 0x00323C68
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BodyStyle>(deep);
		}

		// Token: 0x040095BC RID: 38332
		private const string tagName = "bodyStyle";

		// Token: 0x040095BD RID: 38333
		private const byte tagNsId = 24;

		// Token: 0x040095BE RID: 38334
		internal const int ElementTypeIdConst = 12284;
	}
}
