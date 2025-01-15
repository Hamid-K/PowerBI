using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002770 RID: 10096
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	internal class CustomColor : OpenXmlCompositeElement
	{
		// Token: 0x17006144 RID: 24900
		// (get) Token: 0x0601377D RID: 79741 RVA: 0x00307631 File Offset: 0x00305831
		public override string LocalName
		{
			get
			{
				return "custClr";
			}
		}

		// Token: 0x17006145 RID: 24901
		// (get) Token: 0x0601377E RID: 79742 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006146 RID: 24902
		// (get) Token: 0x0601377F RID: 79743 RVA: 0x00307638 File Offset: 0x00305838
		internal override int ElementTypeId
		{
			get
			{
				return 10130;
			}
		}

		// Token: 0x06013780 RID: 79744 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006147 RID: 24903
		// (get) Token: 0x06013781 RID: 79745 RVA: 0x0030763F File Offset: 0x0030583F
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomColor.attributeTagNames;
			}
		}

		// Token: 0x17006148 RID: 24904
		// (get) Token: 0x06013782 RID: 79746 RVA: 0x00307646 File Offset: 0x00305846
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomColor.attributeNamespaceIds;
			}
		}

		// Token: 0x17006149 RID: 24905
		// (get) Token: 0x06013783 RID: 79747 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013784 RID: 79748 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x06013785 RID: 79749 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomColor()
		{
		}

		// Token: 0x06013786 RID: 79750 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013787 RID: 79751 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013788 RID: 79752 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013789 RID: 79753 RVA: 0x00307650 File Offset: 0x00305850
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

		// Token: 0x1700614A RID: 24906
		// (get) Token: 0x0601378A RID: 79754 RVA: 0x003076EE File Offset: 0x003058EE
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomColor.eleTagNames;
			}
		}

		// Token: 0x1700614B RID: 24907
		// (get) Token: 0x0601378B RID: 79755 RVA: 0x003076F5 File Offset: 0x003058F5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomColor.eleNamespaceIds;
			}
		}

		// Token: 0x1700614C RID: 24908
		// (get) Token: 0x0601378C RID: 79756 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700614D RID: 24909
		// (get) Token: 0x0601378D RID: 79757 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x0601378E RID: 79758 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x1700614E RID: 24910
		// (get) Token: 0x0601378F RID: 79759 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06013790 RID: 79760 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x1700614F RID: 24911
		// (get) Token: 0x06013791 RID: 79761 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06013792 RID: 79762 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17006150 RID: 24912
		// (get) Token: 0x06013793 RID: 79763 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x06013794 RID: 79764 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17006151 RID: 24913
		// (get) Token: 0x06013795 RID: 79765 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x06013796 RID: 79766 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17006152 RID: 24914
		// (get) Token: 0x06013797 RID: 79767 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06013798 RID: 79768 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x06013799 RID: 79769 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601379A RID: 79770 RVA: 0x003076FC File Offset: 0x003058FC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomColor>(deep);
		}

		// Token: 0x0601379B RID: 79771 RVA: 0x00307708 File Offset: 0x00305908
		// Note: this type is marked as 'beforefieldinit'.
		static CustomColor()
		{
			byte[] array = new byte[1];
			CustomColor.attributeNamespaceIds = array;
			CustomColor.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			CustomColor.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x0400865A RID: 34394
		private const string tagName = "custClr";

		// Token: 0x0400865B RID: 34395
		private const byte tagNsId = 10;

		// Token: 0x0400865C RID: 34396
		internal const int ElementTypeIdConst = 10130;

		// Token: 0x0400865D RID: 34397
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x0400865E RID: 34398
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400865F RID: 34399
		private static readonly string[] eleTagNames;

		// Token: 0x04008660 RID: 34400
		private static readonly byte[] eleNamespaceIds;
	}
}
