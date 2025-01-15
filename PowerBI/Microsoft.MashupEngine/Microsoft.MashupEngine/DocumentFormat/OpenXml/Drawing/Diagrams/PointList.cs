using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002660 RID: 9824
	[ChildElementInfo(typeof(Point))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PointList : OpenXmlCompositeElement
	{
		// Token: 0x17005BAE RID: 23470
		// (get) Token: 0x06012B15 RID: 76565 RVA: 0x002FE1CC File Offset: 0x002FC3CC
		public override string LocalName
		{
			get
			{
				return "ptLst";
			}
		}

		// Token: 0x17005BAF RID: 23471
		// (get) Token: 0x06012B16 RID: 76566 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BB0 RID: 23472
		// (get) Token: 0x06012B17 RID: 76567 RVA: 0x002FE1D3 File Offset: 0x002FC3D3
		internal override int ElementTypeId
		{
			get
			{
				return 10641;
			}
		}

		// Token: 0x06012B18 RID: 76568 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012B19 RID: 76569 RVA: 0x00293ECF File Offset: 0x002920CF
		public PointList()
		{
		}

		// Token: 0x06012B1A RID: 76570 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PointList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B1B RID: 76571 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PointList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B1C RID: 76572 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PointList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012B1D RID: 76573 RVA: 0x002FE1DA File Offset: 0x002FC3DA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "pt" == name)
			{
				return new Point();
			}
			return null;
		}

		// Token: 0x06012B1E RID: 76574 RVA: 0x002FE1F5 File Offset: 0x002FC3F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PointList>(deep);
		}

		// Token: 0x0400813A RID: 33082
		private const string tagName = "ptLst";

		// Token: 0x0400813B RID: 33083
		private const byte tagNsId = 14;

		// Token: 0x0400813C RID: 33084
		internal const int ElementTypeIdConst = 10641;
	}
}
