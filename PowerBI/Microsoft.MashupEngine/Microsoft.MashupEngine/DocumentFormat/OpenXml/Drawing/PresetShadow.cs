using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200271F RID: 10015
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(PresetColor))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PresetShadow : OpenXmlCompositeElement
	{
		// Token: 0x17005F72 RID: 24434
		// (get) Token: 0x06013352 RID: 78674 RVA: 0x00304DA8 File Offset: 0x00302FA8
		public override string LocalName
		{
			get
			{
				return "prstShdw";
			}
		}

		// Token: 0x17005F73 RID: 24435
		// (get) Token: 0x06013353 RID: 78675 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F74 RID: 24436
		// (get) Token: 0x06013354 RID: 78676 RVA: 0x00304DAF File Offset: 0x00302FAF
		internal override int ElementTypeId
		{
			get
			{
				return 10077;
			}
		}

		// Token: 0x06013355 RID: 78677 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F75 RID: 24437
		// (get) Token: 0x06013356 RID: 78678 RVA: 0x00304DB6 File Offset: 0x00302FB6
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresetShadow.attributeTagNames;
			}
		}

		// Token: 0x17005F76 RID: 24438
		// (get) Token: 0x06013357 RID: 78679 RVA: 0x00304DBD File Offset: 0x00302FBD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresetShadow.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F77 RID: 24439
		// (get) Token: 0x06013358 RID: 78680 RVA: 0x00304DC4 File Offset: 0x00302FC4
		// (set) Token: 0x06013359 RID: 78681 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "prst")]
		public EnumValue<PresetShadowValues> Preset
		{
			get
			{
				return (EnumValue<PresetShadowValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005F78 RID: 24440
		// (get) Token: 0x0601335A RID: 78682 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x0601335B RID: 78683 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dist")]
		public Int64Value Distance
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005F79 RID: 24441
		// (get) Token: 0x0601335C RID: 78684 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x0601335D RID: 78685 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dir")]
		public Int32Value Direction
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601335E RID: 78686 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresetShadow()
		{
		}

		// Token: 0x0601335F RID: 78687 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresetShadow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013360 RID: 78688 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresetShadow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013361 RID: 78689 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresetShadow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013362 RID: 78690 RVA: 0x00304DD4 File Offset: 0x00302FD4
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

		// Token: 0x17005F7A RID: 24442
		// (get) Token: 0x06013363 RID: 78691 RVA: 0x00304E72 File Offset: 0x00303072
		internal override string[] ElementTagNames
		{
			get
			{
				return PresetShadow.eleTagNames;
			}
		}

		// Token: 0x17005F7B RID: 24443
		// (get) Token: 0x06013364 RID: 78692 RVA: 0x00304E79 File Offset: 0x00303079
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PresetShadow.eleNamespaceIds;
			}
		}

		// Token: 0x17005F7C RID: 24444
		// (get) Token: 0x06013365 RID: 78693 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005F7D RID: 24445
		// (get) Token: 0x06013366 RID: 78694 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06013367 RID: 78695 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17005F7E RID: 24446
		// (get) Token: 0x06013368 RID: 78696 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06013369 RID: 78697 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17005F7F RID: 24447
		// (get) Token: 0x0601336A RID: 78698 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x0601336B RID: 78699 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17005F80 RID: 24448
		// (get) Token: 0x0601336C RID: 78700 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x0601336D RID: 78701 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17005F81 RID: 24449
		// (get) Token: 0x0601336E RID: 78702 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x0601336F RID: 78703 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17005F82 RID: 24450
		// (get) Token: 0x06013370 RID: 78704 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06013371 RID: 78705 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x06013372 RID: 78706 RVA: 0x00304E80 File Offset: 0x00303080
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "prst" == name)
			{
				return new EnumValue<PresetShadowValues>();
			}
			if (namespaceId == 0 && "dist" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013373 RID: 78707 RVA: 0x00304ED7 File Offset: 0x003030D7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresetShadow>(deep);
		}

		// Token: 0x06013374 RID: 78708 RVA: 0x00304EE0 File Offset: 0x003030E0
		// Note: this type is marked as 'beforefieldinit'.
		static PresetShadow()
		{
			byte[] array = new byte[3];
			PresetShadow.attributeNamespaceIds = array;
			PresetShadow.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			PresetShadow.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x04008524 RID: 34084
		private const string tagName = "prstShdw";

		// Token: 0x04008525 RID: 34085
		private const byte tagNsId = 10;

		// Token: 0x04008526 RID: 34086
		internal const int ElementTypeIdConst = 10077;

		// Token: 0x04008527 RID: 34087
		private static string[] attributeTagNames = new string[] { "prst", "dist", "dir" };

		// Token: 0x04008528 RID: 34088
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008529 RID: 34089
		private static readonly string[] eleTagNames;

		// Token: 0x0400852A RID: 34090
		private static readonly byte[] eleNamespaceIds;
	}
}
