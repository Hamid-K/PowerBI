using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029FD RID: 10749
	[ChildElementInfo(typeof(CustomerDataList))]
	[ChildElementInfo(typeof(DefaultTextStyle))]
	[ChildElementInfo(typeof(ModificationVerifier))]
	[ChildElementInfo(typeof(Kinsoku))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HandoutMasterIdList))]
	[ChildElementInfo(typeof(SlideIdList))]
	[ChildElementInfo(typeof(SlideSize))]
	[ChildElementInfo(typeof(NotesSize))]
	[ChildElementInfo(typeof(SmartTags))]
	[ChildElementInfo(typeof(EmbeddedFontList))]
	[ChildElementInfo(typeof(CustomShowList))]
	[ChildElementInfo(typeof(PresentationExtensionList))]
	[ChildElementInfo(typeof(NotesMasterIdList))]
	[ChildElementInfo(typeof(PhotoAlbum))]
	[ChildElementInfo(typeof(SlideMasterIdList))]
	internal class Presentation : OpenXmlPartRootElement
	{
		// Token: 0x17006ECE RID: 28366
		// (get) Token: 0x060156AA RID: 87722 RVA: 0x002A9770 File Offset: 0x002A7970
		public override string LocalName
		{
			get
			{
				return "presentation";
			}
		}

		// Token: 0x17006ECF RID: 28367
		// (get) Token: 0x060156AB RID: 87723 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006ED0 RID: 28368
		// (get) Token: 0x060156AC RID: 87724 RVA: 0x0031EC34 File Offset: 0x0031CE34
		internal override int ElementTypeId
		{
			get
			{
				return 12176;
			}
		}

		// Token: 0x060156AD RID: 87725 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006ED1 RID: 28369
		// (get) Token: 0x060156AE RID: 87726 RVA: 0x0031EC3B File Offset: 0x0031CE3B
		internal override string[] AttributeTagNames
		{
			get
			{
				return Presentation.attributeTagNames;
			}
		}

		// Token: 0x17006ED2 RID: 28370
		// (get) Token: 0x060156AF RID: 87727 RVA: 0x0031EC42 File Offset: 0x0031CE42
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Presentation.attributeNamespaceIds;
			}
		}

		// Token: 0x17006ED3 RID: 28371
		// (get) Token: 0x060156B0 RID: 87728 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060156B1 RID: 87729 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "serverZoom")]
		public Int32Value ServerZoom
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

		// Token: 0x17006ED4 RID: 28372
		// (get) Token: 0x060156B2 RID: 87730 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060156B3 RID: 87731 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "firstSlideNum")]
		public Int32Value FirstSlideNum
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006ED5 RID: 28373
		// (get) Token: 0x060156B4 RID: 87732 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060156B5 RID: 87733 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showSpecialPlsOnTitleSld")]
		public BooleanValue ShowSpecialPlaceholderOnTitleSlide
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006ED6 RID: 28374
		// (get) Token: 0x060156B6 RID: 87734 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060156B7 RID: 87735 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "rtl")]
		public BooleanValue RightToLeft
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006ED7 RID: 28375
		// (get) Token: 0x060156B8 RID: 87736 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060156B9 RID: 87737 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "removePersonalInfoOnSave")]
		public BooleanValue RemovePersonalInfoOnSave
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17006ED8 RID: 28376
		// (get) Token: 0x060156BA RID: 87738 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060156BB RID: 87739 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "compatMode")]
		public BooleanValue CompatibilityMode
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17006ED9 RID: 28377
		// (get) Token: 0x060156BC RID: 87740 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060156BD RID: 87741 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "strictFirstAndLastChars")]
		public BooleanValue StrictFirstAndLastChars
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17006EDA RID: 28378
		// (get) Token: 0x060156BE RID: 87742 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060156BF RID: 87743 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "embedTrueTypeFonts")]
		public BooleanValue EmbedTrueTypeFonts
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17006EDB RID: 28379
		// (get) Token: 0x060156C0 RID: 87744 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060156C1 RID: 87745 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "saveSubsetFonts")]
		public BooleanValue SaveSubsetFonts
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17006EDC RID: 28380
		// (get) Token: 0x060156C2 RID: 87746 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060156C3 RID: 87747 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "autoCompressPictures")]
		public BooleanValue AutoCompressPictures
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17006EDD RID: 28381
		// (get) Token: 0x060156C4 RID: 87748 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x060156C5 RID: 87749 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "bookmarkIdSeed")]
		public UInt32Value BookmarkIdSeed
		{
			get
			{
				return (UInt32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17006EDE RID: 28382
		// (get) Token: 0x060156C6 RID: 87750 RVA: 0x0031EC59 File Offset: 0x0031CE59
		// (set) Token: 0x060156C7 RID: 87751 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "conformance")]
		public EnumValue<ConformanceClassValues> Conformance
		{
			get
			{
				return (EnumValue<ConformanceClassValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x060156C8 RID: 87752 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Presentation(PresentationPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060156C9 RID: 87753 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(PresentationPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006EDF RID: 28383
		// (get) Token: 0x060156CA RID: 87754 RVA: 0x0031EC69 File Offset: 0x0031CE69
		// (set) Token: 0x060156CB RID: 87755 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public PresentationPart PresentationPart
		{
			get
			{
				return base.OpenXmlPart as PresentationPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060156CC RID: 87756 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Presentation(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060156CD RID: 87757 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Presentation(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060156CE RID: 87758 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Presentation(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060156CF RID: 87759 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Presentation()
		{
		}

		// Token: 0x060156D0 RID: 87760 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(PresentationPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060156D1 RID: 87761 RVA: 0x0031EC78 File Offset: 0x0031CE78
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sldMasterIdLst" == name)
			{
				return new SlideMasterIdList();
			}
			if (24 == namespaceId && "notesMasterIdLst" == name)
			{
				return new NotesMasterIdList();
			}
			if (24 == namespaceId && "handoutMasterIdLst" == name)
			{
				return new HandoutMasterIdList();
			}
			if (24 == namespaceId && "sldIdLst" == name)
			{
				return new SlideIdList();
			}
			if (24 == namespaceId && "sldSz" == name)
			{
				return new SlideSize();
			}
			if (24 == namespaceId && "notesSz" == name)
			{
				return new NotesSize();
			}
			if (24 == namespaceId && "smartTags" == name)
			{
				return new SmartTags();
			}
			if (24 == namespaceId && "embeddedFontLst" == name)
			{
				return new EmbeddedFontList();
			}
			if (24 == namespaceId && "custShowLst" == name)
			{
				return new CustomShowList();
			}
			if (24 == namespaceId && "photoAlbum" == name)
			{
				return new PhotoAlbum();
			}
			if (24 == namespaceId && "custDataLst" == name)
			{
				return new CustomerDataList();
			}
			if (24 == namespaceId && "kinsoku" == name)
			{
				return new Kinsoku();
			}
			if (24 == namespaceId && "defaultTextStyle" == name)
			{
				return new DefaultTextStyle();
			}
			if (24 == namespaceId && "modifyVerifier" == name)
			{
				return new ModificationVerifier();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new PresentationExtensionList();
			}
			return null;
		}

		// Token: 0x17006EE0 RID: 28384
		// (get) Token: 0x060156D2 RID: 87762 RVA: 0x0031EDEE File Offset: 0x0031CFEE
		internal override string[] ElementTagNames
		{
			get
			{
				return Presentation.eleTagNames;
			}
		}

		// Token: 0x17006EE1 RID: 28385
		// (get) Token: 0x060156D3 RID: 87763 RVA: 0x0031EDF5 File Offset: 0x0031CFF5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Presentation.eleNamespaceIds;
			}
		}

		// Token: 0x17006EE2 RID: 28386
		// (get) Token: 0x060156D4 RID: 87764 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006EE3 RID: 28387
		// (get) Token: 0x060156D5 RID: 87765 RVA: 0x0031EDFC File Offset: 0x0031CFFC
		// (set) Token: 0x060156D6 RID: 87766 RVA: 0x0031EE05 File Offset: 0x0031D005
		public SlideMasterIdList SlideMasterIdList
		{
			get
			{
				return base.GetElement<SlideMasterIdList>(0);
			}
			set
			{
				base.SetElement<SlideMasterIdList>(0, value);
			}
		}

		// Token: 0x17006EE4 RID: 28388
		// (get) Token: 0x060156D7 RID: 87767 RVA: 0x0031EE0F File Offset: 0x0031D00F
		// (set) Token: 0x060156D8 RID: 87768 RVA: 0x0031EE18 File Offset: 0x0031D018
		public NotesMasterIdList NotesMasterIdList
		{
			get
			{
				return base.GetElement<NotesMasterIdList>(1);
			}
			set
			{
				base.SetElement<NotesMasterIdList>(1, value);
			}
		}

		// Token: 0x17006EE5 RID: 28389
		// (get) Token: 0x060156D9 RID: 87769 RVA: 0x0031EE22 File Offset: 0x0031D022
		// (set) Token: 0x060156DA RID: 87770 RVA: 0x0031EE2B File Offset: 0x0031D02B
		public HandoutMasterIdList HandoutMasterIdList
		{
			get
			{
				return base.GetElement<HandoutMasterIdList>(2);
			}
			set
			{
				base.SetElement<HandoutMasterIdList>(2, value);
			}
		}

		// Token: 0x17006EE6 RID: 28390
		// (get) Token: 0x060156DB RID: 87771 RVA: 0x0031EE35 File Offset: 0x0031D035
		// (set) Token: 0x060156DC RID: 87772 RVA: 0x0031EE3E File Offset: 0x0031D03E
		public SlideIdList SlideIdList
		{
			get
			{
				return base.GetElement<SlideIdList>(3);
			}
			set
			{
				base.SetElement<SlideIdList>(3, value);
			}
		}

		// Token: 0x17006EE7 RID: 28391
		// (get) Token: 0x060156DD RID: 87773 RVA: 0x0031EE48 File Offset: 0x0031D048
		// (set) Token: 0x060156DE RID: 87774 RVA: 0x0031EE51 File Offset: 0x0031D051
		public SlideSize SlideSize
		{
			get
			{
				return base.GetElement<SlideSize>(4);
			}
			set
			{
				base.SetElement<SlideSize>(4, value);
			}
		}

		// Token: 0x17006EE8 RID: 28392
		// (get) Token: 0x060156DF RID: 87775 RVA: 0x0031EE5B File Offset: 0x0031D05B
		// (set) Token: 0x060156E0 RID: 87776 RVA: 0x0031EE64 File Offset: 0x0031D064
		public NotesSize NotesSize
		{
			get
			{
				return base.GetElement<NotesSize>(5);
			}
			set
			{
				base.SetElement<NotesSize>(5, value);
			}
		}

		// Token: 0x17006EE9 RID: 28393
		// (get) Token: 0x060156E1 RID: 87777 RVA: 0x0031EE6E File Offset: 0x0031D06E
		// (set) Token: 0x060156E2 RID: 87778 RVA: 0x0031EE77 File Offset: 0x0031D077
		public SmartTags SmartTags
		{
			get
			{
				return base.GetElement<SmartTags>(6);
			}
			set
			{
				base.SetElement<SmartTags>(6, value);
			}
		}

		// Token: 0x17006EEA RID: 28394
		// (get) Token: 0x060156E3 RID: 87779 RVA: 0x0031EE81 File Offset: 0x0031D081
		// (set) Token: 0x060156E4 RID: 87780 RVA: 0x0031EE8A File Offset: 0x0031D08A
		public EmbeddedFontList EmbeddedFontList
		{
			get
			{
				return base.GetElement<EmbeddedFontList>(7);
			}
			set
			{
				base.SetElement<EmbeddedFontList>(7, value);
			}
		}

		// Token: 0x17006EEB RID: 28395
		// (get) Token: 0x060156E5 RID: 87781 RVA: 0x0031EE94 File Offset: 0x0031D094
		// (set) Token: 0x060156E6 RID: 87782 RVA: 0x0031EE9D File Offset: 0x0031D09D
		public CustomShowList CustomShowList
		{
			get
			{
				return base.GetElement<CustomShowList>(8);
			}
			set
			{
				base.SetElement<CustomShowList>(8, value);
			}
		}

		// Token: 0x17006EEC RID: 28396
		// (get) Token: 0x060156E7 RID: 87783 RVA: 0x0031EEA7 File Offset: 0x0031D0A7
		// (set) Token: 0x060156E8 RID: 87784 RVA: 0x0031EEB1 File Offset: 0x0031D0B1
		public PhotoAlbum PhotoAlbum
		{
			get
			{
				return base.GetElement<PhotoAlbum>(9);
			}
			set
			{
				base.SetElement<PhotoAlbum>(9, value);
			}
		}

		// Token: 0x17006EED RID: 28397
		// (get) Token: 0x060156E9 RID: 87785 RVA: 0x0031EEBC File Offset: 0x0031D0BC
		// (set) Token: 0x060156EA RID: 87786 RVA: 0x0031EEC6 File Offset: 0x0031D0C6
		public CustomerDataList CustomerDataList
		{
			get
			{
				return base.GetElement<CustomerDataList>(10);
			}
			set
			{
				base.SetElement<CustomerDataList>(10, value);
			}
		}

		// Token: 0x17006EEE RID: 28398
		// (get) Token: 0x060156EB RID: 87787 RVA: 0x0031EED1 File Offset: 0x0031D0D1
		// (set) Token: 0x060156EC RID: 87788 RVA: 0x0031EEDB File Offset: 0x0031D0DB
		public Kinsoku Kinsoku
		{
			get
			{
				return base.GetElement<Kinsoku>(11);
			}
			set
			{
				base.SetElement<Kinsoku>(11, value);
			}
		}

		// Token: 0x17006EEF RID: 28399
		// (get) Token: 0x060156ED RID: 87789 RVA: 0x0031EEE6 File Offset: 0x0031D0E6
		// (set) Token: 0x060156EE RID: 87790 RVA: 0x0031EEF0 File Offset: 0x0031D0F0
		public DefaultTextStyle DefaultTextStyle
		{
			get
			{
				return base.GetElement<DefaultTextStyle>(12);
			}
			set
			{
				base.SetElement<DefaultTextStyle>(12, value);
			}
		}

		// Token: 0x17006EF0 RID: 28400
		// (get) Token: 0x060156EF RID: 87791 RVA: 0x0031EEFB File Offset: 0x0031D0FB
		// (set) Token: 0x060156F0 RID: 87792 RVA: 0x0031EF05 File Offset: 0x0031D105
		public ModificationVerifier ModificationVerifier
		{
			get
			{
				return base.GetElement<ModificationVerifier>(13);
			}
			set
			{
				base.SetElement<ModificationVerifier>(13, value);
			}
		}

		// Token: 0x17006EF1 RID: 28401
		// (get) Token: 0x060156F1 RID: 87793 RVA: 0x0031EF10 File Offset: 0x0031D110
		// (set) Token: 0x060156F2 RID: 87794 RVA: 0x0031EF1A File Offset: 0x0031D11A
		public PresentationExtensionList PresentationExtensionList
		{
			get
			{
				return base.GetElement<PresentationExtensionList>(14);
			}
			set
			{
				base.SetElement<PresentationExtensionList>(14, value);
			}
		}

		// Token: 0x060156F3 RID: 87795 RVA: 0x0031EF28 File Offset: 0x0031D128
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "serverZoom" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "firstSlideNum" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "showSpecialPlsOnTitleSld" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rtl" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "removePersonalInfoOnSave" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "compatMode" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "strictFirstAndLastChars" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "embedTrueTypeFonts" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "saveSubsetFonts" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoCompressPictures" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "bookmarkIdSeed" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "conformance" == name)
			{
				return new EnumValue<ConformanceClassValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060156F4 RID: 87796 RVA: 0x0031F045 File Offset: 0x0031D245
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Presentation>(deep);
		}

		// Token: 0x060156F5 RID: 87797 RVA: 0x0031F050 File Offset: 0x0031D250
		// Note: this type is marked as 'beforefieldinit'.
		static Presentation()
		{
			byte[] array = new byte[12];
			Presentation.attributeNamespaceIds = array;
			Presentation.eleTagNames = new string[]
			{
				"sldMasterIdLst", "notesMasterIdLst", "handoutMasterIdLst", "sldIdLst", "sldSz", "notesSz", "smartTags", "embeddedFontLst", "custShowLst", "photoAlbum",
				"custDataLst", "kinsoku", "defaultTextStyle", "modifyVerifier", "extLst"
			};
			Presentation.eleNamespaceIds = new byte[]
			{
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24
			};
		}

		// Token: 0x04009366 RID: 37734
		private const string tagName = "presentation";

		// Token: 0x04009367 RID: 37735
		private const byte tagNsId = 24;

		// Token: 0x04009368 RID: 37736
		internal const int ElementTypeIdConst = 12176;

		// Token: 0x04009369 RID: 37737
		private static string[] attributeTagNames = new string[]
		{
			"serverZoom", "firstSlideNum", "showSpecialPlsOnTitleSld", "rtl", "removePersonalInfoOnSave", "compatMode", "strictFirstAndLastChars", "embedTrueTypeFonts", "saveSubsetFonts", "autoCompressPictures",
			"bookmarkIdSeed", "conformance"
		};

		// Token: 0x0400936A RID: 37738
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400936B RID: 37739
		private static readonly string[] eleTagNames;

		// Token: 0x0400936C RID: 37740
		private static readonly byte[] eleNamespaceIds;
	}
}
