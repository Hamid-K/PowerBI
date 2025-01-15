using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E6 RID: 10214
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorMap : ColorMappingType
	{
		// Token: 0x17006467 RID: 25703
		// (get) Token: 0x06013E81 RID: 81537 RVA: 0x0030D0F1 File Offset: 0x0030B2F1
		public override string LocalName
		{
			get
			{
				return "clrMap";
			}
		}

		// Token: 0x17006468 RID: 25704
		// (get) Token: 0x06013E82 RID: 81538 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006469 RID: 25705
		// (get) Token: 0x06013E83 RID: 81539 RVA: 0x0030D0F8 File Offset: 0x0030B2F8
		internal override int ElementTypeId
		{
			get
			{
				return 10246;
			}
		}

		// Token: 0x06013E84 RID: 81540 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013E85 RID: 81541 RVA: 0x0030D0C5 File Offset: 0x0030B2C5
		public ColorMap()
		{
		}

		// Token: 0x06013E86 RID: 81542 RVA: 0x0030D0CD File Offset: 0x0030B2CD
		public ColorMap(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E87 RID: 81543 RVA: 0x0030D0D6 File Offset: 0x0030B2D6
		public ColorMap(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E88 RID: 81544 RVA: 0x0030D0DF File Offset: 0x0030B2DF
		public ColorMap(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013E89 RID: 81545 RVA: 0x0030D0FF File Offset: 0x0030B2FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorMap>(deep);
		}

		// Token: 0x0400883C RID: 34876
		private const string tagName = "clrMap";

		// Token: 0x0400883D RID: 34877
		private const byte tagNsId = 10;

		// Token: 0x0400883E RID: 34878
		internal const int ElementTypeIdConst = 10246;
	}
}
