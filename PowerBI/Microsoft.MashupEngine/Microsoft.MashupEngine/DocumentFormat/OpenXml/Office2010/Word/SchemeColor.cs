using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002497 RID: 9367
	[ChildElementInfo(typeof(Shade), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Tint), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Alpha), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HueModulation), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Saturation), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SaturationOffset), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SaturationModulation), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Luminance), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LuminanceOffset), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LuminanceModulation), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SchemeColor : OpenXmlCompositeElement
	{
		// Token: 0x1700519D RID: 20893
		// (get) Token: 0x060114A9 RID: 70825 RVA: 0x002ECC74 File Offset: 0x002EAE74
		public override string LocalName
		{
			get
			{
				return "schemeClr";
			}
		}

		// Token: 0x1700519E RID: 20894
		// (get) Token: 0x060114AA RID: 70826 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700519F RID: 20895
		// (get) Token: 0x060114AB RID: 70827 RVA: 0x002ECC7B File Offset: 0x002EAE7B
		internal override int ElementTypeId
		{
			get
			{
				return 12843;
			}
		}

		// Token: 0x060114AC RID: 70828 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170051A0 RID: 20896
		// (get) Token: 0x060114AD RID: 70829 RVA: 0x002ECC82 File Offset: 0x002EAE82
		internal override string[] AttributeTagNames
		{
			get
			{
				return SchemeColor.attributeTagNames;
			}
		}

		// Token: 0x170051A1 RID: 20897
		// (get) Token: 0x060114AE RID: 70830 RVA: 0x002ECC89 File Offset: 0x002EAE89
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SchemeColor.attributeNamespaceIds;
			}
		}

		// Token: 0x170051A2 RID: 20898
		// (get) Token: 0x060114AF RID: 70831 RVA: 0x002ECC90 File Offset: 0x002EAE90
		// (set) Token: 0x060114B0 RID: 70832 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "val")]
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

		// Token: 0x060114B1 RID: 70833 RVA: 0x00293ECF File Offset: 0x002920CF
		public SchemeColor()
		{
		}

		// Token: 0x060114B2 RID: 70834 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SchemeColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060114B3 RID: 70835 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SchemeColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060114B4 RID: 70836 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SchemeColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060114B5 RID: 70837 RVA: 0x002ECCA0 File Offset: 0x002EAEA0
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

		// Token: 0x060114B6 RID: 70838 RVA: 0x002ECD9E File Offset: 0x002EAF9E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "val" == name)
			{
				return new EnumValue<SchemeColorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060114B7 RID: 70839 RVA: 0x002ECDC0 File Offset: 0x002EAFC0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SchemeColor>(deep);
		}

		// Token: 0x04007920 RID: 31008
		private const string tagName = "schemeClr";

		// Token: 0x04007921 RID: 31009
		private const byte tagNsId = 52;

		// Token: 0x04007922 RID: 31010
		internal const int ElementTypeIdConst = 12843;

		// Token: 0x04007923 RID: 31011
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007924 RID: 31012
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
