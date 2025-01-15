using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B0 RID: 9136
	[ChildElementInfo(typeof(SystemColor))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(PresetColor))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(SchemeColor))]
	internal class LaserColor : OpenXmlCompositeElement
	{
		// Token: 0x17004C55 RID: 19541
		// (get) Token: 0x060108B2 RID: 67762 RVA: 0x002E48D7 File Offset: 0x002E2AD7
		public override string LocalName
		{
			get
			{
				return "laserClr";
			}
		}

		// Token: 0x17004C56 RID: 19542
		// (get) Token: 0x060108B3 RID: 67763 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C57 RID: 19543
		// (get) Token: 0x060108B4 RID: 67764 RVA: 0x002E48DE File Offset: 0x002E2ADE
		internal override int ElementTypeId
		{
			get
			{
				return 12791;
			}
		}

		// Token: 0x060108B5 RID: 67765 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060108B6 RID: 67766 RVA: 0x00293ECF File Offset: 0x002920CF
		public LaserColor()
		{
		}

		// Token: 0x060108B7 RID: 67767 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LaserColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060108B8 RID: 67768 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LaserColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060108B9 RID: 67769 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LaserColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060108BA RID: 67770 RVA: 0x002E48E8 File Offset: 0x002E2AE8
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

		// Token: 0x17004C58 RID: 19544
		// (get) Token: 0x060108BB RID: 67771 RVA: 0x002E4986 File Offset: 0x002E2B86
		internal override string[] ElementTagNames
		{
			get
			{
				return LaserColor.eleTagNames;
			}
		}

		// Token: 0x17004C59 RID: 19545
		// (get) Token: 0x060108BC RID: 67772 RVA: 0x002E498D File Offset: 0x002E2B8D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LaserColor.eleNamespaceIds;
			}
		}

		// Token: 0x17004C5A RID: 19546
		// (get) Token: 0x060108BD RID: 67773 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17004C5B RID: 19547
		// (get) Token: 0x060108BE RID: 67774 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x060108BF RID: 67775 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17004C5C RID: 19548
		// (get) Token: 0x060108C0 RID: 67776 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x060108C1 RID: 67777 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17004C5D RID: 19549
		// (get) Token: 0x060108C2 RID: 67778 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x060108C3 RID: 67779 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17004C5E RID: 19550
		// (get) Token: 0x060108C4 RID: 67780 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x060108C5 RID: 67781 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17004C5F RID: 19551
		// (get) Token: 0x060108C6 RID: 67782 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x060108C7 RID: 67783 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17004C60 RID: 19552
		// (get) Token: 0x060108C8 RID: 67784 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x060108C9 RID: 67785 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x060108CA RID: 67786 RVA: 0x002E4A06 File Offset: 0x002E2C06
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LaserColor>(deep);
		}

		// Token: 0x0400752C RID: 29996
		private const string tagName = "laserClr";

		// Token: 0x0400752D RID: 29997
		private const byte tagNsId = 49;

		// Token: 0x0400752E RID: 29998
		internal const int ElementTypeIdConst = 12791;

		// Token: 0x0400752F RID: 29999
		private static readonly string[] eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };

		// Token: 0x04007530 RID: 30000
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
