using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200270C RID: 9996
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	internal class AlphaInverse : OpenXmlCompositeElement
	{
		// Token: 0x17005EB8 RID: 24248
		// (get) Token: 0x060131CC RID: 78284 RVA: 0x00303CE1 File Offset: 0x00301EE1
		public override string LocalName
		{
			get
			{
				return "alphaInv";
			}
		}

		// Token: 0x17005EB9 RID: 24249
		// (get) Token: 0x060131CD RID: 78285 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EBA RID: 24250
		// (get) Token: 0x060131CE RID: 78286 RVA: 0x00303CE8 File Offset: 0x00301EE8
		internal override int ElementTypeId
		{
			get
			{
				return 10058;
			}
		}

		// Token: 0x060131CF RID: 78287 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060131D0 RID: 78288 RVA: 0x00293ECF File Offset: 0x002920CF
		public AlphaInverse()
		{
		}

		// Token: 0x060131D1 RID: 78289 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AlphaInverse(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060131D2 RID: 78290 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AlphaInverse(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060131D3 RID: 78291 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AlphaInverse(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060131D4 RID: 78292 RVA: 0x00303CF0 File Offset: 0x00301EF0
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

		// Token: 0x17005EBB RID: 24251
		// (get) Token: 0x060131D5 RID: 78293 RVA: 0x00303D8E File Offset: 0x00301F8E
		internal override string[] ElementTagNames
		{
			get
			{
				return AlphaInverse.eleTagNames;
			}
		}

		// Token: 0x17005EBC RID: 24252
		// (get) Token: 0x060131D6 RID: 78294 RVA: 0x00303D95 File Offset: 0x00301F95
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AlphaInverse.eleNamespaceIds;
			}
		}

		// Token: 0x17005EBD RID: 24253
		// (get) Token: 0x060131D7 RID: 78295 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005EBE RID: 24254
		// (get) Token: 0x060131D8 RID: 78296 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x060131D9 RID: 78297 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17005EBF RID: 24255
		// (get) Token: 0x060131DA RID: 78298 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x060131DB RID: 78299 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17005EC0 RID: 24256
		// (get) Token: 0x060131DC RID: 78300 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x060131DD RID: 78301 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17005EC1 RID: 24257
		// (get) Token: 0x060131DE RID: 78302 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x060131DF RID: 78303 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17005EC2 RID: 24258
		// (get) Token: 0x060131E0 RID: 78304 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x060131E1 RID: 78305 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17005EC3 RID: 24259
		// (get) Token: 0x060131E2 RID: 78306 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x060131E3 RID: 78307 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x060131E4 RID: 78308 RVA: 0x00303D9C File Offset: 0x00301F9C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaInverse>(deep);
		}

		// Token: 0x040084BD RID: 33981
		private const string tagName = "alphaInv";

		// Token: 0x040084BE RID: 33982
		private const byte tagNsId = 10;

		// Token: 0x040084BF RID: 33983
		internal const int ElementTypeIdConst = 10058;

		// Token: 0x040084C0 RID: 33984
		private static readonly string[] eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };

		// Token: 0x040084C1 RID: 33985
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
