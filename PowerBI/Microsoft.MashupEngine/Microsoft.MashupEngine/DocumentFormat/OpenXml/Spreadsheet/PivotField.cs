using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B73 RID: 11123
	[ChildElementInfo(typeof(PivotFieldExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Items))]
	[ChildElementInfo(typeof(AutoSortScope))]
	internal class PivotField : OpenXmlCompositeElement
	{
		// Token: 0x17007984 RID: 31108
		// (get) Token: 0x06016EC9 RID: 93897 RVA: 0x002E618B File Offset: 0x002E438B
		public override string LocalName
		{
			get
			{
				return "pivotField";
			}
		}

		// Token: 0x17007985 RID: 31109
		// (get) Token: 0x06016ECA RID: 93898 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007986 RID: 31110
		// (get) Token: 0x06016ECB RID: 93899 RVA: 0x0033092A File Offset: 0x0032EB2A
		internal override int ElementTypeId
		{
			get
			{
				return 11103;
			}
		}

		// Token: 0x06016ECC RID: 93900 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007987 RID: 31111
		// (get) Token: 0x06016ECD RID: 93901 RVA: 0x00330931 File Offset: 0x0032EB31
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotField.attributeTagNames;
			}
		}

		// Token: 0x17007988 RID: 31112
		// (get) Token: 0x06016ECE RID: 93902 RVA: 0x00330938 File Offset: 0x0032EB38
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotField.attributeNamespaceIds;
			}
		}

		// Token: 0x17007989 RID: 31113
		// (get) Token: 0x06016ECF RID: 93903 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016ED0 RID: 93904 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700798A RID: 31114
		// (get) Token: 0x06016ED1 RID: 93905 RVA: 0x0033093F File Offset: 0x0032EB3F
		// (set) Token: 0x06016ED2 RID: 93906 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "axis")]
		public EnumValue<PivotTableAxisValues> Axis
		{
			get
			{
				return (EnumValue<PivotTableAxisValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700798B RID: 31115
		// (get) Token: 0x06016ED3 RID: 93907 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016ED4 RID: 93908 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dataField")]
		public BooleanValue DataField
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

		// Token: 0x1700798C RID: 31116
		// (get) Token: 0x06016ED5 RID: 93909 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016ED6 RID: 93910 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "subtotalCaption")]
		public StringValue SubtotalCaption
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

		// Token: 0x1700798D RID: 31117
		// (get) Token: 0x06016ED7 RID: 93911 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016ED8 RID: 93912 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "showDropDowns")]
		public BooleanValue ShowDropDowns
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

		// Token: 0x1700798E RID: 31118
		// (get) Token: 0x06016ED9 RID: 93913 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06016EDA RID: 93914 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "hiddenLevel")]
		public BooleanValue HiddenLevel
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

		// Token: 0x1700798F RID: 31119
		// (get) Token: 0x06016EDB RID: 93915 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06016EDC RID: 93916 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "uniqueMemberProperty")]
		public StringValue UniqueMemberProperty
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007990 RID: 31120
		// (get) Token: 0x06016EDD RID: 93917 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06016EDE RID: 93918 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "compact")]
		public BooleanValue Compact
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

		// Token: 0x17007991 RID: 31121
		// (get) Token: 0x06016EDF RID: 93919 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06016EE0 RID: 93920 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "allDrilled")]
		public BooleanValue AllDrilled
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

		// Token: 0x17007992 RID: 31122
		// (get) Token: 0x06016EE1 RID: 93921 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x06016EE2 RID: 93922 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "numFmtId")]
		public UInt32Value NumberFormatId
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

		// Token: 0x17007993 RID: 31123
		// (get) Token: 0x06016EE3 RID: 93923 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06016EE4 RID: 93924 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "outline")]
		public BooleanValue Outline
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

		// Token: 0x17007994 RID: 31124
		// (get) Token: 0x06016EE5 RID: 93925 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06016EE6 RID: 93926 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "subtotalTop")]
		public BooleanValue SubtotalTop
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

		// Token: 0x17007995 RID: 31125
		// (get) Token: 0x06016EE7 RID: 93927 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06016EE8 RID: 93928 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "dragToRow")]
		public BooleanValue DragToRow
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

		// Token: 0x17007996 RID: 31126
		// (get) Token: 0x06016EE9 RID: 93929 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06016EEA RID: 93930 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "dragToCol")]
		public BooleanValue DragToColumn
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

		// Token: 0x17007997 RID: 31127
		// (get) Token: 0x06016EEB RID: 93931 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x06016EEC RID: 93932 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "multipleItemSelectionAllowed")]
		public BooleanValue MultipleItemSelectionAllowed
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17007998 RID: 31128
		// (get) Token: 0x06016EED RID: 93933 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x06016EEE RID: 93934 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "dragToPage")]
		public BooleanValue DragToPage
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007999 RID: 31129
		// (get) Token: 0x06016EEF RID: 93935 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06016EF0 RID: 93936 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "dragToData")]
		public BooleanValue DragToData
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

		// Token: 0x1700799A RID: 31130
		// (get) Token: 0x06016EF1 RID: 93937 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x06016EF2 RID: 93938 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "dragOff")]
		public BooleanValue DragOff
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

		// Token: 0x1700799B RID: 31131
		// (get) Token: 0x06016EF3 RID: 93939 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x06016EF4 RID: 93940 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "showAll")]
		public BooleanValue ShowAll
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

		// Token: 0x1700799C RID: 31132
		// (get) Token: 0x06016EF5 RID: 93941 RVA: 0x002D6080 File Offset: 0x002D4280
		// (set) Token: 0x06016EF6 RID: 93942 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertBlankRow")]
		public BooleanValue InsertBlankRow
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

		// Token: 0x1700799D RID: 31133
		// (get) Token: 0x06016EF7 RID: 93943 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x06016EF8 RID: 93944 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "serverField")]
		public BooleanValue ServerField
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

		// Token: 0x1700799E RID: 31134
		// (get) Token: 0x06016EF9 RID: 93945 RVA: 0x002DB1B1 File Offset: 0x002D93B1
		// (set) Token: 0x06016EFA RID: 93946 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertPageBreak")]
		public BooleanValue InsertPageBreak
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

		// Token: 0x1700799F RID: 31135
		// (get) Token: 0x06016EFB RID: 93947 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x06016EFC RID: 93948 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "autoShow")]
		public BooleanValue AutoShow
		{
			get
			{
				return (BooleanValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x170079A0 RID: 31136
		// (get) Token: 0x06016EFD RID: 93949 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x06016EFE RID: 93950 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "topAutoShow")]
		public BooleanValue TopAutoShow
		{
			get
			{
				return (BooleanValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x170079A1 RID: 31137
		// (get) Token: 0x06016EFF RID: 93951 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x06016F00 RID: 93952 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "hideNewItems")]
		public BooleanValue HideNewItems
		{
			get
			{
				return (BooleanValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x170079A2 RID: 31138
		// (get) Token: 0x06016F01 RID: 93953 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x06016F02 RID: 93954 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "measureFilter")]
		public BooleanValue MeasureFilter
		{
			get
			{
				return (BooleanValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x170079A3 RID: 31139
		// (get) Token: 0x06016F03 RID: 93955 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x06016F04 RID: 93956 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "includeNewItemsInFilter")]
		public BooleanValue IncludeNewItemsInFilter
		{
			get
			{
				return (BooleanValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x170079A4 RID: 31140
		// (get) Token: 0x06016F05 RID: 93957 RVA: 0x0033094E File Offset: 0x0032EB4E
		// (set) Token: 0x06016F06 RID: 93958 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "itemPageCount")]
		public UInt32Value ItemPageCount
		{
			get
			{
				return (UInt32Value)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x170079A5 RID: 31141
		// (get) Token: 0x06016F07 RID: 93959 RVA: 0x0033095E File Offset: 0x0032EB5E
		// (set) Token: 0x06016F08 RID: 93960 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "sortType")]
		public EnumValue<FieldSortValues> SortType
		{
			get
			{
				return (EnumValue<FieldSortValues>)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x170079A6 RID: 31142
		// (get) Token: 0x06016F09 RID: 93961 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x06016F0A RID: 93962 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "dataSourceSort")]
		public BooleanValue DataSourceSort
		{
			get
			{
				return (BooleanValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x170079A7 RID: 31143
		// (get) Token: 0x06016F0B RID: 93963 RVA: 0x002CB9E8 File Offset: 0x002C9BE8
		// (set) Token: 0x06016F0C RID: 93964 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "nonAutoSortDefault")]
		public BooleanValue NonAutoSortDefault
		{
			get
			{
				return (BooleanValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x170079A8 RID: 31144
		// (get) Token: 0x06016F0D RID: 93965 RVA: 0x0033096E File Offset: 0x0032EB6E
		// (set) Token: 0x06016F0E RID: 93966 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "rankBy")]
		public UInt32Value RankBy
		{
			get
			{
				return (UInt32Value)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x170079A9 RID: 31145
		// (get) Token: 0x06016F0F RID: 93967 RVA: 0x00329FCC File Offset: 0x003281CC
		// (set) Token: 0x06016F10 RID: 93968 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "defaultSubtotal")]
		public BooleanValue DefaultSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x170079AA RID: 31146
		// (get) Token: 0x06016F11 RID: 93969 RVA: 0x00329FDC File Offset: 0x003281DC
		// (set) Token: 0x06016F12 RID: 93970 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "sumSubtotal")]
		public BooleanValue SumSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[33];
			}
			set
			{
				base.Attributes[33] = value;
			}
		}

		// Token: 0x170079AB RID: 31147
		// (get) Token: 0x06016F13 RID: 93971 RVA: 0x00329FEC File Offset: 0x003281EC
		// (set) Token: 0x06016F14 RID: 93972 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "countASubtotal")]
		public BooleanValue CountASubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[34];
			}
			set
			{
				base.Attributes[34] = value;
			}
		}

		// Token: 0x170079AC RID: 31148
		// (get) Token: 0x06016F15 RID: 93973 RVA: 0x002CC6E3 File Offset: 0x002CA8E3
		// (set) Token: 0x06016F16 RID: 93974 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "avgSubtotal")]
		public BooleanValue AverageSubTotal
		{
			get
			{
				return (BooleanValue)base.Attributes[35];
			}
			set
			{
				base.Attributes[35] = value;
			}
		}

		// Token: 0x170079AD RID: 31149
		// (get) Token: 0x06016F17 RID: 93975 RVA: 0x00329FFC File Offset: 0x003281FC
		// (set) Token: 0x06016F18 RID: 93976 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "maxSubtotal")]
		public BooleanValue MaxSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[36];
			}
			set
			{
				base.Attributes[36] = value;
			}
		}

		// Token: 0x170079AE RID: 31150
		// (get) Token: 0x06016F19 RID: 93977 RVA: 0x002CC6F3 File Offset: 0x002CA8F3
		// (set) Token: 0x06016F1A RID: 93978 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "minSubtotal")]
		public BooleanValue MinSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[37];
			}
			set
			{
				base.Attributes[37] = value;
			}
		}

		// Token: 0x170079AF RID: 31151
		// (get) Token: 0x06016F1B RID: 93979 RVA: 0x0032A00C File Offset: 0x0032820C
		// (set) Token: 0x06016F1C RID: 93980 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "productSubtotal")]
		public BooleanValue ApplyProductInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[38];
			}
			set
			{
				base.Attributes[38] = value;
			}
		}

		// Token: 0x170079B0 RID: 31152
		// (get) Token: 0x06016F1D RID: 93981 RVA: 0x0032A01C File Offset: 0x0032821C
		// (set) Token: 0x06016F1E RID: 93982 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(0, "countSubtotal")]
		public BooleanValue CountSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[39];
			}
			set
			{
				base.Attributes[39] = value;
			}
		}

		// Token: 0x170079B1 RID: 31153
		// (get) Token: 0x06016F1F RID: 93983 RVA: 0x002C934A File Offset: 0x002C754A
		// (set) Token: 0x06016F20 RID: 93984 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(0, "stdDevSubtotal")]
		public BooleanValue ApplyStandardDeviationInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[40];
			}
			set
			{
				base.Attributes[40] = value;
			}
		}

		// Token: 0x170079B2 RID: 31154
		// (get) Token: 0x06016F21 RID: 93985 RVA: 0x002D2251 File Offset: 0x002D0451
		// (set) Token: 0x06016F22 RID: 93986 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(0, "stdDevPSubtotal")]
		public BooleanValue ApplyStandardDeviationPInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[41];
			}
			set
			{
				base.Attributes[41] = value;
			}
		}

		// Token: 0x170079B3 RID: 31155
		// (get) Token: 0x06016F23 RID: 93987 RVA: 0x002CD16F File Offset: 0x002CB36F
		// (set) Token: 0x06016F24 RID: 93988 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(0, "varSubtotal")]
		public BooleanValue ApplyVarianceInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[42];
			}
			set
			{
				base.Attributes[42] = value;
			}
		}

		// Token: 0x170079B4 RID: 31156
		// (get) Token: 0x06016F25 RID: 93989 RVA: 0x002D6090 File Offset: 0x002D4290
		// (set) Token: 0x06016F26 RID: 93990 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(0, "varPSubtotal")]
		public BooleanValue ApplyVariancePInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[43];
			}
			set
			{
				base.Attributes[43] = value;
			}
		}

		// Token: 0x170079B5 RID: 31157
		// (get) Token: 0x06016F27 RID: 93991 RVA: 0x002C935A File Offset: 0x002C755A
		// (set) Token: 0x06016F28 RID: 93992 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(0, "showPropCell")]
		public BooleanValue ShowPropCell
		{
			get
			{
				return (BooleanValue)base.Attributes[44];
			}
			set
			{
				base.Attributes[44] = value;
			}
		}

		// Token: 0x170079B6 RID: 31158
		// (get) Token: 0x06016F29 RID: 93993 RVA: 0x002D2261 File Offset: 0x002D0461
		// (set) Token: 0x06016F2A RID: 93994 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(0, "showPropTip")]
		public BooleanValue ShowPropertyTooltip
		{
			get
			{
				return (BooleanValue)base.Attributes[45];
			}
			set
			{
				base.Attributes[45] = value;
			}
		}

		// Token: 0x170079B7 RID: 31159
		// (get) Token: 0x06016F2B RID: 93995 RVA: 0x002C936A File Offset: 0x002C756A
		// (set) Token: 0x06016F2C RID: 93996 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(0, "showPropAsCaption")]
		public BooleanValue ShowPropAsCaption
		{
			get
			{
				return (BooleanValue)base.Attributes[46];
			}
			set
			{
				base.Attributes[46] = value;
			}
		}

		// Token: 0x170079B8 RID: 31160
		// (get) Token: 0x06016F2D RID: 93997 RVA: 0x002D2271 File Offset: 0x002D0471
		// (set) Token: 0x06016F2E RID: 93998 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(0, "defaultAttributeDrillState")]
		public BooleanValue DefaultAttributeDrillState
		{
			get
			{
				return (BooleanValue)base.Attributes[47];
			}
			set
			{
				base.Attributes[47] = value;
			}
		}

		// Token: 0x06016F2F RID: 93999 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotField()
		{
		}

		// Token: 0x06016F30 RID: 94000 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotField(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016F31 RID: 94001 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotField(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016F32 RID: 94002 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotField(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016F33 RID: 94003 RVA: 0x00330980 File Offset: 0x0032EB80
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "items" == name)
			{
				return new Items();
			}
			if (22 == namespaceId && "autoSortScope" == name)
			{
				return new AutoSortScope();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new PivotFieldExtensionList();
			}
			return null;
		}

		// Token: 0x170079B9 RID: 31161
		// (get) Token: 0x06016F34 RID: 94004 RVA: 0x003309D6 File Offset: 0x0032EBD6
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotField.eleTagNames;
			}
		}

		// Token: 0x170079BA RID: 31162
		// (get) Token: 0x06016F35 RID: 94005 RVA: 0x003309DD File Offset: 0x0032EBDD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotField.eleNamespaceIds;
			}
		}

		// Token: 0x170079BB RID: 31163
		// (get) Token: 0x06016F36 RID: 94006 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170079BC RID: 31164
		// (get) Token: 0x06016F37 RID: 94007 RVA: 0x003309E4 File Offset: 0x0032EBE4
		// (set) Token: 0x06016F38 RID: 94008 RVA: 0x003309ED File Offset: 0x0032EBED
		public Items Items
		{
			get
			{
				return base.GetElement<Items>(0);
			}
			set
			{
				base.SetElement<Items>(0, value);
			}
		}

		// Token: 0x170079BD RID: 31165
		// (get) Token: 0x06016F39 RID: 94009 RVA: 0x003309F7 File Offset: 0x0032EBF7
		// (set) Token: 0x06016F3A RID: 94010 RVA: 0x00330A00 File Offset: 0x0032EC00
		public AutoSortScope AutoSortScope
		{
			get
			{
				return base.GetElement<AutoSortScope>(1);
			}
			set
			{
				base.SetElement<AutoSortScope>(1, value);
			}
		}

		// Token: 0x170079BE RID: 31166
		// (get) Token: 0x06016F3B RID: 94011 RVA: 0x00330A0A File Offset: 0x0032EC0A
		// (set) Token: 0x06016F3C RID: 94012 RVA: 0x00330A13 File Offset: 0x0032EC13
		public PivotFieldExtensionList PivotFieldExtensionList
		{
			get
			{
				return base.GetElement<PivotFieldExtensionList>(2);
			}
			set
			{
				base.SetElement<PivotFieldExtensionList>(2, value);
			}
		}

		// Token: 0x06016F3D RID: 94013 RVA: 0x00330A20 File Offset: 0x0032EC20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "axis" == name)
			{
				return new EnumValue<PivotTableAxisValues>();
			}
			if (namespaceId == 0 && "dataField" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "subtotalCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showDropDowns" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hiddenLevel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "uniqueMemberProperty" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "compact" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "allDrilled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "numFmtId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "outline" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "subtotalTop" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragToRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragToCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "multipleItemSelectionAllowed" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragToPage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragToData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dragOff" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showAll" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "insertBlankRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "serverField" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "insertPageBreak" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoShow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "topAutoShow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hideNewItems" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "measureFilter" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "includeNewItemsInFilter" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "itemPageCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sortType" == name)
			{
				return new EnumValue<FieldSortValues>();
			}
			if (namespaceId == 0 && "dataSourceSort" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "nonAutoSortDefault" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rankBy" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "defaultSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sumSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "countASubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "avgSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "maxSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "minSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "productSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "countSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "stdDevSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "stdDevPSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "varSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "varPSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showPropCell" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showPropTip" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showPropAsCaption" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "defaultAttributeDrillState" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016F3E RID: 94014 RVA: 0x00330E55 File Offset: 0x0032F055
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotField>(deep);
		}

		// Token: 0x06016F3F RID: 94015 RVA: 0x00330E60 File Offset: 0x0032F060
		// Note: this type is marked as 'beforefieldinit'.
		static PivotField()
		{
			byte[] array = new byte[48];
			PivotField.attributeNamespaceIds = array;
			PivotField.eleTagNames = new string[] { "items", "autoSortScope", "extLst" };
			PivotField.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009A7B RID: 39547
		private const string tagName = "pivotField";

		// Token: 0x04009A7C RID: 39548
		private const byte tagNsId = 22;

		// Token: 0x04009A7D RID: 39549
		internal const int ElementTypeIdConst = 11103;

		// Token: 0x04009A7E RID: 39550
		private static string[] attributeTagNames = new string[]
		{
			"name", "axis", "dataField", "subtotalCaption", "showDropDowns", "hiddenLevel", "uniqueMemberProperty", "compact", "allDrilled", "numFmtId",
			"outline", "subtotalTop", "dragToRow", "dragToCol", "multipleItemSelectionAllowed", "dragToPage", "dragToData", "dragOff", "showAll", "insertBlankRow",
			"serverField", "insertPageBreak", "autoShow", "topAutoShow", "hideNewItems", "measureFilter", "includeNewItemsInFilter", "itemPageCount", "sortType", "dataSourceSort",
			"nonAutoSortDefault", "rankBy", "defaultSubtotal", "sumSubtotal", "countASubtotal", "avgSubtotal", "maxSubtotal", "minSubtotal", "productSubtotal", "countSubtotal",
			"stdDevSubtotal", "stdDevPSubtotal", "varSubtotal", "varPSubtotal", "showPropCell", "showPropTip", "showPropAsCaption", "defaultAttributeDrillState"
		};

		// Token: 0x04009A7F RID: 39551
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A80 RID: 39552
		private static readonly string[] eleTagNames;

		// Token: 0x04009A81 RID: 39553
		private static readonly byte[] eleNamespaceIds;
	}
}
