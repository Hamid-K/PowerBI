using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028AC RID: 10412
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	internal class DocProperties : OpenXmlCompositeElement
	{
		// Token: 0x170068A4 RID: 26788
		// (get) Token: 0x06014848 RID: 84040 RVA: 0x00314463 File Offset: 0x00312663
		public override string LocalName
		{
			get
			{
				return "docPr";
			}
		}

		// Token: 0x170068A5 RID: 26789
		// (get) Token: 0x06014849 RID: 84041 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x170068A6 RID: 26790
		// (get) Token: 0x0601484A RID: 84042 RVA: 0x0031446A File Offset: 0x0031266A
		internal override int ElementTypeId
		{
			get
			{
				return 10709;
			}
		}

		// Token: 0x0601484B RID: 84043 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170068A7 RID: 26791
		// (get) Token: 0x0601484C RID: 84044 RVA: 0x00314471 File Offset: 0x00312671
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocProperties.attributeTagNames;
			}
		}

		// Token: 0x170068A8 RID: 26792
		// (get) Token: 0x0601484D RID: 84045 RVA: 0x00314478 File Offset: 0x00312678
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170068A9 RID: 26793
		// (get) Token: 0x0601484E RID: 84046 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601484F RID: 84047 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170068AA RID: 26794
		// (get) Token: 0x06014850 RID: 84048 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06014851 RID: 84049 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170068AB RID: 26795
		// (get) Token: 0x06014852 RID: 84050 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06014853 RID: 84051 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170068AC RID: 26796
		// (get) Token: 0x06014854 RID: 84052 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06014855 RID: 84053 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170068AD RID: 26797
		// (get) Token: 0x06014856 RID: 84054 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06014857 RID: 84055 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x06014858 RID: 84056 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocProperties()
		{
		}

		// Token: 0x06014859 RID: 84057 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601485A RID: 84058 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601485B RID: 84059 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601485C RID: 84060 RVA: 0x00314480 File Offset: 0x00312680
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

		// Token: 0x170068AE RID: 26798
		// (get) Token: 0x0601485D RID: 84061 RVA: 0x003144D6 File Offset: 0x003126D6
		internal override string[] ElementTagNames
		{
			get
			{
				return DocProperties.eleTagNames;
			}
		}

		// Token: 0x170068AF RID: 26799
		// (get) Token: 0x0601485E RID: 84062 RVA: 0x003144DD File Offset: 0x003126DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DocProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170068B0 RID: 26800
		// (get) Token: 0x0601485F RID: 84063 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170068B1 RID: 26801
		// (get) Token: 0x06014860 RID: 84064 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06014861 RID: 84065 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x170068B2 RID: 26802
		// (get) Token: 0x06014862 RID: 84066 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x06014863 RID: 84067 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x170068B3 RID: 26803
		// (get) Token: 0x06014864 RID: 84068 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x06014865 RID: 84069 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x06014866 RID: 84070 RVA: 0x003144E4 File Offset: 0x003126E4
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

		// Token: 0x06014867 RID: 84071 RVA: 0x00314567 File Offset: 0x00312767
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocProperties>(deep);
		}

		// Token: 0x06014868 RID: 84072 RVA: 0x00314570 File Offset: 0x00312770
		// Note: this type is marked as 'beforefieldinit'.
		static DocProperties()
		{
			byte[] array = new byte[5];
			DocProperties.attributeNamespaceIds = array;
			DocProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			DocProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04008E74 RID: 36468
		private const string tagName = "docPr";

		// Token: 0x04008E75 RID: 36469
		private const byte tagNsId = 16;

		// Token: 0x04008E76 RID: 36470
		internal const int ElementTypeIdConst = 10709;

		// Token: 0x04008E77 RID: 36471
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04008E78 RID: 36472
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E79 RID: 36473
		private static readonly string[] eleTagNames;

		// Token: 0x04008E7A RID: 36474
		private static readonly byte[] eleNamespaceIds;
	}
}
