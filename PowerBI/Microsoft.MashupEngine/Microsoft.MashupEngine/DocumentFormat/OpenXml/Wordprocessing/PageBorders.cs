using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E46 RID: 11846
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LeftBorder))]
	[ChildElementInfo(typeof(RightBorder))]
	internal class PageBorders : OpenXmlCompositeElement
	{
		// Token: 0x170089EC RID: 35308
		// (get) Token: 0x060192A7 RID: 103079 RVA: 0x00347099 File Offset: 0x00345299
		public override string LocalName
		{
			get
			{
				return "pgBorders";
			}
		}

		// Token: 0x170089ED RID: 35309
		// (get) Token: 0x060192A8 RID: 103080 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089EE RID: 35310
		// (get) Token: 0x060192A9 RID: 103081 RVA: 0x003470A0 File Offset: 0x003452A0
		internal override int ElementTypeId
		{
			get
			{
				return 11532;
			}
		}

		// Token: 0x060192AA RID: 103082 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170089EF RID: 35311
		// (get) Token: 0x060192AB RID: 103083 RVA: 0x003470A7 File Offset: 0x003452A7
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageBorders.attributeTagNames;
			}
		}

		// Token: 0x170089F0 RID: 35312
		// (get) Token: 0x060192AC RID: 103084 RVA: 0x003470AE File Offset: 0x003452AE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageBorders.attributeNamespaceIds;
			}
		}

		// Token: 0x170089F1 RID: 35313
		// (get) Token: 0x060192AD RID: 103085 RVA: 0x003470B5 File Offset: 0x003452B5
		// (set) Token: 0x060192AE RID: 103086 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "zOrder")]
		public EnumValue<PageBorderZOrderValues> ZOrder
		{
			get
			{
				return (EnumValue<PageBorderZOrderValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170089F2 RID: 35314
		// (get) Token: 0x060192AF RID: 103087 RVA: 0x003470C4 File Offset: 0x003452C4
		// (set) Token: 0x060192B0 RID: 103088 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "display")]
		public EnumValue<PageBorderDisplayValues> Display
		{
			get
			{
				return (EnumValue<PageBorderDisplayValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170089F3 RID: 35315
		// (get) Token: 0x060192B1 RID: 103089 RVA: 0x003470D3 File Offset: 0x003452D3
		// (set) Token: 0x060192B2 RID: 103090 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "offsetFrom")]
		public EnumValue<PageBorderOffsetValues> OffsetFrom
		{
			get
			{
				return (EnumValue<PageBorderOffsetValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060192B3 RID: 103091 RVA: 0x00293ECF File Offset: 0x002920CF
		public PageBorders()
		{
		}

		// Token: 0x060192B4 RID: 103092 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PageBorders(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060192B5 RID: 103093 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PageBorders(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060192B6 RID: 103094 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PageBorders(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060192B7 RID: 103095 RVA: 0x003470E4 File Offset: 0x003452E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "top" == name)
			{
				return new TopBorder();
			}
			if (23 == namespaceId && "left" == name)
			{
				return new LeftBorder();
			}
			if (23 == namespaceId && "bottom" == name)
			{
				return new BottomBorder();
			}
			if (23 == namespaceId && "right" == name)
			{
				return new RightBorder();
			}
			return null;
		}

		// Token: 0x170089F4 RID: 35316
		// (get) Token: 0x060192B8 RID: 103096 RVA: 0x00347152 File Offset: 0x00345352
		internal override string[] ElementTagNames
		{
			get
			{
				return PageBorders.eleTagNames;
			}
		}

		// Token: 0x170089F5 RID: 35317
		// (get) Token: 0x060192B9 RID: 103097 RVA: 0x00347159 File Offset: 0x00345359
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PageBorders.eleNamespaceIds;
			}
		}

		// Token: 0x170089F6 RID: 35318
		// (get) Token: 0x060192BA RID: 103098 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170089F7 RID: 35319
		// (get) Token: 0x060192BB RID: 103099 RVA: 0x00345F0C File Offset: 0x0034410C
		// (set) Token: 0x060192BC RID: 103100 RVA: 0x00345F15 File Offset: 0x00344115
		public TopBorder TopBorder
		{
			get
			{
				return base.GetElement<TopBorder>(0);
			}
			set
			{
				base.SetElement<TopBorder>(0, value);
			}
		}

		// Token: 0x170089F8 RID: 35320
		// (get) Token: 0x060192BD RID: 103101 RVA: 0x00345F1F File Offset: 0x0034411F
		// (set) Token: 0x060192BE RID: 103102 RVA: 0x00345F28 File Offset: 0x00344128
		public LeftBorder LeftBorder
		{
			get
			{
				return base.GetElement<LeftBorder>(1);
			}
			set
			{
				base.SetElement<LeftBorder>(1, value);
			}
		}

		// Token: 0x170089F9 RID: 35321
		// (get) Token: 0x060192BF RID: 103103 RVA: 0x00345F32 File Offset: 0x00344132
		// (set) Token: 0x060192C0 RID: 103104 RVA: 0x00345F3B File Offset: 0x0034413B
		public BottomBorder BottomBorder
		{
			get
			{
				return base.GetElement<BottomBorder>(2);
			}
			set
			{
				base.SetElement<BottomBorder>(2, value);
			}
		}

		// Token: 0x170089FA RID: 35322
		// (get) Token: 0x060192C1 RID: 103105 RVA: 0x00345F45 File Offset: 0x00344145
		// (set) Token: 0x060192C2 RID: 103106 RVA: 0x00345F4E File Offset: 0x0034414E
		public RightBorder RightBorder
		{
			get
			{
				return base.GetElement<RightBorder>(3);
			}
			set
			{
				base.SetElement<RightBorder>(3, value);
			}
		}

		// Token: 0x060192C3 RID: 103107 RVA: 0x00347160 File Offset: 0x00345360
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "zOrder" == name)
			{
				return new EnumValue<PageBorderZOrderValues>();
			}
			if (23 == namespaceId && "display" == name)
			{
				return new EnumValue<PageBorderDisplayValues>();
			}
			if (23 == namespaceId && "offsetFrom" == name)
			{
				return new EnumValue<PageBorderOffsetValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060192C4 RID: 103108 RVA: 0x003471BD File Offset: 0x003453BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageBorders>(deep);
		}

		// Token: 0x0400A75F RID: 42847
		private const string tagName = "pgBorders";

		// Token: 0x0400A760 RID: 42848
		private const byte tagNsId = 23;

		// Token: 0x0400A761 RID: 42849
		internal const int ElementTypeIdConst = 11532;

		// Token: 0x0400A762 RID: 42850
		private static string[] attributeTagNames = new string[] { "zOrder", "display", "offsetFrom" };

		// Token: 0x0400A763 RID: 42851
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400A764 RID: 42852
		private static readonly string[] eleTagNames = new string[] { "top", "left", "bottom", "right" };

		// Token: 0x0400A765 RID: 42853
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
