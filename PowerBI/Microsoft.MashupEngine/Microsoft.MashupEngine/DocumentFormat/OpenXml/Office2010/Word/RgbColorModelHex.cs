using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002496 RID: 9366
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shade), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Alpha), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HueModulation), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Saturation), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SaturationOffset), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SaturationModulation), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Luminance), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LuminanceOffset), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LuminanceModulation), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Tint), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class RgbColorModelHex : OpenXmlCompositeElement
	{
		// Token: 0x17005197 RID: 20887
		// (get) Token: 0x06011499 RID: 70809 RVA: 0x002ECAF6 File Offset: 0x002EACF6
		public override string LocalName
		{
			get
			{
				return "srgbClr";
			}
		}

		// Token: 0x17005198 RID: 20888
		// (get) Token: 0x0601149A RID: 70810 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005199 RID: 20889
		// (get) Token: 0x0601149B RID: 70811 RVA: 0x002ECAFD File Offset: 0x002EACFD
		internal override int ElementTypeId
		{
			get
			{
				return 12842;
			}
		}

		// Token: 0x0601149C RID: 70812 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700519A RID: 20890
		// (get) Token: 0x0601149D RID: 70813 RVA: 0x002ECB04 File Offset: 0x002EAD04
		internal override string[] AttributeTagNames
		{
			get
			{
				return RgbColorModelHex.attributeTagNames;
			}
		}

		// Token: 0x1700519B RID: 20891
		// (get) Token: 0x0601149E RID: 70814 RVA: 0x002ECB0B File Offset: 0x002EAD0B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RgbColorModelHex.attributeNamespaceIds;
			}
		}

		// Token: 0x1700519C RID: 20892
		// (get) Token: 0x0601149F RID: 70815 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x060114A0 RID: 70816 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
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

		// Token: 0x060114A1 RID: 70817 RVA: 0x00293ECF File Offset: 0x002920CF
		public RgbColorModelHex()
		{
		}

		// Token: 0x060114A2 RID: 70818 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RgbColorModelHex(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060114A3 RID: 70819 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RgbColorModelHex(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060114A4 RID: 70820 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RgbColorModelHex(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060114A5 RID: 70821 RVA: 0x002ECB14 File Offset: 0x002EAD14
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "tint" == name)
			{
				return new Tint();
			}
			if (52 == namespaceId && "shade" == name)
			{
				return new Shade();
			}
			if (52 == namespaceId && "alpha" == name)
			{
				return new Alpha();
			}
			if (52 == namespaceId && "hueMod" == name)
			{
				return new HueModulation();
			}
			if (52 == namespaceId && "sat" == name)
			{
				return new Saturation();
			}
			if (52 == namespaceId && "satOff" == name)
			{
				return new SaturationOffset();
			}
			if (52 == namespaceId && "satMod" == name)
			{
				return new SaturationModulation();
			}
			if (52 == namespaceId && "lum" == name)
			{
				return new Luminance();
			}
			if (52 == namespaceId && "lumOff" == name)
			{
				return new LuminanceOffset();
			}
			if (52 == namespaceId && "lumMod" == name)
			{
				return new LuminanceModulation();
			}
			return null;
		}

		// Token: 0x060114A6 RID: 70822 RVA: 0x002ECC12 File Offset: 0x002EAE12
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060114A7 RID: 70823 RVA: 0x002ECC34 File Offset: 0x002EAE34
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RgbColorModelHex>(deep);
		}

		// Token: 0x0400791B RID: 31003
		private const string tagName = "srgbClr";

		// Token: 0x0400791C RID: 31004
		private const byte tagNsId = 52;

		// Token: 0x0400791D RID: 31005
		internal const int ElementTypeIdConst = 12842;

		// Token: 0x0400791E RID: 31006
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400791F RID: 31007
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
