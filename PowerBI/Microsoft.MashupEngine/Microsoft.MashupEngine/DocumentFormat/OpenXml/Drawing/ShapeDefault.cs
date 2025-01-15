using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E1 RID: 10209
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeDefault : DefaultShapeDefinitionType
	{
		// Token: 0x17006449 RID: 25673
		// (get) Token: 0x06013E37 RID: 81463 RVA: 0x0030CE60 File Offset: 0x0030B060
		public override string LocalName
		{
			get
			{
				return "spDef";
			}
		}

		// Token: 0x1700644A RID: 25674
		// (get) Token: 0x06013E38 RID: 81464 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700644B RID: 25675
		// (get) Token: 0x06013E39 RID: 81465 RVA: 0x0030CE67 File Offset: 0x0030B067
		internal override int ElementTypeId
		{
			get
			{
				return 10241;
			}
		}

		// Token: 0x06013E3A RID: 81466 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013E3B RID: 81467 RVA: 0x0030CE6E File Offset: 0x0030B06E
		public ShapeDefault()
		{
		}

		// Token: 0x06013E3C RID: 81468 RVA: 0x0030CE76 File Offset: 0x0030B076
		public ShapeDefault(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E3D RID: 81469 RVA: 0x0030CE7F File Offset: 0x0030B07F
		public ShapeDefault(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E3E RID: 81470 RVA: 0x0030CE88 File Offset: 0x0030B088
		public ShapeDefault(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013E3F RID: 81471 RVA: 0x0030CE91 File Offset: 0x0030B091
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeDefault>(deep);
		}

		// Token: 0x0400882C RID: 34860
		private const string tagName = "spDef";

		// Token: 0x0400882D RID: 34861
		private const byte tagNsId = 10;

		// Token: 0x0400882E RID: 34862
		internal const int ElementTypeIdConst = 10241;
	}
}
