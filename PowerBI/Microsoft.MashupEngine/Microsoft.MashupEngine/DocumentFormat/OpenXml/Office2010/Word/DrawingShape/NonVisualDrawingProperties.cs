using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x020024FB RID: 9467
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170053A7 RID: 21415
		// (get) Token: 0x06011934 RID: 71988 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x170053A8 RID: 21416
		// (get) Token: 0x06011935 RID: 71989 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053A9 RID: 21417
		// (get) Token: 0x06011936 RID: 71990 RVA: 0x002F0048 File Offset: 0x002EE248
		internal override int ElementTypeId
		{
			get
			{
				return 13133;
			}
		}

		// Token: 0x06011937 RID: 71991 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170053AA RID: 21418
		// (get) Token: 0x06011938 RID: 71992 RVA: 0x002F004F File Offset: 0x002EE24F
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x170053AB RID: 21419
		// (get) Token: 0x06011939 RID: 71993 RVA: 0x002F0056 File Offset: 0x002EE256
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170053AC RID: 21420
		// (get) Token: 0x0601193A RID: 71994 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601193B RID: 71995 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170053AD RID: 21421
		// (get) Token: 0x0601193C RID: 71996 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601193D RID: 71997 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170053AE RID: 21422
		// (get) Token: 0x0601193E RID: 71998 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601193F RID: 71999 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170053AF RID: 21423
		// (get) Token: 0x06011940 RID: 72000 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06011941 RID: 72001 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170053B0 RID: 21424
		// (get) Token: 0x06011942 RID: 72002 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06011943 RID: 72003 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x06011944 RID: 72004 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x06011945 RID: 72005 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011946 RID: 72006 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011947 RID: 72007 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011948 RID: 72008 RVA: 0x002F0060 File Offset: 0x002EE260
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

		// Token: 0x170053B1 RID: 21425
		// (get) Token: 0x06011949 RID: 72009 RVA: 0x002F00B6 File Offset: 0x002EE2B6
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170053B2 RID: 21426
		// (get) Token: 0x0601194A RID: 72010 RVA: 0x002F00BD File Offset: 0x002EE2BD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170053B3 RID: 21427
		// (get) Token: 0x0601194B RID: 72011 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170053B4 RID: 21428
		// (get) Token: 0x0601194C RID: 72012 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x0601194D RID: 72013 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x170053B5 RID: 21429
		// (get) Token: 0x0601194E RID: 72014 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x0601194F RID: 72015 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x170053B6 RID: 21430
		// (get) Token: 0x06011950 RID: 72016 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x06011951 RID: 72017 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x06011952 RID: 72018 RVA: 0x002F00C4 File Offset: 0x002EE2C4
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

		// Token: 0x06011953 RID: 72019 RVA: 0x002F0147 File Offset: 0x002EE347
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x06011954 RID: 72020 RVA: 0x002F0150 File Offset: 0x002EE350
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04007B63 RID: 31587
		private const string tagName = "cNvPr";

		// Token: 0x04007B64 RID: 31588
		private const byte tagNsId = 61;

		// Token: 0x04007B65 RID: 31589
		internal const int ElementTypeIdConst = 13133;

		// Token: 0x04007B66 RID: 31590
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04007B67 RID: 31591
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B68 RID: 31592
		private static readonly string[] eleTagNames;

		// Token: 0x04007B69 RID: 31593
		private static readonly byte[] eleNamespaceIds;
	}
}
