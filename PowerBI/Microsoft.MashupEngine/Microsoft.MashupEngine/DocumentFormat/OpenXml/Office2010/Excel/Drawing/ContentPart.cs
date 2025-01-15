using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x02002386 RID: 9094
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExcelNonVisualContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExcelNonVisualContentPartShapeProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Transform2D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ContentPart : OpenXmlCompositeElement
	{
		// Token: 0x17004B68 RID: 19304
		// (get) Token: 0x060106AC RID: 67244 RVA: 0x002DF99D File Offset: 0x002DDB9D
		public override string LocalName
		{
			get
			{
				return "contentPart";
			}
		}

		// Token: 0x17004B69 RID: 19305
		// (get) Token: 0x060106AD RID: 67245 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004B6A RID: 19306
		// (get) Token: 0x060106AE RID: 67246 RVA: 0x002E35BD File Offset: 0x002E17BD
		internal override int ElementTypeId
		{
			get
			{
				return 13010;
			}
		}

		// Token: 0x060106AF RID: 67247 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004B6B RID: 19307
		// (get) Token: 0x060106B0 RID: 67248 RVA: 0x002E35C4 File Offset: 0x002E17C4
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContentPart.attributeTagNames;
			}
		}

		// Token: 0x17004B6C RID: 19308
		// (get) Token: 0x060106B1 RID: 67249 RVA: 0x002E35CB File Offset: 0x002E17CB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContentPart.attributeNamespaceIds;
			}
		}

		// Token: 0x17004B6D RID: 19309
		// (get) Token: 0x060106B2 RID: 67250 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060106B3 RID: 67251 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue RelationshipId
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004B6E RID: 19310
		// (get) Token: 0x060106B4 RID: 67252 RVA: 0x002DF9BD File Offset: 0x002DDBBD
		// (set) Token: 0x060106B5 RID: 67253 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060106B6 RID: 67254 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContentPart()
		{
		}

		// Token: 0x060106B7 RID: 67255 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContentPart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060106B8 RID: 67256 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContentPart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060106B9 RID: 67257 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContentPart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060106BA RID: 67258 RVA: 0x002E35D4 File Offset: 0x002E17D4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (54 == namespaceId && "nvContentPr" == name)
			{
				return new ExcelNonVisualContentPartProperties();
			}
			if (54 == namespaceId && "nvContentPartPr" == name)
			{
				return new ExcelNonVisualContentPartShapeProperties();
			}
			if (54 == namespaceId && "nvPr" == name)
			{
				return new ApplicationNonVisualDrawingProperties();
			}
			if (54 == namespaceId && "xfrm" == name)
			{
				return new Transform2D();
			}
			if (54 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17004B6F RID: 19311
		// (get) Token: 0x060106BB RID: 67259 RVA: 0x002E365A File Offset: 0x002E185A
		internal override string[] ElementTagNames
		{
			get
			{
				return ContentPart.eleTagNames;
			}
		}

		// Token: 0x17004B70 RID: 19312
		// (get) Token: 0x060106BC RID: 67260 RVA: 0x002E3661 File Offset: 0x002E1861
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ContentPart.eleNamespaceIds;
			}
		}

		// Token: 0x17004B71 RID: 19313
		// (get) Token: 0x060106BD RID: 67261 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004B72 RID: 19314
		// (get) Token: 0x060106BE RID: 67262 RVA: 0x002E3668 File Offset: 0x002E1868
		// (set) Token: 0x060106BF RID: 67263 RVA: 0x002E3671 File Offset: 0x002E1871
		public ExcelNonVisualContentPartProperties ExcelNonVisualContentPartProperties
		{
			get
			{
				return base.GetElement<ExcelNonVisualContentPartProperties>(0);
			}
			set
			{
				base.SetElement<ExcelNonVisualContentPartProperties>(0, value);
			}
		}

		// Token: 0x17004B73 RID: 19315
		// (get) Token: 0x060106C0 RID: 67264 RVA: 0x002E367B File Offset: 0x002E187B
		// (set) Token: 0x060106C1 RID: 67265 RVA: 0x002E3684 File Offset: 0x002E1884
		public ExcelNonVisualContentPartShapeProperties ExcelNonVisualContentPartShapeProperties
		{
			get
			{
				return base.GetElement<ExcelNonVisualContentPartShapeProperties>(1);
			}
			set
			{
				base.SetElement<ExcelNonVisualContentPartShapeProperties>(1, value);
			}
		}

		// Token: 0x17004B74 RID: 19316
		// (get) Token: 0x060106C2 RID: 67266 RVA: 0x002E368E File Offset: 0x002E188E
		// (set) Token: 0x060106C3 RID: 67267 RVA: 0x002E3697 File Offset: 0x002E1897
		public ApplicationNonVisualDrawingProperties ApplicationNonVisualDrawingProperties
		{
			get
			{
				return base.GetElement<ApplicationNonVisualDrawingProperties>(2);
			}
			set
			{
				base.SetElement<ApplicationNonVisualDrawingProperties>(2, value);
			}
		}

		// Token: 0x17004B75 RID: 19317
		// (get) Token: 0x060106C4 RID: 67268 RVA: 0x002E36A1 File Offset: 0x002E18A1
		// (set) Token: 0x060106C5 RID: 67269 RVA: 0x002E36AA File Offset: 0x002E18AA
		public Transform2D Transform2D
		{
			get
			{
				return base.GetElement<Transform2D>(3);
			}
			set
			{
				base.SetElement<Transform2D>(3, value);
			}
		}

		// Token: 0x17004B76 RID: 19318
		// (get) Token: 0x060106C6 RID: 67270 RVA: 0x002E36B4 File Offset: 0x002E18B4
		// (set) Token: 0x060106C7 RID: 67271 RVA: 0x002E36BD File Offset: 0x002E18BD
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(4);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(4, value);
			}
		}

		// Token: 0x060106C8 RID: 67272 RVA: 0x002DFABF File Offset: 0x002DDCBF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060106C9 RID: 67273 RVA: 0x002E36C7 File Offset: 0x002E18C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContentPart>(deep);
		}

		// Token: 0x060106CA RID: 67274 RVA: 0x002E36D0 File Offset: 0x002E18D0
		// Note: this type is marked as 'beforefieldinit'.
		static ContentPart()
		{
			byte[] array = new byte[2];
			array[0] = 19;
			ContentPart.attributeNamespaceIds = array;
			ContentPart.eleTagNames = new string[] { "nvContentPr", "nvContentPartPr", "nvPr", "xfrm", "extLst" };
			ContentPart.eleNamespaceIds = new byte[] { 54, 54, 54, 54, 54 };
		}

		// Token: 0x04007486 RID: 29830
		private const string tagName = "contentPart";

		// Token: 0x04007487 RID: 29831
		private const byte tagNsId = 54;

		// Token: 0x04007488 RID: 29832
		internal const int ElementTypeIdConst = 13010;

		// Token: 0x04007489 RID: 29833
		private static string[] attributeTagNames = new string[] { "id", "bwMode" };

		// Token: 0x0400748A RID: 29834
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400748B RID: 29835
		private static readonly string[] eleTagNames;

		// Token: 0x0400748C RID: 29836
		private static readonly byte[] eleNamespaceIds;
	}
}
