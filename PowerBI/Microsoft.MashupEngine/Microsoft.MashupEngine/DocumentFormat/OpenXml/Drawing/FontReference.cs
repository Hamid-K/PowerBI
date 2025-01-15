using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002738 RID: 10040
	[ChildElementInfo(typeof(SystemColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PresetColor))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	internal class FontReference : OpenXmlCompositeElement
	{
		// Token: 0x17006030 RID: 24624
		// (get) Token: 0x060134F4 RID: 79092 RVA: 0x00305F31 File Offset: 0x00304131
		public override string LocalName
		{
			get
			{
				return "fontRef";
			}
		}

		// Token: 0x17006031 RID: 24625
		// (get) Token: 0x060134F5 RID: 79093 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006032 RID: 24626
		// (get) Token: 0x060134F6 RID: 79094 RVA: 0x00305F38 File Offset: 0x00304138
		internal override int ElementTypeId
		{
			get
			{
				return 10098;
			}
		}

		// Token: 0x060134F7 RID: 79095 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006033 RID: 24627
		// (get) Token: 0x060134F8 RID: 79096 RVA: 0x00305F3F File Offset: 0x0030413F
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontReference.attributeTagNames;
			}
		}

		// Token: 0x17006034 RID: 24628
		// (get) Token: 0x060134F9 RID: 79097 RVA: 0x00305F46 File Offset: 0x00304146
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontReference.attributeNamespaceIds;
			}
		}

		// Token: 0x17006035 RID: 24629
		// (get) Token: 0x060134FA RID: 79098 RVA: 0x00305F4D File Offset: 0x0030414D
		// (set) Token: 0x060134FB RID: 79099 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idx")]
		public EnumValue<FontCollectionIndexValues> Index
		{
			get
			{
				return (EnumValue<FontCollectionIndexValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060134FC RID: 79100 RVA: 0x00293ECF File Offset: 0x002920CF
		public FontReference()
		{
		}

		// Token: 0x060134FD RID: 79101 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FontReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134FE RID: 79102 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FontReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134FF RID: 79103 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FontReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013500 RID: 79104 RVA: 0x00305F5C File Offset: 0x0030415C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "scrgbClr" == name)
			{
				return new RgbColorModelPercentage();
			}
			if (10 == namespaceId && "srgbClr" == name)
			{
				return new RgbColorModelHex();
			}
			if (10 == namespaceId && "hslClr" == name)
			{
				return new HslColor();
			}
			if (10 == namespaceId && "sysClr" == name)
			{
				return new SystemColor();
			}
			if (10 == namespaceId && "schemeClr" == name)
			{
				return new SchemeColor();
			}
			if (10 == namespaceId && "prstClr" == name)
			{
				return new PresetColor();
			}
			return null;
		}

		// Token: 0x17006036 RID: 24630
		// (get) Token: 0x06013501 RID: 79105 RVA: 0x00305FFA File Offset: 0x003041FA
		internal override string[] ElementTagNames
		{
			get
			{
				return FontReference.eleTagNames;
			}
		}

		// Token: 0x17006037 RID: 24631
		// (get) Token: 0x06013502 RID: 79106 RVA: 0x00306001 File Offset: 0x00304201
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FontReference.eleNamespaceIds;
			}
		}

		// Token: 0x17006038 RID: 24632
		// (get) Token: 0x06013503 RID: 79107 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17006039 RID: 24633
		// (get) Token: 0x06013504 RID: 79108 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06013505 RID: 79109 RVA: 0x002E499D File Offset: 0x002E2B9D
		public RgbColorModelPercentage RgbColorModelPercentage
		{
			get
			{
				return base.GetElement<RgbColorModelPercentage>(0);
			}
			set
			{
				base.SetElement<RgbColorModelPercentage>(0, value);
			}
		}

		// Token: 0x1700603A RID: 24634
		// (get) Token: 0x06013506 RID: 79110 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06013507 RID: 79111 RVA: 0x002E49B0 File Offset: 0x002E2BB0
		public RgbColorModelHex RgbColorModelHex
		{
			get
			{
				return base.GetElement<RgbColorModelHex>(1);
			}
			set
			{
				base.SetElement<RgbColorModelHex>(1, value);
			}
		}

		// Token: 0x1700603B RID: 24635
		// (get) Token: 0x06013508 RID: 79112 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06013509 RID: 79113 RVA: 0x002E49C3 File Offset: 0x002E2BC3
		public HslColor HslColor
		{
			get
			{
				return base.GetElement<HslColor>(2);
			}
			set
			{
				base.SetElement<HslColor>(2, value);
			}
		}

		// Token: 0x1700603C RID: 24636
		// (get) Token: 0x0601350A RID: 79114 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x0601350B RID: 79115 RVA: 0x002E49D6 File Offset: 0x002E2BD6
		public SystemColor SystemColor
		{
			get
			{
				return base.GetElement<SystemColor>(3);
			}
			set
			{
				base.SetElement<SystemColor>(3, value);
			}
		}

		// Token: 0x1700603D RID: 24637
		// (get) Token: 0x0601350C RID: 79116 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x0601350D RID: 79117 RVA: 0x002E49E9 File Offset: 0x002E2BE9
		public SchemeColor SchemeColor
		{
			get
			{
				return base.GetElement<SchemeColor>(4);
			}
			set
			{
				base.SetElement<SchemeColor>(4, value);
			}
		}

		// Token: 0x1700603E RID: 24638
		// (get) Token: 0x0601350E RID: 79118 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x0601350F RID: 79119 RVA: 0x002E49FC File Offset: 0x002E2BFC
		public PresetColor PresetColor
		{
			get
			{
				return base.GetElement<PresetColor>(5);
			}
			set
			{
				base.SetElement<PresetColor>(5, value);
			}
		}

		// Token: 0x06013510 RID: 79120 RVA: 0x00306008 File Offset: 0x00304208
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idx" == name)
			{
				return new EnumValue<FontCollectionIndexValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013511 RID: 79121 RVA: 0x00306028 File Offset: 0x00304228
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontReference>(deep);
		}

		// Token: 0x06013512 RID: 79122 RVA: 0x00306034 File Offset: 0x00304234
		// Note: this type is marked as 'beforefieldinit'.
		static FontReference()
		{
			byte[] array = new byte[1];
			FontReference.attributeNamespaceIds = array;
			FontReference.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			FontReference.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x04008591 RID: 34193
		private const string tagName = "fontRef";

		// Token: 0x04008592 RID: 34194
		private const byte tagNsId = 10;

		// Token: 0x04008593 RID: 34195
		internal const int ElementTypeIdConst = 10098;

		// Token: 0x04008594 RID: 34196
		private static string[] attributeTagNames = new string[] { "idx" };

		// Token: 0x04008595 RID: 34197
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008596 RID: 34198
		private static readonly string[] eleTagNames;

		// Token: 0x04008597 RID: 34199
		private static readonly byte[] eleNamespaceIds;
	}
}
