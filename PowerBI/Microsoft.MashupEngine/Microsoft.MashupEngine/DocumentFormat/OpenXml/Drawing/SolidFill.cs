using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002700 RID: 9984
	[ChildElementInfo(typeof(PresetColor))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SolidFill : OpenXmlCompositeElement
	{
		// Token: 0x17005E6B RID: 24171
		// (get) Token: 0x06013122 RID: 78114 RVA: 0x002ECFFB File Offset: 0x002EB1FB
		public override string LocalName
		{
			get
			{
				return "solidFill";
			}
		}

		// Token: 0x17005E6C RID: 24172
		// (get) Token: 0x06013123 RID: 78115 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E6D RID: 24173
		// (get) Token: 0x06013124 RID: 78116 RVA: 0x00303330 File Offset: 0x00301530
		internal override int ElementTypeId
		{
			get
			{
				return 10048;
			}
		}

		// Token: 0x06013125 RID: 78117 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013126 RID: 78118 RVA: 0x00293ECF File Offset: 0x002920CF
		public SolidFill()
		{
		}

		// Token: 0x06013127 RID: 78119 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SolidFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013128 RID: 78120 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SolidFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013129 RID: 78121 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SolidFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601312A RID: 78122 RVA: 0x00303338 File Offset: 0x00301538
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

		// Token: 0x17005E6E RID: 24174
		// (get) Token: 0x0601312B RID: 78123 RVA: 0x003033D6 File Offset: 0x003015D6
		internal override string[] ElementTagNames
		{
			get
			{
				return SolidFill.eleTagNames;
			}
		}

		// Token: 0x17005E6F RID: 24175
		// (get) Token: 0x0601312C RID: 78124 RVA: 0x003033DD File Offset: 0x003015DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SolidFill.eleNamespaceIds;
			}
		}

		// Token: 0x17005E70 RID: 24176
		// (get) Token: 0x0601312D RID: 78125 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005E71 RID: 24177
		// (get) Token: 0x0601312E RID: 78126 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x0601312F RID: 78127 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17005E72 RID: 24178
		// (get) Token: 0x06013130 RID: 78128 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06013131 RID: 78129 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17005E73 RID: 24179
		// (get) Token: 0x06013132 RID: 78130 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06013133 RID: 78131 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17005E74 RID: 24180
		// (get) Token: 0x06013134 RID: 78132 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x06013135 RID: 78133 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17005E75 RID: 24181
		// (get) Token: 0x06013136 RID: 78134 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x06013137 RID: 78135 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17005E76 RID: 24182
		// (get) Token: 0x06013138 RID: 78136 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06013139 RID: 78137 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x0601313A RID: 78138 RVA: 0x003033E4 File Offset: 0x003015E4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SolidFill>(deep);
		}

		// Token: 0x04008488 RID: 33928
		private const string tagName = "solidFill";

		// Token: 0x04008489 RID: 33929
		private const byte tagNsId = 10;

		// Token: 0x0400848A RID: 33930
		internal const int ElementTypeIdConst = 10048;

		// Token: 0x0400848B RID: 33931
		private static readonly string[] eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };

		// Token: 0x0400848C RID: 33932
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
