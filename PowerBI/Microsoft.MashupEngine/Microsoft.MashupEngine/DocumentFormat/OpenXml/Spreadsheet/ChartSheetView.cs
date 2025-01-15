using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BE8 RID: 11240
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ChartSheetView : OpenXmlCompositeElement
	{
		// Token: 0x17007E14 RID: 32276
		// (get) Token: 0x06017877 RID: 96375 RVA: 0x00337F60 File Offset: 0x00336160
		public override string LocalName
		{
			get
			{
				return "sheetView";
			}
		}

		// Token: 0x17007E15 RID: 32277
		// (get) Token: 0x06017878 RID: 96376 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E16 RID: 32278
		// (get) Token: 0x06017879 RID: 96377 RVA: 0x00337F67 File Offset: 0x00336167
		internal override int ElementTypeId
		{
			get
			{
				return 11212;
			}
		}

		// Token: 0x0601787A RID: 96378 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E17 RID: 32279
		// (get) Token: 0x0601787B RID: 96379 RVA: 0x00337F6E File Offset: 0x0033616E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ChartSheetView.attributeTagNames;
			}
		}

		// Token: 0x17007E18 RID: 32280
		// (get) Token: 0x0601787C RID: 96380 RVA: 0x00337F75 File Offset: 0x00336175
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ChartSheetView.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E19 RID: 32281
		// (get) Token: 0x0601787D RID: 96381 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601787E RID: 96382 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "tabSelected")]
		public BooleanValue TabSelected
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

		// Token: 0x17007E1A RID: 32282
		// (get) Token: 0x0601787F RID: 96383 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017880 RID: 96384 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "zoomScale")]
		public UInt32Value ZoomScale
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

		// Token: 0x17007E1B RID: 32283
		// (get) Token: 0x06017881 RID: 96385 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017882 RID: 96386 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "workbookViewId")]
		public UInt32Value WorkbookViewId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007E1C RID: 32284
		// (get) Token: 0x06017883 RID: 96387 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017884 RID: 96388 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x06017885 RID: 96389 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChartSheetView()
		{
		}

		// Token: 0x06017886 RID: 96390 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChartSheetView(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017887 RID: 96391 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChartSheetView(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017888 RID: 96392 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChartSheetView(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017889 RID: 96393 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007E1D RID: 32285
		// (get) Token: 0x0601788A RID: 96394 RVA: 0x00337F7C File Offset: 0x0033617C
		internal override string[] ElementTagNames
		{
			get
			{
				return ChartSheetView.eleTagNames;
			}
		}

		// Token: 0x17007E1E RID: 32286
		// (get) Token: 0x0601788B RID: 96395 RVA: 0x00337F83 File Offset: 0x00336183
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ChartSheetView.eleNamespaceIds;
			}
		}

		// Token: 0x17007E1F RID: 32287
		// (get) Token: 0x0601788C RID: 96396 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007E20 RID: 32288
		// (get) Token: 0x0601788D RID: 96397 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x0601788E RID: 96398 RVA: 0x00332911 File Offset: 0x00330B11
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x0601788F RID: 96399 RVA: 0x00337F8C File Offset: 0x0033618C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "tabSelected" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "zoomScale" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "workbookViewId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "zoomToFit" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017890 RID: 96400 RVA: 0x00337FF9 File Offset: 0x003361F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChartSheetView>(deep);
		}

		// Token: 0x06017891 RID: 96401 RVA: 0x00338004 File Offset: 0x00336204
		// Note: this type is marked as 'beforefieldinit'.
		static ChartSheetView()
		{
			byte[] array = new byte[4];
			ChartSheetView.attributeNamespaceIds = array;
			ChartSheetView.eleTagNames = new string[] { "extLst" };
			ChartSheetView.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009CA8 RID: 40104
		private const string tagName = "sheetView";

		// Token: 0x04009CA9 RID: 40105
		private const byte tagNsId = 22;

		// Token: 0x04009CAA RID: 40106
		internal const int ElementTypeIdConst = 11212;

		// Token: 0x04009CAB RID: 40107
		private static string[] attributeTagNames = new string[] { "tabSelected", "zoomScale", "workbookViewId", "zoomToFit" };

		// Token: 0x04009CAC RID: 40108
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009CAD RID: 40109
		private static readonly string[] eleTagNames;

		// Token: 0x04009CAE RID: 40110
		private static readonly byte[] eleNamespaceIds;
	}
}
