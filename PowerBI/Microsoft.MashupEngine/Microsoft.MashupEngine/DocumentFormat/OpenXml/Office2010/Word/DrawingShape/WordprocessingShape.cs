using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x020024F9 RID: 9465
	[ChildElementInfo(typeof(NonVisualConnectorProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingShapeProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShapeProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShapeStyle), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TextBoxInfo2), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LinkedTextBox), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TextBodyProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class WordprocessingShape : OpenXmlCompositeElement
	{
		// Token: 0x1700539A RID: 21402
		// (get) Token: 0x06011915 RID: 71957 RVA: 0x002EFE4C File Offset: 0x002EE04C
		public override string LocalName
		{
			get
			{
				return "wsp";
			}
		}

		// Token: 0x1700539B RID: 21403
		// (get) Token: 0x06011916 RID: 71958 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x1700539C RID: 21404
		// (get) Token: 0x06011917 RID: 71959 RVA: 0x002EFE57 File Offset: 0x002EE057
		internal override int ElementTypeId
		{
			get
			{
				return 13131;
			}
		}

		// Token: 0x06011918 RID: 71960 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700539D RID: 21405
		// (get) Token: 0x06011919 RID: 71961 RVA: 0x002EFE5E File Offset: 0x002EE05E
		internal override string[] AttributeTagNames
		{
			get
			{
				return WordprocessingShape.attributeTagNames;
			}
		}

		// Token: 0x1700539E RID: 21406
		// (get) Token: 0x0601191A RID: 71962 RVA: 0x002EFE65 File Offset: 0x002EE065
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WordprocessingShape.attributeNamespaceIds;
			}
		}

		// Token: 0x1700539F RID: 21407
		// (get) Token: 0x0601191B RID: 71963 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601191C RID: 71964 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "normalEastAsianFlow")]
		public BooleanValue NormalEastAsianFlow
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601191D RID: 71965 RVA: 0x00293ECF File Offset: 0x002920CF
		public WordprocessingShape()
		{
		}

		// Token: 0x0601191E RID: 71966 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WordprocessingShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601191F RID: 71967 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WordprocessingShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011920 RID: 71968 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WordprocessingShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011921 RID: 71969 RVA: 0x002EFE6C File Offset: 0x002EE06C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (61 == namespaceId && "cNvPr" == name)
			{
				return new NonVisualDrawingProperties();
			}
			if (61 == namespaceId && "cNvSpPr" == name)
			{
				return new NonVisualDrawingShapeProperties();
			}
			if (61 == namespaceId && "cNvCnPr" == name)
			{
				return new NonVisualConnectorProperties();
			}
			if (61 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (61 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (61 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			if (61 == namespaceId && "txbx" == name)
			{
				return new TextBoxInfo2();
			}
			if (61 == namespaceId && "linkedTxbx" == name)
			{
				return new LinkedTextBox();
			}
			if (61 == namespaceId && "bodyPr" == name)
			{
				return new TextBodyProperties();
			}
			return null;
		}

		// Token: 0x170053A0 RID: 21408
		// (get) Token: 0x06011922 RID: 71970 RVA: 0x002EFF52 File Offset: 0x002EE152
		internal override string[] ElementTagNames
		{
			get
			{
				return WordprocessingShape.eleTagNames;
			}
		}

		// Token: 0x170053A1 RID: 21409
		// (get) Token: 0x06011923 RID: 71971 RVA: 0x002EFF59 File Offset: 0x002EE159
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WordprocessingShape.eleNamespaceIds;
			}
		}

		// Token: 0x170053A2 RID: 21410
		// (get) Token: 0x06011924 RID: 71972 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170053A3 RID: 21411
		// (get) Token: 0x06011925 RID: 71973 RVA: 0x002EFF60 File Offset: 0x002EE160
		// (set) Token: 0x06011926 RID: 71974 RVA: 0x002EFF69 File Offset: 0x002EE169
		public NonVisualDrawingProperties NonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<NonVisualDrawingProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualDrawingProperties>(0, value);
			}
		}

		// Token: 0x06011927 RID: 71975 RVA: 0x002EFF73 File Offset: 0x002EE173
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "normalEastAsianFlow" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011928 RID: 71976 RVA: 0x002EFF93 File Offset: 0x002EE193
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WordprocessingShape>(deep);
		}

		// Token: 0x06011929 RID: 71977 RVA: 0x002EFF9C File Offset: 0x002EE19C
		// Note: this type is marked as 'beforefieldinit'.
		static WordprocessingShape()
		{
			byte[] array = new byte[1];
			WordprocessingShape.attributeNamespaceIds = array;
			WordprocessingShape.eleTagNames = new string[] { "cNvPr", "cNvSpPr", "cNvCnPr", "spPr", "style", "extLst", "txbx", "linkedTxbx", "bodyPr" };
			WordprocessingShape.eleNamespaceIds = new byte[] { 61, 61, 61, 61, 61, 61, 61, 61, 61 };
		}

		// Token: 0x04007B59 RID: 31577
		private const string tagName = "wsp";

		// Token: 0x04007B5A RID: 31578
		private const byte tagNsId = 61;

		// Token: 0x04007B5B RID: 31579
		internal const int ElementTypeIdConst = 13131;

		// Token: 0x04007B5C RID: 31580
		private static string[] attributeTagNames = new string[] { "normalEastAsianFlow" };

		// Token: 0x04007B5D RID: 31581
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B5E RID: 31582
		private static readonly string[] eleTagNames;

		// Token: 0x04007B5F RID: 31583
		private static readonly byte[] eleNamespaceIds;
	}
}
