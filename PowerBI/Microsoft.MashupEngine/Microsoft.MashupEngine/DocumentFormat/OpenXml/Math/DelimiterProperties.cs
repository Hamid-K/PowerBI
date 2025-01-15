using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A1 RID: 10657
	[ChildElementInfo(typeof(BeginChar))]
	[ChildElementInfo(typeof(GrowOperators))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(SeparatorChar))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EndChar))]
	[ChildElementInfo(typeof(ControlProperties))]
	internal class DelimiterProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D1C RID: 27932
		// (get) Token: 0x060152F3 RID: 86771 RVA: 0x0031C998 File Offset: 0x0031AB98
		public override string LocalName
		{
			get
			{
				return "dPr";
			}
		}

		// Token: 0x17006D1D RID: 27933
		// (get) Token: 0x060152F4 RID: 86772 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D1E RID: 27934
		// (get) Token: 0x060152F5 RID: 86773 RVA: 0x0031C99F File Offset: 0x0031AB9F
		internal override int ElementTypeId
		{
			get
			{
				return 10894;
			}
		}

		// Token: 0x060152F6 RID: 86774 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060152F7 RID: 86775 RVA: 0x00293ECF File Offset: 0x002920CF
		public DelimiterProperties()
		{
		}

		// Token: 0x060152F8 RID: 86776 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DelimiterProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060152F9 RID: 86777 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DelimiterProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060152FA RID: 86778 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DelimiterProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060152FB RID: 86779 RVA: 0x0031C9A8 File Offset: 0x0031ABA8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "begChr" == name)
			{
				return new BeginChar();
			}
			if (21 == namespaceId && "sepChr" == name)
			{
				return new SeparatorChar();
			}
			if (21 == namespaceId && "endChr" == name)
			{
				return new EndChar();
			}
			if (21 == namespaceId && "grow" == name)
			{
				return new GrowOperators();
			}
			if (21 == namespaceId && "shp" == name)
			{
				return new Shape();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006D1F RID: 27935
		// (get) Token: 0x060152FC RID: 86780 RVA: 0x0031CA46 File Offset: 0x0031AC46
		internal override string[] ElementTagNames
		{
			get
			{
				return DelimiterProperties.eleTagNames;
			}
		}

		// Token: 0x17006D20 RID: 27936
		// (get) Token: 0x060152FD RID: 86781 RVA: 0x0031CA4D File Offset: 0x0031AC4D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DelimiterProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D21 RID: 27937
		// (get) Token: 0x060152FE RID: 86782 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D22 RID: 27938
		// (get) Token: 0x060152FF RID: 86783 RVA: 0x0031CA54 File Offset: 0x0031AC54
		// (set) Token: 0x06015300 RID: 86784 RVA: 0x0031CA5D File Offset: 0x0031AC5D
		public BeginChar BeginChar
		{
			get
			{
				return base.GetElement<BeginChar>(0);
			}
			set
			{
				base.SetElement<BeginChar>(0, value);
			}
		}

		// Token: 0x17006D23 RID: 27939
		// (get) Token: 0x06015301 RID: 86785 RVA: 0x0031CA67 File Offset: 0x0031AC67
		// (set) Token: 0x06015302 RID: 86786 RVA: 0x0031CA70 File Offset: 0x0031AC70
		public SeparatorChar SeparatorChar
		{
			get
			{
				return base.GetElement<SeparatorChar>(1);
			}
			set
			{
				base.SetElement<SeparatorChar>(1, value);
			}
		}

		// Token: 0x17006D24 RID: 27940
		// (get) Token: 0x06015303 RID: 86787 RVA: 0x0031CA7A File Offset: 0x0031AC7A
		// (set) Token: 0x06015304 RID: 86788 RVA: 0x0031CA83 File Offset: 0x0031AC83
		public EndChar EndChar
		{
			get
			{
				return base.GetElement<EndChar>(2);
			}
			set
			{
				base.SetElement<EndChar>(2, value);
			}
		}

		// Token: 0x17006D25 RID: 27941
		// (get) Token: 0x06015305 RID: 86789 RVA: 0x0031CA8D File Offset: 0x0031AC8D
		// (set) Token: 0x06015306 RID: 86790 RVA: 0x0031CA96 File Offset: 0x0031AC96
		public GrowOperators GrowOperators
		{
			get
			{
				return base.GetElement<GrowOperators>(3);
			}
			set
			{
				base.SetElement<GrowOperators>(3, value);
			}
		}

		// Token: 0x17006D26 RID: 27942
		// (get) Token: 0x06015307 RID: 86791 RVA: 0x0031CAA0 File Offset: 0x0031ACA0
		// (set) Token: 0x06015308 RID: 86792 RVA: 0x0031CAA9 File Offset: 0x0031ACA9
		public Shape Shape
		{
			get
			{
				return base.GetElement<Shape>(4);
			}
			set
			{
				base.SetElement<Shape>(4, value);
			}
		}

		// Token: 0x17006D27 RID: 27943
		// (get) Token: 0x06015309 RID: 86793 RVA: 0x0031C65F File Offset: 0x0031A85F
		// (set) Token: 0x0601530A RID: 86794 RVA: 0x0031C668 File Offset: 0x0031A868
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

		// Token: 0x0601530B RID: 86795 RVA: 0x0031CAB3 File Offset: 0x0031ACB3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DelimiterProperties>(deep);
		}

		// Token: 0x040091FC RID: 37372
		private const string tagName = "dPr";

		// Token: 0x040091FD RID: 37373
		private const byte tagNsId = 21;

		// Token: 0x040091FE RID: 37374
		internal const int ElementTypeIdConst = 10894;

		// Token: 0x040091FF RID: 37375
		private static readonly string[] eleTagNames = new string[] { "begChr", "sepChr", "endChr", "grow", "shp", "ctrlPr" };

		// Token: 0x04009200 RID: 37376
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21, 21, 21 };
	}
}
