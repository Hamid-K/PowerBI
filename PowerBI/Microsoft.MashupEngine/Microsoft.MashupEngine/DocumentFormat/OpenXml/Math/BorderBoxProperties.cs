using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200299F RID: 10655
	[ChildElementInfo(typeof(HideTop))]
	[ChildElementInfo(typeof(StrikeHorizontal))]
	[ChildElementInfo(typeof(HideBottom))]
	[ChildElementInfo(typeof(HideLeft))]
	[ChildElementInfo(typeof(HideRight))]
	[ChildElementInfo(typeof(StrikeBottomLeftToTopRight))]
	[ChildElementInfo(typeof(StrikeVertical))]
	[ChildElementInfo(typeof(StrikeTopLeftToBottomRight))]
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BorderBoxProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D07 RID: 27911
		// (get) Token: 0x060152C7 RID: 86727 RVA: 0x0031C6DC File Offset: 0x0031A8DC
		public override string LocalName
		{
			get
			{
				return "borderBoxPr";
			}
		}

		// Token: 0x17006D08 RID: 27912
		// (get) Token: 0x060152C8 RID: 86728 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D09 RID: 27913
		// (get) Token: 0x060152C9 RID: 86729 RVA: 0x0031C6E3 File Offset: 0x0031A8E3
		internal override int ElementTypeId
		{
			get
			{
				return 10888;
			}
		}

		// Token: 0x060152CA RID: 86730 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060152CB RID: 86731 RVA: 0x00293ECF File Offset: 0x002920CF
		public BorderBoxProperties()
		{
		}

		// Token: 0x060152CC RID: 86732 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BorderBoxProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060152CD RID: 86733 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BorderBoxProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060152CE RID: 86734 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BorderBoxProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060152CF RID: 86735 RVA: 0x0031C6EC File Offset: 0x0031A8EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "hideTop" == name)
			{
				return new HideTop();
			}
			if (21 == namespaceId && "hideBot" == name)
			{
				return new HideBottom();
			}
			if (21 == namespaceId && "hideLeft" == name)
			{
				return new HideLeft();
			}
			if (21 == namespaceId && "hideRight" == name)
			{
				return new HideRight();
			}
			if (21 == namespaceId && "strikeH" == name)
			{
				return new StrikeHorizontal();
			}
			if (21 == namespaceId && "strikeV" == name)
			{
				return new StrikeVertical();
			}
			if (21 == namespaceId && "strikeBLTR" == name)
			{
				return new StrikeBottomLeftToTopRight();
			}
			if (21 == namespaceId && "strikeTLBR" == name)
			{
				return new StrikeTopLeftToBottomRight();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006D0A RID: 27914
		// (get) Token: 0x060152D0 RID: 86736 RVA: 0x0031C7D2 File Offset: 0x0031A9D2
		internal override string[] ElementTagNames
		{
			get
			{
				return BorderBoxProperties.eleTagNames;
			}
		}

		// Token: 0x17006D0B RID: 27915
		// (get) Token: 0x060152D1 RID: 86737 RVA: 0x0031C7D9 File Offset: 0x0031A9D9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BorderBoxProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D0C RID: 27916
		// (get) Token: 0x060152D2 RID: 86738 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D0D RID: 27917
		// (get) Token: 0x060152D3 RID: 86739 RVA: 0x0031C7E0 File Offset: 0x0031A9E0
		// (set) Token: 0x060152D4 RID: 86740 RVA: 0x0031C7E9 File Offset: 0x0031A9E9
		public HideTop HideTop
		{
			get
			{
				return base.GetElement<HideTop>(0);
			}
			set
			{
				base.SetElement<HideTop>(0, value);
			}
		}

		// Token: 0x17006D0E RID: 27918
		// (get) Token: 0x060152D5 RID: 86741 RVA: 0x0031C7F3 File Offset: 0x0031A9F3
		// (set) Token: 0x060152D6 RID: 86742 RVA: 0x0031C7FC File Offset: 0x0031A9FC
		public HideBottom HideBottom
		{
			get
			{
				return base.GetElement<HideBottom>(1);
			}
			set
			{
				base.SetElement<HideBottom>(1, value);
			}
		}

		// Token: 0x17006D0F RID: 27919
		// (get) Token: 0x060152D7 RID: 86743 RVA: 0x0031C806 File Offset: 0x0031AA06
		// (set) Token: 0x060152D8 RID: 86744 RVA: 0x0031C80F File Offset: 0x0031AA0F
		public HideLeft HideLeft
		{
			get
			{
				return base.GetElement<HideLeft>(2);
			}
			set
			{
				base.SetElement<HideLeft>(2, value);
			}
		}

		// Token: 0x17006D10 RID: 27920
		// (get) Token: 0x060152D9 RID: 86745 RVA: 0x0031C819 File Offset: 0x0031AA19
		// (set) Token: 0x060152DA RID: 86746 RVA: 0x0031C822 File Offset: 0x0031AA22
		public HideRight HideRight
		{
			get
			{
				return base.GetElement<HideRight>(3);
			}
			set
			{
				base.SetElement<HideRight>(3, value);
			}
		}

		// Token: 0x17006D11 RID: 27921
		// (get) Token: 0x060152DB RID: 86747 RVA: 0x0031C82C File Offset: 0x0031AA2C
		// (set) Token: 0x060152DC RID: 86748 RVA: 0x0031C835 File Offset: 0x0031AA35
		public StrikeHorizontal StrikeHorizontal
		{
			get
			{
				return base.GetElement<StrikeHorizontal>(4);
			}
			set
			{
				base.SetElement<StrikeHorizontal>(4, value);
			}
		}

		// Token: 0x17006D12 RID: 27922
		// (get) Token: 0x060152DD RID: 86749 RVA: 0x0031C83F File Offset: 0x0031AA3F
		// (set) Token: 0x060152DE RID: 86750 RVA: 0x0031C848 File Offset: 0x0031AA48
		public StrikeVertical StrikeVertical
		{
			get
			{
				return base.GetElement<StrikeVertical>(5);
			}
			set
			{
				base.SetElement<StrikeVertical>(5, value);
			}
		}

		// Token: 0x17006D13 RID: 27923
		// (get) Token: 0x060152DF RID: 86751 RVA: 0x0031C852 File Offset: 0x0031AA52
		// (set) Token: 0x060152E0 RID: 86752 RVA: 0x0031C85B File Offset: 0x0031AA5B
		public StrikeBottomLeftToTopRight StrikeBottomLeftToTopRight
		{
			get
			{
				return base.GetElement<StrikeBottomLeftToTopRight>(6);
			}
			set
			{
				base.SetElement<StrikeBottomLeftToTopRight>(6, value);
			}
		}

		// Token: 0x17006D14 RID: 27924
		// (get) Token: 0x060152E1 RID: 86753 RVA: 0x0031C865 File Offset: 0x0031AA65
		// (set) Token: 0x060152E2 RID: 86754 RVA: 0x0031C86E File Offset: 0x0031AA6E
		public StrikeTopLeftToBottomRight StrikeTopLeftToBottomRight
		{
			get
			{
				return base.GetElement<StrikeTopLeftToBottomRight>(7);
			}
			set
			{
				base.SetElement<StrikeTopLeftToBottomRight>(7, value);
			}
		}

		// Token: 0x17006D15 RID: 27925
		// (get) Token: 0x060152E3 RID: 86755 RVA: 0x0031C878 File Offset: 0x0031AA78
		// (set) Token: 0x060152E4 RID: 86756 RVA: 0x0031C881 File Offset: 0x0031AA81
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(8);
			}
			set
			{
				base.SetElement<ControlProperties>(8, value);
			}
		}

		// Token: 0x060152E5 RID: 86757 RVA: 0x0031C88B File Offset: 0x0031AA8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BorderBoxProperties>(deep);
		}

		// Token: 0x040091F2 RID: 37362
		private const string tagName = "borderBoxPr";

		// Token: 0x040091F3 RID: 37363
		private const byte tagNsId = 21;

		// Token: 0x040091F4 RID: 37364
		internal const int ElementTypeIdConst = 10888;

		// Token: 0x040091F5 RID: 37365
		private static readonly string[] eleTagNames = new string[] { "hideTop", "hideBot", "hideLeft", "hideRight", "strikeH", "strikeV", "strikeBLTR", "strikeTLBR", "ctrlPr" };

		// Token: 0x040091F6 RID: 37366
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21, 21, 21, 21, 21, 21 };
	}
}
