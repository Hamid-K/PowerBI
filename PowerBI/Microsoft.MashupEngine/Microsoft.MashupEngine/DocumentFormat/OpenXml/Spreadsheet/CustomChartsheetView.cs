using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE9 RID: 11241
	[ChildElementInfo(typeof(PageMargins))]
	[ChildElementInfo(typeof(ChartSheetPageSetup))]
	[ChildElementInfo(typeof(HeaderFooter))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomChartsheetView : OpenXmlCompositeElement
	{
		// Token: 0x17007E21 RID: 32289
		// (get) Token: 0x06017892 RID: 96402 RVA: 0x00338072 File Offset: 0x00336272
		public override string LocalName
		{
			get
			{
				return "customSheetView";
			}
		}

		// Token: 0x17007E22 RID: 32290
		// (get) Token: 0x06017893 RID: 96403 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E23 RID: 32291
		// (get) Token: 0x06017894 RID: 96404 RVA: 0x00338079 File Offset: 0x00336279
		internal override int ElementTypeId
		{
			get
			{
				return 11213;
			}
		}

		// Token: 0x06017895 RID: 96405 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E24 RID: 32292
		// (get) Token: 0x06017896 RID: 96406 RVA: 0x00338080 File Offset: 0x00336280
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomChartsheetView.attributeTagNames;
			}
		}

		// Token: 0x17007E25 RID: 32293
		// (get) Token: 0x06017897 RID: 96407 RVA: 0x00338087 File Offset: 0x00336287
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomChartsheetView.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E26 RID: 32294
		// (get) Token: 0x06017898 RID: 96408 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017899 RID: 96409 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "guid")]
		public StringValue Guid
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

		// Token: 0x17007E27 RID: 32295
		// (get) Token: 0x0601789A RID: 96410 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601789B RID: 96411 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "scale")]
		public UInt32Value Scale
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007E28 RID: 32296
		// (get) Token: 0x0601789C RID: 96412 RVA: 0x0033808E File Offset: 0x0033628E
		// (set) Token: 0x0601789D RID: 96413 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "state")]
		public EnumValue<SheetStateValues> State
		{
			get
			{
				return (EnumValue<SheetStateValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007E29 RID: 32297
		// (get) Token: 0x0601789E RID: 96414 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601789F RID: 96415 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "zoomToFit")]
		public BooleanValue ZoomToFit
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

		// Token: 0x060178A0 RID: 96416 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomChartsheetView()
		{
		}

		// Token: 0x060178A1 RID: 96417 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomChartsheetView(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060178A2 RID: 96418 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomChartsheetView(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060178A3 RID: 96419 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomChartsheetView(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060178A4 RID: 96420 RVA: 0x003380A0 File Offset: 0x003362A0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pageMargins" == name)
			{
				return new PageMargins();
			}
			if (22 == namespaceId && "pageSetup" == name)
			{
				return new ChartSheetPageSetup();
			}
			if (22 == namespaceId && "headerFooter" == name)
			{
				return new HeaderFooter();
			}
			return null;
		}

		// Token: 0x17007E2A RID: 32298
		// (get) Token: 0x060178A5 RID: 96421 RVA: 0x003380F6 File Offset: 0x003362F6
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomChartsheetView.eleTagNames;
			}
		}

		// Token: 0x17007E2B RID: 32299
		// (get) Token: 0x060178A6 RID: 96422 RVA: 0x003380FD File Offset: 0x003362FD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomChartsheetView.eleNamespaceIds;
			}
		}

		// Token: 0x17007E2C RID: 32300
		// (get) Token: 0x060178A7 RID: 96423 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007E2D RID: 32301
		// (get) Token: 0x060178A8 RID: 96424 RVA: 0x00338104 File Offset: 0x00336304
		// (set) Token: 0x060178A9 RID: 96425 RVA: 0x0033810D File Offset: 0x0033630D
		public PageMargins PageMargins
		{
			get
			{
				return base.GetElement<PageMargins>(0);
			}
			set
			{
				base.SetElement<PageMargins>(0, value);
			}
		}

		// Token: 0x17007E2E RID: 32302
		// (get) Token: 0x060178AA RID: 96426 RVA: 0x00338117 File Offset: 0x00336317
		// (set) Token: 0x060178AB RID: 96427 RVA: 0x00338120 File Offset: 0x00336320
		public ChartSheetPageSetup ChartSheetPageSetup
		{
			get
			{
				return base.GetElement<ChartSheetPageSetup>(1);
			}
			set
			{
				base.SetElement<ChartSheetPageSetup>(1, value);
			}
		}

		// Token: 0x17007E2F RID: 32303
		// (get) Token: 0x060178AC RID: 96428 RVA: 0x0033812A File Offset: 0x0033632A
		// (set) Token: 0x060178AD RID: 96429 RVA: 0x00338133 File Offset: 0x00336333
		public HeaderFooter HeaderFooter
		{
			get
			{
				return base.GetElement<HeaderFooter>(2);
			}
			set
			{
				base.SetElement<HeaderFooter>(2, value);
			}
		}

		// Token: 0x060178AE RID: 96430 RVA: 0x00338140 File Offset: 0x00336340
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "scale" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "state" == name)
			{
				return new EnumValue<SheetStateValues>();
			}
			if (namespaceId == 0 && "zoomToFit" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060178AF RID: 96431 RVA: 0x003381AD File Offset: 0x003363AD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomChartsheetView>(deep);
		}

		// Token: 0x060178B0 RID: 96432 RVA: 0x003381B8 File Offset: 0x003363B8
		// Note: this type is marked as 'beforefieldinit'.
		static CustomChartsheetView()
		{
			byte[] array = new byte[4];
			CustomChartsheetView.attributeNamespaceIds = array;
			CustomChartsheetView.eleTagNames = new string[] { "pageMargins", "pageSetup", "headerFooter" };
			CustomChartsheetView.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009CAF RID: 40111
		private const string tagName = "customSheetView";

		// Token: 0x04009CB0 RID: 40112
		private const byte tagNsId = 22;

		// Token: 0x04009CB1 RID: 40113
		internal const int ElementTypeIdConst = 11213;

		// Token: 0x04009CB2 RID: 40114
		private static string[] attributeTagNames = new string[] { "guid", "scale", "state", "zoomToFit" };

		// Token: 0x04009CB3 RID: 40115
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009CB4 RID: 40116
		private static readonly string[] eleTagNames;

		// Token: 0x04009CB5 RID: 40117
		private static readonly byte[] eleNamespaceIds;
	}
}
