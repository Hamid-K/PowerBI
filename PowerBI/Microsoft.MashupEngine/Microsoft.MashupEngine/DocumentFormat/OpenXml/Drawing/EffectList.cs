using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002725 RID: 10021
	[ChildElementInfo(typeof(PresetShadow))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SoftEdge))]
	[ChildElementInfo(typeof(FillOverlay))]
	[ChildElementInfo(typeof(Glow))]
	[ChildElementInfo(typeof(InnerShadow))]
	[ChildElementInfo(typeof(OuterShadow))]
	[ChildElementInfo(typeof(Reflection))]
	[ChildElementInfo(typeof(Blur))]
	internal class EffectList : OpenXmlCompositeElement
	{
		// Token: 0x17005FB5 RID: 24501
		// (get) Token: 0x060133D9 RID: 78809 RVA: 0x0030541F File Offset: 0x0030361F
		public override string LocalName
		{
			get
			{
				return "effectLst";
			}
		}

		// Token: 0x17005FB6 RID: 24502
		// (get) Token: 0x060133DA RID: 78810 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FB7 RID: 24503
		// (get) Token: 0x060133DB RID: 78811 RVA: 0x00305426 File Offset: 0x00303626
		internal override int ElementTypeId
		{
			get
			{
				return 10083;
			}
		}

		// Token: 0x060133DC RID: 78812 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060133DD RID: 78813 RVA: 0x00293ECF File Offset: 0x002920CF
		public EffectList()
		{
		}

		// Token: 0x060133DE RID: 78814 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EffectList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060133DF RID: 78815 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EffectList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060133E0 RID: 78816 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EffectList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060133E1 RID: 78817 RVA: 0x00305430 File Offset: 0x00303630
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "blur" == name)
			{
				return new Blur();
			}
			if (10 == namespaceId && "fillOverlay" == name)
			{
				return new FillOverlay();
			}
			if (10 == namespaceId && "glow" == name)
			{
				return new Glow();
			}
			if (10 == namespaceId && "innerShdw" == name)
			{
				return new InnerShadow();
			}
			if (10 == namespaceId && "outerShdw" == name)
			{
				return new OuterShadow();
			}
			if (10 == namespaceId && "prstShdw" == name)
			{
				return new PresetShadow();
			}
			if (10 == namespaceId && "reflection" == name)
			{
				return new Reflection();
			}
			if (10 == namespaceId && "softEdge" == name)
			{
				return new SoftEdge();
			}
			return null;
		}

		// Token: 0x17005FB8 RID: 24504
		// (get) Token: 0x060133E2 RID: 78818 RVA: 0x003054FE File Offset: 0x003036FE
		internal override string[] ElementTagNames
		{
			get
			{
				return EffectList.eleTagNames;
			}
		}

		// Token: 0x17005FB9 RID: 24505
		// (get) Token: 0x060133E3 RID: 78819 RVA: 0x00305505 File Offset: 0x00303705
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return EffectList.eleNamespaceIds;
			}
		}

		// Token: 0x17005FBA RID: 24506
		// (get) Token: 0x060133E4 RID: 78820 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005FBB RID: 24507
		// (get) Token: 0x060133E5 RID: 78821 RVA: 0x0030550C File Offset: 0x0030370C
		// (set) Token: 0x060133E6 RID: 78822 RVA: 0x00305515 File Offset: 0x00303715
		public Blur Blur
		{
			get
			{
				return base.GetElement<Blur>(0);
			}
			set
			{
				base.SetElement<Blur>(0, value);
			}
		}

		// Token: 0x17005FBC RID: 24508
		// (get) Token: 0x060133E7 RID: 78823 RVA: 0x0030551F File Offset: 0x0030371F
		// (set) Token: 0x060133E8 RID: 78824 RVA: 0x00305528 File Offset: 0x00303728
		public FillOverlay FillOverlay
		{
			get
			{
				return base.GetElement<FillOverlay>(1);
			}
			set
			{
				base.SetElement<FillOverlay>(1, value);
			}
		}

		// Token: 0x17005FBD RID: 24509
		// (get) Token: 0x060133E9 RID: 78825 RVA: 0x00305532 File Offset: 0x00303732
		// (set) Token: 0x060133EA RID: 78826 RVA: 0x0030553B File Offset: 0x0030373B
		public Glow Glow
		{
			get
			{
				return base.GetElement<Glow>(2);
			}
			set
			{
				base.SetElement<Glow>(2, value);
			}
		}

		// Token: 0x17005FBE RID: 24510
		// (get) Token: 0x060133EB RID: 78827 RVA: 0x00305545 File Offset: 0x00303745
		// (set) Token: 0x060133EC RID: 78828 RVA: 0x0030554E File Offset: 0x0030374E
		public InnerShadow InnerShadow
		{
			get
			{
				return base.GetElement<InnerShadow>(3);
			}
			set
			{
				base.SetElement<InnerShadow>(3, value);
			}
		}

		// Token: 0x17005FBF RID: 24511
		// (get) Token: 0x060133ED RID: 78829 RVA: 0x00305558 File Offset: 0x00303758
		// (set) Token: 0x060133EE RID: 78830 RVA: 0x00305561 File Offset: 0x00303761
		public OuterShadow OuterShadow
		{
			get
			{
				return base.GetElement<OuterShadow>(4);
			}
			set
			{
				base.SetElement<OuterShadow>(4, value);
			}
		}

		// Token: 0x17005FC0 RID: 24512
		// (get) Token: 0x060133EF RID: 78831 RVA: 0x0030556B File Offset: 0x0030376B
		// (set) Token: 0x060133F0 RID: 78832 RVA: 0x00305574 File Offset: 0x00303774
		public PresetShadow PresetShadow
		{
			get
			{
				return base.GetElement<PresetShadow>(5);
			}
			set
			{
				base.SetElement<PresetShadow>(5, value);
			}
		}

		// Token: 0x17005FC1 RID: 24513
		// (get) Token: 0x060133F1 RID: 78833 RVA: 0x0030557E File Offset: 0x0030377E
		// (set) Token: 0x060133F2 RID: 78834 RVA: 0x00305587 File Offset: 0x00303787
		public Reflection Reflection
		{
			get
			{
				return base.GetElement<Reflection>(6);
			}
			set
			{
				base.SetElement<Reflection>(6, value);
			}
		}

		// Token: 0x17005FC2 RID: 24514
		// (get) Token: 0x060133F3 RID: 78835 RVA: 0x00305591 File Offset: 0x00303791
		// (set) Token: 0x060133F4 RID: 78836 RVA: 0x0030559A File Offset: 0x0030379A
		public SoftEdge SoftEdge
		{
			get
			{
				return base.GetElement<SoftEdge>(7);
			}
			set
			{
				base.SetElement<SoftEdge>(7, value);
			}
		}

		// Token: 0x060133F5 RID: 78837 RVA: 0x003055A4 File Offset: 0x003037A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectList>(deep);
		}

		// Token: 0x04008544 RID: 34116
		private const string tagName = "effectLst";

		// Token: 0x04008545 RID: 34117
		private const byte tagNsId = 10;

		// Token: 0x04008546 RID: 34118
		internal const int ElementTypeIdConst = 10083;

		// Token: 0x04008547 RID: 34119
		private static readonly string[] eleTagNames = new string[] { "blur", "fillOverlay", "glow", "innerShdw", "outerShdw", "prstShdw", "reflection", "softEdge" };

		// Token: 0x04008548 RID: 34120
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10, 10, 10 };
	}
}
