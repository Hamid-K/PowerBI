using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027D0 RID: 10192
	[ChildElementInfo(typeof(Point))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CubicBezierCurveTo : OpenXmlCompositeElement
	{
		// Token: 0x170063CE RID: 25550
		// (get) Token: 0x06013D1A RID: 81178 RVA: 0x0030BFCE File Offset: 0x0030A1CE
		public override string LocalName
		{
			get
			{
				return "cubicBezTo";
			}
		}

		// Token: 0x170063CF RID: 25551
		// (get) Token: 0x06013D1B RID: 81179 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063D0 RID: 25552
		// (get) Token: 0x06013D1C RID: 81180 RVA: 0x0030BFD5 File Offset: 0x0030A1D5
		internal override int ElementTypeId
		{
			get
			{
				return 10226;
			}
		}

		// Token: 0x06013D1D RID: 81181 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013D1E RID: 81182 RVA: 0x00293ECF File Offset: 0x002920CF
		public CubicBezierCurveTo()
		{
		}

		// Token: 0x06013D1F RID: 81183 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CubicBezierCurveTo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D20 RID: 81184 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CubicBezierCurveTo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D21 RID: 81185 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CubicBezierCurveTo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013D22 RID: 81186 RVA: 0x0030BE07 File Offset: 0x0030A007
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "pt" == name)
			{
				return new Point();
			}
			return null;
		}

		// Token: 0x06013D23 RID: 81187 RVA: 0x0030BFDC File Offset: 0x0030A1DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CubicBezierCurveTo>(deep);
		}

		// Token: 0x040087EE RID: 34798
		private const string tagName = "cubicBezTo";

		// Token: 0x040087EF RID: 34799
		private const byte tagNsId = 10;

		// Token: 0x040087F0 RID: 34800
		internal const int ElementTypeIdConst = 10226;
	}
}
