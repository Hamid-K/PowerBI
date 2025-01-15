using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200265D RID: 9821
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(TextLineColorList))]
	[ChildElementInfo(typeof(TextEffectColorList))]
	[ChildElementInfo(typeof(FillColorList))]
	[ChildElementInfo(typeof(LineColorList))]
	[ChildElementInfo(typeof(EffectColorList))]
	[ChildElementInfo(typeof(TextFillColorList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorTransformStyleLabel : OpenXmlCompositeElement
	{
		// Token: 0x17005B7D RID: 23421
		// (get) Token: 0x06012AB0 RID: 76464 RVA: 0x002FDC19 File Offset: 0x002FBE19
		public override string LocalName
		{
			get
			{
				return "styleLbl";
			}
		}

		// Token: 0x17005B7E RID: 23422
		// (get) Token: 0x06012AB1 RID: 76465 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B7F RID: 23423
		// (get) Token: 0x06012AB2 RID: 76466 RVA: 0x002FDC20 File Offset: 0x002FBE20
		internal override int ElementTypeId
		{
			get
			{
				return 10638;
			}
		}

		// Token: 0x06012AB3 RID: 76467 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B80 RID: 23424
		// (get) Token: 0x06012AB4 RID: 76468 RVA: 0x002FDC27 File Offset: 0x002FBE27
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorTransformStyleLabel.attributeTagNames;
			}
		}

		// Token: 0x17005B81 RID: 23425
		// (get) Token: 0x06012AB5 RID: 76469 RVA: 0x002FDC2E File Offset: 0x002FBE2E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorTransformStyleLabel.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B82 RID: 23426
		// (get) Token: 0x06012AB6 RID: 76470 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012AB7 RID: 76471 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012AB8 RID: 76472 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorTransformStyleLabel()
		{
		}

		// Token: 0x06012AB9 RID: 76473 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorTransformStyleLabel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012ABA RID: 76474 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorTransformStyleLabel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012ABB RID: 76475 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorTransformStyleLabel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012ABC RID: 76476 RVA: 0x002FDC38 File Offset: 0x002FBE38
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "fillClrLst" == name)
			{
				return new FillColorList();
			}
			if (14 == namespaceId && "linClrLst" == name)
			{
				return new LineColorList();
			}
			if (14 == namespaceId && "effectClrLst" == name)
			{
				return new EffectColorList();
			}
			if (14 == namespaceId && "txLinClrLst" == name)
			{
				return new TextLineColorList();
			}
			if (14 == namespaceId && "txFillClrLst" == name)
			{
				return new TextFillColorList();
			}
			if (14 == namespaceId && "txEffectClrLst" == name)
			{
				return new TextEffectColorList();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005B83 RID: 23427
		// (get) Token: 0x06012ABD RID: 76477 RVA: 0x002FDCEE File Offset: 0x002FBEEE
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorTransformStyleLabel.eleTagNames;
			}
		}

		// Token: 0x17005B84 RID: 23428
		// (get) Token: 0x06012ABE RID: 76478 RVA: 0x002FDCF5 File Offset: 0x002FBEF5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorTransformStyleLabel.eleNamespaceIds;
			}
		}

		// Token: 0x17005B85 RID: 23429
		// (get) Token: 0x06012ABF RID: 76479 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005B86 RID: 23430
		// (get) Token: 0x06012AC0 RID: 76480 RVA: 0x002FDCFC File Offset: 0x002FBEFC
		// (set) Token: 0x06012AC1 RID: 76481 RVA: 0x002FDD05 File Offset: 0x002FBF05
		public FillColorList FillColorList
		{
			get
			{
				return base.GetElement<FillColorList>(0);
			}
			set
			{
				base.SetElement<FillColorList>(0, value);
			}
		}

		// Token: 0x17005B87 RID: 23431
		// (get) Token: 0x06012AC2 RID: 76482 RVA: 0x002FDD0F File Offset: 0x002FBF0F
		// (set) Token: 0x06012AC3 RID: 76483 RVA: 0x002FDD18 File Offset: 0x002FBF18
		public LineColorList LineColorList
		{
			get
			{
				return base.GetElement<LineColorList>(1);
			}
			set
			{
				base.SetElement<LineColorList>(1, value);
			}
		}

		// Token: 0x17005B88 RID: 23432
		// (get) Token: 0x06012AC4 RID: 76484 RVA: 0x002FDD22 File Offset: 0x002FBF22
		// (set) Token: 0x06012AC5 RID: 76485 RVA: 0x002FDD2B File Offset: 0x002FBF2B
		public EffectColorList EffectColorList
		{
			get
			{
				return base.GetElement<EffectColorList>(2);
			}
			set
			{
				base.SetElement<EffectColorList>(2, value);
			}
		}

		// Token: 0x17005B89 RID: 23433
		// (get) Token: 0x06012AC6 RID: 76486 RVA: 0x002FDD35 File Offset: 0x002FBF35
		// (set) Token: 0x06012AC7 RID: 76487 RVA: 0x002FDD3E File Offset: 0x002FBF3E
		public TextLineColorList TextLineColorList
		{
			get
			{
				return base.GetElement<TextLineColorList>(3);
			}
			set
			{
				base.SetElement<TextLineColorList>(3, value);
			}
		}

		// Token: 0x17005B8A RID: 23434
		// (get) Token: 0x06012AC8 RID: 76488 RVA: 0x002FDD48 File Offset: 0x002FBF48
		// (set) Token: 0x06012AC9 RID: 76489 RVA: 0x002FDD51 File Offset: 0x002FBF51
		public TextFillColorList TextFillColorList
		{
			get
			{
				return base.GetElement<TextFillColorList>(4);
			}
			set
			{
				base.SetElement<TextFillColorList>(4, value);
			}
		}

		// Token: 0x17005B8B RID: 23435
		// (get) Token: 0x06012ACA RID: 76490 RVA: 0x002FDD5B File Offset: 0x002FBF5B
		// (set) Token: 0x06012ACB RID: 76491 RVA: 0x002FDD64 File Offset: 0x002FBF64
		public TextEffectColorList TextEffectColorList
		{
			get
			{
				return base.GetElement<TextEffectColorList>(5);
			}
			set
			{
				base.SetElement<TextEffectColorList>(5, value);
			}
		}

		// Token: 0x17005B8C RID: 23436
		// (get) Token: 0x06012ACC RID: 76492 RVA: 0x002FDD6E File Offset: 0x002FBF6E
		// (set) Token: 0x06012ACD RID: 76493 RVA: 0x002FDD77 File Offset: 0x002FBF77
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(6);
			}
			set
			{
				base.SetElement<ExtensionList>(6, value);
			}
		}

		// Token: 0x06012ACE RID: 76494 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012ACF RID: 76495 RVA: 0x002FDD81 File Offset: 0x002FBF81
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorTransformStyleLabel>(deep);
		}

		// Token: 0x06012AD0 RID: 76496 RVA: 0x002FDD8C File Offset: 0x002FBF8C
		// Note: this type is marked as 'beforefieldinit'.
		static ColorTransformStyleLabel()
		{
			byte[] array = new byte[1];
			ColorTransformStyleLabel.attributeNamespaceIds = array;
			ColorTransformStyleLabel.eleTagNames = new string[] { "fillClrLst", "linClrLst", "effectClrLst", "txLinClrLst", "txFillClrLst", "txEffectClrLst", "extLst" };
			ColorTransformStyleLabel.eleNamespaceIds = new byte[] { 14, 14, 14, 14, 14, 14, 14 };
		}

		// Token: 0x04008125 RID: 33061
		private const string tagName = "styleLbl";

		// Token: 0x04008126 RID: 33062
		private const byte tagNsId = 14;

		// Token: 0x04008127 RID: 33063
		internal const int ElementTypeIdConst = 10638;

		// Token: 0x04008128 RID: 33064
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04008129 RID: 33065
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400812A RID: 33066
		private static readonly string[] eleTagNames;

		// Token: 0x0400812B RID: 33067
		private static readonly byte[] eleNamespaceIds;
	}
}
