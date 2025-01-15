using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200262B RID: 9771
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Transform))]
	[ChildElementInfo(typeof(Graphic))]
	[ChildElementInfo(typeof(NonVisualGraphicFrameProperties))]
	internal class GraphicFrame : OpenXmlCompositeElement
	{
		// Token: 0x17005A0F RID: 23055
		// (get) Token: 0x06012758 RID: 75608 RVA: 0x002EFCFA File Offset: 0x002EDEFA
		public override string LocalName
		{
			get
			{
				return "graphicFrame";
			}
		}

		// Token: 0x17005A10 RID: 23056
		// (get) Token: 0x06012759 RID: 75609 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A11 RID: 23057
		// (get) Token: 0x0601275A RID: 75610 RVA: 0x002FB768 File Offset: 0x002F9968
		internal override int ElementTypeId
		{
			get
			{
				return 10590;
			}
		}

		// Token: 0x0601275B RID: 75611 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005A12 RID: 23058
		// (get) Token: 0x0601275C RID: 75612 RVA: 0x002FB76F File Offset: 0x002F996F
		internal override string[] AttributeTagNames
		{
			get
			{
				return GraphicFrame.attributeTagNames;
			}
		}

		// Token: 0x17005A13 RID: 23059
		// (get) Token: 0x0601275D RID: 75613 RVA: 0x002FB776 File Offset: 0x002F9976
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GraphicFrame.attributeNamespaceIds;
			}
		}

		// Token: 0x17005A14 RID: 23060
		// (get) Token: 0x0601275E RID: 75614 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601275F RID: 75615 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "macro")]
		public StringValue Macro
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005A15 RID: 23061
		// (get) Token: 0x06012760 RID: 75616 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06012761 RID: 75617 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fPublished")]
		public BooleanValue Published
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06012762 RID: 75618 RVA: 0x00293ECF File Offset: 0x002920CF
		public GraphicFrame()
		{
		}

		// Token: 0x06012763 RID: 75619 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GraphicFrame(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012764 RID: 75620 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GraphicFrame(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012765 RID: 75621 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GraphicFrame(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012766 RID: 75622 RVA: 0x002FB780 File Offset: 0x002F9980
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "nvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameProperties();
			}
			if (12 == namespaceId && "xfrm" == name)
			{
				return new Transform();
			}
			if (10 == namespaceId && "graphic" == name)
			{
				return new Graphic();
			}
			return null;
		}

		// Token: 0x17005A16 RID: 23062
		// (get) Token: 0x06012767 RID: 75623 RVA: 0x002FB7D6 File Offset: 0x002F99D6
		internal override string[] ElementTagNames
		{
			get
			{
				return GraphicFrame.eleTagNames;
			}
		}

		// Token: 0x17005A17 RID: 23063
		// (get) Token: 0x06012768 RID: 75624 RVA: 0x002FB7DD File Offset: 0x002F99DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GraphicFrame.eleNamespaceIds;
			}
		}

		// Token: 0x17005A18 RID: 23064
		// (get) Token: 0x06012769 RID: 75625 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A19 RID: 23065
		// (get) Token: 0x0601276A RID: 75626 RVA: 0x002FB7E4 File Offset: 0x002F99E4
		// (set) Token: 0x0601276B RID: 75627 RVA: 0x002FB7ED File Offset: 0x002F99ED
		public NonVisualGraphicFrameProperties NonVisualGraphicFrameProperties
		{
			get
			{
				return base.GetElement<NonVisualGraphicFrameProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualGraphicFrameProperties>(0, value);
			}
		}

		// Token: 0x17005A1A RID: 23066
		// (get) Token: 0x0601276C RID: 75628 RVA: 0x002FB7F7 File Offset: 0x002F99F7
		// (set) Token: 0x0601276D RID: 75629 RVA: 0x002FB800 File Offset: 0x002F9A00
		public Transform Transform
		{
			get
			{
				return base.GetElement<Transform>(1);
			}
			set
			{
				base.SetElement<Transform>(1, value);
			}
		}

		// Token: 0x17005A1B RID: 23067
		// (get) Token: 0x0601276E RID: 75630 RVA: 0x002FB80A File Offset: 0x002F9A0A
		// (set) Token: 0x0601276F RID: 75631 RVA: 0x002FB813 File Offset: 0x002F9A13
		public Graphic Graphic
		{
			get
			{
				return base.GetElement<Graphic>(2);
			}
			set
			{
				base.SetElement<Graphic>(2, value);
			}
		}

		// Token: 0x06012770 RID: 75632 RVA: 0x002DFFB5 File Offset: 0x002DE1B5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "macro" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fPublished" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012771 RID: 75633 RVA: 0x002FB81D File Offset: 0x002F9A1D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GraphicFrame>(deep);
		}

		// Token: 0x06012772 RID: 75634 RVA: 0x002FB828 File Offset: 0x002F9A28
		// Note: this type is marked as 'beforefieldinit'.
		static GraphicFrame()
		{
			byte[] array = new byte[2];
			GraphicFrame.attributeNamespaceIds = array;
			GraphicFrame.eleTagNames = new string[] { "nvGraphicFramePr", "xfrm", "graphic" };
			GraphicFrame.eleNamespaceIds = new byte[] { 12, 12, 10 };
		}

		// Token: 0x0400803B RID: 32827
		private const string tagName = "graphicFrame";

		// Token: 0x0400803C RID: 32828
		private const byte tagNsId = 12;

		// Token: 0x0400803D RID: 32829
		internal const int ElementTypeIdConst = 10590;

		// Token: 0x0400803E RID: 32830
		private static string[] attributeTagNames = new string[] { "macro", "fPublished" };

		// Token: 0x0400803F RID: 32831
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008040 RID: 32832
		private static readonly string[] eleTagNames;

		// Token: 0x04008041 RID: 32833
		private static readonly byte[] eleNamespaceIds;
	}
}
