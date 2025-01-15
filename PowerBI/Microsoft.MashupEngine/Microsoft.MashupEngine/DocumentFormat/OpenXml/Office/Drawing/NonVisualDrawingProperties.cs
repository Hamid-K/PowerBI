using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002326 RID: 8998
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700487A RID: 18554
		// (get) Token: 0x06010049 RID: 65609 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x1700487B RID: 18555
		// (get) Token: 0x0601004A RID: 65610 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x1700487C RID: 18556
		// (get) Token: 0x0601004B RID: 65611 RVA: 0x002DE91E File Offset: 0x002DCB1E
		internal override int ElementTypeId
		{
			get
			{
				return 13021;
			}
		}

		// Token: 0x0601004C RID: 65612 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700487D RID: 18557
		// (get) Token: 0x0601004D RID: 65613 RVA: 0x002DE925 File Offset: 0x002DCB25
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x1700487E RID: 18558
		// (get) Token: 0x0601004E RID: 65614 RVA: 0x002DE92C File Offset: 0x002DCB2C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700487F RID: 18559
		// (get) Token: 0x0601004F RID: 65615 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010050 RID: 65616 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004880 RID: 18560
		// (get) Token: 0x06010051 RID: 65617 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010052 RID: 65618 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004881 RID: 18561
		// (get) Token: 0x06010053 RID: 65619 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06010054 RID: 65620 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004882 RID: 18562
		// (get) Token: 0x06010055 RID: 65621 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010056 RID: 65622 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004883 RID: 18563
		// (get) Token: 0x06010057 RID: 65623 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06010058 RID: 65624 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "title")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
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

		// Token: 0x06010059 RID: 65625 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x0601005A RID: 65626 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601005B RID: 65627 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601005C RID: 65628 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601005D RID: 65629 RVA: 0x002DE944 File Offset: 0x002DCB44
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

		// Token: 0x17004884 RID: 18564
		// (get) Token: 0x0601005E RID: 65630 RVA: 0x002DE99A File Offset: 0x002DCB9A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17004885 RID: 18565
		// (get) Token: 0x0601005F RID: 65631 RVA: 0x002DE9A1 File Offset: 0x002DCBA1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004886 RID: 18566
		// (get) Token: 0x06010060 RID: 65632 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004887 RID: 18567
		// (get) Token: 0x06010061 RID: 65633 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06010062 RID: 65634 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x17004888 RID: 18568
		// (get) Token: 0x06010063 RID: 65635 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x06010064 RID: 65636 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x17004889 RID: 18569
		// (get) Token: 0x06010065 RID: 65637 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x06010066 RID: 65638 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x06010067 RID: 65639 RVA: 0x002DE9E4 File Offset: 0x002DCBE4
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

		// Token: 0x06010068 RID: 65640 RVA: 0x002DEA67 File Offset: 0x002DCC67
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x06010069 RID: 65641 RVA: 0x002DEA70 File Offset: 0x002DCC70
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x040072BA RID: 29370
		private const string tagName = "cNvPr";

		// Token: 0x040072BB RID: 29371
		private const byte tagNsId = 56;

		// Token: 0x040072BC RID: 29372
		internal const int ElementTypeIdConst = 13021;

		// Token: 0x040072BD RID: 29373
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x040072BE RID: 29374
		private static byte[] attributeNamespaceIds;

		// Token: 0x040072BF RID: 29375
		private static readonly string[] eleTagNames;

		// Token: 0x040072C0 RID: 29376
		private static readonly byte[] eleNamespaceIds;
	}
}
