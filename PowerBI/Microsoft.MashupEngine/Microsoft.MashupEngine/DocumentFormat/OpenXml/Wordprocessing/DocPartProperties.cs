using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD7 RID: 12247
	[ChildElementInfo(typeof(Category))]
	[ChildElementInfo(typeof(DocPartTypes))]
	[ChildElementInfo(typeof(Behaviors))]
	[ChildElementInfo(typeof(DocPartId))]
	[ChildElementInfo(typeof(StyleId))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocPartName))]
	[ChildElementInfo(typeof(Description))]
	internal class DocPartProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009463 RID: 37987
		// (get) Token: 0x0601A95D RID: 108893 RVA: 0x003647CC File Offset: 0x003629CC
		public override string LocalName
		{
			get
			{
				return "docPartPr";
			}
		}

		// Token: 0x17009464 RID: 37988
		// (get) Token: 0x0601A95E RID: 108894 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009465 RID: 37989
		// (get) Token: 0x0601A95F RID: 108895 RVA: 0x003647D3 File Offset: 0x003629D3
		internal override int ElementTypeId
		{
			get
			{
				return 11955;
			}
		}

		// Token: 0x0601A960 RID: 108896 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A961 RID: 108897 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocPartProperties()
		{
		}

		// Token: 0x0601A962 RID: 108898 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A963 RID: 108899 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A964 RID: 108900 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A965 RID: 108901 RVA: 0x003647DC File Offset: 0x003629DC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new DocPartName();
			}
			if (23 == namespaceId && "style" == name)
			{
				return new StyleId();
			}
			if (23 == namespaceId && "category" == name)
			{
				return new Category();
			}
			if (23 == namespaceId && "types" == name)
			{
				return new DocPartTypes();
			}
			if (23 == namespaceId && "behaviors" == name)
			{
				return new Behaviors();
			}
			if (23 == namespaceId && "description" == name)
			{
				return new Description();
			}
			if (23 == namespaceId && "guid" == name)
			{
				return new DocPartId();
			}
			return null;
		}

		// Token: 0x17009466 RID: 37990
		// (get) Token: 0x0601A966 RID: 108902 RVA: 0x00364892 File Offset: 0x00362A92
		internal override string[] ElementTagNames
		{
			get
			{
				return DocPartProperties.eleTagNames;
			}
		}

		// Token: 0x17009467 RID: 37991
		// (get) Token: 0x0601A967 RID: 108903 RVA: 0x00364899 File Offset: 0x00362A99
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DocPartProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17009468 RID: 37992
		// (get) Token: 0x0601A968 RID: 108904 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009469 RID: 37993
		// (get) Token: 0x0601A969 RID: 108905 RVA: 0x003648A0 File Offset: 0x00362AA0
		// (set) Token: 0x0601A96A RID: 108906 RVA: 0x003648A9 File Offset: 0x00362AA9
		public DocPartName DocPartName
		{
			get
			{
				return base.GetElement<DocPartName>(0);
			}
			set
			{
				base.SetElement<DocPartName>(0, value);
			}
		}

		// Token: 0x1700946A RID: 37994
		// (get) Token: 0x0601A96B RID: 108907 RVA: 0x003648B3 File Offset: 0x00362AB3
		// (set) Token: 0x0601A96C RID: 108908 RVA: 0x003648BC File Offset: 0x00362ABC
		public StyleId StyleId
		{
			get
			{
				return base.GetElement<StyleId>(1);
			}
			set
			{
				base.SetElement<StyleId>(1, value);
			}
		}

		// Token: 0x1700946B RID: 37995
		// (get) Token: 0x0601A96D RID: 108909 RVA: 0x003648C6 File Offset: 0x00362AC6
		// (set) Token: 0x0601A96E RID: 108910 RVA: 0x003648CF File Offset: 0x00362ACF
		public Category Category
		{
			get
			{
				return base.GetElement<Category>(2);
			}
			set
			{
				base.SetElement<Category>(2, value);
			}
		}

		// Token: 0x1700946C RID: 37996
		// (get) Token: 0x0601A96F RID: 108911 RVA: 0x003648D9 File Offset: 0x00362AD9
		// (set) Token: 0x0601A970 RID: 108912 RVA: 0x003648E2 File Offset: 0x00362AE2
		public DocPartTypes DocPartTypes
		{
			get
			{
				return base.GetElement<DocPartTypes>(3);
			}
			set
			{
				base.SetElement<DocPartTypes>(3, value);
			}
		}

		// Token: 0x1700946D RID: 37997
		// (get) Token: 0x0601A971 RID: 108913 RVA: 0x003648EC File Offset: 0x00362AEC
		// (set) Token: 0x0601A972 RID: 108914 RVA: 0x003648F5 File Offset: 0x00362AF5
		public Behaviors Behaviors
		{
			get
			{
				return base.GetElement<Behaviors>(4);
			}
			set
			{
				base.SetElement<Behaviors>(4, value);
			}
		}

		// Token: 0x1700946E RID: 37998
		// (get) Token: 0x0601A973 RID: 108915 RVA: 0x003648FF File Offset: 0x00362AFF
		// (set) Token: 0x0601A974 RID: 108916 RVA: 0x00364908 File Offset: 0x00362B08
		public Description Description
		{
			get
			{
				return base.GetElement<Description>(5);
			}
			set
			{
				base.SetElement<Description>(5, value);
			}
		}

		// Token: 0x1700946F RID: 37999
		// (get) Token: 0x0601A975 RID: 108917 RVA: 0x00364912 File Offset: 0x00362B12
		// (set) Token: 0x0601A976 RID: 108918 RVA: 0x0036491B File Offset: 0x00362B1B
		public DocPartId DocPartId
		{
			get
			{
				return base.GetElement<DocPartId>(6);
			}
			set
			{
				base.SetElement<DocPartId>(6, value);
			}
		}

		// Token: 0x0601A977 RID: 108919 RVA: 0x00364925 File Offset: 0x00362B25
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartProperties>(deep);
		}

		// Token: 0x0400ADA2 RID: 44450
		private const string tagName = "docPartPr";

		// Token: 0x0400ADA3 RID: 44451
		private const byte tagNsId = 23;

		// Token: 0x0400ADA4 RID: 44452
		internal const int ElementTypeIdConst = 11955;

		// Token: 0x0400ADA5 RID: 44453
		private static readonly string[] eleTagNames = new string[] { "name", "style", "category", "types", "behaviors", "description", "guid" };

		// Token: 0x0400ADA6 RID: 44454
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23 };
	}
}
