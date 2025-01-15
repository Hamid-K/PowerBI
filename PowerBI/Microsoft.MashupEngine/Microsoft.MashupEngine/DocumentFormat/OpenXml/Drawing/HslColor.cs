using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F5 RID: 9973
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
	internal class HslColor : OpenXmlCompositeElement
	{
		// Token: 0x17005E13 RID: 24083
		// (get) Token: 0x0601305E RID: 77918 RVA: 0x003020B0 File Offset: 0x003002B0
		public override string LocalName
		{
			get
			{
				return "hslClr";
			}
		}

		// Token: 0x17005E14 RID: 24084
		// (get) Token: 0x0601305F RID: 77919 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E15 RID: 24085
		// (get) Token: 0x06013060 RID: 77920 RVA: 0x003020B7 File Offset: 0x003002B7
		internal override int ElementTypeId
		{
			get
			{
				return 10037;
			}
		}

		// Token: 0x06013061 RID: 77921 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E16 RID: 24086
		// (get) Token: 0x06013062 RID: 77922 RVA: 0x003020BE File Offset: 0x003002BE
		internal override string[] AttributeTagNames
		{
			get
			{
				return HslColor.attributeTagNames;
			}
		}

		// Token: 0x17005E17 RID: 24087
		// (get) Token: 0x06013063 RID: 77923 RVA: 0x003020C5 File Offset: 0x003002C5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HslColor.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E18 RID: 24088
		// (get) Token: 0x06013064 RID: 77924 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013065 RID: 77925 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "hue")]
		public Int32Value HueValue
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

		// Token: 0x17005E19 RID: 24089
		// (get) Token: 0x06013066 RID: 77926 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013067 RID: 77927 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sat")]
		public Int32Value SatValue
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

		// Token: 0x17005E1A RID: 24090
		// (get) Token: 0x06013068 RID: 77928 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06013069 RID: 77929 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "lum")]
		public Int32Value LumValue
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

		// Token: 0x0601306A RID: 77930 RVA: 0x00293ECF File Offset: 0x002920CF
		public HslColor()
		{
		}

		// Token: 0x0601306B RID: 77931 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HslColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601306C RID: 77932 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HslColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601306D RID: 77933 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HslColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601306E RID: 77934 RVA: 0x003020CC File Offset: 0x003002CC
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

		// Token: 0x0601306F RID: 77935 RVA: 0x0030237C File Offset: 0x0030057C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "hue" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sat" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "lum" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013070 RID: 77936 RVA: 0x003023D3 File Offset: 0x003005D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HslColor>(deep);
		}

		// Token: 0x06013071 RID: 77937 RVA: 0x003023DC File Offset: 0x003005DC
		// Note: this type is marked as 'beforefieldinit'.
		static HslColor()
		{
			byte[] array = new byte[3];
			HslColor.attributeNamespaceIds = array;
		}

		// Token: 0x0400844F RID: 33871
		private const string tagName = "hslClr";

		// Token: 0x04008450 RID: 33872
		private const byte tagNsId = 10;

		// Token: 0x04008451 RID: 33873
		internal const int ElementTypeIdConst = 10037;

		// Token: 0x04008452 RID: 33874
		private static string[] attributeTagNames = new string[] { "hue", "sat", "lum" };

		// Token: 0x04008453 RID: 33875
		private static byte[] attributeNamespaceIds;
	}
}
