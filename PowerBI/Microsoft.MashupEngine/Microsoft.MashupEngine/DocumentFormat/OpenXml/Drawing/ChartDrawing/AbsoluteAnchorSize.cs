using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002628 RID: 9768
	[ChildElementInfo(typeof(Extent))]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FromAnchor))]
	[ChildElementInfo(typeof(Shape))]
	internal class AbsoluteAnchorSize : OpenXmlCompositeElement
	{
		// Token: 0x170059EF RID: 23023
		// (get) Token: 0x06012713 RID: 75539 RVA: 0x002FB288 File Offset: 0x002F9488
		public override string LocalName
		{
			get
			{
				return "absSizeAnchor";
			}
		}

		// Token: 0x170059F0 RID: 23024
		// (get) Token: 0x06012714 RID: 75540 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x170059F1 RID: 23025
		// (get) Token: 0x06012715 RID: 75541 RVA: 0x002FB28F File Offset: 0x002F948F
		internal override int ElementTypeId
		{
			get
			{
				return 10587;
			}
		}

		// Token: 0x06012716 RID: 75542 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012717 RID: 75543 RVA: 0x00293ECF File Offset: 0x002920CF
		public AbsoluteAnchorSize()
		{
		}

		// Token: 0x06012718 RID: 75544 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AbsoluteAnchorSize(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012719 RID: 75545 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AbsoluteAnchorSize(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601271A RID: 75546 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AbsoluteAnchorSize(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601271B RID: 75547 RVA: 0x002FB298 File Offset: 0x002F9498
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "from" == name)
			{
				return new FromAnchor();
			}
			if (12 == namespaceId && "ext" == name)
			{
				return new Extent();
			}
			if (12 == namespaceId && "sp" == name)
			{
				return new Shape();
			}
			if (12 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (12 == namespaceId && "graphicFrame" == name)
			{
				return new GraphicFrame();
			}
			if (12 == namespaceId && "cxnSp" == name)
			{
				return new ConnectionShape();
			}
			if (12 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (47 == namespaceId && "contentPart" == name)
			{
				return new ContentPart();
			}
			return null;
		}

		// Token: 0x170059F2 RID: 23026
		// (get) Token: 0x0601271C RID: 75548 RVA: 0x002FB366 File Offset: 0x002F9566
		internal override string[] ElementTagNames
		{
			get
			{
				return AbsoluteAnchorSize.eleTagNames;
			}
		}

		// Token: 0x170059F3 RID: 23027
		// (get) Token: 0x0601271D RID: 75549 RVA: 0x002FB36D File Offset: 0x002F956D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AbsoluteAnchorSize.eleNamespaceIds;
			}
		}

		// Token: 0x170059F4 RID: 23028
		// (get) Token: 0x0601271E RID: 75550 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170059F5 RID: 23029
		// (get) Token: 0x0601271F RID: 75551 RVA: 0x002FB1E8 File Offset: 0x002F93E8
		// (set) Token: 0x06012720 RID: 75552 RVA: 0x002FB1F1 File Offset: 0x002F93F1
		public FromAnchor FromAnchor
		{
			get
			{
				return base.GetElement<FromAnchor>(0);
			}
			set
			{
				base.SetElement<FromAnchor>(0, value);
			}
		}

		// Token: 0x170059F6 RID: 23030
		// (get) Token: 0x06012721 RID: 75553 RVA: 0x002FB374 File Offset: 0x002F9574
		// (set) Token: 0x06012722 RID: 75554 RVA: 0x002FB37D File Offset: 0x002F957D
		public Extent Extent
		{
			get
			{
				return base.GetElement<Extent>(1);
			}
			set
			{
				base.SetElement<Extent>(1, value);
			}
		}

		// Token: 0x06012723 RID: 75555 RVA: 0x002FB387 File Offset: 0x002F9587
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AbsoluteAnchorSize>(deep);
		}

		// Token: 0x0400802A RID: 32810
		private const string tagName = "absSizeAnchor";

		// Token: 0x0400802B RID: 32811
		private const byte tagNsId = 12;

		// Token: 0x0400802C RID: 32812
		internal const int ElementTypeIdConst = 10587;

		// Token: 0x0400802D RID: 32813
		private static readonly string[] eleTagNames = new string[] { "from", "ext", "sp", "grpSp", "graphicFrame", "cxnSp", "pic", "contentPart" };

		// Token: 0x0400802E RID: 32814
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12, 12, 12, 12, 12, 12, 47 };
	}
}
