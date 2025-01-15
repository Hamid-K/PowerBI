using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002893 RID: 10387
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170067B4 RID: 26548
		// (get) Token: 0x06014652 RID: 83538 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x170067B5 RID: 26549
		// (get) Token: 0x06014653 RID: 83539 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067B6 RID: 26550
		// (get) Token: 0x06014654 RID: 83540 RVA: 0x00312CC7 File Offset: 0x00310EC7
		internal override int ElementTypeId
		{
			get
			{
				return 10748;
			}
		}

		// Token: 0x06014655 RID: 83541 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170067B7 RID: 26551
		// (get) Token: 0x06014656 RID: 83542 RVA: 0x00312CCE File Offset: 0x00310ECE
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x170067B8 RID: 26552
		// (get) Token: 0x06014657 RID: 83543 RVA: 0x00312CD5 File Offset: 0x00310ED5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170067B9 RID: 26553
		// (get) Token: 0x06014658 RID: 83544 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06014659 RID: 83545 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170067BA RID: 26554
		// (get) Token: 0x0601465A RID: 83546 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601465B RID: 83547 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170067BB RID: 26555
		// (get) Token: 0x0601465C RID: 83548 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601465D RID: 83549 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170067BC RID: 26556
		// (get) Token: 0x0601465E RID: 83550 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601465F RID: 83551 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x06014660 RID: 83552 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x06014661 RID: 83553 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014662 RID: 83554 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014663 RID: 83555 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014664 RID: 83556 RVA: 0x00312CDC File Offset: 0x00310EDC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "hlinkClick" == name)
			{
				return new HyperlinkOnClick();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170067BD RID: 26557
		// (get) Token: 0x06014665 RID: 83557 RVA: 0x00312D0F File Offset: 0x00310F0F
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170067BE RID: 26558
		// (get) Token: 0x06014666 RID: 83558 RVA: 0x00312D16 File Offset: 0x00310F16
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170067BF RID: 26559
		// (get) Token: 0x06014667 RID: 83559 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170067C0 RID: 26560
		// (get) Token: 0x06014668 RID: 83560 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06014669 RID: 83561 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x170067C1 RID: 26561
		// (get) Token: 0x0601466A RID: 83562 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x0601466B RID: 83563 RVA: 0x002DEB73 File Offset: 0x002DCD73
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x0601466C RID: 83564 RVA: 0x00312D20 File Offset: 0x00310F20
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601466D RID: 83565 RVA: 0x00312D8D File Offset: 0x00310F8D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x0601466E RID: 83566 RVA: 0x00312D98 File Offset: 0x00310F98
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[4];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04008DE9 RID: 36329
		private const string tagName = "cNvPr";

		// Token: 0x04008DEA RID: 36330
		private const byte tagNsId = 18;

		// Token: 0x04008DEB RID: 36331
		internal const int ElementTypeIdConst = 10748;

		// Token: 0x04008DEC RID: 36332
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden" };

		// Token: 0x04008DED RID: 36333
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008DEE RID: 36334
		private static readonly string[] eleTagNames;

		// Token: 0x04008DEF RID: 36335
		private static readonly byte[] eleNamespaceIds;
	}
}
