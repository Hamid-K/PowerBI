using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200271C RID: 10012
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(SystemColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	internal class InnerShadow : OpenXmlCompositeElement
	{
		// Token: 0x17005F43 RID: 24387
		// (get) Token: 0x060132F2 RID: 78578 RVA: 0x00304917 File Offset: 0x00302B17
		public override string LocalName
		{
			get
			{
				return "innerShdw";
			}
		}

		// Token: 0x17005F44 RID: 24388
		// (get) Token: 0x060132F3 RID: 78579 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F45 RID: 24389
		// (get) Token: 0x060132F4 RID: 78580 RVA: 0x0030491E File Offset: 0x00302B1E
		internal override int ElementTypeId
		{
			get
			{
				return 10074;
			}
		}

		// Token: 0x060132F5 RID: 78581 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F46 RID: 24390
		// (get) Token: 0x060132F6 RID: 78582 RVA: 0x00304925 File Offset: 0x00302B25
		internal override string[] AttributeTagNames
		{
			get
			{
				return InnerShadow.attributeTagNames;
			}
		}

		// Token: 0x17005F47 RID: 24391
		// (get) Token: 0x060132F7 RID: 78583 RVA: 0x0030492C File Offset: 0x00302B2C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return InnerShadow.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F48 RID: 24392
		// (get) Token: 0x060132F8 RID: 78584 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060132F9 RID: 78585 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "blurRad")]
		public Int64Value BlurRadius
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

		// Token: 0x17005F49 RID: 24393
		// (get) Token: 0x060132FA RID: 78586 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x060132FB RID: 78587 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dist")]
		public Int64Value Distance
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005F4A RID: 24394
		// (get) Token: 0x060132FC RID: 78588 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x060132FD RID: 78589 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dir")]
		public Int32Value Direction
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

		// Token: 0x060132FE RID: 78590 RVA: 0x00293ECF File Offset: 0x002920CF
		public InnerShadow()
		{
		}

		// Token: 0x060132FF RID: 78591 RVA: 0x00293ED7 File Offset: 0x002920D7
		public InnerShadow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013300 RID: 78592 RVA: 0x00293EE0 File Offset: 0x002920E0
		public InnerShadow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013301 RID: 78593 RVA: 0x00293EE9 File Offset: 0x002920E9
		public InnerShadow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013302 RID: 78594 RVA: 0x00304934 File Offset: 0x00302B34
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

		// Token: 0x17005F4B RID: 24395
		// (get) Token: 0x06013303 RID: 78595 RVA: 0x003049D2 File Offset: 0x00302BD2
		internal override string[] ElementTagNames
		{
			get
			{
				return InnerShadow.eleTagNames;
			}
		}

		// Token: 0x17005F4C RID: 24396
		// (get) Token: 0x06013304 RID: 78596 RVA: 0x003049D9 File Offset: 0x00302BD9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return InnerShadow.eleNamespaceIds;
			}
		}

		// Token: 0x17005F4D RID: 24397
		// (get) Token: 0x06013305 RID: 78597 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005F4E RID: 24398
		// (get) Token: 0x06013306 RID: 78598 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06013307 RID: 78599 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17005F4F RID: 24399
		// (get) Token: 0x06013308 RID: 78600 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06013309 RID: 78601 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17005F50 RID: 24400
		// (get) Token: 0x0601330A RID: 78602 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x0601330B RID: 78603 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17005F51 RID: 24401
		// (get) Token: 0x0601330C RID: 78604 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x0601330D RID: 78605 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17005F52 RID: 24402
		// (get) Token: 0x0601330E RID: 78606 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x0601330F RID: 78607 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17005F53 RID: 24403
		// (get) Token: 0x06013310 RID: 78608 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06013311 RID: 78609 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x06013312 RID: 78610 RVA: 0x003049E0 File Offset: 0x00302BE0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "blurRad" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "dist" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013313 RID: 78611 RVA: 0x00304A37 File Offset: 0x00302C37
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InnerShadow>(deep);
		}

		// Token: 0x06013314 RID: 78612 RVA: 0x00304A40 File Offset: 0x00302C40
		// Note: this type is marked as 'beforefieldinit'.
		static InnerShadow()
		{
			byte[] array = new byte[3];
			InnerShadow.attributeNamespaceIds = array;
			InnerShadow.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			InnerShadow.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x04008511 RID: 34065
		private const string tagName = "innerShdw";

		// Token: 0x04008512 RID: 34066
		private const byte tagNsId = 10;

		// Token: 0x04008513 RID: 34067
		internal const int ElementTypeIdConst = 10074;

		// Token: 0x04008514 RID: 34068
		private static string[] attributeTagNames = new string[] { "blurRad", "dist", "dir" };

		// Token: 0x04008515 RID: 34069
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008516 RID: 34070
		private static readonly string[] eleTagNames;

		// Token: 0x04008517 RID: 34071
		private static readonly byte[] eleNamespaceIds;
	}
}
