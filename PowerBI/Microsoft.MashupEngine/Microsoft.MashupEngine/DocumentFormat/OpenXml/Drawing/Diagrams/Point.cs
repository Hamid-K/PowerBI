using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200265E RID: 9822
	[ChildElementInfo(typeof(TextBody))]
	[ChildElementInfo(typeof(PtExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(PropertySet))]
	internal class Point : OpenXmlCompositeElement
	{
		// Token: 0x17005B8D RID: 23437
		// (get) Token: 0x06012AD1 RID: 76497 RVA: 0x002F359C File Offset: 0x002F179C
		public override string LocalName
		{
			get
			{
				return "pt";
			}
		}

		// Token: 0x17005B8E RID: 23438
		// (get) Token: 0x06012AD2 RID: 76498 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B8F RID: 23439
		// (get) Token: 0x06012AD3 RID: 76499 RVA: 0x002FDE16 File Offset: 0x002FC016
		internal override int ElementTypeId
		{
			get
			{
				return 10639;
			}
		}

		// Token: 0x06012AD4 RID: 76500 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B90 RID: 23440
		// (get) Token: 0x06012AD5 RID: 76501 RVA: 0x002FDE1D File Offset: 0x002FC01D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Point.attributeTagNames;
			}
		}

		// Token: 0x17005B91 RID: 23441
		// (get) Token: 0x06012AD6 RID: 76502 RVA: 0x002FDE24 File Offset: 0x002FC024
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Point.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B92 RID: 23442
		// (get) Token: 0x06012AD7 RID: 76503 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012AD8 RID: 76504 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "modelId")]
		public StringValue ModelId
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

		// Token: 0x17005B93 RID: 23443
		// (get) Token: 0x06012AD9 RID: 76505 RVA: 0x002FDE2B File Offset: 0x002FC02B
		// (set) Token: 0x06012ADA RID: 76506 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<PointValues> Type
		{
			get
			{
				return (EnumValue<PointValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005B94 RID: 23444
		// (get) Token: 0x06012ADB RID: 76507 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06012ADC RID: 76508 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "cxnId")]
		public StringValue ConnectionId
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06012ADD RID: 76509 RVA: 0x00293ECF File Offset: 0x002920CF
		public Point()
		{
		}

		// Token: 0x06012ADE RID: 76510 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Point(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012ADF RID: 76511 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Point(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012AE0 RID: 76512 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Point(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012AE1 RID: 76513 RVA: 0x002FDE3C File Offset: 0x002FC03C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "prSet" == name)
			{
				return new PropertySet();
			}
			if (14 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (14 == namespaceId && "t" == name)
			{
				return new TextBody();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new PtExtensionList();
			}
			return null;
		}

		// Token: 0x17005B95 RID: 23445
		// (get) Token: 0x06012AE2 RID: 76514 RVA: 0x002FDEAA File Offset: 0x002FC0AA
		internal override string[] ElementTagNames
		{
			get
			{
				return Point.eleTagNames;
			}
		}

		// Token: 0x17005B96 RID: 23446
		// (get) Token: 0x06012AE3 RID: 76515 RVA: 0x002FDEB1 File Offset: 0x002FC0B1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Point.eleNamespaceIds;
			}
		}

		// Token: 0x17005B97 RID: 23447
		// (get) Token: 0x06012AE4 RID: 76516 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005B98 RID: 23448
		// (get) Token: 0x06012AE5 RID: 76517 RVA: 0x002FDEB8 File Offset: 0x002FC0B8
		// (set) Token: 0x06012AE6 RID: 76518 RVA: 0x002FDEC1 File Offset: 0x002FC0C1
		public PropertySet PropertySet
		{
			get
			{
				return base.GetElement<PropertySet>(0);
			}
			set
			{
				base.SetElement<PropertySet>(0, value);
			}
		}

		// Token: 0x17005B99 RID: 23449
		// (get) Token: 0x06012AE7 RID: 76519 RVA: 0x002FDECB File Offset: 0x002FC0CB
		// (set) Token: 0x06012AE8 RID: 76520 RVA: 0x002FDED4 File Offset: 0x002FC0D4
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

		// Token: 0x17005B9A RID: 23450
		// (get) Token: 0x06012AE9 RID: 76521 RVA: 0x002FDEDE File Offset: 0x002FC0DE
		// (set) Token: 0x06012AEA RID: 76522 RVA: 0x002FDEE7 File Offset: 0x002FC0E7
		public TextBody TextBody
		{
			get
			{
				return base.GetElement<TextBody>(2);
			}
			set
			{
				base.SetElement<TextBody>(2, value);
			}
		}

		// Token: 0x17005B9B RID: 23451
		// (get) Token: 0x06012AEB RID: 76523 RVA: 0x002FDEF1 File Offset: 0x002FC0F1
		// (set) Token: 0x06012AEC RID: 76524 RVA: 0x002FDEFA File Offset: 0x002FC0FA
		public PtExtensionList PtExtensionList
		{
			get
			{
				return base.GetElement<PtExtensionList>(3);
			}
			set
			{
				base.SetElement<PtExtensionList>(3, value);
			}
		}

		// Token: 0x06012AED RID: 76525 RVA: 0x002FDF04 File Offset: 0x002FC104
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "modelId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<PointValues>();
			}
			if (namespaceId == 0 && "cxnId" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012AEE RID: 76526 RVA: 0x002FDF5B File Offset: 0x002FC15B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Point>(deep);
		}

		// Token: 0x06012AEF RID: 76527 RVA: 0x002FDF64 File Offset: 0x002FC164
		// Note: this type is marked as 'beforefieldinit'.
		static Point()
		{
			byte[] array = new byte[3];
			Point.attributeNamespaceIds = array;
			Point.eleTagNames = new string[] { "prSet", "spPr", "t", "extLst" };
			Point.eleNamespaceIds = new byte[] { 14, 14, 14, 14 };
		}

		// Token: 0x0400812C RID: 33068
		private const string tagName = "pt";

		// Token: 0x0400812D RID: 33069
		private const byte tagNsId = 14;

		// Token: 0x0400812E RID: 33070
		internal const int ElementTypeIdConst = 10639;

		// Token: 0x0400812F RID: 33071
		private static string[] attributeTagNames = new string[] { "modelId", "type", "cxnId" };

		// Token: 0x04008130 RID: 33072
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008131 RID: 33073
		private static readonly string[] eleTagNames;

		// Token: 0x04008132 RID: 33074
		private static readonly byte[] eleNamespaceIds;
	}
}
