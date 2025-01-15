using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F3 RID: 9971
	[ChildElementInfo(typeof(Tint))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shade))]
	[ChildElementInfo(typeof(Complement))]
	[ChildElementInfo(typeof(Inverse))]
	[ChildElementInfo(typeof(Gray))]
	[ChildElementInfo(typeof(Alpha))]
	[ChildElementInfo(typeof(AlphaOffset))]
	[ChildElementInfo(typeof(AlphaModulation))]
	[ChildElementInfo(typeof(Hue))]
	[ChildElementInfo(typeof(HueOffset))]
	[ChildElementInfo(typeof(HueModulation))]
	[ChildElementInfo(typeof(Saturation))]
	[ChildElementInfo(typeof(SaturationOffset))]
	[ChildElementInfo(typeof(SaturationModulation))]
	[ChildElementInfo(typeof(Luminance))]
	[ChildElementInfo(typeof(LuminanceOffset))]
	[ChildElementInfo(typeof(LuminanceModulation))]
	[ChildElementInfo(typeof(Red))]
	[ChildElementInfo(typeof(RedOffset))]
	[ChildElementInfo(typeof(RedModulation))]
	[ChildElementInfo(typeof(Green))]
	[ChildElementInfo(typeof(GreenOffset))]
	[ChildElementInfo(typeof(GreenModulation))]
	[ChildElementInfo(typeof(Blue))]
	[ChildElementInfo(typeof(BlueOffset))]
	[ChildElementInfo(typeof(BlueModulation))]
	[ChildElementInfo(typeof(Gamma))]
	[ChildElementInfo(typeof(InverseGamma))]
	internal class RgbColorModelPercentage : OpenXmlCompositeElement
	{
		// Token: 0x17005E04 RID: 24068
		// (get) Token: 0x06013038 RID: 77880 RVA: 0x00301A03 File Offset: 0x002FFC03
		public override string LocalName
		{
			get
			{
				return "scrgbClr";
			}
		}

		// Token: 0x17005E05 RID: 24069
		// (get) Token: 0x06013039 RID: 77881 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E06 RID: 24070
		// (get) Token: 0x0601303A RID: 77882 RVA: 0x00301A0A File Offset: 0x002FFC0A
		internal override int ElementTypeId
		{
			get
			{
				return 10035;
			}
		}

		// Token: 0x0601303B RID: 77883 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E07 RID: 24071
		// (get) Token: 0x0601303C RID: 77884 RVA: 0x00301A11 File Offset: 0x002FFC11
		internal override string[] AttributeTagNames
		{
			get
			{
				return RgbColorModelPercentage.attributeTagNames;
			}
		}

		// Token: 0x17005E08 RID: 24072
		// (get) Token: 0x0601303D RID: 77885 RVA: 0x00301A18 File Offset: 0x002FFC18
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RgbColorModelPercentage.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E09 RID: 24073
		// (get) Token: 0x0601303E RID: 77886 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601303F RID: 77887 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public Int32Value RedPortion
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005E0A RID: 24074
		// (get) Token: 0x06013040 RID: 77888 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013041 RID: 77889 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "g")]
		public Int32Value GreenPortion
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005E0B RID: 24075
		// (get) Token: 0x06013042 RID: 77890 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06013043 RID: 77891 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "b")]
		public Int32Value BluePortion
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

		// Token: 0x06013044 RID: 77892 RVA: 0x00293ECF File Offset: 0x002920CF
		public RgbColorModelPercentage()
		{
		}

		// Token: 0x06013045 RID: 77893 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RgbColorModelPercentage(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013046 RID: 77894 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RgbColorModelPercentage(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013047 RID: 77895 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RgbColorModelPercentage(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013048 RID: 77896 RVA: 0x00301A20 File Offset: 0x002FFC20
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "tint" == name)
			{
				return new Tint();
			}
			if (10 == namespaceId && "shade" == name)
			{
				return new Shade();
			}
			if (10 == namespaceId && "comp" == name)
			{
				return new Complement();
			}
			if (10 == namespaceId && "inv" == name)
			{
				return new Inverse();
			}
			if (10 == namespaceId && "gray" == name)
			{
				return new Gray();
			}
			if (10 == namespaceId && "alpha" == name)
			{
				return new Alpha();
			}
			if (10 == namespaceId && "alphaOff" == name)
			{
				return new AlphaOffset();
			}
			if (10 == namespaceId && "alphaMod" == name)
			{
				return new AlphaModulation();
			}
			if (10 == namespaceId && "hue" == name)
			{
				return new Hue();
			}
			if (10 == namespaceId && "hueOff" == name)
			{
				return new HueOffset();
			}
			if (10 == namespaceId && "hueMod" == name)
			{
				return new HueModulation();
			}
			if (10 == namespaceId && "sat" == name)
			{
				return new Saturation();
			}
			if (10 == namespaceId && "satOff" == name)
			{
				return new SaturationOffset();
			}
			if (10 == namespaceId && "satMod" == name)
			{
				return new SaturationModulation();
			}
			if (10 == namespaceId && "lum" == name)
			{
				return new Luminance();
			}
			if (10 == namespaceId && "lumOff" == name)
			{
				return new LuminanceOffset();
			}
			if (10 == namespaceId && "lumMod" == name)
			{
				return new LuminanceModulation();
			}
			if (10 == namespaceId && "red" == name)
			{
				return new Red();
			}
			if (10 == namespaceId && "redOff" == name)
			{
				return new RedOffset();
			}
			if (10 == namespaceId && "redMod" == name)
			{
				return new RedModulation();
			}
			if (10 == namespaceId && "green" == name)
			{
				return new Green();
			}
			if (10 == namespaceId && "greenOff" == name)
			{
				return new GreenOffset();
			}
			if (10 == namespaceId && "greenMod" == name)
			{
				return new GreenModulation();
			}
			if (10 == namespaceId && "blue" == name)
			{
				return new Blue();
			}
			if (10 == namespaceId && "blueOff" == name)
			{
				return new BlueOffset();
			}
			if (10 == namespaceId && "blueMod" == name)
			{
				return new BlueModulation();
			}
			if (10 == namespaceId && "gamma" == name)
			{
				return new Gamma();
			}
			if (10 == namespaceId && "invGamma" == name)
			{
				return new InverseGamma();
			}
			return null;
		}

		// Token: 0x06013049 RID: 77897 RVA: 0x00301CD0 File Offset: 0x002FFED0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "g" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601304A RID: 77898 RVA: 0x00301D27 File Offset: 0x002FFF27
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RgbColorModelPercentage>(deep);
		}

		// Token: 0x0601304B RID: 77899 RVA: 0x00301D30 File Offset: 0x002FFF30
		// Note: this type is marked as 'beforefieldinit'.
		static RgbColorModelPercentage()
		{
			byte[] array = new byte[3];
			RgbColorModelPercentage.attributeNamespaceIds = array;
		}

		// Token: 0x04008445 RID: 33861
		private const string tagName = "scrgbClr";

		// Token: 0x04008446 RID: 33862
		private const byte tagNsId = 10;

		// Token: 0x04008447 RID: 33863
		internal const int ElementTypeIdConst = 10035;

		// Token: 0x04008448 RID: 33864
		private static string[] attributeTagNames = new string[] { "r", "g", "b" };

		// Token: 0x04008449 RID: 33865
		private static byte[] attributeNamespaceIds;
	}
}
