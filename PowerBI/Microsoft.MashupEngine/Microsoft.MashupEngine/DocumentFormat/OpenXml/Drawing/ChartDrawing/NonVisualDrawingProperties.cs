using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200262E RID: 9774
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005A37 RID: 23095
		// (get) Token: 0x060127AB RID: 75691 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x17005A38 RID: 23096
		// (get) Token: 0x060127AC RID: 75692 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A39 RID: 23097
		// (get) Token: 0x060127AD RID: 75693 RVA: 0x002FBB16 File Offset: 0x002F9D16
		internal override int ElementTypeId
		{
			get
			{
				return 10593;
			}
		}

		// Token: 0x060127AE RID: 75694 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005A3A RID: 23098
		// (get) Token: 0x060127AF RID: 75695 RVA: 0x002FBB1D File Offset: 0x002F9D1D
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17005A3B RID: 23099
		// (get) Token: 0x060127B0 RID: 75696 RVA: 0x002FBB24 File Offset: 0x002F9D24
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17005A3C RID: 23100
		// (get) Token: 0x060127B1 RID: 75697 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060127B2 RID: 75698 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005A3D RID: 23101
		// (get) Token: 0x060127B3 RID: 75699 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060127B4 RID: 75700 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17005A3E RID: 23102
		// (get) Token: 0x060127B5 RID: 75701 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060127B6 RID: 75702 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17005A3F RID: 23103
		// (get) Token: 0x060127B7 RID: 75703 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060127B8 RID: 75704 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17005A40 RID: 23104
		// (get) Token: 0x060127B9 RID: 75705 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060127BA RID: 75706 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x060127BB RID: 75707 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x060127BC RID: 75708 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060127BD RID: 75709 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060127BE RID: 75710 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060127BF RID: 75711 RVA: 0x002FBB2C File Offset: 0x002F9D2C
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

		// Token: 0x17005A41 RID: 23105
		// (get) Token: 0x060127C0 RID: 75712 RVA: 0x002FBB82 File Offset: 0x002F9D82
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17005A42 RID: 23106
		// (get) Token: 0x060127C1 RID: 75713 RVA: 0x002FBB89 File Offset: 0x002F9D89
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005A43 RID: 23107
		// (get) Token: 0x060127C2 RID: 75714 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A44 RID: 23108
		// (get) Token: 0x060127C3 RID: 75715 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x060127C4 RID: 75716 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x17005A45 RID: 23109
		// (get) Token: 0x060127C5 RID: 75717 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x060127C6 RID: 75718 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x17005A46 RID: 23110
		// (get) Token: 0x060127C7 RID: 75719 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x060127C8 RID: 75720 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x060127C9 RID: 75721 RVA: 0x002FBB90 File Offset: 0x002F9D90
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

		// Token: 0x060127CA RID: 75722 RVA: 0x002FBC13 File Offset: 0x002F9E13
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x060127CB RID: 75723 RVA: 0x002FBC1C File Offset: 0x002F9E1C
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04008050 RID: 32848
		private const string tagName = "cNvPr";

		// Token: 0x04008051 RID: 32849
		private const byte tagNsId = 12;

		// Token: 0x04008052 RID: 32850
		internal const int ElementTypeIdConst = 10593;

		// Token: 0x04008053 RID: 32851
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04008054 RID: 32852
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008055 RID: 32853
		private static readonly string[] eleTagNames;

		// Token: 0x04008056 RID: 32854
		private static readonly byte[] eleNamespaceIds;
	}
}
