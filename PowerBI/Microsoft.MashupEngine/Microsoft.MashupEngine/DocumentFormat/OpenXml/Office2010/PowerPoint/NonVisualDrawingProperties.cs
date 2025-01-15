using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B9 RID: 9145
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004C8C RID: 19596
		// (get) Token: 0x06010929 RID: 67881 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x17004C8D RID: 19597
		// (get) Token: 0x0601092A RID: 67882 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C8E RID: 19598
		// (get) Token: 0x0601092B RID: 67883 RVA: 0x002E4E00 File Offset: 0x002E3000
		internal override int ElementTypeId
		{
			get
			{
				return 12799;
			}
		}

		// Token: 0x0601092C RID: 67884 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C8F RID: 19599
		// (get) Token: 0x0601092D RID: 67885 RVA: 0x002E4E07 File Offset: 0x002E3007
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17004C90 RID: 19600
		// (get) Token: 0x0601092E RID: 67886 RVA: 0x002E4E0E File Offset: 0x002E300E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C91 RID: 19601
		// (get) Token: 0x0601092F RID: 67887 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010930 RID: 67888 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004C92 RID: 19602
		// (get) Token: 0x06010931 RID: 67889 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010932 RID: 67890 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004C93 RID: 19603
		// (get) Token: 0x06010933 RID: 67891 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06010934 RID: 67892 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004C94 RID: 19604
		// (get) Token: 0x06010935 RID: 67893 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010936 RID: 67894 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004C95 RID: 19605
		// (get) Token: 0x06010937 RID: 67895 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06010938 RID: 67896 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x06010939 RID: 67897 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x0601093A RID: 67898 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601093B RID: 67899 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601093C RID: 67900 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601093D RID: 67901 RVA: 0x002E4E18 File Offset: 0x002E3018
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

		// Token: 0x17004C96 RID: 19606
		// (get) Token: 0x0601093E RID: 67902 RVA: 0x002E4E6E File Offset: 0x002E306E
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17004C97 RID: 19607
		// (get) Token: 0x0601093F RID: 67903 RVA: 0x002E4E75 File Offset: 0x002E3075
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004C98 RID: 19608
		// (get) Token: 0x06010940 RID: 67904 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004C99 RID: 19609
		// (get) Token: 0x06010941 RID: 67905 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06010942 RID: 67906 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x17004C9A RID: 19610
		// (get) Token: 0x06010943 RID: 67907 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x06010944 RID: 67908 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x17004C9B RID: 19611
		// (get) Token: 0x06010945 RID: 67909 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x06010946 RID: 67910 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x06010947 RID: 67911 RVA: 0x002E4E7C File Offset: 0x002E307C
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

		// Token: 0x06010948 RID: 67912 RVA: 0x002E4EFF File Offset: 0x002E30FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x06010949 RID: 67913 RVA: 0x002E4F08 File Offset: 0x002E3108
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04007550 RID: 30032
		private const string tagName = "cNvPr";

		// Token: 0x04007551 RID: 30033
		private const byte tagNsId = 49;

		// Token: 0x04007552 RID: 30034
		internal const int ElementTypeIdConst = 12799;

		// Token: 0x04007553 RID: 30035
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04007554 RID: 30036
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007555 RID: 30037
		private static readonly string[] eleTagNames;

		// Token: 0x04007556 RID: 30038
		private static readonly byte[] eleNamespaceIds;
	}
}
