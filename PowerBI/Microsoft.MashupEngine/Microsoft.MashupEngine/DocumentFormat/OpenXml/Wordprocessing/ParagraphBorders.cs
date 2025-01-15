using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E26 RID: 11814
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(RightBorder))]
	[ChildElementInfo(typeof(BetweenBorder))]
	[ChildElementInfo(typeof(BarBorder))]
	[ChildElementInfo(typeof(LeftBorder))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ParagraphBorders : OpenXmlCompositeElement
	{
		// Token: 0x17008926 RID: 35110
		// (get) Token: 0x0601910E RID: 102670 RVA: 0x00345E50 File Offset: 0x00344050
		public override string LocalName
		{
			get
			{
				return "pBdr";
			}
		}

		// Token: 0x17008927 RID: 35111
		// (get) Token: 0x0601910F RID: 102671 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008928 RID: 35112
		// (get) Token: 0x06019110 RID: 102672 RVA: 0x00345E57 File Offset: 0x00344057
		internal override int ElementTypeId
		{
			get
			{
				return 11500;
			}
		}

		// Token: 0x06019111 RID: 102673 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019112 RID: 102674 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphBorders()
		{
		}

		// Token: 0x06019113 RID: 102675 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphBorders(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019114 RID: 102676 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphBorders(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019115 RID: 102677 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphBorders(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019116 RID: 102678 RVA: 0x00345E60 File Offset: 0x00344060
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "top" == name)
			{
				return new TopBorder();
			}
			if (23 == namespaceId && "left" == name)
			{
				return new LeftBorder();
			}
			if (23 == namespaceId && "bottom" == name)
			{
				return new BottomBorder();
			}
			if (23 == namespaceId && "right" == name)
			{
				return new RightBorder();
			}
			if (23 == namespaceId && "between" == name)
			{
				return new BetweenBorder();
			}
			if (23 == namespaceId && "bar" == name)
			{
				return new BarBorder();
			}
			return null;
		}

		// Token: 0x17008929 RID: 35113
		// (get) Token: 0x06019117 RID: 102679 RVA: 0x00345EFE File Offset: 0x003440FE
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphBorders.eleTagNames;
			}
		}

		// Token: 0x1700892A RID: 35114
		// (get) Token: 0x06019118 RID: 102680 RVA: 0x00345F05 File Offset: 0x00344105
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphBorders.eleNamespaceIds;
			}
		}

		// Token: 0x1700892B RID: 35115
		// (get) Token: 0x06019119 RID: 102681 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700892C RID: 35116
		// (get) Token: 0x0601911A RID: 102682 RVA: 0x00345F0C File Offset: 0x0034410C
		// (set) Token: 0x0601911B RID: 102683 RVA: 0x00345F15 File Offset: 0x00344115
		public TopBorder TopBorder
		{
			get
			{
				return base.GetElement<TopBorder>(0);
			}
			set
			{
				base.SetElement<TopBorder>(0, value);
			}
		}

		// Token: 0x1700892D RID: 35117
		// (get) Token: 0x0601911C RID: 102684 RVA: 0x00345F1F File Offset: 0x0034411F
		// (set) Token: 0x0601911D RID: 102685 RVA: 0x00345F28 File Offset: 0x00344128
		public LeftBorder LeftBorder
		{
			get
			{
				return base.GetElement<LeftBorder>(1);
			}
			set
			{
				base.SetElement<LeftBorder>(1, value);
			}
		}

		// Token: 0x1700892E RID: 35118
		// (get) Token: 0x0601911E RID: 102686 RVA: 0x00345F32 File Offset: 0x00344132
		// (set) Token: 0x0601911F RID: 102687 RVA: 0x00345F3B File Offset: 0x0034413B
		public BottomBorder BottomBorder
		{
			get
			{
				return base.GetElement<BottomBorder>(2);
			}
			set
			{
				base.SetElement<BottomBorder>(2, value);
			}
		}

		// Token: 0x1700892F RID: 35119
		// (get) Token: 0x06019120 RID: 102688 RVA: 0x00345F45 File Offset: 0x00344145
		// (set) Token: 0x06019121 RID: 102689 RVA: 0x00345F4E File Offset: 0x0034414E
		public RightBorder RightBorder
		{
			get
			{
				return base.GetElement<RightBorder>(3);
			}
			set
			{
				base.SetElement<RightBorder>(3, value);
			}
		}

		// Token: 0x17008930 RID: 35120
		// (get) Token: 0x06019122 RID: 102690 RVA: 0x00345F58 File Offset: 0x00344158
		// (set) Token: 0x06019123 RID: 102691 RVA: 0x00345F61 File Offset: 0x00344161
		public BetweenBorder BetweenBorder
		{
			get
			{
				return base.GetElement<BetweenBorder>(4);
			}
			set
			{
				base.SetElement<BetweenBorder>(4, value);
			}
		}

		// Token: 0x17008931 RID: 35121
		// (get) Token: 0x06019124 RID: 102692 RVA: 0x00345F6B File Offset: 0x0034416B
		// (set) Token: 0x06019125 RID: 102693 RVA: 0x00345F74 File Offset: 0x00344174
		public BarBorder BarBorder
		{
			get
			{
				return base.GetElement<BarBorder>(5);
			}
			set
			{
				base.SetElement<BarBorder>(5, value);
			}
		}

		// Token: 0x06019126 RID: 102694 RVA: 0x00345F7E File Offset: 0x0034417E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphBorders>(deep);
		}

		// Token: 0x0400A6E1 RID: 42721
		private const string tagName = "pBdr";

		// Token: 0x0400A6E2 RID: 42722
		private const byte tagNsId = 23;

		// Token: 0x0400A6E3 RID: 42723
		internal const int ElementTypeIdConst = 11500;

		// Token: 0x0400A6E4 RID: 42724
		private static readonly string[] eleTagNames = new string[] { "top", "left", "bottom", "right", "between", "bar" };

		// Token: 0x0400A6E5 RID: 42725
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
