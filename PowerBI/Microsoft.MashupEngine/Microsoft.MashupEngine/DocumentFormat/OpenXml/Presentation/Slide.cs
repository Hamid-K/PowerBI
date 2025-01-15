using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029FF RID: 10751
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonSlideData))]
	[ChildElementInfo(typeof(ColorMapOverride))]
	[ChildElementInfo(typeof(Transition))]
	[ChildElementInfo(typeof(Timing))]
	[ChildElementInfo(typeof(SlideExtensionList))]
	internal class Slide : OpenXmlPartRootElement
	{
		// Token: 0x17006EFF RID: 28415
		// (get) Token: 0x06015715 RID: 87829 RVA: 0x0031F324 File Offset: 0x0031D524
		public override string LocalName
		{
			get
			{
				return "sld";
			}
		}

		// Token: 0x17006F00 RID: 28416
		// (get) Token: 0x06015716 RID: 87830 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F01 RID: 28417
		// (get) Token: 0x06015717 RID: 87831 RVA: 0x0031F32B File Offset: 0x0031D52B
		internal override int ElementTypeId
		{
			get
			{
				return 12178;
			}
		}

		// Token: 0x06015718 RID: 87832 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006F02 RID: 28418
		// (get) Token: 0x06015719 RID: 87833 RVA: 0x0031F332 File Offset: 0x0031D532
		internal override string[] AttributeTagNames
		{
			get
			{
				return Slide.attributeTagNames;
			}
		}

		// Token: 0x17006F03 RID: 28419
		// (get) Token: 0x0601571A RID: 87834 RVA: 0x0031F339 File Offset: 0x0031D539
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Slide.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F04 RID: 28420
		// (get) Token: 0x0601571B RID: 87835 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601571C RID: 87836 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006F05 RID: 28421
		// (get) Token: 0x0601571D RID: 87837 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601571E RID: 87838 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17006F06 RID: 28422
		// (get) Token: 0x0601571F RID: 87839 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015720 RID: 87840 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "show")]
		public BooleanValue Show
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

		// Token: 0x06015721 RID: 87841 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Slide(SlidePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06015722 RID: 87842 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SlidePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F07 RID: 28423
		// (get) Token: 0x06015723 RID: 87843 RVA: 0x0031F340 File Offset: 0x0031D540
		// (set) Token: 0x06015724 RID: 87844 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SlidePart SlidePart
		{
			get
			{
				return base.OpenXmlPart as SlidePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06015725 RID: 87845 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Slide(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015726 RID: 87846 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Slide(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015727 RID: 87847 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Slide(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015728 RID: 87848 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Slide()
		{
		}

		// Token: 0x06015729 RID: 87849 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SlidePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601572A RID: 87850 RVA: 0x0031F350 File Offset: 0x0031D550
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
			if (24 == namespaceId && "extLst" == name)
			{
				return new SlideExtensionList();
			}
			return null;
		}

		// Token: 0x17006F08 RID: 28424
		// (get) Token: 0x0601572B RID: 87851 RVA: 0x0031F3D6 File Offset: 0x0031D5D6
		internal override string[] ElementTagNames
		{
			get
			{
				return Slide.eleTagNames;
			}
		}

		// Token: 0x17006F09 RID: 28425
		// (get) Token: 0x0601572C RID: 87852 RVA: 0x0031F3DD File Offset: 0x0031D5DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Slide.eleNamespaceIds;
			}
		}

		// Token: 0x17006F0A RID: 28426
		// (get) Token: 0x0601572D RID: 87853 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F0B RID: 28427
		// (get) Token: 0x0601572E RID: 87854 RVA: 0x0031F3E4 File Offset: 0x0031D5E4
		// (set) Token: 0x0601572F RID: 87855 RVA: 0x0031F3ED File Offset: 0x0031D5ED
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

		// Token: 0x17006F0C RID: 28428
		// (get) Token: 0x06015730 RID: 87856 RVA: 0x0031F3F7 File Offset: 0x0031D5F7
		// (set) Token: 0x06015731 RID: 87857 RVA: 0x0031F400 File Offset: 0x0031D600
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

		// Token: 0x17006F0D RID: 28429
		// (get) Token: 0x06015732 RID: 87858 RVA: 0x0031F40A File Offset: 0x0031D60A
		// (set) Token: 0x06015733 RID: 87859 RVA: 0x0031F413 File Offset: 0x0031D613
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

		// Token: 0x17006F0E RID: 28430
		// (get) Token: 0x06015734 RID: 87860 RVA: 0x0031F41D File Offset: 0x0031D61D
		// (set) Token: 0x06015735 RID: 87861 RVA: 0x0031F426 File Offset: 0x0031D626
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

		// Token: 0x17006F0F RID: 28431
		// (get) Token: 0x06015736 RID: 87862 RVA: 0x0031F430 File Offset: 0x0031D630
		// (set) Token: 0x06015737 RID: 87863 RVA: 0x0031F439 File Offset: 0x0031D639
		public SlideExtensionList SlideExtensionList
		{
			get
			{
				return base.GetElement<SlideExtensionList>(4);
			}
			set
			{
				base.SetElement<SlideExtensionList>(4, value);
			}
		}

		// Token: 0x06015738 RID: 87864 RVA: 0x0031F444 File Offset: 0x0031D644
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
			if (namespaceId == 0 && "show" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015739 RID: 87865 RVA: 0x0031F49B File Offset: 0x0031D69B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Slide>(deep);
		}

		// Token: 0x0601573A RID: 87866 RVA: 0x0031F4A4 File Offset: 0x0031D6A4
		// Note: this type is marked as 'beforefieldinit'.
		static Slide()
		{
			byte[] array = new byte[3];
			Slide.attributeNamespaceIds = array;
			Slide.eleTagNames = new string[] { "cSld", "clrMapOvr", "transition", "timing", "extLst" };
			Slide.eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
		}

		// Token: 0x04009372 RID: 37746
		private const string tagName = "sld";

		// Token: 0x04009373 RID: 37747
		private const byte tagNsId = 24;

		// Token: 0x04009374 RID: 37748
		internal const int ElementTypeIdConst = 12178;

		// Token: 0x04009375 RID: 37749
		private static string[] attributeTagNames = new string[] { "showMasterSp", "showMasterPhAnim", "show" };

		// Token: 0x04009376 RID: 37750
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009377 RID: 37751
		private static readonly string[] eleTagNames;

		// Token: 0x04009378 RID: 37752
		private static readonly byte[] eleNamespaceIds;
	}
}
