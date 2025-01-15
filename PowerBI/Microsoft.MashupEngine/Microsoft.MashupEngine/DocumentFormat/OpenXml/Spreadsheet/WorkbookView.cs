using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C3A RID: 11322
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class WorkbookView : OpenXmlCompositeElement
	{
		// Token: 0x17008153 RID: 33107
		// (get) Token: 0x06017F81 RID: 98177 RVA: 0x0033D2D3 File Offset: 0x0033B4D3
		public override string LocalName
		{
			get
			{
				return "workbookView";
			}
		}

		// Token: 0x17008154 RID: 33108
		// (get) Token: 0x06017F82 RID: 98178 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008155 RID: 33109
		// (get) Token: 0x06017F83 RID: 98179 RVA: 0x0033D2DA File Offset: 0x0033B4DA
		internal override int ElementTypeId
		{
			get
			{
				return 11304;
			}
		}

		// Token: 0x06017F84 RID: 98180 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008156 RID: 33110
		// (get) Token: 0x06017F85 RID: 98181 RVA: 0x0033D2E1 File Offset: 0x0033B4E1
		internal override string[] AttributeTagNames
		{
			get
			{
				return WorkbookView.attributeTagNames;
			}
		}

		// Token: 0x17008157 RID: 33111
		// (get) Token: 0x06017F86 RID: 98182 RVA: 0x0033D2E8 File Offset: 0x0033B4E8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WorkbookView.attributeNamespaceIds;
			}
		}

		// Token: 0x17008158 RID: 33112
		// (get) Token: 0x06017F87 RID: 98183 RVA: 0x0033D2EF File Offset: 0x0033B4EF
		// (set) Token: 0x06017F88 RID: 98184 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "visibility")]
		public EnumValue<VisibilityValues> Visibility
		{
			get
			{
				return (EnumValue<VisibilityValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008159 RID: 33113
		// (get) Token: 0x06017F89 RID: 98185 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017F8A RID: 98186 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "minimized")]
		public BooleanValue Minimized
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700815A RID: 33114
		// (get) Token: 0x06017F8B RID: 98187 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017F8C RID: 98188 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showHorizontalScroll")]
		public BooleanValue ShowHorizontalScroll
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700815B RID: 33115
		// (get) Token: 0x06017F8D RID: 98189 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017F8E RID: 98190 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showVerticalScroll")]
		public BooleanValue ShowVerticalScroll
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

		// Token: 0x1700815C RID: 33116
		// (get) Token: 0x06017F8F RID: 98191 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017F90 RID: 98192 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "showSheetTabs")]
		public BooleanValue ShowSheetTabs
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700815D RID: 33117
		// (get) Token: 0x06017F91 RID: 98193 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x06017F92 RID: 98194 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "xWindow")]
		public Int32Value XWindow
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700815E RID: 33118
		// (get) Token: 0x06017F93 RID: 98195 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x06017F94 RID: 98196 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "yWindow")]
		public Int32Value YWindow
		{
			get
			{
				return (Int32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700815F RID: 33119
		// (get) Token: 0x06017F95 RID: 98197 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x06017F96 RID: 98198 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "windowWidth")]
		public UInt32Value WindowWidth
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008160 RID: 33120
		// (get) Token: 0x06017F97 RID: 98199 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06017F98 RID: 98200 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "windowHeight")]
		public UInt32Value WindowHeight
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008161 RID: 33121
		// (get) Token: 0x06017F99 RID: 98201 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x06017F9A RID: 98202 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "tabRatio")]
		public UInt32Value TabRatio
		{
			get
			{
				return (UInt32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17008162 RID: 33122
		// (get) Token: 0x06017F9B RID: 98203 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x06017F9C RID: 98204 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "firstSheet")]
		public UInt32Value FirstSheet
		{
			get
			{
				return (UInt32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17008163 RID: 33123
		// (get) Token: 0x06017F9D RID: 98205 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x06017F9E RID: 98206 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "activeTab")]
		public UInt32Value ActiveTab
		{
			get
			{
				return (UInt32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17008164 RID: 33124
		// (get) Token: 0x06017F9F RID: 98207 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06017FA0 RID: 98208 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "autoFilterDateGrouping")]
		public BooleanValue AutoFilterDateGrouping
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x06017FA1 RID: 98209 RVA: 0x00293ECF File Offset: 0x002920CF
		public WorkbookView()
		{
		}

		// Token: 0x06017FA2 RID: 98210 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WorkbookView(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017FA3 RID: 98211 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WorkbookView(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017FA4 RID: 98212 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WorkbookView(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017FA5 RID: 98213 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17008165 RID: 33125
		// (get) Token: 0x06017FA6 RID: 98214 RVA: 0x0033D2FE File Offset: 0x0033B4FE
		internal override string[] ElementTagNames
		{
			get
			{
				return WorkbookView.eleTagNames;
			}
		}

		// Token: 0x17008166 RID: 33126
		// (get) Token: 0x06017FA7 RID: 98215 RVA: 0x0033D305 File Offset: 0x0033B505
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WorkbookView.eleNamespaceIds;
			}
		}

		// Token: 0x17008167 RID: 33127
		// (get) Token: 0x06017FA8 RID: 98216 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008168 RID: 33128
		// (get) Token: 0x06017FA9 RID: 98217 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x06017FAA RID: 98218 RVA: 0x00332911 File Offset: 0x00330B11
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

		// Token: 0x06017FAB RID: 98219 RVA: 0x0033D30C File Offset: 0x0033B50C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "visibility" == name)
			{
				return new EnumValue<VisibilityValues>();
			}
			if (namespaceId == 0 && "minimized" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showHorizontalScroll" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showVerticalScroll" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showSheetTabs" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xWindow" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "yWindow" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "windowWidth" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "windowHeight" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "tabRatio" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "firstSheet" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "activeTab" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "autoFilterDateGrouping" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017FAC RID: 98220 RVA: 0x0033D43F File Offset: 0x0033B63F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorkbookView>(deep);
		}

		// Token: 0x06017FAD RID: 98221 RVA: 0x0033D448 File Offset: 0x0033B648
		// Note: this type is marked as 'beforefieldinit'.
		static WorkbookView()
		{
			byte[] array = new byte[13];
			WorkbookView.attributeNamespaceIds = array;
			WorkbookView.eleTagNames = new string[] { "extLst" };
			WorkbookView.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009E56 RID: 40534
		private const string tagName = "workbookView";

		// Token: 0x04009E57 RID: 40535
		private const byte tagNsId = 22;

		// Token: 0x04009E58 RID: 40536
		internal const int ElementTypeIdConst = 11304;

		// Token: 0x04009E59 RID: 40537
		private static string[] attributeTagNames = new string[]
		{
			"visibility", "minimized", "showHorizontalScroll", "showVerticalScroll", "showSheetTabs", "xWindow", "yWindow", "windowWidth", "windowHeight", "tabRatio",
			"firstSheet", "activeTab", "autoFilterDateGrouping"
		};

		// Token: 0x04009E5A RID: 40538
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009E5B RID: 40539
		private static readonly string[] eleTagNames;

		// Token: 0x04009E5C RID: 40540
		private static readonly byte[] eleNamespaceIds;
	}
}
