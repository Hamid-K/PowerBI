using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F7 RID: 9975
	[ChildElementInfo(typeof(GreenOffset))]
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
	[ChildElementInfo(typeof(Tint))]
	[ChildElementInfo(typeof(GreenModulation))]
	[ChildElementInfo(typeof(Blue))]
	[ChildElementInfo(typeof(BlueOffset))]
	[ChildElementInfo(typeof(BlueModulation))]
	[ChildElementInfo(typeof(Gamma))]
	[ChildElementInfo(typeof(InverseGamma))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SchemeColor : OpenXmlCompositeElement
	{
		// Token: 0x17005E22 RID: 24098
		// (get) Token: 0x06013084 RID: 77956 RVA: 0x002ECC74 File Offset: 0x002EAE74
		public override string LocalName
		{
			get
			{
				return "schemeClr";
			}
		}

		// Token: 0x17005E23 RID: 24099
		// (get) Token: 0x06013085 RID: 77957 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E24 RID: 24100
		// (get) Token: 0x06013086 RID: 77958 RVA: 0x0030276F File Offset: 0x0030096F
		internal override int ElementTypeId
		{
			get
			{
				return 10039;
			}
		}

		// Token: 0x06013087 RID: 77959 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E25 RID: 24101
		// (get) Token: 0x06013088 RID: 77960 RVA: 0x00302776 File Offset: 0x00300976
		internal override string[] AttributeTagNames
		{
			get
			{
				return SchemeColor.attributeTagNames;
			}
		}

		// Token: 0x17005E26 RID: 24102
		// (get) Token: 0x06013089 RID: 77961 RVA: 0x0030277D File Offset: 0x0030097D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SchemeColor.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E27 RID: 24103
		// (get) Token: 0x0601308A RID: 77962 RVA: 0x00302784 File Offset: 0x00300984
		// (set) Token: 0x0601308B RID: 77963 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<SchemeColorValues> Val
		{
			get
			{
				return (EnumValue<SchemeColorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601308C RID: 77964 RVA: 0x00293ECF File Offset: 0x002920CF
		public SchemeColor()
		{
		}

		// Token: 0x0601308D RID: 77965 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SchemeColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601308E RID: 77966 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SchemeColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601308F RID: 77967 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SchemeColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013090 RID: 77968 RVA: 0x00302794 File Offset: 0x00300994
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

		// Token: 0x06013091 RID: 77969 RVA: 0x00302A42 File Offset: 0x00300C42
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<SchemeColorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013092 RID: 77970 RVA: 0x00302A62 File Offset: 0x00300C62
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SchemeColor>(deep);
		}

		// Token: 0x06013093 RID: 77971 RVA: 0x00302A6C File Offset: 0x00300C6C
		// Note: this type is marked as 'beforefieldinit'.
		static SchemeColor()
		{
			byte[] array = new byte[1];
			SchemeColor.attributeNamespaceIds = array;
		}

		// Token: 0x04008459 RID: 33881
		private const string tagName = "schemeClr";

		// Token: 0x0400845A RID: 33882
		private const byte tagNsId = 10;

		// Token: 0x0400845B RID: 33883
		internal const int ElementTypeIdConst = 10039;

		// Token: 0x0400845C RID: 33884
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400845D RID: 33885
		private static byte[] attributeNamespaceIds;
	}
}
