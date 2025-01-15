using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C4 RID: 10692
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AccentChar))]
	[ChildElementInfo(typeof(LimitLocation))]
	[ChildElementInfo(typeof(GrowOperators))]
	[ChildElementInfo(typeof(HideSubArgument))]
	[ChildElementInfo(typeof(HideSuperArgument))]
	internal class NaryProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006DD0 RID: 28112
		// (get) Token: 0x0601547B RID: 87163 RVA: 0x0031D861 File Offset: 0x0031BA61
		public override string LocalName
		{
			get
			{
				return "naryPr";
			}
		}

		// Token: 0x17006DD1 RID: 28113
		// (get) Token: 0x0601547C RID: 87164 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DD2 RID: 28114
		// (get) Token: 0x0601547D RID: 87165 RVA: 0x0031D868 File Offset: 0x0031BA68
		internal override int ElementTypeId
		{
			get
			{
				return 10926;
			}
		}

		// Token: 0x0601547E RID: 87166 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601547F RID: 87167 RVA: 0x00293ECF File Offset: 0x002920CF
		public NaryProperties()
		{
		}

		// Token: 0x06015480 RID: 87168 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NaryProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015481 RID: 87169 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NaryProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015482 RID: 87170 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NaryProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015483 RID: 87171 RVA: 0x0031D870 File Offset: 0x0031BA70
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "chr" == name)
			{
				return new AccentChar();
			}
			if (21 == namespaceId && "limLoc" == name)
			{
				return new LimitLocation();
			}
			if (21 == namespaceId && "grow" == name)
			{
				return new GrowOperators();
			}
			if (21 == namespaceId && "subHide" == name)
			{
				return new HideSubArgument();
			}
			if (21 == namespaceId && "supHide" == name)
			{
				return new HideSuperArgument();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006DD3 RID: 28115
		// (get) Token: 0x06015484 RID: 87172 RVA: 0x0031D90E File Offset: 0x0031BB0E
		internal override string[] ElementTagNames
		{
			get
			{
				return NaryProperties.eleTagNames;
			}
		}

		// Token: 0x17006DD4 RID: 28116
		// (get) Token: 0x06015485 RID: 87173 RVA: 0x0031D915 File Offset: 0x0031BB15
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NaryProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006DD5 RID: 28117
		// (get) Token: 0x06015486 RID: 87174 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006DD6 RID: 28118
		// (get) Token: 0x06015487 RID: 87175 RVA: 0x0031BAAE File Offset: 0x00319CAE
		// (set) Token: 0x06015488 RID: 87176 RVA: 0x0031BAB7 File Offset: 0x00319CB7
		public AccentChar AccentChar
		{
			get
			{
				return base.GetElement<AccentChar>(0);
			}
			set
			{
				base.SetElement<AccentChar>(0, value);
			}
		}

		// Token: 0x17006DD7 RID: 28119
		// (get) Token: 0x06015489 RID: 87177 RVA: 0x0031D91C File Offset: 0x0031BB1C
		// (set) Token: 0x0601548A RID: 87178 RVA: 0x0031D925 File Offset: 0x0031BB25
		public LimitLocation LimitLocation
		{
			get
			{
				return base.GetElement<LimitLocation>(1);
			}
			set
			{
				base.SetElement<LimitLocation>(1, value);
			}
		}

		// Token: 0x17006DD8 RID: 28120
		// (get) Token: 0x0601548B RID: 87179 RVA: 0x0031D92F File Offset: 0x0031BB2F
		// (set) Token: 0x0601548C RID: 87180 RVA: 0x0031D938 File Offset: 0x0031BB38
		public GrowOperators GrowOperators
		{
			get
			{
				return base.GetElement<GrowOperators>(2);
			}
			set
			{
				base.SetElement<GrowOperators>(2, value);
			}
		}

		// Token: 0x17006DD9 RID: 28121
		// (get) Token: 0x0601548D RID: 87181 RVA: 0x0031D942 File Offset: 0x0031BB42
		// (set) Token: 0x0601548E RID: 87182 RVA: 0x0031D94B File Offset: 0x0031BB4B
		public HideSubArgument HideSubArgument
		{
			get
			{
				return base.GetElement<HideSubArgument>(3);
			}
			set
			{
				base.SetElement<HideSubArgument>(3, value);
			}
		}

		// Token: 0x17006DDA RID: 28122
		// (get) Token: 0x0601548F RID: 87183 RVA: 0x0031D955 File Offset: 0x0031BB55
		// (set) Token: 0x06015490 RID: 87184 RVA: 0x0031D95E File Offset: 0x0031BB5E
		public HideSuperArgument HideSuperArgument
		{
			get
			{
				return base.GetElement<HideSuperArgument>(4);
			}
			set
			{
				base.SetElement<HideSuperArgument>(4, value);
			}
		}

		// Token: 0x17006DDB RID: 28123
		// (get) Token: 0x06015491 RID: 87185 RVA: 0x0031C65F File Offset: 0x0031A85F
		// (set) Token: 0x06015492 RID: 87186 RVA: 0x0031C668 File Offset: 0x0031A868
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(5);
			}
			set
			{
				base.SetElement<ControlProperties>(5, value);
			}
		}

		// Token: 0x06015493 RID: 87187 RVA: 0x0031D968 File Offset: 0x0031BB68
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NaryProperties>(deep);
		}

		// Token: 0x0400927D RID: 37501
		private const string tagName = "naryPr";

		// Token: 0x0400927E RID: 37502
		private const byte tagNsId = 21;

		// Token: 0x0400927F RID: 37503
		internal const int ElementTypeIdConst = 10926;

		// Token: 0x04009280 RID: 37504
		private static readonly string[] eleTagNames = new string[] { "chr", "limLoc", "grow", "subHide", "supHide", "ctrlPr" };

		// Token: 0x04009281 RID: 37505
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21, 21, 21 };
	}
}
