using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200272E RID: 10030
	[ChildElementInfo(typeof(GradientFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	internal class FillProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005FF8 RID: 24568
		// (get) Token: 0x06013469 RID: 78953 RVA: 0x002BF458 File Offset: 0x002BD658
		public override string LocalName
		{
			get
			{
				return "fill";
			}
		}

		// Token: 0x17005FF9 RID: 24569
		// (get) Token: 0x0601346A RID: 78954 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FFA RID: 24570
		// (get) Token: 0x0601346B RID: 78955 RVA: 0x00305A81 File Offset: 0x00303C81
		internal override int ElementTypeId
		{
			get
			{
				return 10093;
			}
		}

		// Token: 0x0601346C RID: 78956 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601346D RID: 78957 RVA: 0x00293ECF File Offset: 0x002920CF
		public FillProperties()
		{
		}

		// Token: 0x0601346E RID: 78958 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FillProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601346F RID: 78959 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FillProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013470 RID: 78960 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FillProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013471 RID: 78961 RVA: 0x00305A88 File Offset: 0x00303C88
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
			}
			return null;
		}

		// Token: 0x17005FFB RID: 24571
		// (get) Token: 0x06013472 RID: 78962 RVA: 0x00305B26 File Offset: 0x00303D26
		internal override string[] ElementTagNames
		{
			get
			{
				return FillProperties.eleTagNames;
			}
		}

		// Token: 0x17005FFC RID: 24572
		// (get) Token: 0x06013473 RID: 78963 RVA: 0x00305B2D File Offset: 0x00303D2D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FillProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005FFD RID: 24573
		// (get) Token: 0x06013474 RID: 78964 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005FFE RID: 24574
		// (get) Token: 0x06013475 RID: 78965 RVA: 0x002E078C File Offset: 0x002DE98C
		// (set) Token: 0x06013476 RID: 78966 RVA: 0x002E0795 File Offset: 0x002DE995
		public NoFill NoFill
		{
			get
			{
				return base.GetElement<NoFill>(0);
			}
			set
			{
				base.SetElement<NoFill>(0, value);
			}
		}

		// Token: 0x17005FFF RID: 24575
		// (get) Token: 0x06013477 RID: 78967 RVA: 0x002E079F File Offset: 0x002DE99F
		// (set) Token: 0x06013478 RID: 78968 RVA: 0x002E07A8 File Offset: 0x002DE9A8
		public SolidFill SolidFill
		{
			get
			{
				return base.GetElement<SolidFill>(1);
			}
			set
			{
				base.SetElement<SolidFill>(1, value);
			}
		}

		// Token: 0x17006000 RID: 24576
		// (get) Token: 0x06013479 RID: 78969 RVA: 0x002E07B2 File Offset: 0x002DE9B2
		// (set) Token: 0x0601347A RID: 78970 RVA: 0x002E07BB File Offset: 0x002DE9BB
		public GradientFill GradientFill
		{
			get
			{
				return base.GetElement<GradientFill>(2);
			}
			set
			{
				base.SetElement<GradientFill>(2, value);
			}
		}

		// Token: 0x17006001 RID: 24577
		// (get) Token: 0x0601347B RID: 78971 RVA: 0x002E07C5 File Offset: 0x002DE9C5
		// (set) Token: 0x0601347C RID: 78972 RVA: 0x002E07CE File Offset: 0x002DE9CE
		public BlipFill BlipFill
		{
			get
			{
				return base.GetElement<BlipFill>(3);
			}
			set
			{
				base.SetElement<BlipFill>(3, value);
			}
		}

		// Token: 0x17006002 RID: 24578
		// (get) Token: 0x0601347D RID: 78973 RVA: 0x002E07D8 File Offset: 0x002DE9D8
		// (set) Token: 0x0601347E RID: 78974 RVA: 0x002E07E1 File Offset: 0x002DE9E1
		public PatternFill PatternFill
		{
			get
			{
				return base.GetElement<PatternFill>(4);
			}
			set
			{
				base.SetElement<PatternFill>(4, value);
			}
		}

		// Token: 0x17006003 RID: 24579
		// (get) Token: 0x0601347F RID: 78975 RVA: 0x002E07EB File Offset: 0x002DE9EB
		// (set) Token: 0x06013480 RID: 78976 RVA: 0x002E07F4 File Offset: 0x002DE9F4
		public GroupFill GroupFill
		{
			get
			{
				return base.GetElement<GroupFill>(5);
			}
			set
			{
				base.SetElement<GroupFill>(5, value);
			}
		}

		// Token: 0x06013481 RID: 78977 RVA: 0x00305B34 File Offset: 0x00303D34
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillProperties>(deep);
		}

		// Token: 0x0400856F RID: 34159
		private const string tagName = "fill";

		// Token: 0x04008570 RID: 34160
		private const byte tagNsId = 10;

		// Token: 0x04008571 RID: 34161
		internal const int ElementTypeIdConst = 10093;

		// Token: 0x04008572 RID: 34162
		private static readonly string[] eleTagNames = new string[] { "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill" };

		// Token: 0x04008573 RID: 34163
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
