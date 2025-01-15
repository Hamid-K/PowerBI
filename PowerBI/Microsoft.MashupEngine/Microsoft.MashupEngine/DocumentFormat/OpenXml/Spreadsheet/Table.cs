using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B28 RID: 11048
	[ChildElementInfo(typeof(TableExtensionList))]
	[ChildElementInfo(typeof(SortState))]
	[ChildElementInfo(typeof(TableColumns))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AutoFilter))]
	[ChildElementInfo(typeof(TableStyleInfo))]
	internal class Table : OpenXmlPartRootElement
	{
		// Token: 0x170076FB RID: 30459
		// (get) Token: 0x0601690E RID: 92430 RVA: 0x00049581 File Offset: 0x00047781
		public override string LocalName
		{
			get
			{
				return "table";
			}
		}

		// Token: 0x170076FC RID: 30460
		// (get) Token: 0x0601690F RID: 92431 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170076FD RID: 30461
		// (get) Token: 0x06016910 RID: 92432 RVA: 0x0032C78B File Offset: 0x0032A98B
		internal override int ElementTypeId
		{
			get
			{
				return 11046;
			}
		}

		// Token: 0x06016911 RID: 92433 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170076FE RID: 30462
		// (get) Token: 0x06016912 RID: 92434 RVA: 0x0032C792 File Offset: 0x0032A992
		internal override string[] AttributeTagNames
		{
			get
			{
				return Table.attributeTagNames;
			}
		}

		// Token: 0x170076FF RID: 30463
		// (get) Token: 0x06016913 RID: 92435 RVA: 0x0032C799 File Offset: 0x0032A999
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Table.attributeNamespaceIds;
			}
		}

		// Token: 0x17007700 RID: 30464
		// (get) Token: 0x06016914 RID: 92436 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016915 RID: 92437 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007701 RID: 30465
		// (get) Token: 0x06016916 RID: 92438 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016917 RID: 92439 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007702 RID: 30466
		// (get) Token: 0x06016918 RID: 92440 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016919 RID: 92441 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "displayName")]
		public StringValue DisplayName
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

		// Token: 0x17007703 RID: 30467
		// (get) Token: 0x0601691A RID: 92442 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601691B RID: 92443 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "comment")]
		public StringValue Comment
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007704 RID: 30468
		// (get) Token: 0x0601691C RID: 92444 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601691D RID: 92445 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x17007705 RID: 30469
		// (get) Token: 0x0601691E RID: 92446 RVA: 0x0032C7A0 File Offset: 0x0032A9A0
		// (set) Token: 0x0601691F RID: 92447 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "tableType")]
		public EnumValue<TableValues> TableType
		{
			get
			{
				return (EnumValue<TableValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007706 RID: 30470
		// (get) Token: 0x06016920 RID: 92448 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06016921 RID: 92449 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "headerRowCount")]
		public UInt32Value HeaderRowCount
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007707 RID: 30471
		// (get) Token: 0x06016922 RID: 92450 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06016923 RID: 92451 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "insertRow")]
		public BooleanValue InsertRow
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

		// Token: 0x17007708 RID: 30472
		// (get) Token: 0x06016924 RID: 92452 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06016925 RID: 92453 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "insertRowShift")]
		public BooleanValue InsertRowShift
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

		// Token: 0x17007709 RID: 30473
		// (get) Token: 0x06016926 RID: 92454 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x06016927 RID: 92455 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "totalsRowCount")]
		public UInt32Value TotalsRowCount
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

		// Token: 0x1700770A RID: 30474
		// (get) Token: 0x06016928 RID: 92456 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06016929 RID: 92457 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "totalsRowShown")]
		public BooleanValue TotalsRowShown
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

		// Token: 0x1700770B RID: 30475
		// (get) Token: 0x0601692A RID: 92458 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0601692B RID: 92459 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "published")]
		public BooleanValue Published
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

		// Token: 0x1700770C RID: 30476
		// (get) Token: 0x0601692C RID: 92460 RVA: 0x002E6EFA File Offset: 0x002E50FA
		// (set) Token: 0x0601692D RID: 92461 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "headerRowDxfId")]
		public UInt32Value HeaderRowFormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700770D RID: 30477
		// (get) Token: 0x0601692E RID: 92462 RVA: 0x0032C7AF File Offset: 0x0032A9AF
		// (set) Token: 0x0601692F RID: 92463 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "dataDxfId")]
		public UInt32Value DataFormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x1700770E RID: 30478
		// (get) Token: 0x06016930 RID: 92464 RVA: 0x003299DA File Offset: 0x00327BDA
		// (set) Token: 0x06016931 RID: 92465 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "totalsRowDxfId")]
		public UInt32Value TotalsRowFormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x1700770F RID: 30479
		// (get) Token: 0x06016932 RID: 92466 RVA: 0x002E6F0A File Offset: 0x002E510A
		// (set) Token: 0x06016933 RID: 92467 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "headerRowBorderDxfId")]
		public UInt32Value HeaderRowBorderFormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007710 RID: 30480
		// (get) Token: 0x06016934 RID: 92468 RVA: 0x002E6F1A File Offset: 0x002E511A
		// (set) Token: 0x06016935 RID: 92469 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "tableBorderDxfId")]
		public UInt32Value BorderFormatId
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

		// Token: 0x17007711 RID: 30481
		// (get) Token: 0x06016936 RID: 92470 RVA: 0x0030F16C File Offset: 0x0030D36C
		// (set) Token: 0x06016937 RID: 92471 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "totalsRowBorderDxfId")]
		public UInt32Value TotalsRowBorderFormatId
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

		// Token: 0x17007712 RID: 30482
		// (get) Token: 0x06016938 RID: 92472 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x06016939 RID: 92473 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "headerRowCellStyle")]
		public StringValue HeaderRowCellStyle
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17007713 RID: 30483
		// (get) Token: 0x0601693A RID: 92474 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0601693B RID: 92475 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "dataCellStyle")]
		public StringValue DataCellStyle
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17007714 RID: 30484
		// (get) Token: 0x0601693C RID: 92476 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0601693D RID: 92477 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "totalsRowCellStyle")]
		public StringValue TotalsRowCellStyle
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17007715 RID: 30485
		// (get) Token: 0x0601693E RID: 92478 RVA: 0x002E6F3A File Offset: 0x002E513A
		// (set) Token: 0x0601693F RID: 92479 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "connectionId")]
		public UInt32Value ConnectionId
		{
			get
			{
				return (UInt32Value)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x06016940 RID: 92480 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Table(TableDefinitionPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016941 RID: 92481 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(TableDefinitionPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17007716 RID: 30486
		// (get) Token: 0x06016942 RID: 92482 RVA: 0x0032C7BF File Offset: 0x0032A9BF
		// (set) Token: 0x06016943 RID: 92483 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public TableDefinitionPart TableDefinitionPart
		{
			get
			{
				return base.OpenXmlPart as TableDefinitionPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016944 RID: 92484 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Table(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016945 RID: 92485 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Table(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016946 RID: 92486 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Table(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016947 RID: 92487 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Table()
		{
		}

		// Token: 0x06016948 RID: 92488 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(TableDefinitionPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016949 RID: 92489 RVA: 0x0032C7CC File Offset: 0x0032A9CC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "autoFilter" == name)
			{
				return new AutoFilter();
			}
			if (22 == namespaceId && "sortState" == name)
			{
				return new SortState();
			}
			if (22 == namespaceId && "tableColumns" == name)
			{
				return new TableColumns();
			}
			if (22 == namespaceId && "tableStyleInfo" == name)
			{
				return new TableStyleInfo();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new TableExtensionList();
			}
			return null;
		}

		// Token: 0x17007717 RID: 30487
		// (get) Token: 0x0601694A RID: 92490 RVA: 0x0032C852 File Offset: 0x0032AA52
		internal override string[] ElementTagNames
		{
			get
			{
				return Table.eleTagNames;
			}
		}

		// Token: 0x17007718 RID: 30488
		// (get) Token: 0x0601694B RID: 92491 RVA: 0x0032C859 File Offset: 0x0032AA59
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Table.eleNamespaceIds;
			}
		}

		// Token: 0x17007719 RID: 30489
		// (get) Token: 0x0601694C RID: 92492 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700771A RID: 30490
		// (get) Token: 0x0601694D RID: 92493 RVA: 0x0032C860 File Offset: 0x0032AA60
		// (set) Token: 0x0601694E RID: 92494 RVA: 0x0032C869 File Offset: 0x0032AA69
		public AutoFilter AutoFilter
		{
			get
			{
				return base.GetElement<AutoFilter>(0);
			}
			set
			{
				base.SetElement<AutoFilter>(0, value);
			}
		}

		// Token: 0x1700771B RID: 30491
		// (get) Token: 0x0601694F RID: 92495 RVA: 0x0032C873 File Offset: 0x0032AA73
		// (set) Token: 0x06016950 RID: 92496 RVA: 0x0032C87C File Offset: 0x0032AA7C
		public SortState SortState
		{
			get
			{
				return base.GetElement<SortState>(1);
			}
			set
			{
				base.SetElement<SortState>(1, value);
			}
		}

		// Token: 0x1700771C RID: 30492
		// (get) Token: 0x06016951 RID: 92497 RVA: 0x0032C886 File Offset: 0x0032AA86
		// (set) Token: 0x06016952 RID: 92498 RVA: 0x0032C88F File Offset: 0x0032AA8F
		public TableColumns TableColumns
		{
			get
			{
				return base.GetElement<TableColumns>(2);
			}
			set
			{
				base.SetElement<TableColumns>(2, value);
			}
		}

		// Token: 0x1700771D RID: 30493
		// (get) Token: 0x06016953 RID: 92499 RVA: 0x0032C899 File Offset: 0x0032AA99
		// (set) Token: 0x06016954 RID: 92500 RVA: 0x0032C8A2 File Offset: 0x0032AAA2
		public TableStyleInfo TableStyleInfo
		{
			get
			{
				return base.GetElement<TableStyleInfo>(3);
			}
			set
			{
				base.SetElement<TableStyleInfo>(3, value);
			}
		}

		// Token: 0x1700771E RID: 30494
		// (get) Token: 0x06016955 RID: 92501 RVA: 0x0032C8AC File Offset: 0x0032AAAC
		// (set) Token: 0x06016956 RID: 92502 RVA: 0x0032C8B5 File Offset: 0x0032AAB5
		public TableExtensionList TableExtensionList
		{
			get
			{
				return base.GetElement<TableExtensionList>(4);
			}
			set
			{
				base.SetElement<TableExtensionList>(4, value);
			}
		}

		// Token: 0x06016957 RID: 92503 RVA: 0x0032C8C0 File Offset: 0x0032AAC0
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
			if (namespaceId == 0 && "displayName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "comment" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tableType" == name)
			{
				return new EnumValue<TableValues>();
			}
			if (namespaceId == 0 && "headerRowCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "insertRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "insertRowShift" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "totalsRowCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "totalsRowShown" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "published" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "headerRowDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "dataDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "totalsRowDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "headerRowBorderDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "tableBorderDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "totalsRowBorderDxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "headerRowCellStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dataCellStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "totalsRowCellStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "connectionId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016958 RID: 92504 RVA: 0x0032CAB9 File Offset: 0x0032ACB9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Table>(deep);
		}

		// Token: 0x06016959 RID: 92505 RVA: 0x0032CAC4 File Offset: 0x0032ACC4
		// Note: this type is marked as 'beforefieldinit'.
		static Table()
		{
			byte[] array = new byte[22];
			Table.attributeNamespaceIds = array;
			Table.eleTagNames = new string[] { "autoFilter", "sortState", "tableColumns", "tableStyleInfo", "extLst" };
			Table.eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22 };
		}

		// Token: 0x04009925 RID: 39205
		private const string tagName = "table";

		// Token: 0x04009926 RID: 39206
		private const byte tagNsId = 22;

		// Token: 0x04009927 RID: 39207
		internal const int ElementTypeIdConst = 11046;

		// Token: 0x04009928 RID: 39208
		private static string[] attributeTagNames = new string[]
		{
			"id", "name", "displayName", "comment", "ref", "tableType", "headerRowCount", "insertRow", "insertRowShift", "totalsRowCount",
			"totalsRowShown", "published", "headerRowDxfId", "dataDxfId", "totalsRowDxfId", "headerRowBorderDxfId", "tableBorderDxfId", "totalsRowBorderDxfId", "headerRowCellStyle", "dataCellStyle",
			"totalsRowCellStyle", "connectionId"
		};

		// Token: 0x04009929 RID: 39209
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400992A RID: 39210
		private static readonly string[] eleTagNames;

		// Token: 0x0400992B RID: 39211
		private static readonly byte[] eleNamespaceIds;
	}
}
