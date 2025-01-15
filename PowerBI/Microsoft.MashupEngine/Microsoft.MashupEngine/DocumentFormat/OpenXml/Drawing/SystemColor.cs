using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F6 RID: 9974
	[ChildElementInfo(typeof(GreenOffset))]
	[ChildElementInfo(typeof(Tint))]
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
	[ChildElementInfo(typeof(GreenModulation))]
	[ChildElementInfo(typeof(Blue))]
	[ChildElementInfo(typeof(BlueOffset))]
	[ChildElementInfo(typeof(BlueModulation))]
	[ChildElementInfo(typeof(Gamma))]
	[ChildElementInfo(typeof(InverseGamma))]
	[GeneratedCode("DomGen", "2.0")]
	public class SystemColor : OpenXmlCompositeElement
	{
		// Token: 0x17005E1B RID: 24091
		// (get) Token: 0x06013072 RID: 77938 RVA: 0x0030241B File Offset: 0x0030061B
		public override string LocalName
		{
			get
			{
				return "sysClr";
			}
		}

		// Token: 0x17005E1C RID: 24092
		// (get) Token: 0x06013073 RID: 77939 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E1D RID: 24093
		// (get) Token: 0x06013074 RID: 77940 RVA: 0x00302422 File Offset: 0x00300622
		internal override int ElementTypeId
		{
			get
			{
				return 10038;
			}
		}

		// Token: 0x06013075 RID: 77941 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E1E RID: 24094
		// (get) Token: 0x06013076 RID: 77942 RVA: 0x00302429 File Offset: 0x00300629
		internal override string[] AttributeTagNames
		{
			get
			{
				return SystemColor.attributeTagNames;
			}
		}

		// Token: 0x17005E1F RID: 24095
		// (get) Token: 0x06013077 RID: 77943 RVA: 0x00302430 File Offset: 0x00300630
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SystemColor.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E20 RID: 24096
		// (get) Token: 0x06013078 RID: 77944 RVA: 0x00302437 File Offset: 0x00300637
		// (set) Token: 0x06013079 RID: 77945 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<SystemColorValues> Val
		{
			get
			{
				return (EnumValue<SystemColorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005E21 RID: 24097
		// (get) Token: 0x0601307A RID: 77946 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x0601307B RID: 77947 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "lastClr")]
		public HexBinaryValue LastColor
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601307C RID: 77948 RVA: 0x00293ECF File Offset: 0x002920CF
		public SystemColor()
		{
		}

		// Token: 0x0601307D RID: 77949 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SystemColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601307E RID: 77950 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SystemColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601307F RID: 77951 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SystemColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013080 RID: 77952 RVA: 0x00302448 File Offset: 0x00300648
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

		// Token: 0x06013081 RID: 77953 RVA: 0x003026F6 File Offset: 0x003008F6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<SystemColorValues>();
			}
			if (namespaceId == 0 && "lastClr" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013082 RID: 77954 RVA: 0x0030272C File Offset: 0x0030092C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SystemColor>(deep);
		}

		// Token: 0x06013083 RID: 77955 RVA: 0x00302738 File Offset: 0x00300938
		// Note: this type is marked as 'beforefieldinit'.
		static SystemColor()
		{
			byte[] array = new byte[2];
			SystemColor.attributeNamespaceIds = array;
		}

		// Token: 0x04008454 RID: 33876
		private const string tagName = "sysClr";

		// Token: 0x04008455 RID: 33877
		private const byte tagNsId = 10;

		// Token: 0x04008456 RID: 33878
		internal const int ElementTypeIdConst = 10038;

		// Token: 0x04008457 RID: 33879
		private static string[] attributeTagNames = new string[] { "val", "lastClr" };

		// Token: 0x04008458 RID: 33880
		private static byte[] attributeNamespaceIds;
	}
}
