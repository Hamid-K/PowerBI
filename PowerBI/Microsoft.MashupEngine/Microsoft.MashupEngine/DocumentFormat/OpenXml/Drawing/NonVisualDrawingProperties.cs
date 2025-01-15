using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A4 RID: 10148
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700628F RID: 25231
		// (get) Token: 0x06013A6E RID: 80494 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x17006290 RID: 25232
		// (get) Token: 0x06013A6F RID: 80495 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006291 RID: 25233
		// (get) Token: 0x06013A70 RID: 80496 RVA: 0x0030A4C7 File Offset: 0x003086C7
		internal override int ElementTypeId
		{
			get
			{
				return 10181;
			}
		}

		// Token: 0x06013A71 RID: 80497 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006292 RID: 25234
		// (get) Token: 0x06013A72 RID: 80498 RVA: 0x0030A4CE File Offset: 0x003086CE
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17006293 RID: 25235
		// (get) Token: 0x06013A73 RID: 80499 RVA: 0x0030A4D5 File Offset: 0x003086D5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17006294 RID: 25236
		// (get) Token: 0x06013A74 RID: 80500 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06013A75 RID: 80501 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006295 RID: 25237
		// (get) Token: 0x06013A76 RID: 80502 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06013A77 RID: 80503 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17006296 RID: 25238
		// (get) Token: 0x06013A78 RID: 80504 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06013A79 RID: 80505 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17006297 RID: 25239
		// (get) Token: 0x06013A7A RID: 80506 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06013A7B RID: 80507 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17006298 RID: 25240
		// (get) Token: 0x06013A7C RID: 80508 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06013A7D RID: 80509 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x06013A7E RID: 80510 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x06013A7F RID: 80511 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013A80 RID: 80512 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013A81 RID: 80513 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013A82 RID: 80514 RVA: 0x0030A4DC File Offset: 0x003086DC
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

		// Token: 0x17006299 RID: 25241
		// (get) Token: 0x06013A83 RID: 80515 RVA: 0x0030A532 File Offset: 0x00308732
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x1700629A RID: 25242
		// (get) Token: 0x06013A84 RID: 80516 RVA: 0x0030A539 File Offset: 0x00308739
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700629B RID: 25243
		// (get) Token: 0x06013A85 RID: 80517 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700629C RID: 25244
		// (get) Token: 0x06013A86 RID: 80518 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06013A87 RID: 80519 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x1700629D RID: 25245
		// (get) Token: 0x06013A88 RID: 80520 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x06013A89 RID: 80521 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x1700629E RID: 25246
		// (get) Token: 0x06013A8A RID: 80522 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x06013A8B RID: 80523 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x06013A8C RID: 80524 RVA: 0x0030A540 File Offset: 0x00308740
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

		// Token: 0x06013A8D RID: 80525 RVA: 0x0030A5C3 File Offset: 0x003087C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x06013A8E RID: 80526 RVA: 0x0030A5CC File Offset: 0x003087CC
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04008726 RID: 34598
		private const string tagName = "cNvPr";

		// Token: 0x04008727 RID: 34599
		private const byte tagNsId = 10;

		// Token: 0x04008728 RID: 34600
		internal const int ElementTypeIdConst = 10181;

		// Token: 0x04008729 RID: 34601
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x0400872A RID: 34602
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400872B RID: 34603
		private static readonly string[] eleTagNames;

		// Token: 0x0400872C RID: 34604
		private static readonly byte[] eleNamespaceIds;
	}
}
