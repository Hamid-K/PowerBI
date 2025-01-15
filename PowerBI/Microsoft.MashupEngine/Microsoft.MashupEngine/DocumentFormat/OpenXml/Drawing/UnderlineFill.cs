using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200275F RID: 10079
	[ChildElementInfo(typeof(BlipFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	internal class UnderlineFill : OpenXmlCompositeElement
	{
		// Token: 0x170060D2 RID: 24786
		// (get) Token: 0x06013674 RID: 79476 RVA: 0x00306A45 File Offset: 0x00304C45
		public override string LocalName
		{
			get
			{
				return "uFill";
			}
		}

		// Token: 0x170060D3 RID: 24787
		// (get) Token: 0x06013675 RID: 79477 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060D4 RID: 24788
		// (get) Token: 0x06013676 RID: 79478 RVA: 0x00306A4C File Offset: 0x00304C4C
		internal override int ElementTypeId
		{
			get
			{
				return 10116;
			}
		}

		// Token: 0x06013677 RID: 79479 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013678 RID: 79480 RVA: 0x00293ECF File Offset: 0x002920CF
		public UnderlineFill()
		{
		}

		// Token: 0x06013679 RID: 79481 RVA: 0x00293ED7 File Offset: 0x002920D7
		public UnderlineFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601367A RID: 79482 RVA: 0x00293EE0 File Offset: 0x002920E0
		public UnderlineFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601367B RID: 79483 RVA: 0x00293EE9 File Offset: 0x002920E9
		public UnderlineFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601367C RID: 79484 RVA: 0x00306A54 File Offset: 0x00304C54
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

		// Token: 0x170060D5 RID: 24789
		// (get) Token: 0x0601367D RID: 79485 RVA: 0x00306AF2 File Offset: 0x00304CF2
		internal override string[] ElementTagNames
		{
			get
			{
				return UnderlineFill.eleTagNames;
			}
		}

		// Token: 0x170060D6 RID: 24790
		// (get) Token: 0x0601367E RID: 79486 RVA: 0x00306AF9 File Offset: 0x00304CF9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return UnderlineFill.eleNamespaceIds;
			}
		}

		// Token: 0x170060D7 RID: 24791
		// (get) Token: 0x0601367F RID: 79487 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170060D8 RID: 24792
		// (get) Token: 0x06013680 RID: 79488 RVA: 0x002E078C File Offset: 0x002DE98C
		// (set) Token: 0x06013681 RID: 79489 RVA: 0x002E0795 File Offset: 0x002DE995
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

		// Token: 0x170060D9 RID: 24793
		// (get) Token: 0x06013682 RID: 79490 RVA: 0x002E079F File Offset: 0x002DE99F
		// (set) Token: 0x06013683 RID: 79491 RVA: 0x002E07A8 File Offset: 0x002DE9A8
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

		// Token: 0x170060DA RID: 24794
		// (get) Token: 0x06013684 RID: 79492 RVA: 0x002E07B2 File Offset: 0x002DE9B2
		// (set) Token: 0x06013685 RID: 79493 RVA: 0x002E07BB File Offset: 0x002DE9BB
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

		// Token: 0x170060DB RID: 24795
		// (get) Token: 0x06013686 RID: 79494 RVA: 0x002E07C5 File Offset: 0x002DE9C5
		// (set) Token: 0x06013687 RID: 79495 RVA: 0x002E07CE File Offset: 0x002DE9CE
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

		// Token: 0x170060DC RID: 24796
		// (get) Token: 0x06013688 RID: 79496 RVA: 0x002E07D8 File Offset: 0x002DE9D8
		// (set) Token: 0x06013689 RID: 79497 RVA: 0x002E07E1 File Offset: 0x002DE9E1
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

		// Token: 0x170060DD RID: 24797
		// (get) Token: 0x0601368A RID: 79498 RVA: 0x002E07EB File Offset: 0x002DE9EB
		// (set) Token: 0x0601368B RID: 79499 RVA: 0x002E07F4 File Offset: 0x002DE9F4
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

		// Token: 0x0601368C RID: 79500 RVA: 0x00306B00 File Offset: 0x00304D00
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnderlineFill>(deep);
		}

		// Token: 0x04008613 RID: 34323
		private const string tagName = "uFill";

		// Token: 0x04008614 RID: 34324
		private const byte tagNsId = 10;

		// Token: 0x04008615 RID: 34325
		internal const int ElementTypeIdConst = 10116;

		// Token: 0x04008616 RID: 34326
		private static readonly string[] eleTagNames = new string[] { "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill" };

		// Token: 0x04008617 RID: 34327
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
