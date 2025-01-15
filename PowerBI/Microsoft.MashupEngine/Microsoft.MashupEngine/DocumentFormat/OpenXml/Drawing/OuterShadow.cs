using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200271E RID: 10014
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(PresetColor))]
	[GeneratedCode("DomGen", "2.0")]
	internal class OuterShadow : OpenXmlCompositeElement
	{
		// Token: 0x17005F5B RID: 24411
		// (get) Token: 0x06013323 RID: 78627 RVA: 0x00304B27 File Offset: 0x00302D27
		public override string LocalName
		{
			get
			{
				return "outerShdw";
			}
		}

		// Token: 0x17005F5C RID: 24412
		// (get) Token: 0x06013324 RID: 78628 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F5D RID: 24413
		// (get) Token: 0x06013325 RID: 78629 RVA: 0x00304B2E File Offset: 0x00302D2E
		internal override int ElementTypeId
		{
			get
			{
				return 10076;
			}
		}

		// Token: 0x06013326 RID: 78630 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F5E RID: 24414
		// (get) Token: 0x06013327 RID: 78631 RVA: 0x00304B35 File Offset: 0x00302D35
		internal override string[] AttributeTagNames
		{
			get
			{
				return OuterShadow.attributeTagNames;
			}
		}

		// Token: 0x17005F5F RID: 24415
		// (get) Token: 0x06013328 RID: 78632 RVA: 0x00304B3C File Offset: 0x00302D3C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OuterShadow.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F60 RID: 24416
		// (get) Token: 0x06013329 RID: 78633 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x0601332A RID: 78634 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "blurRad")]
		public Int64Value BlurRadius
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005F61 RID: 24417
		// (get) Token: 0x0601332B RID: 78635 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x0601332C RID: 78636 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17005F62 RID: 24418
		// (get) Token: 0x0601332D RID: 78637 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x0601332E RID: 78638 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17005F63 RID: 24419
		// (get) Token: 0x0601332F RID: 78639 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06013330 RID: 78640 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sx")]
		public Int32Value HorizontalRatio
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005F64 RID: 24420
		// (get) Token: 0x06013331 RID: 78641 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x06013332 RID: 78642 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "sy")]
		public Int32Value VerticalRatio
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005F65 RID: 24421
		// (get) Token: 0x06013333 RID: 78643 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x06013334 RID: 78644 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "kx")]
		public Int32Value HorizontalSkew
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005F66 RID: 24422
		// (get) Token: 0x06013335 RID: 78645 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x06013336 RID: 78646 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "ky")]
		public Int32Value VerticalSkew
		{
			get
			{
				return (Int32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17005F67 RID: 24423
		// (get) Token: 0x06013337 RID: 78647 RVA: 0x00304B43 File Offset: 0x00302D43
		// (set) Token: 0x06013338 RID: 78648 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "algn")]
		public EnumValue<RectangleAlignmentValues> Alignment
		{
			get
			{
				return (EnumValue<RectangleAlignmentValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17005F68 RID: 24424
		// (get) Token: 0x06013339 RID: 78649 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0601333A RID: 78650 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "rotWithShape")]
		public BooleanValue RotateWithShape
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x0601333B RID: 78651 RVA: 0x00293ECF File Offset: 0x002920CF
		public OuterShadow()
		{
		}

		// Token: 0x0601333C RID: 78652 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OuterShadow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601333D RID: 78653 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OuterShadow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601333E RID: 78654 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OuterShadow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601333F RID: 78655 RVA: 0x00304B54 File Offset: 0x00302D54
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

		// Token: 0x17005F69 RID: 24425
		// (get) Token: 0x06013340 RID: 78656 RVA: 0x00304BF2 File Offset: 0x00302DF2
		internal override string[] ElementTagNames
		{
			get
			{
				return OuterShadow.eleTagNames;
			}
		}

		// Token: 0x17005F6A RID: 24426
		// (get) Token: 0x06013341 RID: 78657 RVA: 0x00304BF9 File Offset: 0x00302DF9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OuterShadow.eleNamespaceIds;
			}
		}

		// Token: 0x17005F6B RID: 24427
		// (get) Token: 0x06013342 RID: 78658 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005F6C RID: 24428
		// (get) Token: 0x06013343 RID: 78659 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06013344 RID: 78660 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17005F6D RID: 24429
		// (get) Token: 0x06013345 RID: 78661 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06013346 RID: 78662 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17005F6E RID: 24430
		// (get) Token: 0x06013347 RID: 78663 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06013348 RID: 78664 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17005F6F RID: 24431
		// (get) Token: 0x06013349 RID: 78665 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x0601334A RID: 78666 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17005F70 RID: 24432
		// (get) Token: 0x0601334B RID: 78667 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x0601334C RID: 78668 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17005F71 RID: 24433
		// (get) Token: 0x0601334D RID: 78669 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x0601334E RID: 78670 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x0601334F RID: 78671 RVA: 0x00304C00 File Offset: 0x00302E00
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "blurRad" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "dist" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sy" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "kx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "ky" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "algn" == name)
			{
				return new EnumValue<RectangleAlignmentValues>();
			}
			if (namespaceId == 0 && "rotWithShape" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013350 RID: 78672 RVA: 0x00304CDB File Offset: 0x00302EDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OuterShadow>(deep);
		}

		// Token: 0x06013351 RID: 78673 RVA: 0x00304CE4 File Offset: 0x00302EE4
		// Note: this type is marked as 'beforefieldinit'.
		static OuterShadow()
		{
			byte[] array = new byte[9];
			OuterShadow.attributeNamespaceIds = array;
			OuterShadow.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			OuterShadow.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x0400851D RID: 34077
		private const string tagName = "outerShdw";

		// Token: 0x0400851E RID: 34078
		private const byte tagNsId = 10;

		// Token: 0x0400851F RID: 34079
		internal const int ElementTypeIdConst = 10076;

		// Token: 0x04008520 RID: 34080
		private static string[] attributeTagNames = new string[] { "blurRad", "dist", "dir", "sx", "sy", "kx", "ky", "algn", "rotWithShape" };

		// Token: 0x04008521 RID: 34081
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008522 RID: 34082
		private static readonly string[] eleTagNames;

		// Token: 0x04008523 RID: 34083
		private static readonly byte[] eleNamespaceIds;
	}
}
