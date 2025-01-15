using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Diagram
{
	// Token: 0x02002341 RID: 9025
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004953 RID: 18771
		// (get) Token: 0x06010230 RID: 66096 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x17004954 RID: 18772
		// (get) Token: 0x06010231 RID: 66097 RVA: 0x002E01B3 File Offset: 0x002DE3B3
		internal override byte NamespaceId
		{
			get
			{
				return 58;
			}
		}

		// Token: 0x17004955 RID: 18773
		// (get) Token: 0x06010232 RID: 66098 RVA: 0x002E01B7 File Offset: 0x002DE3B7
		internal override int ElementTypeId
		{
			get
			{
				return 13116;
			}
		}

		// Token: 0x06010233 RID: 66099 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004956 RID: 18774
		// (get) Token: 0x06010234 RID: 66100 RVA: 0x002E01BE File Offset: 0x002DE3BE
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17004957 RID: 18775
		// (get) Token: 0x06010235 RID: 66101 RVA: 0x002E01C5 File Offset: 0x002DE3C5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004958 RID: 18776
		// (get) Token: 0x06010236 RID: 66102 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010237 RID: 66103 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004959 RID: 18777
		// (get) Token: 0x06010238 RID: 66104 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010239 RID: 66105 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700495A RID: 18778
		// (get) Token: 0x0601023A RID: 66106 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601023B RID: 66107 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x1700495B RID: 18779
		// (get) Token: 0x0601023C RID: 66108 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601023D RID: 66109 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x1700495C RID: 18780
		// (get) Token: 0x0601023E RID: 66110 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601023F RID: 66111 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x06010240 RID: 66112 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x06010241 RID: 66113 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010242 RID: 66114 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010243 RID: 66115 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010244 RID: 66116 RVA: 0x002E01CC File Offset: 0x002DE3CC
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

		// Token: 0x1700495D RID: 18781
		// (get) Token: 0x06010245 RID: 66117 RVA: 0x002E0222 File Offset: 0x002DE422
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x1700495E RID: 18782
		// (get) Token: 0x06010246 RID: 66118 RVA: 0x002E0229 File Offset: 0x002DE429
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700495F RID: 18783
		// (get) Token: 0x06010247 RID: 66119 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004960 RID: 18784
		// (get) Token: 0x06010248 RID: 66120 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06010249 RID: 66121 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x17004961 RID: 18785
		// (get) Token: 0x0601024A RID: 66122 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x0601024B RID: 66123 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x17004962 RID: 18786
		// (get) Token: 0x0601024C RID: 66124 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x0601024D RID: 66125 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x0601024E RID: 66126 RVA: 0x002E0230 File Offset: 0x002DE430
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

		// Token: 0x0601024F RID: 66127 RVA: 0x002E02B3 File Offset: 0x002DE4B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x06010250 RID: 66128 RVA: 0x002E02BC File Offset: 0x002DE4BC
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x0400733C RID: 29500
		private const string tagName = "cNvPr";

		// Token: 0x0400733D RID: 29501
		private const byte tagNsId = 58;

		// Token: 0x0400733E RID: 29502
		internal const int ElementTypeIdConst = 13116;

		// Token: 0x0400733F RID: 29503
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04007340 RID: 29504
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007341 RID: 29505
		private static readonly string[] eleTagNames;

		// Token: 0x04007342 RID: 29506
		private static readonly byte[] eleNamespaceIds;
	}
}
