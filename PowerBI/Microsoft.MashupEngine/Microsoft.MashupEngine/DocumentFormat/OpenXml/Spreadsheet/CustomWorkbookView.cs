using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C37 RID: 11319
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomWorkbookView : OpenXmlCompositeElement
	{
		// Token: 0x17008121 RID: 33057
		// (get) Token: 0x06017F1C RID: 98076 RVA: 0x0033CDA4 File Offset: 0x0033AFA4
		public override string LocalName
		{
			get
			{
				return "customWorkbookView";
			}
		}

		// Token: 0x17008122 RID: 33058
		// (get) Token: 0x06017F1D RID: 98077 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008123 RID: 33059
		// (get) Token: 0x06017F1E RID: 98078 RVA: 0x0033CDAB File Offset: 0x0033AFAB
		internal override int ElementTypeId
		{
			get
			{
				return 11301;
			}
		}

		// Token: 0x06017F1F RID: 98079 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008124 RID: 33060
		// (get) Token: 0x06017F20 RID: 98080 RVA: 0x0033CDB2 File Offset: 0x0033AFB2
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomWorkbookView.attributeTagNames;
			}
		}

		// Token: 0x17008125 RID: 33061
		// (get) Token: 0x06017F21 RID: 98081 RVA: 0x0033CDB9 File Offset: 0x0033AFB9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomWorkbookView.attributeNamespaceIds;
			}
		}

		// Token: 0x17008126 RID: 33062
		// (get) Token: 0x06017F22 RID: 98082 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017F23 RID: 98083 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17008127 RID: 33063
		// (get) Token: 0x06017F24 RID: 98084 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017F25 RID: 98085 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "guid")]
		public StringValue Guid
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

		// Token: 0x17008128 RID: 33064
		// (get) Token: 0x06017F26 RID: 98086 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017F27 RID: 98087 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "autoUpdate")]
		public BooleanValue AutoUpdate
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

		// Token: 0x17008129 RID: 33065
		// (get) Token: 0x06017F28 RID: 98088 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06017F29 RID: 98089 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "mergeInterval")]
		public UInt32Value MergeInterval
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700812A RID: 33066
		// (get) Token: 0x06017F2A RID: 98090 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017F2B RID: 98091 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "changesSavedWin")]
		public BooleanValue ChangesSavedWin
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

		// Token: 0x1700812B RID: 33067
		// (get) Token: 0x06017F2C RID: 98092 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017F2D RID: 98093 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "onlySync")]
		public BooleanValue OnlySync
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700812C RID: 33068
		// (get) Token: 0x06017F2E RID: 98094 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017F2F RID: 98095 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "personalView")]
		public BooleanValue PersonalView
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700812D RID: 33069
		// (get) Token: 0x06017F30 RID: 98096 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017F31 RID: 98097 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "includePrintSettings")]
		public BooleanValue IncludePrintSettings
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700812E RID: 33070
		// (get) Token: 0x06017F32 RID: 98098 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017F33 RID: 98099 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "includeHiddenRowCol")]
		public BooleanValue IncludeHiddenRowColumn
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x1700812F RID: 33071
		// (get) Token: 0x06017F34 RID: 98100 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017F35 RID: 98101 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "maximized")]
		public BooleanValue Maximized
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17008130 RID: 33072
		// (get) Token: 0x06017F36 RID: 98102 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06017F37 RID: 98103 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "minimized")]
		public BooleanValue Minimized
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17008131 RID: 33073
		// (get) Token: 0x06017F38 RID: 98104 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06017F39 RID: 98105 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "showHorizontalScroll")]
		public BooleanValue ShowHorizontalScroll
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17008132 RID: 33074
		// (get) Token: 0x06017F3A RID: 98106 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06017F3B RID: 98107 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "showVerticalScroll")]
		public BooleanValue ShowVerticalScroll
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

		// Token: 0x17008133 RID: 33075
		// (get) Token: 0x06017F3C RID: 98108 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06017F3D RID: 98109 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "showSheetTabs")]
		public BooleanValue ShowSheetTabs
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17008134 RID: 33076
		// (get) Token: 0x06017F3E RID: 98110 RVA: 0x00300801 File Offset: 0x002FEA01
		// (set) Token: 0x06017F3F RID: 98111 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "xWindow")]
		public Int32Value XWindow
		{
			get
			{
				return (Int32Value)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17008135 RID: 33077
		// (get) Token: 0x06017F40 RID: 98112 RVA: 0x002C82A1 File Offset: 0x002C64A1
		// (set) Token: 0x06017F41 RID: 98113 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "yWindow")]
		public Int32Value YWindow
		{
			get
			{
				return (Int32Value)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17008136 RID: 33078
		// (get) Token: 0x06017F42 RID: 98114 RVA: 0x002E6F1A File Offset: 0x002E511A
		// (set) Token: 0x06017F43 RID: 98115 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "windowWidth")]
		public UInt32Value WindowWidth
		{
			get
			{
				return (UInt32Value)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17008137 RID: 33079
		// (get) Token: 0x06017F44 RID: 98116 RVA: 0x0030F16C File Offset: 0x0030D36C
		// (set) Token: 0x06017F45 RID: 98117 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "windowHeight")]
		public UInt32Value WindowHeight
		{
			get
			{
				return (UInt32Value)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17008138 RID: 33080
		// (get) Token: 0x06017F46 RID: 98118 RVA: 0x003389D0 File Offset: 0x00336BD0
		// (set) Token: 0x06017F47 RID: 98119 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "tabRatio")]
		public UInt32Value TabRatio
		{
			get
			{
				return (UInt32Value)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17008139 RID: 33081
		// (get) Token: 0x06017F48 RID: 98120 RVA: 0x003218CD File Offset: 0x0031FACD
		// (set) Token: 0x06017F49 RID: 98121 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "activeSheetId")]
		public UInt32Value ActiveSheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x1700813A RID: 33082
		// (get) Token: 0x06017F4A RID: 98122 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x06017F4B RID: 98123 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "showFormulaBar")]
		public BooleanValue ShowFormulaBar
		{
			get
			{
				return (BooleanValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x1700813B RID: 33083
		// (get) Token: 0x06017F4C RID: 98124 RVA: 0x002DB1B1 File Offset: 0x002D93B1
		// (set) Token: 0x06017F4D RID: 98125 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "showStatusbar")]
		public BooleanValue ShowStatusbar
		{
			get
			{
				return (BooleanValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x1700813C RID: 33084
		// (get) Token: 0x06017F4E RID: 98126 RVA: 0x0033CDC0 File Offset: 0x0033AFC0
		// (set) Token: 0x06017F4F RID: 98127 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "showComments")]
		public EnumValue<CommentsValues> ShowComments
		{
			get
			{
				return (EnumValue<CommentsValues>)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x1700813D RID: 33085
		// (get) Token: 0x06017F50 RID: 98128 RVA: 0x0033CDD0 File Offset: 0x0033AFD0
		// (set) Token: 0x06017F51 RID: 98129 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "showObjects")]
		public EnumValue<ObjectDisplayValues> ShowObjects
		{
			get
			{
				return (EnumValue<ObjectDisplayValues>)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x06017F52 RID: 98130 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomWorkbookView()
		{
		}

		// Token: 0x06017F53 RID: 98131 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomWorkbookView(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017F54 RID: 98132 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomWorkbookView(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017F55 RID: 98133 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomWorkbookView(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017F56 RID: 98134 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700813E RID: 33086
		// (get) Token: 0x06017F57 RID: 98135 RVA: 0x0033CDE0 File Offset: 0x0033AFE0
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomWorkbookView.eleTagNames;
			}
		}

		// Token: 0x1700813F RID: 33087
		// (get) Token: 0x06017F58 RID: 98136 RVA: 0x0033CDE7 File Offset: 0x0033AFE7
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomWorkbookView.eleNamespaceIds;
			}
		}

		// Token: 0x17008140 RID: 33088
		// (get) Token: 0x06017F59 RID: 98137 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008141 RID: 33089
		// (get) Token: 0x06017F5A RID: 98138 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x06017F5B RID: 98139 RVA: 0x00332911 File Offset: 0x00330B11
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

		// Token: 0x06017F5C RID: 98140 RVA: 0x0033CDF0 File Offset: 0x0033AFF0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "autoUpdate" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "mergeInterval" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "changesSavedWin" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "onlySync" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "personalView" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "includePrintSettings" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "includeHiddenRowCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "maximized" == name)
			{
				return new BooleanValue();
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
			if (namespaceId == 0 && "activeSheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "showFormulaBar" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showStatusbar" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showComments" == name)
			{
				return new EnumValue<CommentsValues>();
			}
			if (namespaceId == 0 && "showObjects" == name)
			{
				return new EnumValue<ObjectDisplayValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017F5D RID: 98141 RVA: 0x0033D015 File Offset: 0x0033B215
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomWorkbookView>(deep);
		}

		// Token: 0x06017F5E RID: 98142 RVA: 0x0033D020 File Offset: 0x0033B220
		// Note: this type is marked as 'beforefieldinit'.
		static CustomWorkbookView()
		{
			byte[] array = new byte[24];
			CustomWorkbookView.attributeNamespaceIds = array;
			CustomWorkbookView.eleTagNames = new string[] { "extLst" };
			CustomWorkbookView.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009E45 RID: 40517
		private const string tagName = "customWorkbookView";

		// Token: 0x04009E46 RID: 40518
		private const byte tagNsId = 22;

		// Token: 0x04009E47 RID: 40519
		internal const int ElementTypeIdConst = 11301;

		// Token: 0x04009E48 RID: 40520
		private static string[] attributeTagNames = new string[]
		{
			"name", "guid", "autoUpdate", "mergeInterval", "changesSavedWin", "onlySync", "personalView", "includePrintSettings", "includeHiddenRowCol", "maximized",
			"minimized", "showHorizontalScroll", "showVerticalScroll", "showSheetTabs", "xWindow", "yWindow", "windowWidth", "windowHeight", "tabRatio", "activeSheetId",
			"showFormulaBar", "showStatusbar", "showComments", "showObjects"
		};

		// Token: 0x04009E49 RID: 40521
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009E4A RID: 40522
		private static readonly string[] eleTagNames;

		// Token: 0x04009E4B RID: 40523
		private static readonly byte[] eleNamespaceIds;
	}
}
