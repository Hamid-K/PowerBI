using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A00 RID: 10752
	[ChildElementInfo(typeof(CommonSlideData))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ColorMapOverride))]
	[ChildElementInfo(typeof(Transition))]
	[ChildElementInfo(typeof(Timing))]
	[ChildElementInfo(typeof(HeaderFooter))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal class SlideLayout : OpenXmlPartRootElement
	{
		// Token: 0x17006F10 RID: 28432
		// (get) Token: 0x0601573B RID: 87867 RVA: 0x0031F52E File Offset: 0x0031D72E
		public override string LocalName
		{
			get
			{
				return "sldLayout";
			}
		}

		// Token: 0x17006F11 RID: 28433
		// (get) Token: 0x0601573C RID: 87868 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F12 RID: 28434
		// (get) Token: 0x0601573D RID: 87869 RVA: 0x0031F535 File Offset: 0x0031D735
		internal override int ElementTypeId
		{
			get
			{
				return 12179;
			}
		}

		// Token: 0x0601573E RID: 87870 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006F13 RID: 28435
		// (get) Token: 0x0601573F RID: 87871 RVA: 0x0031F53C File Offset: 0x0031D73C
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideLayout.attributeTagNames;
			}
		}

		// Token: 0x17006F14 RID: 28436
		// (get) Token: 0x06015740 RID: 87872 RVA: 0x0031F543 File Offset: 0x0031D743
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideLayout.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F15 RID: 28437
		// (get) Token: 0x06015741 RID: 87873 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015742 RID: 87874 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showMasterSp")]
		public BooleanValue ShowMasterShapes
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

		// Token: 0x17006F16 RID: 28438
		// (get) Token: 0x06015743 RID: 87875 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06015744 RID: 87876 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showMasterPhAnim")]
		public BooleanValue ShowMasterPlaceholderAnimations
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006F17 RID: 28439
		// (get) Token: 0x06015745 RID: 87877 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06015746 RID: 87878 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "matchingName")]
		public StringValue MatchingName
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006F18 RID: 28440
		// (get) Token: 0x06015747 RID: 87879 RVA: 0x0031F54A File Offset: 0x0031D74A
		// (set) Token: 0x06015748 RID: 87880 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "type")]
		public EnumValue<SlideLayoutValues> Type
		{
			get
			{
				return (EnumValue<SlideLayoutValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006F19 RID: 28441
		// (get) Token: 0x06015749 RID: 87881 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601574A RID: 87882 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "preserve")]
		public BooleanValue Preserve
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

		// Token: 0x17006F1A RID: 28442
		// (get) Token: 0x0601574B RID: 87883 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0601574C RID: 87884 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "userDrawn")]
		public BooleanValue UserDrawn
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

		// Token: 0x0601574D RID: 87885 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal SlideLayout(SlideLayoutPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601574E RID: 87886 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SlideLayoutPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F1B RID: 28443
		// (get) Token: 0x0601574F RID: 87887 RVA: 0x0031F559 File Offset: 0x0031D759
		// (set) Token: 0x06015750 RID: 87888 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SlideLayoutPart SlideLayoutPart
		{
			get
			{
				return base.OpenXmlPart as SlideLayoutPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06015751 RID: 87889 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public SlideLayout(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015752 RID: 87890 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public SlideLayout(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015753 RID: 87891 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public SlideLayout(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015754 RID: 87892 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public SlideLayout()
		{
		}

		// Token: 0x06015755 RID: 87893 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SlideLayoutPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06015756 RID: 87894 RVA: 0x0031F568 File Offset: 0x0031D768
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cSld" == name)
			{
				return new CommonSlideData();
			}
			if (24 == namespaceId && "clrMapOvr" == name)
			{
				return new ColorMapOverride();
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
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x17006F1C RID: 28444
		// (get) Token: 0x06015757 RID: 87895 RVA: 0x0031F606 File Offset: 0x0031D806
		internal override string[] ElementTagNames
		{
			get
			{
				return SlideLayout.eleTagNames;
			}
		}

		// Token: 0x17006F1D RID: 28445
		// (get) Token: 0x06015758 RID: 87896 RVA: 0x0031F60D File Offset: 0x0031D80D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlideLayout.eleNamespaceIds;
			}
		}

		// Token: 0x17006F1E RID: 28446
		// (get) Token: 0x06015759 RID: 87897 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F1F RID: 28447
		// (get) Token: 0x0601575A RID: 87898 RVA: 0x0031F3E4 File Offset: 0x0031D5E4
		// (set) Token: 0x0601575B RID: 87899 RVA: 0x0031F3ED File Offset: 0x0031D5ED
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

		// Token: 0x17006F20 RID: 28448
		// (get) Token: 0x0601575C RID: 87900 RVA: 0x0031F3F7 File Offset: 0x0031D5F7
		// (set) Token: 0x0601575D RID: 87901 RVA: 0x0031F400 File Offset: 0x0031D600
		public ColorMapOverride ColorMapOverride
		{
			get
			{
				return base.GetElement<ColorMapOverride>(1);
			}
			set
			{
				base.SetElement<ColorMapOverride>(1, value);
			}
		}

		// Token: 0x17006F21 RID: 28449
		// (get) Token: 0x0601575E RID: 87902 RVA: 0x0031F40A File Offset: 0x0031D60A
		// (set) Token: 0x0601575F RID: 87903 RVA: 0x0031F413 File Offset: 0x0031D613
		public Transition Transition
		{
			get
			{
				return base.GetElement<Transition>(2);
			}
			set
			{
				base.SetElement<Transition>(2, value);
			}
		}

		// Token: 0x17006F22 RID: 28450
		// (get) Token: 0x06015760 RID: 87904 RVA: 0x0031F41D File Offset: 0x0031D61D
		// (set) Token: 0x06015761 RID: 87905 RVA: 0x0031F426 File Offset: 0x0031D626
		public Timing Timing
		{
			get
			{
				return base.GetElement<Timing>(3);
			}
			set
			{
				base.SetElement<Timing>(3, value);
			}
		}

		// Token: 0x17006F23 RID: 28451
		// (get) Token: 0x06015762 RID: 87906 RVA: 0x0031F614 File Offset: 0x0031D814
		// (set) Token: 0x06015763 RID: 87907 RVA: 0x0031F61D File Offset: 0x0031D81D
		public HeaderFooter HeaderFooter
		{
			get
			{
				return base.GetElement<HeaderFooter>(4);
			}
			set
			{
				base.SetElement<HeaderFooter>(4, value);
			}
		}

		// Token: 0x17006F24 RID: 28452
		// (get) Token: 0x06015764 RID: 87908 RVA: 0x0031F627 File Offset: 0x0031D827
		// (set) Token: 0x06015765 RID: 87909 RVA: 0x0031F630 File Offset: 0x0031D830
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(5);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(5, value);
			}
		}

		// Token: 0x06015766 RID: 87910 RVA: 0x0031F63C File Offset: 0x0031D83C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "showMasterSp" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showMasterPhAnim" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "matchingName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<SlideLayoutValues>();
			}
			if (namespaceId == 0 && "preserve" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "userDrawn" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015767 RID: 87911 RVA: 0x0031F6D5 File Offset: 0x0031D8D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideLayout>(deep);
		}

		// Token: 0x06015768 RID: 87912 RVA: 0x0031F6E0 File Offset: 0x0031D8E0
		// Note: this type is marked as 'beforefieldinit'.
		static SlideLayout()
		{
			byte[] array = new byte[6];
			SlideLayout.attributeNamespaceIds = array;
			SlideLayout.eleTagNames = new string[] { "cSld", "clrMapOvr", "transition", "timing", "hf", "extLst" };
			SlideLayout.eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24, 24 };
		}

		// Token: 0x04009379 RID: 37753
		private const string tagName = "sldLayout";

		// Token: 0x0400937A RID: 37754
		private const byte tagNsId = 24;

		// Token: 0x0400937B RID: 37755
		internal const int ElementTypeIdConst = 12179;

		// Token: 0x0400937C RID: 37756
		private static string[] attributeTagNames = new string[] { "showMasterSp", "showMasterPhAnim", "matchingName", "type", "preserve", "userDrawn" };

		// Token: 0x0400937D RID: 37757
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400937E RID: 37758
		private static readonly string[] eleTagNames;

		// Token: 0x0400937F RID: 37759
		private static readonly byte[] eleNamespaceIds;
	}
}
