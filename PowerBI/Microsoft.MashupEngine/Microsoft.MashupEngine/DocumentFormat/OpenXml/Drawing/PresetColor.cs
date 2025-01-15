using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F8 RID: 9976
	[ChildElementInfo(typeof(AlphaModulation))]
	[ChildElementInfo(typeof(Shade))]
	[ChildElementInfo(typeof(Complement))]
	[ChildElementInfo(typeof(Inverse))]
	[ChildElementInfo(typeof(Gray))]
	[ChildElementInfo(typeof(Alpha))]
	[ChildElementInfo(typeof(AlphaOffset))]
	[ChildElementInfo(typeof(Tint))]
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
	[GeneratedCode("DomGen", "2.0")]
	internal class PresetColor : OpenXmlCompositeElement
	{
		// Token: 0x17005E28 RID: 24104
		// (get) Token: 0x06013094 RID: 77972 RVA: 0x00302A9B File Offset: 0x00300C9B
		public override string LocalName
		{
			get
			{
				return "prstClr";
			}
		}

		// Token: 0x17005E29 RID: 24105
		// (get) Token: 0x06013095 RID: 77973 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E2A RID: 24106
		// (get) Token: 0x06013096 RID: 77974 RVA: 0x00302AA2 File Offset: 0x00300CA2
		internal override int ElementTypeId
		{
			get
			{
				return 10040;
			}
		}

		// Token: 0x06013097 RID: 77975 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E2B RID: 24107
		// (get) Token: 0x06013098 RID: 77976 RVA: 0x00302AA9 File Offset: 0x00300CA9
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresetColor.attributeTagNames;
			}
		}

		// Token: 0x17005E2C RID: 24108
		// (get) Token: 0x06013099 RID: 77977 RVA: 0x00302AB0 File Offset: 0x00300CB0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresetColor.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E2D RID: 24109
		// (get) Token: 0x0601309A RID: 77978 RVA: 0x00302AB7 File Offset: 0x00300CB7
		// (set) Token: 0x0601309B RID: 77979 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<PresetColorValues> Val
		{
			get
			{
				return (EnumValue<PresetColorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601309C RID: 77980 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresetColor()
		{
		}

		// Token: 0x0601309D RID: 77981 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresetColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601309E RID: 77982 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresetColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601309F RID: 77983 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresetColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060130A0 RID: 77984 RVA: 0x00302AC8 File Offset: 0x00300CC8
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

		// Token: 0x060130A1 RID: 77985 RVA: 0x00302D76 File Offset: 0x00300F76
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<PresetColorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060130A2 RID: 77986 RVA: 0x00302D96 File Offset: 0x00300F96
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresetColor>(deep);
		}

		// Token: 0x060130A3 RID: 77987 RVA: 0x00302DA0 File Offset: 0x00300FA0
		// Note: this type is marked as 'beforefieldinit'.
		static PresetColor()
		{
			byte[] array = new byte[1];
			PresetColor.attributeNamespaceIds = array;
		}

		// Token: 0x0400845E RID: 33886
		private const string tagName = "prstClr";

		// Token: 0x0400845F RID: 33887
		private const byte tagNsId = 10;

		// Token: 0x04008460 RID: 33888
		internal const int ElementTypeIdConst = 10040;

		// Token: 0x04008461 RID: 33889
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04008462 RID: 33890
		private static byte[] attributeNamespaceIds;
	}
}
