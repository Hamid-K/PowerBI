using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A5D RID: 10845
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700722E RID: 29230
		// (get) Token: 0x06015E1B RID: 89627 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x1700722F RID: 29231
		// (get) Token: 0x06015E1C RID: 89628 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007230 RID: 29232
		// (get) Token: 0x06015E1D RID: 89629 RVA: 0x00324053 File Offset: 0x00322253
		internal override int ElementTypeId
		{
			get
			{
				return 12263;
			}
		}

		// Token: 0x06015E1E RID: 89630 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007231 RID: 29233
		// (get) Token: 0x06015E1F RID: 89631 RVA: 0x0032405A File Offset: 0x0032225A
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17007232 RID: 29234
		// (get) Token: 0x06015E20 RID: 89632 RVA: 0x00324061 File Offset: 0x00322261
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007233 RID: 29235
		// (get) Token: 0x06015E21 RID: 89633 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015E22 RID: 89634 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007234 RID: 29236
		// (get) Token: 0x06015E23 RID: 89635 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015E24 RID: 89636 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007235 RID: 29237
		// (get) Token: 0x06015E25 RID: 89637 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06015E26 RID: 89638 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17007236 RID: 29238
		// (get) Token: 0x06015E27 RID: 89639 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06015E28 RID: 89640 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17007237 RID: 29239
		// (get) Token: 0x06015E29 RID: 89641 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06015E2A RID: 89642 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x06015E2B RID: 89643 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x06015E2C RID: 89644 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E2D RID: 89645 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015E2E RID: 89646 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015E2F RID: 89647 RVA: 0x00324068 File Offset: 0x00322268
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

		// Token: 0x17007238 RID: 29240
		// (get) Token: 0x06015E30 RID: 89648 RVA: 0x003240BE File Offset: 0x003222BE
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17007239 RID: 29241
		// (get) Token: 0x06015E31 RID: 89649 RVA: 0x003240C5 File Offset: 0x003222C5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700723A RID: 29242
		// (get) Token: 0x06015E32 RID: 89650 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700723B RID: 29243
		// (get) Token: 0x06015E33 RID: 89651 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06015E34 RID: 89652 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x1700723C RID: 29244
		// (get) Token: 0x06015E35 RID: 89653 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x06015E36 RID: 89654 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x1700723D RID: 29245
		// (get) Token: 0x06015E37 RID: 89655 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x06015E38 RID: 89656 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x06015E39 RID: 89657 RVA: 0x003240CC File Offset: 0x003222CC
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

		// Token: 0x06015E3A RID: 89658 RVA: 0x0032414F File Offset: 0x0032234F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x06015E3B RID: 89659 RVA: 0x00324158 File Offset: 0x00322358
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04009541 RID: 38209
		private const string tagName = "cNvPr";

		// Token: 0x04009542 RID: 38210
		private const byte tagNsId = 24;

		// Token: 0x04009543 RID: 38211
		internal const int ElementTypeIdConst = 12263;

		// Token: 0x04009544 RID: 38212
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04009545 RID: 38213
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009546 RID: 38214
		private static readonly string[] eleTagNames;

		// Token: 0x04009547 RID: 38215
		private static readonly byte[] eleNamespaceIds;
	}
}
