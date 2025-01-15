using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002715 RID: 10005
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	internal class ColorReplacement : OpenXmlCompositeElement
	{
		// Token: 0x17005EFF RID: 24319
		// (get) Token: 0x06013260 RID: 78432 RVA: 0x0030429B File Offset: 0x0030249B
		public override string LocalName
		{
			get
			{
				return "clrRepl";
			}
		}

		// Token: 0x17005F00 RID: 24320
		// (get) Token: 0x06013261 RID: 78433 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F01 RID: 24321
		// (get) Token: 0x06013262 RID: 78434 RVA: 0x003042A2 File Offset: 0x003024A2
		internal override int ElementTypeId
		{
			get
			{
				return 10067;
			}
		}

		// Token: 0x06013263 RID: 78435 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013264 RID: 78436 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorReplacement()
		{
		}

		// Token: 0x06013265 RID: 78437 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorReplacement(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013266 RID: 78438 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorReplacement(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013267 RID: 78439 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorReplacement(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013268 RID: 78440 RVA: 0x003042AC File Offset: 0x003024AC
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

		// Token: 0x17005F02 RID: 24322
		// (get) Token: 0x06013269 RID: 78441 RVA: 0x0030434A File Offset: 0x0030254A
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorReplacement.eleTagNames;
			}
		}

		// Token: 0x17005F03 RID: 24323
		// (get) Token: 0x0601326A RID: 78442 RVA: 0x00304351 File Offset: 0x00302551
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorReplacement.eleNamespaceIds;
			}
		}

		// Token: 0x17005F04 RID: 24324
		// (get) Token: 0x0601326B RID: 78443 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005F05 RID: 24325
		// (get) Token: 0x0601326C RID: 78444 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x0601326D RID: 78445 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17005F06 RID: 24326
		// (get) Token: 0x0601326E RID: 78446 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x0601326F RID: 78447 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17005F07 RID: 24327
		// (get) Token: 0x06013270 RID: 78448 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06013271 RID: 78449 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17005F08 RID: 24328
		// (get) Token: 0x06013272 RID: 78450 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x06013273 RID: 78451 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17005F09 RID: 24329
		// (get) Token: 0x06013274 RID: 78452 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x06013275 RID: 78453 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17005F0A RID: 24330
		// (get) Token: 0x06013276 RID: 78454 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06013277 RID: 78455 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x06013278 RID: 78456 RVA: 0x00304358 File Offset: 0x00302558
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorReplacement>(deep);
		}

		// Token: 0x040084EE RID: 34030
		private const string tagName = "clrRepl";

		// Token: 0x040084EF RID: 34031
		private const byte tagNsId = 10;

		// Token: 0x040084F0 RID: 34032
		internal const int ElementTypeIdConst = 10067;

		// Token: 0x040084F1 RID: 34033
		private static readonly string[] eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };

		// Token: 0x040084F2 RID: 34034
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
