using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002719 RID: 10009
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(PresetColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Glow : OpenXmlCompositeElement
	{
		// Token: 0x17005F29 RID: 24361
		// (get) Token: 0x060132BD RID: 78525 RVA: 0x002ED29C File Offset: 0x002EB49C
		public override string LocalName
		{
			get
			{
				return "glow";
			}
		}

		// Token: 0x17005F2A RID: 24362
		// (get) Token: 0x060132BE RID: 78526 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F2B RID: 24363
		// (get) Token: 0x060132BF RID: 78527 RVA: 0x003046F2 File Offset: 0x003028F2
		internal override int ElementTypeId
		{
			get
			{
				return 10071;
			}
		}

		// Token: 0x060132C0 RID: 78528 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F2C RID: 24364
		// (get) Token: 0x060132C1 RID: 78529 RVA: 0x003046F9 File Offset: 0x003028F9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Glow.attributeTagNames;
			}
		}

		// Token: 0x17005F2D RID: 24365
		// (get) Token: 0x060132C2 RID: 78530 RVA: 0x00304700 File Offset: 0x00302900
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Glow.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F2E RID: 24366
		// (get) Token: 0x060132C3 RID: 78531 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060132C4 RID: 78532 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rad")]
		public Int64Value Radius
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060132C5 RID: 78533 RVA: 0x00293ECF File Offset: 0x002920CF
		public Glow()
		{
		}

		// Token: 0x060132C6 RID: 78534 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Glow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060132C7 RID: 78535 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Glow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060132C8 RID: 78536 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Glow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060132C9 RID: 78537 RVA: 0x00304708 File Offset: 0x00302908
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

		// Token: 0x17005F2F RID: 24367
		// (get) Token: 0x060132CA RID: 78538 RVA: 0x003047A6 File Offset: 0x003029A6
		internal override string[] ElementTagNames
		{
			get
			{
				return Glow.eleTagNames;
			}
		}

		// Token: 0x17005F30 RID: 24368
		// (get) Token: 0x060132CB RID: 78539 RVA: 0x003047AD File Offset: 0x003029AD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Glow.eleNamespaceIds;
			}
		}

		// Token: 0x17005F31 RID: 24369
		// (get) Token: 0x060132CC RID: 78540 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005F32 RID: 24370
		// (get) Token: 0x060132CD RID: 78541 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x060132CE RID: 78542 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17005F33 RID: 24371
		// (get) Token: 0x060132CF RID: 78543 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x060132D0 RID: 78544 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17005F34 RID: 24372
		// (get) Token: 0x060132D1 RID: 78545 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x060132D2 RID: 78546 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17005F35 RID: 24373
		// (get) Token: 0x060132D3 RID: 78547 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x060132D4 RID: 78548 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17005F36 RID: 24374
		// (get) Token: 0x060132D5 RID: 78549 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x060132D6 RID: 78550 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17005F37 RID: 24375
		// (get) Token: 0x060132D7 RID: 78551 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x060132D8 RID: 78552 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x060132D9 RID: 78553 RVA: 0x00303F1B File Offset: 0x0030211B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rad" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060132DA RID: 78554 RVA: 0x003047B4 File Offset: 0x003029B4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Glow>(deep);
		}

		// Token: 0x060132DB RID: 78555 RVA: 0x003047C0 File Offset: 0x003029C0
		// Note: this type is marked as 'beforefieldinit'.
		static Glow()
		{
			byte[] array = new byte[1];
			Glow.attributeNamespaceIds = array;
			Glow.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			Glow.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x04008502 RID: 34050
		private const string tagName = "glow";

		// Token: 0x04008503 RID: 34051
		private const byte tagNsId = 10;

		// Token: 0x04008504 RID: 34052
		internal const int ElementTypeIdConst = 10071;

		// Token: 0x04008505 RID: 34053
		private static string[] attributeTagNames = new string[] { "rad" };

		// Token: 0x04008506 RID: 34054
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008507 RID: 34055
		private static readonly string[] eleTagNames;

		// Token: 0x04008508 RID: 34056
		private static readonly byte[] eleNamespaceIds;
	}
}
