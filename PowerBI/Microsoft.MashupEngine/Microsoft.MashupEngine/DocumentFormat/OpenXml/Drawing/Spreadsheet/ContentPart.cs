using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel.Drawing;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200287E RID: 10366
	[ChildElementInfo(typeof(Transform2D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExcelNonVisualContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExcelNonVisualContentPartShapeProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ContentPart : OpenXmlCompositeElement
	{
		// Token: 0x1700671F RID: 26399
		// (get) Token: 0x06014500 RID: 83200 RVA: 0x002DF99D File Offset: 0x002DDB9D
		public override string LocalName
		{
			get
			{
				return "contentPart";
			}
		}

		// Token: 0x17006720 RID: 26400
		// (get) Token: 0x06014501 RID: 83201 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006721 RID: 26401
		// (get) Token: 0x06014502 RID: 83202 RVA: 0x00311EBE File Offset: 0x003100BE
		internal override int ElementTypeId
		{
			get
			{
				return 10728;
			}
		}

		// Token: 0x06014503 RID: 83203 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17006722 RID: 26402
		// (get) Token: 0x06014504 RID: 83204 RVA: 0x00311EC5 File Offset: 0x003100C5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContentPart.attributeTagNames;
			}
		}

		// Token: 0x17006723 RID: 26403
		// (get) Token: 0x06014505 RID: 83205 RVA: 0x00311ECC File Offset: 0x003100CC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContentPart.attributeNamespaceIds;
			}
		}

		// Token: 0x17006724 RID: 26404
		// (get) Token: 0x06014506 RID: 83206 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06014507 RID: 83207 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006725 RID: 26405
		// (get) Token: 0x06014508 RID: 83208 RVA: 0x002DF9BD File Offset: 0x002DDBBD
		// (set) Token: 0x06014509 RID: 83209 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601450A RID: 83210 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContentPart()
		{
		}

		// Token: 0x0601450B RID: 83211 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContentPart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601450C RID: 83212 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContentPart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601450D RID: 83213 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContentPart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601450E RID: 83214 RVA: 0x00311ED4 File Offset: 0x003100D4
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

		// Token: 0x17006726 RID: 26406
		// (get) Token: 0x0601450F RID: 83215 RVA: 0x00311F5A File Offset: 0x0031015A
		internal override string[] ElementTagNames
		{
			get
			{
				return ContentPart.eleTagNames;
			}
		}

		// Token: 0x17006727 RID: 26407
		// (get) Token: 0x06014510 RID: 83216 RVA: 0x00311F61 File Offset: 0x00310161
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ContentPart.eleNamespaceIds;
			}
		}

		// Token: 0x17006728 RID: 26408
		// (get) Token: 0x06014511 RID: 83217 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006729 RID: 26409
		// (get) Token: 0x06014512 RID: 83218 RVA: 0x002E3668 File Offset: 0x002E1868
		// (set) Token: 0x06014513 RID: 83219 RVA: 0x002E3671 File Offset: 0x002E1871
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

		// Token: 0x1700672A RID: 26410
		// (get) Token: 0x06014514 RID: 83220 RVA: 0x002E367B File Offset: 0x002E187B
		// (set) Token: 0x06014515 RID: 83221 RVA: 0x002E3684 File Offset: 0x002E1884
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

		// Token: 0x1700672B RID: 26411
		// (get) Token: 0x06014516 RID: 83222 RVA: 0x002E368E File Offset: 0x002E188E
		// (set) Token: 0x06014517 RID: 83223 RVA: 0x002E3697 File Offset: 0x002E1897
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

		// Token: 0x1700672C RID: 26412
		// (get) Token: 0x06014518 RID: 83224 RVA: 0x002E36A1 File Offset: 0x002E18A1
		// (set) Token: 0x06014519 RID: 83225 RVA: 0x002E36AA File Offset: 0x002E18AA
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

		// Token: 0x1700672D RID: 26413
		// (get) Token: 0x0601451A RID: 83226 RVA: 0x002E36B4 File Offset: 0x002E18B4
		// (set) Token: 0x0601451B RID: 83227 RVA: 0x002E36BD File Offset: 0x002E18BD
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

		// Token: 0x0601451C RID: 83228 RVA: 0x002DFABF File Offset: 0x002DDCBF
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

		// Token: 0x0601451D RID: 83229 RVA: 0x00311F68 File Offset: 0x00310168
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContentPart>(deep);
		}

		// Token: 0x0601451E RID: 83230 RVA: 0x00311F74 File Offset: 0x00310174
		// Note: this type is marked as 'beforefieldinit'.
		static ContentPart()
		{
			byte[] array = new byte[2];
			array[0] = 19;
			ContentPart.attributeNamespaceIds = array;
			ContentPart.eleTagNames = new string[] { "nvContentPr", "nvContentPartPr", "nvPr", "xfrm", "extLst" };
			ContentPart.eleNamespaceIds = new byte[] { 54, 54, 54, 54, 54 };
		}

		// Token: 0x04008D89 RID: 36233
		private const string tagName = "contentPart";

		// Token: 0x04008D8A RID: 36234
		private const byte tagNsId = 18;

		// Token: 0x04008D8B RID: 36235
		internal const int ElementTypeIdConst = 10728;

		// Token: 0x04008D8C RID: 36236
		private static string[] attributeTagNames = new string[] { "id", "bwMode" };

		// Token: 0x04008D8D RID: 36237
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D8E RID: 36238
		private static readonly string[] eleTagNames;

		// Token: 0x04008D8F RID: 36239
		private static readonly byte[] eleNamespaceIds;
	}
}
