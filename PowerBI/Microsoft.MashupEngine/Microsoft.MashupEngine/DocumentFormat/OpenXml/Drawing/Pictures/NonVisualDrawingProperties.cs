using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Pictures
{
	// Token: 0x02002871 RID: 10353
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700668B RID: 26251
		// (get) Token: 0x060143C7 RID: 82887 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x1700668C RID: 26252
		// (get) Token: 0x060143C8 RID: 82888 RVA: 0x000E78AE File Offset: 0x000E5AAE
		internal override byte NamespaceId
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x1700668D RID: 26253
		// (get) Token: 0x060143C9 RID: 82889 RVA: 0x00310C20 File Offset: 0x0030EE20
		internal override int ElementTypeId
		{
			get
			{
				return 10715;
			}
		}

		// Token: 0x060143CA RID: 82890 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700668E RID: 26254
		// (get) Token: 0x060143CB RID: 82891 RVA: 0x00310C27 File Offset: 0x0030EE27
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x1700668F RID: 26255
		// (get) Token: 0x060143CC RID: 82892 RVA: 0x00310C2E File Offset: 0x0030EE2E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17006690 RID: 26256
		// (get) Token: 0x060143CD RID: 82893 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060143CE RID: 82894 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006691 RID: 26257
		// (get) Token: 0x060143CF RID: 82895 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060143D0 RID: 82896 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006692 RID: 26258
		// (get) Token: 0x060143D1 RID: 82897 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060143D2 RID: 82898 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "descr")]
		public StringValue Description
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006693 RID: 26259
		// (get) Token: 0x060143D3 RID: 82899 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060143D4 RID: 82900 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006694 RID: 26260
		// (get) Token: 0x060143D5 RID: 82901 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060143D6 RID: 82902 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "title")]
		public StringValue Title
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x060143D7 RID: 82903 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x060143D8 RID: 82904 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143D9 RID: 82905 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143DA RID: 82906 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060143DB RID: 82907 RVA: 0x00310C38 File Offset: 0x0030EE38
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "hlinkClick" == name)
			{
				return new HyperlinkOnClick();
			}
			if (10 == namespaceId && "hlinkHover" == name)
			{
				return new HyperlinkOnHover();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new NonVisualDrawingPropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x17006695 RID: 26261
		// (get) Token: 0x060143DC RID: 82908 RVA: 0x00310C8E File Offset: 0x0030EE8E
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17006696 RID: 26262
		// (get) Token: 0x060143DD RID: 82909 RVA: 0x00310C95 File Offset: 0x0030EE95
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006697 RID: 26263
		// (get) Token: 0x060143DE RID: 82910 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006698 RID: 26264
		// (get) Token: 0x060143DF RID: 82911 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x060143E0 RID: 82912 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
		public HyperlinkOnClick HyperlinkOnClick
		{
			get
			{
				return base.GetElement<HyperlinkOnClick>(0);
			}
			set
			{
				base.SetElement<HyperlinkOnClick>(0, value);
			}
		}

		// Token: 0x17006699 RID: 26265
		// (get) Token: 0x060143E1 RID: 82913 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x060143E2 RID: 82914 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
		public HyperlinkOnHover HyperlinkOnHover
		{
			get
			{
				return base.GetElement<HyperlinkOnHover>(1);
			}
			set
			{
				base.SetElement<HyperlinkOnHover>(1, value);
			}
		}

		// Token: 0x1700669A RID: 26266
		// (get) Token: 0x060143E3 RID: 82915 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x060143E4 RID: 82916 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
		public NonVisualDrawingPropertiesExtensionList NonVisualDrawingPropertiesExtensionList
		{
			get
			{
				return base.GetElement<NonVisualDrawingPropertiesExtensionList>(2);
			}
			set
			{
				base.SetElement<NonVisualDrawingPropertiesExtensionList>(2, value);
			}
		}

		// Token: 0x060143E5 RID: 82917 RVA: 0x00310C9C File Offset: 0x0030EE9C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "descr" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060143E6 RID: 82918 RVA: 0x00310D1F File Offset: 0x0030EF1F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x060143E7 RID: 82919 RVA: 0x00310D28 File Offset: 0x0030EF28
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04008D36 RID: 36150
		private const string tagName = "cNvPr";

		// Token: 0x04008D37 RID: 36151
		private const byte tagNsId = 17;

		// Token: 0x04008D38 RID: 36152
		internal const int ElementTypeIdConst = 10715;

		// Token: 0x04008D39 RID: 36153
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04008D3A RID: 36154
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D3B RID: 36155
		private static readonly string[] eleTagNames;

		// Token: 0x04008D3C RID: 36156
		private static readonly byte[] eleNamespaceIds;
	}
}
