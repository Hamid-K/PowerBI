using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x02002336 RID: 9014
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700490C RID: 18700
		// (get) Token: 0x0601018B RID: 65931 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x1700490D RID: 18701
		// (get) Token: 0x0601018C RID: 65932 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x1700490E RID: 18702
		// (get) Token: 0x0601018D RID: 65933 RVA: 0x002DFB87 File Offset: 0x002DDD87
		internal override int ElementTypeId
		{
			get
			{
				return 12704;
			}
		}

		// Token: 0x0601018E RID: 65934 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700490F RID: 18703
		// (get) Token: 0x0601018F RID: 65935 RVA: 0x002DFB8E File Offset: 0x002DDD8E
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17004910 RID: 18704
		// (get) Token: 0x06010190 RID: 65936 RVA: 0x002DFB95 File Offset: 0x002DDD95
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004911 RID: 18705
		// (get) Token: 0x06010191 RID: 65937 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010192 RID: 65938 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004912 RID: 18706
		// (get) Token: 0x06010193 RID: 65939 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010194 RID: 65940 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004913 RID: 18707
		// (get) Token: 0x06010195 RID: 65941 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06010196 RID: 65942 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004914 RID: 18708
		// (get) Token: 0x06010197 RID: 65943 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010198 RID: 65944 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004915 RID: 18709
		// (get) Token: 0x06010199 RID: 65945 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601019A RID: 65946 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x0601019B RID: 65947 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x0601019C RID: 65948 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601019D RID: 65949 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601019E RID: 65950 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601019F RID: 65951 RVA: 0x002DFB9C File Offset: 0x002DDD9C
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

		// Token: 0x17004916 RID: 18710
		// (get) Token: 0x060101A0 RID: 65952 RVA: 0x002DFBF2 File Offset: 0x002DDDF2
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17004917 RID: 18711
		// (get) Token: 0x060101A1 RID: 65953 RVA: 0x002DFBF9 File Offset: 0x002DDDF9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004918 RID: 18712
		// (get) Token: 0x060101A2 RID: 65954 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004919 RID: 18713
		// (get) Token: 0x060101A3 RID: 65955 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x060101A4 RID: 65956 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x1700491A RID: 18714
		// (get) Token: 0x060101A5 RID: 65957 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x060101A6 RID: 65958 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x1700491B RID: 18715
		// (get) Token: 0x060101A7 RID: 65959 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x060101A8 RID: 65960 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x060101A9 RID: 65961 RVA: 0x002DFC00 File Offset: 0x002DDE00
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

		// Token: 0x060101AA RID: 65962 RVA: 0x002DFC83 File Offset: 0x002DDE83
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x060101AB RID: 65963 RVA: 0x002DFC8C File Offset: 0x002DDE8C
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x0400730F RID: 29455
		private const string tagName = "cNvPr";

		// Token: 0x04007310 RID: 29456
		private const byte tagNsId = 47;

		// Token: 0x04007311 RID: 29457
		internal const int ElementTypeIdConst = 12704;

		// Token: 0x04007312 RID: 29458
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04007313 RID: 29459
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007314 RID: 29460
		private static readonly string[] eleTagNames;

		// Token: 0x04007315 RID: 29461
		private static readonly byte[] eleNamespaceIds;
	}
}
