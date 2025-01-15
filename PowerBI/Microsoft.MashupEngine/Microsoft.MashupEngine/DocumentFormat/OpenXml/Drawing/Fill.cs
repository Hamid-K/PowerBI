using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002717 RID: 10007
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	internal class Fill : OpenXmlCompositeElement
	{
		// Token: 0x17005F0E RID: 24334
		// (get) Token: 0x06013284 RID: 78468 RVA: 0x002BF458 File Offset: 0x002BD658
		public override string LocalName
		{
			get
			{
				return "fill";
			}
		}

		// Token: 0x17005F0F RID: 24335
		// (get) Token: 0x06013285 RID: 78469 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F10 RID: 24336
		// (get) Token: 0x06013286 RID: 78470 RVA: 0x0030447B File Offset: 0x0030267B
		internal override int ElementTypeId
		{
			get
			{
				return 10069;
			}
		}

		// Token: 0x06013287 RID: 78471 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013288 RID: 78472 RVA: 0x00293ECF File Offset: 0x002920CF
		public Fill()
		{
		}

		// Token: 0x06013289 RID: 78473 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Fill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601328A RID: 78474 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Fill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601328B RID: 78475 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Fill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601328C RID: 78476 RVA: 0x00304484 File Offset: 0x00302684
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

		// Token: 0x17005F11 RID: 24337
		// (get) Token: 0x0601328D RID: 78477 RVA: 0x00304522 File Offset: 0x00302722
		internal override string[] ElementTagNames
		{
			get
			{
				return Fill.eleTagNames;
			}
		}

		// Token: 0x17005F12 RID: 24338
		// (get) Token: 0x0601328E RID: 78478 RVA: 0x00304529 File Offset: 0x00302729
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Fill.eleNamespaceIds;
			}
		}

		// Token: 0x17005F13 RID: 24339
		// (get) Token: 0x0601328F RID: 78479 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17005F14 RID: 24340
		// (get) Token: 0x06013290 RID: 78480 RVA: 0x002E078C File Offset: 0x002DE98C
		// (set) Token: 0x06013291 RID: 78481 RVA: 0x002E0795 File Offset: 0x002DE995
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

		// Token: 0x17005F15 RID: 24341
		// (get) Token: 0x06013292 RID: 78482 RVA: 0x002E079F File Offset: 0x002DE99F
		// (set) Token: 0x06013293 RID: 78483 RVA: 0x002E07A8 File Offset: 0x002DE9A8
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

		// Token: 0x17005F16 RID: 24342
		// (get) Token: 0x06013294 RID: 78484 RVA: 0x002E07B2 File Offset: 0x002DE9B2
		// (set) Token: 0x06013295 RID: 78485 RVA: 0x002E07BB File Offset: 0x002DE9BB
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

		// Token: 0x17005F17 RID: 24343
		// (get) Token: 0x06013296 RID: 78486 RVA: 0x002E07C5 File Offset: 0x002DE9C5
		// (set) Token: 0x06013297 RID: 78487 RVA: 0x002E07CE File Offset: 0x002DE9CE
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

		// Token: 0x17005F18 RID: 24344
		// (get) Token: 0x06013298 RID: 78488 RVA: 0x002E07D8 File Offset: 0x002DE9D8
		// (set) Token: 0x06013299 RID: 78489 RVA: 0x002E07E1 File Offset: 0x002DE9E1
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

		// Token: 0x17005F19 RID: 24345
		// (get) Token: 0x0601329A RID: 78490 RVA: 0x002E07EB File Offset: 0x002DE9EB
		// (set) Token: 0x0601329B RID: 78491 RVA: 0x002E07F4 File Offset: 0x002DE9F4
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

		// Token: 0x0601329C RID: 78492 RVA: 0x00304530 File Offset: 0x00302730
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Fill>(deep);
		}

		// Token: 0x040084F6 RID: 34038
		private const string tagName = "fill";

		// Token: 0x040084F7 RID: 34039
		private const byte tagNsId = 10;

		// Token: 0x040084F8 RID: 34040
		internal const int ElementTypeIdConst = 10069;

		// Token: 0x040084F9 RID: 34041
		private static readonly string[] eleTagNames = new string[] { "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill" };

		// Token: 0x040084FA RID: 34042
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
