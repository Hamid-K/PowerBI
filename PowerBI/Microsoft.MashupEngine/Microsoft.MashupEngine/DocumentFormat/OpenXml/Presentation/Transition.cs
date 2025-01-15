using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A6E RID: 10862
	[ChildElementInfo(typeof(WipeTransition))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ZoomTransition))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	[ChildElementInfo(typeof(BlindsTransition))]
	[ChildElementInfo(typeof(CheckerTransition))]
	[ChildElementInfo(typeof(CircleTransition))]
	[ChildElementInfo(typeof(DissolveTransition))]
	[ChildElementInfo(typeof(CombTransition))]
	[ChildElementInfo(typeof(CoverTransition))]
	[ChildElementInfo(typeof(CutTransition))]
	[ChildElementInfo(typeof(DiamondTransition))]
	[ChildElementInfo(typeof(FadeTransition))]
	[ChildElementInfo(typeof(NewsflashTransition))]
	[ChildElementInfo(typeof(PlusTransition))]
	[ChildElementInfo(typeof(PullTransition))]
	[ChildElementInfo(typeof(PushTransition))]
	[ChildElementInfo(typeof(RandomTransition))]
	[ChildElementInfo(typeof(RandomBarTransition))]
	[ChildElementInfo(typeof(SplitTransition))]
	[ChildElementInfo(typeof(StripsTransition))]
	[ChildElementInfo(typeof(WedgeTransition))]
	[ChildElementInfo(typeof(WheelTransition))]
	[ChildElementInfo(typeof(FlashTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(VortexTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SwitchTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FlipTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RippleTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GlitterTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HoneycombTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PrismTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DoorsTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WindowTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShredTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FerrisTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FlythroughTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WarpTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GalleryTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConveyorTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PanTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RevealTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WheelReverseTransition), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SoundAction))]
	internal class Transition : OpenXmlCompositeElement
	{
		// Token: 0x170072E0 RID: 29408
		// (get) Token: 0x06015F99 RID: 90009 RVA: 0x00325166 File Offset: 0x00323366
		public override string LocalName
		{
			get
			{
				return "transition";
			}
		}

		// Token: 0x170072E1 RID: 29409
		// (get) Token: 0x06015F9A RID: 90010 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072E2 RID: 29410
		// (get) Token: 0x06015F9B RID: 90011 RVA: 0x0032516D File Offset: 0x0032336D
		internal override int ElementTypeId
		{
			get
			{
				return 12280;
			}
		}

		// Token: 0x06015F9C RID: 90012 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170072E3 RID: 29411
		// (get) Token: 0x06015F9D RID: 90013 RVA: 0x00325174 File Offset: 0x00323374
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transition.attributeTagNames;
			}
		}

		// Token: 0x170072E4 RID: 29412
		// (get) Token: 0x06015F9E RID: 90014 RVA: 0x0032517B File Offset: 0x0032337B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transition.attributeNamespaceIds;
			}
		}

		// Token: 0x170072E5 RID: 29413
		// (get) Token: 0x06015F9F RID: 90015 RVA: 0x00325182 File Offset: 0x00323382
		// (set) Token: 0x06015FA0 RID: 90016 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spd")]
		public EnumValue<TransitionSpeedValues> Speed
		{
			get
			{
				return (EnumValue<TransitionSpeedValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170072E6 RID: 29414
		// (get) Token: 0x06015FA1 RID: 90017 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015FA2 RID: 90018 RVA: 0x002BD47A File Offset: 0x002BB67A
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(49, "dur")]
		public StringValue Duration
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170072E7 RID: 29415
		// (get) Token: 0x06015FA3 RID: 90019 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015FA4 RID: 90020 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "advClick")]
		public BooleanValue AdvanceOnClick
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

		// Token: 0x170072E8 RID: 29416
		// (get) Token: 0x06015FA5 RID: 90021 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06015FA6 RID: 90022 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "advTm")]
		public StringValue AdvanceAfterTime
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06015FA7 RID: 90023 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transition()
		{
		}

		// Token: 0x06015FA8 RID: 90024 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015FA9 RID: 90025 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015FAA RID: 90026 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015FAB RID: 90027 RVA: 0x00325194 File Offset: 0x00323394
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "blinds" == name)
			{
				return new BlindsTransition();
			}
			if (24 == namespaceId && "checker" == name)
			{
				return new CheckerTransition();
			}
			if (24 == namespaceId && "circle" == name)
			{
				return new CircleTransition();
			}
			if (24 == namespaceId && "dissolve" == name)
			{
				return new DissolveTransition();
			}
			if (24 == namespaceId && "comb" == name)
			{
				return new CombTransition();
			}
			if (24 == namespaceId && "cover" == name)
			{
				return new CoverTransition();
			}
			if (24 == namespaceId && "cut" == name)
			{
				return new CutTransition();
			}
			if (24 == namespaceId && "diamond" == name)
			{
				return new DiamondTransition();
			}
			if (24 == namespaceId && "fade" == name)
			{
				return new FadeTransition();
			}
			if (24 == namespaceId && "newsflash" == name)
			{
				return new NewsflashTransition();
			}
			if (24 == namespaceId && "plus" == name)
			{
				return new PlusTransition();
			}
			if (24 == namespaceId && "pull" == name)
			{
				return new PullTransition();
			}
			if (24 == namespaceId && "push" == name)
			{
				return new PushTransition();
			}
			if (24 == namespaceId && "random" == name)
			{
				return new RandomTransition();
			}
			if (24 == namespaceId && "randomBar" == name)
			{
				return new RandomBarTransition();
			}
			if (24 == namespaceId && "split" == name)
			{
				return new SplitTransition();
			}
			if (24 == namespaceId && "strips" == name)
			{
				return new StripsTransition();
			}
			if (24 == namespaceId && "wedge" == name)
			{
				return new WedgeTransition();
			}
			if (24 == namespaceId && "wheel" == name)
			{
				return new WheelTransition();
			}
			if (24 == namespaceId && "wipe" == name)
			{
				return new WipeTransition();
			}
			if (24 == namespaceId && "zoom" == name)
			{
				return new ZoomTransition();
			}
			if (49 == namespaceId && "flash" == name)
			{
				return new FlashTransition();
			}
			if (49 == namespaceId && "vortex" == name)
			{
				return new VortexTransition();
			}
			if (49 == namespaceId && "switch" == name)
			{
				return new SwitchTransition();
			}
			if (49 == namespaceId && "flip" == name)
			{
				return new FlipTransition();
			}
			if (49 == namespaceId && "ripple" == name)
			{
				return new RippleTransition();
			}
			if (49 == namespaceId && "glitter" == name)
			{
				return new GlitterTransition();
			}
			if (49 == namespaceId && "honeycomb" == name)
			{
				return new HoneycombTransition();
			}
			if (49 == namespaceId && "prism" == name)
			{
				return new PrismTransition();
			}
			if (49 == namespaceId && "doors" == name)
			{
				return new DoorsTransition();
			}
			if (49 == namespaceId && "window" == name)
			{
				return new WindowTransition();
			}
			if (49 == namespaceId && "shred" == name)
			{
				return new ShredTransition();
			}
			if (49 == namespaceId && "ferris" == name)
			{
				return new FerrisTransition();
			}
			if (49 == namespaceId && "flythrough" == name)
			{
				return new FlythroughTransition();
			}
			if (49 == namespaceId && "warp" == name)
			{
				return new WarpTransition();
			}
			if (49 == namespaceId && "gallery" == name)
			{
				return new GalleryTransition();
			}
			if (49 == namespaceId && "conveyor" == name)
			{
				return new ConveyorTransition();
			}
			if (49 == namespaceId && "pan" == name)
			{
				return new PanTransition();
			}
			if (49 == namespaceId && "reveal" == name)
			{
				return new RevealTransition();
			}
			if (49 == namespaceId && "wheelReverse" == name)
			{
				return new WheelReverseTransition();
			}
			if (24 == namespaceId && "sndAc" == name)
			{
				return new SoundAction();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x06015FAC RID: 90028 RVA: 0x00325594 File Offset: 0x00323794
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spd" == name)
			{
				return new EnumValue<TransitionSpeedValues>();
			}
			if (49 == namespaceId && "dur" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "advClick" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "advTm" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015FAD RID: 90029 RVA: 0x00325603 File Offset: 0x00323803
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transition>(deep);
		}

		// Token: 0x06015FAE RID: 90030 RVA: 0x0032560C File Offset: 0x0032380C
		// Note: this type is marked as 'beforefieldinit'.
		static Transition()
		{
			byte[] array = new byte[4];
			array[1] = 49;
			Transition.attributeNamespaceIds = array;
		}

		// Token: 0x040095A6 RID: 38310
		private const string tagName = "transition";

		// Token: 0x040095A7 RID: 38311
		private const byte tagNsId = 24;

		// Token: 0x040095A8 RID: 38312
		internal const int ElementTypeIdConst = 12280;

		// Token: 0x040095A9 RID: 38313
		private static string[] attributeTagNames = new string[] { "spd", "dur", "advClick", "advTm" };

		// Token: 0x040095AA RID: 38314
		private static byte[] attributeNamespaceIds;
	}
}
