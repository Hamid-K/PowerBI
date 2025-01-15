using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E2 RID: 10210
	[GeneratedCode("DomGen", "2.0")]
	internal class LineDefault : DefaultShapeDefinitionType
	{
		// Token: 0x1700644C RID: 25676
		// (get) Token: 0x06013E40 RID: 81472 RVA: 0x0030CE9A File Offset: 0x0030B09A
		public override string LocalName
		{
			get
			{
				return "lnDef";
			}
		}

		// Token: 0x1700644D RID: 25677
		// (get) Token: 0x06013E41 RID: 81473 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700644E RID: 25678
		// (get) Token: 0x06013E42 RID: 81474 RVA: 0x0030CEA1 File Offset: 0x0030B0A1
		internal override int ElementTypeId
		{
			get
			{
				return 10242;
			}
		}

		// Token: 0x06013E43 RID: 81475 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013E44 RID: 81476 RVA: 0x0030CE6E File Offset: 0x0030B06E
		public LineDefault()
		{
		}

		// Token: 0x06013E45 RID: 81477 RVA: 0x0030CE76 File Offset: 0x0030B076
		public LineDefault(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E46 RID: 81478 RVA: 0x0030CE7F File Offset: 0x0030B07F
		public LineDefault(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E47 RID: 81479 RVA: 0x0030CE88 File Offset: 0x0030B088
		public LineDefault(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013E48 RID: 81480 RVA: 0x0030CEA8 File Offset: 0x0030B0A8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineDefault>(deep);
		}

		// Token: 0x0400882F RID: 34863
		private const string tagName = "lnDef";

		// Token: 0x04008830 RID: 34864
		private const byte tagNsId = 10;

		// Token: 0x04008831 RID: 34865
		internal const int ElementTypeIdConst = 10242;
	}
}
