using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200262C RID: 9772
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualConnectorShapeDrawingProperties))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(Style))]
	internal class ConnectionShape : OpenXmlCompositeElement
	{
		// Token: 0x17005A1C RID: 23068
		// (get) Token: 0x06012773 RID: 75635 RVA: 0x002FB89A File Offset: 0x002F9A9A
		public override string LocalName
		{
			get
			{
				return "cxnSp";
			}
		}

		// Token: 0x17005A1D RID: 23069
		// (get) Token: 0x06012774 RID: 75636 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A1E RID: 23070
		// (get) Token: 0x06012775 RID: 75637 RVA: 0x002FB8A1 File Offset: 0x002F9AA1
		internal override int ElementTypeId
		{
			get
			{
				return 10591;
			}
		}

		// Token: 0x06012776 RID: 75638 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005A1F RID: 23071
		// (get) Token: 0x06012777 RID: 75639 RVA: 0x002FB8A8 File Offset: 0x002F9AA8
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConnectionShape.attributeTagNames;
			}
		}

		// Token: 0x17005A20 RID: 23072
		// (get) Token: 0x06012778 RID: 75640 RVA: 0x002FB8AF File Offset: 0x002F9AAF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConnectionShape.attributeNamespaceIds;
			}
		}

		// Token: 0x17005A21 RID: 23073
		// (get) Token: 0x06012779 RID: 75641 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601277A RID: 75642 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005A22 RID: 23074
		// (get) Token: 0x0601277B RID: 75643 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601277C RID: 75644 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601277D RID: 75645 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionShape()
		{
		}

		// Token: 0x0601277E RID: 75646 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601277F RID: 75647 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012780 RID: 75648 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012781 RID: 75649 RVA: 0x002FB8B8 File Offset: 0x002F9AB8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "nvCxnSpPr" == name)
			{
				return new NonVisualConnectorShapeDrawingProperties();
			}
			if (12 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (12 == namespaceId && "style" == name)
			{
				return new Style();
			}
			return null;
		}

		// Token: 0x17005A23 RID: 23075
		// (get) Token: 0x06012782 RID: 75650 RVA: 0x002FB90E File Offset: 0x002F9B0E
		internal override string[] ElementTagNames
		{
			get
			{
				return ConnectionShape.eleTagNames;
			}
		}

		// Token: 0x17005A24 RID: 23076
		// (get) Token: 0x06012783 RID: 75651 RVA: 0x002FB915 File Offset: 0x002F9B15
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConnectionShape.eleNamespaceIds;
			}
		}

		// Token: 0x17005A25 RID: 23077
		// (get) Token: 0x06012784 RID: 75652 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A26 RID: 23078
		// (get) Token: 0x06012785 RID: 75653 RVA: 0x002FB91C File Offset: 0x002F9B1C
		// (set) Token: 0x06012786 RID: 75654 RVA: 0x002FB925 File Offset: 0x002F9B25
		public NonVisualConnectorShapeDrawingProperties NonVisualConnectorShapeDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualConnectorShapeDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualConnectorShapeDrawingProperties>(0, value);
			}
		}

		// Token: 0x17005A27 RID: 23079
		// (get) Token: 0x06012787 RID: 75655 RVA: 0x002FB4A7 File Offset: 0x002F96A7
		// (set) Token: 0x06012788 RID: 75656 RVA: 0x002FB4B0 File Offset: 0x002F96B0
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x17005A28 RID: 23080
		// (get) Token: 0x06012789 RID: 75657 RVA: 0x002FB4BA File Offset: 0x002F96BA
		// (set) Token: 0x0601278A RID: 75658 RVA: 0x002FB4C3 File Offset: 0x002F96C3
		public Style Style
		{
			get
			{
				return base.GetElement<Style>(2);
			}
			set
			{
				base.SetElement<Style>(2, value);
			}
		}

		// Token: 0x0601278B RID: 75659 RVA: 0x002DFFB5 File Offset: 0x002DE1B5
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

		// Token: 0x0601278C RID: 75660 RVA: 0x002FB92F File Offset: 0x002F9B2F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionShape>(deep);
		}

		// Token: 0x0601278D RID: 75661 RVA: 0x002FB938 File Offset: 0x002F9B38
		// Note: this type is marked as 'beforefieldinit'.
		static ConnectionShape()
		{
			byte[] array = new byte[2];
			ConnectionShape.attributeNamespaceIds = array;
			ConnectionShape.eleTagNames = new string[] { "nvCxnSpPr", "spPr", "style" };
			ConnectionShape.eleNamespaceIds = new byte[] { 12, 12, 12 };
		}

		// Token: 0x04008042 RID: 32834
		private const string tagName = "cxnSp";

		// Token: 0x04008043 RID: 32835
		private const byte tagNsId = 12;

		// Token: 0x04008044 RID: 32836
		internal const int ElementTypeIdConst = 10591;

		// Token: 0x04008045 RID: 32837
		private static string[] attributeTagNames = new string[] { "macro", "fPublished" };

		// Token: 0x04008046 RID: 32838
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008047 RID: 32839
		private static readonly string[] eleTagNames;

		// Token: 0x04008048 RID: 32840
		private static readonly byte[] eleNamespaceIds;
	}
}
