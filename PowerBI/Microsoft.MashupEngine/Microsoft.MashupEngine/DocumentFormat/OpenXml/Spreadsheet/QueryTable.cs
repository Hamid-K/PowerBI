using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B1C RID: 11036
	[ChildElementInfo(typeof(QueryTableRefresh))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class QueryTable : OpenXmlPartRootElement
	{
		// Token: 0x1700765A RID: 30298
		// (get) Token: 0x06016785 RID: 92037 RVA: 0x002A8436 File Offset: 0x002A6636
		public override string LocalName
		{
			get
			{
				return "queryTable";
			}
		}

		// Token: 0x1700765B RID: 30299
		// (get) Token: 0x06016786 RID: 92038 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700765C RID: 30300
		// (get) Token: 0x06016787 RID: 92039 RVA: 0x0032ADD8 File Offset: 0x00328FD8
		internal override int ElementTypeId
		{
			get
			{
				return 11034;
			}
		}

		// Token: 0x06016788 RID: 92040 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700765D RID: 30301
		// (get) Token: 0x06016789 RID: 92041 RVA: 0x0032ADDF File Offset: 0x00328FDF
		internal override string[] AttributeTagNames
		{
			get
			{
				return QueryTable.attributeTagNames;
			}
		}

		// Token: 0x1700765E RID: 30302
		// (get) Token: 0x0601678A RID: 92042 RVA: 0x0032ADE6 File Offset: 0x00328FE6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return QueryTable.attributeNamespaceIds;
			}
		}

		// Token: 0x1700765F RID: 30303
		// (get) Token: 0x0601678B RID: 92043 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601678C RID: 92044 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007660 RID: 30304
		// (get) Token: 0x0601678D RID: 92045 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601678E RID: 92046 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "headers")]
		public BooleanValue Headers
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

		// Token: 0x17007661 RID: 30305
		// (get) Token: 0x0601678F RID: 92047 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016790 RID: 92048 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "rowNumbers")]
		public BooleanValue RowNumbers
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

		// Token: 0x17007662 RID: 30306
		// (get) Token: 0x06016791 RID: 92049 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016792 RID: 92050 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "disableRefresh")]
		public BooleanValue DisableRefresh
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

		// Token: 0x17007663 RID: 30307
		// (get) Token: 0x06016793 RID: 92051 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016794 RID: 92052 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "backgroundRefresh")]
		public BooleanValue BackgroundRefresh
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

		// Token: 0x17007664 RID: 30308
		// (get) Token: 0x06016795 RID: 92053 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06016796 RID: 92054 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "firstBackgroundRefresh")]
		public BooleanValue FirstBackgroundRefresh
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

		// Token: 0x17007665 RID: 30309
		// (get) Token: 0x06016797 RID: 92055 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06016798 RID: 92056 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "refreshOnLoad")]
		public BooleanValue RefreshOnLoad
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

		// Token: 0x17007666 RID: 30310
		// (get) Token: 0x06016799 RID: 92057 RVA: 0x0032ADED File Offset: 0x00328FED
		// (set) Token: 0x0601679A RID: 92058 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "growShrinkType")]
		public EnumValue<GrowShrinkValues> GrowShrinkType
		{
			get
			{
				return (EnumValue<GrowShrinkValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007667 RID: 30311
		// (get) Token: 0x0601679B RID: 92059 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0601679C RID: 92060 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "fillFormulas")]
		public BooleanValue FillFormulas
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

		// Token: 0x17007668 RID: 30312
		// (get) Token: 0x0601679D RID: 92061 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0601679E RID: 92062 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "removeDataOnSave")]
		public BooleanValue RemoveDataOnSave
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

		// Token: 0x17007669 RID: 30313
		// (get) Token: 0x0601679F RID: 92063 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060167A0 RID: 92064 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "disableEdit")]
		public BooleanValue DisableEdit
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

		// Token: 0x1700766A RID: 30314
		// (get) Token: 0x060167A1 RID: 92065 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x060167A2 RID: 92066 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "preserveFormatting")]
		public BooleanValue PreserveFormatting
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

		// Token: 0x1700766B RID: 30315
		// (get) Token: 0x060167A3 RID: 92067 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x060167A4 RID: 92068 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "adjustColumnWidth")]
		public BooleanValue AdjustColumnWidth
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

		// Token: 0x1700766C RID: 30316
		// (get) Token: 0x060167A5 RID: 92069 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x060167A6 RID: 92070 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "intermediate")]
		public BooleanValue Intermediate
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

		// Token: 0x1700766D RID: 30317
		// (get) Token: 0x060167A7 RID: 92071 RVA: 0x003299DA File Offset: 0x00327BDA
		// (set) Token: 0x060167A8 RID: 92072 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "connectionId")]
		public UInt32Value ConnectionId
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

		// Token: 0x1700766E RID: 30318
		// (get) Token: 0x060167A9 RID: 92073 RVA: 0x002E6F0A File Offset: 0x002E510A
		// (set) Token: 0x060167AA RID: 92074 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "autoFormatId")]
		public UInt32Value AutoFormatId
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

		// Token: 0x1700766F RID: 30319
		// (get) Token: 0x060167AB RID: 92075 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x060167AC RID: 92076 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "applyNumberFormats")]
		public BooleanValue ApplyNumberFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17007670 RID: 30320
		// (get) Token: 0x060167AD RID: 92077 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x060167AE RID: 92078 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "applyBorderFormats")]
		public BooleanValue ApplyBorderFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17007671 RID: 30321
		// (get) Token: 0x060167AF RID: 92079 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x060167B0 RID: 92080 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "applyFontFormats")]
		public BooleanValue ApplyFontFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17007672 RID: 30322
		// (get) Token: 0x060167B1 RID: 92081 RVA: 0x002D6080 File Offset: 0x002D4280
		// (set) Token: 0x060167B2 RID: 92082 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "applyPatternFormats")]
		public BooleanValue ApplyPatternFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17007673 RID: 30323
		// (get) Token: 0x060167B3 RID: 92083 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x060167B4 RID: 92084 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "applyAlignmentFormats")]
		public BooleanValue ApplyAlignmentFormats
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

		// Token: 0x17007674 RID: 30324
		// (get) Token: 0x060167B5 RID: 92085 RVA: 0x002DB1B1 File Offset: 0x002D93B1
		// (set) Token: 0x060167B6 RID: 92086 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "applyWidthHeightFormats")]
		public BooleanValue ApplyWidthHeightFormats
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

		// Token: 0x060167B7 RID: 92087 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal QueryTable(QueryTablePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060167B8 RID: 92088 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(QueryTablePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17007675 RID: 30325
		// (get) Token: 0x060167B9 RID: 92089 RVA: 0x0032ADFC File Offset: 0x00328FFC
		// (set) Token: 0x060167BA RID: 92090 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public QueryTablePart QueryTablePart
		{
			get
			{
				return base.OpenXmlPart as QueryTablePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060167BB RID: 92091 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public QueryTable(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060167BC RID: 92092 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public QueryTable(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060167BD RID: 92093 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public QueryTable(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060167BE RID: 92094 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public QueryTable()
		{
		}

		// Token: 0x060167BF RID: 92095 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(QueryTablePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060167C0 RID: 92096 RVA: 0x0032AE09 File Offset: 0x00329009
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "queryTableRefresh" == name)
			{
				return new QueryTableRefresh();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007676 RID: 30326
		// (get) Token: 0x060167C1 RID: 92097 RVA: 0x0032AE3C File Offset: 0x0032903C
		internal override string[] ElementTagNames
		{
			get
			{
				return QueryTable.eleTagNames;
			}
		}

		// Token: 0x17007677 RID: 30327
		// (get) Token: 0x060167C2 RID: 92098 RVA: 0x0032AE43 File Offset: 0x00329043
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return QueryTable.eleNamespaceIds;
			}
		}

		// Token: 0x17007678 RID: 30328
		// (get) Token: 0x060167C3 RID: 92099 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007679 RID: 30329
		// (get) Token: 0x060167C4 RID: 92100 RVA: 0x0032AE4A File Offset: 0x0032904A
		// (set) Token: 0x060167C5 RID: 92101 RVA: 0x0032AE53 File Offset: 0x00329053
		public QueryTableRefresh QueryTableRefresh
		{
			get
			{
				return base.GetElement<QueryTableRefresh>(0);
			}
			set
			{
				base.SetElement<QueryTableRefresh>(0, value);
			}
		}

		// Token: 0x1700767A RID: 30330
		// (get) Token: 0x060167C6 RID: 92102 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x060167C7 RID: 92103 RVA: 0x002E96F3 File Offset: 0x002E78F3
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x060167C8 RID: 92104 RVA: 0x0032AE60 File Offset: 0x00329060
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "headers" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rowNumbers" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "disableRefresh" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "backgroundRefresh" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "firstBackgroundRefresh" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "refreshOnLoad" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "growShrinkType" == name)
			{
				return new EnumValue<GrowShrinkValues>();
			}
			if (namespaceId == 0 && "fillFormulas" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "removeDataOnSave" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "disableEdit" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "preserveFormatting" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "adjustColumnWidth" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "intermediate" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "connectionId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "autoFormatId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "applyNumberFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyBorderFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyFontFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyPatternFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyAlignmentFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyWidthHeightFormats" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060167C9 RID: 92105 RVA: 0x0032B059 File Offset: 0x00329259
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QueryTable>(deep);
		}

		// Token: 0x060167CA RID: 92106 RVA: 0x0032B064 File Offset: 0x00329264
		// Note: this type is marked as 'beforefieldinit'.
		static QueryTable()
		{
			byte[] array = new byte[22];
			QueryTable.attributeNamespaceIds = array;
			QueryTable.eleTagNames = new string[] { "queryTableRefresh", "extLst" };
			QueryTable.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x040098ED RID: 39149
		private const string tagName = "queryTable";

		// Token: 0x040098EE RID: 39150
		private const byte tagNsId = 22;

		// Token: 0x040098EF RID: 39151
		internal const int ElementTypeIdConst = 11034;

		// Token: 0x040098F0 RID: 39152
		private static string[] attributeTagNames = new string[]
		{
			"name", "headers", "rowNumbers", "disableRefresh", "backgroundRefresh", "firstBackgroundRefresh", "refreshOnLoad", "growShrinkType", "fillFormulas", "removeDataOnSave",
			"disableEdit", "preserveFormatting", "adjustColumnWidth", "intermediate", "connectionId", "autoFormatId", "applyNumberFormats", "applyBorderFormats", "applyFontFormats", "applyPatternFormats",
			"applyAlignmentFormats", "applyWidthHeightFormats"
		};

		// Token: 0x040098F1 RID: 39153
		private static byte[] attributeNamespaceIds;

		// Token: 0x040098F2 RID: 39154
		private static readonly string[] eleTagNames;

		// Token: 0x040098F3 RID: 39155
		private static readonly byte[] eleNamespaceIds;
	}
}
