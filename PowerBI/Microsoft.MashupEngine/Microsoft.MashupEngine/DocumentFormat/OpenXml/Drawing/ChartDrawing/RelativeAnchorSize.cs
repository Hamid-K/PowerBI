using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002627 RID: 9767
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(ToAnchor))]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(FromAnchor))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shape))]
	internal class RelativeAnchorSize : OpenXmlCompositeElement
	{
		// Token: 0x170059E7 RID: 23015
		// (get) Token: 0x06012701 RID: 75521 RVA: 0x002FB0FD File Offset: 0x002F92FD
		public override string LocalName
		{
			get
			{
				return "relSizeAnchor";
			}
		}

		// Token: 0x170059E8 RID: 23016
		// (get) Token: 0x06012702 RID: 75522 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x170059E9 RID: 23017
		// (get) Token: 0x06012703 RID: 75523 RVA: 0x002FB104 File Offset: 0x002F9304
		internal override int ElementTypeId
		{
			get
			{
				return 10586;
			}
		}

		// Token: 0x06012704 RID: 75524 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012705 RID: 75525 RVA: 0x00293ECF File Offset: 0x002920CF
		public RelativeAnchorSize()
		{
		}

		// Token: 0x06012706 RID: 75526 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RelativeAnchorSize(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012707 RID: 75527 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RelativeAnchorSize(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012708 RID: 75528 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RelativeAnchorSize(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012709 RID: 75529 RVA: 0x002FB10C File Offset: 0x002F930C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "from" == name)
			{
				return new FromAnchor();
			}
			if (12 == namespaceId && "to" == name)
			{
				return new ToAnchor();
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

		// Token: 0x170059EA RID: 23018
		// (get) Token: 0x0601270A RID: 75530 RVA: 0x002FB1DA File Offset: 0x002F93DA
		internal override string[] ElementTagNames
		{
			get
			{
				return RelativeAnchorSize.eleTagNames;
			}
		}

		// Token: 0x170059EB RID: 23019
		// (get) Token: 0x0601270B RID: 75531 RVA: 0x002FB1E1 File Offset: 0x002F93E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RelativeAnchorSize.eleNamespaceIds;
			}
		}

		// Token: 0x170059EC RID: 23020
		// (get) Token: 0x0601270C RID: 75532 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170059ED RID: 23021
		// (get) Token: 0x0601270D RID: 75533 RVA: 0x002FB1E8 File Offset: 0x002F93E8
		// (set) Token: 0x0601270E RID: 75534 RVA: 0x002FB1F1 File Offset: 0x002F93F1
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

		// Token: 0x170059EE RID: 23022
		// (get) Token: 0x0601270F RID: 75535 RVA: 0x002FB1FB File Offset: 0x002F93FB
		// (set) Token: 0x06012710 RID: 75536 RVA: 0x002FB204 File Offset: 0x002F9404
		public ToAnchor ToAnchor
		{
			get
			{
				return base.GetElement<ToAnchor>(1);
			}
			set
			{
				base.SetElement<ToAnchor>(1, value);
			}
		}

		// Token: 0x06012711 RID: 75537 RVA: 0x002FB20E File Offset: 0x002F940E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RelativeAnchorSize>(deep);
		}

		// Token: 0x04008025 RID: 32805
		private const string tagName = "relSizeAnchor";

		// Token: 0x04008026 RID: 32806
		private const byte tagNsId = 12;

		// Token: 0x04008027 RID: 32807
		internal const int ElementTypeIdConst = 10586;

		// Token: 0x04008028 RID: 32808
		private static readonly string[] eleTagNames = new string[] { "from", "to", "sp", "grpSp", "graphicFrame", "cxnSp", "pic", "contentPart" };

		// Token: 0x04008029 RID: 32809
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12, 12, 12, 12, 12, 12, 47 };
	}
}
