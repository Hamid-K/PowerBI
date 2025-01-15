using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A01 RID: 10753
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	[ChildElementInfo(typeof(CommonSlideData))]
	[ChildElementInfo(typeof(ColorMap))]
	[ChildElementInfo(typeof(SlideLayoutIdList))]
	[ChildElementInfo(typeof(Transition))]
	[ChildElementInfo(typeof(Timing))]
	[ChildElementInfo(typeof(HeaderFooter))]
	[ChildElementInfo(typeof(TextStyles))]
	internal class SlideMaster : OpenXmlPartRootElement
	{
		// Token: 0x17006F25 RID: 28453
		// (get) Token: 0x06015769 RID: 87913 RVA: 0x0031F78A File Offset: 0x0031D98A
		public override string LocalName
		{
			get
			{
				return "sldMaster";
			}
		}

		// Token: 0x17006F26 RID: 28454
		// (get) Token: 0x0601576A RID: 87914 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F27 RID: 28455
		// (get) Token: 0x0601576B RID: 87915 RVA: 0x0031F791 File Offset: 0x0031D991
		internal override int ElementTypeId
		{
			get
			{
				return 12180;
			}
		}

		// Token: 0x0601576C RID: 87916 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006F28 RID: 28456
		// (get) Token: 0x0601576D RID: 87917 RVA: 0x0031F798 File Offset: 0x0031D998
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideMaster.attributeTagNames;
			}
		}

		// Token: 0x17006F29 RID: 28457
		// (get) Token: 0x0601576E RID: 87918 RVA: 0x0031F79F File Offset: 0x0031D99F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideMaster.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F2A RID: 28458
		// (get) Token: 0x0601576F RID: 87919 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015770 RID: 87920 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "preserve")]
		public BooleanValue Preserve
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015771 RID: 87921 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal SlideMaster(SlideMasterPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06015772 RID: 87922 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SlideMasterPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F2B RID: 28459
		// (get) Token: 0x06015773 RID: 87923 RVA: 0x0031F7A6 File Offset: 0x0031D9A6
		// (set) Token: 0x06015774 RID: 87924 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SlideMasterPart SlideMasterPart
		{
			get
			{
				return base.OpenXmlPart as SlideMasterPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06015775 RID: 87925 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public SlideMaster(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015776 RID: 87926 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public SlideMaster(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015777 RID: 87927 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public SlideMaster(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015778 RID: 87928 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public SlideMaster()
		{
		}

		// Token: 0x06015779 RID: 87929 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SlideMasterPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601577A RID: 87930 RVA: 0x0031F7B4 File Offset: 0x0031D9B4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cSld" == name)
			{
				return new CommonSlideData();
			}
			if (24 == namespaceId && "clrMap" == name)
			{
				return new ColorMap();
			}
			if (24 == namespaceId && "sldLayoutIdLst" == name)
			{
				return new SlideLayoutIdList();
			}
			if (24 == namespaceId && "transition" == name)
			{
				return new Transition();
			}
			if (24 == namespaceId && "timing" == name)
			{
				return new Timing();
			}
			if (24 == namespaceId && "hf" == name)
			{
				return new HeaderFooter();
			}
			if (24 == namespaceId && "txStyles" == name)
			{
				return new TextStyles();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x17006F2C RID: 28460
		// (get) Token: 0x0601577B RID: 87931 RVA: 0x0031F882 File Offset: 0x0031DA82
		internal override string[] ElementTagNames
		{
			get
			{
				return SlideMaster.eleTagNames;
			}
		}

		// Token: 0x17006F2D RID: 28461
		// (get) Token: 0x0601577C RID: 87932 RVA: 0x0031F889 File Offset: 0x0031DA89
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlideMaster.eleNamespaceIds;
			}
		}

		// Token: 0x17006F2E RID: 28462
		// (get) Token: 0x0601577D RID: 87933 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F2F RID: 28463
		// (get) Token: 0x0601577E RID: 87934 RVA: 0x0031F3E4 File Offset: 0x0031D5E4
		// (set) Token: 0x0601577F RID: 87935 RVA: 0x0031F3ED File Offset: 0x0031D5ED
		public CommonSlideData CommonSlideData
		{
			get
			{
				return base.GetElement<CommonSlideData>(0);
			}
			set
			{
				base.SetElement<CommonSlideData>(0, value);
			}
		}

		// Token: 0x17006F30 RID: 28464
		// (get) Token: 0x06015780 RID: 87936 RVA: 0x0031F890 File Offset: 0x0031DA90
		// (set) Token: 0x06015781 RID: 87937 RVA: 0x0031F899 File Offset: 0x0031DA99
		public ColorMap ColorMap
		{
			get
			{
				return base.GetElement<ColorMap>(1);
			}
			set
			{
				base.SetElement<ColorMap>(1, value);
			}
		}

		// Token: 0x17006F31 RID: 28465
		// (get) Token: 0x06015782 RID: 87938 RVA: 0x0031F8A3 File Offset: 0x0031DAA3
		// (set) Token: 0x06015783 RID: 87939 RVA: 0x0031F8AC File Offset: 0x0031DAAC
		public SlideLayoutIdList SlideLayoutIdList
		{
			get
			{
				return base.GetElement<SlideLayoutIdList>(2);
			}
			set
			{
				base.SetElement<SlideLayoutIdList>(2, value);
			}
		}

		// Token: 0x17006F32 RID: 28466
		// (get) Token: 0x06015784 RID: 87940 RVA: 0x0031F8B6 File Offset: 0x0031DAB6
		// (set) Token: 0x06015785 RID: 87941 RVA: 0x0031F8BF File Offset: 0x0031DABF
		public Transition Transition
		{
			get
			{
				return base.GetElement<Transition>(3);
			}
			set
			{
				base.SetElement<Transition>(3, value);
			}
		}

		// Token: 0x17006F33 RID: 28467
		// (get) Token: 0x06015786 RID: 87942 RVA: 0x0031F8C9 File Offset: 0x0031DAC9
		// (set) Token: 0x06015787 RID: 87943 RVA: 0x0031F8D2 File Offset: 0x0031DAD2
		public Timing Timing
		{
			get
			{
				return base.GetElement<Timing>(4);
			}
			set
			{
				base.SetElement<Timing>(4, value);
			}
		}

		// Token: 0x17006F34 RID: 28468
		// (get) Token: 0x06015788 RID: 87944 RVA: 0x0031F8DC File Offset: 0x0031DADC
		// (set) Token: 0x06015789 RID: 87945 RVA: 0x0031F8E5 File Offset: 0x0031DAE5
		public HeaderFooter HeaderFooter
		{
			get
			{
				return base.GetElement<HeaderFooter>(5);
			}
			set
			{
				base.SetElement<HeaderFooter>(5, value);
			}
		}

		// Token: 0x17006F35 RID: 28469
		// (get) Token: 0x0601578A RID: 87946 RVA: 0x0031F8EF File Offset: 0x0031DAEF
		// (set) Token: 0x0601578B RID: 87947 RVA: 0x0031F8F8 File Offset: 0x0031DAF8
		public TextStyles TextStyles
		{
			get
			{
				return base.GetElement<TextStyles>(6);
			}
			set
			{
				base.SetElement<TextStyles>(6, value);
			}
		}

		// Token: 0x17006F36 RID: 28470
		// (get) Token: 0x0601578C RID: 87948 RVA: 0x0031F902 File Offset: 0x0031DB02
		// (set) Token: 0x0601578D RID: 87949 RVA: 0x0031F90B File Offset: 0x0031DB0B
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(7);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(7, value);
			}
		}

		// Token: 0x0601578E RID: 87950 RVA: 0x0031F915 File Offset: 0x0031DB15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "preserve" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601578F RID: 87951 RVA: 0x0031F935 File Offset: 0x0031DB35
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideMaster>(deep);
		}

		// Token: 0x06015790 RID: 87952 RVA: 0x0031F940 File Offset: 0x0031DB40
		// Note: this type is marked as 'beforefieldinit'.
		static SlideMaster()
		{
			byte[] array = new byte[1];
			SlideMaster.attributeNamespaceIds = array;
			SlideMaster.eleTagNames = new string[] { "cSld", "clrMap", "sldLayoutIdLst", "transition", "timing", "hf", "txStyles", "extLst" };
			SlideMaster.eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24, 24, 24, 24 };
		}

		// Token: 0x04009380 RID: 37760
		private const string tagName = "sldMaster";

		// Token: 0x04009381 RID: 37761
		private const byte tagNsId = 24;

		// Token: 0x04009382 RID: 37762
		internal const int ElementTypeIdConst = 12180;

		// Token: 0x04009383 RID: 37763
		private static string[] attributeTagNames = new string[] { "preserve" };

		// Token: 0x04009384 RID: 37764
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009385 RID: 37765
		private static readonly string[] eleTagNames;

		// Token: 0x04009386 RID: 37766
		private static readonly byte[] eleNamespaceIds;
	}
}
