using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200277A RID: 10106
	[ChildElementInfo(typeof(EffectStyleList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LineStyleList))]
	[ChildElementInfo(typeof(BackgroundFillStyleList))]
	[ChildElementInfo(typeof(FillStyleList))]
	internal class FormatScheme : OpenXmlCompositeElement
	{
		// Token: 0x17006195 RID: 24981
		// (get) Token: 0x06013838 RID: 79928 RVA: 0x00307FA6 File Offset: 0x003061A6
		public override string LocalName
		{
			get
			{
				return "fmtScheme";
			}
		}

		// Token: 0x17006196 RID: 24982
		// (get) Token: 0x06013839 RID: 79929 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006197 RID: 24983
		// (get) Token: 0x0601383A RID: 79930 RVA: 0x00307FAD File Offset: 0x003061AD
		internal override int ElementTypeId
		{
			get
			{
				return 10146;
			}
		}

		// Token: 0x0601383B RID: 79931 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006198 RID: 24984
		// (get) Token: 0x0601383C RID: 79932 RVA: 0x00307FB4 File Offset: 0x003061B4
		internal override string[] AttributeTagNames
		{
			get
			{
				return FormatScheme.attributeTagNames;
			}
		}

		// Token: 0x17006199 RID: 24985
		// (get) Token: 0x0601383D RID: 79933 RVA: 0x00307FBB File Offset: 0x003061BB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FormatScheme.attributeNamespaceIds;
			}
		}

		// Token: 0x1700619A RID: 24986
		// (get) Token: 0x0601383E RID: 79934 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601383F RID: 79935 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06013840 RID: 79936 RVA: 0x00293ECF File Offset: 0x002920CF
		public FormatScheme()
		{
		}

		// Token: 0x06013841 RID: 79937 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FormatScheme(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013842 RID: 79938 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FormatScheme(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013843 RID: 79939 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FormatScheme(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013844 RID: 79940 RVA: 0x00307FC4 File Offset: 0x003061C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "fillStyleLst" == name)
			{
				return new FillStyleList();
			}
			if (10 == namespaceId && "lnStyleLst" == name)
			{
				return new LineStyleList();
			}
			if (10 == namespaceId && "effectStyleLst" == name)
			{
				return new EffectStyleList();
			}
			if (10 == namespaceId && "bgFillStyleLst" == name)
			{
				return new BackgroundFillStyleList();
			}
			return null;
		}

		// Token: 0x1700619B RID: 24987
		// (get) Token: 0x06013845 RID: 79941 RVA: 0x00308032 File Offset: 0x00306232
		internal override string[] ElementTagNames
		{
			get
			{
				return FormatScheme.eleTagNames;
			}
		}

		// Token: 0x1700619C RID: 24988
		// (get) Token: 0x06013846 RID: 79942 RVA: 0x00308039 File Offset: 0x00306239
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FormatScheme.eleNamespaceIds;
			}
		}

		// Token: 0x1700619D RID: 24989
		// (get) Token: 0x06013847 RID: 79943 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700619E RID: 24990
		// (get) Token: 0x06013848 RID: 79944 RVA: 0x00308040 File Offset: 0x00306240
		// (set) Token: 0x06013849 RID: 79945 RVA: 0x00308049 File Offset: 0x00306249
		public FillStyleList FillStyleList
		{
			get
			{
				return base.GetElement<FillStyleList>(0);
			}
			set
			{
				base.SetElement<FillStyleList>(0, value);
			}
		}

		// Token: 0x1700619F RID: 24991
		// (get) Token: 0x0601384A RID: 79946 RVA: 0x00308053 File Offset: 0x00306253
		// (set) Token: 0x0601384B RID: 79947 RVA: 0x0030805C File Offset: 0x0030625C
		public LineStyleList LineStyleList
		{
			get
			{
				return base.GetElement<LineStyleList>(1);
			}
			set
			{
				base.SetElement<LineStyleList>(1, value);
			}
		}

		// Token: 0x170061A0 RID: 24992
		// (get) Token: 0x0601384C RID: 79948 RVA: 0x00308066 File Offset: 0x00306266
		// (set) Token: 0x0601384D RID: 79949 RVA: 0x0030806F File Offset: 0x0030626F
		public EffectStyleList EffectStyleList
		{
			get
			{
				return base.GetElement<EffectStyleList>(2);
			}
			set
			{
				base.SetElement<EffectStyleList>(2, value);
			}
		}

		// Token: 0x170061A1 RID: 24993
		// (get) Token: 0x0601384E RID: 79950 RVA: 0x00308079 File Offset: 0x00306279
		// (set) Token: 0x0601384F RID: 79951 RVA: 0x00308082 File Offset: 0x00306282
		public BackgroundFillStyleList BackgroundFillStyleList
		{
			get
			{
				return base.GetElement<BackgroundFillStyleList>(3);
			}
			set
			{
				base.SetElement<BackgroundFillStyleList>(3, value);
			}
		}

		// Token: 0x06013850 RID: 79952 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013851 RID: 79953 RVA: 0x0030808C File Offset: 0x0030628C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormatScheme>(deep);
		}

		// Token: 0x06013852 RID: 79954 RVA: 0x00308098 File Offset: 0x00306298
		// Note: this type is marked as 'beforefieldinit'.
		static FormatScheme()
		{
			byte[] array = new byte[1];
			FormatScheme.attributeNamespaceIds = array;
			FormatScheme.eleTagNames = new string[] { "fillStyleLst", "lnStyleLst", "effectStyleLst", "bgFillStyleLst" };
			FormatScheme.eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
		}

		// Token: 0x04008688 RID: 34440
		private const string tagName = "fmtScheme";

		// Token: 0x04008689 RID: 34441
		private const byte tagNsId = 10;

		// Token: 0x0400868A RID: 34442
		internal const int ElementTypeIdConst = 10146;

		// Token: 0x0400868B RID: 34443
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x0400868C RID: 34444
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400868D RID: 34445
		private static readonly string[] eleTagNames;

		// Token: 0x0400868E RID: 34446
		private static readonly byte[] eleNamespaceIds;
	}
}
