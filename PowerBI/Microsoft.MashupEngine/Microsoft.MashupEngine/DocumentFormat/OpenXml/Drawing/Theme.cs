using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002765 RID: 10085
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ThemeElements))]
	[ChildElementInfo(typeof(ObjectDefaults))]
	[ChildElementInfo(typeof(ExtraColorSchemeList))]
	[ChildElementInfo(typeof(CustomColorList))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Theme : OpenXmlPartRootElement
	{
		// Token: 0x17006109 RID: 24841
		// (get) Token: 0x060136EF RID: 79599 RVA: 0x002AD23F File Offset: 0x002AB43F
		public override string LocalName
		{
			get
			{
				return "theme";
			}
		}

		// Token: 0x1700610A RID: 24842
		// (get) Token: 0x060136F0 RID: 79600 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700610B RID: 24843
		// (get) Token: 0x060136F1 RID: 79601 RVA: 0x00307119 File Offset: 0x00305319
		internal override int ElementTypeId
		{
			get
			{
				return 10122;
			}
		}

		// Token: 0x060136F2 RID: 79602 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700610C RID: 24844
		// (get) Token: 0x060136F3 RID: 79603 RVA: 0x00307120 File Offset: 0x00305320
		internal override string[] AttributeTagNames
		{
			get
			{
				return Theme.attributeTagNames;
			}
		}

		// Token: 0x1700610D RID: 24845
		// (get) Token: 0x060136F4 RID: 79604 RVA: 0x00307127 File Offset: 0x00305327
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Theme.attributeNamespaceIds;
			}
		}

		// Token: 0x1700610E RID: 24846
		// (get) Token: 0x060136F5 RID: 79605 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060136F6 RID: 79606 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060136F7 RID: 79607 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Theme(ThemePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060136F8 RID: 79608 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ThemePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x1700610F RID: 24847
		// (get) Token: 0x060136F9 RID: 79609 RVA: 0x0030712E File Offset: 0x0030532E
		// (set) Token: 0x060136FA RID: 79610 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ThemePart ThemePart
		{
			get
			{
				return base.OpenXmlPart as ThemePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060136FB RID: 79611 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Theme(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136FC RID: 79612 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Theme(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136FD RID: 79613 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Theme(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060136FE RID: 79614 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Theme()
		{
		}

		// Token: 0x060136FF RID: 79615 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ThemePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06013700 RID: 79616 RVA: 0x0030713C File Offset: 0x0030533C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "themeElements" == name)
			{
				return new ThemeElements();
			}
			if (10 == namespaceId && "objectDefaults" == name)
			{
				return new ObjectDefaults();
			}
			if (10 == namespaceId && "extraClrSchemeLst" == name)
			{
				return new ExtraColorSchemeList();
			}
			if (10 == namespaceId && "custClrLst" == name)
			{
				return new CustomColorList();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006110 RID: 24848
		// (get) Token: 0x06013701 RID: 79617 RVA: 0x003071C2 File Offset: 0x003053C2
		internal override string[] ElementTagNames
		{
			get
			{
				return Theme.eleTagNames;
			}
		}

		// Token: 0x17006111 RID: 24849
		// (get) Token: 0x06013702 RID: 79618 RVA: 0x003071C9 File Offset: 0x003053C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Theme.eleNamespaceIds;
			}
		}

		// Token: 0x17006112 RID: 24850
		// (get) Token: 0x06013703 RID: 79619 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006113 RID: 24851
		// (get) Token: 0x06013704 RID: 79620 RVA: 0x003071D0 File Offset: 0x003053D0
		// (set) Token: 0x06013705 RID: 79621 RVA: 0x003071D9 File Offset: 0x003053D9
		public ThemeElements ThemeElements
		{
			get
			{
				return base.GetElement<ThemeElements>(0);
			}
			set
			{
				base.SetElement<ThemeElements>(0, value);
			}
		}

		// Token: 0x17006114 RID: 24852
		// (get) Token: 0x06013706 RID: 79622 RVA: 0x003071E3 File Offset: 0x003053E3
		// (set) Token: 0x06013707 RID: 79623 RVA: 0x003071EC File Offset: 0x003053EC
		public ObjectDefaults ObjectDefaults
		{
			get
			{
				return base.GetElement<ObjectDefaults>(1);
			}
			set
			{
				base.SetElement<ObjectDefaults>(1, value);
			}
		}

		// Token: 0x17006115 RID: 24853
		// (get) Token: 0x06013708 RID: 79624 RVA: 0x003071F6 File Offset: 0x003053F6
		// (set) Token: 0x06013709 RID: 79625 RVA: 0x003071FF File Offset: 0x003053FF
		public ExtraColorSchemeList ExtraColorSchemeList
		{
			get
			{
				return base.GetElement<ExtraColorSchemeList>(2);
			}
			set
			{
				base.SetElement<ExtraColorSchemeList>(2, value);
			}
		}

		// Token: 0x17006116 RID: 24854
		// (get) Token: 0x0601370A RID: 79626 RVA: 0x00307209 File Offset: 0x00305409
		// (set) Token: 0x0601370B RID: 79627 RVA: 0x00307212 File Offset: 0x00305412
		public CustomColorList CustomColorList
		{
			get
			{
				return base.GetElement<CustomColorList>(3);
			}
			set
			{
				base.SetElement<CustomColorList>(3, value);
			}
		}

		// Token: 0x17006117 RID: 24855
		// (get) Token: 0x0601370C RID: 79628 RVA: 0x002E0DD0 File Offset: 0x002DEFD0
		// (set) Token: 0x0601370D RID: 79629 RVA: 0x002E0DD9 File Offset: 0x002DEFD9
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x0601370E RID: 79630 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601370F RID: 79631 RVA: 0x0030721C File Offset: 0x0030541C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Theme>(deep);
		}

		// Token: 0x06013710 RID: 79632 RVA: 0x00307228 File Offset: 0x00305428
		// Note: this type is marked as 'beforefieldinit'.
		static Theme()
		{
			byte[] array = new byte[1];
			Theme.attributeNamespaceIds = array;
			Theme.eleTagNames = new string[] { "themeElements", "objectDefaults", "extraClrSchemeLst", "custClrLst", "extLst" };
			Theme.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
		}

		// Token: 0x04008633 RID: 34355
		private const string tagName = "theme";

		// Token: 0x04008634 RID: 34356
		private const byte tagNsId = 10;

		// Token: 0x04008635 RID: 34357
		internal const int ElementTypeIdConst = 10122;

		// Token: 0x04008636 RID: 34358
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x04008637 RID: 34359
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008638 RID: 34360
		private static readonly string[] eleTagNames;

		// Token: 0x04008639 RID: 34361
		private static readonly byte[] eleNamespaceIds;
	}
}
