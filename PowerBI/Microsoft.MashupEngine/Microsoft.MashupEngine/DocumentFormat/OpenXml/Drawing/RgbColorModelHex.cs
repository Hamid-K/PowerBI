using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F4 RID: 9972
	[ChildElementInfo(typeof(SaturationModulation))]
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
	internal class RgbColorModelHex : OpenXmlCompositeElement
	{
		// Token: 0x17005E0C RID: 24076
		// (get) Token: 0x0601304C RID: 77900 RVA: 0x002ECAF6 File Offset: 0x002EACF6
		public override string LocalName
		{
			get
			{
				return "srgbClr";
			}
		}

		// Token: 0x17005E0D RID: 24077
		// (get) Token: 0x0601304D RID: 77901 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E0E RID: 24078
		// (get) Token: 0x0601304E RID: 77902 RVA: 0x00301D6F File Offset: 0x002FFF6F
		internal override int ElementTypeId
		{
			get
			{
				return 10036;
			}
		}

		// Token: 0x0601304F RID: 77903 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E0F RID: 24079
		// (get) Token: 0x06013050 RID: 77904 RVA: 0x00301D76 File Offset: 0x002FFF76
		internal override string[] AttributeTagNames
		{
			get
			{
				return RgbColorModelHex.attributeTagNames;
			}
		}

		// Token: 0x17005E10 RID: 24080
		// (get) Token: 0x06013051 RID: 77905 RVA: 0x00301D7D File Offset: 0x002FFF7D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RgbColorModelHex.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E11 RID: 24081
		// (get) Token: 0x06013052 RID: 77906 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06013053 RID: 77907 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public HexBinaryValue Val
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005E12 RID: 24082
		// (get) Token: 0x06013054 RID: 77908 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06013055 RID: 77909 RVA: 0x002BD47A File Offset: 0x002BB67A
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(48, "legacySpreadsheetColorIndex")]
		public Int32Value LegacySpreadsheetColorIndex
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

		// Token: 0x06013056 RID: 77910 RVA: 0x00293ECF File Offset: 0x002920CF
		public RgbColorModelHex()
		{
		}

		// Token: 0x06013057 RID: 77911 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RgbColorModelHex(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013058 RID: 77912 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RgbColorModelHex(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013059 RID: 77913 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RgbColorModelHex(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601305A RID: 77914 RVA: 0x00301D84 File Offset: 0x002FFF84
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

		// Token: 0x0601305B RID: 77915 RVA: 0x00302032 File Offset: 0x00300232
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new HexBinaryValue();
			}
			if (48 == namespaceId && "legacySpreadsheetColorIndex" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601305C RID: 77916 RVA: 0x0030206A File Offset: 0x0030026A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RgbColorModelHex>(deep);
		}

		// Token: 0x0400844A RID: 33866
		private const string tagName = "srgbClr";

		// Token: 0x0400844B RID: 33867
		private const byte tagNsId = 10;

		// Token: 0x0400844C RID: 33868
		internal const int ElementTypeIdConst = 10036;

		// Token: 0x0400844D RID: 33869
		private static string[] attributeTagNames = new string[] { "val", "legacySpreadsheetColorIndex" };

		// Token: 0x0400844E RID: 33870
		private static byte[] attributeNamespaceIds = new byte[] { 0, 48 };
	}
}
