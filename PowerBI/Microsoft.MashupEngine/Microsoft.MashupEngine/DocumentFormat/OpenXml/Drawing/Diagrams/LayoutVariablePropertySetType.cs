using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200266E RID: 9838
	[ChildElementInfo(typeof(MaxNumberOfChildren))]
	[ChildElementInfo(typeof(AnimateOneByOne))]
	[ChildElementInfo(typeof(AnimationLevel))]
	[ChildElementInfo(typeof(ResizeHandles))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PreferredNumberOfChildren))]
	[ChildElementInfo(typeof(BulletEnabled))]
	[ChildElementInfo(typeof(Direction))]
	[ChildElementInfo(typeof(HierarchyBranch))]
	[ChildElementInfo(typeof(OrganizationChart))]
	internal abstract class LayoutVariablePropertySetType : OpenXmlCompositeElement
	{
		// Token: 0x06012C1B RID: 76827 RVA: 0x002FEDD0 File Offset: 0x002FCFD0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "orgChart" == name)
			{
				return new OrganizationChart();
			}
			if (14 == namespaceId && "chMax" == name)
			{
				return new MaxNumberOfChildren();
			}
			if (14 == namespaceId && "chPref" == name)
			{
				return new PreferredNumberOfChildren();
			}
			if (14 == namespaceId && "bulletEnabled" == name)
			{
				return new BulletEnabled();
			}
			if (14 == namespaceId && "dir" == name)
			{
				return new Direction();
			}
			if (14 == namespaceId && "hierBranch" == name)
			{
				return new HierarchyBranch();
			}
			if (14 == namespaceId && "animOne" == name)
			{
				return new AnimateOneByOne();
			}
			if (14 == namespaceId && "animLvl" == name)
			{
				return new AnimationLevel();
			}
			if (14 == namespaceId && "resizeHandles" == name)
			{
				return new ResizeHandles();
			}
			return null;
		}

		// Token: 0x17005C20 RID: 23584
		// (get) Token: 0x06012C1C RID: 76828 RVA: 0x002FEEB6 File Offset: 0x002FD0B6
		internal override string[] ElementTagNames
		{
			get
			{
				return LayoutVariablePropertySetType.eleTagNames;
			}
		}

		// Token: 0x17005C21 RID: 23585
		// (get) Token: 0x06012C1D RID: 76829 RVA: 0x002FEEBD File Offset: 0x002FD0BD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LayoutVariablePropertySetType.eleNamespaceIds;
			}
		}

		// Token: 0x17005C22 RID: 23586
		// (get) Token: 0x06012C1E RID: 76830 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005C23 RID: 23587
		// (get) Token: 0x06012C1F RID: 76831 RVA: 0x002FEEC4 File Offset: 0x002FD0C4
		// (set) Token: 0x06012C20 RID: 76832 RVA: 0x002FEECD File Offset: 0x002FD0CD
		public OrganizationChart OrganizationChart
		{
			get
			{
				return base.GetElement<OrganizationChart>(0);
			}
			set
			{
				base.SetElement<OrganizationChart>(0, value);
			}
		}

		// Token: 0x17005C24 RID: 23588
		// (get) Token: 0x06012C21 RID: 76833 RVA: 0x002FEED7 File Offset: 0x002FD0D7
		// (set) Token: 0x06012C22 RID: 76834 RVA: 0x002FEEE0 File Offset: 0x002FD0E0
		public MaxNumberOfChildren MaxNumberOfChildren
		{
			get
			{
				return base.GetElement<MaxNumberOfChildren>(1);
			}
			set
			{
				base.SetElement<MaxNumberOfChildren>(1, value);
			}
		}

		// Token: 0x17005C25 RID: 23589
		// (get) Token: 0x06012C23 RID: 76835 RVA: 0x002FEEEA File Offset: 0x002FD0EA
		// (set) Token: 0x06012C24 RID: 76836 RVA: 0x002FEEF3 File Offset: 0x002FD0F3
		public PreferredNumberOfChildren PreferredNumberOfChildren
		{
			get
			{
				return base.GetElement<PreferredNumberOfChildren>(2);
			}
			set
			{
				base.SetElement<PreferredNumberOfChildren>(2, value);
			}
		}

		// Token: 0x17005C26 RID: 23590
		// (get) Token: 0x06012C25 RID: 76837 RVA: 0x002FEEFD File Offset: 0x002FD0FD
		// (set) Token: 0x06012C26 RID: 76838 RVA: 0x002FEF06 File Offset: 0x002FD106
		public BulletEnabled BulletEnabled
		{
			get
			{
				return base.GetElement<BulletEnabled>(3);
			}
			set
			{
				base.SetElement<BulletEnabled>(3, value);
			}
		}

		// Token: 0x17005C27 RID: 23591
		// (get) Token: 0x06012C27 RID: 76839 RVA: 0x002FEF10 File Offset: 0x002FD110
		// (set) Token: 0x06012C28 RID: 76840 RVA: 0x002FEF19 File Offset: 0x002FD119
		public Direction Direction
		{
			get
			{
				return base.GetElement<Direction>(4);
			}
			set
			{
				base.SetElement<Direction>(4, value);
			}
		}

		// Token: 0x17005C28 RID: 23592
		// (get) Token: 0x06012C29 RID: 76841 RVA: 0x002FEF23 File Offset: 0x002FD123
		// (set) Token: 0x06012C2A RID: 76842 RVA: 0x002FEF2C File Offset: 0x002FD12C
		public HierarchyBranch HierarchyBranch
		{
			get
			{
				return base.GetElement<HierarchyBranch>(5);
			}
			set
			{
				base.SetElement<HierarchyBranch>(5, value);
			}
		}

		// Token: 0x17005C29 RID: 23593
		// (get) Token: 0x06012C2B RID: 76843 RVA: 0x002FEF36 File Offset: 0x002FD136
		// (set) Token: 0x06012C2C RID: 76844 RVA: 0x002FEF3F File Offset: 0x002FD13F
		public AnimateOneByOne AnimateOneByOne
		{
			get
			{
				return base.GetElement<AnimateOneByOne>(6);
			}
			set
			{
				base.SetElement<AnimateOneByOne>(6, value);
			}
		}

		// Token: 0x17005C2A RID: 23594
		// (get) Token: 0x06012C2D RID: 76845 RVA: 0x002FEF49 File Offset: 0x002FD149
		// (set) Token: 0x06012C2E RID: 76846 RVA: 0x002FEF52 File Offset: 0x002FD152
		public AnimationLevel AnimationLevel
		{
			get
			{
				return base.GetElement<AnimationLevel>(7);
			}
			set
			{
				base.SetElement<AnimationLevel>(7, value);
			}
		}

		// Token: 0x17005C2B RID: 23595
		// (get) Token: 0x06012C2F RID: 76847 RVA: 0x002FEF5C File Offset: 0x002FD15C
		// (set) Token: 0x06012C30 RID: 76848 RVA: 0x002FEF65 File Offset: 0x002FD165
		public ResizeHandles ResizeHandles
		{
			get
			{
				return base.GetElement<ResizeHandles>(8);
			}
			set
			{
				base.SetElement<ResizeHandles>(8, value);
			}
		}

		// Token: 0x06012C31 RID: 76849 RVA: 0x00293ECF File Offset: 0x002920CF
		protected LayoutVariablePropertySetType()
		{
		}

		// Token: 0x06012C32 RID: 76850 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected LayoutVariablePropertySetType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C33 RID: 76851 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected LayoutVariablePropertySetType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012C34 RID: 76852 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected LayoutVariablePropertySetType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400817C RID: 33148
		private static readonly string[] eleTagNames = new string[] { "orgChart", "chMax", "chPref", "bulletEnabled", "dir", "hierBranch", "animOne", "animLvl", "resizeHandles" };

		// Token: 0x0400817D RID: 33149
		private static readonly byte[] eleNamespaceIds = new byte[] { 14, 14, 14, 14, 14, 14, 14, 14, 14 };
	}
}
