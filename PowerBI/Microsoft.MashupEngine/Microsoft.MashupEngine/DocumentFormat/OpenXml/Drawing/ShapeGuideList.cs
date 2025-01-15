using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D4 RID: 10196
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeGuideList : GeometryGuideListType
	{
		// Token: 0x170063DE RID: 25566
		// (get) Token: 0x06013D4A RID: 81226 RVA: 0x0030C1DC File Offset: 0x0030A3DC
		public override string LocalName
		{
			get
			{
				return "gdLst";
			}
		}

		// Token: 0x170063DF RID: 25567
		// (get) Token: 0x06013D4B RID: 81227 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063E0 RID: 25568
		// (get) Token: 0x06013D4C RID: 81228 RVA: 0x0030C1E3 File Offset: 0x0030A3E3
		internal override int ElementTypeId
		{
			get
			{
				return 10229;
			}
		}

		// Token: 0x06013D4D RID: 81229 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013D4E RID: 81230 RVA: 0x0030C1B0 File Offset: 0x0030A3B0
		public ShapeGuideList()
		{
		}

		// Token: 0x06013D4F RID: 81231 RVA: 0x0030C1B8 File Offset: 0x0030A3B8
		public ShapeGuideList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D50 RID: 81232 RVA: 0x0030C1C1 File Offset: 0x0030A3C1
		public ShapeGuideList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D51 RID: 81233 RVA: 0x0030C1CA File Offset: 0x0030A3CA
		public ShapeGuideList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013D52 RID: 81234 RVA: 0x0030C1EA File Offset: 0x0030A3EA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeGuideList>(deep);
		}

		// Token: 0x040087F9 RID: 34809
		private const string tagName = "gdLst";

		// Token: 0x040087FA RID: 34810
		private const byte tagNsId = 10;

		// Token: 0x040087FB RID: 34811
		internal const int ElementTypeIdConst = 10229;
	}
}
