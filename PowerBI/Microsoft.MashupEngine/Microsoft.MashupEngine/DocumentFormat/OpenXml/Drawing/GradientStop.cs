using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C2 RID: 10178
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(PresetColor))]
	internal class GradientStop : OpenXmlCompositeElement
	{
		// Token: 0x17006366 RID: 25446
		// (get) Token: 0x06013C39 RID: 80953 RVA: 0x002EE827 File Offset: 0x002ECA27
		public override string LocalName
		{
			get
			{
				return "gs";
			}
		}

		// Token: 0x17006367 RID: 25447
		// (get) Token: 0x06013C3A RID: 80954 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006368 RID: 25448
		// (get) Token: 0x06013C3B RID: 80955 RVA: 0x0030B79C File Offset: 0x0030999C
		internal override int ElementTypeId
		{
			get
			{
				return 10208;
			}
		}

		// Token: 0x06013C3C RID: 80956 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006369 RID: 25449
		// (get) Token: 0x06013C3D RID: 80957 RVA: 0x0030B7A3 File Offset: 0x003099A3
		internal override string[] AttributeTagNames
		{
			get
			{
				return GradientStop.attributeTagNames;
			}
		}

		// Token: 0x1700636A RID: 25450
		// (get) Token: 0x06013C3E RID: 80958 RVA: 0x0030B7AA File Offset: 0x003099AA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GradientStop.attributeNamespaceIds;
			}
		}

		// Token: 0x1700636B RID: 25451
		// (get) Token: 0x06013C3F RID: 80959 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013C40 RID: 80960 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pos")]
		public Int32Value Position
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06013C41 RID: 80961 RVA: 0x00293ECF File Offset: 0x002920CF
		public GradientStop()
		{
		}

		// Token: 0x06013C42 RID: 80962 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GradientStop(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013C43 RID: 80963 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GradientStop(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013C44 RID: 80964 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GradientStop(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013C45 RID: 80965 RVA: 0x0030B7B4 File Offset: 0x003099B4
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

		// Token: 0x1700636C RID: 25452
		// (get) Token: 0x06013C46 RID: 80966 RVA: 0x0030B852 File Offset: 0x00309A52
		internal override string[] ElementTagNames
		{
			get
			{
				return GradientStop.eleTagNames;
			}
		}

		// Token: 0x1700636D RID: 25453
		// (get) Token: 0x06013C47 RID: 80967 RVA: 0x0030B859 File Offset: 0x00309A59
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GradientStop.eleNamespaceIds;
			}
		}

		// Token: 0x1700636E RID: 25454
		// (get) Token: 0x06013C48 RID: 80968 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700636F RID: 25455
		// (get) Token: 0x06013C49 RID: 80969 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06013C4A RID: 80970 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17006370 RID: 25456
		// (get) Token: 0x06013C4B RID: 80971 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06013C4C RID: 80972 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17006371 RID: 25457
		// (get) Token: 0x06013C4D RID: 80973 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06013C4E RID: 80974 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17006372 RID: 25458
		// (get) Token: 0x06013C4F RID: 80975 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x06013C50 RID: 80976 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17006373 RID: 25459
		// (get) Token: 0x06013C51 RID: 80977 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x06013C52 RID: 80978 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17006374 RID: 25460
		// (get) Token: 0x06013C53 RID: 80979 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06013C54 RID: 80980 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x06013C55 RID: 80981 RVA: 0x0030B860 File Offset: 0x00309A60
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pos" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013C56 RID: 80982 RVA: 0x0030B880 File Offset: 0x00309A80
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GradientStop>(deep);
		}

		// Token: 0x06013C57 RID: 80983 RVA: 0x0030B88C File Offset: 0x00309A8C
		// Note: this type is marked as 'beforefieldinit'.
		static GradientStop()
		{
			byte[] array = new byte[1];
			GradientStop.attributeNamespaceIds = array;
			GradientStop.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			GradientStop.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x040087AD RID: 34733
		private const string tagName = "gs";

		// Token: 0x040087AE RID: 34734
		private const byte tagNsId = 10;

		// Token: 0x040087AF RID: 34735
		internal const int ElementTypeIdConst = 10208;

		// Token: 0x040087B0 RID: 34736
		private static string[] attributeTagNames = new string[] { "pos" };

		// Token: 0x040087B1 RID: 34737
		private static byte[] attributeNamespaceIds;

		// Token: 0x040087B2 RID: 34738
		private static readonly string[] eleTagNames;

		// Token: 0x040087B3 RID: 34739
		private static readonly byte[] eleNamespaceIds;
	}
}
